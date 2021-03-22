public class StockPurchaseRecord
{
    public string PlayerName { get; }

    public int TotalShare { get; }

    public int Price { get; }

    public StockPurchaseRecord(string playerName, int totalShare, int price)
    {
        this.PlayerName = playerName;
        this.TotalShare = totalShare;
        this.Price = price;
    }
}
