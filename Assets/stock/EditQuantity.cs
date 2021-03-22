using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EditQuantity : MonoBehaviour
{
    public Text StockNameText;
    public Text StockPriceText;  
    public Text QuantityText;  
    public Text AmountText;


    public int quantity=0;
    public double amount=0;


   // Stocks tesla = new Stocks("Tesla" , 500);
    Stock tesla = StockStore.SelectedStock;

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        StockNameText.text = tesla.Name.ToString();
        StockPriceText.text = tesla.CurrentStockPrice.ToString();
        QuantityText.text = quantity.ToString();
        AmountText.text = amount.ToString();
    }

    public void addQuantity()
    {
        quantity++;
        amount += tesla.CurrentStockPrice;
    }

    public void minusQuantity()
    {
        if (quantity > 0) 
        { 
            quantity--;
            amount -= tesla.CurrentStockPrice;
        }
    }

}
