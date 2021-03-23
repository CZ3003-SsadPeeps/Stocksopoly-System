using System.Collections.Generic;

class StockListManager
{
    public List<Stock> GetAllStocks()
    {
        return StockStore.Stocks;
    }
}
