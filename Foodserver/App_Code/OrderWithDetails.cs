using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrderWithDetails
/// </summary>
[Serializable]
public class OrderWithDetails
{
    #region Datamembers

    private Order order;
    //private Dictionary<int, OrderDetail> orderDetails;
    private List<Cart> orderDetails;

    #endregion

    #region Constructors

    /*public OrderWithDetails(Order order, Dictionary<int,OrderDetail> orderDetails)
    {
        this.order = order;
        this.orderDetails = orderDetails;
    }*/

    public OrderWithDetails(Order order, List<Cart> orderDetails)
    {
        this.order = order;
        this.orderDetails = orderDetails;
    }

    #endregion

    #region Properties

    public Order Order
    {
        get { return order; }
    }

    /*public Dictionary<int, OrderDetail> OrderDetails
    {
        get { return orderDetails; }
    }*/

    public List<Cart> OrderDetails
    {
        get
        {
            return orderDetails;
        }
    }

    #endregion

    #region Methods

    #endregion
}