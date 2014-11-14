using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for Mailer
/// </summary>
public class Mailer
{

    SmtpClient smtpClient;

    public Mailer() {
        smtpClient = new SmtpClient();
        //configureer de smpt
        //Voor Howest
        smtpClient.Host = "smtpmailer.howest.be";
        smtpClient.Port = 25;
        smtpClient.Credentials = new NetworkCredential("", "");
    }

	public void SendOrderConfirmation(OrderWithDetails order, string session)
	{
        //Maak de nodige instanties
        
        MailMessage message = new MailMessage();
        
        //configureer de mail
        
        MailAddress fromAddress = new MailAddress("foodserver@howest.be", "Cafetaria " + order.Order.Campus);
        MailAddress toAddress = new MailAddress(session, "Student");
        message.From = fromAddress;
        message.To.Add(toAddress); //Recipent email 
        message.Subject = "Uw bestelling is geplaatst op campus " + order.Order.Campus + "!";
        message.Body = "Beste,\n\n";
        message.Body += "Uw bestelling is geplaatst en kan vanaf 12u opgehaald worden op " + order.Order.DeliveryDate.ToShortDateString() + " op campus " + order.Order.Campus + ".";
        message.Body += "\nU hebt het volgende besteld: ";
        message.Body += "\n\n";

        decimal optprice = 0;
        //foreach (OrderDetail od in order.OrderDetails.Values)
        foreach (Cart od in order.OrderDetails)
        {
            //message.Body += od.or.Quantity + " x " + Database.DatabaseInstance.GetMealName(od.MealId);
            message.Body += od.OrderDetail.Quantity + " x " + Database.DatabaseInstance.GetMealName(od.OrderDetail.MealId);
            //foreach (Option opt in od.Options)
            foreach (Option opt in od.OrderDetail.Options)
            {
                optprice += opt.Price;
                message.Body += " " + opt.Optionname;
            }
            //message.Body += " : EUR " + ((od.Price + optprice) * (decimal)od.Quantity) + "\n";
            message.Body += " : EUR " + ((od.OrderDetail.Price + optprice) * (decimal)od.OrderDetail.Quantity) + "\n";
        }
        message.Body += "\n\nEet smakelijk!";

        //zend de mail
        smtpClient.Send(message);
	}

    public void SendFeedbackToAdmins(string feedback) {

        List<MailAddress> admins = new List<MailAddress>();
        MailMessage message = new MailMessage();

        foreach(string admin in Database.DatabaseInstance.GetAdmins()){
            admins.Add(new MailAddress(admin));
        }

        //configureer de mail

        MailAddress fromAddress = new MailAddress("foodserver@howest.be", "Feedback");

        foreach (MailAddress adminEmail in admins) {
            //message.To.Add(adminEmail);
            message.To.Add(new MailAddress("niels.gunst@student.howest.be"));
        }
        
        message.From = fromAddress;
        message.Subject = "Feedback foodserver (automatische mail)";
        message.Body = "Geachte \nEr is nieuwe feedback binnengekomen voor de foodserver website:\n\n\n\n";
        message.Body += feedback;
        

        //zend de mail
        smtpClient.Send(message);
    }


}