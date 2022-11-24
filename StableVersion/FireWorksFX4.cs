using UnityEngine;

public class FireWorksFX4 : MonoBehaviour
{
    private BoxCollider boxCollider;
    private FireWorksFX4 fireWorksFX4;
    private GameObject parent;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        fireWorksFX4 = GetComponentInChildren<FireWorksFX4>(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Board>() || other.GetComponent<Floor>())
        {
            parent = fireWorksFX4.gameObject.transform.parent.gameObject;
            fireWorksFX4.gameObject.transform.parent = null;
            Destroy(parent);
            fireWorksFX4.GetComponent<ParticleSystem>().Play();
        }
    }
}
