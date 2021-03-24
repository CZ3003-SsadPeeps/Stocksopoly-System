using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelectionUi : MonoBehaviour
{
    public void ShowQuizUi(string difficulty)
    {
        DifficultyStore.Difficulty = difficulty;
        SceneManager.LoadScene("Quiz", LoadSceneMode.Additive);
    }
}
