using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Order
/// </summary>
public class Order
{
    #region Datamembers

    private string email;
    private string campus;
    private DateTime deliveryDate;
    private DateTime orderDate;
    //List<OrderDetail> orderDetails;

    #endregion

    #region Constructors

    public Order(string email, DateTime deliveryDate, string campus)
	{
        this.email = email;
        this.campus = campus;
        this.deliveryDate = deliveryDate;
        this.orderDate = DateTime.Now;
        //this.orderDetails = orderDetails;
    }

    #endregion

    #region Properties

    public DateTime OrderDate
    {
        get { return orderDate; }
    }

    public DateTime DeliveryDate
    {
        get { return deliveryDate; }
    }

    public string Campus
    {
      get { return campus; }
    }

    public string Email
    {
        get { return email; }
    }

    #endregion

    #region Methods

    #endregion
}