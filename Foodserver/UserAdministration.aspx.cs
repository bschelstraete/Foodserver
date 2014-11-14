using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserAdministration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request["__EVENTARGUMENT"] == "doubleclick")
        {
            loadUserInformation(listbox_users.SelectedItem.Text);
            divGeneral.Visible = false;
            divUser.Visible = true;
        }
        listbox_users.Attributes.Add("ondblclick", ClientScript.GetPostBackEventReference(listbox_users, "doubleclick"));

        if (!IsPostBack)
        {
            //txt_Amount.Visible = false;
            //btn_AddCredits.Visible = false;
            //lbl_CurrentCredit.Visible = false;
            divGeneral.Visible = true;
            divUser.Visible = false;
        }
        if ((string)Session["type"] != "admin")
        {
            Response.Write("<script type='text/javascript'>alert('U hebt geen toegang tot deze pagina.');</script>");
            System.Threading.Thread.Sleep(2000);
            //Response.Redirect("Main.aspx");
            Server.Transfer("Main.aspx");
        }
    }

    private void loadUserInformation(string p)
    {
        User u = Database.DatabaseInstance.GetUser(p);
        foreach (Employment e in Enum.GetValues(typeof(Employment)))
        {
            ddlEmployment.Items.Add(new ListItem(Enum.GetName(typeof(Employment), e), e.ToString()));
        }
        lbl_Email.Text = u.Email;
        lbl_CurrentCredit.Text = u.Mywallet.Euro.ToString("C");
        ddlEmployment.SelectedValue = u.Employment.ToString();
        if (u.Employment == Employment.Admin)
        {
            divFeedback.Visible = true;
        }
        else
        {
            divFeedback.Visible = false;
        }
    }

    protected void btn_Zoek_Click(object sender, EventArgs e)
    {
        List<User> list = new List<User>();
        try
        {
            list = Database.DatabaseInstance.findUsers(txt_Zoek.Text.Replace(" ", "."));
            //User user = new User("aaron.caenepeel@student.howest.be", new eWallet(1000, 1000), "Aaron", "Caenepeel", Employment.Admin);
            //list.Add(user);
            lbl_Error.Visible = false;
        }
        catch
        {
          lbl_Error.Visible = true;
          lbl_Error.Text = "Te veel resultaten, gelieve uw zoekterm te verfijnen.";
        }
        listbox_users.Items.Clear();

        foreach (User user in list)
        {
            listbox_users.Items.Add(user.Email);
        }
        //SetFormVisible();
    }

    private void SetFormVisible()
    {
        txt_Amount.Visible = true;
        btn_AddCredits.Visible = true;
        lbl_CurrentCredit.Visible = true;
        listbox_users.Visible = true;
    }

    protected void btn_AddCredits_Click(object sender, EventArgs e)
    {
        try
        {
            //lbl_Error.Visible = false;
            User user = Database.DatabaseInstance.GetUser(lbl_Email.Text);
            string result = Regex.Replace(txt_Amount.Text, "[.]", ",");
            result = Regex.Replace(result, " ", "");
            user.ReloadWallet(Decimal.Parse(result), Session["user"].ToString());
            lbl_CurrentCredit.Text = String.Format("{0:C}", Database.DatabaseInstance.GetUserEWallet(listbox_users.SelectedItem.Text.ToString()).Euro);
            txt_Amount.Text = "";
        }
        catch (Exception ex)
        {
            ex.ToString();
            //lbl_Error.Visible = true;
            //lbl_Error.Text = "Gelieve een correcte waarde op te geven";
        }
    }

    //protected void listbox_users_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    lbl_CurrentCredit.Text = String.Format("Huidige balans: {0:C}", Database.DatabaseInstance.GetUserEWallet(listbox_users.SelectedItem.Text.ToString()).Euro);
    //}

    protected void btnChangeEmployment_Click(object sender, EventArgs e)
    {
        User user = Database.DatabaseInstance.GetUser(lbl_Email.Text);
        user.ChangeEmployment(user, (Employment) Enum.Parse(typeof(Employment), ddlEmployment.SelectedValue));
    }

    protected void chkFeedback_CheckedChanged(object sender, EventArgs e)
    {
        User user = Database.DatabaseInstance.GetUser(lbl_Email.Text);
        if (chkFeedback.Checked)
        {
            user.ChangeFeedbackMail(user, true);
        }
        else
        {
            user.ChangeFeedbackMail(user, false);
        }
    }
}