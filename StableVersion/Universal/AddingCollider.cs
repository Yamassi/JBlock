using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AddingCollider : MonoBehaviour
{
    private Transform cubeCollider;
    // public LayerMask layerMask;
    // private Collider[] hitColliders;

    private GameObject cube;
    private SoundManager soundManager;
    private bool flyToPlayer;
    private float speed = 20;
    private UniversalCube universalCube;
    // private PlayerController playerController;
    private Chamber chamber;
    void Start()
    {
        cubeCollider = transform;

        soundManager = FindObjectOfType<SoundManager>();
        universalCube = GetComponentInParent<UniversalCube>();
        // playerController = FindObjectOfType<PlayerController>();
        chamber = FindObjectOfType<Chamber>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<UniversalCube>())
        {
            universalCube.GetComponent<BoxCollider>().enabled = false;
            cubeCollider.GetComponent<BoxCollider>().enabled = false;

            cube = cubeCollider.parent.gameObject;
            soundManager.AddCubesSoundPlay();
            flyToPlayer = true;
            universalCube.cubeState = UniversalCube.CubeState.Fly;
            StartCoroutine(WaitForOn());
            // StartCoroutine(WaitForDestroy());
            // Destroy(cube);
        }
    }
    private void FixedUpdate()
    {
        // hitColliders = Physics.OverlapSphere(cubeCollider.position, 0.01f, layerMask);
        // if (hitColliders.Length > 0)
        // {

        // }
        if (flyToPlayer)
        {
            universalCube.transform.position = Vector3.MoveTowards(universalCube.transform.position, chamber.transform.position, speed * Time.deltaTime);
        }
    }

    IEnumerator WaitForOn()
    {
        yield return new WaitForSeconds(0.2f);
        universalCube.GetComponent<BoxCollider>().enabled = true;
        cubeCollider.GetComponent<BoxCollider>().enabled = true;

    }
    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(universalCube.gameObject);
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.01f);
    }
}
