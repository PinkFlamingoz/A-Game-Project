using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlashLight : MonoBehaviourPunCallbacks
{
    public int scene;
    public Light flashLight;
    public bool OnOrOff = false;
    public PhotonView view;

    public bool isOn = false;
    public bool failSafe = false;

    public void Awake()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
    }
    public void Update()
    {
        switch (scene)
        {
            case 3:
                view = GetComponent<PhotonView>();
                if (!photonView.IsMine)
                    return;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (isOn == false && failSafe == false)
                    {
                        view.RPC("On", RpcTarget.All);
                    }
                    if (isOn == true && failSafe == false)
                    {
                        view.RPC("Off", RpcTarget.All);
                    }
                }
                break;
            case 1:
                OhNoItsDark();
                break;
            default:
                print("Wait for level load");
                break;
        }
    }
    public void OhNoItsDark()
    {
        if (Input.GetKeyDown(KeyCode.F) && OnOrOff == false)
        {
            flashLight.gameObject.SetActive(true);
            OnOrOff = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && OnOrOff == true)
        {
            flashLight.gameObject.SetActive(false);
            OnOrOff = false;
        }
    }
    [PunRPC]
    void On()
    {
        failSafe = true;
        flashLight.gameObject.SetActive(true);
        isOn = true;
        StartCoroutine(FailSafe());
    }
    [PunRPC]
    void Off()
    {
        failSafe = true;
        flashLight.gameObject.SetActive(false);
        isOn = false;
        StartCoroutine(FailSafe());
    }
    IEnumerator FailSafe()
    {
        yield return new WaitForSeconds(0.25f);
        failSafe = false;
    }
}
