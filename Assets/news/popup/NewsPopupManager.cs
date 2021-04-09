/// <summary>
/// News pops up using this class
/// <br></br>
///  Created by Khairuddin Bin Ali
/// </summary>
public class NewsPopupManager
{
    /// <summary>
    /// Used to get news from NewsStore
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Updates the stock price using news, randomizes stock price for other stocks
    /// </summary>
    /// <param name="Cname"></param>
    /// <param name="FlucRate"></param>
    void UpdateStockPrice(string Cname, int FlucRate)
    {
        GameStore.ShouldUpdatePlayerStock = true;
        foreach (Stock stock in StockStore.Stocks)
        {
            stock.ChangePrice((stock.Name == Cname) ? FlucRate : 0);
        }

        GameStore.ShouldUpdatePlayerStock = false;
    }
}
