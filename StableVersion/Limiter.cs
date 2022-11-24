using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limiter : MonoBehaviour
{

    private Transform limiter;
    private WallCollider wallCollider;

    void Start()
    {
        wallCollider = FindObjectOfType<WallCollider>();
        limiter = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        limiter.position = new Vector3(0, wallCollider.HighCubeCount - 0.5f, 8.5f);
    }
}
