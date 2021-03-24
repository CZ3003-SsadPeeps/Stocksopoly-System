using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NameInputUi : MonoBehaviour
{
    public InputField[] inputFields;
    public Text[] errorMessages;

    NameInputController controller = new NameInputController();

    public void OnStartButtonClick()
    {
        // Dismiss all error messages
        foreach (Text errorMessage in errorMessages)
        {
            errorMessage.gameObject.SetActive(false);
        }

        // Get all names
        string[] nameInputs = new string[inputFields.Length];
        for (int i = 0; i < inputFields.Length; i++)
        {
            nameInputs[i] = inputFields[i].text;
        }

        List<NameValidationError> results = controller.SubmitNames(nameInputs);
        // Check if any errors occur
        if (results.Count > 0)
        {
            DisplayErrors(results);
            return;
        }

        controller.InitializeGame(nameInputs);
        SceneManager.LoadScene("Game");
    }

    void DisplayErrors(List<NameValidationError> errors)
    {
        Text errorMessage;
        int otherPos;
        foreach (NameValidationError result in errors)
        {
            errorMessage = errorMessages[result.Pos];

            if (result is NameValidationError.IsBlank)
            {
                errorMessage.text = "Name cannot be blank!";
                errorMessage.gameObject.SetActive(true);
                continue;
            }

            if (result is NameValidationError.Clash)
            {
                otherPos = (result as NameValidationError.Clash).WithPos + 1;
                errorMessage.text = $"Name cannot be the same as Player {otherPos}'s name";
                errorMessage.gameObject.SetActive(true);
            }
        }
    }
}
