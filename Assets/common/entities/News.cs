// class to store the news objects when brought over from db
public class News
{
    public string CompanyName;
    public string Content;
    public int FluctuationRate;

    public News(string CompanyName, string Content, int FluctuationRate){
        this.CompanyName = CompanyName;
        this.Content= Content;
        this.FluctuationRate = FluctuationRate;
    }
}
