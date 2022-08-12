using Photon.Pun;
using UnityEngine;
public class PlayerStatus : MonoBehaviourPun
{

    public GameObject MenuGame;
    public GameObject MenuCamera;
    public GameObject Camera;
    public float health;
    public float points;
    public static float pointsForBuild;
    public HealthBarScript healthBar;
    public PointsDisplay pointsDisplatScript;
    public GameObject gameOverScreen;
    public void Start()
    {
        healthBar = FindObjectOfType<HealthBarScript>();
        pointsDisplatScript = FindObjectOfType<PointsDisplay>();
        healthBar.SetMaxHealth(health);
    }
    public void Update()
    {
        pointsForBuild = points;
        pointsDisplatScript.SetPointsSP();
        healthBar.SetHealt(health);
        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        //UnityEngine.Debug.Log("Death!");
        gameOverScreen.SetActive(true);
        //Destroy(this.gameObject);
        //Time.timeScale = 0f;
    }
    public void SaveThePlayerPP()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            MenuGame.SetActive(false);
            MenuCamera.SetActive(false);
            Camera.SetActive(true);
            SaveSystem.SaveThePlayer(this);
            Debug.Log("GameSaved!");
            PressESCforBackToGame.GameIsPaused = false;
        }
        else
        {
            Debug.Log("Enemy's still exist, can't save yet!");
        }
    }

    public void LoadThePlayerPP()
    {
        PlayerData data = SaveSystem.LoadThePlayer();

        health = data.health;
        points = data.points;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        MenuGame.SetActive(false);
        MenuCamera.SetActive(false);
        Camera.SetActive(true);
        PressESCforBackToGame.GameIsPaused = false;
        Debug.Log("Gameloaded!");
    }
}
