/// <summary>
/// This is an entity class that stores a player's details in the database & leaderboard.
/// <br/><br/>
/// Created by Khairuddin Bin Ali
/// </summary>
public class PlayerRecord
{
    /// <summary>
    /// A unique ID assigned to the player by the database management system
    /// </summary>
    public int PlayerID { get; }

    /// <summary>
    /// The name of the player
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The amount of credit that the player has accumulated during a game
    /// </summary>
    public int CreditEarned { get; }

    /// <summary>
    /// The date & time when the player score is saved to the database in Unix time
    /// </summary>
    public long DateAchieved { get; }

    /// <summary>
    /// Constructs this class without an ID. To be used when the player details have not been saved to
    /// the database.
    /// </summary>
    /// <param name="name">Name of the player</param>
    /// <param name="credit">Amount of credit the player has earned</param>
    /// <param name="dateAchieved">The current date & time in Unix time</param>
    public PlayerRecord(string name, int credit, long dateAchieved): this(-1, name, credit, dateAchieved) {}

    /// <summary>
    /// Constructs this class. To be used when retrieving details from the database.
    /// </summary>
    /// <param name="ID">Unique ID assigned by database management system</param>
    /// <param name="name">Name of the player</param>
    /// <param name="credit">Amount of credit earned</param>
    /// <param name="dateAchieved">When these details are saved in the database in Unix time</param>
    public PlayerRecord(int ID, string name, int credit, long dateAchieved)
    {
        PlayerID = ID;
        Name = name;
        CreditEarned = credit;
        DateAchieved = dateAchieved;
    }
}
