using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MenuWeekly : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        DisplayWeeklyMenu(weekmenuRSS, "RSS");
        DisplayWeeklyMenu(weekmenuSJS, "SJS");
    }

    private void DisplayWeeklyMenu(HtmlGenericControl weekmenu, string campus)
    {
        DateTime[] days = new DateTime[5];
        days = GetDays();

        for (int i = 1; i < 6; i++)
        {
            HtmlGenericControl span = (HtmlGenericControl)weekmenu.FindControl("spanDag" + campus + i);
            span.InnerHtml = days[i - 1].ToString("dd MMMM");

            HtmlGenericControl ul = (HtmlGenericControl)weekmenu.FindControl("ulDag" + campus + i);
            string[] menu = Database.DatabaseInstance.GetMenu(days[i - 1], campus).ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string dag in menu)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                li.InnerHtml = dag;
                ul.Controls.Add(li);
            }
        }
    }

    public DateTime[] GetDays()
    {
        int offset = 0;
        switch (DateTime.Now.DayOfWeek.ToString())
        {
            case "Monday":
                offset = 0;
                break;
            case "Tuesday":
                offset = -1;
                break;
            case "Wednesday":
                offset = -2;
                break;
            case "Thursday":
                offset = -3;
                break;
            case "Friday":
                offset = -4;
                break;
            case "Saturday":
                offset = 2;
                break;
            case "Sunday":
                offset = 1;
                break;
        }
        DateTime[] days = new DateTime[5];
        for (int i = 0; i < 5; i++)
        {
            days[i] = DateTime.Now.AddDays(i + offset); //days of the week
        }
        return days;
    }
}