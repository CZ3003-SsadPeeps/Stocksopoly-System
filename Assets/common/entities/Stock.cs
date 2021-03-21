using System.Collections.Generic;

public class Stock
{
    static readonly System.Random random = new System.Random();

    public string Name { get; }
    public List<int> StockPriceHistory { get; set; } = new List<int>(10);

    public List<StockPurchaseRecord> BuyRecord = new List<StockPurchaseRecord>();
    public List<StockPurchaseRecord> SellRecord = new List<StockPurchaseRecord>();

    // class in charge of the stock name and price list, edit this with db
    public Stock(string name, int initialPrice)
    {
        Name = name;
        for (int i = 0; i < 9; i++)
        {
            StockPriceHistory.Add(random.Next(initialPrice - 10, initialPrice + 10));
        }

        StockPriceHistory.Add(initialPrice);
    }
}
