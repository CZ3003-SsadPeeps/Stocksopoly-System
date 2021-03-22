using UnityEngine;
using UnityEngine.UI;

public class StockListItem : MonoBehaviour
{
    public Text stockNameText;
    public Button tradeButton;

    public void SetStockName(string name)
    {
        stockNameText.text = name;
    }

    public void SetClickListener(Button.ButtonClickedEvent clickEvent)
    {
        tradeButton.onClick = clickEvent;
    }
}
