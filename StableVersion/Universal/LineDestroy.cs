using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDestroy : MonoBehaviour
{
    LineDestroyCollider lineDestroyCollider;
    private void Start()
    {
        lineDestroyCollider = gameObject.GetComponentInChildren<LineDestroyCollider>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<UniversalCube>())
        {
            lineDestroyCollider.destroyAll = true;
        }

    }
}
