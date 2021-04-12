using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Used to load each News item into the list
/// <br></br>
/// Created by Khairuddin Bin Ali
/// </summary>
public class NewsListItem : MonoBehaviour
{
    public Text newsText;

    internal void SetNews(News news)
    {
        newsText.text = $"{news.CompanyName} - {news.Content}";
    }
}
