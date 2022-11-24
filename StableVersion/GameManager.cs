using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixerGroup;
    DynamicJoystick dynamicJoystick;
    [SerializeField] private LevelsSettings levelsSettings;
    Volume volume;
    GameObject volumeChild;
    Vibration vibration;
    GameObject vibrationChild;
    SoundManager soundManager;
    [SerializeField] GameObject sDKPlugin;
    [SerializeField] GameObject yCManager;

    private void Start()
    {
        // dynamicJoystick = FindObjectOfType<DynamicJoystick>();
        // dynamicJoystick.DeadZone = levelsSettings.DeadZone;
        volume = FindObjectOfType<Volume>(true);
        vibration = FindObjectOfType<Vibration>(true);
        volumeChild = volume.transform.GetChild(0).gameObject;
        vibrationChild = vibration.transform.GetChild(0).gameObject;
        soundManager = FindObjectOfType<SoundManager>();
        if (levelsSettings.SoundOn)
        {
            SoundOn();

        }
        else if (!levelsSettings.SoundOn)
        {
            SoundOff();

        }
        if (levelsSettings.VibrationOn)
        {
            VibrateOn();
        }
        else if (!levelsSettings.VibrationOn)
        {
            VibrateOff();
        }
        // if (FindObjectOfType<FacebookInit>() == null)
        // {
        //     Instantiate(facebook, Vector3.zero, Quaternion.identity);
        // }
        if (FindObjectOfType<SDKPlugin>() == null)
        {
            Instantiate(sDKPlugin, Vector3.zero, Quaternion.identity);
        }
        // if (FindObjectOfType<YsoCorp.GameUtils.YCManager>() == null)
        // {
        //     Instantiate(yCManager, Vector3.zero, Quaternion.identity);
        // }
    }
    private void SoundOn()
    {
        volume.IsMute = false;
        audioMixerGroup.SetFloat("MasterVol", 0);
        volumeChild.SetActive(false);
    }
    private void SoundOff()
    {
        volume.IsMute = true;
        audioMixerGroup.SetFloat("MasterVol", -80);
        volumeChild.SetActive(true);
    }
    private void VibrateOn()
    {
        vibration.IsVibrate = true;
        soundManager.isVibrateON = true;
        vibrationChild.gameObject.SetActive(false);
    }
    private void VibrateOff()
    {
        vibration.IsVibrate = false;
        soundManager.isVibrateON = false;
        vibrationChild.gameObject.SetActive(true);
    }
}
