using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCardBig : PlayerCard
{
    public ScrollRect scrollView;
    public GameObject TextPrefab;
    public Transform content;

    Color textColor = Color.white;

    internal void SetVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }

    internal void SetTextColor(bool isWhite)
    {
        textColor = isWhite ? Color.white : Color.black;
    }

    internal void SetStockDetails(List<PlayerStock> stocks)
    {
        //Remove existing stock info
        int numChildren = content.childCount;
        for (int i = 0; i < numChildren; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }

        Text headerText;

        // Headers
        headerText = CreateNewText();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "Company";

        headerText = CreateNewText();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "Quantity";

        headerText = CreateNewText();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "Average Purchase Price";

        headerText = CreateNewText();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "Current Stock Price";

        // Stock info
        foreach (PlayerStock stock in stocks)
        {
            CreateNewText().text = stock.CompanyName;
            CreateNewText().text = stock.Quantity.ToString();
            CreateNewText().text = $"{stock.AvgPurchasePrice}C";
            CreateNewText().text = $"{stock.CurrentStockPrice}C";
        }

        // Scroll layout by default scrolls to the middle of the list, so must scroll back to top
        scrollView.normalizedPosition = new Vector2(0, 1);
    }

    Text CreateNewText()
    {
        GameObject textObject = Instantiate(TextPrefab);
        textObject.transform.SetParent(content, false);

        Text text = textObject.GetComponent<Text>();
        text.color = textColor;
        return text;
    }
}
