using System.Collections;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;
using MoreMountains.Feedbacks;
// using UnityEngine.Events;
// using System.Threading;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] cubePrefab = new GameObject[7];
    // public int cubeCount;
    public float spawnSpeed;
    private Transform player;
    [SerializeField] private int count;
    [SerializeField] private int dividentCount;
    public int Count
    {
        get { return count; }
        set { count = value; }
    }
    [SerializeField] private int secondGunCount;
    public int SecondGunCount
    {
        get { return secondGunCount; }
        set { secondGunCount = value; }
    }
    private GameObject spawnCube;
    NativeArray<Vector3> bridge;
    NativeArray<Vector3> bridge2;
    private SoundManager soundManager;
    private TMPro.TextMeshPro spawnCubeCount;
    private Animator playerAnimator;
    private Chamber chamber;
    private Animator gunAnimator;
    private Animator gunAnimator2;
    private ParticleSystem gunFireFX;
    private ParticleSystem gunFireFX2;
    private bool playerMove;
    // private int warningCount = 0;
    [SerializeField] private int firstWarning;
    [SerializeField] private int secondWarning;
    [SerializeField] private int thirdWarning;
    // private Gun blueGun;
    // [SerializeField] private Material materialBlue;
    // [SerializeField] private Material materialWhite;
    private static Color colorBlue = new Color(61, 221, 255);
    private static Color colorWhite = new Color(255, 255, 255);
    private static Color colorRed = new Color(255, 0, 0);
    // public Coroutine redWarning;
    private PlayerController playerController;
    private bool isFire = true;
    private LevelManager levelManager;
    private bool isDoubleFire = false;
    private static Vector3 doubleFireOffset = new Vector3(-1f, 0, 0);
    private static Vector3 cubeSpawnOffset = new Vector3(0, 0, 3);
    private int number;
    private Vector3 secondCubeVector;
    private bool isFirstDoubleFire = true;
    private int fireCounter = 0;
    private Chamber2 chamber2;
    private AmmoCounter ammoCounter;
    private Animator ammoCounterAnimator;
    private Vector3 cube1Vector;
    private Vector3 cube2Vector;
    private bool isSpeedFire;
    private Coroutine normalFireCoroutine;
    private int speedFireCount = 0;
    private int doubeFireCount = 0;
    private RedWarning redWarning;
    private Coroutine redWarningCoroutine;
    private bool isRedWarningOn = false;
    private float redRedWarningSecond = 0.15f;
    private Alert alert;
    private Coroutine redAlert;
    private float alertSecond = 0.3f;
    // [SerializeField] MMFeedbacks MmFxs;



    [BurstCompile]
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        // player = GameObject.Find("Player").transform;
        levelManager = FindObjectOfType<LevelManager>();
        InvokeRepeating("SpawnCube", 0, spawnSpeed);
        soundManager = FindObjectOfType<SoundManager>();
        spawnCubeCount = FindObjectOfType<SpawnCubeCount>().GetComponent<TMPro.TextMeshPro>();
        playerAnimator = player.GetComponent<Animator>();
        chamber = FindObjectOfType<Chamber>();
        gunAnimator = FindObjectOfType<Gun>().gameObject.GetComponent<Animator>();
        gunAnimator2 = FindObjectOfType<Gun2>(true).gameObject.GetComponent<Animator>();


        gunFireFX = player.GetComponentInChildren<ParticleSystem>();
        gunFireFX2 = FindObjectOfType<GunFX2>(true).GetComponent<ParticleSystem>();
        chamber.SetAmmoInChamber(count);
        // blueGun = player.gameObject.GetComponentInChildren<Gun>();
        // materialBlue.color = colorBlue / 255f;
        playerController = player.gameObject.GetComponent<PlayerController>();
        chamber2 = FindObjectOfType<Chamber2>(true);
        ammoCounter = FindObjectOfType<AmmoCounter>();
        ammoCounterAnimator = ammoCounter.GetComponent<Animator>();
        redWarning = FindObjectOfType<RedWarning>(true);
        alert = FindObjectOfType<Alert>(true);
        // ColorUtility.TryParseHtmlString("FFFFFF", out colorWhite);
        // Shaded white 70BFFF
        // ColorUtility.TryParseHtmlString("FF0000", out colorRed);
        // ColorUtility.TryParseHtmlString("3DDDFF", out colorBlue);
        // Shaded blue 456DFF

    }
    private void FixedUpdate()
    {
        if (playerMove)
        {
            // StopCoroutine(redWarning);
            // StopAllCoroutines();
            isFire = true;
        }
        if (isSpeedFire && speedFireCount <= 0)
        {
            NormalFire();

        }
        if (isDoubleFire && doubeFireCount <= 0)
        {
            NormalFire();
            DoubleFireOff();
        }

        // print(speedFireCount);
        // print(warningCount);
    }
    [BurstCompile]
    public void AddSpawnCubes(int newcount)
    {
        count += newcount;
        spawnCubeCount.fontSize = 8;
        StartCoroutine(FontSizeNormalize());
        chamber.AddAmmoInChamber(count);
    }
    public void AddDoubleCubes(int newcount)
    {
        doubeFireCount += newcount;
    }
    public void AddSpeedCubes(int newcount)
    {
        speedFireCount += newcount;
    }
    public void AddSpawnSecondGunCubes(int newcount)
    {
        secondGunCount += newcount;
    }
    public void StopAlert()
    {
        StopCoroutine(redAlert);
        alert.gameObject.SetActive(false);
    }
    // public void StopSpawn()
    // {
    //     StartCoroutine(RedWarning());
    //     isFire = false;
    // }
    [BurstCompile]
    void SpawnCube()
    {
        if (count > 0 && isFire)
        {

            if (count <= 10 && !isRedWarningOn)
            {
                // print("start red warning");
                redWarningCoroutine = StartCoroutine(RedWarningAlert());
                redAlert = StartCoroutine(RedAlert());
                isRedWarningOn = true;
            }
            else if (count > 10)
            {
                if (redWarningCoroutine != null)
                {
                    StopCoroutine(redWarningCoroutine);
                    StopCoroutine(redAlert);
                    isRedWarningOn = false;
                }
                redWarning.gameObject.SetActive(false);
                alert.gameObject.SetActive(false);
            }


            // if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAddCubes"))
            // {
            //     // print("PlayerNotAddsCubes");
            //     playerAnimator.Play("PlayerSpitsOut");
            // }
            if (!isDoubleFire)
            {
                gunAnimator.Play(StaticData.GunAnimator);
                gunFireFX.Play();


                bridge = new NativeArray<Vector3>(length: 2, Allocator.TempJob);
                //float3[]
                bridge[0] = player.position + cubeSpawnOffset;
                var SpawnJob = new SpawnCubeInRound()
                {
                    Bridge = bridge
                };
                JobHandle spawnHandle = SpawnJob.Schedule();
                spawnHandle.Complete();
                SpawnLevelCube();
                bridge.Dispose();


                soundManager.ForwardCubeSoundPlay();
                count--;
                if (isSpeedFire)
                {
                    speedFireCount--;
                }
                if (count % dividentCount == 0)
                {
                    chamber.SetAmmoInChamber(count);
                }
            }
            else if (isDoubleFire)
            {
                if (isFirstDoubleFire)
                {
                    fireCounter = 0;
                    gunAnimator2.gameObject.SetActive(true);
                    gunAnimator2.enabled = true;
                    gunAnimator2.Play(StaticData.CreateSecondGun);
                    ammoCounterAnimator.Play(StaticData.MoveToDoubleChetchick);
                    isFirstDoubleFire = false;
                }

                gunAnimator.Play(StaticData.GunAnimator);
                gunFireFX.Play();
                // MmFxs.PlayFeedbacks();

                bridge = new NativeArray<Vector3>(length: 2, Allocator.TempJob);
                //float3[]
                bridge[0] = player.position + cubeSpawnOffset;
                var SpawnJob = new SpawnCubeInRound()
                {
                    Bridge = bridge
                };
                JobHandle spawnHandle = SpawnJob.Schedule();
                spawnHandle.Complete();
                SpawnLevelCube();
                bridge.Dispose();
                soundManager.ForwardCubeSoundPlay();
                if (!isFirstDoubleFire)
                {
                    secondGunCount--;
                }
                StartCoroutine(DelayFire());
                count -= 2;
                doubeFireCount -= 1;

                if (count % dividentCount == 0)
                {
                    chamber.SetAmmoInChamber(count);
                }

            }
        }


    }
    public void DoubleFireOn()
    {
        isDoubleFire = true;
        isFirstDoubleFire = true;
    }
    public void DoubleFireOff()
    {
        playerController.DoubleFireOff();
        isDoubleFire = false;
        gunAnimator2.Play(StaticData.DestroySecondGun);
        ammoCounterAnimator.Play(StaticData.MoveToStartChetchick);
        StartCoroutine(DestroyAfter());
    }
    private void SpawnLevelCube()
    {
        for (int i = 0; i < cubePrefab.Length; i++)
        {
            if ("Level" + (i + 1) == levelManager.CurrentScene)
            {
                spawnCube = Instantiate(cubePrefab[i], bridge[1], cubePrefab[i].transform.rotation);
                // GameObject spawnCube = ObjectPool.instance.GetPooledObject();
                // if (spawnCube != null)
                // {
                //     spawnCube.transform.position = bridge[1];
                //     spawnCube.SetActive(true);
                // }
                spawnCube.GetComponent<UniversalCube>().cubeState = UniversalCube.CubeState.MoveForward;
            }
        }
    }
    private void SpawnLevel2Cube()
    {
        for (int i = 0; i < cubePrefab.Length; i++)
        {
            if ("Level" + (i + 1) == levelManager.CurrentScene)
            {
                spawnCube = Instantiate(cubePrefab[i], bridge[1] + doubleFireOffset, cubePrefab[i].transform.rotation);
                spawnCube.GetComponent<UniversalCube>().cubeState = UniversalCube.CubeState.MoveForward;
                // number = i;
                // secondCubeVector = bridge[1];
                // StartCoroutine(DelayFire(i));
            }
        }
    }
    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(0.12f);
        gunAnimator2.gameObject.SetActive(false);
        gunAnimator2.enabled = false;
        gunAnimator2.gameObject.transform.localScale = new Vector3(1.98f, 1.98f, 1.98f);
    }
    IEnumerator DelayFire()
    {
        yield return new WaitForSeconds(0.1f);
        fireCounter += 1;
        chamber2.SetAmmoInChamber(secondGunCount);
        if (fireCounter > 1)
        {
            // gunAnimator2.enabled = true;
            gunAnimator2.Play(StaticData.GunAnimator);
        }
        gunFireFX2.Play();


        bridge = new NativeArray<Vector3>(length: 2, Allocator.TempJob);
        //float3[]
        bridge[0] = player.position + cubeSpawnOffset;
        var SpawnJob = new SpawnCubeInRound()
        {
            Bridge = bridge
        };
        JobHandle spawnHandle = SpawnJob.Schedule();
        spawnHandle.Complete();
        SpawnLevel2Cube();
        bridge.Dispose();

        soundManager.ForwardCubeSoundPlay();
    }
    IEnumerator FontSizeNormalize()
    {
        yield return new WaitForSeconds(0.1f);
        spawnCubeCount.fontSize = 6;
    }
    IEnumerator GunBreaksDown()
    {
        yield return new WaitForSeconds(1f);
        gunAnimator.Play(StaticData.GunBreaksDown);
    }
    IEnumerator NormalFireAfter()
    {
        CancelInvoke("SpawnCube");
        yield return new WaitForSeconds(0.3f);
        spawnSpeed = 0.5f;
        InvokeRepeating("SpawnCube", 0, spawnSpeed);
    }
    IEnumerator RedWarningAlert()
    {
        bool isActive = false;
        for (int i = 0; i < 30; i++)
        {
            isActive = !isActive;
            redWarning.gameObject.SetActive(isActive);
            yield return new WaitForSeconds(redRedWarningSecond);
        }
    }
    IEnumerator RedAlert()
    {
        bool isActive = false;
        for (int i = 0; i < 30; i++)
        {
            isActive = !isActive;
            alert.gameObject.SetActive(isActive);
            yield return new WaitForSeconds(alertSecond);
        }
    }
    // public IEnumerator RedWarning()
    // {
    //     materialBlue.color = colorRed / 255f;
    //     yield return new WaitForSeconds(0.2f);
    //     materialBlue.color = colorBlue / 255f;
    //     yield return new WaitForSeconds(0.2f);
    //     materialBlue.color = colorRed / 255f;
    //     yield return new WaitForSeconds(0.1f);
    //     materialBlue.color = colorBlue / 255f;
    //     yield return new WaitForSeconds(0.1f);
    //     materialBlue.color = colorRed / 255f;
    //     yield return new WaitForSeconds(0.05f);
    //     materialBlue.color = colorBlue / 255f;
    //     yield return new WaitForSeconds(0.05f);
    //     materialBlue.color = colorRed / 255f;
    // }
    public void NormalFire()
    {
        isSpeedFire = false;
        normalFireCoroutine = StartCoroutine(NormalFireAfter());
    }
    public void AddSpeedFire()
    {
        CancelInvoke("SpawnCube");
        spawnSpeed = 0.3f;
        InvokeRepeating("SpawnCube", 0, spawnSpeed);
        isSpeedFire = true;
    }

    public void SetPlayerMove(bool move)
    {
        playerMove = move;

        if (move)
        {
            // warningCount = 0;
            // materialBlue.color = colorBlue / 255f;
        }
    }
}

[BurstCompile]
public struct SpawnCubeInRound : IJob
{
    public NativeArray<Vector3> Bridge;
    public void Execute()
    {
        Bridge[1] = GetSpawnPosition(Bridge[0]);
    }

    private Vector3 GetSpawnPosition(Vector3 player)
    {
        return new Vector3(Mathf.Round(player.x), player.y, player.z);
    }
}
[BurstCompile]
public struct Spawn2CubeInRound : IJob
{
    public NativeArray<Vector3> Bridge;
    public void Execute()
    {
        Bridge[1] = GetSpawnPosition(Bridge[0]);
        Bridge[3] = GetSpawnPosition(Bridge[2]);
    }

    private Vector3 GetSpawnPosition(Vector3 player)
    {
        return new Vector3(Mathf.Round(player.x), player.y, player.z);
    }
}


