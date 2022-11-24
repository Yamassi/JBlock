using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    public float sensivity;
    public bool twoButton;
    public void SaveSettings()
    {
        sensivity = FindObjectOfType<Sensivity>().gameObject.GetComponent<Slider>().value;
        twoButton = FindObjectOfType<ControllerType>().gameObject.GetComponent<Toggle>().isOn;
        DataSystem.SaveSettings(this);
    }
    public void LoadSettings()
    {
        // if (DataSystem.LoadSettings() == null)
        // {
        GameSettingsData data = DataSystem.LoadSettings();
        sensivity = data.sensivity;
        twoButton = data.twoButton;
        if (FindObjectOfType<Sensivity>())
        {
            FindObjectOfType<Sensivity>().gameObject.GetComponent<Slider>().value = sensivity;
            FindObjectOfType<ControllerType>().gameObject.GetComponent<Toggle>().isOn = twoButton;
        }


        // }

    }
    private void Awake()
    {
        LoadSettings();
    }
}
