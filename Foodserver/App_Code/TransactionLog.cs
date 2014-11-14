using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TransactionLog
/// </summary>
public class TransactionLog
{
    private DateTime date;
    private string user;
    private string order; //order in string zetten, anders kan delete niet als opmerking
    private decimal wallet;

	public TransactionLog(DateTime date, string user, string order, decimal wallet)
	{
        this.date = date;
        this.user = user;
        this.order = order;
        this.wallet = wallet;
	}

    public DateTime Date { get { return date; } }
    public string User { get { return user; } }
    public string Order { get { return order; } }
    public decimal Wallet { get { return wallet; } }

}