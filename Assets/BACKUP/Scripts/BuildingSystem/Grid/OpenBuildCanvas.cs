using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenBuildCanvas : MonoBehaviour
{
    public GameObject canvasMenu;
    public GameObject isActive;
    public GameObject NodeLvl;
    public bool isOpen = false;
    public PhotonView _view;
    public int scene;

    public void Start()
    {
        _view = GetComponent<PhotonView>();
        scene = SceneManager.GetActiveScene().buildIndex;
        isActive = GameObject.FindGameObjectWithTag("IsActive");
        NodeLvl = GameObject.FindGameObjectWithTag("LvlHolder");
        NodeLvl.SetActive(false);
    }
    void Update()
    {
        switch (scene)
        {
            case 3:
                if (_view.IsMine)
                {
                    if (Input.GetKeyDown(KeyCode.B) && isOpen == false)
                    {
                        canvasMenu.SetActive(true);
                        NodeLvl.SetActive(true);
                        isOpen = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.B) && isOpen == true)
                    {
                        canvasMenu.SetActive(false);
                        NodeLvl.SetActive(false);
                        isOpen = false;
                    }
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.B) && isOpen == false)
                {
                    canvasMenu.SetActive(true);
                    NodeLvl.SetActive(true);
                    isOpen = true;
                }
                else if (Input.GetKeyDown(KeyCode.B) && isOpen == true)
                {
                    canvasMenu.SetActive(false);
                    NodeLvl.SetActive(false);
                    isOpen = false;
                }
                break;
            default:
                print("Wait for level load");
                break;
        }
    }
}
