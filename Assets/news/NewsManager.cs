﻿public class NewsManager
{
    public News GetNews()
    {
        if (!NewsStore.IsNewsLoaded())
        {
            NewsStore.LoadNews();
        }

        News news = NewsStore.GetRandomNews();
        UpdateStockPrice(news.CompanyName, news.FluctuationRate);

        return news;
    }

    // function to change the stock affected by fluc rate, while randomizing the chanage for other stocks
    void UpdateStockPrice(string Cname, int FlucRate)
    {
        foreach (Stock stock in StockStore.Stocks)
        {
            stock.ChangePrice((stock.Name == Cname) ? FlucRate : 0);
        }
    }
}
