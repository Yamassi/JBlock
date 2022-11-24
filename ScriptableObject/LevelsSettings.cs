
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsSettings", menuName = "LevelsSettings")]
public class LevelsSettings : ScriptableObject
{
    public float DeadZone;
    public float PlayerSpeed;
    public bool SoundOn;
    public bool VibrationOn;
}
