using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HomeTabScript : MonoBehaviour
{
    public void StartTheGame()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Application.LoadLevel(Application.loadedLevel);
            PressESCforBackToGame.GameIsPaused = false;
        }
    }

    public void QuitTheGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
    public void OnClickMainMenu()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MultiPlayGame"))
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("MainMenu");
        }
        SceneManager.LoadScene("MainMenu");
    }
}
