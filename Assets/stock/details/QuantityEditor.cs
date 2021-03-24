using System;
using UnityEngine;
using UnityEngine.UI;

public class QuantityEditor : MonoBehaviour
{
    public Text QuantityText;
    public Text StockPriceText;

    public Button decreaseButton;

    int currentQuantity;
    public int QuantityChange { get; private set; } = 0;

    Action<int> QuantityChangeListener;

    public void OnQuantityChange(bool isIncrease)
    {
        if (isIncrease)
        {
            QuantityChange++;
            decreaseButton.interactable = true;
        }
        else
        {
            QuantityChange--;
        }

        QuantityText.text = (currentQuantity + QuantityChange).ToString();
        if ((-QuantityChange) >= currentQuantity)
        {
            decreaseButton.interactable = false;
        }

        QuantityChangeListener.Invoke(QuantityChange);
    }

    internal void SetStockPrice(int stockPrice)
    {
        StockPriceText.text = $"@ ${stockPrice} per stock";
    }

    internal void SetQuantityChangeListener(Action<int> changeListener)
    {
        QuantityChangeListener = changeListener;
    }

    internal void SetQuantity(int currentQuantity)
    {
        this.currentQuantity = currentQuantity;
        QuantityChange = 0;

        QuantityText.text = currentQuantity.ToString();
        if (currentQuantity == 0)
        {
            decreaseButton.interactable = false;
        }
    }
}
