public class StockPurchaseRecord
{
    public string StockName { get; }

    public int Quantity { get; } = 0;

    public int AverageStockPrice { get; } = 0;

    public StockPurchaseRecord(string stockName)
    {
        StockName = stockName;
    }
}
