using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDesiredCubes : MonoBehaviour
{
    private Vector3 MoveDownPosition = new Vector3(0f, -1f, 0f);
    public void MoveDownUpCubes(List<Collider> hitColliders)
    {
        for (int i = 0; i < hitColliders.Count; i++)
        {
            var universalCube = hitColliders[i].gameObject.GetComponent<UniversalCube>();
            universalCube.transform.position += MoveDownPosition;
        }
    }
}
