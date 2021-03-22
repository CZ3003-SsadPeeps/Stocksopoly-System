using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyNSellStock : IStockTrader
{
    //I think need to pass in another attribute "round" from the board so that we can get the current price of the stock
    public List<PlayerStock> GetPlayerStocks(string playerName)
    {
        List<PlayerStock> temp = new List<PlayerStock>();
         
        for (int i=0; i< StockStore.stocks.Length; i++)
        {
            int PlayerShare = 0;
            int PurchasePrice = 0;
            Stock stock = StockStore.stocks[i];

            //Count total share the player had bought for a stock
            for (int j=0; j < stock.BuyRecord.Count; j++)
            {
                if (string.Compare(stock.BuyRecord[j].PlayerName, playerName)==0)
                {
                    PlayerShare += stock.BuyRecord[j].TotalShare;
                    PurchasePrice += stock.BuyRecord[j].Price;
                }
            }

            //Minus the number of stock the player had sold
            for (int k = 0; k < stock.SellRecord.Count; k++)
            {
                if (string.Compare(stock.SellRecord[k].PlayerName, playerName)==0)
                {
                    PlayerShare -= stock.BuyRecord[k].TotalShare;
                    PurchasePrice -= stock.SellRecord[k].Price;
                }
            }

            //round is needed to get the current price
            //temp.Add(PlayerStock(stock.Name, PlayerShare, PurchasePrice/PlayerShare, stock.StockPriceHistory[round]));
        }

        return temp;
    }

    //Use at the end of the game
    public void SellAllStocks(Player[] players)
    {
        List<PlayerStock> temp = new List<PlayerStock>();

        for (int j=0; j<players.Length; j++) 
        { 
            temp = GetPlayerStocks(players[j].Name);

            for (int i=0; i<temp.Count; i++)
            {
                //The Quantity might be negative if the player sell more than buy
                players[j].AddCredit(temp[i].Quantity * temp[i].CurrentStockPrice);
                //players[j].Credit += temp[i].Quantity * temp[i].CurrentStockPrice;
            }
        }
    }

    //Use this method when user press "Yes" in the prompt "Confirm to buy"
    public void buyStock(Player player, int amount, Stock stock, int price)
    {
        //Add into the buy record
        stock.BuyRecord.Add(new StockPurchaseRecord(player.Name, amount, price));

        player.AddCredit(-amount * price);
        //player.Credit -= amount * price;
    }

    //Use this method when user press "Yes" in the prompt "Confirm to sell"
    //Assume player can sell stock even they didn't buy
    public void sellStock(Player player, int amount, Stock stock, int price)
    {
        //Add into the sell record
        stock.SellRecord.Add(new StockPurchaseRecord(player.Name, amount, price));

        player.AddCredit(amount * price);
        //player.Credit += amount * price;
    }
}
