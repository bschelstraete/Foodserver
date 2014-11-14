<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ErrorPage</title>
    <link href="CSS/Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6 col-md-offset-3 divError">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Error</h3>
                    </div>
                    <div class="panel-body">
                        <asp:Label ID="Lbl_FriendlyErrorMsg" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                        <p>
                            U kunt dit probleem proberen op te lossen door uw browser cache te legen, uitleg vindt u op <a href="http://www.browserchecker.nl/cache-bestanden-verwijderen">deze website</a>.<br />
                            Bij onverwachte wijzigingen aan uw krediet, gelieve contact op te nemen met Peter aan het onthaal. <br />
                            Onze excuses voor het ongemak.
                        </p>
                    </div>
                </div>   
            </div>
        </div>
    </div>
</form>
</body>
</html>
