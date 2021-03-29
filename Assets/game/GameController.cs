using Random = UnityEngine.Random;
using System;
using System.Collections.Generic;

class GameController
{
    static readonly int GO_PAYOUT = 150;
    static readonly int MAX_LAPS = 1;
    static readonly int[] DICE_VALUES = { 2, 3, 5, 6, 4, 5, 6, 1, 6 };
    static int DICE_POS = 0;

    public Player[] Players
    {
        get { return GameStore.Players; }
    }

    public Player CurrentPlayer
    {
        get { return GameStore.CurrentPlayer; }
    }

    public int CurrentPlayerPos
    {
        get { return GameStore.CurrentPlayerPos; }
    }

    public int PrevPlayerPos
    {
        get { return (CurrentPlayerPos == 0 ? Players.Length : CurrentPlayerPos) - 1; }
    }

    public bool ShouldUpdatePlayerStock
    {
        get { return GameStore.ShouldUpdatePlayerStock; }
    }

    readonly IStockTrader stockTrader;
    readonly IPlayerRecordDAO playerRecordDAO;

    internal GameController(IStockTrader stockTrader, IPlayerRecordDAO playerRecordDAO)
    {
        this.stockTrader = stockTrader;
        this.playerRecordDAO = playerRecordDAO;
    }

    internal List<PlayerStock> GetPlayerStocks()
    {
        return stockTrader.GetPlayerStocks(CurrentPlayer.Name);
    }

    internal int GenerateDiceValue()
    {
        int diceValue = DICE_VALUES[DICE_POS++];
        DICE_POS %= DICE_VALUES.Length;
        return diceValue;
    }

    internal void IssueGoPayout()
    {
        CurrentPlayer.AddCredit(GO_PAYOUT);
    }

    internal bool NextTurn()
    {
        GameStore.IncrementTurn();
        return CurrentPlayerPos == 0;
    }

    internal bool HasReachedMaxLaps(int numLaps)
    {
        return numLaps >= MAX_LAPS;
    }

    internal void SavePlayerScores()
    {
        stockTrader.SellAllStocks(Players);

        PlayerRecord[] records = new PlayerRecord[Players.Length];
        Player player;
        for (int i = 0; i < records.Length; i++)
        {
            player = Players[i];
            records[i] = new PlayerRecord(player.Name, player.Credit, (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
        }

        playerRecordDAO.StorePlayerRecords(records);
    }
}
