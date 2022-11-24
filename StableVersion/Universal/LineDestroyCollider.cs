using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDestroyCollider : MonoBehaviour
{
    public bool destroyAll = false;
    public List<GameObject> cubes;
    private void OnTriggerStay(Collider other)
    {
        if (destroyAll && other.GetComponent<UniversalCube>())
        {
            if (!cubes.Contains(other.gameObject))
            {
                cubes.Add(other.gameObject);
                print(cubes.Count);
                for (int i = 0; i < cubes.Count; i++)
                {
                    Destroy(cubes[i].gameObject);
                }

            }


        }
    }
}
