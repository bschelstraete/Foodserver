using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Log
/// </summary>
public class Log
{
    #region Datamembers

    private string logType; //RELOAD: loggen herladen geld //ORDER: loggen geplaatste bestelling
    private DateTime logTime;
    private string loggedInUser;
    private string optionalParameter1; // Wordt gebruikt om te zien voor wie er geld opgeladen werd. normaal mag dit enkel PeterVanloo zijn!
    // als het gaat om een type ORDER dan staat dit veld leeg
    private string logMessage;

    #endregion

    #region Constructors

    public Log(string logtype, DateTime logtime, string loggedinuser, string logmessage, string optionalparameter1) //id autogenerate
    {
        this.logType = logtype;
        this.logTime = logtime;
        this.loggedInUser = loggedinuser;
        this.logMessage = logmessage;
        this.optionalParameter1 = optionalparameter1;
    }

    #endregion

    #region Properties



    public string LogType
    {
        get { return logType; }
    }

    public DateTime LogTime
    {
        get { return logTime; }
    }

    public string LoggedInUser
    {
        get { return loggedInUser; }
    }

    public string OptionalParameter1
    {
        get { return optionalParameter1; }
    }

    public string LogMessage
    {
        get { return logMessage; }
    }

    #endregion

    #region Methods

    #endregion
}