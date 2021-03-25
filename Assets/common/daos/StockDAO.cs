using System.Collections.Generic;
using System.Data;

namespace Database
{
    public class StockDAO : SqliteHelper
    {
        static readonly string TABLE_NAME = "Stock";
        static readonly string KEY_NAME = "name";
        static readonly string KEY_INITIAL_PRICE = "initialPrice";

        public StockDAO() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = $"CREATE TABLE IF NOT EXISTS {TABLE_NAME} ("
                + $"{KEY_NAME} TEXT PRIMARY KEY NOT NULL"
                + $", {KEY_INITIAL_PRICE} INT NOT NULL"
                + ")";
            dbcmd.ExecuteNonQuery();
        }

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

        public void addData(Stock Stock)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_NAME + ", "
                + KEY_INITIAL_PRICE + " ) "

                + "VALUES ( \""
                + Stock.Name + "\", \""
                + Stock.StockPriceHistory.Peek() +"\" )";
            dbcmd.ExecuteNonQuery();

        }
        public bool StoreStock(Stock[] Stocks)
        {
            foreach (Stock stock in Stocks)
            {
                addData(stock);
            }
            return true;
        }

    }
}