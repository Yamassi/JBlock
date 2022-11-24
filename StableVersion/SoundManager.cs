using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource addCubesSound;
    [SerializeField] private AudioSource explosingCubesSound;
    [SerializeField] private AudioSource AllCubesDestroyedSound;
    [SerializeField] private AudioSource ForwardCubeSound;
    [SerializeField] private AudioSource WinSound;
    [SerializeField] private AudioSource LoseSound;
    [SerializeField] private AudioSource ClickSound;
    [SerializeField] private AudioSource SpeedFire;
    [SerializeField] private AudioSource DoubleFire;
    public bool isVibrateON = true;
    public void SpeedFireSoundPlay()
    {
        SpeedFire.Play();
    }
    public void DoubleFireSoundPlay()
    {
        DoubleFire.Play();
    }
    public void AddCubesSoundPlay()
    {
        addCubesSound.Play();
    }
    public void ExplosingCubesSoundPlay()
    {
        explosingCubesSound.Play();
        if (isVibrateON)
        {
            Handheld.Vibrate();
        }
    }
    public void AllCubesDestroyedSoundPlay()
    {
        AllCubesDestroyedSound.Play();
        if (isVibrateON)
        {
            Handheld.Vibrate();
        }
    }
    public void ForwardCubeSoundPlay()
    {
        ForwardCubeSound.Play();
    }
    public void WinSoundPlay()
    {
        WinSound.Play();
    }
    public void LoseSoundPlay()
    {
        LoseSound.Play();
    }
    public void ClickSoundPlay()
    {
        ClickSound.Play();
    }

}
