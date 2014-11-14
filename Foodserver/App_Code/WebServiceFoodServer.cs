using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for WebServiceFoodServer
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebServiceFoodServer : System.Web.Services.WebService {

    public WebServiceFoodServer () {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<User> findUsers(string naam, string token) {
        return Database.DatabaseInstance.findUsers(naam.Replace(" ", "."));
    }

    [WebMethod]
    public User getUser(string naam, string token)
    {
        return Database.DatabaseInstance.GetUser(naam);
    }

    [WebMethod]
    public void addCredits(User u, decimal c, string token)
    {
      //  u.ReloadWallet(c);
    }
    

}
