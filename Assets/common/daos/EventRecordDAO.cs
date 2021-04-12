using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Database
{
    /// <summary>
    /// This is a DAO class inherited from SqliteHelper and created for database interaction for EventRecord.
    /// This class can store and retrieve EventRecord from database.
    /// <br/><br/>
    /// Created by Liew Zi Peng
    /// </summary>
    public class EventRecordDAO : SqliteHelper
    {

        private const String TABLE_NAME = "Event";
        private const String KEY_EventID = "EventID";
        private const String KEY_Content = "Content";
        private const String KEY_Amount = "Amount";

        /// <summary>
        /// Constructs this class. Create a EventRecord table in database if it hasn't exist yet.
        /// </summary>
        public EventRecordDAO() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_EventID + " INTEGER PRIMARY KEY, " +
                KEY_Content + " TEXT NOT NULL, " +
                KEY_Amount + " INTEGER NOT NULL)";
            dbcmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Stores EventRecord into database.
        /// </summary>
        /// <param name="eventRecord">Single EventRecord</param>
        public void addData(EventRecord eventRecord)
        {
            try
            {
                IDbCommand dbcmd = getDbCommand();
                dbcmd.CommandText =
                    "INSERT INTO " + TABLE_NAME
                    + " ( "
                    //+ KEY_EventID + ", "
                    + KEY_Content + ", "
                    + KEY_Amount + " ) "

                    + "VALUES ( '"
                    //+ eventRecord.EventID + "', '"
                    + eventRecord.Content + "', '"
                    + eventRecord.Amount + "' )";
                dbcmd.ExecuteNonQuery();
            }
            catch
            {
                Debug.Log("Invalid data key because repeated. Please change a new key");

            }

        }

        /// <summary>
        /// Stores List of EventRecord into database.
        /// </summary>
        /// <param name="eventRecords">List of EventRecord</param>
        public void StoreEventRecords(List<EventRecord> eventRecords)
        {
            foreach (EventRecord eventRecord in eventRecords)
            {
                addData(eventRecord);
            }
        }

        /// <summary>
        /// Retrieves EventRecord from database.
        /// </summary>
        /// <returns> List of EventRecord from database</returns>
        public List<EventRecord> RetrieveEventRecords()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME;
            System.Data.IDataReader reader = dbcmd.ExecuteReader();
            List<EventRecord> res = new List<EventRecord>();
            while (reader.Read())
            {
                res.Add(new EventRecord(Convert.ToInt32(reader[0]), reader[1].ToString(), Convert.ToInt32(reader[2])));
            }
            return res;
        }

    }
}