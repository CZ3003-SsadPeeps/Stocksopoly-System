using UnityEngine;
using UnityEngine.SceneManagement;

public class NewsListUi : MonoBehaviour
{
    public NewsList newsList;

    readonly NewsListManager manager = new NewsListManager();

    void Start()
    {
        newsList.SetNewsList(manager.GetNewsList());
    }

    public void OnBackButtonClick()
    {
        SceneManager.UnloadSceneAsync("NewsList");
    }
}
