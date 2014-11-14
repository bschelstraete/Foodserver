using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrderDetail
/// </summary>
public class OrderDetail
{
    #region Datamembers

    private decimal price;
    private int mealId;
    private int quantity;
    private int orderid;
    private List<Option> options;
    private string comment;

    #endregion

    #region Constructors

    public OrderDetail(int mealId, int orderid,int quantity, string comment, decimal price)
    {
        this.mealId = mealId;
        this.orderid = orderid;
        this.quantity = quantity;
        this.comment = comment;
        this.price = price;
    }

    public OrderDetail(int mealId, int orderid, int quantity, string comment, decimal price, List<Option> options)
    {
        this.mealId = mealId;
        this.orderid = orderid;
        this.quantity = quantity;
        this.comment = comment;
        this.price = price;
        this.options = options;
    }

    #endregion

    #region Properties

    public int MealId
    {
        get { return mealId; }
    }

    public int Quantity
    {
        get { return quantity; }
    }

    public string Comment
    {
        get { return comment; }
    }

    public decimal Price
    {
        get { return price; }
    }

    public int Orderid
    {
        get { return orderid; }
        set { orderid = value; }
    }

    public List<Option> Options
    {
        get { return options; }
    }

    #endregion

    #region Methods

    #endregion
}