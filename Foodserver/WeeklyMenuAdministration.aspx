<%@ Page Title="" Language="C#" MasterPageFile="~/Foodserver.master" AutoEventWireup="true" CodeFile="WeeklyMenuAdministration.aspx.cs" Inherits="WeeklyMenuAdministration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_MainContent" runat="Server">
    <div class="col-md-9 col-md-pull-3">
        <div class="box">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Weekmenu administratie</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal" role="form">
                        <div class="form-group">
                            <div class="col-sm-4">
                                <asp:Calendar ID="Cal_Menu" runat="server" OnSelectionChanged="Cal_Menu_SelectionChanged"
                                    BackColor="White" BorderColor="#34495E" BorderWidth="2px" CellPadding="1"
                                    DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                    ForeColor="#003399" Height="200px" Width="100%"
                                    OnDayRender="Cal_Menu_DayRender">
                                    <DayHeaderStyle BackColor="#4C708D" ForeColor="#FFFFFF" Height="1px" HorizontalAlign="Center" />
                                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                    <OtherMonthDayStyle ForeColor="#999999" />
                                    <SelectedDayStyle BackColor="#4F94CD" Font-Bold="True" ForeColor="#FFFFFF" />
                                    <SelectorStyle BackColor="#4C708D" ForeColor="#FFFFFF" />
                                    <TitleStyle BackColor="#34495E" BorderColor="#34495E" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#FFFFFF" Height="25px" />
                                    <TodayDayStyle ForeColor="Red" Font-Bold="true" />
                                    <WeekendDayStyle BackColor="#CCCCFF" />
                                </asp:Calendar>
                            </div>
                            <div class="col-sm-3">  
                                <asp:ListBox ID="lstFrom" runat="server"></asp:ListBox>
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="Button1" runat="server" Text="Kopieer" OnClick="btnKopkieer_Click" />
                            </div>
                            <div class="col-sm-3">
                                <asp:CheckBoxList ID="cblTo" runat="server"></asp:CheckBoxList>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="Date,Campus" CssClass="table table-hover table-striped table-condensed">
                                <Columns>
                                    <asp:BoundField DataField="Date" HeaderText="Datum" ReadOnly="True" SortExpression="Date" DataFormatString="{0:D}" />
                                    <asp:TemplateField HeaderText="Meal" SortExpression="Meal" ConvertEmptyStringToNull="False">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Meal") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="MealTextBox" runat="server" Text='<%# Bind("Meal") %>' Rows="3" TextMode="MultiLine"></asp:TextBox>
                                        </ItemTemplate>
                                        <ControlStyle Width="100%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Campus" HeaderText="Campus" ReadOnly="True" SortExpression="Campus" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FoodserverConnectionString %>" DeleteCommand="DELETE FROM [WeeklyMenu] WHERE [Date] = @Date AND [Campus] = @Campus" InsertCommand="INSERT INTO [WeeklyMenu] ([Date], [Meal], [Campus]) VALUES (@Date, @Meal, @Campus)" SelectCommand="SELECT WeeklyMenu.Date, WeeklyMenu.Meal, Campus FROM WeeklyMenu WHERE (WeeklyMenu.Date BETWEEN DATEADD(wk, DATEDIFF(wk, 0, @Date), 0) AND DATEADD(wk, DATEDIFF(wk, 0, @Date), 0) + 6) ORDER BY Campus, WeeklyMenu.Date" UpdateCommand="UPDATE [WeeklyMenu] SET [Meal] = @Meal WHERE [Date] = @Date AND [Campus] = @Campus">
                            <DeleteParameters>
                                <asp:Parameter DbType="Date" Name="Date" />
                                <asp:Parameter Name="Campus" Type="String" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter DbType="Date" Name="Date" />
                                <asp:Parameter Name="Meal" Type="String" />
                                <asp:Parameter Name="Campus" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="Meal" Type="String" />
                                <asp:Parameter DbType="Date" Name="Date" />
                                <asp:Parameter Name="Campus" Type="String" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="Cal_Menu" DbType="Date" Name="Date" PropertyName="SelectedDate" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                    </div>





                    <div class="form-group">

                        <div class="col-sm-12">
                            <asp:Button ID="Btn_Submit" runat="server" Text="Update" OnClick="UpdateButton_Click" CssClass="btn btn-danger form-control" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

