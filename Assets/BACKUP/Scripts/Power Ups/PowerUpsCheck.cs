using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerUpsCheck : MonoBehaviour
{
    public Transform[] spawns;
    public GameObject[] objects;
    public GameObject[] powerUpsOnMap;
    public float duration = 600;
    public bool spawning = false;
    public int scene;
    void Start()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
    }
    public void Update()
    {
        switch (scene)
        {
            case 3:
                if (PhotonNetwork.IsMasterClient)
                {
                    powerUpsOnMap = GameObject.FindGameObjectsWithTag("PowerUp");
                    if (powerUpsOnMap.Length == 0 && spawning == false)
                    {
                        Debug.Log(powerUpsOnMap.Length);
                        StartCoroutine(SpawnObjects(objects, spawns));
                    }
                }
                break;
            case 1:
                powerUpsOnMap = GameObject.FindGameObjectsWithTag("PowerUp");
                if (powerUpsOnMap.Length == 0 && spawning == false)
                {
                    Debug.Log(powerUpsOnMap.Length);
                    StartCoroutine(SpawnObjects(objects, spawns));
                }
                break;
            case 0:
                powerUpsOnMap = GameObject.FindGameObjectsWithTag("PowerUp");
                if (powerUpsOnMap.Length == 0 && spawning == false)
                {
                    Debug.Log(powerUpsOnMap.Length);
                    StartCoroutine(SpawnObjects(objects, spawns));
                }
                break;
            case 2:
                powerUpsOnMap = GameObject.FindGameObjectsWithTag("PowerUp");
                if (powerUpsOnMap.Length == 0 && spawning == false)
                {
                    Debug.Log(powerUpsOnMap.Length);
                    StartCoroutine(SpawnObjects(objects, spawns));
                }
                break;
        }
    }
    IEnumerator SpawnObjects(GameObject[] gameObjects, Transform[] locations, bool allowOverlap = false)
    {
        switch (scene)
        {
            case 3:
                spawning = true;
                yield return new WaitForSeconds(duration);
                Debug.Log("Spawn");
                List<GameObject> remainingGameObjects = new List<GameObject>(gameObjects);
                List<Transform> freeLocations = new List<Transform>(locations);

                if (locations.Length < gameObjects.Length)
                    Debug.LogWarning(allowOverlap ? "There are more gameObjects than locations. Some objects will overlap." : "There are not enough locations for all the gameObjects. Some won't spawn.");

                while (remainingGameObjects.Count > 0)
                {
                    if (freeLocations.Count == 0)
                    {
                        if (allowOverlap) freeLocations.AddRange(locations);
                        else break;
                    }

                    int gameObjectIndex = Random.Range(0, remainingGameObjects.Count);
                    int locationIndex = Random.Range(0, freeLocations.Count);
                    PhotonNetwork.Instantiate(gameObjects[gameObjectIndex].name, locations[locationIndex].position, Quaternion.identity);
                    powerUpsOnMap = GameObject.FindGameObjectsWithTag("PowerUp");
                    remainingGameObjects.RemoveAt(gameObjectIndex);
                    freeLocations.RemoveAt(locationIndex);

                }
                spawning = false;
                break;
            case 1:
                spawning = true;
                yield return new WaitForSeconds(duration);
                Debug.Log("Spawn");
                List<GameObject> remainingGameObjectss = new List<GameObject>(gameObjects);
                List<Transform> freeLocationss = new List<Transform>(locations);

                if (locations.Length < gameObjects.Length)
                    Debug.LogWarning(allowOverlap ? "There are more gameObjects than locations. Some objects will overlap." : "There are not enough locations for all the gameObjects. Some won't spawn.");

                while (remainingGameObjectss.Count > 0)
                {
                    if (freeLocationss.Count == 0)
                    {
                        if (allowOverlap) freeLocationss.AddRange(locations);
                        else break;
                    }

                    int gameObjectIndex = Random.Range(0, remainingGameObjectss.Count);
                    int locationIndex = Random.Range(0, freeLocationss.Count);
                    Instantiate(gameObjects[gameObjectIndex], locations[locationIndex].position, Quaternion.identity);
                    powerUpsOnMap = GameObject.FindGameObjectsWithTag("PowerUp");
                    remainingGameObjectss.RemoveAt(gameObjectIndex);
                    freeLocationss.RemoveAt(locationIndex);

                }
                spawning = false;
                break;
            case 0:
                spawning = true;
                yield return new WaitForSeconds(duration);
                Debug.Log("Spawn");
                List<GameObject> remainingGameObjectsss = new List<GameObject>(gameObjects);
                List<Transform> freeLocationsss = new List<Transform>(locations);

                if (locations.Length < gameObjects.Length)
                    Debug.LogWarning(allowOverlap ? "There are more gameObjects than locations. Some objects will overlap." : "There are not enough locations for all the gameObjects. Some won't spawn.");

                while (remainingGameObjectsss.Count > 0)
                {
                    if (freeLocationsss.Count == 0)
                    {
                        if (allowOverlap) freeLocationsss.AddRange(locations);
                        else break;
                    }

                    int gameObjectIndex = Random.Range(0, remainingGameObjectsss.Count);
                    int locationIndex = Random.Range(0, freeLocationsss.Count);
                    Instantiate(gameObjects[gameObjectIndex], locations[locationIndex].position, Quaternion.identity);
                    powerUpsOnMap = GameObject.FindGameObjectsWithTag("PowerUp");
                    remainingGameObjectsss.RemoveAt(gameObjectIndex);
                    freeLocationsss.RemoveAt(locationIndex);

                }
                spawning = false;
                break;
            case 2:
                spawning = true;
                yield return new WaitForSeconds(duration);
                Debug.Log("Spawn");
                List<GameObject> remainingGameObjectssss = new List<GameObject>(gameObjects);
                List<Transform> freeLocationssss = new List<Transform>(locations);

                if (locations.Length < gameObjects.Length)
                    Debug.LogWarning(allowOverlap ? "There are more gameObjects than locations. Some objects will overlap." : "There are not enough locations for all the gameObjects. Some won't spawn.");

                while (remainingGameObjectssss.Count > 0)
                {
                    if (freeLocationssss.Count == 0)
                    {
                        if (allowOverlap) freeLocationssss.AddRange(locations);
                        else break;
                    }

                    int gameObjectIndex = Random.Range(0, remainingGameObjectssss.Count);
                    int locationIndex = Random.Range(0, freeLocationssss.Count);
                    Instantiate(gameObjects[gameObjectIndex], locations[locationIndex].position, Quaternion.identity);
                    powerUpsOnMap = GameObject.FindGameObjectsWithTag("PowerUp");
                    remainingGameObjectssss.RemoveAt(gameObjectIndex);
                    freeLocationssss.RemoveAt(locationIndex);

                }
                spawning = false;
                break;
            default:
                print("Wait for level load");
                break;
        }

    }
}