<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorListPage.aspx.cs" Inherits="ErrorListPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>Errors</h3>
        <a href="Main.aspx">Back</a>
        <asp:GridView ID="GridViewErrors" CssClass="table table-striped table-condensed " runat="server" AutoGenerateColumns="False" DataSourceID="DataSourceErrors" DataKeyNames="EventId">
            <Columns>
                <asp:BoundField DataField="EventId" HeaderText="EventId" ReadOnly="True" SortExpression="EventId" />
                <asp:BoundField DataField="EventTimeUtc" HeaderText="EventTimeUtc" SortExpression="EventTimeUtc" />
                <asp:BoundField DataField="EventTime" HeaderText="EventTime" SortExpression="EventTime" />
                <asp:BoundField DataField="EventType" HeaderText="EventType" SortExpression="EventType" />
                <asp:BoundField DataField="EventCode" HeaderText="EventCode" SortExpression="EventCode" />
                <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" />
                <asp:BoundField DataField="ApplicationPath" HeaderText="ApplicationPath" SortExpression="ApplicationPath" />
                <asp:BoundField DataField="MachineName" HeaderText="MachineName" SortExpression="MachineName" />
                <asp:BoundField DataField="RequestUrl" HeaderText="RequestUrl" SortExpression="RequestUrl" />
                <asp:BoundField DataField="ExceptionType" HeaderText="ExceptionType" SortExpression="ExceptionType" />
                <asp:BoundField DataField="Details" HeaderText="Details" SortExpression="Details" />
            </Columns>

            <HeaderStyle Height="10px" HorizontalAlign="Left" />

        </asp:GridView>
        <asp:SqlDataSource ID="DataSourceErrors" runat="server" ConnectionString="<%$ ConnectionStrings:FoodserverTestConnectionString %>" 
            SelectCommand="SELECT EventId, EventTimeUtc, EventTime, EventType, EventCode, Message, ApplicationPath, MachineName, RequestUrl, ExceptionType, Details FROM aspnet_WebEvent_Events ORDER BY EventTime DESC"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
