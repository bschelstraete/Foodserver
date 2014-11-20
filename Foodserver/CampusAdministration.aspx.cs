using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CampusAdministration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtMotd.Attributes.Add("placeholder", Database.DatabaseInstance.getMotd());
    }

    protected void VoegCampusToe(object sender, EventArgs e) {
        Database.DatabaseInstance.CreateNewCampus(txtCampusId.Text, txtCampusNaam.Text);
        GridView1.DataBind();
    }

    protected void changeMotd(object sender, EventArgs e) {
        Database.DatabaseInstance.UpdateMotd(txtMotd.Text);
    }
}