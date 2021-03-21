using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Database;

public class LeaderboardUi : MonoBehaviour
{
    public ScrollRect scrollView;
    public Transform scrollViewContent;
    public Text contentTextPrefab;

    LeaderboardController controller;

    void Start()
    {
        controller = new LeaderboardController(new PlayerRecordDAO());
        List<PlayerRecord> playerRecords = controller.GetLeaderboard();

        Text contentText;
        foreach(PlayerRecord playerRecord in playerRecords)
        {
            // Display ID
            contentText = Instantiate(contentTextPrefab);
            contentText.transform.SetParent(scrollViewContent, false);
            contentText.text = playerRecord.PlayerID.ToString();

            // Display name
            contentText = Instantiate(contentTextPrefab);
            contentText.transform.SetParent(scrollViewContent, false);
            contentText.text = playerRecord.Name;

            // Display date achieved
            contentText = Instantiate(contentTextPrefab);
            contentText.transform.SetParent(scrollViewContent, false);
            contentText.text = controller.ConvertToDateTimeString(playerRecord.DateAchieved);

            // Display credit earned
            contentText = Instantiate(contentTextPrefab);
            contentText.transform.SetParent(scrollViewContent, false);
            contentText.text = playerRecord.CreditEarned.ToString();
        }
    }

    public void OnBackButtonClick()
    {
        SceneManager.UnloadSceneAsync("Leaderboard");
    }
}
