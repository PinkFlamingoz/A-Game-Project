using UnityEngine;
public class PressESCforBackToGame : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject MenuGame;
    public GameObject Camera;
    public GameObject MenuCamera;
    public GameObject CountDown;
    // Start is called before the first frame update
    void Start()
    {
        MenuGame.SetActive(false);
        MenuCamera.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        Camera.SetActive(false);
        MenuGame.SetActive(true);
        MenuCamera.SetActive(true);
        CountDown.SetActive(false);
        //Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log(GameIsPaused + "pause");
    }

    public void Resume()
    {
        Camera.SetActive(true);
        MenuGame.SetActive(false);
        MenuCamera.SetActive(false);
        CountDown.SetActive(true);
        //Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log(GameIsPaused + "resume");
    }
}

