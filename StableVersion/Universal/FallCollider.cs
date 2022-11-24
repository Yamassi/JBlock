
using UnityEngine;
using Unity.Burst;
public class FallCollider : MonoBehaviour
{
    private Transform fallCollider;
    // public LayerMask layerMask;
    // private Collider[] hitColliders = new Collider[1];
    // private int numHitColliders;
    private UniversalCube universalCube;
    void Start()
    {
        fallCollider = transform;
        universalCube = GetComponentInParent<UniversalCube>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<UniversalCube>())
        {
            if (universalCube!)
            {
                universalCube.FallDown = false;
                universalCube.cubeState = UniversalCube.CubeState.Stay;
                fallCollider.gameObject.SetActive(false);
            }

        }
        if (other.GetComponent<Floor>())
        {
            if (universalCube!)
            {
                universalCube.FallDown = false;
                universalCube.cubeState = UniversalCube.CubeState.Stay;
                fallCollider.gameObject.SetActive(false);
            }

        }


    }
    // void FixedUpdate()
    // {

    //     numHitColliders = Physics.OverlapSphereNonAlloc(this.transform.position, 0.1f, hitColliders, layerMask);
    //     print(numHitColliders);
    //     if (numHitColliders > 0)
    //     {

    //         if (hitColliders[0].transform.GetComponent<UniversalCube>())
    //         {
    //             universalCube.FallDown = false;
    //             universalCube.cubeState = UniversalCube.CubeState.Stay;
    //         }
    //         if (hitColliders[0].transform.GetComponent<Floor>())
    //         {
    //             universalCube.FallDown = false;
    //             universalCube.cubeState = UniversalCube.CubeState.Stay;
    //         }

    //     }
    // }
    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(this.transform.position, 0.1f);
    // }
}
