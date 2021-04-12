using System.Collections.Generic;
/// <summary>
/// Used to get the news itself
/// <br></br>
/// Created by Khairuddin Bin Ali
/// </summary>
class NewsListManager
{
    internal List<News> GetNewsList()
    {
        return NewsStore.issuedNews;
    }
}