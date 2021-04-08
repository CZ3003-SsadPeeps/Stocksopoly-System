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
        static readonly int NUM_OPTIONS = 4;

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

        public List<QuestionsNAnswers> RetrieveQuestions(string difficulty)
        {
            // Execute SQL statement to retrieve questions of a certain difficulty
            IDbCommand selectQuestionsCommand = getDbCommand();
            selectQuestionsCommand.CommandText = $"SELECT * FROM {TABLE_NAME} WHERE {KEY_Difficulty} = '{difficulty}'";
            IDataReader questionReader = selectQuestionsCommand.ExecuteReader();

            List<QuestionsNAnswers> res = new List<QuestionsNAnswers>();
            IDbCommand selectOptionsCommand;
            IDataReader optionReader;
            while (questionReader.Read())
            {
                // Get options for the question
                int quizID = questionReader.GetInt32(0);

                // Execute SQL statement to retrieve optios of a question
                selectOptionsCommand = getDbCommand();
                selectOptionsCommand.CommandText = $"SELECT {KEY_OptionIndex}, {KEY_Content} FROM {TABLE_NAME2} WHERE {KEY_QuizID} = {quizID} LIMIT {NUM_OPTIONS}";
                optionReader = selectOptionsCommand.ExecuteReader();

                string[] answerSelection = new string[NUM_OPTIONS];
                while (optionReader.Read())
                {
                    answerSelection[optionReader.GetInt32(0)] = optionReader.GetString(1);
                }
                res.Add(new QuestionsNAnswers(quizID, questionReader.GetString(1), answerSelection, questionReader.GetInt32(2), questionReader.GetInt32(3), questionReader.GetString(4)));

                optionReader.Close();
            }

            questionReader.Close();
            return res;
        }
    }
}