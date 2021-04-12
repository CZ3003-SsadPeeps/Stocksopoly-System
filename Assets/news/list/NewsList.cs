using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class used to store each news into a list
/// <br></br>
/// Created by Ting Qi
/// </summary>
public class NewsList : MonoBehaviour
{
    public GameObject newsItemPrefab;
    public Transform contentRoot;

    internal void SetNewsList(List<News> newsList)
    {
        GameObject newsItemObject;
        NewsListItem newsListItem;
        foreach (News news in newsList)
        {
            newsItemObject = Instantiate(newsItemPrefab);
            newsItemObject.transform.SetParent(contentRoot, false);
            newsListItem = newsItemObject.GetComponent<NewsListItem>();
            newsListItem.SetNews(news);
        }
    }
}
