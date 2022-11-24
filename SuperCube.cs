using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCube : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float upSpeed;
    private float wallLimit = 9;
    private Transform cube;
    Rigidbody cubeRigidbody;
    BoxCollider cubeBoxCollider;
    void Start()
    {
        cube = transform;

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
        Rigidbody cubeRigidbody = cube.transform.GetComponent<Rigidbody>();
        BoxCollider cubeBoxCollider = cube.transform.GetComponent<BoxCollider>();
        if (other.GetComponent<UniWall>())
        {

            // print("WallContact");
            // cubeRigidbody.useGravity = true;
            // cubeBoxCollider.isTrigger = false;

        }
        if (other.GetComponent<UniCube>())
        {
            print("Other Cube Contact");
            // Rigidbody otherCubeRigidbody = other.transform.GetComponent<Rigidbody>();
            // BoxCollider otherCubeBoxCollider = other.transform.GetComponent<BoxCollider>();
            // if (!otherCubeRigidbody.useGravity == true)
            // {
            //     MoveUpOnStep(1);
            // }

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
