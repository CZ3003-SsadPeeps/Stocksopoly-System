using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsManager : MonoBehaviour
{   
    public RectTransform NewsDisplayed;
    public string newscontent;
    public RectTransform BackGround;
    static readonly System.Random random = new System.Random();
    public int FlucRate;
    public string CompanyName;
   
    void Awake()
    {
        // to get the randomized news obj from NewsStore and pass to DisplayNews
        BackGround = transform.Find("BackGround").GetComponent<RectTransform>();
        NewsDisplayed = BackGround.Find("NewsDisplayed").GetComponent<RectTransform>();
        int i = Random.Range(0, 15);
        News news = NewsStore.news[i];
        FlucRate = news.FluctuationRate;
        CompanyName = news.CompanyName;


        //displays the news using the function below
        DisplayNews(news.CompanyName,news.Content);

        //need to find out how to only call change stock when end of all 4 player turns
        // ChangeStock(CompanyName,FlucRate);
    }





    //code to display news
    public void DisplayNews(string CName, string CContent)
    {
        RectTransform Newsdisplayed = Instantiate(NewsDisplayed);
        Newsdisplayed.gameObject.SetActive(true);
        Newsdisplayed.transform.SetParent(BackGround.transform, false);
        Newsdisplayed.anchoredPosition = new Vector2(109f, 0f);
        Newsdisplayed.GetComponent<Text>().text = CName + CContent;
    }



    // function to change the stock affected by fluc rate, while randomizing the chanage for other stocks
    public void ChangeStock(string Cname, int FlucRate)
    {
        System.Random random = new System.Random();
        for (int i=0; i < StockStore.stocks.Length; i++)
        {
            Stock stock = StockStore.stocks[i];
            int lastprice = stock.StockPriceHistory[9];
            if(stock.Name == Cname)
            {   

                // save a new stock queue, dequeue and then enqueue the newest price
                var StockQueue = new Queue<int>(stock.StockPriceHistory);
                StockQueue.Dequeue();
                StockQueue.Enqueue(lastprice * (FlucRate + 100) / 100);
                var StockList = new List<int>(StockQueue);
                stock.StockPriceHistory = StockList;

            }

            else
            {
                var StockQueue = new Queue<int>(stock.StockPriceHistory);
                StockQueue.Dequeue();
                StockQueue.Enqueue(random.Next(lastprice-10,lastprice+10));
                var StockList = new List<int>(StockQueue);
                stock.StockPriceHistory = StockList;
            }


        }

    }

    
}
