<%@ Page Title="" Language="C#" MasterPageFile="~/Foodserver.master" AutoEventWireup="true" CodeFile="UserAdministration.aspx.cs" Inherits="UserAdministration" %>

<asp:Content ID="UserAdministration" ContentPlaceHolderID="cph_MainContent" runat="Server">

    <div class="col-md-9 col-md-pull-3">
        <div class="box">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Gebruiker Administratie</h3>
                </div>
                <div class="panel-body">
                    <div id="divGeneral" runat="server" class="form-horizontal" role="form">
                        <div class="form-group">
                            <div class="col-sm-9">
                                <asp:TextBox ID="txt_Zoek" runat="server" Text="" AutoPostBack="False" ClientIDMode="Inherit" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                <asp:Button ID="btn_Zoek" runat="server" Text="Zoek persoon" CssClass="btn btn-info form-control" OnClick="btn_Zoek_Click" />
                            </div>
                            <asp:Label ID="lbl_Error" runat="server" ForeColor="Tomato"></asp:Label>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12">
                                <asp:ListBox ID="listbox_users" runat="server" Width="100%" Height="500px" CssClass="form-control"></asp:ListBox>
                            </div>
                        </div>
                        <%--<div class="form-group">
                            <div class="col-sm-2">
                                <asp:TextBox ID="txt_Amount" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btn_AddCredits" runat="server" Text="Krediet opladen" CssClass="btn btn-info form-control" OnClick="btn_AddCredits_Click" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-5">
                                <asp:Label ID="lbl_CurrentCredit" runat="server" Text="" CssClass="control-label"></asp:Label>
                            </div>
                        </div>--%>
                    </div>
                    <div id="divUser" runat="server" class="form-horizontal" role="form">
                        <div class="form-group col-md-12">
                            <label for="lblEmail" class="col-sm-2 control-label">Email</label>
                            <asp:Label ID="lbl_Email" runat="server" Text="" CssClass="control-label col-sm-10"></asp:Label>
                        </div>
                        <div class="form-group col-md-12">
                            <label for="lblSaldo" class="col-sm-2 control-label">Huidig Saldo</label>
                            <asp:Label ID="lbl_CurrentCredit" runat="server" Text="" CssClass="control-label col-sm-10"></asp:Label>
                        </div>
                        <div class="form-group col-md-12">
                            <label for="lblOpladen" class="col-sm-2 control-label">Krediet Opladen</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txt_Amount" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btn_AddCredits" runat="server" CssClass="form-control btn-info" Text="Opladen" OnClick="btn_AddCredits_Click" />
                            </div>
                        </div>
                        <div class="form-group col-md-12">
                            <label for="lblOpladen" class="col-sm-2 control-label">Toegang</label>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlEmployment" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnChangeEmployment" runat="server" CssClass="form-control btn-info" Text="Verander" OnClick="btnChangeEmployment_Click" />
                            </div>
                        </div>
                        <div id="divFeedback" runat="server" class="col-md-12 form-group">
                            <label for="lblFeedback" class="col-sm-2 control-label">Feedback</label>
                            <div class="col-md-2">
                                <asp:CheckBox ID="chkFeedback" runat="server" Text="Email" CssClass="checkbox" OnCheckedChanged="chkFeedback_CheckedChanged"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

