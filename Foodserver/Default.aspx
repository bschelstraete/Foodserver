<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <link href="CSS/Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Site.css" rel="stylesheet" type="text/css" />
    <link href="CSS/MediaLogin.css" rel="stylesheet" type="text/css" />
</head>
<body>
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <asp:Image ID="img_howest" ImageUrl="~/Images/Howest (T).png" Width="100%" Height="100%" runat="server" CssClass="img-responsive" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="alert alert-info" runat="server" id="motd"></div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-default" id="Login">
                        <div class="panel-heading" id="HeadLogin">Aanmelden op de Foodserver</div>
                    <div id="loginPanel" class="panel-body">
                        <form class="form-horizontal" role="form" runat="server">
                                <div class="form-group">
                                    <label for="Username" class="col-sm-3 control-label">Email</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="UserName" runat="server" Placeholder="Howest e-mail adres" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" Text="Verplicht" ForeColor="Red" Font-Size="10px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="Password" class="col-sm-3 control-label">Password</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" Placeholder="Paswoord" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" Text="Verplicht" ForeColor="Red" Font-Size="10px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="ddlCampus" id="Label1" class="col-sm-3 control-label" runat="server">Campus</label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlCampus" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:Button ID="Button1" CommandName="Login" runat="server" Text="Login" OnClick="Login_Click" CssClass="btn btn-info form-control"></asp:Button>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
            <div class="col-md-offset-2 col-md-8" id="loginInfo">
                <p>
                    <a href="MenuWeekly.aspx" target="_blank">Bekijk het weekmenu hier</a> |
                    <a href="About.aspx">Over deze applicatie</a>
                </p>
                </div>
            </div>
        </div>
</body>
</html>
