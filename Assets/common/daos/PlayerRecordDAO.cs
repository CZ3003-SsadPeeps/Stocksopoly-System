using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Database
{
    /// <summary>
    /// A class to query and store player-related information. This class prepares the SQL statement
    /// <br> and abstracts the communication with the database.
    /// 
    /// </summary>
    public class PlayerRecordDAO : SqliteHelper, IPlayerRecordDAO
    {
        private const String TABLE_NAME = "PlayerRecord";
        private const String KEY_PlayerID = "PlayerID";
        private const String KEY_Name = "Name";
        private const String KEY_DateAchieved = "DateAchieved";
        private const String KEY_CreditEarned = "CreditEarned";

        /// <summary>
        /// Constructor, creating table if table not exists in the database
        /// </summary>
        public PlayerRecordDAO() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_PlayerID + " INTEGER PRIMARY KEY," +
                KEY_Name + " TEXT NOT NULL, " +
                KEY_DateAchieved + " INTEGER NOT NULL, " +
                KEY_CreditEarned + " INTEGER NOT NULL)";
            dbcmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Store information of a player into the database
        /// </summary>
        /// <param name="player"> Player object to store</param>        

        public void addData(PlayerRecord player)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                // + KEY_PlayerID + ", "
                + KEY_Name + ", "
                + KEY_DateAchieved + ", "
                + KEY_CreditEarned + " ) "

                + "VALUES ( '"
                // + "NULL" + "', '"
                + player.Name + "', '"
                + player.DateAchieved + "', '"
                + player.CreditEarned + "' )";
            dbcmd.ExecuteNonQuery();

        }

        /// <summary>
        /// Retreive related information of a player
        /// </summary>
        /// <param name="str">Player ID</param>
        /// <returns>List of player record objects</returns>
        public List<PlayerRecord> getDataByString(string str)
        {
            Debug.Log(Tag + "Getting Location: " + str);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_PlayerID + " = '" + str + "'";
            return convertToList(dbcmd.ExecuteReader());
        }

        /// <summary>
        /// Delete related information of a player
        /// </summary>
        /// <param name="str">Player ID</param>
        public override void deleteDataByString(string str)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText = "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_PlayerID + " = '" + str + "'";
            dbcmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Delete the entire table
        /// </summary>
        public override void deleteAllData()
        {
            Debug.Log(Tag + "Deleting Table");

            base.deleteAllData(TABLE_NAME);
        }

        /// <summary>
        /// Dummy funciton for debug purpose
        /// </summary>
        public void doNothing()
        {
            Debug.Log("PlayerRecordDAO");
        }
        /// <summary>
        /// Store all player records in the array
        /// </summary>
        /// <param name="playerRecords">Array containing one or more player record objects</param>
        /// <returns>True</returns>

        public bool StorePlayerRecords(PlayerRecord[] playerRecords)
        {
            foreach (PlayerRecord player in playerRecords)
            {
                addData(player);
            }
            return true;
        }

        /// <summary>
        /// Retrieve player records with top 30 highest credit earned
        /// </summary>
        /// <returns>List of player record objects</returns>
        public List<PlayerRecord> RetrievePlayerRecords()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " ORDER BY " + KEY_CreditEarned + " DESC LIMIT 30";
            System.Data.IDataReader reader = dbcmd.ExecuteReader();
            return convertToList(reader);
        }

        /// <summary>
        /// A helper fucntion to convert query result to correct format
        /// </summary>
        /// <param name="reader">Query to be executed</param>
        /// <returns>List of player record objects</returns>
        public List<PlayerRecord> convertToList(System.Data.IDataReader reader)
        {
            List<PlayerRecord> res = new List<PlayerRecord>();
            while (reader.Read())
            {
                res.Add(new PlayerRecord(Convert.ToInt32(reader[0]), reader[1].ToString(), Convert.ToInt32(reader[3]), (long)reader[2]));
            }
            return res;
        }
    }
}