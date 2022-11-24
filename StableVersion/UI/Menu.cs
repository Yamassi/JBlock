using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    PauseMenu pauseMenu;
    WinMenu winMenu;
    LoseMenu loseMenu;
    StartGame startGame;
    SpawnManager spawnManager;
    SoundManager soundManager;
    EducationAnimation educationAnimation;
    [SerializeField] private bool isFirstEdu;
    public bool IsFirstEdu
    {
        get { return isFirstEdu; }
        set { isFirstEdu = value; }
    }
    private Training training;
    private string currentScene;
    private Pause pause;
    private Resume resume;
    private PointMetr pointMetr;
    private void Start()
    {
        pointMetr = FindObjectOfType<PointMetr>();
        pause = FindObjectOfType<Pause>();
        resume = FindObjectOfType<Resume>(true);
        soundManager = FindObjectOfType<SoundManager>();
        Time.timeScale = 0;
        pauseMenu = FindObjectOfType<PauseMenu>(true);
        startGame = FindObjectOfType<StartGame>(true);
        startGame.gameObject.SetActive(true);
        spawnManager = FindObjectOfType<SpawnManager>(true);
        educationAnimation = FindObjectOfType<EducationAnimation>(true);
        training = FindObjectOfType<Training>(true);
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Level1")
        {
            isFirstEdu = true;
        }
        else
        {
            isFirstEdu = false;
        }

    }
    public void PlayMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PlaySettings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void PlayLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void PlayLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void PlayLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void PlayLevel4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void PlayLevel5()
    {
        SceneManager.LoadScene("Level5");
    }
    public void PlayLevel6()
    {
        SceneManager.LoadScene("Level6");
    }
    public void PlayLevel7()
    {
        SceneManager.LoadScene("Level7");
    }
    public void PlayLevel8()
    {
        SceneManager.LoadScene("Level8");
    }
    public void Pause()
    {
        pauseMenu.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);
        // resume.gameObject.SetActive(true);
        Time.timeScale = 0;
        soundManager.ClickSoundPlay();
        pointMetr.gameObject.SetActive(false);
    }
    public void Resume()
    {
        pauseMenu.gameObject.SetActive(false);
        pause.gameObject.SetActive(true);
        // resume.gameObject.SetActive(false);
        Time.timeScale = 1;
        soundManager.ClickSoundPlay();
        pointMetr.gameObject.SetActive(true);
    }
    public void StartGame()
    {
        if (isFirstEdu)
        {
            startGame.gameObject.SetActive(false);
            soundManager.ClickSoundPlay();
            training.gameObject.SetActive(true);
            educationAnimation.StartEdu();
        }
        else if (!isFirstEdu)
        {
            Destroy(training.gameObject);
            soundManager.ClickSoundPlay();
            startGame.gameObject.SetActive(false);
            spawnManager.gameObject.SetActive(true);
        }
        Time.timeScale = 1;
    }
}
