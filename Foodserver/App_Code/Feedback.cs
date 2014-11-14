using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Feedback
/// </summary>
public class Feedback
{
    #region Datamembers

    private string feedback;
    private string email;
    private DateTime date;

    #endregion

    #region Constructors

    public Feedback(string f, string e, DateTime d)
    {
        feedback = f;
        email = e;
        date = d;
    }

    #endregion

    #region Properties

    public string FeedbackText
    {
        get { return feedback; }
    }

    public string Email
    {
        get { return email; }
    }

    public DateTime Date
    {
        get { return date; }
    }

    #endregion

    #region Methods

    #endregion
}