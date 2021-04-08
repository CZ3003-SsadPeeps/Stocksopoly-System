using System.Collections.Generic;
using UnityEngine;
using Database;

/// <summary>
/// Manage the quiz question and answer between the Quiz UI and Database
/// <br></br>
/// Created by Lau Zhen Jie and Ng Ching Ting
/// </summary>
public class QuizManager
{
    QuestionsNAnswers question;

    /// <summary>
    /// The int index for the correct answer
    /// </summary>
    public int CorrectAnswer
    {
        get { return question.CorrectAnswer; }
    }

    /// <summary>
    /// Retrieved the questions and answer from the database
    /// </summary>
    /// <returns> the list of questions is returned </returns>
    public QuestionsNAnswers LoadQuestion()
    {
        QuestionsNAnswersDAO dao = new QuestionsNAnswersDAO();
        List<QuestionsNAnswers> questions = dao.RetrieveQuestions(DifficultyStore.Difficulty);
        question = questions[Random.Range(0, questions.Count)];

        return question;
    }


    /// <summary>
    /// Verify whether the answer selected by the player is correct
    /// </summary>
    /// <param name="selectedOption"></param>
    /// <returns> The correctness of answer is returned </returns>
    public bool VerifyAnswer(int selectedOption)
    {
        if (selectedOption != question.CorrectAnswer)
        {
            return false;
        }

        GameStore.CurrentPlayer.AddCredit(question.Credit);
        return true;
    }

    /// <summary>
    /// get the correct answer based on the index
    /// </summary>
    /// <returns> index location of the correct answer is returned</returns>
    public int GetCorrectAnswer() { return question.CorrectAnswer; }

    internal int GetAmountToCredit() { return question.Credit; }
}
