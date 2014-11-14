using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for eWallet
/// </summary>
public class eWallet
{
    #region Datamembers

    private decimal euro;
    private decimal howesters;

    #endregion

    #region Constructors

    public eWallet(decimal eu, decimal howesters)
    {
        this.euro = eu;
        this.howesters = howesters;
    }

    #endregion

    #region Properties

    public decimal Euro
    {
        get { return euro; }
    }
    
    public decimal Howesters
    {
        get { return howesters; }
    }

    #endregion

    #region Methods

    public void AddEuro(decimal amount)
    {
        //dit moet later via de comp coins webservice gebeuren
        this.euro += amount;
    }

    public void SubtractEuro(decimal amount)
    {
        //dit moet later via de comp coins webservice gebeuren
        this.euro -= amount;
    }

    #endregion
}