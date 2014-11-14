<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Kitchen.aspx.cs" Inherits="Kitchen" UICulture="nl-BE" Culture="nl-BE" MasterPageFile="~/Foodserver.master" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="kitchenMainContent" ContentPlaceHolderID="cph_MainContent" runat="server">

    <div class="col-md-9 col-md-pull-3">
        <div class="box">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Bestellingen</h3>
                </div>
                <div class="panel-body">
                    <p>
                        Dag van bestellingen: <asp:Label ID="Lbl_Datum" runat="server" Text="" Font-Bold="true"></asp:Label>
                    </p>
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:Calendar ID="Cld_Datum" runat="server" OnSelectionChanged="Cld_Datum_SelectionChanged" 
                        BackColor="White" BorderColor="#34495E" BorderWidth="2px" CellPadding="1" DayNameFormat="Shortest" 
                        Font-Names="Verdana" Font-Size="10px" ForeColor="#003399" Height="200px" Width="30%" CssClass="calKitchen" 
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
                    <h4>Bestellingen</h4>
                    <div class="col-md-12" style="margin-bottom:20px">
                        <rsweb:ReportViewer ID="ReportViewerNewKitchenReport" AsyncRendering="false" runat="server" Font-Names="Verdana"
                            Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" ShowBackButton="False"
                            ShowPageNavigationControls="False" Width="100%" ShowFindControls="False" SizeToReportContent="True"
                            ToolBarItemBorderStyle="Ridge">
                            <LocalReport ReportPath="Reports\NewKitchenReport.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="DSNewKitchenReport" Name="DSNewKitchenReport" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </div>

                    <asp:SqlDataSource ID="DSNewKitchenReport" runat="server" ConnectionString="<%$ ConnectionStrings:FoodserverTestConnectionString %>" SelectCommand="select orders.orderid ,order_details.orderdetailsid, replace(replace(replace(orders.email,'student.',''),'@howest.be',''),'.',' ') as Naam, 
                        orders.deliverydate, order_details.quantity, order_details.comment, meals.mealname + ' ' + replace(options.optionname,'(Geen optie)','') as Maaltijd 
                        from orders 
                        join order_details on orders.orderid = order_details.orderid 
                        join order_details_option on order_details.orderdetailsid = order_details_option.orderdetailsid  
                        join meals on order_details.mealid = meals.mealid 
                        join options on order_details_option.optionid = options.optionid  
                        where DATEDIFF(day,orders.deliverydate,@Date) = 0 
                        and orders.campus = @Campus ">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Cld_Datum" Name="Date" PropertyName="SelectedDate" />
                            <asp:SessionParameter Name="Campus" SessionField="campus" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <h4>Broodjes (labels)</h4>
                    <rsweb:ReportViewer ID="ReportViewerBroodjesLabels" AsyncRendering="false" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" ShowBackButton="False" ShowPageNavigationControls="False" Width="752px" ShowFindControls="False" SizeToReportContent="True" Style="margin-right: 0px" ToolBarItemBorderStyle="Ridge">
                        <LocalReport ReportPath="Reports\NewKitchenBroodjesReport.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="DSNewKitchenBroodjes" Name="DSNewKitchenBroodjes" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>

                    <asp:SqlDataSource ID="DSNewKitchenBroodjes" runat="server" ConnectionString="<%$ ConnectionStrings:FoodserverTestConnectionString %>" SelectCommand="select orders.orderid ,order_details.orderdetailsid, replace(replace(replace(orders.email,'student.',''),'@howest.be',''),'.',' ') as Naam, 
                        orders.deliverydate, order_details.quantity, order_details.comment, meals.mealname + ' ' + replace(options.optionname,'(Geen optie)','') as Maaltijd 
                        from orders 
                        join order_details on orders.orderid = order_details.orderid 
                        join order_details_option on order_details.orderdetailsid = order_details_option.orderdetailsid 
                        join meals on order_details.mealid = meals.mealid 
                        join options on order_details_option.optionid = options.optionid 
                        where DATEDIFF(day,orders.deliverydate,@Date) = 0 
                        and orders.campus = @Campus
                        and (meals.mealname like '%broodje%' or meals.mealname like '%Italiaanse%')">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Cld_Datum" Name="Date" PropertyName="SelectedDate" />
                            <asp:SessionParameter Name="Campus" SessionField="campus" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
