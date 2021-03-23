using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewsPopupUi : MonoBehaviour
{
    public Text newsContentText;

    NewsManager manager;

    void Start()
    {
        manager = new NewsManager();
        News news = manager.GetNews();

        newsContentText.text = $"{news.CompanyName}: {news.Content}";
    }

    public void OnBackButtonClick()
    {
        SceneManager.UnloadSceneAsync("News");
    }
}
