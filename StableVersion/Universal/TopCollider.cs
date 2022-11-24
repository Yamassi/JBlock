
using UnityEngine;

public class TopCollider : MonoBehaviour
{
    private Transform cubeCollider;
    public LayerMask layerMask;
    private Collider[] hitColliders;
    public Collider[] HitColliders
    {
        get { return hitColliders; }
    }
    private UniversalCube otherCube;
    private UniversalCube cube;

    void Start()
    {
        cubeCollider = transform;
        cube = GetComponentInParent<UniversalCube>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hitColliders = Physics.OverlapSphere(cubeCollider.position, 0.01f, layerMask);
        if (hitColliders.Length > 0)
        {
            if (hitColliders[0].transform.GetComponent<UniversalCube>())
            {
                otherCube = hitColliders[0].transform.GetComponent<UniversalCube>();
            }

        }
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.01f);
    }

}
