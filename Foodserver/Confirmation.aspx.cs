using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class Confirmation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataSource1.SelectCommand = "SELECT DISTINCT DATEADD(dd, 0, DATEDIFF(dd, 0, orders.deliverydate)) AS 'Date', meals.mealname AS 'Maaltijd', SUM(order_details.quantity) AS aantal, (meals.price * sum(order_details.quantity)) AS Totaal, order_details.comment as Opmerking " +
        "FROM orders " +
        "INNER JOIN order_details ON orders.orderid = order_details.orderid " +
        "INNER JOIN meals ON order_details.mealid = meals.mealid " +
        "WHERE (orders.email = '" + Session["user"].ToString() + "') AND (DATEPART(YYYY, orders.deliverydate) = DATEPART(YYYY, GETDATE())) AND (DATEPART(MM, orders.deliverydate) = DATEPART(mm, GETDATE())) AND (DATEPART(dd, orders.deliverydate) = DATEPART(dd, GETDATE())) OR " +
        "(orders.email = '" + Session["user"].ToString() + "') AND (orders.deliverydate > GETDATE()) " +
        "GROUP BY DATEADD(dd, 0, DATEDIFF(dd, 0, orders.deliverydate)), meals.mealname, meals.price, order_details.comment " +
        "ORDER BY 'Date'";
    }
}
