using LootLocker.Requests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardController : MonoBehaviour
{
    public TMP_InputField MemberID;
    public TextMeshProUGUI PlayerScore;
    public float FinalScore;
    public int ID;
    public Button recordScore;
    private void Start()
    {

        LootLockerSDKManager.StartSession("Player", (response) =>
        {
            if (response.success)
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    public void Update()
    {
        PlayerScore.text = "FINAL SCORE :" + FindObjectOfType<FinalScore>().finalScore.ToString();
        FinalScore = FindObjectOfType<FinalScore>().finalScore;
    }
    public void SubmitScore()
    {
        recordScore.interactable = false;
        LootLockerSDKManager.SubmitScore(MemberID.text, (int)FinalScore, ID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
}
