using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixerGroup;
    [SerializeField] private LevelsSettings levelsSettings;
    private bool isMute = false;
    public bool IsMute
    {
        get { return isMute; }
        set { isMute = value; }
    }
    private Transform muteVolumeImage;
    private Image volumeImage;
    private void Start()
    {
        muteVolumeImage = transform.GetChild(0);
        volumeImage = GetComponent<Image>();
    }

    public void SoundSwitch()
    {
        if (!isMute)
        {

            Mute();


        }
        else if (isMute)
        {

            UnMute();

        }
    }
    public void Mute()
    {
        isMute = true;
        audioMixerGroup.SetFloat("MasterVol", -80);
        muteVolumeImage.gameObject.SetActive(true);
        volumeImage.enabled = false;
        levelsSettings.SoundOn = false;
    }
    public void UnMute()
    {
        isMute = false;
        audioMixerGroup.SetFloat("MasterVol", 0);
        muteVolumeImage.gameObject.SetActive(false);
        volumeImage.enabled = true;
        levelsSettings.SoundOn = true;
    }
}
