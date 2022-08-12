using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject BubbleSpeachObject;
    public GameObject ChatMenu;
    public bool isOpen = false;
    public PhotonView view;
    public TextMeshProUGUI updateText;
    public TMP_InputField ChatInputField;
    public bool disableSend;
    public GameObject chatMenuCheck;
    public void Awake()
    {
        view = GetComponent<PhotonView>();
        if (chatMenuCheck != null)
        {
            ChatInputField = GameObject.Find("Chat InputField (TMP)").GetComponent<TMP_InputField>();
        }
    }
    public void Update()
    {
        chatMenuCheck = GameObject.FindGameObjectWithTag("Chat");
        if (view.IsMine)
        {
            OpenCHAT();
            if (!disableSend && ChatInputField.isFocused)
            {
                if (ChatInputField.text != "" && ChatInputField.text.Length > 0 && Input.GetKeyUp(KeyCode.Return))
                {
                    SendMessage();
                    BubbleSpeachObject.SetActive(true);
                    ChatInputField.text = "";
                    disableSend = true;
                }
            }

        }

    }
    public void SendMessage()
    {
        photonView.RPC("SendMessageRPC", RpcTarget.AllBuffered, ChatInputField.text);
    }
    [PunRPC]
    public void SendMessageRPC(string message)
    {
        updateText.text = message;
        StartCoroutine("Remove");
    }
    IEnumerator Remove()
    {
        yield return new WaitForSeconds(4f);
        BubbleSpeachObject.SetActive(false);
        disableSend = false;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(BubbleSpeachObject.active);
        }
        else if (stream.IsReading)
        {
            BubbleSpeachObject.SetActive((bool)stream.ReceiveNext());
        }
    }
    public void OpenCHAT()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && isOpen == false)
        {
            GetComponent<TopDownCharacterMover>().enabled = false;
            isOpen = true;
            ChatMenu.SetActive(true);
            ChatInputField.Select();
            ChatInputField.ActivateInputField();
        }
        else if (Input.GetKeyUp(KeyCode.Tab) && isOpen == true)
        {
            GetComponent<TopDownCharacterMover>().enabled = true;
            isOpen = false;
            ChatMenu.SetActive(false);
        }
    }
}
