using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Create safe error messages.
        string generalErrorMsg = "Er is een probleem ontdekt. De administrators zijn al op de hoogte gebracht.";

        // Display safe error message.
        Lbl_FriendlyErrorMsg.Text = generalErrorMsg;

    }
}