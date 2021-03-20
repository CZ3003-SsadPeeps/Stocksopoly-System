using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelectScript : MonoBehaviour
{
    static public string difficultySelectionText;
    

    public void selectDifficulty()
    {
        switch (this.gameObject.name)
        {
            case "NoviceButton":
                Difficulty.difficulty = "Novice";           
                break;
            case "IntermediateButton":
                Difficulty.difficulty = "Intermediate";
                break;
            case "ExpertButton":
                Difficulty.difficulty = "Expert";
                break;
        }
        SceneManager.LoadScene("QuizScene");
    }
}
