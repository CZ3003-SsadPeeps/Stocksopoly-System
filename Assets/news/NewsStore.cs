public class NewsStore
{
    //stores the list of news objects
    public static readonly News[] news = new News[]
    {
        //pull news objs into here 
        new News("Tesla",": brand new Tesla car launching in one month",5),
        new News("Tesla",": car sales had increased by 40% in last quarterly report", 10),
        new News("Tesla",": one big investor had just divest from Tesla", -5),
        new News("Tesla",": a Tesla's car caught on fire with no reason",-10),

        new News("NIO Inc",": brand Nio new car launching in one month",5),
        new News("NIO Inc",": car sales had increased by 40% in last quarterly report", 10),
        new News("NIO Inc",": one big investor had just divest from Nio", -5),
        new News("NIO Inc",": a Nio's car caught on fire with no reason",-10),

        new News("AMC",": cinemas reopening",5),
        new News("AMC",": food revenue increased by 10 percent", 10),
        new News("AMC",": getting shorted by hedge funds", -5),
        new News("AMC",": cinemagoer affected by coronavirus",-10),

        new News("GME",": games sells increased by 40 percent",10),
        new News("GME",": digital sales of opponents dropped",5),
        new News("GME",": hedge funds shorting once more",-5),
        new News("GME",": 1000 stores closed in America",-10),
    };
}

