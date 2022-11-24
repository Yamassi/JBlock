using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSettingsData
{
    public float sensivity;
    public bool twoButton;

    public GameSettingsData(GameSettings gameSettings)
    {
        sensivity = gameSettings.sensivity;
        twoButton = gameSettings.twoButton;
    }
}
