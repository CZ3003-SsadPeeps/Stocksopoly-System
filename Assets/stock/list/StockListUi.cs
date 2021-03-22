using System;
using System.Collections.Generic;
using UnityEngine;

public class StockListUi : MonoBehaviour
{
    public GameObject stockListItemPrefab;
    Action<int> itemClickListener;

    public Transform content;

    public void SetItemClickListener(Action<int> itemClickListener)
    {
        this.itemClickListener = itemClickListener;
    }

    public void SetList(List<Stock> stocks)
    {
        GameObject stockObject;
        StockListItem stockListItem;
        for (int i = 0; i < stocks.Count; i++)
        {
            stockObject = Instantiate(stockListItemPrefab);
            stockObject.transform.SetParent(content, false);

            stockListItem = stockObject.GetComponent<StockListItem>();
            stockListItem.SetStockName(stocks[i].Name);
            stockListItem.SetClickListener(delegate { itemClickListener.Invoke(i); });
        }
    }
}
