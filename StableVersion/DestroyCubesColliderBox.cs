using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using FirstGearGames.SmoothCameraShaker;
public class DestroyCubesColliderBox : MonoBehaviour
{
    public LayerMask m_LayerMask;
    public bool isDestroyed;
    private static Vector3 DestroyCubesColliderBoxScale = new Vector3(10, 0.1f, 0.1f);
    private Collider[] hitColliders = new Collider[7];
    private int numHitColliders;
    private Transform colliderBox;
    private PointCounter pointCounter;
    private WallCollider wallCollider;
    public UnityEvent destroyDownLine;
    private SoundManager soundManager;
    [SerializeField] GameObject ComboAnimation;
    [SerializeField] GameObject AddPointsAnimation;
    private GameObject addPoints;
    private GameObject combo;
    private Transform canvas;
    private int comboCount;
    [SerializeField] private ShakeData shakeData;
    private PointsAndCombos pointsAndCombos;

    void Start()
    {
        pointsAndCombos = FindObjectOfType<PointsAndCombos>();
        colliderBox = transform;
        pointCounter = FindObjectOfType<PointCounter>();
        soundManager = FindObjectOfType<SoundManager>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }
    void FixedUpdate()
    {
        // hitColliders = null;
        numHitColliders = Physics.OverlapBoxNonAlloc(colliderBox.position, DestroyCubesColliderBoxScale, hitColliders, Quaternion.identity, m_LayerMask);
        // print(numHitColliders);
        if (numHitColliders >= 7)
        {
            // print("Больше чем нужно?" + hitColliders.Length);
            for (int i = 0; i < numHitColliders; i++)
            {
                if (!hitColliders[i].gameObject.GetComponent<UniversalCube>().FallDown)
                {
                    Destroy(hitColliders[i].gameObject);
                    CameraShakerHandler.Shake(shakeData);
                }

            }
            isDestroyed = true;
            soundManager.AllCubesDestroyedSoundPlay();
            comboCount += 1;
            StartCoroutine(WaitForComboReset());
            ComboSwitch();


        }
    }
    private void AddPointsForAnimation(int pointCount)
    {
        addPoints = Instantiate(AddPointsAnimation);
        addPoints.transform.SetParent(pointsAndCombos.transform, false);
        // addPoints.transform.SetParent(canvas, false);
        addPoints.GetComponentInChildren<Combo>().PlayAddPointsAnimation(pointCount);
    }
    private void AddCombo(int comboCount)
    {
        combo = Instantiate(ComboAnimation);
        combo.transform.SetParent(pointsAndCombos.transform, false);
        // combo.transform.SetParent(canvas, false);
        combo.GetComponentInChildren<Combo>().PlayComboAnimation(comboCount);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.transform.position, DestroyCubesColliderBoxScale);
    }
    IEnumerator WaitForComboReset()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        comboCount = 0;
    }
    IEnumerator WaitForComboX(float seconds, int points, int comboX)
    {
        yield return new WaitForSecondsRealtime(seconds);
        pointCounter.AddPoints(points);
        AddCombo(comboX);
        AddPointsForAnimation(points);
    }
    private void ComboSwitch()
    {
        switch (comboCount)
        {
            case 1:

                pointCounter.AddPoints(100);
                AddPointsForAnimation(100);
                break;
            case 2:
                StartCoroutine(WaitForComboX(seconds: 0.2f, points: 300, comboX: 2));
                // StartCoroutine(WaitForComboX2());
                // pointCounter.AddPoints(300);
                break;
            case 3:
                StartCoroutine(WaitForComboX(seconds: 0.2f, points: 600, comboX: 3));
                break;
            case 4:
                StartCoroutine(WaitForComboX(seconds: 0.2f, points: 800, comboX: 4));
                break;
            case 5:
                StartCoroutine(WaitForComboX(seconds: 0.2f, points: 800, comboX: 5));
                break;
            case 6:
                StartCoroutine(WaitForComboX(seconds: 0.2f, points: 800, comboX: 6));
                break;
            case 7:
                StartCoroutine(WaitForComboX(seconds: 0.2f, points: 800, comboX: 7));
                break;
            case 8:
                StartCoroutine(WaitForComboX(seconds: 0.2f, points: 800, comboX: 8));
                break;
            case 9:
                StartCoroutine(WaitForComboX(seconds: 0.2f, points: 800, comboX: 9));
                break;
            case 10:
                StartCoroutine(WaitForComboX(seconds: 0.2f, points: 800, comboX: 10));
                break;
        }
    }

}
