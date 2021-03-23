using System.Collections.Generic;
using Database;

public class StockStore
{
    //initializes the list of stocks and stores here
    public static List<Stock> Stocks { get; } = new List<Stock>();
    public static Dictionary<string, List<StockPurchaseRecord>> purchaseRecords = new Dictionary<string, List<StockPurchaseRecord>>();

    // initializes the stock pointer to -1
    public static int SelectedStockPos = -1;

    // throws the stock obj to window to display when selected
    public static Stock SelectedStock
    {
        get { return Stocks[SelectedStockPos]; }
    }

    public static bool IsStockLoaded()
    {
        return Stocks.Count > 0;
    }

    public static void LoadStocks()
    {
        StockDAO dao = new StockDAO();
        Stocks.AddRange(dao.RetrieveStocks());

        List<StockPurchaseRecord> stockPurchaseRecords;
        foreach (Player player in GameStore.Players)
        {
            stockPurchaseRecords = new List<StockPurchaseRecord>();
            foreach(Stock stock in Stocks)
            {
                stockPurchaseRecords.Add(new StockPurchaseRecord(stock.Name));
            }

            purchaseRecords.Add(player.Name, stockPurchaseRecords);
        }
    }

    public static StockPurchaseRecord GetPurchaseRecord(string playerName, string stockName)
    {
        return purchaseRecords[playerName].Find(record => record.StockName == stockName);
    }
}
