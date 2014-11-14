using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WeeklyMenu
/// </summary>
public class WeeklyMenu
{
    #region Datamembers

        private DateTime date;
        private string meal;
        private string campus;

    #endregion

    #region Constructors

    public WeeklyMenu(DateTime d, string m, string c)
    {
        date = d;
        meal = m;
        campus = c;
    }

    #endregion

    #region Properties

    public DateTime Date
    {
        get { return date; }
    }

    public string Meal
    {
        get { return meal; }
    }
    public string Campus {
        get { return campus; }
    }

    #endregion

    public override string ToString()
    {
        return String.Format(meal);
    }
}