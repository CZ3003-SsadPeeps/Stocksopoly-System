using System.Collections.Generic;
using UnityEngine;
using Database;

public class NewsStore
{
    //stores the list of news objects
    private static readonly List<News> news = new List<News>();

    public static bool IsNewsLoaded() { return news.Count > 0; }

    public static void LoadNews()
    {
        NewsRecordDAO dao = new NewsRecordDAO();
        news.AddRange(dao.RetrieveNewsRecords());
    }

    public static News GetRandomNews() { return news[Random.Range(0, news.Count)]; }
}
