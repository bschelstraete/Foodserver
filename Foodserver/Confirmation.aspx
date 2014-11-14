<%@ Page Title="" Language="C#" MasterPageFile="~/Foodserver.master" AutoEventWireup="true" CodeFile="Confirmation.aspx.cs" Inherits="Confirmation" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_MainContent" Runat="Server">
    <div class="centercontent">
        <h3>Bevestiging</h3>
        <p>Uw bestelling is succesvol geplaatst. De volgende maaltijden zijn besteld: </p>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" ShowBackButton="False" ShowPageNavigationControls="False" Width="752px" ShowFindControls="False" SizeToReportContent="True" style="margin-right: 0px" ToolBarItemBorderStyle="Ridge">
            <LocalReport ReportPath="Reports\reportConfirmation.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DSOrderReport" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FoodserverConnectionString1 %>" SelectCommand="SELECT * FROM [admins]"></asp:SqlDataSource>
    </div>
</asp:Content>

