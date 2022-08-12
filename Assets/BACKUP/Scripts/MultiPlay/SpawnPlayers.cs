using Photon.Pun;
using UnityEngine;
public class SpawnPlayers : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPlayerPoints;

    public void Start()
    {
        int randomNumber = Random.Range(0, spawnPlayerPoints.Length);
        Transform spawnPoint = spawnPlayerPoints[randomNumber];
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
    }
}
