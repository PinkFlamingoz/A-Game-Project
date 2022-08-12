using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SaveThePlayer(PlayerStatus player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log(Application.persistentDataPath);
    }

    public static PlayerData LoadThePlayer()
    {
        string path = Application.persistentDataPath + "/game.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveTheWave(WaveSpawner wave)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gamewave.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        WaveData data = new WaveData(wave);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log(Application.persistentDataPath);
    }
    public static WaveData LoadTheWave()
    {
        string path = Application.persistentDataPath + "/gamewave.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            WaveData data = formatter.Deserialize(stream) as WaveData;
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
