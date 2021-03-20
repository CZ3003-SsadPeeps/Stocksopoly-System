using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public Text questionText, messageText;
    public Button confirmButton, closeButton;
    public Toggle[] optionToggles;

    QuizManager quizManager = new QuizManager();

    void Start()
    {
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
            // TODO: Display error message
            Debug.LogError("No options selected");
            return;
        }

        // Disable option selection
        closeButton.gameObject.SetActive(true);
        confirmButton.enabled = false;
        foreach (Toggle optionToggle in optionToggles)
        {
            optionToggle.enabled = false;
        }

        // Mark correct answer
        optionToggles[quizManager.CorrectAnswer].image.color = Color.green;

        // Verify answer
        if (!quizManager.VerifyAnswer(selectedOption))
        {
            optionToggles[selectedOption].image.color = Color.red;
        }
    }

    public void OnQuitButtonClick()
    {
        // TODO: Unload this scene
    }
}
