<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" MasterPageFile="~/Foodserver.master" %>

<%@ PreviousPageType VirtualPath="~/Default.aspx" %>

<asp:Content ID="studentMainContent" ContentPlaceHolderID="cph_MainContent" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div class="col-md-6">
        <div class="box">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Maaltijd bestellen</h3>
                </div>
                <div class="panel-body pnlBestellen pnlExtra">
                        <div class="form-group">
                            <div class="col-sm-12">
                                <asp:Label ID="lblError" runat="server" Text="" CssClass="control-label label label-danger col-sm-12 lblError"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="campusLbl" class="col-sm-3 control-label">Campus</label>
                            <div class="col-sm-9">
                                <asp:Label ID="campusLbl" runat="server" Text="" CssClass="form-control" disabled="true"></asp:Label>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-3 control-label" for="deliveryDateText">Dag bestelling</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="deliveryDateText" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="ddlMeals" class="col-sm-3 control-label">Maaltijd</label>
                            <div class="col-sm-9">
                                <asp:DropDownList ID="ddlMeals" runat="server" CssClass="form-control" OnSelectedIndexChanged="ShowOptionsForMeal"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="ddlOptions" class="col-sm-3 control-label">Optie</label>
                            <div class="col-sm-9">
                                <asp:DropDownList ID="ddlOptions" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="ddlOptions" class="col-sm-3 control-label">Opmerking</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtOpmerking" runat="server" MaxLength="27" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="ddlOptions" class="col-sm-3 control-label">Aantal</label>
                            <div class="col-sm-1">
                                <asp:Button ID="btnMinBread" runat="server" Text="-" CssClass="btn btn-info form-control" />
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="numberOrderDetails" runat="server" Text="1" CssClass="form-control txtAantal"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <asp:Button ID="btnPlusBread" runat="server" Text="+" CssClass="btn btn-info form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-6">
                                <asp:Button ID="btn_Bestel" runat="server" Text="Bestel nu!" CssClass="btn btn-success form-control" OnClick="btn_Bestel_Click" />
                            </div>
                            <div class="col-sm-6">
                                <asp:Button ID="btnVoegToe" AutoPostBack="false" runat="server" Text="Voeg toe aan winkelmandje " OnClick="btnVoegToe_Click" CssClass="btn btn-info form-control" />
                            </div>
                        </div>

                        <div id="divWinkelmandje" runat="server" visible="false">
                            <div class="form-group">
                                <label id="ShoppingBasketLbl" visible="true" runat="server" for="ddlOptions" class="col-sm-3 control-label">Winkelmandje</label>
                                <div class="col-sm-9">
                                    <asp:ListBox ID="lbCart" runat="server" CssClass="listCart form-control" Rows="9" Visible="true"></asp:ListBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6 col-sm-offset-6">
                                    <asp:Button ID="btnDeleteSandwiches" runat="server" OnClick="btnDeleteSandwiches_Click" Text="Verwijder geselecteerd item" CssClass="btn btn-info form-control" Visible="true" />
                                </div>
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-3 col-md-pull-9">
        <div class="box">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Weekmenu</h3>
                </div>
                <div class="panel-body">
                    <div id="weekmenuDiv" runat="server">
                        <div class="row col-sm-12 divWeekdag">
                            <asp:LinkButton ID="lbtnDag1" runat="server" CssClass="lbtnWeekmenu" OnClick="lbtnDag_Click">
                                <div class="col-sm-3 divDag">
                                    <span class="spanDate">Ma</span>
                                    <span id="spanDag1" class="spanDag" runat="server"></span>
                                </div>
                                <div class="col-sm-9 divDag">
                                    <ul id="ulDag1" runat="server"></ul>
                                </div>
                            </asp:LinkButton>
                        </div>
                        <div class="row col-sm-12 divWeekdag">
                            <asp:LinkButton ID="lbtnDag2" runat="server" CssClass="lbtnWeekmenu" OnClick="lbtnDag_Click">
                                <div class="col-sm-3 divDag">
                                    <span class="spanDate">Di</span>
                                    <span id="spanDag2" class="spanDag" runat="server"></span>
                                </div>
                                <div class="col-sm-9 divDag">
                                    <ul id="ulDag2" runat="server"></ul>
                                </div>
                            </asp:LinkButton>
                        </div>
                        <div class="row col-sm-12 divWeekdag">
                            <asp:LinkButton ID="lbtnDag3" runat="server" CssClass="lbtnWeekmenu" OnClick="lbtnDag_Click">
                                <div class="col-sm-3 divDag">
                                    <span class="spanDate">Wo</span>
                                    <span id="spanDag3" class="spanDag" runat="server"></span>
                                </div>
                                <div class="col-sm-9 divDag">
                                    <ul id="ulDag3" runat="server"></ul>
                                </div>
                            </asp:LinkButton>
                        </div>
                        <div class="row col-sm-12 divWeekdag">
                            <asp:LinkButton ID="lbtnDag4" runat="server" CssClass="lbtnWeekmenu" OnClick="lbtnDag_Click">
                                <div class="col-sm-3 divDag">
                                    <span class="spanDate">Do</span>
                                    <span id="spanDag4" class="spanDag" runat="server"></span>
                                </div>
                                <div class="col-sm-9 divDag">
                                    <ul id="ulDag4" runat="server"></ul>
                                </div>
                            </asp:LinkButton>
                        </div>
                        <div class="row col-sm-12 divWeekdag">
                            <asp:LinkButton ID="lbtnDag5" runat="server" CssClass="lbtnWeekmenu" OnClick="lbtnDag_Click">
                                <div class="col-sm-3 divDag">
                                    <span class="spanDate">Vr</span>
                                    <span id="spanDag5" class="spanDag" runat="server"></span>
                                </div>
                                <div class="col-sm-9 divDag">
                                    <ul id="ulDag5" runat="server"></ul>
                                </div>
                            </asp:LinkButton>
                        </div>
                        <div class="row col-md-12">
                            Elke dag verse soep en desserts te koop aan de zelfbediening voor slechts €0.50!
                        </div>
                    </div>
                </div>
            </div>
        </div>
            <div class="panel panel-default feedbackDiv  hidden-lg hidden-md">
                <div class="panel-heading">
                    <h3 class="panel-title">Feedback</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                            <div class="col-sm-12">
                                <asp:TextBox ID="txtFeedback" runat="server" Rows="7" TextMode="MultiLine" CssClass="txtFeedback" Width="100%" style="resize:none" 
                                                Placeholder="Heeft u fouten gevonden of opmerkingen over deze applicatie, gelieve het ons via deze weg te laten weten.">
                                </asp:TextBox>
                            </div>
                            <div class="col-sm-12">
                                <asp:Button ID="btnSend" runat="server" Text="Verstuur feedback" CssClass="btn btn-info form-control btnFeedback" OnClick="lbtn_Feedback_Click" />
                            </div>
                            <asp:Label ID="lblConfirm" runat="server" Text="" ForeColor="Green"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
