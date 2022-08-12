using Photon.Pun;
using TMPro;
using UnityEngine;

public class PointsDisplay : MonoBehaviourPun
{
    public TextMeshProUGUI pointsText;
    public float points = 0;
    public float currentPoints;
    public static float pointsForBuild;
    public Enemy enemyScript;
    public GameObject EnemyCheck;

    public void Start()
    {
        currentPoints = points;
    }
    public void Update()
    {
        pointsForBuild = currentPoints;
        EnemyCheck = GameObject.FindGameObjectWithTag("Enemy");
        if (EnemyCheck != null)
        {
            enemyScript = FindObjectOfType<Enemy>().GetComponent<Enemy>();
        }
    }
    public void SetPointsSP()
    {
        pointsText.text = "Points: " + GetComponent<PlayerStatus>().points;
    }
    public void SetPoints()
    {
        photonView.RPC("SetPointsRPC", RpcTarget.All);
    }
    [PunRPC]
    public void SetPointsRPC()
    {
        currentPoints += enemyScript.pointsToGive;
        pointsText.text = "Points: " + currentPoints;
    }
}
