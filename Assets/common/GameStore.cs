/// <summary>
/// This is a class that stores the player details and current turn information & shares them with other subsystems.
/// This class should not be instantiated and can only be accessed through static methods.
/// <br/><br/>
/// Created by Khairuddin Bin Ali
/// </summary>
public class GameStore
{
    // Private constuctor to prevent other classes from constructing an instance of this class
    private GameStore() { }

    /// <summary>
    /// List of players in the game session.
    /// </summary>
    public static Player[] Players { get; private set; }

    /// <summary>
    /// The player that is currently playing.
    /// </summary>
    public static Player CurrentPlayer
    {
        get { return Players[CurrentPlayerPos]; }
    }

    /// <summary>
    /// Index of the player that's currently playing.
    /// </summary>
    public static int CurrentPlayerPos { get; private set; } = 0;

    /// <summary>
    /// Initializes the list of players.
    /// </summary>
    /// <param name="names">The names of the players</param>
    public static void InitPlayers(string[] names)
    {
        Players = new Player[names.Length];
        for (int i = 0; i < names.Length; i++)
        {
            Players[i] = new Player(names[i]);
        }

        CurrentPlayerPos = 0;
    }

    /// <summary>
    /// Increments the index of the current player. If the new index is equal to the number of players, the index is
    /// set to 0.
    /// </summary>
    public static void IncrementTurn()
    {
        CurrentPlayerPos = (CurrentPlayerPos + 1) % Players.Length;
    }

    /// <summary>
    /// Flag that determines if <c>GameUi</c> should update the list of the player's stock purchase record. If true,
    /// the list will be updated at every frame. Otherwise the list will not be updated.
    /// </summary>
    public static bool ShouldUpdatePlayerStock { get; set; } = false;
}
