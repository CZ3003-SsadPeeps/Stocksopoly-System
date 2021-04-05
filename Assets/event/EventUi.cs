using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script displays a random event and adds or deducts the player's credits according to the event shown.
/// <br/><br/>
/// Created by Lau Zhen Jie and Ng Ching Ting
/// </summary>
public class EventUi : MonoBehaviour
{
    /// <summary>
    /// The text field where the event content and credits gained/lost will be displayed.
    /// </summary>
    public Text contentText, creditText;

    /// <summary>
    /// Fetch the RectTransform from the GameObject.
    /// </summary>
    public RectTransform PosTransform
    {
        get { return GetComponent<RectTransform>(); }
    }

    readonly EventManager manager = new EventManager();

    /// <summary>
    /// Retrieve event from the manager and display a random event and adds or deducts the player's credits according to the event shown.
    /// If the player receives credits, the text showing the player the amount of credits earned would be in green.
    /// Else, the player's amount of credits to be deducted will be shown. 
    /// </summary>
    public void LoadNewEvent()
    {
        EventRecord eventRecord = manager.GetEvent();
        contentText.text = eventRecord.Content;
        int creditAmount = eventRecord.Amount;
        if (creditAmount >= 0)
        {
            creditText.text = $"+${creditAmount}";
            creditText.color = Color.green;
        } else
        {
            creditText.text = $"-${Mathf.Abs(creditAmount)}";
            creditText.color = Color.red;
        }
    }
}
