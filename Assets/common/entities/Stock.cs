using System.Collections.Generic;
using UnityEngine;

public class Stock
{
    public string Name { get; }
    public Queue<int> StockPriceHistory { get; set; } = new Queue<int>(9);
    public int CurrentStockPrice { get; private set; }

    public List<StockPurchaseRecord> BuyRecord = new List<StockPurchaseRecord>();
    public List<StockPurchaseRecord> SellRecord = new List<StockPurchaseRecord>();

    // class in charge of the stock name and price list, edit this with db
    public Stock(string name, int initialPrice)
    {
        Name = name;
        for (int i = 0; i < 9; i++)
        {
            StockPriceHistory.Enqueue(Random.Range(initialPrice - 10, initialPrice + 10));
        }

        CurrentStockPrice = initialPrice;
    }

    public void ChangePrice(int fluctuationRate)
    {
        int newStockPrice = CurrentStockPrice + fluctuationRate;

        StockPriceHistory.Dequeue();
        StockPriceHistory.Enqueue(CurrentStockPrice);
        CurrentStockPrice = newStockPrice;
    }
}
