using LootLocker.Requests;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GetScores : MonoBehaviour
{
    public int ID;
    int MaxScores = 100;
    //public TextMeshProUGUI[] Entries;
    public List<TextMeshProUGUI> Entries = new List<TextMeshProUGUI>();
    public TextMeshProUGUI ScorePrefab;
    public Transform contentObject;
    public void ShowScores()
    {
        LootLockerSDKManager.GetScoreList(ID, MaxScores, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] scores = response.items;
                for (int i = 0; i < scores.Length; i++)
                {

                    Entries[i].text = (scores[i].rank + "." + scores[i].member_id + " : " + scores[i].score);
                    TextMeshProUGUI newTextMeshProUGUI = Instantiate(ScorePrefab, contentObject);
                }
                if (scores.Length < MaxScores)
                {
                    for (int i = scores.Length; i < MaxScores; i++)
                    {

                        Entries[i].text = (i + 1).ToString() + ". NONE";
                        TextMeshProUGUI newTextMeshProUGUI = Instantiate(ScorePrefab, contentObject);
                    }
                }
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
}
