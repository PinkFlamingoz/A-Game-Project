using Photon.Pun;
using UnityEngine;
public class PlayerStatusMulti : MonoBehaviourPun
{
    public PhotonView view;
    public Health healthScript;
    public GameObject gameOverScreen;
    public void Start()
    {
        view = GetComponent<PhotonView>();
        healthScript = GetComponent<Health>();
    }
    public void Update()
    {
        if (view.IsMine)
        {
            if (healthScript.currentHealth <= 0)
            {
                Death();
            }
        }
    }

    public void Death()
    {
        photonView.RPC("DeathRPC", RpcTarget.All);
    }
    [PunRPC]
    public void DeathRPC()
    {
        //UnityEngine.Debug.Log("Death!");
        gameOverScreen.SetActive(true);
        //Destroy(this.gameObject);
        //Time.timeScale = 0f;
    }
}