using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Demo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;

        for (int i = 0; i < StockStore.stocks.Length; i++)
        {
            Stock stock = StockStore.stocks[i];
            g = Instantiate(buttonTemplate, transform);
            g.GetComponent<StockRow>().RowPos = i;
            g.transform.GetChild(0).GetComponent<Text>().text = stock.Name;
        }

        Destroy(buttonTemplate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
