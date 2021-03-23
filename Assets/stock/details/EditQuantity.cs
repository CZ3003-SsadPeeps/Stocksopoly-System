using System;
using UnityEngine;
using UnityEngine.UI;


public class EditQuantity : MonoBehaviour
{
    public Text QuantityText;
    public Text StockPriceText;

    public int Quantity { get; private set; };

    Action<int> QuantityChangeListener;

    public void OnQuantityChange(bool isIncrease)
    {
        if (isIncrease)
        {
            Quantity++;
        }
        else
        {
            Quantity--;
        }

        QuantityText.text = Quantity.ToString();
        QuantityChangeListener.Invoke(Quantity);
    }

    public void SetQuantity(int quantity)
    {
        Quantity = quantity;
        QuantityText.text = Quantity.ToString();
    }

    public void SetQuantityChangeListener(Action<int> changeListener)
    {
        QuantityChangeListener = changeListener;
    }
}
