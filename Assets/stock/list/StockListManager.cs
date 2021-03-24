using System.Collections.Generic;

class StockListManager
{
    internal List<Stock> GetAllStocks()
    {
        return StockStore.Stocks;
    }
}
