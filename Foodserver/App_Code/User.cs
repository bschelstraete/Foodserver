using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    #region Datamembers

    private eWallet mywallet;
    private string email;
    private string fname;
    private string lname;
    private Employment employment;

    #endregion

    #region Constructors

    public User(string email, eWallet wallet, string fname, string lname, Employment employment)
    {
        this.email = email;
        this.mywallet = wallet;
        this.fname = fname;
        this.lname = lname;
        this.employment = employment;
    }

    #endregion

    #region Properties

    public string Email
    {
        get { return email; }
    }

    public eWallet Mywallet
    {
        get { return mywallet; }
    }

    public string Fname
    {
        get { return fname; }
    }

    public string Lname
    {
        get { return lname; }
    }

    public Employment Employment
    {
        get { return employment; }
    }

    #endregion

    #region Methods

    public string WelcomeUser()
    {
        return string.Format("Welkom {0} {1}!", this.Fname, this.Lname);
    }

    public void ReloadWallet(decimal amount, string loggedInUser)
    {
        /*ItemAmountDto amountitem = new ItemAmountDto();
        ItemDto item = new ItemDto();
        item.itemID = 2;
        amountitem.amount = amount;
        amountitem.item = item;//howesters = 3 EN euro = 2

        ccc.depositItemToWallet(token, GetWallet(), amountitem);*/
        if (amount > 0)
        {
            Database.DatabaseInstance.WriteLog(new Userlog(DateTime.Now, this.email, "RELOAD", loggedInUser, amount));
        }
        else {
            Database.DatabaseInstance.WriteLog(new Userlog(DateTime.Now, this.email, "PAYMENT", loggedInUser, amount));
        }
        Database.DatabaseInstance.addEuro(amount, email);
    }

    public void PayOrder(decimal amount)
    {
        /*ItemAmountDto amountitem = new ItemAmountDto();
        ItemDto item = new ItemDto();
        item.itemID = 2;
        amountitem.amount = amount;
        amountitem.item = item;//howesters = 3 EN euro = 2
        
        ccc.withdrawItemFromWallet(token, GetWallet(), amountitem);*/
        
        Database.DatabaseInstance.PayOrder(amount, email);
    }

    public decimal getWalletAmount()
    {
        /*foreach (ItemAmountDto item in GetWallet().itemAmountDto)
        {
            if (item.item.itemID == 2)
            {
                return item.amount;
            }
        }
        return 0;*/
        eWallet wallet = Database.DatabaseInstance.GetUserEWallet(email);
        return wallet.Euro;
    }

    public void ChangeEmployment(User user, Employment e)
    {
        Database.DatabaseInstance.ChangeEmployment(user, e);
    }

    public void ChangeFeedbackMail(User user, bool b)
    {
        Database.DatabaseInstance.ChangeFeedBackMail(user, b);
    }

    #endregion
}