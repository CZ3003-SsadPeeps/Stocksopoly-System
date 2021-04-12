/// <summary>
/// In charge of getting stock price for each player, and the stock purchased by the players.
/// ///<br></br>
/// Created by Khairuddin Bin Ali
/// </summary>
class StockDetailsManager
{
    public Stock Stock { get; } = StockStore.SelectedStock;
    public Player Player { get; } = GameStore.CurrentPlayer;
    public StockPurchaseRecord PurchaseRecord { get; }
    public int QuantityChange { get; set; }

    internal StockDetailsManager()
    {
        // Cannot be initialized in field because Player & Stock attributes are not initialized yet
        PurchaseRecord = StockStore.GetPurchaseRecord(Player.Name, Stock.Name);
        QuantityChange = PurchaseRecord.Quantity;
    }

    internal void SaveQuantityChange()
    {
        PurchaseRecord.AddQuantity(QuantityChange, Stock.CurrentStockPrice);
        Player.AddCredit(-(QuantityChange * Stock.CurrentStockPrice));
    }
}
