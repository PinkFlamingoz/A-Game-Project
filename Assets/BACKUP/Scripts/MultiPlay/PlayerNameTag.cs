using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerNameTag : MonoBehaviourPun
{
    [SerializeField] public TextMeshProUGUI nameText;
    public void Start()
    {
        if (photonView.IsMine) { SetName(); }

        SetName();
    }

    private void SetName()
    {
        nameText.text = photonView.Owner.NickName;

    }
}
