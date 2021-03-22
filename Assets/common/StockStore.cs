using System.Collections.Generic;
using Database;

public class StockStore
{
    //initializes the list of stocks and stores here
    public static List<Stock> Stocks { get; } = new List<Stock>();

    // initializes the stock pointer to -1
    public static int SelectedStockPos = -1;

    // throws the stock obj to window to display when selected
    public static Stock SelectedStock
    {
        get
        {
            return Stocks[SelectedStockPos];
        }
    }

    public static bool IsStockLoaded()
    {
        return Stocks.Count > 0;
    }

    public static void LoadStocks()
    {
        StockDAO dao = new StockDAO();
        Stocks.AddRange(dao.RetrieveStocks());
    }
}
