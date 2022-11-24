
using UnityEngine;

public class HideCollider : MonoBehaviour
{
    private Transform hideCollider;
    public LayerMask layerMask;
    private Collider[] hitColliders;
    void Start()
    {
        hideCollider = transform;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hitColliders = Physics.OverlapSphere(hideCollider.position, 0.01f, layerMask);
        if (hitColliders.Length > 0)
        {
            // print("HitColliders.Length" + hitColliders.Length);
            UniCube uniCube = hitColliders[0].transform.GetComponent<UniCube>();
            if (uniCube.isStatic == false)
            {
                // print("isStatic" + uniCube.isStatic);
                Rigidbody cubeRigidbody = hideCollider.transform.parent.GetComponent<Rigidbody>();
                cubeRigidbody.constraints = RigidbodyConstraints.FreezePositionX | ~RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                UniCube myUniCube = hideCollider.transform.parent.GetComponent<UniCube>();
                myUniCube.isStatic = false;
                hideCollider.gameObject.SetActive(false);
            }
        }
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.01f);
    }
}
