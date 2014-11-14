using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DSMyTodayOrders.SelectCommand = "SELECT DISTINCT CONVERT(date,orders.deliverydate,103) as 'Date',  orders.orderid, order_details.orderdetailsid, meals.mealname AS 'Maaltijd', options.optionname AS 'Option', SUM(order_details.quantity) AS aantal,((meals.price + options.price) * sum(order_details.quantity)) AS Totaal, order_details.comment AS Opmerking, orders.campus AS Campus from orders INNER JOIN order_details on order_details.orderid = orders.orderid INNER JOIN meals ON order_details.mealid = meals.mealid INNER JOIN order_details_option ON order_details_option.orderdetailsid = order_details.orderdetailsid INNER JOIN options on options.optionid = order_details_option.optionid where (orders.email = '" + Session["user"].ToString() + "') and DATEDIFF(day, orders.deliverydate, GETDATE()) = 0  GROUP BY CONVERT(date,orders.deliverydate,103), orders.orderid,  order_details.orderdetailsid, options.optionname, options.price, meals.mealname, meals.price, order_details.comment, orders.campus ORDER BY CONVERT(date,orders.deliverydate,103) DESC";
        DSMyFutureOrders.SelectCommand = "SELECT DISTINCT CONVERT(date,orders.deliverydate,103) as 'Date',  orders.orderid, order_details.orderdetailsid, meals.mealname AS 'Maaltijd', options.optionname AS 'Option', SUM(order_details.quantity) AS aantal,((meals.price + options.price) * sum(order_details.quantity)) AS Totaal, order_details.comment AS Opmerking, orders.campus AS Campus FROM[orders] INNER JOIN order_details ON order_details.orderid = orders.orderid INNER JOIN meals ON order_details.mealid = meals.mealid INNER JOIN order_details_option ON order_details_option.orderdetailsid = order_details.orderdetailsid INNER JOIN options ON options.optionid = order_details_option.optionid WHERE (orders.email = '" + Session["user"].ToString() + "') and DATEDIFF(day, orders.deliverydate, GETDATE()) < 0 GROUP BY CONVERT(date,orders.deliverydate,103), orders.orderid,  order_details.orderdetailsid, options.optionname, options.price, meals.mealname, meals.price, order_details.comment, orders.campus ORDER BY CONVERT(date,orders.deliverydate,103) DESC";

        DSMyPastOrders.SelectCommand = "SELECT DISTINCT CONVERT(date,orders.deliverydate,103) as 'Date',  orders.orderid, order_details.orderdetailsid, meals.mealname AS 'Maaltijd', options.optionname AS 'Option', SUM(order_details.quantity) AS aantal,((meals.price + options.price) * sum(order_details.quantity)) AS Totaal, order_details.comment AS Opmerking, orders.campus AS Campus from orders INNER JOIN order_details on order_details.orderid = orders.orderid INNER JOIN meals ON order_details.mealid = meals.mealid INNER JOIN order_details_option ON order_details_option.orderdetailsid = order_details.orderdetailsid INNER JOIN options on options.optionid = order_details_option.optionid where (orders.email = '" + Session["user"].ToString() + "') and DATEDIFF(day, orders.deliverydate, GETDATE()) > 0  GROUP BY CONVERT(date,orders.deliverydate,103), orders.orderid,  order_details.orderdetailsid, options.optionname, options.price, meals.mealname, meals.price, order_details.comment, orders.campus ORDER BY CONVERT(date,orders.deliverydate,103) DESC ";

        if (!IsPostBack)
        {
            if (Database.DatabaseInstance.CheckIfUserInMailinglist(Session["user"].ToString()))
            {
               cbEmailConfimation.Checked = true;
            }
           else cbEmailConfimation.Checked = false;
        }

        if (!IsPostBack)
        {
            showVandaag();
        }
        

        setCancelVisability();
	}

    private void setCancelVisability()
    {
        TimeSpan maxOrderHour = new TimeSpan(10, 00, 00);
        if (DateTime.Now.TimeOfDay <= maxOrderHour)
        {
            GridMyTodayOrders.Columns[9].Visible = true;
        }
    }

    protected void GridFutureDelete_Click(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GridMyFutureOrders.Rows[index];
        int orderId = Convert.ToInt32(GridMyFutureOrders.DataKeys[index].Values[0]);
        int orderDetailId = Convert.ToInt32(GridMyFutureOrders.DataKeys[index].Values[1]);
        CancelOrder(orderId, orderDetailId);
    }

    protected void GridTodayDelete_Click(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GridMyTodayOrders.Rows[index];
        int orderId = Convert.ToInt32(GridMyTodayOrders.DataKeys[index].Values[0]);
        int orderDetailId = Convert.ToInt32(GridMyTodayOrders.DataKeys[index].Values[1]);
        CancelOrder(orderId, orderDetailId);
    }

    private void CancelOrder(int oid, int odid)
    {
        if (Database.DatabaseInstance.GetOrderDetailsById(oid).Count > 1)
        {
            //1 verwijderen
            Database.DatabaseInstance.CancelOrderDetail(odid, Session["user"].ToString());
            Response.Redirect(Request.RawUrl);
        }
        else
        {
            //alles verwijderen
            Database.DatabaseInstance.CancelOrder(oid, odid, Session["user"].ToString());
            Response.Redirect(Request.RawUrl);
        }
    }

    protected void btnSaveAccountSettings_Click(object sender, EventArgs e)
    {
        lblConfirmAccountSettings.Visible = true;
        lblConfirmAccountSettings.Text = "Instellingen opgeslagen";
        if (cbEmailConfimation.Checked)
        {
            Database.DatabaseInstance.AddUserToMailinglist(Session["user"].ToString());
            cbEmailConfimation.Checked = true;
        }
        else
        {
            Database.DatabaseInstance.RemoveUserFromMailinglist(Session["user"].ToString());
            cbEmailConfimation.Checked = false;
        }

        //if (IsPostBack)
        //{
        if (Database.DatabaseInstance.CheckIfUserInMailinglist(Session["user"].ToString()))
        {
            cbEmailConfimation.Checked = true;
        }
        else cbEmailConfimation.Checked = false;
        //}
    }

    private void showVandaag()
    {
        besVandaag.Visible = true;
        besToekomst.Visible = false;
        besVerleden.Visible = false;
        besInstellingen.Visible = false;
        li_Vandaag.Attributes.Add("class", "active");
        li_Toekomst.Attributes.Add("class", "");
        li_Verleden.Attributes.Add("class", "");
        li_Instellingen.Attributes.Add("class", "");
        //h3_MyOrders.InnerHtml = "Bestellingen voor vandaag";
    }

    private void showToekomst()
    {
        besVandaag.Visible = false;
        besToekomst.Visible = true;
        besVerleden.Visible = false;
        besInstellingen.Visible = false;
        li_Vandaag.Attributes.Add("class", "");
        li_Toekomst.Attributes.Add("class", "active");
        li_Verleden.Attributes.Add("class", "");
        li_Instellingen.Attributes.Add("class", "");
        //h3_MyOrders.InnerHtml = "Toekomstige bestellingen";
    }

    private void showVerleden()
    {
        besVandaag.Visible = false;
        besToekomst.Visible = false;
        besVerleden.Visible = true;
        besInstellingen.Visible = false;
        li_Vandaag.Attributes.Add("class", "");
        li_Toekomst.Attributes.Add("class", "");
        li_Verleden.Attributes.Add("class", "active");
        li_Instellingen.Attributes.Add("class", "");
        //h3_MyOrders.InnerHtml = "Vorige bestellingen";
    }

    private void showInstellingen()
    {
        besVandaag.Visible = false;
        besToekomst.Visible = false;
        besVerleden.Visible = false;
        besInstellingen.Visible = true;
        li_Vandaag.Attributes.Add("class", "");
        li_Toekomst.Attributes.Add("class", "");
        li_Verleden.Attributes.Add("class", "");
        li_Instellingen.Attributes.Add("class", "active");
        //h3_MyOrders.InnerHtml = "Instellingen";
    }

    protected void lbtn_Vandaag_Click(object sender, EventArgs e)
    {
        showVandaag();
    }

    protected void lbtn_Toekomst_Click(object sender, EventArgs e)
    {
        showToekomst();
    }

    protected void lbtn_Verleden_Click(object sender, EventArgs e)
    {
        showVerleden();
    }
    protected void lbtn_Instellingen_Click(object sender, EventArgs e)
    {
        showInstellingen();
    }
}