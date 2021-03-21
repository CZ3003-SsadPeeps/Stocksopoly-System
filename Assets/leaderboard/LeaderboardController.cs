using System.Collections.Generic;

class LeaderboardController
{
    IPlayerRecordDAO playerRecordDAO;

    public LeaderboardController(IPlayerRecordDAO playerRecordDAO)
    {
        this.playerRecordDAO = playerRecordDAO;
    }

    public List<PlayerRecord> GetLeaderboard()
    {
        return playerRecordDAO.RetrievePlayerRecords();
    }
}
