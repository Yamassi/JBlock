using UnityEngine.Events;
using UnityEngine;
using System.Collections;
using FirstGearGames.SmoothCameraShaker;

public class ExplodingCollider : MonoBehaviour
{
    private Transform cubeCollider;
    public LayerMask layerMask;
    private Collider[] hitColliders = new Collider[9];
    private int numHitColliders;
    private SoundManager soundManager;
    private UpCubesCollider upCubesCollider;
    private NearBoxCollider nearBoxCollider;
    [SerializeField] private ShakeData shakeData;
    [SerializeField]
    public enum LineType
    {
        Near,
        Horizontal,
        Vertical,
    }
    public LineType lineType = 0;
    private UniversalCube universalCube;
    private ExplodingCollider explodingCollider;
    private Animator animator;
    private Rotator rotator;
    private static Vector3 lineOffset = new Vector3(0, 0, -1);
    private bool isFirstBoom = true;
    private FireWorksFX fireWorksFX;
    private FireWorksFX2 fireWorksFX2;

    void Start()
    {
        cubeCollider = transform;
        soundManager = FindObjectOfType<SoundManager>();
        universalCube = GetComponentInParent<UniversalCube>();
        nearBoxCollider = cubeCollider.GetComponentInChildren<NearBoxCollider>();
        explodingCollider = GetComponent<ExplodingCollider>();

        if (explodingCollider.lineType == LineType.Near)
        {
            upCubesCollider = cubeCollider.GetComponentInChildren<UpCubesCollider>();
        }
        if (explodingCollider.lineType == LineType.Horizontal || explodingCollider.lineType == LineType.Vertical)
        {
            rotator = cubeCollider.parent.gameObject.GetComponentInChildren<Rotator>();
            animator = rotator.gameObject.GetComponent<Animator>();
            fireWorksFX = rotator.GetComponentInChildren<FireWorksFX>(true);
            fireWorksFX2 = rotator.GetComponentInChildren<FireWorksFX2>(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        numHitColliders = Physics.OverlapSphereNonAlloc(cubeCollider.position, 0.01f, hitColliders, layerMask);
        if (numHitColliders > 0)
        {
            if (hitColliders[0].transform.GetComponent<UniversalCube>())
            {
                if (explodingCollider.lineType == LineType.Near)
                {
                    upCubesCollider.enabled = true;
                    upCubesCollider.transform.parent = null;
                    soundManager.ExplosingCubesSoundPlay();
                    universalCube.FXPlay();
                    CameraShakerHandler.Shake(shakeData);
                    Destroy(cubeCollider.gameObject);
                    Destroy(universalCube.gameObject);
                    nearBoxCollider.transform.parent = null;
                    nearBoxCollider.enabled = true;

                    nearBoxCollider.DestroyNearCubes();
                }
                else if (explodingCollider.lineType == LineType.Horizontal)
                {
                    if (isFirstBoom)
                    {
                        soundManager.ExplosingCubesSoundPlay();
                        universalCube.FXPlay();
                        rotator.RotateOff();
                        rotator.TimerOn();
                        rotator.transform.rotation = Quaternion.identity;
                        rotator.transform.parent = null;
                        animator.enabled = true;
                        animator.Play(StaticData.HorizontalBoom);
                        CameraShakerHandler.Shake(shakeData);

                        nearBoxCollider.transform.parent = null;
                        nearBoxCollider.enabled = true;

                        // print(rotator.transform.position);
                        rotator.transform.position += lineOffset;


                        fireWorksFX.gameObject.SetActive(true);
                        fireWorksFX2.gameObject.SetActive(true);

                        nearBoxCollider.DestroyNearCubes();
                        isFirstBoom = false;
                    }



                }
                else if (explodingCollider.lineType == LineType.Vertical)
                {
                    if (isFirstBoom)
                    {
                        soundManager.ExplosingCubesSoundPlay();
                        universalCube.FXPlay();
                        rotator.RotateOff();
                        rotator.transform.rotation = Quaternion.identity;
                        rotator.transform.parent = null;
                        rotator.TimerOn();
                        animator.enabled = true;
                        animator.Play(StaticData.VerticalBoom);
                        CameraShakerHandler.Shake(shakeData);

                        nearBoxCollider.transform.parent = null;
                        nearBoxCollider.enabled = true;

                        // print(rotator.transform.position);
                        rotator.transform.position += lineOffset;


                        fireWorksFX.gameObject.SetActive(true);
                        fireWorksFX2.gameObject.SetActive(true);

                        nearBoxCollider.DestroyNearCubes();
                        isFirstBoom = false;
                    }

                }



            }
            // print("HitColliders.Length" + hitColliders.Length);
            // UniversalCube universalCube = hitColliders[0].transform.GetComponent<UniversalCube>();

        }
    }
    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(1f);
        Destroy(cubeCollider.gameObject);
        Destroy(universalCube.gameObject);
        Destroy(rotator.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<UniversalCube>())
        {
            // upCubesCollider.enabled = true;
            // soundManager.ExplosingCubesSoundPlay();

            // nearBoxCollider.DestroyNearCubes();

        }
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.01f);
    }
}
