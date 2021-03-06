using System.Collections.Generic;
using Database;
/// <summary>
/// Where the stock information is pulled from database
/// <br></br>
/// Created by Khairrudin Bin Ali
/// </summary>
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

    public static void LoadPurchaseRecords()
    {
        // Load stocks if not loaded
        if (Stocks.Count == 0)
        {
            StockDAO dao = new StockDAO();
            Stocks.AddRange(dao.RetrieveStocks());
        }

        purchaseRecords.Clear();

        List<StockPurchaseRecord> stockPurchaseRecords;
        foreach (Player player in GameStore.Players)
        {
            stockPurchaseRecords = new List<StockPurchaseRecord>();
            foreach (Stock stock in Stocks)
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

    public static List<StockPurchaseRecord> GetPlayerPurchaseRecord(string playerName)
    {
        return purchaseRecords[playerName];
    }

    public static int GetPriceOfStock(string name)
    {
        return Stocks.Find(stock => stock.Name == name).CurrentStockPrice;
    }
}
