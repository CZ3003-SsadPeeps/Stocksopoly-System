using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Database
{
    public class QuestionsNAnswersDAO : SqliteHelper
    {

        private const String TABLE_NAME = "QuestionsNAnswers";
        private const String KEY_QuizID = "QuizID";
        private const String KEY_Question = "Question";
        private const String KEY_Credit = "Credit";
        private const String KEY_Difficulty = "Difficulty";
        private const String KEY_CorrectOptionIndex = "CorrectOptionIndex";

        private const String TABLE_NAME2 = "QuestionOption";
        private const String KEY_Content = "Content";
        private const String KEY_OptionIndex = "OptionIndex";

        public QuestionsNAnswersDAO() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_QuizID + " INTEGER PRIMARY KEY, " +
                KEY_Question + " TEXT NOT NULL, " +
                KEY_CorrectOptionIndex + " INTEGER NOT NULL, " +
                KEY_Credit + " INTEGER NOT NULL, " +
                KEY_Difficulty + " TEXT NOT NULL)";
            dbcmd.ExecuteNonQuery();

            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME2 + " ( " +
                KEY_QuizID + " INTEGER, " +
                KEY_Content + " TEXT NOT NULL, " +
                KEY_OptionIndex + " INTEGER NOT NULL, " +
                "FOREIGN KEY(" + KEY_QuizID + ") REFERENCES " + TABLE_NAME + "(" + KEY_QuizID + "))";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(QuestionsNAnswers quizRecord)
        {
            try
            {
                IDbCommand dbcmd = getDbCommand();
                dbcmd.CommandText =
                    "INSERT INTO " + TABLE_NAME
                    + " ( "
                    + KEY_QuizID + ", "
                    + KEY_Question + ", "
                    + KEY_CorrectOptionIndex + ", "
                    + KEY_Credit + ", "
                    + KEY_Difficulty + " ) "

                    + "VALUES ( '"
                    + quizRecord.QuizID + "', '"
                    + quizRecord.Question + "', '"
                    + quizRecord.CorrectAnswer + "', '"
                    + quizRecord.Credit + "', '"
                    + quizRecord.Difficulty + "' )";
                dbcmd.ExecuteNonQuery();



                foreach (string option in quizRecord.AnswerSelections)
                {

                    dbcmd.CommandText =
                    "INSERT INTO " + TABLE_NAME2
                    + " ( "
                    + KEY_QuizID + ", "
                    + KEY_Content + ", "
                    + KEY_OptionIndex + " ) "

                    + "VALUES ( '"
                    + quizRecord.QuizID + "', '"
                    + option + "', '"
                    + Array.IndexOf(quizRecord.AnswerSelections, option) + "' )";
                    dbcmd.ExecuteNonQuery();
                }
            }
            catch
            {
                Debug.Log("Duplicate ID!!");
            }
            

        }

        public void StoreQNARecords(List<QuestionsNAnswers> qnas)
        {
            foreach (QuestionsNAnswers qna in qnas)
            {
                addData(qna);
            }
        }

        public List<QuestionsNAnswers> RetrieveQuestionsNAnswers()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME;
            System.Data.IDataReader reader = dbcmd.ExecuteReader();
            List<QuestionsNAnswers> res = new List<QuestionsNAnswers>();
            while (reader.Read())
            {
                IDbCommand dbcmd2 = getDbCommand();
                dbcmd2.CommandText = "SELECT * FROM " + TABLE_NAME2 + " WHERE " + KEY_QuizID + " = '" + reader[0].ToString() + "'";
                System.Data.IDataReader reader2 = dbcmd2.ExecuteReader();
                string[] answerSelection = new string[4];
                while (reader2.Read())
                {
                    answerSelection[Convert.ToInt32(reader2[2])] = reader2[1].ToString();
                }
                res.Add(new QuestionsNAnswers(Convert.ToInt32(reader[0]), reader[1].ToString(), answerSelection,Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]), reader[4].ToString()));
            }
            return res;
        }

    }
}