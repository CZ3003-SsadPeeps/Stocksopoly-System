using Database;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardUi : MonoBehaviour
{
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
        foreach (PlayerRecord playerRecord in playerRecords)
        {
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
            textObject.text = playerRecord.DateAchieved.ToString();
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
