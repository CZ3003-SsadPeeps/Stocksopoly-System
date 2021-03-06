using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sets stock price, quantity to be bought by the player in the stock purchase screen
/// ///<br></br>
/// Created by Chia Ze
/// </summary>
public class QuantityEditor : MonoBehaviour
{
    public Text QuantityText;
    public Text StockPriceText;

    public Button increaseButton, decreaseButton;

    public int QuantityChange { get; private set; } = 0;

    int currentQuantity;
    int maxQuantity;

    Action<int> QuantityChangeListener;

    /// <summary>
    /// Changes the quantity of stocks purchased on screen
    /// </summary>
    /// <param name="isIncrease"></param>
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
            increaseButton.interactable = true;
        }

        QuantityText.text = (currentQuantity + QuantityChange).ToString();
        if ((-QuantityChange) >= currentQuantity)
        {
            decreaseButton.interactable = false;
        }

        if (QuantityChange == maxQuantity)
        {
            increaseButton.interactable = false;
        }

        QuantityChangeListener.Invoke(QuantityChange);
    }

    internal void SetStockPrice(int stockPrice, int playerCredit)
    {
        StockPriceText.text = $"@ ${stockPrice} per stock";

        maxQuantity = Mathf.FloorToInt(playerCredit / stockPrice);
        if (maxQuantity == 0)
        {
            increaseButton.interactable = false;
        }
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
