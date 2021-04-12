using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary> 
/// Class of UI for Quiz
/// <br></br>
/// Created by Lau Zhen Jie and Ng Ching Ting
/// </summary>

public class QuizUi : MonoBehaviour
{
    /// <summary>ENUM Color for the quiz button to indicate whether the answer is correct/// </summary>
    static readonly Color32 COLOR_CORRECT = new Color32(99, 160, 118, 255);
    static readonly Color32 COLOR_WRONG = new Color32(218, 85, 86, 255);

    /// <summary>
    /// questionText is the Question's text while messageText is error text when the player press the confirm button without selecting an answer
    /// </summary>
    public Text questionText, messageText;
    
    /// <summary>
    /// confirmButton is the button for the player to press when he/she has confirmed the answer while 
    /// closeButton is the button for the player to press to close the quiz popup window
    /// </summary>
    public Button confirmButton, closeButton;

    /// <summary>
    /// The array of toggles used to store the possible answer selections
    /// </summary>
    public Toggle[] optionToggles;

    readonly QuizManager quizManager = new QuizManager();

    /// <summary>
    /// Load quiz question and answer from database through quizManager based on the dfficulty selected by the player
    /// </summary>
    void Start()
    {
        SceneManager.UnloadSceneAsync("DifficultySelection");

        // Generate question
        QuestionsNAnswers question = quizManager.LoadQuestion();

        // Display question details
        questionText.text = question.Question;
        Toggle optionToggle;
        for (int i = 0; i < optionToggles.Length; i++)
        {
            optionToggle = optionToggles[i];
            optionToggle.GetComponentInChildren<Text>().text = question.AnswerSelections[i];
        }
    }


    /// <summary>
    /// Shows the user whether his/her answer is correct. If correct, the answer button will turn green. If not, the answer selected will turn red and the correct answer will turn green 
    /// </summary>
    public void OnConfirmButtonClick()
    {
        messageText.gameObject.SetActive(true);

        // Check if there are any toggles that are selected
        int selectedOption = -1;
        for (int i = 0; i < optionToggles.Length; i++)
        {
            if (!optionToggles[i].isOn) continue;

            selectedOption = i;
            break;
        }

        if (selectedOption == -1)
        {
            messageText.color = COLOR_WRONG;
            messageText.text = "You must select an option!";
            return;
        }

        // Disable option selection
        closeButton.gameObject.SetActive(true);
        confirmButton.interactable = false;
        foreach (Toggle optionToggle in optionToggles)
        {
            optionToggle.enabled = false;
        }

        // Mark correct answer
        optionToggles[quizManager.CorrectAnswer].image.color = COLOR_CORRECT;

        // Verify answer
        if (!quizManager.VerifyAnswer(selectedOption))
        {
            optionToggles[selectedOption].image.color = COLOR_WRONG;
            messageText.color = COLOR_WRONG;
            messageText.text = "-0C";
        } else
        {
            messageText.color = COLOR_CORRECT;
            messageText.text = $"+{quizManager.GetAmountToCredit()}C";
        }
    }

    /// <summary>
    /// Leave the Quiz UI popup
    /// </summary>
    public void OnQuitButtonClick()
    {
        SceneManager.UnloadSceneAsync("Quiz");
    }
}
