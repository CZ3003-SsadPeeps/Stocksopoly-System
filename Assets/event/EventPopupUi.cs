using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script retrieves a random event and adds or deducts the player's credits according to the event shown.
/// <br/><br/>
/// Created by Lau Zhen Jie and Ng Ching Ting
/// </summary>
public class EventPopupUi : MonoBehaviour
{
    /// <summary>
    /// Retrieving of the random event content and amount of credits to add or deduct. 
    /// </summary>
    public Text contentText, creditText;

    readonly EventManager manager = new EventManager();

    void Start()
    {
        EventRecord eventRecord = manager.GetEvent();
        contentText.text = eventRecord.Content;
        int creditAmount = eventRecord.Amount;
        if (creditAmount >= 0)
        {
            creditText.text = $"+{creditAmount}C";
            creditText.color = Color.green;
        }
        else
        {
            creditText.text = $"{creditAmount}C";
            creditText.color = Color.red;
        }
    }

    /// <summary>
    /// Close the event popup and continue playing with the game. 
    /// </summary>
    public void OnConfirmButtonClick()
    {
        SceneManager.UnloadSceneAsync("EventPopup");
    }
}
