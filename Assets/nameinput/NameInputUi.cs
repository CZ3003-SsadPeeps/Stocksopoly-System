using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// This is the user interface that provides forms for the players to enter their names.
/// <br/><br/>
/// Created by Khairuddin Bin Ali
/// </summary>
public class NameInputUi : MonoBehaviour
{
    /// <summary>
    /// The list of text fields where the players will enter their names. Each <c>InputField</c> objects
    /// represents a form for each user.
    /// </summary>
    public InputField[] inputFields;

    /// <summary>
    /// The text widgets that displays an error message for each form. All the <c>Text</c> objects are
    /// hidden by default and are displayed only when a validation errors occurs on a form.
    /// </summary>
    public Text[] errorMessages;

    private readonly NameInputController controller = new NameInputController();

    /// <summary>
    /// Submits the names entered by the players to the controller for validation. If all names passed
    /// validation, <c>GameUi</c> is launched. Otherwise an error message is displayed for the form(s)
    /// that failed the validation.
    /// </summary>
    public void SubmitNames()
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

    private void DisplayErrors(List<NameValidationError> errors)
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
