using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Database
{
    public class NewsRecordDAO : SqliteHelper
    {
        static readonly string TABLE_NAME = "News";
        static readonly string KEY_NewsID = "NewsID";
        static readonly string KEY_CompanyName = "CompanyName";
        static readonly string KEY_Content = "Content";
        static readonly string KEY_FluctuationRate = "FluctuationRate";

        public NewsRecordDAO() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_NewsID + " INTEGER PRIMARY KEY, " +
                KEY_CompanyName + " TEXT NOT NULL, " +
                KEY_Content + " TEXT NOT NULL, " +
                KEY_FluctuationRate + " INTEGER NOT NULL)";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(News news)
        {
            try
            {
                IDbCommand dbcmd = getDbCommand();
                dbcmd.CommandText =
                    "INSERT INTO " + TABLE_NAME
                    + " ( "
                    + KEY_CompanyName + ", "
                    + KEY_Content + ", "
                    + KEY_FluctuationRate + " ) "

                    + "VALUES ( '"
                    + news.CompanyName + "', '"
                    + news.Content + "', '"
                    + news.FluctuationRate + "' )";
                dbcmd.ExecuteNonQuery();
            }
            catch
            {
                Debug.Log("Invalid data key because repeated. Please change a new key");

            }

        }

        public void StoreNewsRecords(List<News> newsRecords)
        {
            foreach (News news in newsRecords)
            {
                addData(news);
            }
        }

        public List<News> RetrieveNewsRecords()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = $"SELECT {KEY_CompanyName}, {KEY_Content}, {KEY_FluctuationRate} FROM {TABLE_NAME}";
            IDataReader reader = dbcmd.ExecuteReader();
            List<News> res = new List<News>();
            while (reader.Read())
            {
                res.Add(new News(reader.GetString(0), reader.GetString(1), reader.GetInt32(2)));
            }
            return res;
        }

    }
}