using UnityEngine;
using UnityEngine.UI;
public class RoomItem : MonoBehaviour
{
    public Text roomName;
    CreateTheLoby manager;

    public void Start()
    {
        manager = FindObjectOfType<CreateTheLoby>();
    }
    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }
    public void OnClickItem()
    {
        manager.JoinRoom(roomName.text);
        roomName.text = "Joining Room ...";
    }
}
