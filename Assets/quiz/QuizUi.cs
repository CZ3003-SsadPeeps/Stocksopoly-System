using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizUi : MonoBehaviour
{

    static readonly Color32 COLOR_CORRECT = new Color32(99, 160, 118, 255);
    static readonly Color32 COLOR_WRONG = new Color32(218, 85, 86, 255);

    public Text questionText, messageText;
    public Button confirmButton, closeButton;
    public Toggle[] optionToggles;

    readonly QuizManager quizManager = new QuizManager();

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

    public void OnQuitButtonClick()
    {
        SceneManager.UnloadSceneAsync("Quiz");
    }
}
