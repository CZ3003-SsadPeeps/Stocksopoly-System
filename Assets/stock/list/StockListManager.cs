using System.Collections.Generic;
/// <summary>
/// Created by Khairuddin Bin Ali
/// </summary>
class StockListManager
{
    internal List<Stock> GetAllStocks()
    {
        return StockStore.Stocks;
    }
}
