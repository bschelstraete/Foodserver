using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Userlog
/// </summary>
public class Userlog
{
    #region Datamembers

    private DateTime logTime;
    private string affectedUser;
    private string events;
    private string executedBy;
    private decimal amount;

    #endregion

    #region Constructors

    public Userlog(DateTime logtime, string auser, string ev, string ex, decimal amount) //id autogenerate
    {
        this.logTime = logtime;
        this.affectedUser = auser;
        this.events = ev;
        this.executedBy = ex;
        this.amount = amount;
    }

    #endregion

    #region Properties

    public DateTime LogTime
    {
        get { return logTime; }
    }

    public string AffectedUser
    {
        get { return affectedUser; }
    }

    public string Events
    {
        get { return events; }
    }

    public string ExecutedBy
    {
        get { return executedBy; }
    }

    public decimal Amount
    {
        get { return amount; }
    }

    #endregion

    #region Methods

    #endregion
}