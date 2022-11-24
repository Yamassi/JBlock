using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppFPS : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 1000;
    }
}
