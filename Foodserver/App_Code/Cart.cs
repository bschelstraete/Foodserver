using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cart
/// </summary>
public class Cart
{
	#region Datamembers

    private int teller;
    private OrderDetail orderDetail;
    private DateTime orderDate;

    #endregion

    #region Constructors

    public Cart(int teller, OrderDetail orderDetail, DateTime orderDate)
    {
        this.teller = teller;
        this.orderDetail = orderDetail;
        this.orderDate = orderDate;
    }

    #endregion

    #region Properties

    public int Teller
    {
        get
        {
            return teller;
        }
    }

    public OrderDetail OrderDetail
    {
        get
        {
            return orderDetail;
        }
    }

    public DateTime OrderDate
    {
        get
        {
            return orderDate;
        }
    }

    #endregion

    #region Methods

    #endregion
}