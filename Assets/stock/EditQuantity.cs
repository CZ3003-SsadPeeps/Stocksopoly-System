using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EditQuantity : MonoBehaviour
{

    public class Stocks
    {
        public string Name;
        public double Price;
        public Stocks(string name, double price)
        {
            Name = name;
            Price = price;
        }
        // Other properties, methods, events...
    }

    public Text StockNameText;
    public Text StockPriceText;  
    public Text QuantityText;  
    public Text AmountText;


    public int quantity=0;
    public double amount=0;


   // Stocks tesla = new Stocks("Tesla" , 500);
    Stocks tesla = new Stocks(StockStore.SelectedStock.Name,StockStore.SelectedStock.StockPriceHistory[9]);

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        StockNameText.text = tesla.Name.ToString();
        StockPriceText.text = tesla.Price.ToString();
        QuantityText.text = quantity.ToString();
        AmountText.text = amount.ToString();
    }

    public void addQuantity()
    {
        quantity++;
        amount += tesla.Price;
    }

    public void minusQuantity()
    {
        if (quantity > 0) 
        { 
            quantity--;
            amount -= tesla.Price;
        }
    }

}
