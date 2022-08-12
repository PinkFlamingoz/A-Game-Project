using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI scoreDisplay;
    public Button restartButton;
    public Button recordScoreButton;
    public TextMeshProUGUI waitingForHost;
    PhotonView view;
    private void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MultiPlayGame"))
        {
            view = GetComponent<PhotonView>();
            FindObjectOfType<FinalScore>().SetFinalScore();
            scoreDisplay.text = "SCORE : " + FindObjectOfType<PointsDisplay>().currentPoints.ToString()
                            + "\nWAVE : " + FindObjectOfType<WaveSpawner>().nextWave.ToString()
                            + "\nLOOP COUNT : " + FindObjectOfType<WaveSpawner>().loopCount.ToString();

            if (PhotonNetwork.IsMasterClient == false)
            {
                restartButton.interactable = false;
                waitingForHost.text = "WAITING FOR HOST TO RESTART";
            }
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainGame"))
        {
            FindObjectOfType<FinalScore>().SetFinalScore();
            scoreDisplay.text = "SCORE : " + FindObjectOfType<PlayerStatus>().points.ToString()
                            + "\nWAVE : " + FindObjectOfType<WaveSpawner>().nextWave.ToString()
                            + "\nLOOP COUNT : " + FindObjectOfType<WaveSpawner>().loopCount.ToString();
        }

    }
    public void OnClickRestart()
    {
        view.RPC("RestartRPC", RpcTarget.All);
    }
    [PunRPC]
    public void RestartRPC()
    {
        PhotonNetwork.LoadLevel("MultiPlayGame");
    }
    public void onRestartSP()
    {
        SceneManager.LoadScene("MainGame");
    }
}
