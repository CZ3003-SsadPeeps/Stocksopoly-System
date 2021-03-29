using System.Collections.Generic;
using UnityEngine;
using Database;

public class QuizManager
{
    QuestionsNAnswers question;
    public int CorrectAnswer
    {
        get { return question.CorrectAnswer; }
    }

    public QuestionsNAnswers LoadQuestion()
    {
        QuestionsNAnswersDAO dao = new QuestionsNAnswersDAO();
        List<QuestionsNAnswers> questions = dao.RetrieveQuestions(DifficultyStore.Difficulty);
        question = questions[Random.Range(0, questions.Count)];

        return question;
    }

    public bool VerifyAnswer(int selectedOption)
    {
        if (selectedOption != question.CorrectAnswer)
        {
            return false;
        }

        GameStore.CurrentPlayer.AddCredit(question.Credit);
        return true;
    }

    public int GetCorrectAnswer() { return question.CorrectAnswer; }

    internal int GetAmountToCredit() { return question.Credit; }
}
