using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizMgr;
    public Button button;
    public GameObject quitButton;


    public void verifyAnswer()
    {
        button.GetComponent<Button>().onClick.AddListener(Answer);
    }

    public void Answer()
    {
        Toggle tg = this.GetComponent<Toggle>();
        ColorBlock cb = tg.colors;
        Vector3 pos = quitButton.transform.position;
        pos.x -= 500f;
        quitButton.transform.position = pos;

        if (this.GetComponent<Toggle>().isOn == true)
        {
            if (isCorrect)
            {
                Debug.Log("Correct Answer");
                cb.normalColor = Color.green;
                tg.colors = cb;
                quizMgr.correct();

            }
            else
            {
                Debug.Log("Wrong Answer");
                cb.normalColor = Color.red;
                tg.colors = cb;
                wrong();
            }
        }
        Destroy(button.gameObject);
        quitButton.GetComponent<Button>().onClick.AddListener(quizMgr.quitQuiz);

    }

    public void showCorrectAsnwer(GameObject Gobj)
    {
        Toggle tg = Gobj.GetComponentInParent<Toggle>();
        ColorBlock cb = tg.colors;
        cb.normalColor = Color.green;
        tg.colors = cb;
    }
    public void wrong()
    {

        for (int i = 0; i < quizMgr.options.Length; i++)
        {
            if (quizMgr.options[i].GetComponent<AnswerScript>().isCorrect == true)
            {
                showCorrectAsnwer(quizMgr.options[i]);
            }
        }
    }
}
