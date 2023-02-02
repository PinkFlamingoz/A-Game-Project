using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Shoot : MonoBehaviour
{
    public int scene;
    PhotonView _view;
    [SerializeField] private GameObject bulletSpawnPoint;

    [SerializeField] private GameObject bullet;
    public bool doubleDamageBool = false;
    public ParticleSystem shootEffect;
    private void Awake()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MultiPlayGame"))
        {
            _view = GetComponent<PhotonView>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (scene)
        {
            case 3:
                if (_view.IsMine)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Shooting();
                    }
                }
                break;
            case 1:
                if (Input.GetMouseButtonDown(0))
                {
                    Shooting();
                }
                break;
            default:
                print("Wait for level load");
                break;
        }
    }
    public void ShootEffectEmit()
    {
        _view.RPC("ShootEffectEmitRPC", RpcTarget.All);
    }
    [PunRPC]
    public void ShootEffectEmitRPC()
    {
        shootEffect.Emit(1);
    }
    private void Shooting()
    {
        switch (scene)
        {
            case 3:
                PhotonNetwork.Instantiate(bullet.transform.name, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
                ShootEffectEmit();
                break;
            case 1:
                Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
                shootEffect.Emit(1);
                break;
            default:
                print("Wait for level load");
                break;
        }
    }
}
