﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCardBig : PlayerCard
{
    public ScrollRect scrollView;
    public GameObject TextPrefab;
    public Transform content;

    internal void SetVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }

    internal void SetStockDetails(List<PlayerStock> stocks)
    {
        //Remove existing stock info
        int numChildren = content.childCount;
        for (int i = 0; i < numChildren; i++)
        {
            GameObject.Destroy(content.GetChild(i).gameObject);
        }

        GameObject textObject;
        Text headerText;

        // Headers
        textObject = Instantiate(TextPrefab);
        textObject.transform.SetParent(content, false);
        headerText = textObject.GetComponent<Text>();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "Company";

        textObject = Instantiate(TextPrefab);
        textObject.transform.SetParent(content, false);
        headerText = textObject.GetComponent<Text>();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "Quantity";

        textObject = Instantiate(TextPrefab);
        textObject.transform.SetParent(content, false);
        headerText = textObject.GetComponent<Text>();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "Average Purchase Price";

        textObject = Instantiate(TextPrefab);
        textObject.transform.SetParent(content, false);
        headerText = textObject.GetComponent<Text>();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "Current Stock Price";

        // Stock info
        foreach (PlayerStock stock in stocks)
        {
            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(content, false);
            textObject.GetComponent<Text>().text = stock.CompanyName;

            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(content, false);
            textObject.GetComponent<Text>().text = stock.Quantity.ToString();

            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(content, false);
            textObject.GetComponent<Text>().text = $"${stock.AvgPurchasePrice}";

            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(content, false);
            textObject.GetComponent<Text>().text = $"${stock.CurrentStockPrice}";
        }

        // Scroll layout by default scrolls to the middle of the list, so must scroll back to top
        scrollView.normalizedPosition = new Vector2(0, 1);
    }
}
