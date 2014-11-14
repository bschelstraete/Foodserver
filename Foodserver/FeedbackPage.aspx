<%@ Page Title="" Language="C#" MasterPageFile="~/Foodserver.master" AutoEventWireup="true" CodeFile="FeedbackPage.aspx.cs" Inherits="FeedbackPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_MainContent" runat="Server">

    <div class="col-md-9 col-md-pull-3">
        <div class="box">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Feedback</h3>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="GridViewFeedback" runat="server" AutoGenerateColumns="False" DataSourceID="DataSourceFeedback"
                        BackColor="White" Width="100%" BorderColor="#336666" BorderWidth="1px"
                        CellPadding="4" GridLines="Horizontal" BorderStyle="Double">
                        <Columns>
                            <asp:BoundField DataField="feedback" HeaderText="Feedback" SortExpression="feedback"></asp:BoundField>
                            <asp:BoundField DataField="email" HeaderText="Emailadres" SortExpression="email"></asp:BoundField>
                            <asp:BoundField DataField="date" HeaderText="Datum" SortExpression="date"></asp:BoundField>
                        </Columns>

                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#D0D0D0" Font-Bold="True" ForeColor="Black" Height="10px" HorizontalAlign="Left" />
                        <PagerStyle BackColor="#336666" ForeColor="White" />
                        <RowStyle BackColor="White" ForeColor="#333333" />

                        <EmptyDataTemplate>
                            Er is geen feedback.
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource runat="server" ID="DataSourceFeedback" ConnectionString='<%$ ConnectionStrings:FoodserverTestConnectionString %>'
                        SelectCommand="SELECT [feedback], [email], [date] FROM [feedback] ORDER BY [date] DESC"></asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

