using UnityEngine;
using UnityEngine.UI;

public class EventUi : MonoBehaviour
{
    public Text contentText, creditText;

    EventManager manager = new EventManager();

    void Start()
    {
        EventRecord eventRecord = manager.GetEvent();
        contentText.text = eventRecord.Content;
        int creditAmount = eventRecord.Amount;
        if (creditAmount >= 0)
        {
            creditText.text = $"+${creditAmount}";
        } else
        {
            creditText.text = $"-${Mathf.Abs(creditAmount)}";
        }
    }

    public void OnConfirmButtonClick()
    {

    }
}
