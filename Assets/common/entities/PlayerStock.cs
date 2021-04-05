/// <summary>
/// This is an entity class that stores the player's stock purchase record. The attributes are raad-only, so this
/// class is only meant for sending these information to the board system to display to the user.
/// <br/><br/>
/// Created by Khairuddin Bin Ali
/// </summary>
public class PlayerStock
{
    /// <summary>
    /// Name of the company whom the stocks are purchased from.
    /// </summary>
    public string CompanyName { get; }

    /// <summary>
    /// Number of stocks purchased.
    /// </summary>
    public int Quantity { get; }

    /// <summary>
    /// Average price spent on each stock.
    /// </summary>
    public int AvgPurchasePrice { get; }

    /// <summary>
    /// The current price of the stock in the stock market.
    /// </summary>
    public int CurrentStockPrice { get; }

    /// <summary>
    /// Constructor of the player's stock purchase record.
    /// </summary>
    /// <param name="companyName">Name of the company whom the stocks are purchased from</param>
    /// <param name="quantity">Number of stocks purchased</param>
    /// <param name="avgPurchasePrice">Average price spent on each stock.</param>
    /// <param name="currentStockPrice">Current prive of the stock</param>
    public PlayerStock(string companyName, int quantity, int avgPurchasePrice, int currentStockPrice)
    {
        CompanyName = companyName;
        Quantity = quantity;
        AvgPurchasePrice = avgPurchasePrice;
        CurrentStockPrice = currentStockPrice;
    }
}
