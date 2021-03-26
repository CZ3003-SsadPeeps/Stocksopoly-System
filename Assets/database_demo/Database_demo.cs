using Database;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Database_demo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //store stocks
        StockDAO sdao = new StockDAO();
        Stock[] stocklist = { new Stock("Tesla", 45), new Stock("NIO Inc", 23), new Stock("AMC", 15), new Stock("GameStop", 37) };
        sdao.StoreStock(stocklist);

        //store news
        List<News> newslist = new List<News>() { new News("Tesla", "A brand new car is expected to launch in one month", 5),new News("Tesla", "Car sales have increased by 40% in the last quarterly report", 10),new News("Tesla", "A big investor had just divest from Tesla", -5),new News("Tesla", "A Tesla car unexpectedly catches on fire", -10),new News("NIO Inc", "A brand new car is expected to launch in one month", 5),new News("NIO Inc", "Car sales have increased by 40% in the last quarterly report", 10),new News("NIO Inc", "A big investor had just divest from NIO", -5),new News("NIO Inc", "An NIO car unexpectedly catches on fire", -10),new News("AMC", "Cinemas are finally reopening", 5),new News("AMC", "Food revenue have increased by 10%", 10),new News("AMC", "AMC is getting shorted by hedge funds", -5),new News("AMC", "A cinema-goer has caught COVID-19 at an AMC cinema", -10),new News("GameStop", "Reddit users wreck havoc in the stock market, causing Gamestop's stock price to skyrocket", 200),new News("GameStop", "Game sales have increased by 53% in the last year", 10),new News("GameStop", "1000 Gamestop stores in America have been closed", -10),new News("GameStop", "Trading app Robinhood is silently selling users' Gamestop stocks", -50),
        };
        NewsRecordDAO nrdao = new NewsRecordDAO();
        nrdao.StoreNewsRecords(newslist);

        //store qna
        QuestionsNAnswersDAO qnadao = new QuestionsNAnswersDAO();
        //List<QuestionsNAnswers> qnalist = new List<QuestionsNAnswers>() {
        //    new QuestionsNAnswers(1, "What is the stock market?",
        //    new string[]{ "A type of farmers market where people buy and sell food."
        //    , "A place where parts of businesses are bought and sold."
        //    , "A special type of grocery store that sells stocks."
        //    ,"A type of bank that gives out loans to new businesses." }
        //    ,1,50,"Novice"),
        //new QuestionsNAnswers(2, "The name for a part of a business that is bought and sold on the stock market is:",
        //    new string[]{ "Part"
        //    , "Marker"
        //    , "Stocker"
        //    ,"Share" }
        //    ,4,50,"Novice"),
        //new QuestionsNAnswers(3, "Why would a company need to issue stock?",
        //    new string[]{ "To show customers that it's successful."
        //    , "To increase its' customer base."
        //    , "To raise money."
        //    ,"To stop the government from regulating it." }
        //    ,2,75,"Intermediate"),
        //new QuestionsNAnswers(4, "When you own stock in a company,",
        //    new string[]{ "you are part owner."
        //    , "you are involved in day to day management."
        //    , "you are entitled to a dividend."
        //    ,"you are the CEO." }
        //    ,0,75,"Intermediate"),
        //new QuestionsNAnswers(5, "IPO stands for",
        //    new string[]{ "Itemized Public Organization"
        //    , "Initial Primary Offering"
        //    , "Initial Public Offering"
        //    ,"Imminent Profitable Option" }
        //    ,2,110,"Expert")};
        List<QuestionsNAnswers> qnalist = new List<QuestionsNAnswers>() {
            new QuestionsNAnswers(1, "What is a stock market? ",
            new string[]{ "A market where people bet on race horses to gain some money. "
            , "A market where people buy products which the merchants have a lot of stocks. "
            , "A place where people can buy stocks which are shares of companies. "
            ,"A place where people can trade items to earn money. " }
            ,2,50,"Novice"),
        new QuestionsNAnswers(2, "What do the bear and the bull stand for in stocks trading? ",
            new string[]{ "Bear means stock are falling while bull means stocks are going up. "
            , "Bear means stocks market are closing while bull means stock market are opening. "
            , "Bear means stocks are rising while bull means stock are falling. "
            ,"Bear means stocks market are closing while bull means stock market are opening. " }
            ,0,50,"Novice"),
            new QuestionsNAnswers(3, "How many types of stocks are there? ",
            new string[]{ "3 "
            , "4 "
            , "5"
            ,"8" }
            ,1,75,"Intermediate"),
            new QuestionsNAnswers(4, "What is meant by book value: ",
            new string[]{ "The value of the company excluding its tangible assets "
            , "The value of the company as done by the external appraiser "
            , "The stock value of the company. "
            ,"Theoretical value of company if all assets are liquidated or sold at prices shown on balance sheet. " }
            ,3,75,"Intermediate"),
            new QuestionsNAnswers(5, "Which statements about blue chip stocks is true? ",
            new string[]{ "Earnings are used for reinvestment in order to maintain growing trend of stock "
            , "No dividends "
            , "Consistently profitable with a dividend payment "
            ,"Traded below market price " }
            ,2,110,"Expert"),
            new QuestionsNAnswers(6, "Which of the following about illiquid market is true: ",
            new string[]{ "Also called thick market "
            , "Lack of buyer and seller "
            , "Lack of stock traded. "
            ,"Lack of alternative investment venues " }
            ,1,110,"Expert"),};
        qnadao.StoreQNARecords(qnalist);

        //store events
        EventRecordDAO erdao = new EventRecordDAO();
        List<EventRecord> eventlist = new List<EventRecord>() {
            //new EventRecord(1,"Receive 50 credits consultancy fee. ",50),
            //new EventRecord(2,"Hospital Fees. Pay 50 credits.",-50),
            //new EventRecord(3,"Life insurance matures – Collect 100 credits",100),
            //new EventRecord(4,"You inherit 200 credits. ",200),
            //new EventRecord(5,"School fees. Pay 100 credits.",-100),
            new EventRecord(1,"Yay! You strike a lottery! ",200),
            new EventRecord(2,"Oops! You are involved in an accident! ",-50),
            new EventRecord(3,"Wow! You are so hardworking! Here is some rewards for you. ",50),
            new EventRecord(4,"All your properties are destroy due to tornado disaster ",-200 ),
            new EventRecord(5,"Congrats! You earned a house property! ",100),
            new EventRecord(6,"Your company went bankrupt and you lost your job ",-150),
            new EventRecord(7,"Your children are preparing for marriage and need some money from you ",-75),
            new EventRecord(8,"You are promoted as the manager! ",150),
            new EventRecord(9,"Your company are giving profit sharing for all workers ",75),
            new EventRecord(10,"Your house is accidentally burned down when your children play with fire cracker. ",-175),
            new EventRecord(11,"You are tested positive for covid : (  ",-185),
            new EventRecord(12,"You won a lucky draw to London during a promotion! ",175),
        };
        erdao.StoreEventRecords(eventlist);

        //store players record
        PlayerRecordDAO prdao = new PlayerRecordDAO();
        for (int i = 0; i < 5; i++)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);//from 1970/1/1 00:00:00 to now
            DateTime dtNow = new DateTime(2021, 3, 1+i);
            TimeSpan result = dtNow.Subtract(dt);
            long seconds = (long)(result.TotalSeconds);
            prdao.addData(new PlayerRecord("user" + i, 100 * (i+1), seconds));
        }


        //to convert total seconds to date string
        //var timeSpan = TimeSpan.FromSeconds(seconds);
        //string strFromEpoch = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).Add(timeSpan).ToString("yyyy-MM-dd HH:mm:ss");
        //Debug.Log(strFromEpoch);

        //List<Stock> newslist2 = nrdao.RetrieveNewsRecords();
        //string newsstring = "";
        //foreach (News newss in newslist2)
        //{
        //    newsstring += string.Format("new News(\"{0}\", \"{1}\", {2}),", newss.CompanyName, newss.Content, newss.FluctuationRate);
        //    //newsstring +=$"new News('{newss.CompanyName}','{newss.Content}',{newss.FluctuationRate}),";
        //}
        //Debug.Log(newslist2[0].Content);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
