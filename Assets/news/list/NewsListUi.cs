using UnityEngine;
using UnityEngine.SceneManagement;

public class NewsListUi : MonoBehaviour
{
    public NewsList newsList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnBackButtonClick()
    {
        SceneManager.UnloadSceneAsync("NewsList");
    }
}
