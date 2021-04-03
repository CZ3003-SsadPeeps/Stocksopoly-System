using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Data;

namespace Database
{
    /// <summary>
    /// This is a high level class which can be extended to other classes, in order to access basic operations of any database.
    /// Created by Rizwan Asif 
    /// source: https://medium.com/@rizasif92/sqlite-and-unity-how-to-do-it-right-31991712190
    /// </summary>
    public class SqliteHelper
    {
        /// <summary>
        /// A tag which will be used in constructor to indicate SqliteHelper is connected.
        /// </summary>
        public const string Tag = "Riz: SqliteHelper:\t";

        private const string database_name = "stocksopoly_database.db";

        private string db_connection_string;

        /// <summary>
        /// Database connection.
        /// </summary>
        public IDbConnection db_connection;

        /// <summary>
        /// Contructs this class. Build connection to the database file.
        /// </summary>
        public SqliteHelper()
        {
            Debug.Log(Tag + "Connected");

            string databaseFile;
#if UNITY_EDITOR
            databaseFile = $"Assets/Resources/{database_name}";
#else
            databaseFile = $"{Application.dataPath}/Resources/{database_name}";
#endif

            db_connection_string = $"URI=file:{databaseFile}";
            Debug.Log("db_connection_string" + db_connection_string);
            db_connection = new SqliteConnection(db_connection_string);
            db_connection.Open();
        }

        /// <summary>
        /// Destructor for this class.
        /// </summary>
        ~SqliteHelper()
        {
            Debug.Log(Tag + "Disconnected");
            db_connection.Close();
        }

        /// <summary>
        /// Virtual function for deleting data which has the id.
        /// </summary>
        /// <param name="id">Data ID in string format </param>
        public virtual void deleteDataByString(string id)
        {
            Debug.Log(Tag + "This function is not implemented");
            throw null;
        }

        /// <summary>
        /// Virtual function for deleting all data of the database table.
        /// </summary>
        public virtual void deleteAllData()
        {
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }

        /// <summary>
        /// Helper function to create sql command.
        /// </summary>
        /// <returns>SQL command</returns>
        public IDbCommand getDbCommand()
        {
            return db_connection.CreateCommand();
        }

   
        /// <summary>
        /// Delete all data exists in the table.
        /// </summary>
        /// <param name="table_name">Name of the table from database</param>
        public void deleteAllData(string table_name)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText = "DROP TABLE IF EXISTS " + table_name;
            dbcmd.ExecuteNonQuery();
        }

     
        /// <summary>
        /// Disconnect the database connection.
        /// </summary>
	public void close ()
        {
            db_connection.Close ();
	}
    }
}