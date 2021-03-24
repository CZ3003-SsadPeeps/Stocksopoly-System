using System.Collections.Generic;

class NewsListManager
{
    public List<News> GetNewsList()
    {
        return NewsStore.issuedNews;
    }
}