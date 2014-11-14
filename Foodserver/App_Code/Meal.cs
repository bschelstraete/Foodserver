using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Object meal
/// </summary>
public class Meal
{
    #region Datamembers

    private int mealid;
    private string mealname;
    private decimal price;
    private bool enabled;
    public List<string> CampusList { get; set; }

    #endregion

    #region Constructors

    public Meal(string mealname, decimal price, bool enabled)
    {
        this.mealname = mealname;
        this.price = price;
        this.enabled = enabled;
    }

    public Meal(string mealname, decimal price, bool enabled, List<string> campusList)
    {
        this.mealname = mealname;
        this.price = price;
        this.enabled = enabled;
        CampusList = campusList;
    }

    public Meal(int mealdid, string mealname, decimal price, bool enabled)
    {
        this.mealid = mealdid;
        this.mealname = mealname;
        this.price = price;
        this.enabled = enabled;
    }

    public Meal(int mealdid, string mealname, decimal price, bool enabled, List<string> campusList)
    {
        this.mealid = mealdid;
        this.mealname = mealname;
        this.price = price;
        this.enabled = enabled;
        CampusList = campusList;
    }

    #endregion

    #region Properties

    public int Mealid
    {
        get { return mealid; }
    }

    public bool Enabled
    {
        get { return enabled; }
    }

    public string Mealname
    {
        get { return mealname; }
    }

    public decimal Price
    {
        get { return price; }
    }

    
    #endregion

    #region Methods

    public override string ToString()
    {
        return string.Format("{0}", this.Mealname);
    }

    #endregion
}