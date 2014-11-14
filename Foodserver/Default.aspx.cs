using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private string email;

    public string pp_username
    {
        get { return email; }
        set { value = email; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Database.DatabaseInstance.getMotd() != "")
        {
            motd.InnerText = Database.DatabaseInstance.getMotd();
        }
        else {
            motd.Visible = false;
        }
        if (!IsPostBack)
        {
            foreach (Campus campus in Database.DatabaseInstance.GetCampusList())
            {
                ddlCampus.Items.Add(new ListItem(campus.Name, campus.Id));
            }
        }
    }

    protected bool lgn_Login_Authenticate(object sender, AuthenticateEventArgs e)
    {
        email = UserName.Text;
        return (e.Authenticated = Database.DatabaseInstance.Login(UserName.Text, Password.Text));
    }

    protected void lgn_Login_LoggedIn(object sender, EventArgs e)
    {
        Session["campus"] = ddlCampus.SelectedItem.Value;
        Session["user"] = UserName.Text;
        Session["loggedin"] = true;
        if (Database.DatabaseInstance.GetAdmins().Contains(UserName.Text))
        {
            Session["type"] = "admin";
        }
        else
        {
            if (Database.DatabaseInstance.GetKitchenStaff().Contains(UserName.Text))
            {
                Session["type"] = "kitchenstaff";
            }
            else
            {
                Session["type"] = "user";
            }
        }
        Response.Redirect("~/Main.aspx");
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        if (lgn_Login_Authenticate(sender, new AuthenticateEventArgs()).Equals(true))
        {
            lgn_Login_LoggedIn(sender, EventArgs.Empty);
        }
    }
}