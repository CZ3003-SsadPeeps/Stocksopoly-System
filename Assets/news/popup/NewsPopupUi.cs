using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public void OnBackButtonClick()
    {
        SceneManager.UnloadSceneAsync("News");
    }
}
