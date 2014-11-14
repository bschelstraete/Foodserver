using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Paypal_IPN : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Post back to either sandbox or liv
        string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
       // string strLive = "https://www.paypal.com/cgi-bin/webscr";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strSandbox);
        //Set values for the request back
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        byte[] param = Request.BinaryRead(HttpContext.Current.Request.ContentLength);
        string strRequest = Encoding.ASCII.GetString(param);
        string strResponse_copy = strRequest;  //Save a copy of the initial info sent by PayPal
        NameValueCollection form = HttpUtility.ParseQueryString(strResponse_copy);
        strRequest += "&cmd=_notify-validate";



        //File.AppendAllText(@"paypal.txt", "New Post: "+Environment.NewLine);

        foreach (String parameter in form) {
            //File.AppendAllText(@"paypal.txt", "parameter : " + parameter +" = " + form.Get(parameter) + Environment.NewLine);

        }

        //Send the request to PayPal and get the response
        StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
        streamOut.Write(strRequest);
        streamOut.Close();
        StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
        string strResponse = streamIn.ReadToEnd();
        streamIn.Close();
        string pay_stat = form["payment_status"];
        //File.AppendAllText(@"paypal.txt","Response = "+strResponse+Environment.NewLine);
        //File.AppendAllText(@"paypal.txt", "Payment status =  " + pay_stat + Environment.NewLine);

        if (strResponse == "VERIFIED")
        {
            //check the payment_status is Completed
            //check that txn_id has not been previously processed
            //check that receiver_email is your Primary PayPal email
            //check that payment_amount/payment_currency are correct
            //process payment

            // pull the values passed on the initial message from PayPal
            //File.AppendAllText(@"paypal.txt", "VERIFIED" + Environment.NewLine);

            
            string user_email = form["payer_email"];
            
            //.
            //.  more args as needed look at the list from paypal IPN doc
            //.


            if (pay_stat.Equals("Completed"))
            {

                //File.AppendAllText(@"paypal.txt", "VERIFIED+COMPLETED" + Environment.NewLine);
                if (Database.DatabaseInstance.IsPaypalIpnProcessed(int.Parse(form["txn_id"])) == false)
                {
                    Database.DatabaseInstance.InsertIpnId(int.Parse(form["txn_id"]));
                    User user = Database.DatabaseInstance.GetUser(form["custom"]);
                    user.ReloadWallet(Decimal.Parse(form["mc_gross"]), form["custom"]);
                }
            }


            // more checks needed here specially your account number and related stuff
        }
        else if (strResponse == "INVALID")
        {
            //File.AppendAllText(@"paypal.txt", "INVALID!!!!" + Environment.NewLine);
        }
        else
        {
            //log response/ipn data for manual investigation
        }

    }
}