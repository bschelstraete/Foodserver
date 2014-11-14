using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Foodserver : System.Web.UI.MasterPage
{
    private User loggedInUser;

    public User LoggedInUser { 
        get { return loggedInUser; } 
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["loggedin"] == null || !(bool)Session["loggedin"])
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
        {
            loggedInUser = Database.DatabaseInstance.GetUser(Session["user"].ToString());
            switch (Session["type"].ToString())
            {
                case "admin": LoadAdmin();
                    break;
                case "kitchenstaff": LoadKitchenStaff();
                    break;
                //case "user": LoadUser(); //useless case!
                //    break;
                default: LoadUser();
                    break;
            }
            lbl_LoggedInUser.Text = loggedInUser.WelcomeUser();
                
            //quick bugfix MOET NOG DEFTIG GEDAAN WORDEN
            if (Session["type"].ToString() == "admin")
            {
                dropdown.Visible = true;
                kitchen.Visible = true;
            }
            else
            {
                if (Session["type"].ToString() == "kitchenstaff")
                {
                    kitchen.Visible = true;
                }
                else {
                    dropdown.Visible = false;
                    kitchen.Visible = false;
                }
            }

            foreach (Campus campus in Database.DatabaseInstance.GetCampusList())
            {
                ddlChooseCampus.Items.Add(new ListItem(campus.Name, campus.Id));
            }

 
            LoadCampus();
        }
    }

    private void LoadCampus()
    {
        if (Session["campus"] != null)
        {
            string campus = Session["campus"].ToString();
            switch (campus)
            {
                case "RSS": ddlChooseCampus.SelectedIndex = 0;
                    break;
                case "SJS": ddlChooseCampus.SelectedIndex = 1;
                    break;
                default:
                    break;
            }
        }
        /*lbl_Campus.Text = "Campus: ";
        if (Session["campus"] != null)
        {
            string campus = Session["campus"].ToString();
            switch (campus)
            {
                case "RSS": lbl_Campus.Text += "Rijselstraat";
                    break;
                case "SJS": lbl_Campus.Text += "St. Jorisstraat";
                    break;
            }
            ddlChooseCampus.SelectedValue = campus;
        }*/
    }

    private void LoadUser()
    {
        //mMenu.Items.Add(new MenuItem("Maaltijd bestellen", "Eten bestellen", "", "Main.aspx"));
        //mMenu.Items.Add(new MenuItem("Mijn bestellingen", "Mijn bestellingen", "","MyOrders.aspx"));
        //Paypal menuitem
        //mMenu.Items.Add(new MenuItem("Krediet opladen", "Krediet opladen", "", "Payment.aspx"));
        lbl_Euros.Text = string.Format("{0:C} op je account",loggedInUser.getWalletAmount());
    }

    private void LoadKitchenStaff()
    {
        LoadUser();
        //mMenu.Items.Add(new MenuItem("Keuken", "Keuken", "", "Kitchen.aspx"));
    }

    private void LoadAdmin()
    {
        //Response.Redirect("UserAdministration.aspx");
        //Server.Transfer("UserAdministration.aspx");
        LoadKitchenStaff();
        //mMenu.Items.Add(new MenuItem("Administratie", "Administratie", ""));
        //mMenu.Items[3].ChildItems.Add(new MenuItem("Gebruiker administratie", "Gebruiker administratie", "", "UserAdministration.aspx"));
        //mMenu.Items[3].ChildItems.Add(new MenuItem("Maaltijden administratie", "Maaltijden administratie", "", "FoodAdministration.aspx"));
        //mMenu.Items[3].ChildItems.Add(new MenuItem("Feedback", "Feedback", "", "FeedbackPage.aspx"));
        //mMenu.Items[3].ChildItems.Add(new MenuItem("Errors", "Erros", "", "ErrorListPage.aspx"));
    }

    protected void LogOut(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Default.aspx");
    }

    /*protected void ChooseCampus(object sender, EventArgs e)
    {
        ddlChooseCampus.Visible = true;
        lblChooseCampus.Visible = true;
        btnChangeCampus.Visible = true;
        ddlChooseCampus.AutoPostBack = false;
        //lbtn_ChooseCampus.Visible = false;
    }*/

    /*protected void ChangeCampus(object sender, EventArgs e)
    {
        Session["campus"] = ddlChooseCampus.SelectedItem.Value;
        ddlChooseCampus.Visible = false;
        lblChooseCampus.Visible = false;
        btnChangeCampus.Visible = false;
        //lbtn_ChooseCampus.Visible = true;
        LoadCampus();
        Response.Redirect(Request.RawUrl);
    }*/

    protected void lbtn_Feedback_Click(object sender, EventArgs e)
    {
        if (txtFeedback.Text != "")
        {
            Feedback fb = new Feedback(txtFeedback.Text, Session["user"].ToString(), DateTime.Now);
            Database.DatabaseInstance.WriteFeedback(fb);
            Mailer mailer = new Mailer();
            mailer.SendFeedbackToAdmins(txtFeedback.Text);
            txtFeedback.Text = "";
            lblConfirm.Text = "De feedback is succesvol verzonden.";
        }        
    }
    protected void ddlChooseCampus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["campus"] = ddlChooseCampus.SelectedItem.Value;
        LoadCampus();
        Response.Redirect(Request.RawUrl);
    }
}
