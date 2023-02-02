using Photon.Pun;
using UnityEngine;

public class Health : MonoBehaviourPun
{
    public PhotonView view;
    public float maxHealth = 100;
    public float currentHealth;
    public HealthBarScript[] healthBarScript;
    public EnemyAttack enemyAttackScript;
    public GameObject EnemyCheck;
    public void Start()
    {
        healthBarScript = GetComponents<HealthBarScript>();
        view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            for (int i = 0; i < healthBarScript.Length; i++)
            {
                currentHealth = maxHealth;
                healthBarScript[i].SetMaxHealth(maxHealth);
            }
        }
        for (int i = 0; i < healthBarScript.Length; i++)
        {
            currentHealth = maxHealth;
            healthBarScript[i].SetMaxHealth(maxHealth);

        }
    }
    public void Update()
    {
        EnemyCheck = GameObject.FindGameObjectWithTag("Enemy");
        if (EnemyCheck != null)
        {
            enemyAttackScript = FindObjectOfType<EnemyAttack>().GetComponent<EnemyAttack>();
        }
    }
    public void TakeDamage()
    {
        photonView.RPC("TakeDamageRPC", RpcTarget.All);
    }
    [PunRPC]
    public void TakeDamageRPC()
    {
        for (int i = 0; i < healthBarScript.Length; i++)
        {
            currentHealth -= enemyAttackScript.damage;
            healthBarScript[i].SetHealt(currentHealth);
        }

    }
}
