using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

public partial class Kitchen : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["type"] == "user")
        {
            Response.Write("<script type='text/javascript'>alert('U hebt geen toegang tot deze pagina.');</script>");
            System.Threading.Thread.Sleep(2000);
            Server.Transfer("Main.aspx");
        }
        else
        {
            if (!IsPostBack)
            {              
              Cld_Datum.SelectedDate = DateTime.Today;              
            }
           UpdateDate();
        }
    }       

    private void UpdateDate()
    {               
        Lbl_Datum.Text = Cld_Datum.SelectedDate.ToShortDateString();
    }

    protected void Cld_Datum_SelectionChanged(object sender, EventArgs e)
    {
        UpdateDate();
        ReportViewerNewKitchenReport.LocalReport.Refresh();
        ReportViewerBroodjesLabels.LocalReport.Refresh();
    }

    protected void Cal_Menu_DayRender(object sender, DayRenderEventArgs e)
    {
        if (e.Day.IsWeekend)
        {
            e.Day.IsSelectable = false;
        }
    }
}