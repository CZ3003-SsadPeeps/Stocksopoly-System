using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Database;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsNAnswers> qna;
    public GameObject[] options;
    public int currentQuestion;
    public AnswerScript AScript=null;
    public GameObject quitButton;
    //public Player player;
    public QuestionsNAnswersDAO QnADAO;
    public Text QuestionTxt;
    public Text title;
   

    private void Start()
    {
        title.text = Difficulty.difficulty;
        Debug.Log(Difficulty.difficulty);
        this.QnADAO = new QuestionsNAnswersDAO();
        this.qna = QnADAO.RetrieveQuestionsNAnswers();
        /*string[] answer1 = { "e", "f", "g", "h" };
        QuestionsNAnswers q2 = new QuestionsNAnswers(2, "intermediate question", answer1, 3, (float)0.50, "Intermediate");
        QnADAO.addData(q2);
        string[] answer2 = { "M", "L", "K", "J" };
        QuestionsNAnswers q3 = new QuestionsNAnswers(3, "very hard question", answer2, 4, (float)0.50, "Expert");
        QnADAO.addData(q3);*/

        generateQuestion(Difficulty.difficulty);
        Vector3 pos = quitButton.transform.position;
        pos.x += 500f;
        quitButton.transform.position = pos;
    }

    public void correct()
    {
        // need to have access to the player's method to increase the credit.
        GameStore.CurrentPlayer.AddCredit(qna[currentQuestion].Credit);
    }

    void SetAnswer()
    {
        for(int i=0; i<options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(1).GetComponentInChildren<Text>().text = qna[currentQuestion].AnswerSelections[i];
            if (qna[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }

        }
    }

    void generateQuestion(string difficulty)
    {
        currentQuestion = Random.Range(0, qna.Count);
        while (qna[currentQuestion].Difficulty != difficulty)
        {
            currentQuestion = Random.Range(0, qna.Count);
        }
        QuestionTxt.text = qna[currentQuestion].Question;
        SetAnswer();
        qna.RemoveAt(currentQuestion);
    }

    void generateQuestion()
    {
        currentQuestion = Random.Range(0, qna.Count);
        
        QuestionTxt.text = qna[currentQuestion].Question;
        SetAnswer();
        qna.RemoveAt(currentQuestion);
    }

    public void quitQuiz()
    {
        Debug.Log("quit quiz");
        //destroying all game objects
        /*foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
        {
            Destroy(o);
        }*/
    }
}
