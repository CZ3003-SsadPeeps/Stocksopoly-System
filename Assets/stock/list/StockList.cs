using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Stores the stocks in a list
/// ///<br></br>
/// Created by Khairuddin Bin Ali
/// </summary>
public class StockList : MonoBehaviour
{
    public GameObject stockListItemPrefab;
    Action<int> itemClickListener;

    public Transform content;

    internal void SetItemClickListener(Action<int> itemClickListener)
    {
        this.itemClickListener = itemClickListener;
    }

    internal void SetList(List<Stock> stocks)
    {
        GameObject stockObject;
        StockListItem stockListItem;
        for (int i = 0; i < stocks.Count; i++)
        {
            stockObject = Instantiate(stockListItemPrefab);
            stockObject.transform.SetParent(content, false);

            stockListItem = stockObject.GetComponent<StockListItem>();
            stockListItem.SetPos(i);
            stockListItem.SetStockName(stocks[i].Name);
            stockListItem.SetClickListener(itemClickListener);
        }
    }
}
