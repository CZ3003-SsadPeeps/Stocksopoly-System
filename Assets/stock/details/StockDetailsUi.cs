using UnityEngine;
using UnityEngine.UI;

public class StockDetailsUi : MonoBehaviour
{
    public Text stockNameText, buyAmountText, sellAmountText, playerDetails;
    public Window_Graph graph;
    public EditQuantity quantityEditor;
    public Button buyButton, sellButton;

    readonly StockDetailsManager manager = new StockDetailsManager();

    // Start is called before the first frame update
    void Start()
    {
        Stock stock = manager.Stock;
        Player player = manager.Player;
        StockPurchaseRecord purchaseRecord = manager.PurchaseRecord;

        stockNameText.text = stock.Name;
        quantityEditor.SetQuantity(purchaseRecord.Quantity);
        quantityEditor.SetStockPrice(stock.CurrentStockPrice);
        playerDetails.text = $"{player.Name} - ${player.Credit}";
        graph.ShowGraph(stock.StockPriceHistory.ToArray());

        quantityEditor.SetQuantityChangeListener(quantity =>
        {
            buyAmountText.text = string.Empty;
            sellAmountText.text = string.Empty;
            buyButton.interactable = false;
            sellButton.interactable = false;

            int amountChanged = quantity * stock.CurrentStockPrice;
            if (amountChanged > 0)
            {
                buyAmountText.text = $"-${amountChanged}";
                buyButton.interactable = true;
            }
            else if (amountChanged < 0)
            {
                sellAmountText.text = $"+${-amountChanged}";
                sellButton.interactable = true;
            }

            manager.QuantityChange = quantity;
        });
    }

    public void OnBackButtonClick()
    {

    }

    public void OnBuySellButtonClick()
    {
        manager.SaveQuantityChange();
        quantityEditor.SetQuantity(manager.PurchaseRecord.Quantity);
    }
}
