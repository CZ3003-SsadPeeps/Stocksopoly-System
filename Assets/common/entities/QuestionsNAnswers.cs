
[System.Serializable]
public class QuestionsNAnswers
{
    public int QuizID;
    public string Question;
    public string[] AnswerSelections;
    public int CorrectAnswer;
    public int Credit;
    public string Difficulty;

    public QuestionsNAnswers(int QuizID,string Question,string[] AnswerSelections,int CorrectAnswer,int Credit,string Difficulty)
    {
        this.QuizID = QuizID;
        this.Question = Question;
        this.AnswerSelections = AnswerSelections;
        this.CorrectAnswer = CorrectAnswer;
        this.Credit = Credit;
        this.Difficulty = Difficulty;
    }

    /*public void creditChange(Player player)
    {
        player.AddCredit(Credit);
    }*/
}
