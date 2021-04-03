using System.Collections.Generic;
using System.Data;

namespace Database
{
    /// <summary>
    /// A class to query and store stock-related information. This class prepares the SQL statement
    /// <br> and abstracts the communication with the database.
    /// 
    /// </summary>
    public class StockDAO : SqliteHelper
    {
        static readonly string TABLE_NAME = "Stock";
        static readonly string KEY_NAME = "name";
        static readonly string KEY_INITIAL_PRICE = "initialPrice";

        /// <summary>
        /// Constructor, creating table if table not exists in the database
        /// </summary>
        public StockDAO() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = $"CREATE TABLE IF NOT EXISTS {TABLE_NAME} ("
                + $"{KEY_NAME} TEXT PRIMARY KEY NOT NULL"
                + $", {KEY_INITIAL_PRICE} INT NOT NULL"
                + ")";
            dbcmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Retrieve Stock objects
        /// </summary>
        /// <returns>List of Stock objects</returns>

        public List<Stock> RetrieveStocks()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME;
            IDataReader reader = dbcmd.ExecuteReader();

            List<Stock> stocks = new List<Stock>();
            while (reader.Read())
            {
                stocks.Add(new Stock(reader.GetString(0), reader.GetInt32(1)));
            }

            return stocks;
        }
    }
}