using System.Collections.Generic;
using UnityEngine;
using Database;
/// <summary>
/// Where the News is pulled from databse and stored
/// <br></br>
/// Made by Ting Qi
/// </summary>
public class NewsStore
{
    //stores the list of news objects
    static readonly List<News> news = new List<News>();

    public static List<News> issuedNews { get; } = new List<News>();

    public static bool IsNewsLoaded() { return news.Count > 0; }

    public static void LoadNews()
    {
        NewsRecordDAO dao = new NewsRecordDAO();
        news.AddRange(dao.RetrieveNewsRecords());
    }

    public static void ResetNews()
    {
        issuedNews.Clear();
    }

    public static News GetRandomNews() {
        News randomNews = news[Random.Range(0, news.Count)];
        issuedNews.Add(randomNews);
        return randomNews;
    }
}
