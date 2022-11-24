using System.Collections.Generic;
using UnityEngine;
using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;

public class WallCollider : MonoBehaviour
{
    private UniversalCube cube;
    private Collider[] hitColliders = new Collider[220];
    private int numHitColliders;
    public LayerMask m_LayerMask;
    private DestroyCubesColliderBox destroyCubesColliderBox;
    private Vector3 WallColliderScale = new Vector3(9, 40f, 1f);
    private int highCubeCount = 20;
    public int HighCubeCount
    {
        get { return highCubeCount; }
        set { highCubeCount = value; }
    }
    [SerializeField] private bool isFind = false;
    UniversalCube universalCube;
    public void Start()
    {
        destroyCubesColliderBox = GameObject.FindObjectOfType<DestroyCubesColliderBox>();
    }
    private void FixedUpdate()
    {
        CalculateWallCollider();
        if (!isFind)
        {
            // FindHighCube();
            isFind = true;
        }
    }
    [BurstCompile]
    public void CalculateWallCollider()
    {
        numHitColliders = Physics.OverlapBoxNonAlloc(transform.position, WallColliderScale, hitColliders, Quaternion.identity, m_LayerMask);
        // print("hitColliders Length" + hitColliders.Length);
        if (numHitColliders > 0)
        {
            // print(hideColliderBox.isDestroyed);
            if (destroyCubesColliderBox.isDestroyed == true)
            {
                for (int i = 0; i < numHitColliders; i++)
                {
                    cube = hitColliders[i].transform.gameObject.GetComponent<UniversalCube>();
                    cube.MoveDownOnStepWithAnimation(1);

                }

                destroyCubesColliderBox.isDestroyed = false;
            }
            isFind = false;
        }
    }

    [BurstCompile]
    public void FindHighCube()
    {

        if (numHitColliders > 0)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < numHitColliders; i++)
            {
                if (!hitColliders[i]) return;
                list.Add((int)hitColliders[i].transform.position.y);
            }
            list.Sort();
            highCubeCount = list[list.Count - 1] + 1;
            list.Clear();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(this.transform.position, WallColliderScale);
    }
}
