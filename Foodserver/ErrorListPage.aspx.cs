using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ErrorListPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["type"] != "admin")
        {
            Response.Write("<script type='text/javascript'>alert('U hebt geen toegang tot deze pagina.');</script>");
            System.Threading.Thread.Sleep(2000);
            //Response.Redirect("Main.aspx");
            Server.Transfer("Main.aspx");
        }
    }
}