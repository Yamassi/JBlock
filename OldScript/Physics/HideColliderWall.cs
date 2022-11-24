
using UnityEngine;

public class HideColliderWall : MonoBehaviour
{
    public LayerMask m_LayerMask;
    private HideColliderBox hideColliderBox;
    private Vector3 HideColliderWallScale = new Vector3(9, 20f, 1f);
    void Start()
    {
        hideColliderBox = GameObject.FindObjectOfType<HideColliderBox>();
    }

    void FixedUpdate()
    {

        Collider[] hitColliders = Physics.OverlapBox(transform.position, HideColliderWallScale, Quaternion.identity, m_LayerMask);
        // print("hitColliders Length" + hitColliders.Length);
        if (hitColliders.Length > 0)
        {
            // print(hideColliderBox.isDestroyed);
            if (hideColliderBox.isDestroyed == true)
            {
                foreach (var item in hitColliders)
                {
                    HideCollider cubeHideCollider = item.transform.GetChild(0).GetComponent<HideCollider>();
                    cubeHideCollider.gameObject.SetActive(false);
                }
                foreach (var item in hitColliders)
                {
                    UniCube cube = item.transform.gameObject.GetComponent<UniCube>();
                    cube.MoveDownOnStep(1);
                }
                hideColliderBox.isDestroyed = false;
                foreach (var item in hitColliders)
                {
                    HideCollider cubeHideCollider = item.transform.GetChild(0).GetComponent<HideCollider>();
                    cubeHideCollider.gameObject.SetActive(true);
                }
            }
        }

        // foreach (var item in hitColliders)
        // {
        //     // print("Destroy!");
        //     Destroy(item);
        // }

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.white;
        Gizmos.DrawCube(this.transform.position, HideColliderWallScale);
    }
}
