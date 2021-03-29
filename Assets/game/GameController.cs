using Random = UnityEngine.Random;
using System;
using System.Collections.Generic;

class GameController
{
    static readonly int GO_PAYOUT = 150;
    static readonly int MAX_LAPS = 14;

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
        get { return GameStore.PrevPlayerPos; }
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
        return stockTrader.GetPlayerStocks(GameStore.CurrentPlayer.Name);
    }

    internal int GenerateDiceValue()
    {
        // [Note] Upper bound is exclusive
        return Random.Range(1, 7);
    }

    internal void IssueGoPayout()
    {
        GameStore.CurrentPlayer.AddCredit(GO_PAYOUT);
    }

    internal bool NextTurn()
    {
        return GameStore.IncrementTurn();
    }

    internal bool HasReachedMaxLaps(int numLaps)
    {
        return numLaps >= MAX_LAPS;
    }

    internal void SavePlayerScores()
    {
        stockTrader.SellAllStocks(GameStore.Players);

        PlayerRecord[] records = new PlayerRecord[GameStore.Players.Length];
        Player player;
        for (int i = 0; i < records.Length; i++)
        {
            player = GameStore.Players[i];
            records[i] = new PlayerRecord(player.Name, player.Credit, (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
        }

        playerRecordDAO.StorePlayerRecords(records);
    }
}
