using System.Collections.Generic;

class NewsListManager
{
    internal List<News> GetNewsList()
    {
        return NewsStore.issuedNews;
    }
}