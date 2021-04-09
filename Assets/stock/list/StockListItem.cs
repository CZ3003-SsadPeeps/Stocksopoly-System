using System;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Initializes each stock into the correct position in the list
/// ///<br></br>
/// Created by Khairuddin Bin Ali
/// </summary>
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

    internal void SetPos(int pos)
    {
        this.pos = pos;
    }

    internal void SetStockName(string name)
    {
        stockNameText.text = name;
    }

    internal void SetClickListener(Action<int> listener)
    {
        itemClickListener = listener;
    }
}
