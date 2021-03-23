using System.Collections.Generic;

public class StockTrader : IStockTrader
{
    public List<PlayerStock> GetPlayerStocks(string playerName)
    {
        List<StockPurchaseRecord> purchaseRecords = StockStore.GetPlayerPurchaseRecord(playerName);
        List<PlayerStock> playerStocks = new List<PlayerStock>(purchaseRecords.Count);
        foreach (StockPurchaseRecord purchaseRecord in purchaseRecords)
        {
            if (purchaseRecord.Quantity == 0) continue;

            playerStocks.Add(new PlayerStock(
                purchaseRecord.StockName,
                purchaseRecord.Quantity,
                purchaseRecord.AverageStockPrice,
                StockStore.GetPriceOfStock(purchaseRecord.StockName)));
        }

        return playerStocks;
    }

    public void SellAllStocks(Player[] players)
    {
        List<StockPurchaseRecord> purchaseRecords;
        foreach (Player player in players)
        {
            purchaseRecords = StockStore.GetPlayerPurchaseRecord(player.Name);
            foreach (StockPurchaseRecord purchaseRecord in purchaseRecords)
            {
                if (purchaseRecord.Quantity == 0) continue;
                player.AddCredit(purchaseRecord.Quantity * StockStore.GetPriceOfStock(purchaseRecord.StockName));
            }
        }
    }
}
