using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock
{
    public string companyName;
    public int total;
    public int shares;
    public int price;
    public int day;

    public Stock(string companyName, int total, int shares, int day,int price)
    {
        this.companyName=companyName;
        this.total=total;
        this.shares=shares;
        this.day =day;
        this.price=price;
    }
}
