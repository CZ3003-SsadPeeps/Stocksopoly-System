using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>Concrete class which inherits PlayerCard. Contains the properties and methods of the big player card displayed in the middle of the main user interface
///<br></br>
///Done by: Yi Shen
///</summary>
public class PlayerCardBig : PlayerCard
{
    ///<summary>References the scrollable area of the big player card</summary>
    public ScrollRect scrollView;

    ///<summary>References the text displayed in the scrollable area</summary>
    public GameObject TextPrefab;

    ///<summary>References the transform of the scrollable area</summary>
    public Transform content;

    private Color textColor = Color.white;

    internal void SetVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }

    internal void SetTextColor(bool isWhite)
    {
        textColor = isWhite ? Color.white : Color.black;
        nameText.color = textColor;
        creditText.color = textColor;
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
        headerText.text = "Company";

        headerText = CreateNewText();
        headerText.text = "Quantity";

        headerText = CreateNewText();
        headerText.text = "Avg Purchase Price";

        headerText = CreateNewText();
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

    private Text CreateNewText()
    {
        GameObject textObject = Instantiate(TextPrefab);
        textObject.transform.SetParent(content, false);

        Text text = textObject.GetComponent<Text>();
        text.color = textColor;
        return text;
    }
}
