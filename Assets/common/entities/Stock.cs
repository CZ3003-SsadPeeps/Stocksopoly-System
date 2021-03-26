using System.Collections.Generic;
using UnityEngine;

public class Stock
{
    public string Name { get; }
    public Queue<int> StockPriceHistory { get; set; } = new Queue<int>(9);
    public int CurrentStockPrice { get; private set; }

    // class in charge of the stock name and price list, edit this with db
    public Stock(string name, int initialPrice)
    {
        Name = name;

        int price;
        for (int i = 0; i < 9; i++)
        {
            price = Random.Range(initialPrice - 10, initialPrice + 10);
            if (price < 1)
            {
                price = 1;
            }

            StockPriceHistory.Enqueue(price);
        }

        CurrentStockPrice = initialPrice;
    }

    public void ChangePrice(int fluctuationRate)
    {
        int newStockPrice = CurrentStockPrice + fluctuationRate;
        if (newStockPrice < 1)
        {
            newStockPrice = 1;
        }

        StockPriceHistory.Dequeue();
        StockPriceHistory.Enqueue(CurrentStockPrice);
        CurrentStockPrice = newStockPrice;
    }
}
