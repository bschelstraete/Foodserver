<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CampusAdministration.aspx.cs" MasterPageFile="~/Foodserver.master" Inherits="CampusAdministration" %>

<asp:Content ID="adminContent" ContentPlaceHolderID="cph_MainContent" runat="server">

    <div class="col-md-9 col-md-pull-3">
        <div class="box">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Campus Administratie</h3>
                </div>
                <div class="panel-body">


                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="CampusID" DataSourceID="SqlDataSource1" CssClass="table table-hover table-striped table-condensed">
                        <Columns>
                            <asp:BoundField DataField="CampusID" HeaderText="CampusID" ReadOnly="True" SortExpression="CampusID" />
                            <asp:BoundField DataField="CampusName" HeaderText="CampusName" SortExpression="CampusName" />
                        </Columns>
                    </asp:GridView>


                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FoodserverConnectionString %>" DeleteCommand="DELETE FROM [Campus] WHERE [CampusID] = @CampusID" InsertCommand="INSERT INTO [Campus] ([CampusID], [CampusName]) VALUES (@CampusID, @CampusName)" SelectCommand="SELECT [CampusID], [CampusName] FROM [Campus]" UpdateCommand="UPDATE [Campus] SET [CampusName] = @CampusName WHERE [CampusID] = @CampusID">
                        <DeleteParameters>
                            <asp:Parameter Name="CampusID" Type="String" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="CampusID" Type="String" />
                            <asp:Parameter Name="CampusName" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="CampusName" Type="String" />
                            <asp:Parameter Name="CampusID" Type="String" />
                        </UpdateParameters>
                    </asp:SqlDataSource>



                    <div class="form-group">
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtCampusId" runat="server" Text="" AutoPostBack="False" ClientIDMode="Inherit" CssClass="form-control" placeholder="CampusId"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtCampusNaam" runat="server" Text="" AutoPostBack="False" ClientIDMode="Inherit" CssClass="form-control" placeholder="CampusNaam"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:Button ID="btn_voegToe" runat="server" Text="Voeg Campus toe" CssClass="btn btn-info form-control" OnClick="VoegCampusToe" />
                        </div>
                        <asp:Label ID="lbl_Error" runat="server" ForeColor="Tomato"></asp:Label>
                    </div>




                    <div class="form-group">
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtMotd" runat="server" Text="" AutoPostBack="False" ClientIDMode="Inherit" CssClass="form-control" ></asp:TextBox>
                        </div>                        
                        <div class="col-sm-4">
                            <asp:Button ID="Button1" runat="server" Text="Wijzig message of the day" CssClass="btn btn-info form-control" OnClick="changeMotd" />
                        </div>
                    </div>



                </div>
            </div>
        </div>
    </div>
</asp:Content>
