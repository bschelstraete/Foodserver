using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HowestIdentity;
using AjaxControlToolkit;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class Main : System.Web.UI.Page
{
    #region Datamembers

    List<Meal> mealList;
    List<TableCell> cellList;
    ContentPlaceHolder cph;
    Calendar cld = new Calendar();
    DateTime deliveryDate;
    string campus;
    //WeeklyMenuHtml menu;
    //Bestelpanel
    CalendarExtender calex;
    NumericUpDownExtender numBread;
    Table mealTable;
    //Dictionary<int, OrderDetail> Cart;
    List<Cart> cart;
    int teller;

    #endregion

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            //Cart = (Dictionary<int, OrderDetail>)Session["Cart"];
            cart = (List<Cart>)Session["Cart"];
        }
        else
        {
            //Session["Cart"] = Cart;
            Session["Cart"] = cart;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        campus = Session["campus"].ToString();

        DateTime[] days = new DateTime[5];
        days = GetDays();

        for (int i = 1; i < 6; i++)
        {
            HtmlGenericControl span = (HtmlGenericControl)weekmenuDiv.FindControl("spanDag" + i);
            span.InnerHtml = days[i - 1].ToString("dd MMMM");

            HtmlGenericControl ul = (HtmlGenericControl)weekmenuDiv.FindControl("ulDag" + i);
            string[] weekmenu = Database.DatabaseInstance.GetMenu(days[i - 1], campus).ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string dag in weekmenu)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                li.InnerHtml = dag;
                ul.Controls.Add(li);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Initialiseer();
        DisplayMeals();

        if (Session["Cart"] != null && IsPostBack)
        {
            //Cart = (Dictionary<int, OrderDetail>)Session["Cart"];
            cart = (List<Cart>)Session["Cart"];
        }
        else
        {
            //Cart = new Dictionary<int, OrderDetail>();
            cart = new List<Cart>();
        }
        if (Session["teller"] != null)
        {
            teller = (int)Session["teller"];
        }
        else
        {
            teller = 0;
        }
        if (!IsPostBack)
        {
            ShowOptionsForMeal(this, EventArgs.Empty);
        }
    }
    private void Initialiseer()
    {
        //cph = (ContentPlaceHolder)this.Master.FindControl("cph_MainContent");

        ////mealList = Database.DatabaseInstance.GetMeals();
        //cellList = new List<TableCell>();

        //mealTable = new Table();
    }

    private void DisplayMeals()
    {
        BestelPanel();
        SelecteerMaaltijd();
    }

    private void BestelPanel()
    {
        campusLbl.Text = CampusText(Session["campus"].ToString());
        cph = (ContentPlaceHolder)this.Master.FindControl("cph_MainContent"); //is nodig

        calex = new CalendarExtender();
        calex.TargetControlID = deliveryDateText.ID;
        calex.CssClass = "calDel";
        if (!IsPostBack)
        {
            if (DateTime.Now.Hour < 10 || (DateTime.Now.Hour == 10 && DateTime.Now.Minute <= 30))
            {
                calex.StartDate = DateTime.Today;
                deliveryDateText.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
            else
            {
                deliveryDateText.Text = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");
                calex.StartDate = DateTime.Today.AddDays(1);
            }
        }
        calex.Format = "dd/MM/yyyy";
        calex.FirstDayOfWeek = FirstDayOfWeek.Monday;

    }

    private string CampusText(string campus)
    {
        return Database.DatabaseInstance.GetCampusByName(campus);
    }

    private void SelecteerMaaltijd()
    {
        cph.Controls.Add(calex);

        if (!IsPostBack)
        {
            string campus = Session["campus"].ToString();
            foreach (Meal meal in Database.DatabaseInstance.GetVisibleMealsByCampus(campus))
            {
                if (!IsPostBack)
                {
                    ddlMeals.Items.Add(new ListItem(meal.Mealname, meal.Mealid.ToString()));
                }
            }
            ddlMeals.AutoPostBack = true;
        }

        numBread = new NumericUpDownExtender() { TargetControlID = "numberOrderDetails", Width = 100, Minimum = 1, Maximum = 99, RefValues = "", ServiceDownMethod = "", ServiceUpMethod = "", TargetButtonDownID = "btnMinBread", TargetButtonUpID = "btnPlusBread" };

        TextBoxWatermarkExtender watermark = new TextBoxWatermarkExtender();

        watermark.WatermarkText = "(Vb: geen tomaat)";
        watermark.TargetControlID = "txtOpmerking";

        cph.Controls.Add(watermark);
        cph.Controls.Add(numBread);
    }

    public void ShowOptionsForMeal(object sender, EventArgs e)
    {
        ddlOptions.Items.Clear();
        string campus = Session["campus"].ToString();
        if (ddlMeals.SelectedIndex != -1)
        {
            foreach (Option option in Database.DatabaseInstance.GetOptionsForMeal(int.Parse(ddlMeals.SelectedItem.Value), campus))
            {
                if (option.Enabled)
                {
                    ddlOptions.Items.Add(new ListItem(option.Optionname, option.Optionid.ToString()));
                }
            }
        }
        else {
        ddlOptions.Items.Add(new ListItem("nog geen opties beschikbaar", "NA"));
        }
    }

    public void btnVoegToe_Click(object sender, EventArgs e)
    {
        btn_Bestel.Text = "Bestel winkelmandje";
        divWinkelmandje.Visible = true;

        if (Int32.Parse(numberOrderDetails.Text) != 0 && ddlOptions.SelectedValue != "")
        {
            List<Option> options = new List<Option>();
            Option option = Database.DatabaseInstance.GetOption(int.Parse(ddlOptions.SelectedItem.Value), Session["campus"].ToString());
            options.Add(option);

            OrderDetail order = new OrderDetail(int.Parse(ddlMeals.SelectedItem.Value), 0, int.Parse(numberOrderDetails.Text), txtOpmerking.Text, Database.DatabaseInstance.GetActualMealPrice(int.Parse(ddlMeals.SelectedItem.Value), Session["campus"].ToString()), options);
            //Cart.Add(teller, order);
            DateTime orderDate = Convert.ToDateTime(deliveryDateText.Text);
            cart.Add(new Cart(teller, order, Convert.ToDateTime(deliveryDateText.Text)));
            decimal totalprice = order.Quantity * (order.Price + option.Price);
            lbCart.Items.Add(new ListItem(ddlMeals.SelectedItem.Text + " " + ddlOptions.SelectedItem.Text + " " + order.Comment + " " + order.Quantity + " stuk(s)" + ": € " + totalprice, teller.ToString()));
            //Session["Cart"] = Cart;
            Session["Cart"] = cart;
            teller += 1;
            Session["teller"] = teller;
        }
        txtOpmerking.Text = "";
    }

    public void btnDeleteSandwiches_Click(object sender, EventArgs e)
    {
        if (lbCart.Items.Count != 0)
        {
            if (lbCart.SelectedItem != null)
            {

                //Cart.Remove(int.Parse(lbCart.SelectedItem.Value));
                cart.RemoveAt(Int32.Parse(lbCart.SelectedItem.Value));
                lbCart.Items.Remove(lbCart.SelectedItem);
                if (lbCart.Items.Count == 0)
                {
                    btn_Bestel.Text = "Bestel nu!";
                    divWinkelmandje.Visible = false;
                }
            }
        }
    }

    public void btn_Bestel_Click(object sender, EventArgs e)
    {
        try
        {
            deliveryDate = new DateTime(
            Convert.ToInt32(deliveryDateText.Text.Substring(6, 4)), //jaar
            Convert.ToInt32(deliveryDateText.Text.Substring(3, 2)), //maand
            Convert.ToInt32(deliveryDateText.Text.Substring(0, 2))  //dag
            );
            cld.SelectedDate = deliveryDate;
            TimeSpan minOrderHour = new TimeSpan(8, 00, 00);
            TimeSpan maxOrderHour = new TimeSpan(10, 30, 00);

            if ((cld.SelectedDate.DayOfWeek != DayOfWeek.Saturday && cld.SelectedDate.DayOfWeek != DayOfWeek.Sunday)
                && (DateTime.Now.Date < cld.SelectedDate.Date ||
                (DateTime.Now.Date == cld.SelectedDate.Date &&
                DateTime.Now.TimeOfDay <= maxOrderHour)))
            {
                Order order = new Order(Session["user"].ToString(), cld.SelectedDate.Date, Session["campus"].ToString());

                //OrderWithDetails orderWithDetails = new OrderWithDetails(order, Cart);
                OrderWithDetails orderWithDetails = new OrderWithDetails(order, cart);

                //if (Cart.Count == 0) {
                if (cart.Count == 0)
                {
                    btnVoegToe_Click(this, EventArgs.Empty);
                }

                if (Payment(orderWithDetails))
                {
                    if (orderWithDetails.OrderDetails.Count > 0)
                    {
                        string orderString = cld.SelectedDate.Date + " ; " + Session["campus"].ToString() + " ; ";
                        foreach (object item in lbCart.Items)
                        {
                            orderString += item.ToString() + " ; ";
                        }
                        //Database.DatabaseInstance.WriteTransaction(new TransactionLog(DateTime.Now, Session["user"].ToString(), orderString, Database.DatabaseInstance.GetUserEWallet(Session["user"].ToString()).Euro));
                        //OrderWithDetails newOrder = new OrderWithDetails(order, Cart);
                        Database.DatabaseInstance.AddOrderWithDetails(orderWithDetails);
                        //Database.DatabaseInstance.WriteTransaction(new TransactionLog(DateTime.Now, ));
                        if (Database.DatabaseInstance.CheckIfUserInMailinglist(Session["user"].ToString()))
                        {
                            SendConfirmationMail(orderWithDetails);
                        }
                        Response.Redirect("~/MyOrders.aspx");
                    }
                    else
                    {
                        lblError.Text = "Gelieve een maaltijd op te geven";
                    }
                }
                else
                {
                    lblError.Text = "Saldo ontoereikend";
                }
            }
            else
            {
                if (cld.SelectedDate.Date < DateTime.Now.Date)
                {
                    lblError.Text = "Je kan niet bestellen voor een datum in het verleden";
                }
                else
                {
                    if (cld.SelectedDate.DayOfWeek != DayOfWeek.Saturday && cld.SelectedDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        lblError.Text = "Bestellingen voor de geselecteerde dag zijn afgesloten. Gelieve een andere datum te selecteren.";
                    }
                    else
                    {
                        lblError.Text = "U kan niet bestellen in het weekend";
                    }
                }
                //Je kan enkel bestellen tussen 8.30 en 10.30
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
            lblError.Text = "Er is iets misgelopen maar uw order is wel geplaatst. U wordt doorverwezen naar uw bestellingen.";
            Response.Redirect("~/MyOrders.aspx");
        }
    }


    private Boolean Payment(OrderWithDetails owd)
    {
        Decimal price = 0;

        //foreach (OrderDetail od in owd.OrderDetails.Values)
        foreach (Cart od in owd.OrderDetails)
        {
            //foreach (Option opt in od..Options)
            foreach (Option opt in od.OrderDetail.Options)
            {
                //price += opt.Price * od.Quantity;
                price += opt.Price * od.OrderDetail.Quantity;
            }
            //price += od.Price * od.Quantity;
            price += od.OrderDetail.Price * od.OrderDetail.Quantity;
        }

        User user = Database.DatabaseInstance.GetUser(Session["user"].ToString());

        if (user.getWalletAmount() >= price)
        {
            user.PayOrder(price);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SendConfirmationMail(OrderWithDetails order)
    {
        string session = Session["user"].ToString();
        Mailer mailer = new Mailer();
        mailer.SendOrderConfirmation(order, session);
    }

    protected void lbtn_Feedback_Click(object sender, EventArgs e)
    {
        Feedback fb = new Feedback(txtFeedback.Text, Session["user"].ToString(), DateTime.Now);
        Database.DatabaseInstance.WriteFeedback(fb);
        txtFeedback.Text = "";
        lblConfirm.Text = "De feedback is succesvol verzonden.";
    }

    protected void lbtnDag_Click(object sender, EventArgs e)
    {
        string id = ((LinkButton)sender).ID;
        DateTime[] days = new DateTime[5];
        days = GetDays();

        switch (id)
        {
            case "lbtnDag1":
                if (!TestDate(days[0]))
                {
                    break;
                }
                deliveryDateText.Text = days[0].ToShortDateString();
                ddlMeals.SelectedIndex = 0;
                break;
            case "lbtnDag2":
                if (!TestDate(days[1]))
                {
                    break;
                }
                deliveryDateText.Text = days[1].ToShortDateString();
                ddlMeals.SelectedIndex = 0;
                break;
            case "lbtnDag3":
                if (!TestDate(days[2]))
                {
                    break;
                }
                deliveryDateText.Text = days[2].ToShortDateString();
                ddlMeals.SelectedIndex = 0;
                break;
            case "lbtnDag4":
                if (!TestDate(days[3]))
                {
                    break;
                }
                deliveryDateText.Text = days[3].ToShortDateString();
                ddlMeals.SelectedIndex = 0;
                break;
            case "lbtnDag5":
                if (!TestDate(days[4]))
                {
                    break;
                }

                deliveryDateText.Text = days[4].ToShortDateString();
                ddlMeals.SelectedIndex = 0;
                break;
            default:
                break;
        }
        ShowOptionsForMeal(this, EventArgs.Empty);
    }

    public bool TestDate(DateTime date)
    {
        TimeSpan tijd = new TimeSpan(10, 30, 0);
        if (date.Date < DateTime.Now.Date)
        {
            return false;
        }
        else if (date.Date == DateTime.Now.Date && date.TimeOfDay > tijd)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public DateTime[] GetDays()
    {
        int offset = 0;
        switch (DateTime.Now.DayOfWeek.ToString())
        {
            case "Monday":
                offset = 0;
                break;
            case "Tuesday":
                offset = -1;
                break;
            case "Wednesday":
                offset = -2;
                break;
            case "Thursday":
                offset = -3;
                break;
            case "Friday":
                offset = -4;
                break;
            case "Saturday":
                offset = 2;
                break;
            case "Sunday":
                offset = 1;
                break;
        }
        DateTime[] days = new DateTime[5];
        for (int i = 0; i < 5; i++)
        {
            days[i] = DateTime.Now.AddDays(i + offset); //days of the week
        }
        return days;
    }
}