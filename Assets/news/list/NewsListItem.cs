using UnityEngine;
using UnityEngine.UI;

public class NewsListItem : MonoBehaviour
{
    public Text newsText;

    internal void SetNews(News news)
    {
        newsText.text = $"{news.CompanyName} - {news.Content}";
    }
}
