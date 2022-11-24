using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpCubesCollider : MonoBehaviour
{
    private Collider[] hitColliders;
    private Transform cubeCollider;
    public LayerMask layerMask;
    private Vector3 MoveUpCubesDownColliderBoxScale = new Vector3(2f, 14f, 0.5f);
    private Vector3 offset = new Vector3(0f, 9f, 0f);
    private Vector3 MoveUpThreeCubesDownColliderBoxScale = new Vector3(2f, 2.5f, 0.5f);
    private Vector3 offsetThreeBox = new Vector3(0f, 3f, 0f);

    private Vector3 MoveUpOneCubesDownColliderBoxScale = new Vector3(2f, 0.5f, 0.5f);
    private Vector3 offsetOneBox = new Vector3(0f, 2f, 0f);
    private List<Collider> list = new List<Collider>();
    private Vector3 MoveDownPosition = new Vector3(0f, -3f, 0f);
    private UniversalCube universalCube;

    private void Start()
    {
        cubeCollider = transform;
    }
    void FixedUpdate()
    {
        hitColliders = Physics.OverlapBox(this.transform.position + offsetOneBox, MoveUpOneCubesDownColliderBoxScale / 2, Quaternion.identity, layerMask);

        // print("hitColliders Length" + hitColliders.Length);
        if (hitColliders.Length > 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (!list.Contains(hitColliders[i]))
                    list.Add(hitColliders[i]);
            }
            // moveDesiredCubes.MoveDownUpCubes(hitColliders);
        }
        // print(list.Count);
        // print(hitColliders.Length);
    }
    public void FallDownUpCubes()
    {

        if (list.Count > 0)
        {

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    universalCube = list[i].gameObject.GetComponent<UniversalCube>();
                    StartCoroutine(FallAfter(universalCube));
                }

            }
        }
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(this.transform.position + offsetOneBox, MoveUpOneCubesDownColliderBoxScale);
    }
    IEnumerator FallAfter(UniversalCube universalCube)
    {
        yield return new WaitForSeconds(0.1f);
        if (universalCube != null)
        {
            universalCube.FallDown = true;
            Destroy(cubeCollider.gameObject);
        }

    }
}
