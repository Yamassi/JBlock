using System.Collections;
using UnityEngine;

public class NearBoxCollider : MonoBehaviour
{
    private Transform cubeCollider;
    public LayerMask layerMask;
    private Vector3 hideColliderBoxScale = new Vector3(1.5f, 1.5f, 0.5f);
    private Vector3 horizontalColliderBoxScale = new Vector3(11f, 0.8f, 0.5f);
    private Vector3 verticalColliderBoxScale = new Vector3(0.5f, 16f, 0.5f);
    private Collider[] hitColliders = new Collider[16];
    private int numHitColliders;
    private PointCounter pointCounter;
    private UpCubesCollider upCubesCollider;
    private int points;
    private GameObject pointsAnimation;
    [SerializeField] GameObject Animation;
    private Transform canvas;
    private ExplodingCollider explodingCollider;
    private PointsAndCombos pointsAndCombos;


    void Start()
    {
        pointsAndCombos = FindObjectOfType<PointsAndCombos>();
        cubeCollider = transform;
        pointCounter = FindObjectOfType<PointCounter>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        explodingCollider = GetComponentInParent<ExplodingCollider>();
        if (explodingCollider.lineType == ExplodingCollider.LineType.Near)
        {
            upCubesCollider = GetComponentInChildren<UpCubesCollider>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (explodingCollider.lineType == ExplodingCollider.LineType.Near)
        {
            numHitColliders = Physics.OverlapBoxNonAlloc(cubeCollider.position, hideColliderBoxScale / 2, hitColliders, Quaternion.identity, layerMask);
        }
        else if (explodingCollider.lineType == ExplodingCollider.LineType.Horizontal)
        {
            numHitColliders = Physics.OverlapBoxNonAlloc(cubeCollider.position, horizontalColliderBoxScale / 2, hitColliders, Quaternion.identity, layerMask);
        }
        else if (explodingCollider.lineType == ExplodingCollider.LineType.Vertical)
        {
            numHitColliders = Physics.OverlapBoxNonAlloc(cubeCollider.position, verticalColliderBoxScale / 2, hitColliders, Quaternion.identity, layerMask);
        }


        // print("hitColliders Length" + hitColliders.Length);
    }

    public void DestroyNearCubes()
    {
        points = 20 * (numHitColliders);
        pointCounter.AddPoints(points);


        pointsAnimation = Instantiate(Animation);
        pointsAnimation.transform.SetParent(pointsAndCombos.transform, false);
        pointsAnimation.GetComponentInChildren<Combo>().PlayAddPointsAnimation(points);
        for (int i = 0; i < numHitColliders; i++)
        {
            // hitColliders[i].GetComponent<UniversalCube>().IsBoom = true;
            // Destroy(hitColliders[i].gameObject);

            switch (i)
            {
                case 0:
                    Destroy(hitColliders[i].gameObject);
                    break;
                case 1:
                    StartCoroutine(WaitForDestroy(hitColliders[i].gameObject, 0.15f));
                    break;
                case 2:
                    StartCoroutine(WaitForDestroy(hitColliders[i].gameObject, 0.2f));
                    break;
                case 3:
                    StartCoroutine(WaitForDestroy(hitColliders[i].gameObject, 0.25f));
                    break;
                case 4:
                    StartCoroutine(WaitForDestroy(hitColliders[i].gameObject, 0.3f));
                    break;
                case 5:
                    StartCoroutine(WaitForDestroy(hitColliders[i].gameObject, 0.35f));
                    break;
                case 6:
                    StartCoroutine(WaitForDestroy(hitColliders[i].gameObject, 0.4f));
                    break;
                case 7:
                    StartCoroutine(WaitForDestroy(hitColliders[i].gameObject, 0.45f));
                    break;
                case 8:
                    StartCoroutine(WaitForDestroy(hitColliders[i].gameObject, 0.5f));
                    break;
                case 9:
                    StartCoroutine(WaitForDestroy(hitColliders[i].gameObject, 0.55f));
                    break;
                case 10:
                    StartCoroutine(WaitForDestroy(hitColliders[i].gameObject, 0.6f));
                    break;
                case 11:
                    StartCoroutine(WaitForDestroy(hitColliders[i].gameObject, 0.65f));
                    break;
                case 12:
                    StartCoroutine(WaitForDestroy(hitColliders[i].gameObject, 0.7f));
                    break;
                case 13:
                    StartCoroutine(WaitForDestroy(hitColliders[i].gameObject, 0.75f));
                    break;
                case 14:
                    StartCoroutine(WaitForDestroy(hitColliders[i].gameObject, 0.8f));
                    break;
            }


            // if (i == 0)
            // {
            //     Destroy(hitColliders2[i].gameObject);
            // }
            // else if (i == 1)
            // {
            //     StartCoroutine(WaitForDestroy(hitColliders2[i].gameObject));
            // }


            // 
        }
        if (explodingCollider.lineType == ExplodingCollider.LineType.Near)
        {
            StartCoroutine(WaitForFall());
        }

    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        if (explodingCollider.lineType == ExplodingCollider.LineType.Near)
        {
            Gizmos.DrawCube(this.transform.position, hideColliderBoxScale);
        }
        else if (explodingCollider.lineType == ExplodingCollider.LineType.Horizontal)
        {
            Gizmos.DrawCube(this.transform.position, horizontalColliderBoxScale);
        }
        else if (explodingCollider.lineType == ExplodingCollider.LineType.Vertical)
        {
            Gizmos.DrawCube(this.transform.position, verticalColliderBoxScale);
        }

    }
    IEnumerator WaitForFall()
    {
        yield return new WaitForSeconds(0.5f);
        upCubesCollider.FallDownUpCubes();
    }
    IEnumerator WaitForDestroy(GameObject cubeForDestroy, float seconds)
    {
        // print("Start");
        yield return new WaitForSeconds(seconds);
        if (cubeForDestroy != null)
        {
            cubeForDestroy.GetComponent<UniversalCube>().IsBoom = true;
            Destroy(cubeForDestroy);
        }

    }
}
