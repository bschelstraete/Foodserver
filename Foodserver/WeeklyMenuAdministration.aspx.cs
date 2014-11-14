using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WeeklyMenuAdministration : System.Web.UI.Page
{
    DateTime[] days;
    List<TextBox> RSSBoxes, SJSBoxes;
    protected void Page_Init(object sender, EventArgs e) { }

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["type"] != "admin")
        {
            Response.Write("<script type='text/javascript'>alert('U hebt geen toegang tot deze pagina.');</script>");
            System.Threading.Thread.Sleep(2000);
            Server.Transfer("Main.aspx");
        }





        LoadDates();
        if (!IsPostBack)
        {
            Cal_Menu.SelectedDate = DateTime.Now.Date;
            foreach (Campus campus in Database.DatabaseInstance.GetCampusList())
            {

                cblTo.Items.Add(new ListItem(campus.Name, campus.Id));
                lstFrom.Items.Add(new ListItem(campus.Name, campus.Id));

            }
        }
        MakeWeeklyMenuIfNotExist();





        LoadTable();
    }

    public void MakeWeeklyMenuIfNotExist()
    {
        if (!Database.DatabaseInstance.weeklyMenuExists(Cal_Menu.SelectedDate))
        {
            Database.DatabaseInstance.CreateEmptyWeeklyMenu(Cal_Menu.SelectedDate);
        }
    }

    private void LoadDates()
    {
        foreach (Control c in Page.Controls)
        {
            if (c is Label)
            {
                Label label = (Label)c;
                foreach (String key in label.Attributes.Keys)
                {
                    Console.WriteLine(key + "=" + label.Attributes[key]);
                }

            }
        }
    }

    private void GetMeals()
    {
        for (int i = 0; i < RSSBoxes.Count; i++)
        {
            RSSBoxes[i].Text = Database.DatabaseInstance.GetMenu(days[i], "RSS").ToString();
        }
        for (int i = 0; i < SJSBoxes.Count; i++)
        {
            SJSBoxes[i].Text = Database.DatabaseInstance.GetMenu(days[i], "SJS").ToString();
        }
    }

    private void LoadTable()
    {
        WeeklyMenuHtml menu = new WeeklyMenuHtml();
        days = menu.GetDays(Cal_Menu.SelectedDate);
        //LoadDates();
    }
    protected void Cal_Menu_SelectionChanged(object sender, EventArgs e)
    {
        //LoadTable();
        //GetMeals();
        MakeWeeklyMenuIfNotExist();
        GridView1.DataBind();
    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < RSSBoxes.Count; i++)
        {
            Database.DatabaseInstance.InsertMenu(days[i], RSSBoxes[i].Text, "RSS");
        }
        for (int i = 0; i < SJSBoxes.Count; i++)
        {
            Database.DatabaseInstance.InsertMenu(days[i], SJSBoxes[i].Text, "SJS");
        }
        GetMeals();
    }
    protected void Cal_Menu_DayRender(object sender, DayRenderEventArgs e)
    {
        if (e.Day.IsWeekend)
        {
            e.Day.IsSelectable = false;
        }
    }



    private HtmlGenericControl getHtmlForBody(Campus campus)
    {
        string[] days = new string[5] { "Mon", "Tue", "Wed", "Thu", "Fri" };

        foreach (string day in days)
        {
            HtmlGenericControl formGroup = new HtmlGenericControl("div");
            formGroup.Attributes.Add("class", "form-group");
        }




        return null;
    }


    //Bulk update van gridview bron: http://msdn.microsoft.com/en-us/library/aa992036(v=vs.100).aspx
    //
    private bool tableCopied = false;
    private System.Data.DataTable originalDataTable;

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            if (!tableCopied)
            {
                originalDataTable = ((System.Data.DataRowView)e.Row.DataItem).Row.Table.Copy();
                ViewState["originalValuesDataTable"] = originalDataTable;
                tableCopied = true;
            }
    }
    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        originalDataTable = (System.Data.DataTable)ViewState["originalValuesDataTable"];

        foreach (GridViewRow r in GridView1.Rows)
            if (IsRowModified(r)) { GridView1.UpdateRow(r.RowIndex, false); }

        // Rebind the Grid to repopulate the original values table.
        tableCopied = false;
        GridView1.DataBind();
    }

    protected bool IsRowModified(GridViewRow r)
    {
        //int currentID;
        //string currentLastName;

        //currentID = Convert.ToInt32(GridView1.DataKeys[r.RowIndex].Value);

        //currentLastName = ((TextBox)r.FindControl("LastNameTextBox")).Text;

        //System.Data.DataRow row =
        //    originalDataTable.Select(String.Format("EmployeeID = {0}", currentID))[0];

        //if (!currentLastName.Equals(row["LastName"].ToString())) { return true; }

        return true;
    }
    protected void btnKopkieer_Click(object sender, EventArgs e)
    {
        List<Campus> campusList = new List<Campus>();
        foreach (ListItem listItem in cblTo.Items)
        {
            if (listItem.Selected)
            {
                campusList.Add(new Campus(listItem.Value, listItem.Text));
            }
        }
        Database.DatabaseInstance.CopyWeeklyMenu(Cal_Menu.SelectedDate, new Campus(lstFrom.SelectedItem.Value,lstFrom.SelectedItem.Text ), campusList);
        GridView1.DataBind();
    }
}