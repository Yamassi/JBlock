
using UnityEngine;

public class HideColliderBox : MonoBehaviour
{
    public LayerMask m_LayerMask;
    public bool isDestroyed;
    private Vector3 DestroyCubesColliderBoxScale = new Vector3(10, 0.2f, 0.2f);
    private Collider[] hitColliders;
    private Transform colliderBox;
    private PointCounter pointCounter;
    void Start()
    {
        colliderBox = transform;
        pointCounter = FindObjectOfType<PointCounter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        hitColliders = Physics.OverlapBox(colliderBox.position, DestroyCubesColliderBoxScale, Quaternion.identity, m_LayerMask);
        // print("hitColliders Length" + hitColliders.Length);
        if (hitColliders.Length >= 9)
        {

            foreach (var item in hitColliders)
            {
                // print("Destroy!");
                Destroy(item.gameObject);
            }
            isDestroyed = true;

            pointCounter.AddPoints(100);
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.transform.position, DestroyCubesColliderBoxScale);
    }
}
