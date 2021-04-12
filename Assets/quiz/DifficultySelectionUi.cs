using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Let the player choose the difficulty level of the quiz
/// <br></br>
/// Created by Lau Zhen Jie and Ng Ching Ting
/// </summary>
public class DifficultySelectionUi : MonoBehaviour
{
    public void ShowQuizUi(string difficulty)
    {
        DifficultyStore.Difficulty = difficulty;
        SceneManager.LoadScene("Quiz", LoadSceneMode.Additive);
    }
}
