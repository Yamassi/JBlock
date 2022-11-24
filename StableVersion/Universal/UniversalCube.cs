using System.Collections;
using UnityEngine;


public class UniversalCube : MonoBehaviour
{
    private ParticleSystem FX;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float upSpeed;
    private float wallLimit = 9;
    private Transform cube;
    [SerializeField]
    public enum CubeState
    {
        Stay,
        FallDown,
        MoveForward,
        MoveUp,
        Fly,
    }
    public CubeState cubeState = 0;
    [SerializeField] private bool fallDown = false;
    public bool FallDown
    {
        get { return fallDown; }
        set { fallDown = value; }
    }
    [SerializeField] private bool isBoom = false;
    public bool IsBoom
    {
        get { return isBoom; }
        set { isBoom = value; }
    }
    [SerializeField] private bool dynamic;
    public bool Dinamic
    {
        get { return dynamic; }
    }
    [SerializeField] private bool staticCube;

    [SerializeField] private bool isOnTheWall;
    private UniversalCube otherCube;
    private UniversalCube universalCube;
    private FallCollider fallCollider;
    private Animator animator;
    [SerializeField] private bool playerForwardFly;
    private DestroyAnimation destroyAnimation;
    private int RandomBoomAnimation;
    private int RandomExplodeAnimation;
    public float lastTimeOnPosition;
    private SpawnManager spawnManager;
    private FXs fXs;
    void Start()
    {
        cube = transform;
        universalCube = cube.GetComponent<UniversalCube>();
        fallCollider = cube.GetComponentInChildren<FallCollider>(true);
        if (dynamic)
        {
            animator = GetComponent<Animator>();
        }
        destroyAnimation = cube.GetComponentInChildren<DestroyAnimation>(true);
        RandomBoomAnimation = Animator.StringToHash("Boom" + Random.Range(1, 7).ToString());
        RandomExplodeAnimation = Animator.StringToHash("Explode" + Random.Range(1, 7).ToString());
        FX = GetComponentInChildren<ParticleSystem>(true);
        spawnManager = FindObjectOfType<SpawnManager>(true);
        // 
        fXs = FindObjectOfType<FXs>();
        lastTimeOnPosition = Time.time;



    }
    void FixedUpdate()
    {

        if (cube.position.z != 9 && playerForwardFly)
        {
            MoveForward();
        }
        //Включение искусственной гравитации
        if (fallDown)
        {
            if (fallCollider != null)
            {
                fallCollider.gameObject.SetActive(true);
                MoveDownOnStep(1f);
            }

        }

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<UniWall>() && !isOnTheWall)
        {
            PlayUpCubeAnimation();
            isOnTheWall = true;
            playerForwardFly = false;
            cubeState = CubeState.Stay;
            lastTimeOnPosition = Time.time;
        }
        if (other.GetComponent<UniversalCube>())
        {

            otherCube = other.GetComponent<UniversalCube>();

            if (Mathf.Round(cube.position.y) == Mathf.Round(other.transform.position.y))
            {
                PlayUpCubeAnimation();
                PlayUpOtherCubeAnimation(otherCube);
                if (universalCube.lastTimeOnPosition > otherCube.lastTimeOnPosition)
                {
                    // print("другой куб улетает вверх");
                    // print(otherCube.lastTimeOnPosition);
                    otherCube.lastTimeOnPosition = Time.time;
                    universalCube.lastTimeOnPosition = Time.time;
                    otherCube.MoveUpOnStep(1);

                    // print(Mathf.Round(universalCube.transform.position.y));
                }
                // else if (universalCube.lastTimeOnPosition < otherCube.lastTimeOnPosition)
                // {
                //     print("куб улетает вверх");
                //     universalCube.MoveUpOnStep(1);
                //     otherCube.lastTimeOnPosition = Time.time;
                //     universalCube.lastTimeOnPosition = Time.time;
                // }
            }
            //Куб летит вперед и при столкновении двигает встретившийся куб вверх
            // if (cube.position.z < otherCube.transform.position.z)
            // {
            //     // print("Куб летит вперед");

            //     otherCube.MoveUpOnStep(1f);
            //     otherCube.cubeState = CubeState.MoveUp;
            //     StartCoroutine(MoveUpOnStayOtherCube());

            // }
            //Куб летит вверх
            // if (!fallDown && otherCube.cubeState == CubeState.Stay)
            // {
            //     // print("Куб летит вверх");
            //     otherCube.MoveUpOnStep(1f);
            //     otherCube.cubeState = CubeState.MoveUp;
            //     StartCoroutine(MoveUpOnStayOtherCube());
            // }

        }
        if (other.GetComponent<Floor>() && fallDown)
        {
            fallCollider.gameObject.SetActive(false);
            fallDown = false;
            MoveOnZero();
        }
        if (other.GetComponent<Chamber>())
        {

            FX.transform.parent = fXs.gameObject.transform;
            FXPlay();
            spawnManager.AddSpawnCubes(10);
            Destroy(cube.gameObject);
        }

    }

    public void MoveUpOnStep(float stepAmount)
    {
        cube.position = new Vector3(cube.position.x, cube.position.y + stepAmount, cube.position.z);
    }
    // public void StayOnThePosition()
    // {
    //     cube.position = new Vector3(cube.position.x, cube.position.y, cube.position.z);
    // }
    public void MoveDownOnStep(float stepAmount)
    {
        cube.position = new Vector3(cube.position.x, cube.position.y - stepAmount, cube.position.z);
        // StartCoroutine(MoveDownSlow(stepAmount));
    }
    public void MoveDownOnStepWithAnimation(float stepAmount)
    {
        // cube.position = new Vector3(cube.position.x, cube.position.y - stepAmount, cube.position.z);
        StartCoroutine(MoveDownSlow(stepAmount));
        PlayUpCubeAnimation();
    }
    public void MoveOnZero()
    {
        cube.position = new Vector3(cube.position.x, 0, cube.position.z);
    }

    private void MoveForward()
    {
        cube.Translate(cube.forward * forwardSpeed * Time.fixedDeltaTime);

        if (cube.position.z > wallLimit)
        {
            cube.position = new Vector3(cube.position.x, cube.position.y, wallLimit);
        }
    }
    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;

        if (dynamic)
        {
            if (destroyAnimation != null)
            {
                destroyAnimation.transform.parent = fXs.gameObject.transform;
                destroyAnimation.enabled = true;
                destroyAnimation.gameObject.SetActive(true);
                if (isBoom)
                {
                    destroyAnimation.gameObject.GetComponentInChildren<Animator>().Play(RandomBoomAnimation);
                }
                else
                {
                    destroyAnimation.transform.position = new Vector3(cube.position.x, -0.5f, cube.position.z);
                    destroyAnimation.gameObject.GetComponentInChildren<Animator>().Play(RandomExplodeAnimation);
                }

                destroyAnimation.DestroyAnimationFX();
                FX.transform.parent = fXs.gameObject.transform;
                FXPlay();

            }
        }


        // FXPlay();

    }
    IEnumerator PlayFXAfter()
    {
        yield return new WaitForSeconds(0.3f);

    }
    IEnumerator MoveUpOnStayOtherCube()
    {
        yield return new WaitForSeconds(0.08f);
        otherCube.cubeState = CubeState.Stay;
    }
    IEnumerator CubeSpawnAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        playerForwardFly = false;

    }
    IEnumerator MoveUpOnStayCube()
    {
        yield return new WaitForSeconds(0.07f);
        cubeState = CubeState.Stay;
    }
    IEnumerator MoveDownSlow(float stepAmount)
    {

        cube.position = new Vector3(cube.position.x, cube.position.y - stepAmount / 5, cube.position.z);
        cubeState = CubeState.FallDown;
        yield return new WaitForSeconds(0.01f);
        cube.position = new Vector3(cube.position.x, cube.position.y - stepAmount / 5, cube.position.z);
        yield return new WaitForSeconds(0.01f);
        cube.position = new Vector3(cube.position.x, cube.position.y - stepAmount / 5, cube.position.z);
        yield return new WaitForSeconds(0.01f);
        cube.position = new Vector3(cube.position.x, cube.position.y - stepAmount / 5, cube.position.z);
        yield return new WaitForSeconds(0.01f);
        cube.position = new Vector3(cube.position.x, cube.position.y - stepAmount / 5, cube.position.z);
        cubeState = CubeState.Stay;
    }
    IEnumerator AnimatorOffAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        animator.enabled = false;
    }
    IEnumerator OtherAnimatorOffAfter(float seconds, UniversalCube otherCube)
    {
        yield return new WaitForSeconds(seconds);
        if (otherCube != null)
        {
            otherCube.animator.enabled = false;
        }

    }
    public void PlayUpCubeAnimation()
    {
        if (dynamic)
        {
            // animator.playbackTime = 0;
            animator.enabled = true;
            animator.Play(StaticData.CubeUpAnimation);
            // StartCoroutine(AnimatorOffAfter(1f));
        }
    }
    // public void PlayDownCubeAnimation()
    // {

    //     // animator.playbackTime = 0;
    //     animator.Play("CubeDownAnimation");

    // }
    public void PlayUpOtherCubeAnimation(UniversalCube otherCube)
    {
        if (otherCube.Dinamic)
        {
            // otherCube.animator.playbackTime = 0;
            otherCube.animator.enabled = true;
            otherCube.animator.Play(StaticData.CubeUpAnimation);
            // StartCoroutine(OtherAnimatorOffAfter(1f, otherCube));
        }
    }
    public void FXPlay()
    {
        if (FX != null)
        {
            FX.gameObject.transform.SetParent(fXs.gameObject.transform);
            FX.transform.gameObject.SetActive(true);
        }

        // 
        // FX.Play();
        // FX.GetComponent<FXDestroy>().DestroyFXAfter(1);
    }
}

//Замедление объектов в update
// float timerFallDown;
// float timeToFallDown;
// timerFallDown += Time.fixedDeltaTime;

// if (fallDown && timerFallDown >= timeToFallDown)
// {
//     timerFallDown = 0;
//     MoveDownOnStep(1);
// }