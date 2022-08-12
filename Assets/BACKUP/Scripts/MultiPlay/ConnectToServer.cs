using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public InputField usernameInput;
    public Text buttonText;

    public GameObject dummyBox;

    public void OnClickConnect()
    {
        if (usernameInput.text.Length >= 1)
        {
            PhotonNetwork.NickName = usernameInput.text;
            buttonText.text = "Connecting Please Wait Kind Sir";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            dummyBox.SetActive(true);
        }
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Loby");
    }
    public void OnClickThankYOU()
    {
        dummyBox.SetActive(false);
    }
}
