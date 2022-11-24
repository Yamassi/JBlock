
using UnityEngine;

public class UniCube : MonoBehaviour
{

    [SerializeField] private float forwardSpeed;
    [SerializeField] private float upSpeed;
    public bool isStatic;
    // public bool IsStatic
    // {
    //     get { return isStatic; }
    // }
    private float wallLimit = 9;
    private Transform cube;
    Rigidbody cubeRigidbody;
    BoxCollider cubeBoxCollider;
    void Start()
    {
        cube = transform;
        cubeRigidbody = cube.transform.GetComponent<Rigidbody>();
        cubeBoxCollider = cube.transform.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cube.position.z != 9)
        {
            MoveForward();
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<UniWall>())
        {
            cubeRigidbody.useGravity = true;
            cubeBoxCollider.isTrigger = false;
        }
        if (other.GetComponent<UniCube>())
        {
            Rigidbody otherCubeRigidbody = other.transform.GetComponent<Rigidbody>();
            BoxCollider otherCubeBoxCollider = other.transform.GetComponent<BoxCollider>();
            if (!otherCubeRigidbody.useGravity == true)
            {
                MoveUpOnStep(1);
            }

        }
    }

    public void MoveUpOnStep(int stepAmount)
    {
        cube.position = new Vector3(cube.position.x, cube.position.y + stepAmount, cube.position.z);
    }
    public void MoveDownOnStep(int stepAmount)
    {
        cube.position = new Vector3(cube.position.x, cube.position.y - stepAmount, cube.position.z);
    }

    void MoveForward()
    {
        cube.Translate(cube.forward * forwardSpeed * Time.deltaTime);

        if (cube.position.z > wallLimit)
        {
            cube.position = new Vector3(cube.position.x, cube.position.y, wallLimit);
        }
    }
}
