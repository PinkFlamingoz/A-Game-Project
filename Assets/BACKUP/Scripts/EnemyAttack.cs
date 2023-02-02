using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour
{
    //public PhotonView view;
    public Health[] healthScript;
    public float Range = 1f;
    public Transform AttackPoint;
    public LayerMask PlayerLayer;
    bool PlayerCheck;
    public float damage;
    public float attackSpeed;
    float nextTimeAttack = 0f;
    public int scene;
    // Start is called before the first frame update
    public void Start()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        healthScript = FindObjectsOfType<Health>();
    }
    // Update is called once per frame
    public void Update()
    {
        //view = FindObjectOfType<PlayerStatusMulti>().GetComponent<PhotonView>();
        switch (scene)
        {
            case 3:
                PlayerCheck = (Physics.CheckSphere(AttackPoint.position, Range, PlayerLayer));
                if (PlayerCheck == true && Time.time >= nextTimeAttack)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        for (int i = 0; i < healthScript.Length; i++)
                        {
                            healthScript[i].TakeDamage();
                        }
                    }
                    nextTimeAttack = Time.time + 1f / attackSpeed;
                }
                break;
            case 1:
                PlayerCheck = (Physics.CheckSphere(AttackPoint.position, Range, PlayerLayer));
                if (PlayerCheck == true && Time.time >= nextTimeAttack)
                {
                    Attack();
                    nextTimeAttack = Time.time + 1f / attackSpeed;
                }
                break;
            default:
                print("Wait for level load");
                break;
        }
    }
    public void Attack()
    {
        //Debug.Log("Damaged!" + damage);
        FindObjectOfType<PlayerStatus>().health -= damage;

    }
}
