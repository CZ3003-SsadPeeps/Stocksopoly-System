using System.Collections.Generic;
using UnityEngine;
using Database;

/// Created by Lau Zhen Jie and Ng Ching Ting
class EventManager
{
    internal EventRecord GetEvent()
    {
        // Retrieve random event
        EventRecordDAO dao = new EventRecordDAO();
        List<EventRecord> events = dao.RetrieveEventRecords();
        EventRecord eventRecord = events[Random.Range(0, events.Count)];

        // Add to player's credit
        GameStore.CurrentPlayer.AddCredit(eventRecord.Amount);

        // Return event for UI to display
        return eventRecord;
    }
}
