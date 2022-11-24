using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class DataSystem
{
    public static void SaveSettings(GameSettings gameSettings)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameSettingsData data = new GameSettingsData(gameSettings);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameSettingsData LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameSettingsData data = formatter.Deserialize(stream) as GameSettingsData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
