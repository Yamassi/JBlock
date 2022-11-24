
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private Transform cube;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float upSpeed;
    private float wallLimit = 9;
    public bool isStatic = true;
    void Start()
    {
        cube = transform;
    }
    void Update()
    {
        MoveForward();
    }
    void MoveForward()
    {
        cube.Translate(cube.forward * forwardSpeed * Time.deltaTime);

        if (cube.position.z > wallLimit)
        {
            cube.position = new Vector3(cube.position.x, cube.position.y, wallLimit);
        }
    }

    public void MoveUpOnStep(int stepAmount)
    {
        cube.position = new Vector3(cube.position.x, stepAmount, cube.position.z);
    }
    public void MoveDownOnOneStep()
    {
        // print("before" + cube.position);
        cube.position = new Vector3(cube.position.x, cube.position.y - 1, cube.position.z);
        // print("after" + cube.position);
    }
}
