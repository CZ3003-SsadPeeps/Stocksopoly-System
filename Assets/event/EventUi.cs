using UnityEngine;
using UnityEngine.UI;

public class EventUi : MonoBehaviour
{
    public Text contentText, creditText;

    public RectTransform PosTransform
    {
        get { return GetComponent<RectTransform>(); }
    }

    readonly EventManager manager = new EventManager();

    public void LoadNewEvent()
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
}
