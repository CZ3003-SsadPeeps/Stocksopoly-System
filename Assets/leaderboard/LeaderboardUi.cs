using Database;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardUi : MonoBehaviour
{
    // Start is called before the first frame update
    public Text TextPrefab;
    public Transform content;

    void Start()
    {
        SetLeaderboardDetails();
    }

    internal void SetLeaderboardDetails()
    {
        //Remove existing stock info
        content.DetachChildren();

        Text textObject;

        string[] headers = { "Name", "ID", "Date", "Credit" };
        for (int i = 0; i < 4; i++)
        {
            textObject = Instantiate(TextPrefab) as Text;
            textObject.transform.SetParent(content, false);
            textObject.fontStyle = FontStyle.Bold;
            textObject.text = headers[i];
            textObject.transform.localScale = new Vector3(1, 1, 1);
        }
       

        PlayerRecordDAO playerRecordDAO = new PlayerRecordDAO();

        //add testdata
        //PlayerRecord[] testplayers = new PlayerRecord[30];
        //string testplayername = "xiaoming";
        //for (int i = 0; i < 30; i++)
        //{
        //    testplayers[i] = new PlayerRecord(i, testplayername + i.ToString(), i + 100, i);
        //}
        //playerRecordDAO.StorePlayerRecords(testplayers);



        List<PlayerRecord> playerRecords = playerRecordDAO.RetrievePlayerRecords();
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
        SceneManager.UnloadSceneAsync("Leaderboard");
    }
}
