using UnityEngine;
using Unity.Burst;

public class PlayerController : MonoBehaviour
{
    Transform player;
    [SerializeField] private float speed;
    [SerializeField] private LevelsSettings levelsSettings;
    private float borderLimit = 3;
    private float borderLimitForDoubleGun = 2;
    // private float horizontalMove = 0;
    private DynamicJoystick dynamicJoystick;
    private Menu menu;
    private SpawnManager spawnManager;
    // private Smoke smoke;
    private ParticleSystem smokeParticle;
    private Touch touch;
    private bool isDoubleFire = false;
    private float horizontalMove;

    [BurstCompile]
    private void Awake()
    {
        speed = levelsSettings.PlayerSpeed;
        dynamicJoystick = FindObjectOfType<DynamicJoystick>();
        menu = FindObjectOfType<Menu>();
        spawnManager = FindObjectOfType<SpawnManager>(true);
        // smoke = FindObjectOfType<Smoke>(true);
        // smokeParticle = smoke.gameObject.GetComponent<ParticleSystem>();

    }
    void Start()
    {
        player = transform;
    }
    [BurstCompile]

    void Update()
    {
        // horizontalMove = dynamicJoystick.Direction.x;

        // if (horizontalMove == 0)
        // {
        // StopSpawnAnimation();
        // }
        // if (horizontalMove > 0 || horizontalMove < 0)
        // {
        //     spawnManager.SetPlayerMove(true);
        //     SmokeOff();
        // }

        // if (horizontalMove != 0 && menu.IsFirstEdu)
        // {
        //     menu.IsFirstEdu = false;
        //     menu.StartGame();
        // }
        // GridMove();
        TouchControl2();
        Keyboard();


        // transform.Translate(Vector3.right * horizontalMove * speed * Time.deltaTime);
        // print(horizontalMove);
        if (!isDoubleFire)
        {
            if (player.position.x > borderLimit)
            {
                player.position = new Vector3(borderLimit, player.position.y, player.position.z);

            }
            else if (player.position.x < -borderLimit)
            {
                player.position = new Vector3(-borderLimit, player.position.y, player.position.z);
            }
        }
        if (isDoubleFire)
        {
            if (player.position.x > borderLimit)
            {
                player.position = new Vector3(borderLimit, player.position.y, player.position.z);

            }
            else if (player.position.x < -borderLimitForDoubleGun)
            {
                player.position = new Vector3(-borderLimitForDoubleGun, player.position.y, player.position.z);
            }
        }

    }
    public void Keyboard()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        player.Translate(Vector3.right * horizontalMove * (speed * 1000) * Time.deltaTime);
    }
    public void DoubleFireOn()
    {
        isDoubleFire = true;
    }
    public void DoubleFireOff()
    {
        isDoubleFire = false;
    }
    public void StopSpawnAnimation()
    {
        spawnManager.SetPlayerMove(false);
        // SmokeOn();
    }

    [BurstCompile]
    public void MoveLeft()
    {
        horizontalMove = -1;
        // print("left move");
    }
    [BurstCompile]
    public void MoveRight()
    {
        horizontalMove = +1;
        // print("right move");
    }
    [BurstCompile]
    public void NoMove()
    {
        // horizontalMove = 0;

    }
    // public void SmokeOn()
    // {

    //     smoke.gameObject.SetActive(true);
    //     smokeParticle.Play();

    // }
    // public void SmokeOff()
    // {
    //     smoke.gameObject.SetActive(false);
    // }

    public void TouchControl2()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                spawnManager.SetPlayerMove(true);
                // SmokeOff();
            }

            if (touch.phase == TouchPhase.Moved && menu.IsFirstEdu)
            {
                menu.IsFirstEdu = false;
                menu.StartGame();
            }
            if (touch.phase == TouchPhase.Moved)
            {

                player.position = new Vector3(transform.position.x + touch.deltaPosition.x * speed, player.position.y, player.position.z);
            }
        }
    }
}
