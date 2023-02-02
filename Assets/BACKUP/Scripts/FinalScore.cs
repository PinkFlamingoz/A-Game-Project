using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScore : MonoBehaviour
{
    public float finalScore;
    public void SetFinalScore()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MultiPlayGame"))
        {
            finalScore = FindObjectOfType<PointsDisplay>().currentPoints * (FindObjectOfType<WaveSpawner>().nextWave + 1) * (FindObjectOfType<WaveSpawner>().loopCount + 1);
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainGame"))
        {
            finalScore = FindObjectOfType<PlayerStatus>().points * (FindObjectOfType<WaveSpawner>().nextWave + 1) * (FindObjectOfType<WaveSpawner>().loopCount + 1);
        }
    }

}

