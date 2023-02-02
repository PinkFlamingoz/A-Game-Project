using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float points;
    public float health;
    public float[] position;

    public PlayerData(PlayerStatus player)
    {

        points = player.points;
        health = player.health;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
[System.Serializable]
public class WaveData
{
    public float waveID;
    public WaveData(WaveSpawner wave)
    {
        waveID = wave.nextWave;
    }
}
