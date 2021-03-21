using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class to store the news objects when brought over from db
public class News : MonoBehaviour
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
