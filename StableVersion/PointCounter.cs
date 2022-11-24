using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;
    private Transform textCount;
    private int pointCount = 0;
    private Slider countProgressBar;
    [SerializeField] private int minCount;
    private TMPro.TextMeshProUGUI minCountText;
    [SerializeField] private LevelSettings levelSettings;
    private SpawnManager spawnManager;
    private WinMenu winMenu;
    private LoseMenu loseMenu;
    private PointMetr pointMetr;
    private SoundManager soundManager;
    public bool gameIsEnd = false;
    private PointsAndCombos pointsAndCombos;
    private Menu menu;
    private FXWin fXWin;
    private FXWin2 fXWin2;
    private bool isWin = false;
    private bool isWinFirst = true;
    private Animator pointMetrAnimator;
    private LevelManager levelManager;
    private PointCounter pointCounter;

    void Start()
    {
        text = transform.GetComponent<TMPro.TextMeshProUGUI>();
        minCountText = FindObjectOfType<MinCount>().GetComponent<TMPro.TextMeshProUGUI>();
        minCount = levelSettings.MinCountForWin;
        spawnManager = FindObjectOfType<SpawnManager>(true);
        minCountText.text = minCount.ToString();
        winMenu = FindObjectOfType<WinMenu>(true);
        loseMenu = FindObjectOfType<LoseMenu>(true);
        pointMetr = FindObjectOfType<PointMetr>();
        pointMetrAnimator = pointMetr.GetComponent<Animator>();
        soundManager = FindObjectOfType<SoundManager>();
        pointsAndCombos = FindObjectOfType<PointsAndCombos>();
        menu = FindObjectOfType<Menu>();
        fXWin = FindObjectOfType<FXWin>(true);
        fXWin2 = FindObjectOfType<FXWin2>(true);
        levelManager = FindObjectOfType<LevelManager>();
        pointCounter = FindObjectOfType<PointCounter>();
        gameIsEnd = false;
    }
    private void Update()
    {
        if (spawnManager.Count <= 0 && pointCount >= minCount)
        {
            // print("YOU WIN! GO TO THE NEXT LEVEL");
            StartCoroutine(WinAfter());
        }
        else if (spawnManager.Count <= 0 && pointCount < minCount)
        {
            // print("YOU LOSE! RESTART");
            Lose();
        }
        if (pointCount >= minCount & isWinFirst)
        {
            isWin = true;
        }
        if (isWin)
        {
            fXWin.gameObject.SetActive(true);
            pointMetrAnimator.enabled = true;
            pointMetrAnimator.Play("PointMetrAnimation");
            StartCoroutine(CountAnimation());
            isWin = false;
            isWinFirst = false;
        }

    }

    public void AddPoints(int newPoint)
    {
        pointCount += newPoint;
        text.fontSize = 72;
        UpdateUI();
        StartCoroutine(FontSizeNormalize());

    }
    IEnumerator CountAnimation()
    {
        yield return new WaitForSeconds(1.5f);

        pointMetrAnimator.Play("PointMetrAnimationCount");

    }
    IEnumerator FXRepeat()
    {
        yield return new WaitForSeconds(7f);
        Destroy(fXWin.gameObject);

        // print("1");
        // fXWin2.GetComponent<ParticleSystem>().Play(true);
        // yield return new WaitForSeconds(1.5f);
        // print("2");
        // fXWin2.GetComponent<ParticleSystem>().Play(true);
    }
    IEnumerator WinAfter()
    {
        spawnManager.StopAlert();
        yield return new WaitForSeconds(1.5f);
        Win();
    }
    public void Win()
    {
        pointMetr.gameObject.SetActive(false);
        if (gameIsEnd == false)
        {
            pointCounter.gameObject.transform.parent = null;
            pointMetr.gameObject.SetActive(false);
            pointsAndCombos.gameObject.SetActive(false);
            menu.gameObject.SetActive(false);
            gameIsEnd = true;
            winMenu.gameObject.SetActive(true);
            var score = winMenu.gameObject.GetComponentInChildren<Score>().GetComponent<TMPro.TextMeshProUGUI>();
            score.text = pointCount.ToString();

            soundManager.WinSoundPlay();
            levelManager.LevelComplete();
            SDKPlugin.addLevelCount();

            StartCoroutine(TimeStopAfter());
        }

    }
    public void Lose()
    {
        pointMetr.gameObject.SetActive(false);
        if (gameIsEnd == false)
        {
            pointCounter.gameObject.transform.parent = null;
            pointMetr.gameObject.SetActive(false);
            pointsAndCombos.gameObject.SetActive(false);
            menu.gameObject.SetActive(false);
            gameIsEnd = true;
            loseMenu.gameObject.SetActive(true);
            var score = loseMenu.gameObject.GetComponentInChildren<Score>().GetComponent<TMPro.TextMeshProUGUI>();
            score.text = pointCount.ToString();

            soundManager.LoseSoundPlay();
            levelManager.LevelFailed();

            StartCoroutine(TimeStopAfter());
        }

    }
    IEnumerator TimeStopAfter()
    {
        yield return new WaitForSeconds(1.5f);
        // Time.timeScale = 0;
        spawnManager.Count = 0;
    }
    IEnumerator FontSizeNormalize()
    {
        yield return new WaitForSeconds(0.1f);
        text.fontSize = 58;
    }
    private void UpdateUI()
    {
        text.text = pointCount.ToString();
        pointMetr.changePointMetrScale(pointCount, minCount);
        // countProgressBar.value = pointCount;
    }
}

