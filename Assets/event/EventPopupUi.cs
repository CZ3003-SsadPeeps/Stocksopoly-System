using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventPopupUi : MonoBehaviour
{
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

    public void OnConfirmButtonClick()
    {
        SceneManager.UnloadSceneAsync("EventPopup");
    }
}
