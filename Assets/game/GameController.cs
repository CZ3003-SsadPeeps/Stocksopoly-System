using System;
using System.Collections.Generic;

class GameController
{
    static readonly int GO_PAYOUT = 150;
    static readonly int[] DICE_VALUES = { 2, 3, 5, 6, 4, 5, 6, 1, 6 };
    static int DICE_POS = 0;

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
        int diceValue = DICE_VALUES[DICE_POS++];
        DICE_POS %= DICE_VALUES.Length;
        return diceValue;
    }

    internal void IssueGoPayout()
    {
        GameStore.CurrentPlayer.AddCredit(GO_PAYOUT);
    }

    internal bool NextTurn()
    {
        return GameStore.IncrementTurn();
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
