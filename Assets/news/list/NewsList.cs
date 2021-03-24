using System.Collections.Generic;
using UnityEngine;

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
