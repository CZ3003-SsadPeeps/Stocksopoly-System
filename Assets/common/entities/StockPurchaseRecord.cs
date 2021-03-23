public class StockPurchaseRecord
{
    public string StockName { get; }

    public int Quantity { get; private set;  } = 0;

    public int AverageStockPrice { get; private set; } = 0;

    public StockPurchaseRecord(string stockName)
    {
        StockName = stockName;
    }

    public void AddQuantity(int quantity, int currentStockPrice)
    {
        if (quantity > 0)
        {
            AverageStockPrice = ((Quantity * AverageStockPrice) + (quantity * currentStockPrice)) / (Quantity + quantity);
        }

        Quantity += quantity;
    }

    public void Reset()
    {
        Quantity = 0;
        AverageStockPrice = 0;
    }
}
