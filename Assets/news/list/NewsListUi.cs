using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Code to display the news onto the Unity UI
/// <br></br>
/// Created by Khairuddin Bin Ali
/// </summary>
public class NewsListUi : MonoBehaviour
{
    public NewsList newsList;

    readonly NewsListManager manager = new NewsListManager();

    void Start()
    {
        newsList.SetNewsList(manager.GetNewsList());
    }
    /// <summary>
    /// Loads the list of news on button click
    /// </summary>
    public void OnBackButtonClick()
    {
        SceneManager.UnloadSceneAsync("NewsList");
    }
}
