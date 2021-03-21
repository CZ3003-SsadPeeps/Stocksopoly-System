using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Database;

public class LeaderboardUi : MonoBehaviour
{
    static readonly string DATETIME_FORMAT = "d/M/yyyy hh:mm";
    static readonly string[] HEADERS = { "Name", "ID", "Date", "Credit" };

    // Start is called before the first frame update
    public Text TextPrefab;
    public Transform content;

    LeaderboardController controller;

    void Start()
    {
        controller = new LeaderboardController(new PlayerRecordDAO());
        List<PlayerRecord> playerRecords = controller.GetLeaderboard();

        // Load headers
        Text textObject;
        foreach (string header in HEADERS)
        {
            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(content, false);
            textObject.fontStyle = FontStyle.Bold;
            textObject.text = header;
            textObject.transform.localScale = new Vector3(1, 1, 1);
        }

        // Load player details
        DateTime dateTime;
        foreach (PlayerRecord playerRecord in playerRecords)
        {
            dateTime = new DateTime(1970, 1, 1).AddMilliseconds(playerRecord.DateAchieved);

            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(content, false);
            textObject.text = playerRecord.Name;
            textObject.transform.localScale = new Vector3(1, 1, 1);

            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(content, false);
            textObject.text = playerRecord.PlayerID.ToString();
            textObject.transform.localScale = new Vector3(1, 1, 1);

            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(content, false);
            textObject.text = dateTime.ToString(DATETIME_FORMAT);
            textObject.transform.localScale = new Vector3(1, 1, 1);

            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(content, false);
            textObject.text = playerRecord.CreditEarned.ToString();
            textObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void backTopPreviousScene()
    {
        SceneManager.UnloadSceneAsync("Leaderboard", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
    }
}
