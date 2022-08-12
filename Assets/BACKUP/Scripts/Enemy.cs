using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class Enemy : MonoBehaviour
{
    public int scene;
    public float health;
    public float pointsToGive = 2;
    public PointsDisplay[] PointsDisplayScript;
    public ParticleSystem hit;
    public GameObject DeathEffect;
    public NavMeshAgent agentSpeed;
    public float startSpeed = 3;
    // public PhotonView view;
    public void Start()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        PointsDisplayScript = FindObjectsOfType<PointsDisplay>();
        agentSpeed.speed = startSpeed;
    }
    public void Update()
    {
        // view = FindObjectOfType<PlayerStatusMulti>().GetComponent<PhotonView>();
        if (health <= 0)
        {
            die();
        }
    }

    public void die()
    {
        switch (scene)
        {
            case 3:
                print("Zombie " + this.gameObject + " has died!");
                if (PhotonNetwork.IsMasterClient)
                {
                    GameObject effectt = (GameObject)PhotonNetwork.Instantiate(DeathEffect.name, transform.position, Quaternion.identity);
                    for (int i = 0; i < PointsDisplayScript.Length; i++)
                    {
                        PointsDisplayScript[i].SetPoints();
                    }
                    Destroy(effectt, 5f);

                }
                if (PhotonNetwork.IsMasterClient)
                {

                    PhotonNetwork.Destroy(this.gameObject);
                }
                break;
            case 1:
                GameObject effect = (GameObject)Instantiate(DeathEffect, transform.position, Quaternion.identity);
                print("Zombie " + this.gameObject + " has died!");
                FindObjectOfType<PlayerStatus>().points += pointsToGive;
                Destroy(effect, 5f);
                Destroy(this.gameObject);
                break;
            default:
                print("Wait for level load");
                break;
        }

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    public void Slow(float slow)
    {
        agentSpeed.speed = startSpeed * (1f - slow);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            hit.Emit(1);
        }
    }
}
