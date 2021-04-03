using System.Collections.Generic;

/// <summary>
/// This interface defines the methods for the board system to store & retrieve
/// the players' scores from the database system
/// <br/><br/>
/// Created by Khairuddin Bin Ali
/// </summary>
interface IPlayerRecordDAO
{
    /// <summary>
    /// Stores the players' scores to the database
    /// </summary>
    /// <param name="playerRecords">The players' scores</param>
    /// <returns>Whether the scores are stored successfully</returns>
    bool StorePlayerRecords(PlayerRecord[] playerRecords);

    /// <summary>
    /// Retrieves the top 30 player scores from the database
    /// </summary>
    /// <returns>A list of 30 player scores</returns>
    List<PlayerRecord> RetrievePlayerRecords();
}
