using System.Collections.Generic;

/// <summary>
/// This interface defines the methods for the board system to retrieve a player's
/// stock purchase records & sell all players' stocks at the end of the
/// game from the stock system
/// <br/><br/>
/// Created by Khairuddin Bin Ali
/// </summary>
interface IStockTrader
{
    /// <summary>
    /// Retrieves the stock purchase records of a player
    /// </summary>
    /// <param name="playerName">The player's name</param>
    /// <returns>
    /// A list of stock purchase records. This list will be empty if the
    /// player has not purchased any stocks yet
    /// </returns>
    List<PlayerStock> GetPlayerStocks(string playerName);

    /// <summary>
    /// Sells all players' stocks
    /// </summary>
    /// <param name="players">List of players that have just ended their game</param>
    void SellAllStocks(Player[] players);
}
