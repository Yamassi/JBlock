using UnityEngine.UI;
using UnityEngine;

public class Vibration : MonoBehaviour
{
    private bool isVibrate = true;
    public bool IsVibrate
    {
        get { return isVibrate; }
        set { isVibrate = value; }
    }
    private Transform muteVibrationImage;
    private SoundManager soundManager;
    [SerializeField] private LevelsSettings levelsSettings;
    private Image vibrationImage;
    private void Start()
    {
        muteVibrationImage = transform.GetChild(0);
        soundManager = FindObjectOfType<SoundManager>();
        vibrationImage = GetComponent<Image>();

    }

    public void VibrationSwitch()
    {
        if (isVibrate)
        {

            VibrationOff();


        }
        else if (!isVibrate)
        {

            VibratOn();

        }
    }
    private void VibratOn()
    {
        isVibrate = true;
        soundManager.isVibrateON = true;
        muteVibrationImage.gameObject.SetActive(false);
        vibrationImage.enabled = true;
        levelsSettings.VibrationOn = true;
    }
    private void VibrationOff()
    {
        isVibrate = false;
        soundManager.isVibrateON = false;
        muteVibrationImage.gameObject.SetActive(true);
        vibrationImage.enabled = false;
        levelsSettings.VibrationOn = false;
    }
}
