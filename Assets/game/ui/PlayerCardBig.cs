using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>Concrete class which inherits PlayerCard. Contains the properties and methods of the big player card displayed in the middle of the main user interface
///<br></br>
///Done by: Khairuddin and Yi Shen
///</summary>
public class PlayerCardBig : PlayerCard
{
    ///<summary>References the scrollable area of the big player card</summary>
    public ScrollRect scrollView;

    ///<summary>References the text displayed in the scrollable area</summary>
    public GameObject TextPrefab;

    ///<summary>References the transform of the scrollable area</summary>
    public Transform content;

    ///<summary>Defines the text color of the player card</summary>
    Color textColor = Color.white;

    ///<summary>Method that sets the visibility of the player card</summary>
    /// <param name="isVisible">Setting this as true will make the player card visible</param>
    internal void SetVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }

    ///<summary>Method that sets the text color of the player card to be black or white</summary>
    /// <param name="isWhite">Setting this as true will make the text color white, setting this to false will make text color black</param>
    internal void SetTextColor(bool isWhite)
    {
        textColor = isWhite ? Color.white : Color.black;
        nameText.color = textColor;
        creditText.color = textColor;
    }

    ///<summary>Method that replaces the stock information in the scrollable area of the player card</summary>
    /// <param name="stocks">A list of PlayerStock objects whose properties will be displayed in the scrollable area of the player card</param>
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

    ///<summary>Method that creates the text objects that will be placed in the scrollable area of the player card</summary>
    Text CreateNewText()
    {
        GameObject textObject = Instantiate(TextPrefab);
        textObject.transform.SetParent(content, false);

        Text text = textObject.GetComponent<Text>();
        text.color = textColor;
        return text;
    }
}
