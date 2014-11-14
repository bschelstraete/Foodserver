using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HowestIdentity;
//using NewDatasetFoodserverTableAdapters;
using DatasetFoodserverV3TableAdapters;

/// <summary>
/// Summary description for Database
/// </summary>
public class Database
{
    #region Datamembers
    private static Database databaseInstance = new Database();
    private HowestIdentity.IdentityManagementWebserviceSoapClient identityService = new IdentityManagementWebserviceSoapClient();
    //private NewDatasetFoodserver ds = new NewDatasetFoodserver();
    private DatasetFoodserverV3 ds = new DatasetFoodserverV3();
    private mealsTableAdapter taMeals = new mealsTableAdapter();
    private ordersTableAdapter taOrders = new ordersTableAdapter();
    private order_detailsTableAdapter taOrderDetails = new order_detailsTableAdapter();
    private kitchenstaffTableAdapter taKitchenStaff = new kitchenstaffTableAdapter();
    private adminsTableAdapter taAdmins = new adminsTableAdapter();
    private walletTableAdapter taWallet = new walletTableAdapter();
    private feedbackTableAdapter taFeedback = new feedbackTableAdapter();
    private userlogTableAdapter taLogs = new userlogTableAdapter();
    private WeeklyMenuTableAdapter taWeeklyMenu = new WeeklyMenuTableAdapter();
    private CampusTableAdapter taCampus = new CampusTableAdapter();
    //new ta's
    private order_details_optionTableAdapter taOrderDetailsOptions = new order_details_optionTableAdapter();
    private meal_optionsTableAdapter taMealOptions = new meal_optionsTableAdapter();
    private optionsTableAdapter taOptions = new optionsTableAdapter();
    private mailinglistTableAdapter taMailinglist = new mailinglistTableAdapter();
    private Paypal_IPNTableAdapter taPaypalIpn = new Paypal_IPNTableAdapter();
    private meal_visibleTableAdapter taMealVisible = new meal_visibleTableAdapter();
    private option_visibleTableAdapter taOptionVisible = new option_visibleTableAdapter();
    private DataBaseNameAdapter taDataTableDatabaseName = new DataBaseNameAdapter();
    private motdTableAdapter taMotd = new motdTableAdapter();
    private options_CampusTableAdapter taOptionCampus = new options_CampusTableAdapter();
    private meal_CampusTableAdapter taMealCampus = new meal_CampusTableAdapter();
    private transactionlogTableAdapter taTransLog = new transactionlogTableAdapter();
    #endregion

    #region Constructors

    private Database()
    {

    }

    #endregion

    #region Properties

    public IdentityManagementWebserviceSoapClient IdentityService
    {
        get { return identityService; }
        set { identityService = value; }
    }

    public static Database DatabaseInstance
    {
        get
        {
            if (databaseInstance == null)
            {
                databaseInstance = new Database();
            }
            return databaseInstance;
        }
    }

    public string DataBaseName
    {
        get
        {
            taDataTableDatabaseName.Fill(ds.DataTable1);
            return ds.DataTable1[0].DataBaseName;
        }
    }


    #endregion
    
    #region Methods 

    #region GlobalMethods

    /// <summary>
    /// Inloggen indien de username & wachtwoord kloppen op de iBamaFlex databank
    /// </summary>
    /// <param name="email">email van de gebruiker</param>
    /// <param name="password">wachtwoord van de gebruiker</param>
    /// <returns>De gebruiker is WEL/NIET ingelogd</returns>
    public bool Login(string email, string password)
    {
#if DEBUG
        return true;
#else
        if (this.IdentityService.AuthenticateUserByEmail(email, password)) 
        {
            return true;
        }
        else return false;
#endif
        
    }

    /// <summary>
    /// Haalt de user op van de iBamaFlex databank op basis van een email adres
    /// </summary>
    /// <param name="email">email van de user</param>
    /// <returns>User object</returns>
    public User GetUser(string email)
    {
        
#if DEBUG
                return new User(email, GetUserEWallet(email), email.Split('.')[0], email.Split('.')[1], GetUsersEmployment(email));

#else
        HowestIdentity.User user = this.IdentityService.GetUserByEmail(email);
        
        return new User(user.Email, GetUserEWallet(email), user.Firstname, user.Lastname, GetUsersEmployment(email));
#endif

    }

    /// <summary>
    /// (oude method)Geeft de prijs van de maaltijd terug.
    /// </summary>
    /// <param name="mealID">De ID van de maaltijd</param>
    /// <returns></returns>
    public decimal GetMealPrice(int mealID)
    {
        List<Meal> meals = GetMeals();
        foreach (Meal meal in meals)
        {
            if (meal.Mealid == mealID)
            {
                return meal.Price;
            }
        }
        return 0;
    }

    /// <summary>
    /// Haalt de naam van een maaltijd op
    /// </summary>
    /// <param name="mid">ID van de maaltijd</param>
    /// <returns>Naam van de maaltijd</returns>
    public string GetMealName(int mid)
    {
        taMeals.FillByMealId(ds.meals, mid);
        return ds.meals[0].mealname;
    }
    
    /// <summary>
    /// Haalt users op op basis van een search string
    /// </summary>
    /// <param name="naam"></param>
    /// <returns></returns>
    public List<User> findUsers(string name)
    {
        List<HowestIdentity.User> list = new List<HowestIdentity.User>();
        List<User> listusers = new List<User>();
        list.AddRange(this.identityService.GetUsersBySearchString(name));
        foreach (HowestIdentity.User user in list)
        {
            listusers.Add(new User(user.Email, GetUserEWallet(user.Email), user.Firstname, user.Lastname, GetUsersEmployment(user.Email)));
        }
        return listusers;

    }

    /// <summary>
    /// Haalt de email adressen van het keukenpersoneel op
    /// </summary>
    /// <returns></returns>
    public List<string> GetKitchenStaff()
    {
        List<string> kitchenstaffList = new List<string>();
        taKitchenStaff.Fill(ds.kitchenstaff);
        for (int i = 0; i < ds.kitchenstaff.Count; i++)
        {
            kitchenstaffList.Add(ds.kitchenstaff[i].email);
        }
        return kitchenstaffList;
    }

    /// <summary>
    /// Haalt de email adressen van alle administrators op
    /// </summary>
    /// <returns>Lijst van emails van de administrators</returns>
    public List<string> GetAdmins()
    {
        List<string> adminsList = new List<string>();
        taAdmins.Fill(ds.admins);
        for (int i = 0; i < ds.admins.Count; i++)
        {
            adminsList.Add(ds.admins[i].email);
        }
        return adminsList;
    }

    public List<string> GetAdminsFeedbackEnabled()
    {
        List<string> adminsList = new List<string>();
        taAdmins.Fill(ds.admins);
        for (int i = 0; i < ds.admins.Count; i++)
        {
           
            adminsList.Add(ds.admins[i].email);
        }
        return adminsList;
    }

    /// <summary>
    /// Geeft alle maaltijden terug                                             EDIT
    /// </summary>
    /// <returns>Lijst van maaltijden</returns>
    public List<Meal> GetMeals()
    {
        List<Meal> mealList = new List<Meal>();
        taMeals.FillMeals(ds.meals);
        for (int i = 0; i < ds.meals.Count; i++)
        {
          mealList.Add(new Meal(ds.meals[i].mealid, ds.meals[i].mealname, ds.meals[i].price, ds.meals[i].enabled, GetCampussenByMealId(ds.meals[i].mealid)));
        }
        return mealList;
    }

    public List<Meal> GetVisibleMeals()
    {
        taMeals.FillByVisibleMeals(ds.meals);
        List<Meal> mealList = new List<Meal>();
        for (int i = 0; i < ds.meals.Count; i++)
            mealList.Add(new Meal(ds.meals[i].mealid, ds.meals[i].mealname, ds.meals[i].price, ds.meals[i].enabled, GetCampussenByMealId(ds.meals[i].mealid)));
        return mealList;
    }

    public List<Option> GetVisibleOptions()
    {
        taOptions.FillByVisibleOptions(ds.options);
        List<Option> optionList = new List<Option>();
        for (int i = 0; i < ds.options.Count; i++)
            optionList.Add(new Option(ds.options[i].optionid, ds.options[i].optionname, ds.options[i].enabled, ds.options[i].price));

        return optionList;
    }

    /// <summary>
    /// Geeft alle deelbestellingen terug voor een hoofdbestelling
    /// </summary>
    /// <param name="oid">ID van de hoofdbestelling</param>
    /// <returns>Lijst van alle deelbestellingen van de hoofdbestelling</returns>
    public List<OrderDetail> GetOrderDetailsById(int oid)
    {
        List<OrderDetail> orderDetailList = new List<OrderDetail>();
        taOrderDetails.FillByOrderDetailId(ds.order_details, oid);
        for (int i = 0; i < ds.order_details.Count; i++)
        {
            orderDetailList.Add(new OrderDetail(ds.order_details[i].mealid, ds.order_details[i].orderid, ds.order_details[i].quantity, ds.order_details[i].comment, ds.order_details[i].price));
        }
        return orderDetailList;
    }

    /// <summary>
    /// Geeft de actuele prijs van de maaltijd terug
    /// </summary>
    /// <param name="mid">ID van de maaltijd</param>
    /// <returns>actuele prijs van de maaltijd</returns>
    public decimal GetActualMealPrice(int mid, string campus)
    {
        taMeals.FillMealPrice(ds.meals, mid, campus);
        return ds.meals[0].price;
    }

    /// <summary>
    /// Haalt een optie op uit de databank                                         EDIT
    /// </summary>
    /// <param name="oid">ID van de optie</param>
    /// <returns>Een option object</returns>
    public Option GetOption(int oid, string campus)
    {
        taOptions.FillOptionByID(ds.options, oid, campus);
        return new Option(ds.options[0].optionid, ds.options[0].optionname, ds.options[0].enabled, ds.options[0].price);
    }

    /// <summary>
    /// Geeft de beschikbare opties voor een maaltijd terug                     EDIT
    /// </summary>
    /// <param name="mid">ID van de maaltijd</param>
    /// <returns>Lijst van beschikbare opties voor die maaltijd</returns>
    public List<Option> GetOptionsForMeal(int mid, string campus)
    {
        List<Option> optionlist = new List<Option>();
        //taOptions.FillOptionsForMeal()
        taOptions.FillOptionsForMeal(ds.options, campus, mid);
        for (int i = 0; i < ds.options.Count; i++)
        {
            optionlist.Add(new Option(ds.options[i].optionid, ds.options[i].optionname, ds.options[i].enabled, ds.options[i].price));
        }
        return optionlist;
    }

    /// <summary>
    /// Geeft alle beschikbare opties terug
    /// </summary>
    /// /// <returns>Lijst van alle opties</returns>
    public List<Option> GetAllOptions()
    {
        List<Option> optionList = new List<Option>();
        taOptions.FillByName(ds.options);
        for (int i = 0; i < ds.options.Count; i++)
        { 
            optionList.Add(new Option(ds.options[i].optionid, ds.options[i].optionname, ds.options[i].enabled, ds.options[i].price));
        }

        return optionList;
    }

    public Option GetOptionByOptionName(string optionName)
    {
        Option option = new Option("", false, 0);
        taOptions.FillByOptionName(ds.options, optionName);
        for (int i = 0; i < ds.options.Count; i++)
        {
            option = new Option(ds.options[i].optionid, ds.options[i].optionname, ds.options[i].enabled, ds.options[i].price);
        }

        return option;
    }

    /// <summary>
    /// Geeft de feedback terug
    /// </summary>
    /// <returns>Lijst van de feedback</returns>
    public List<Feedback> GetFeedback()
    {
        List<Feedback> feedbackList = new List<Feedback>();
        taFeedback.Fill(ds.feedback);
        for (int i = 0; i < ds.feedback.Count; i++)
        {
            feedbackList.Add(new Feedback(ds.feedback[i].feedback, ds.feedback[i].email, ds.feedback[i].date));
        }
        return feedbackList;
    }

    /// <summary>
    /// Splitst een campus entry in verschillende campussen (als er meerdere aanwezig zijn).            EDIT
    /// </summary>
    /// <param name="campusString">de campus entry (formaat: 'RSS' of 'RSS SJS', ...)</param>
    /// <returns>Lijst van campussen</returns>
    

    /// <summary>
    /// Schrijft een feedback entry in de databank 
    /// </summary>
    /// <param name="feedback">een feedback entry object</param>
    public void WriteFeedback(Feedback feedback)
    {
        ds.feedback.Clear();

        DatasetFoodserverV3.feedbackRow feedbackRow = ds.feedback.NewfeedbackRow();
        feedbackRow.feedback = feedback.FeedbackText;
        feedbackRow.email = feedback.Email;
        feedbackRow.date = feedback.Date;
        ds.feedback.Rows.Add(feedbackRow);
        taFeedback.Update(ds.feedback);
    }

    /// <summary>
    /// Schrijft een log entry in de databank
    /// </summary>
    /// <param name="log">een log entry object</param>
    public void WriteLog(Userlog log)
    {
        ds.userlog.Clear();
        //herschrijven
        DatasetFoodserverV3.userlogRow logRow = ds.userlog.NewuserlogRow();
        logRow.date = log.LogTime;
        logRow.user = log.AffectedUser;
        logRow._event = log.Events;
        logRow.done_by = log.ExecutedBy;
        logRow.amount = log.Amount;

        ds.userlog.Rows.Add(logRow);

        taLogs.Update(ds.userlog);
    }

    public List<Userlog> GetUserLog()
    {
        List<Userlog> userlog = new List<Userlog>();
        taLogs.Fill(ds.userlog);
        for (int i = 0; i < ds.userlog.Count; i++)
        {
            userlog.Add(new Userlog(ds.userlog[i].date, ds.userlog[i].user, ds.userlog[i]._event, ds.userlog[i].done_by, ds.userlog[i].amount));
        }
        return userlog;
    }

    public List<Userlog> GetUserLogForUser(string email)
    {
        List<Userlog> userlog = new List<Userlog>();
        taLogs.FillByUser(ds.userlog, email);
        for (int i = 0; i < ds.userlog.Count; i++)
        {
            userlog.Add(new Userlog(ds.userlog[i].date, ds.userlog[i].user, ds.userlog[i]._event, ds.userlog[i].done_by, ds.userlog[i].amount));
        }
        return userlog;
    }

    //public void WriteTransaction(TransactionLog log) {
    //    ds.transactionlog.Clear();
    //    //herschrijven
    //    DatasetFoodserverV3.transactionlogRow logRow = ds.transactionlog.NewtransactionlogRow();
    //    logRow.date = log.Date;
    //    logRow.user = log.User;
    //    logRow.order = log.Order;
    //    logRow.wallet = log.Wallet;

    //    ds.transactionlog.Rows.Add(logRow);

    //    taTransLog.Update(ds.transactionlog);
    //}

    //public List<TransactionLog> GetTransactionLog() {
    //    List<TransactionLog> transLog = new List<TransactionLog>();
    //    taTransLog.Fill(ds.transactionlog);
    //    for (int i = 0; i < ds.transactionlog.Count; i++)
    //    {
    //        transLog.Add(new TransactionLog(ds.transactionlog[i].date, ds.transactionlog[i].user, ds.transactionlog[i].order, ds.transactionlog[i].wallet));
    //    }
    //    return transLog;
    //}

    //public List<TransactionLog> GetTransactionLogForUser(string email)
    //{
    //    List<TransactionLog> transLog = new List<TransactionLog>();
    //    taTransLog.FillByUser(ds.transactionlog, email);
    //    for (int i = 0; i < ds.transactionlog.Count; i++)
    //    {
    //        transLog.Add(new TransactionLog(ds.transactionlog[i].date, ds.transactionlog[i].user, ds.transactionlog[i].order, ds.transactionlog[i].wallet));
    //    }
    //    return transLog;
    //}
    /// <summary>
    /// Controleert als een IPN_ID reeds verwerkt is 
    /// </summary>
    /// <param name="Ipn_ID">De txn_id uit het IPN bericht</param>
    /// <returns></returns>
    public bool IsPaypalIpnProcessed(int Ipn_ID)
    {
        //Heeft paypalipn een string of int nodig?
        if (taPaypalIpn.CountIpn(ds.Paypal_IPN, Ipn_ID.ToString()) >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Registreert de IPN ID als verwerkt
    /// </summary>
    /// <param name="Ipn_ID">De txn_id uit het IPN bericht</param>
    /// <returns></returns>
    public void InsertIpnId(int Ipn_ID)
    {
        //Heeft paypalipn een string of int nodig?
        taPaypalIpn.Insert_IpnID(Ipn_ID.ToString());
    }

    #endregion

    #region UserMethods

    /// <summary>
    /// Voegt euro toe aan een wallet van een user
    /// </summary>
    /// <param name="amount">Aantal euro</param>
    /// <param name="email">email van de gebruiker</param>
    public void addEuro(decimal amount, string email)
    {
        eWallet wallet = GetUserEWallet(email);
        decimal newammaount = wallet.Euro + amount;
        taWallet.UpdateEuro(newammaount, email);
    }

    /// <summary>
    /// Kijkt of de user in de mailinglist zit
    /// </summary>
    /// <param name="email">De email van de user</param>
    /// <returns></returns>
    public bool CheckIfUserInMailinglist(string email)
    {
        ds.mailinglist.Clear();
        taMailinglist.FillByMail(ds.mailinglist, email);
        if (ds.mailinglist.Count != 0)
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Verwijdert de user uit de mailinglist
    /// </summary>
    /// <param name="email">De email van de user</param>
    public void RemoveUserFromMailinglist(string email)
    {
        if (CheckIfUserInMailinglist(email))
        {
            taMailinglist.DeleteQuery(email);
        }
    }

    /// <summary>
    /// Voegt de user toe aan de mailinglist
    /// </summary>
    /// <param name="email">De email van de user</param>
    public void AddUserToMailinglist(string email)
    {
        if (!CheckIfUserInMailinglist(email))
        {
            taMailinglist.InsertQuery(email);
        }
    }

    /// <summary>
    /// Geeft de functie van de user terug
    /// </summary>
    /// <param name="email">email van de user</param>
    /// <returns>Een Employment object</returns>
    public Employment GetUsersEmployment(string email)
    {
#if DEBUG
        return Employment.Admin;
#else
        if (GetAdmins().Contains(email))
        {
            return Employment.Admin;
        }
        else
        {
            if (GetKitchenStaff().Contains(email))
            {
                return Employment.Kitchen;
            }
            else return Employment.User;
        }
#endif
        

    }

    /// <summary>
    /// Annuleert een deelbestelling in de hoofdbestelling
    /// </summary>
    /// <param name="odid">ID van een deelbestelling in de hoofdbestelling</param>
    /// <param name="email">email van de user</param>
    public void CancelOrderDetail(int odid, string email)
    {
        decimal ammount = (decimal)taOrderDetails.FillOrderDetailsRefundPriceById(ds.order_details, odid);
        taOrderDetailsOptions.DeleteOrderDetailOption(odid);
        taOrderDetails.DeleteOrderDetail(odid);
        taWallet.AddToWallet(ammount, email);
    }

    /// <summary>
    /// Annuleert de hoofdbestelling van de user
    /// </summary>
    /// <param name="oid">ID van de hoofdbestelling</param>
    /// <param name="odid">ID van een deelbestelling in de hoofdbestelling</param>
    /// <param name="email">email van de user</param>
    public void CancelOrder(int oid, int odid, string email)
    {
        try
        {
            CancelOrderDetail(odid, email);
            taOrders.DeleteOrder(oid);
        }
        catch (Exception e)
        {
            e.ToString();
        }
    }

    /// <summary>
    /// Betaalt de order 
    /// </summary>
    /// <param name="amount">Het te betalen bedrag</param>
    /// <param name="email">Email van de gebruiker</param>
    public void PayOrder(decimal amount, string email)
    {
        eWallet wallet = GetUserEWallet(email);
        decimal newammount = wallet.Euro - amount;
        taWallet.UpdateEuro(newammount, email);
    }

    /// <summary>
    /// Geeft de wallet terug van de user
    /// </summary>
    /// <param name="email">email van de user</param>
    /// <returns>Een eWallet object</returns>
    public eWallet GetUserEWallet(string email)
    {
        //Dit wordt later vervangen door Comp Coins - NIET DUS
        taWallet.FillByEmail(ds.wallet, email);
       
#if DEBUG
        if (ds.wallet.Count != 0)
        {
            return new eWallet(ds.wallet[0].euro, ds.wallet[0].howesters);
        }
        else
        {
            //NewDatasetFoodserver.walletRow newWallet = ds.wallet.NewwalletRow();
            DatasetFoodserverV3.walletRow newWallet = ds.wallet.NewwalletRow();
            newWallet.email = email;
            newWallet.euro = 0;
            newWallet.howesters = 0;
            ds.wallet.Rows.Add(newWallet);
            taWallet.Update(ds.wallet);
            return new eWallet(0, 0);
        }
#else
        return new eWallet(ds.wallet[0].euro, ds.wallet[0].howesters);
#endif
    }

    /// <summary>
    /// Verandert employment van User 
    /// </summary>
    /// /// <param name="u">User</param>
    /// <param name="e">Naam van employment</param>
    public void ChangeEmployment(User u, Employment e)
    {
        switch (e)
        {
            case Employment.Admin:
                if (taAdmins.FillByEmail(ds.admins, u.Email) == 0)
                {
                    taAdmins.AddAdmin(u.Email);
                    if (taKitchenStaff.FillByEmail(ds.kitchenstaff, u.Email) != 0)
                    {
                        taKitchenStaff.DeleteKitchenStaff(u.Email);
                    }
                }
                taAdmins.AddAdmin(u.Email);
                break;
            case Employment.Kitchen:
                if (taKitchenStaff.FillByEmail(ds.kitchenstaff, u.Email) == 0)
                {
                    taKitchenStaff.AddKitchenStaff(u.Email);
                    if (taAdmins.FillByEmail(ds.admins, u.Email) != 0)
                    {
                        taAdmins.DeleteAdmin(u.Email);
                    }
                }
                break;
            case Employment.User:
                if (taKitchenStaff.FillByEmail(ds.kitchenstaff, u.Email) != 0)
                {
                    taKitchenStaff.DeleteKitchenStaff(u.Email);
                }
                if (taAdmins.FillByEmail(ds.admins, u.Email) != 0)
                {
                    taAdmins.DeleteAdmin(u.Email);
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Verandert de status van de Feedbackmail 
    /// </summary>
    /// /// <param name="u">User</param>
    /// <param name="e">Status van feedbackmail</param>
    public void ChangeFeedBackMail(User user, bool b)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Schrijft een hoofdbestelling samen met zijn deelbestellingen in de databank                        EDIT
    /// </summary>
    /// <param name="orderWithDetails">Object OrderWithDetails (hoofdbestelling met een lijst deelbestellingen)</param>
    public void AddOrderWithDetails(OrderWithDetails orderWithDetails)
    {
        ds.orders.Clear();
        ds.order_details.Clear();
        ds.order_details_option.Clear();
        //Order toevoegen
        int orderid = (int)(taOrders.InsertQuery(orderWithDetails.Order.Email, orderWithDetails.Order.DeliveryDate, orderWithDetails.Order.OrderDate, orderWithDetails.Order.Campus));

        //foreach (OrderDetail od in orderWithDetails.OrderDetails.Values)
        foreach (Cart od in orderWithDetails.OrderDetails)
        {
            //OrderDetail toevoegen
            //int orderDetailsid = (int)(taOrderDetails.InsertQuery(orderid, od.MealId, od.Quantity, od.Price, od.Comment));
            int orderDetailsid = (int)(taOrderDetails.InsertQuery(orderid, od.OrderDetail.MealId, od.OrderDetail.Quantity, od.OrderDetail.Price, od.OrderDetail.Comment));
            //foreach (Option opt in od.Options)
            foreach (Option opt in od.OrderDetail.Options)
            {
                taOrderDetailsOptions.InsertQuery(orderDetailsid, opt.Optionid, opt.Price);
            }
        }       
    }
    #endregion

    #region AdminMethods
    /// <summary>
    /// Linkt een maaltijd met een optie
    /// </summary>
    /// <param name="mealid">ID van de maaltijd</param>
    /// <param name="optionid">ID van de optie</param>
    public void insertMeal_options(int mealid, int optionid)
    {
        taMealOptions.InsertQuery(mealid, optionid);
    }

    public List<Meal> GetVisibleMealsByCampus(string campus)
    {
        taMeals.FillByCampusAndVisible(ds.meals, campus);
        List<Meal> mealList = new List<Meal>();
        for (int i = 0; i < ds.meals.Count; i++)
            mealList.Add(new Meal(ds.meals[i].mealid, ds.meals[i].mealname, ds.meals[i].price, ds.meals[i].enabled, GetCampussenByMealId(ds.meals[i].mealid))); ;
        return mealList;
    }


    public List<Option> GetVisibleOptionsByCampus(string campus)
    {
        taOptions.FillByCampusAndVisible(ds.options, campus);
        List<Option> optionList = new List<Option>();
        for (int i = 0; i < ds.options.Count; i++)
            optionList.Add(new Option(ds.options[i].optionid, ds.options[i].optionname, ds.options[i].enabled, ds.options[i].price));

        return optionList;
    }


    /// <summary>
    /// Verwijdert een link tussen een maaltijd en een optie
    /// </summary>
    /// <param name="mealid">ID van de maaltijd</param>
    /// <param name="optionid">ID van de optie</param>
    public void deleteMeal_options(int mealid, int optionid)
    {
        taMealOptions.DeleteQuery(mealid, optionid);
    }

    /// <summary>
    /// Verwijdert een link tussen een maaltijd en een optie
    /// </summary>
    /// <param name="optionid">ID van de optie</param>
    public void deleteLinkByOptionId(int optionid)
    {
        taMealOptions.DeleteLinkByOptionId(optionid);
    }

    /// <summary>
    /// Verwijdert een link tussen een maaltijd en alle opties die gelinkt staan met die maaltijd
    /// </summary>
    /// <param name="mealid">ID van de maaltijd</param>
    public void deleteLinkByMealId(int mealid)
    {
        taMealOptions.DeleteLinkByMealId(mealid);
    }

    /// <summary>
    /// Verwijdert een maaltijd uit de databank
    /// </summary>
    /// <param name="mealid">ID van de maaltijd</param>
    public void deleteMeal(int mealid)
    {
        taMeals.DeleteMealByMealId(mealid);
    }

    public void DeleteVisibleLinkWithMeal(int mealid)
    {
        taMealVisible.DeleteVisibleLinkMealByMealID(mealid);
    }

    public void DeleteVisibleLinkWithOption(int optionid)
    {
        taOptionVisible.DeleteVisibleLinkOptionByOptionID(optionid);
    }
    /// <summary>
    /// Verwijdert een optie uit de databank
    /// </summary>
    /// <param name="optionid">ID van de optie</param>
    public void deleteOption(int optionid)
    {
        taOptions.DeleteOptionByOptionID(optionid);
    }

    /// <summary>
    /// Schrijft een nieuwe optie in de databank                                        EDIT
    /// </summary>
    /// <param name="optionname">Naam van de optie</param>
    /// <param name="enabled">Beschikbaarheid van de optie</param>
    /// <param name="price">Prijs van de optie</param>
    /// <param name="campus">Campus(sen) waar de optie beschikbaar is</param>
    public void insertOptions(string optionname, bool enabled, decimal price, List<string> campussen)
    {
        taOptions.InsertQuery(optionname, enabled, price);
        AddOptionToCampus(GetLatestOption(), campussen);
        taOptionVisible.SetNewOptionVisible(GetLatestOption());
    }

    public void insertMeal(string mealname, decimal price, bool enabled, List<string> campusList)
    {
        taMeals.InsertQuery(mealname, price, enabled);
        int mealId = GetLatestMeal();
        addMealToCampussen(mealId, campusList);
        taMealVisible.SetNewMealVisible(mealId);  
    }

    public void addMealToCampussen(int mealId, List<string> campusList)
    {
        foreach (string campus in campusList)
        {
            if (!CheckAvailabilityInCampus(mealId, campus))
            {
                taMealCampus.AddMealToCampus(campus, mealId);
            }
        }
    }

    public void DeleteMealCampusLink(int mealId, List<string> listCampus)
    {
        foreach (string campus in listCampus)
        {
            if (CheckAvailabilityInCampus(mealId, campus))
            {
                taMealCampus.DeleteMealCampusLink(campus, mealId);
            }
        }
    }

    public void updateMeal(int mealid, string mealname, decimal price, bool enabled, List<string> campusList)
    {
      taMeals.UpdateQuery(mealname, price, enabled, mealid);
      UpdateMealCampussen(mealid, campusList);
    }

    public void UpdateMealCampussen(int mealId, List<string> campusList)
    {
        foreach (string campus in campusList)
        {
            if (!CheckAvailabilityInCampus(mealId, campus))
                taMealCampus.AddMealToCampus(campus, mealId);
        }
    }

    public void updateOption(int optionid, string optionname, bool enabled, decimal price, List<string> campussen)
    {
      taOptions.UpdateQuery(optionname, enabled, price, optionid);
        AddOptionToCampus(optionid, campussen);
    }

    public void AddOptionToCampus(int optionid, List<string> campusList)
    {
        foreach (string campus in campusList)
        {
            if (!CheckOptionAvailabilityInCampus(optionid, campus))
            {
                taOptionCampus.AddOptionToCampus(campus, optionid);
            }
        }
    }

    public void SetMealInivisble(int mealid)
    {
        taMealVisible.SetMealInvisible(mealid);
    }

    public void setOptionInvisible(int optionid)
    {
        taOptionVisible.SetOptionInvisible(optionid); 
    }

    public int GetLatestMeal()
    {
        taMeals.FillByLatestMeal(ds.meals);
        return ds.meals[0].mealid;
    }

    public int GetLatestOption()
    {
        taOptions.FillByLatestOption(ds.options);
        int optionid = 0;

        for (int i = 0; i < ds.options.Count; i++)
            optionid = ds.options[i].optionid;

        return optionid;
    }

    public WeeklyMenu GetMenu(DateTime d, string campus)
    {
        taWeeklyMenu.FillByDate(ds.WeeklyMenu, d.Date.ToShortDateString(), campus);
        if (ds.WeeklyMenu.Rows.Count != 0)
        {
            try { return new WeeklyMenu(ds.WeeklyMenu[0].Date, ds.WeeklyMenu[0].Meal, ds.WeeklyMenu[0].Campus); }
            catch
            {
                return new WeeklyMenu(ds.WeeklyMenu[0].Date, "", ds.WeeklyMenu[0].Campus);
            }
            
        }
        else
        {
            return new WeeklyMenu(d, "", "");
        }
    }

    public void InsertMenu(DateTime d, string meal, string c)
    {
        taWeeklyMenu.FillByDate(ds.WeeklyMenu, d.Date.ToShortDateString(), c);
        if (ds.WeeklyMenu.Rows.Count != 0)
        {
            taWeeklyMenu.UpdateMenu(d.Date.ToShortDateString(), meal, c);
        }
        else
        {
            taWeeklyMenu.InsertMenu(d.Date.ToShortDateString(), meal, c);
        }
    }
    public List<string> GetCampus()
    {
        taCampus.Fill(ds.Campus);
        List<string> campussen = new List<string>();
        for (int i = 0; i < ds.Campus.Count; i++)
        {
            campussen.Add(ds.Campus[i].CampusID);
        }
        return campussen;
    }

    public List<Campus> GetCampusList()
    {
        taCampus.Fill(ds.Campus);
        List<Campus> campussen = new List<Campus>();
        for (int i = 0; i < ds.Campus.Count; i++)
        {
            campussen.Add(new Campus(ds.Campus[i].CampusID, ds.Campus[i].CampusName));
        }
        return campussen;
    }

    public string getMotd()
    {
        taMotd.Fill(ds.motd);
        return ds.motd[0].motd.ToString();
    }
    #endregion
    #endregion

    public string GetCampusByName(string campusid)
    {
        taCampus.FillByCampusName(ds.Campus, campusid);
        return ds.Campus[0].CampusName;
    }

    public List<string> GetCampussenByMealId(int mealId)
    {
        List<string> listCampus = new List<string>();
        taMealCampus.FillByCampusMealId(ds.meal_Campus, mealId);

        for (int i = 0; i < ds.meal_Campus.Count; i++)
        {
            listCampus.Add(ds.meal_Campus[i].CampusID);
        }

        return listCampus;
    }

    private bool CheckAvailabilityInCampus(int mealId, string campus)
    {
        List<string> listCampus = Database.DatabaseInstance.GetCampussenByMealId(mealId);
        foreach (string cs in listCampus)
        {
            if (cs == campus)
            {
                return true;
            }
        }
        return false;
    }

    public void CreateEmptyWeeklyMenu(DateTime date)
    {


        foreach (Campus campus in GetCampusList())
        {
            
                DateTime startOfWeek = getForstDateTimeOfWeek(date);
                for (int i = 0; i < 5; i++)
                {
                    
                    taWeeklyMenu.InsertMenu(startOfWeek.AddDays(i).ToShortDateString(), "", campus.Id);
                }

                      
                        
        }

    }

    public bool weeklyMenuExists(DateTime date)
    {
        int menuCount = (int)taWeeklyMenu.WeeklyMenuCountForDate(date);
        if ((int)taWeeklyMenu.WeeklyMenuCountForDate(date) == (5*taCampus.getCampusCount() ))
        {
            return true;
        }
        return false;
    }

    private DateTime getForstDateTimeOfWeek(DateTime date)
    {
        System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
        DayOfWeek firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        DayOfWeek today = date.DayOfWeek;
        return date.AddDays(-(today - firstDayOfWeek)).Date;
    }


    private bool CheckOptionAvailabilityInCampus(int optionid, string campus)
    {
        List<string> listCampus = Database.DatabaseInstance.GetCampussenByOptionid(optionid);
        foreach (string cs in listCampus)
        {
            if (cs == campus)
            {
                return true;
            }
        }
        return false;
    }

    public List<string> GetCampussenByOptionid(int optionid)
    {
        List<string> listCampus = new List<string>();
        taCampus.FillCampusByOptionid(ds.Campus, optionid);
        for (int i = 0; i < ds.Campus.Count; i++)
        {
            listCampus.Add(ds.Campus[i].CampusID);
        }

        return listCampus;
    }

    public Meal GetMealByMealname(string mealname)
    {
        Meal meal = new Meal("", 0M, true);
        taMeals.FillMealsByMealname(ds.meals, mealname);
        for (int i = 0; i < ds.meals.Count; i++)
        {
            meal = new Meal(
                ds.meals[i].mealid,
                ds.meals[i].mealname,
                ds.meals[i].price,
                ds.meals[i].enabled
                );
        }
        return meal;
    }

    public List<Meal> GetMealsForOption(int optionid, string campus)
    {
        List<Meal> listMeals = new List<Meal>();
        taMeals.FillMealsByOptionid(ds.meals, optionid, campus);
        for (int i = 0; i < ds.meals.Count; i++)
        {
            listMeals.Add(new Meal(
                ds.meals[i].mealid,
                ds.meals[i].mealname,
                ds.meals[i].price,
                ds.meals[i].enabled
                ));
        }

        return listMeals;
    }

    public void DeleteOptionCampusLink(int optionId, List<string> unselectedCampus)
    {
        foreach (string campus in unselectedCampus)
        {
            if (CheckOptionAvailabilityInCampus(optionId, campus))
            {
                taOptionCampus.DeleteCampusByOption(campus, optionId);
            }
        }
    }

    public void CopyWeeklyMenu(DateTime date, Campus campusFrom, List<Campus> campusListTo) {
        DateTime startOfWeek = getForstDateTimeOfWeek(date);

        for (int i = 0; i < 5; i++)
        {
            foreach (Campus campus in campusListTo)
            {
                taWeeklyMenu.FillByDate(ds.WeeklyMenu, startOfWeek.AddDays(i).ToShortDateString(), campusFrom.Id);

                try
                {
                    taWeeklyMenu.UpdateMenu(startOfWeek.AddDays(i).ToShortDateString(), ds.WeeklyMenu[0].Meal, campus.Id);
                }
                catch (Exception e) {
                    taWeeklyMenu.UpdateMenu(startOfWeek.AddDays(i).ToShortDateString(), "", campus.Id);
                }
            }
        }
    }
}
