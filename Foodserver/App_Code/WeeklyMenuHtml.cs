using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for WeeklyMenuHtml
/// </summary>
public class WeeklyMenuHtml
{
	public WeeklyMenuHtml()
	{
	}

    public HtmlGenericControl GetWeeklyMenu(HtmlGenericControl weekmenuDiv, DateTime days, string campus)
    {
        foreach (DateTime date in GetDays(days))
        {
            Label dateHeader = new Label();
            dateHeader.Text = translateDayOfWeek(date) + " - " + date.Date.ToShortDateString();
            weekmenuDiv.Controls.Add(dateHeader);

            string[] dayFood = Database.DatabaseInstance.GetMenu(date, campus).ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            System.Web.UI.WebControls.BulletedList list = new System.Web.UI.WebControls.BulletedList();
            weekmenuDiv.Controls.Add(list);
            foreach (string s in dayFood)
            {
                ListItem food = new ListItem();
                food.Text = s;
                list.Items.Add(food);
            }
        }
        return weekmenuDiv;
    }

    private string translateDayOfWeek(DateTime date)
    {
        switch (date.DayOfWeek.ToString())
        {
            case "Monday":
                return "Maandag";
            case "Tuesday":
                return "Dinsdag";
            case "Wednesday":
                return "Woensdag";
            case "Thursday":
                return "Donderdag";
            case "Friday":
                return "Vrijdag";
        }
        return "";
    }
    public DateTime[] GetDays(DateTime date)
    {
        int offset = 0;
        switch (date.DayOfWeek.ToString())
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
            days[i] = date.AddDays(i + offset).Date; //days of the week
        }
        return days;
    }
}