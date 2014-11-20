using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Option
/// </summary>
public class Option
{
    #region Datamembers

    private int optionid;
    private string optionname;
    private bool enabled;
    private decimal price;
    public List<string> Campussen { get; set; }

    #endregion

    #region Constructors

    public Option(string oname, bool e, decimal p)
	{
        this.optionname = oname;
        this.enabled = e;
        this.price = p;
	}

    public Option(string oname, bool e, decimal p, List<string> campussen)
    {
        this.optionname = oname;
        this.enabled = e;
        this.price = p;
        Campussen = campussen;
    }

    public Option(int id, string oname, bool e, decimal p)
    {
        this.optionid = id;
        this.optionname = oname;
        this.enabled = e;
        this.price = p;
    }

    public Option(int id, string oname, bool e, decimal p, List<string> campussen)
    {
        this.optionid = id;
        this.optionname = oname;
        this.enabled = e;
        this.price = p;
        Campussen = campussen;
    }

    #endregion

    #region Properties

    public int Optionid
    {
        get { return optionid; }
    }

    public decimal Price
    {
        get { return price; }
    }

    public bool Enabled
    {
        get { return enabled; }
    }

    public string Optionname
    {
        get { return optionname; }
    }

    #endregion

    #region Methods

    public override string ToString()
    {
        return optionname;
    }

    #endregion
}