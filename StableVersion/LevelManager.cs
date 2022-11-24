using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    private string currentScene;
    public string CurrentScene
    {
        get { return currentScene; }
    }
    SoundManager soundManager;
    Dictionary<string, object> parameters = new Dictionary<string, object>();
    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void Start()
    {
        // print(SDKPlugin.LevelCount);
        YsoCorp.GameUtils.YCManager.instance.OnGameStarted(SDKPlugin.levelCount);
    }
    public void Continue()
    {
        if (currentScene == "Level1")
        {
            SceneManager.LoadSceneAsync("Level3");
        }
        else if (currentScene == "Level2")
        {
            SceneManager.LoadSceneAsync("Level3");
        }
        else if (currentScene == "Level3")
        {
            SceneManager.LoadSceneAsync("Level4");
        }
        else if (currentScene == "Level4")
        {
            SceneManager.LoadSceneAsync("Level5");
        }
        else if (currentScene == "Level5")
        {
            SceneManager.LoadSceneAsync("Level6");
        }
        else if (currentScene == "Level6")
        {
            SceneManager.LoadSceneAsync("Level7");
        }
        else if (currentScene == "Level7")
        {
            SceneManager.LoadSceneAsync("Level8");
        }
        else if (currentScene == "Level8")
        {
            SceneManager.LoadSceneAsync("Level1");
        }
        soundManager.ClickSoundPlay();
    }
    public void Restart()
    {
        soundManager.ClickSoundPlay();
        SceneManager.LoadSceneAsync(currentScene);
    }
    public void LevelComplete()
    {
        YsoCorp.GameUtils.YCManager.instance.OnGameFinished(true);
    }
    public void LevelFailed()
    {
        YsoCorp.GameUtils.YCManager.instance.OnGameFinished(false);
    }

}
