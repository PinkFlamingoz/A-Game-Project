using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateTheLoby : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject PanelRoomsCreateAndJoin;
    public GameObject PanelCreatedRoom;
    public InputField RoomNameInputField;

    public Text roomName;

    public RoomItem RoomItemPrefab;
    List<RoomItem> RoomItemsList = new List<RoomItem>();

    public Transform contentObject;

    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;

    public Text buttonText;

    public List<PlayerCard> playerCardsList = new List<PlayerCard>();
    public PlayerCard playerCardPrefab;
    public Transform playerCardParent;

    public GameObject dummyBox;

    public Button startGameButton;

    private bool PlayerReady = false;
    private ExitGames.Client.Photon.Hashtable _playerCustomProperties = new ExitGames.Client.Photon.Hashtable();

    public TextMeshProUGUI RPNumberText;
    public Button readyButton;
    public Button notYetButton;
    PhotonView view;
    private void Start()
    {
        PlayerReady = false;
        PhotonNetwork.SetPlayerCustomProperties(_playerCustomProperties);
        _playerCustomProperties["PlayerReady"] = PlayerReady;
        PhotonNetwork.SetPlayerCustomProperties(_playerCustomProperties);
        _playerCustomProperties["PlayerReady"] = PlayerReady;
        startGameButton.interactable = false;
        notYetButton.interactable = false;
        PhotonNetwork.JoinLobby();
    }
    public void OnClickCreate()
    {
        if (RoomNameInputField.text.Length >= 1)
        {
            buttonText.text = "Creating Room Please Wait Kind Sir";
            PhotonNetwork.CreateRoom(RoomNameInputField.text, new RoomOptions { MaxPlayers = 4, BroadcastPropsChangeToAll = true });
        }
        else
        {
            dummyBox.SetActive(true);
        }
    }
    public override void OnJoinedRoom()
    {
        PanelRoomsCreateAndJoin.SetActive(false);
        PanelCreatedRoom.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
        view = GetComponent<PhotonView>();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }
    private void UpdateRoomList(List<RoomInfo> List)
    {
        foreach (RoomItem item in RoomItemsList)
        {
            Destroy(item.gameObject);
        }
        RoomItemsList.Clear();
        foreach (RoomInfo room in List)
        {
            if (room.RemovedFromList)
            {
                return;
            }
            RoomItem newRoom = Instantiate(RoomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            RoomItemsList.Add(newRoom);
        }
    }

    public void JoinRoom(String roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    public void OnClickLeaveRoom()
    {
        buttonText.text = "CREATE ROOM";
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PlayerReady = false;
        PhotonNetwork.SetPlayerCustomProperties(_playerCustomProperties);
        _playerCustomProperties["PlayerReady"] = PlayerReady;
        PhotonNetwork.SetPlayerCustomProperties(_playerCustomProperties);
        _playerCustomProperties["PlayerReady"] = PlayerReady;
        readyButton.interactable = true;
        notYetButton.interactable = false;

        PanelCreatedRoom.SetActive(false);
        PanelRoomsCreateAndJoin.SetActive(true);
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnClickBack()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }

    void UpdatePlayerList()
    {
        foreach (PlayerCard item in playerCardsList)
        {
            Destroy(item.gameObject);
        }
        playerCardsList.Clear();
        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerCard newPlayerCard = Instantiate(playerCardPrefab, playerCardParent);
            newPlayerCard.SetPlayerInfo(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerCard.ApplyLocalChanges();
            }
            playerCardsList.Add(newPlayerCard);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }
    public void OnClickThankYOU()
    {
        dummyBox.SetActive(false);
    }
    public void Update()
    {
        UpdatePRNumberText();
        //find inactive object
        //RPNumberText = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>().FirstOrDefault(g => g.CompareTag("Active"));
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 1 && AllPlayersReady) // change to 4
        {
            startGameButton.interactable = true;
        }
        else
        {
            startGameButton.interactable = false;
        }
    }
    public void OnClickStartGameButton()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if (AllPlayersReady == true)
        {
            PhotonNetwork.LoadLevel("MultiPlayGame");
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }
    private bool AllPlayersReady => PhotonNetwork.PlayerList.All(player => (bool)player.CustomProperties["PlayerReady"] == true);
    public void onClickReadyButton()
    {
        PlayerReady = true;
        PhotonNetwork.SetPlayerCustomProperties(_playerCustomProperties);
        _playerCustomProperties["PlayerReady"] = PlayerReady;
        PhotonNetwork.SetPlayerCustomProperties(_playerCustomProperties);
        _playerCustomProperties["PlayerReady"] = PlayerReady;
        readyButton.interactable = false;
        notYetButton.interactable = true;
    }
    public void onClickNotYet()
    {
        PlayerReady = false;
        PhotonNetwork.SetPlayerCustomProperties(_playerCustomProperties);
        _playerCustomProperties["PlayerReady"] = PlayerReady;
        PhotonNetwork.SetPlayerCustomProperties(_playerCustomProperties);
        _playerCustomProperties["PlayerReady"] = PlayerReady;
        readyButton.interactable = true;
        notYetButton.interactable = false;
    }
    public void UpdatePRNumberText()
    {
        photonView.RPC("UpdatePRNumberTextRPC", RpcTarget.AllBuffered, RPNumberText.text);
    }
    [PunRPC]
    public void UpdatePRNumberTextRPC(string message)
    {
        if (AllPlayersReady == true)
        {
            RPNumberText.text = PhotonNetwork.CurrentRoom.PlayerCount + " READY";
            message = RPNumberText.text;

        }
        else if (AllPlayersReady == false)
        {
            RPNumberText.text = "WAITING FOR PLAYERS";
            message = RPNumberText.text;
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(RPNumberText.text);
        }
        else if (stream.IsReading)
        {
            RPNumberText.text = (string)stream.ReceiveNext();
        }
    }
}
