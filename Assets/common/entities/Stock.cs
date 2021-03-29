using System.Collections.Generic;
using UnityEngine;

public class Stock
{
    public string Name { get; }
    public int[] StockPriceHistory {
        get
        {
            int[] priceHistory = new int[10];
            PrevPrices.ToArray().CopyTo(priceHistory, 0);
            priceHistory[9] = CurrentStockPrice;

            return priceHistory;
        }
    }
    public int CurrentStockPrice { get; private set; }

    readonly Queue<int> PrevPrices = new Queue<int>(9);

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

            PrevPrices.Enqueue(price);
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

        PrevPrices.Dequeue();
        PrevPrices.Enqueue(CurrentStockPrice);
        CurrentStockPrice = newStockPrice;
    }
}
