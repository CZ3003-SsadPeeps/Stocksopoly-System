/// <summary>
/// This is an entity class that stores a player's details during gameplay.
/// <br/><br/>
/// Created by Khairuddin Bin Ali
/// </summary>
public class Player
{
    /// <summary>
    /// The name of the player.
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// The amount of credit that the player has. This is set to 1024 at the start of the game.
    /// </summary>
    public int Credit { get; private set; } = 1024;

    /// <summary>
    /// Constructs a new player.
    /// </summary>
    /// <param name="name">Name of the player</param>
    public Player(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Adds to the player's credit, or subtracts if the amount is negative.
    /// </summary>
    /// <param name="amount">
    /// Amount to add. If amount is negative, the amount will subtract from the player's credit
    /// </param>
    public void AddCredit(int amount)
    {
        Credit += amount;
    }
}
