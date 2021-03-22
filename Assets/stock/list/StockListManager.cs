using System.Collections.Generic;

class StockListManager
{
    public List<Stock> GetAllStocks()
    {
        if (!StockStore.IsStockLoaded())
        {
            StockStore.LoadStocks();
        }

        return StockStore.Stocks;
    }
}
