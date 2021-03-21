using System;
using System.Collections.Generic;

class LeaderboardController
{
    static readonly string DATETIME_FORMAT = "d/M/yyyy h:mm";

    readonly IPlayerRecordDAO playerRecordDAO;

    public LeaderboardController(IPlayerRecordDAO playerRecordDAO)
    {
        this.playerRecordDAO = playerRecordDAO;
    }

    public List<PlayerRecord> GetLeaderboard()
    {
        return playerRecordDAO.RetrievePlayerRecords();
    }

    public string ConvertToDateTimeString(long milliseconds)
    {
        return new DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToString(DATETIME_FORMAT);
    }
}
