using System;
using UnityEngine;
using UnityEngine.UI;

public class StockListItem : MonoBehaviour
{
    public Text stockNameText;
    public Button tradeButton;

    int pos;

    Action<int> itemClickListener;

    public void OnTradeButtonClick()
    {
        itemClickListener.Invoke(pos);
    }

    public void SetPos(int pos)
    {
        this.pos = pos;
    }

    public void SetStockName(string name)
    {
        stockNameText.text = name;
    }

    public void SetClickListener(Action<int> listener)
    {
        itemClickListener = listener;
    }
}
