using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Code used to load News pop up into UI
/// <br></br>
/// Created by Khairuddin Bin Ali
/// </summary>
public class NewsPopupUi : MonoBehaviour
{
    public Text newsContentText;

    NewsPopupManager manager;

    void Start()
    {
        manager = new NewsPopupManager();
        News news = manager.GetNews();

        newsContentText.text = $"{news.CompanyName}: {news.Content}";
    }
    /// <summary>
    /// Loads the news when button is clicked
    /// </summary>
    public void OnBackButtonClick()
    {
        SceneManager.UnloadSceneAsync("News");
    }
}
