using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnCounter : MonoBehaviour
{
    public LayerMask layerMask;
    private Vector3 verticalColliderBoxScale = new Vector3(0.5f, 9.5f, 0.5f);
    private Collider[] hitColliders = new Collider[10];
    private int numHitColliders;
    private ColumnCounter columnCounter;
    private Transform column;
    private PlayerController playerController;
    private SpawnManager spawnManager;
    void Start()
    {
        columnCounter = GetComponent<ColumnCounter>();
        column = transform;
        playerController = FindObjectOfType<PlayerController>();
        spawnManager = FindObjectOfType<SpawnManager>(true);
    }
    private void FixedUpdate()
    {
        numHitColliders = Physics.OverlapBoxNonAlloc(column.position, verticalColliderBoxScale / 2, hitColliders, Quaternion.identity, layerMask);
        // print("column position" + column.position.x);
        // print("player position" + Mathf.Round(playerController.transform.position.x));
        if (numHitColliders >= 10 && Mathf.Round(playerController.transform.position.x) == Mathf.Round(column.position.x))
        {
            // print("AllColumnIsBusy");
            playerController.StopSpawnAnimation();
            // spawnManager.StopSpawn();
        }
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawCube(this.transform.position, verticalColliderBoxScale);


    }
}
