using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelectScript : MonoBehaviour
{
    public void OnNoviceButtonClick()
    {
        ShowQuizUi("Novice");
    }
    
    public void OnIntermediateButtonClick()
    {
        ShowQuizUi("Intermediate");
    }
    
    public void OnExpertButtonClick()
    {
        ShowQuizUi("Expert");
    }

    void ShowQuizUi(string difficulty)
    {
        Difficulty.difficulty = difficulty;
        SceneManager.LoadScene("QuizScene");
    }
}
