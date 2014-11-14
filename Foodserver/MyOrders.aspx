<%@ Page Title="" Language="C#" MasterPageFile="~/Foodserver.master" AutoEventWireup="true" CodeFile="MyOrders.aspx.cs" Inherits="MyAccount" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="myaccountMainContent" ContentPlaceHolderID="cph_MainContent" runat="Server">
    <div class="centercontent">
        <div class="col-md-9 col-md-pull-3">
            <div class="box">
                <ul class="nav nav-tabs">
                    <li id="li_Vandaag" runat="server">
                        <asp:LinkButton ID="lbtn_Vandaag" runat="server" OnClick="lbtn_Vandaag_Click" CssClass="lbtnMyOrders">Bestellingen voor vandaag</asp:LinkButton>
                    </li>
                    <li id="li_Toekomst" runat="server">
                      <asp:LinkButton ID="lbtn_Toekomst" runat="server" OnClick="lbtn_Toekomst_Click" CssClass="lbtnMyOrders">Toekomstige bestellingen</asp:LinkButton>
                    </li>
                    <li id="li_Verleden" runat="server">
                        <asp:LinkButton ID="lbtn_Verleden" runat="server" OnClick="lbtn_Verleden_Click" CssClass="lbtnMyOrders">Vorige bestellingen</asp:LinkButton>
                    </li>
                    <li id="li_Instellingen" runat="server">
                        <asp:LinkButton ID="lbtn_Instellingen" runat="server" OnClick="lbtn_Instellingen_Click" CssClass="lbtnMyOrders"><span style="font-size:medium" class="glyphicon glyphicon-cog"></span></asp:LinkButton>
                    </li>
                </ul>
                <div class="panel panel-default">
                    <%--<div class="panel-heading">
                        <h3 id="h3_MyOrders" class="panel-title" runat="server"></h3>
                    </div>--%>
                    <div class="panel-body" style="min-height:433px;">
                        <asp:Label ID="error" runat="server" Text=""></asp:Label>
                        
                        <div id="besVandaag" runat="server">
                            <p>U kunt bestellingen de dag zelf annuleren vóór 10:30u.</p>

                            <asp:GridView ID="GridMyTodayOrders" runat="server" DataKeyNames="orderid,orderdetailsid" DataSourceID="DSMyTodayOrders" AutoGenerateColumns="False"
                                 OnRowCommand="GridTodayDelete_Click" CssClass="table table-striped table-condensed" AllowPaging="True" PageSize="10">
                                <Columns>
                                    <asp:BoundField DataField="orderid" HeaderText="" InsertVisible="False" ReadOnly="True" SortExpression="orderid" Visible="False" />
                                    <asp:BoundField DataField="orderdetailsid" HeaderText="" InsertVisible="False" ReadOnly="True" SortExpression="orderdetailsid" Visible="False" />
                                    <asp:BoundField DataField="Date" HeaderText="Datum" SortExpression="Date" HtmlEncode="False" DataFormatString="{0:d}"/>
                                    <asp:BoundField DataField="Maaltijd" HeaderText="Maaltijd" SortExpression="Maaltijd" />
                                    <asp:BoundField DataField="Option" HeaderText="Optie" SortExpression="Option" />
                                    <asp:BoundField DataField="Aantal" HeaderText="Aantal" SortExpression="Aantal" />
                                    <asp:BoundField DataField="Totaal" HeaderText="Prijs" SortExpression="Totaal" DataFormatString="{0:C}"/>
                                    <asp:BoundField DataField="Campus" HeaderText="Campus" SortExpression="Campus" />
                                    <asp:BoundField DataField="Opmerking" HeaderText="Opmerking" SortExpression="Opmerking" />
                                    <asp:ButtonField HeaderText="Annuleren" ShowHeader="True" Text="Annuleer" ItemStyle-ForeColor="Red" />
                                </Columns>

                                <HeaderStyle Height="10px" />

                                <EmptyDataTemplate>
                                    Er zijn geen bestellingen voor vandaag.  
                                </EmptyDataTemplate>
                            </asp:GridView>

                            <asp:SqlDataSource ID="DSMyTodayOrders" runat="server" ConnectionString="<%$ ConnectionStrings:FoodserverTestConnectionString %>"
                                DeleteCommand="DELETE FROM [order_details] where orderid=@orderid;DELETE FROM [orders] where orderid=@orderid">
                                <DeleteParameters>
                                    <asp:Parameter Name="orderid" />
                                </DeleteParameters>
                            </asp:SqlDataSource>
                        </div>
                        
                        <div id="besToekomst" runat="server">
                            <p></p>
                            <asp:GridView ID="GridMyFutureOrders" runat="server" DataKeyNames="orderid,orderdetailsid" DataSourceID="DSMyFutureOrders" AutoGenerateColumns="False" 
                                OnRowCommand="GridFutureDelete_Click" CssClass="table table-striped table-condensed" AllowPaging="True" PageSize="10">
                                <Columns>
                                    <asp:BoundField DataField="orderid" HeaderText="" InsertVisible="False" Visible="False" ReadOnly="True" SortExpression="orderid" />
                                    <asp:BoundField DataField="orderdetailsid" HeaderText="" InsertVisible="False" Visible="False" ReadOnly="True" SortExpression="orderdetailsid" />
                                    <asp:BoundField DataField="Date" HeaderText="Datum" SortExpression="Date" HtmlEncode="False" DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="Maaltijd" HeaderText="Maaltijd" SortExpression="Maaltijd" />
                                    <asp:BoundField DataField="Option" HeaderText="Optie" SortExpression="Option" />
                                    <asp:BoundField DataField="Aantal" HeaderText="Aantal" SortExpression="Aantal" />
                                    <asp:BoundField DataField="Totaal" HeaderText="Prijs" SortExpression="Totaal" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="Campus" HeaderText="Campus" SortExpression="Campus" />
                                    <asp:BoundField DataField="Opmerking" HeaderText="Opmerking" SortExpression="Opmerking" />
                                    <asp:ButtonField HeaderText="Annuleren" ShowHeader="True" Text="Annuleer" ItemStyle-ForeColor="Red"></asp:ButtonField>
                                </Columns>

                                <HeaderStyle Height="10px" />
                                <RowStyle />

                                <EmptyDataTemplate>
                                    Er zijn geen opkomende bestellingen. 
                                </EmptyDataTemplate>
                            </asp:GridView>

                            <asp:SqlDataSource ID="DSMyFutureOrders" runat="server" ConnectionString="<%$ ConnectionStrings:FoodserverTestConnectionString %>">
                                <DeleteParameters>
                                    <asp:Parameter Name="orderid" />
                                    <asp:Parameter Name="mealid" />
                                </DeleteParameters>
                            </asp:SqlDataSource>
                        </div>

                        <div id="besVerleden" runat="server">
                            <p></p>

                            <asp:GridView ID="GridMyPastOrders" runat="server" DataKeyNames="orderid" DataSourceID="DSMyPastOrders" AutoGenerateColumns="False"
                                CssClass="table table-striped table-condensed" AllowPaging="True" PageSize="10">
                                <Columns>
                                    <asp:BoundField DataField="orderdetailsid" HeaderText="id" InsertVisible="False" Visible="false" ReadOnly="True" SortExpression="orderdetailsid" />
                                    <asp:BoundField DataField="orderid" HeaderText="id" InsertVisible="False" Visible="false" ReadOnly="True" SortExpression="orderid" />
                                    <asp:BoundField DataField="Date" HeaderText="Datum" SortExpression="Date" HtmlEncode="False" DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="Maaltijd" HeaderText="Maaltijd" SortExpression="Maaltijd" />
                                    <asp:BoundField DataField="Option" HeaderText="Optie" SortExpression="Option" />
                                    <asp:BoundField DataField="Aantal" HeaderText="Aantal" SortExpression="Aantal" />
                                    <asp:BoundField DataField="Totaal" HeaderText="Prijs" SortExpression="Totaal" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="Campus" HeaderText="Campus" SortExpression="Campus" />
                                    <asp:BoundField DataField="Opmerking" HeaderText="Opmerking" SortExpression="Opmerking" />
                                </Columns>

                                <HeaderStyle Height="10px" />

                                <EmptyDataTemplate>
                                    Er zijn geen voorbije Bestellingen.  
                                </EmptyDataTemplate>
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="<<" LastPageText=">>" />
                            </asp:GridView>

                            <asp:SqlDataSource ID="DSMyPastOrders" runat="server" ConnectionString="<%$  ConnectionStrings:FoodserverTestConnectionString %>"></asp:SqlDataSource>
                        </div>
                        <div id="besInstellingen" runat="server">
                            <div class="col-md-12">
                            <asp:CheckBox ID="cbEmailConfimation" runat="server" Text="Ik wil graag een email bevestiging ontvangen wanneer ik een bestelling plaats." CssClass="chkEmail"/>
                            </div>
                            <br /><br />
                            <div class="col-md-3">
                                <asp:Button ID="btnSaveAccountSettings" runat="server" Text="Instellingen opslaan" OnClick="btnSaveAccountSettings_Click" CssClass="btn btn-info form-control" /><br />
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="lblConfirmAccountSettings" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

