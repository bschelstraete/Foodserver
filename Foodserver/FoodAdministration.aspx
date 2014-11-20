<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FoodAdministration.aspx.cs" Inherits="Admin" MasterPageFile="~/Foodserver.master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="adminContent" ContentPlaceHolderID="cph_MainContent" runat="server">
    <div class="col-md-9 col-md-pull-3">
        <div class="box">
            <ul class="nav nav-tabs">
                <li id="li_Administratie" runat="server">
                    <asp:LinkButton ID="lbtn_Administratie" runat="server" OnClick="Btn_Adminsitratie_Click" CssClass="lbtnMyOrders">Administratie</asp:LinkButton>
                </li>
                <li id="li_OverzichtMaaltijd" runat="server">
                    <asp:LinkButton ID="lbtn_OverzichtMaaltijd" runat="server" OnClick="Btn_OverzichtMaaltijd_Click" CssClass="lbtnMyOrders">Overzicht maaltijden</asp:LinkButton>
                </li>
                <li id="li_OverzichtOptie" runat="server">
                    <asp:LinkButton ID="lbtn_OverzichtOptie" runat="server" OnClick="Btn_OverzichtOptie_Click" CssClass="lbtnMyOrders">Overzicht opties</asp:LinkButton>
                </li>
            </ul>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 runat="server" id="headerTitle" class="panel-title">Maaltijden Administratie</h3>
                </div>
                <div class="panel-body pnlFoodAdmin">

                    <div id="overviewAdmin" runat="server" class="col-md-12">
                        <div id="adminIndex" runat="server" class="col-md-12 form-group">
                            <div id="divAlert" class="alert alert-success" runat="server">
                                <a href="#" class="close" data-dismiss="alert">&times;</a>
                                <strong>Success!</strong> De maaltijd is succesvol toegevoegd.
                            </div>
                            <div id="divNewOptionAlert" class="alert alert-success" runat="server">
                                <a href="#" class="close" data-dismiss="alert">&times;</a>
                                <strong>Success!</strong> De optie is succesvol toegevoegd.
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnAddNewMeal" OnClick="BtnAddNewMealPage_Click" runat="server" class="form-control" Text="Nieuwe maaltijd toevoegen" />
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnAddNewOption" OnClick="BtnAddNewOptionPage_Click" runat="server" class="form-control" Text="Nieuwe optie toevoegen" />
                            </div>
                        </div>

                        <div id="addNewMealPage" runat="server" class="col-md-12 form-group">
                            <div id="divNewMeal" runat="server" class="form-group">
                                <h3>Nieuwe maaltijd toevoegen</h3>
                                <asp:Label ID="lblMealNaam" runat="server" class="col-sm-3 control-label">Naam</asp:Label>
                                <asp:TextBox ID="txtMealNaam" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lblMealNaamError" runat="server" Visible="false">Gelieve een naam in te geven</asp:Label>
                                <br />
                                <asp:Label ID="lblMealPrijs" runat="server" class="col-sm-3 control-label">Prijs</asp:Label>
                                <asp:TextBox ID="txtMealPrijs" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lblMealPrijsError" runat="server" Visible="false">Gelieve een geldige prijs in te geven</asp:Label>
                                <br />
                                <asp:Label ID="lblCampus" runat="server" class="col-sm-3 control-label">Selecteer een campus</asp:Label><br />
                                <asp:CheckBoxList ID="cblMealCampus" runat="server" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                                <br />
                                <asp:Label ID="lblOpties" runat="server" class="col-sm-3 control-label">Selecteer een optie</asp:Label><br />
                                <asp:DropDownList ID="ddlOpties" runat="server" CssClass="form-control"></asp:DropDownList>
                                <br />

                                <div class="form-group">
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnNewMeal" Text="Maaltijd toevoegen" runat="server" CssClass="btn btn-success form-control" OnClick="Btn_AddNewMeal_Click" />
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnNext" Text="Optie toevoegen aan de maaltijd" runat="server" CssClass="btn btn-info form-control" OnClick="Btn_Next_Click" />
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnCancelNewMeal" OnClick="BtnCancelNewMeal_Click" CssClass="btn btn-danger form-control" runat="server" Text="Annuleren" />
                                    </div>
                                </div>
                                <br />
                            </div>

                            <div id="divNewOptionMeal" runat="server" class="form-group">
                                <asp:Label ID="lblOptionNaam" runat="server" class="col-sm-3 control-label">Naam</asp:Label>
                                <asp:TextBox ID="txtOptionNaam" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lblOptionNaamError" runat="server" CssClass="form-control" Visible="false">Gelieve een naam in te geven</asp:Label>
                                <br />
                                <asp:Label ID="lblOptionPrijs" runat="server" class="col-sm-3 control-label">Prijs</asp:Label>
                                <asp:TextBox ID="txtOptionPrijs" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lblOptionPrijsError" runat="server" CssClass="form-control" Visible="false">Gelieve een geldige prijs in te geven</asp:Label>
                                <br />
                                <div class="form-group">
                                    <div class="col-sm-6">
                                        <asp:Button ID="btnNewOption" Text="Opslaan" runat="server" CssClass="btn btn-success form-control" OnClick="Btn_AddNewOption_Click" />
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Button ID="btnCancelAddOption" Text="Terug" runat="server" CssClass="btn btn-info form-control" OnClick="Btn_CancelAddOption_Click" />
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>

                        <div id="addNewOptionPage" runat="server" class="col-md-12 form-group">
                            <div id="divNewOption" runat="server" class="form-group">
                                <h3>Nieuwe Optie toevoegen</h3>
                                <asp:Label ID="lblNewOptionNaam" runat="server" class="col-sm-3 control-label">Naam</asp:Label>
                                <asp:TextBox ID="txtNewOptionname" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lblNewOptionnameError" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblNewOptionPrijs" runat="server" class="col-sm-3 control-label">Prijs</asp:Label>
                                <asp:TextBox ID="txtNewOptionprice" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lblNewOptionpriceError" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblNewOptioncampus" runat="server" class="col-sm-3 control-label">Beschikbaar in</asp:Label><br />
                                <asp:CheckBoxList ID="cblNewOptioncampus" runat="server" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                                <br />
                                <asp:CheckBox runat="server" ID="chbNewOptionEnabled" Text="Mogelijk te bestellen" />
                                <br />
                                <asp:Label ID="lblNewAddMeal" runat="server" class="col-sm-4 control-label">Voeg een bestaande maaltijd toe</asp:Label>
                                <asp:DropDownList ID="ddlNewAddMeal" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                <br />
                                <br />

                                <div class="form-group">
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnSaveNewOption" OnClick="btn_SaveNewOption_Click" Text="Opslaan" runat="server" CssClass="btn btn-success form-control" />
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnNewOptionAddMeal" OnClick="BtnNewOptionAddMeal_Click" Text="Nieuwe maaltijd toevoegen" runat="server" CssClass="btn btn-info form-control" />
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnNewOptionCancel" OnClick="BtnCancelNewMeal_Click" Text="Annuleren" runat="server" CssClass="btn btn-danger form-control" />
                                    </div>

                                    <br />
                                </div>
                                <br />
                            </div>
                            <div id="divNewOptionNewMeal" runat="server">
                                <asp:Label ID="lblOptionNewMeal" runat="server" class="col-sm-3 control-label">Naam</asp:Label>
                                <asp:TextBox ID="txtOptionNewMeal" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lblOptionNewMealError" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblOptionNewMealPrice" runat="server" class="col-sm-3 control-label">Prijs</asp:Label>
                                <asp:TextBox ID="txtOptionNewMealPrice" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lblOptionNewMealPriceError" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblOptionNewMealCampus" runat="server" class="col-sm-3 control-label">Selecteer een campus</asp:Label><br />
                                <asp:CheckBoxList ID="cblOptionNewMealCampus" runat="server" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                                <br />
                                <asp:Label ID="lblOptionNewMealOption" runat="server" class="col-sm-6 control-label">Voeg een andere optie toe aan de maaltijd</asp:Label><br />
                                <asp:DropDownList ID="ddlOptionNewMealOption" runat="server" CssClass="form-control"></asp:DropDownList>
                                <br />

                                <div id="AddOptionButtons" class="form-group">
                                    <div class="col-sm-6">
                                        <asp:Button ID="btnSaveNewOptionAndMeal" OnClick="SaveNewOptionWithNewMeal" Text="Maaltijd en optie toevoegen" runat="server" CssClass="btn btn-success form-control" />
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Button ID="btnBackToNewOption" OnClick="BackToNewOption" Text="Terug" runat="server" CssClass="btn btn-danger form-control" />
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>

                    <div id="overviewMaaltijden" runat="server" class="col-md-12">
                        <div id="divOverzichtMaaltijd" runat="server">
                            <div id="divEditConfirmation" class="alert alert-success" runat="server">
                                <a href="#" class="close" data-dismiss="alert">&times;</a>
                                <strong>Success!</strong> De maaltijd is succesvol bewerkt.
                            </div>
                            <asp:GridView ID="ListMeals" OnRowCommand="ListMeals_RowCommand" OnPageIndexChanging="ListMeals_PageIndexChanging" CssClass="table table-hover" AllowPaging="true" AutoGenerateColumns="false" DataKeyNames="Mealid" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="NoDisplay" ItemStyle-CssClass="NoDisplay" HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Mealid") %>' ID="lblMealid" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maaltijd">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Mealname") %>' ID="lblMealname" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prijs">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Price") %>' ID="lblMealPrice" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button CommandArgument='<%# ((GridViewRow) Container).DataItemIndex + ";" + "EDIT" %>' ID="btnEdit" Text="Bewerken" runat="server" />
                                            <asp:Button CommandArgument='<%# ((GridViewRow) Container).DataItemIndex + ";" + "DELETE" %>' OnClientClick="return confirm('Bent u zeker dat u deze maaltijd wil verwijderen?')" Text="Verwijder" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div id="divEditPagina" runat="server">
                            <asp:Label ID="lblEditMealNaam" runat="server" class="col-sm-3 control-label">Naam</asp:Label>
                            <asp:TextBox ID="txtEditMealname" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblEditMealnameError" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="lblEditMealPrijs" runat="server" class="col-sm-3 control-label">Prijs</asp:Label>
                            <asp:TextBox ID="txtEditMealprice" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblEditMealpriceError" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="lblEditMealcampus" runat="server" class="col-sm-3 control-label">Beschikbaar in</asp:Label><br />
                            <asp:CheckBoxList ID="cblEditMealcampus" runat="server" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                            <br />
                            <asp:CheckBox runat="server" ID="chbEnabled" Text="Mogelijk te bestellen" />
                            <br />
                            <asp:Label ID="lblEditAddOptions" runat="server" class="col-sm-4 control-label">Voeg een bestaande optie toe</asp:Label><asp:Label ID="lblEditAddOptionError" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlEditAddOption" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                            <br />
                            <asp:Label ID="lblEditDeleteOpties" runat="server" class="col-sm-5 control-label">Selecteer een te verwijderen optie</asp:Label><br />
                            <asp:DropDownList ID="ddlEditDeleteOpties" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            </asp:DropDownList>
                            <br />

                            <div class="form-group">
                                <div class="col-sm-4">
                                    <asp:Button ID="btnFinishEdit" Text="Maaltijd bewerken" runat="server" OnClick="SaveEditMeal" CssClass="btn btn-success form-control" />
                                </div>
                                <div class="col-sm-4">
                                    <asp:Button ID="btnEditMealAddOption" OnClick="InitAddOptionToEdit" Text="Nieuwe optie toevoegen" runat="server" CssClass="btn btn-info form-control" />
                                </div>
                                <div class="col-sm-4">
                                    <asp:Button ID="btnEditCancel" Text="Annuleren" OnClick="CancelEditMeal" runat="server" CssClass="btn btn-danger form-control" />
                                </div>

                                <br />
                            </div>
                            <br />
                        </div>
                        <div id="divEditNewOption" runat="server" class="form-group">
                            <asp:Label ID="lblEditNewOption" runat="server" class="col-sm-3 control-label">Naam</asp:Label>
                            <asp:TextBox ID="txtEditNewOption" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblEditNewOptionNaamError" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="lblEditNewPrijs" runat="server" class="col-sm-3 control-label">Prijs</asp:Label>
                            <asp:TextBox ID="txtEditNewPrijs" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblEditNewOptionPrijsError" runat="server"></asp:Label>
                            <br />
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <asp:Button ID="btnEditAddOption" OnClick="AddOptionToEdit" Text="Opslaan" runat="server" CssClass="btn btn-success form-control" />
                                </div>
                                <div class="col-sm-6">
                                    <asp:Button ID="btnEditOptionBack" OnClick="BackToEditMeal" Text="Terug" runat="server" CssClass="btn btn-info form-control" />
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>

                    <div id="overviewOpties" runat="server" class="col-md-12">
                        <div id="divOverzichtOpties" runat="server">
                            <div id="divEditOptionConfirmation" class="alert alert-success" runat="server">
                                <a href="#" class="close" data-dismiss="alert">&times;</a>
                                <strong>Success!</strong> De optie is succesvol bewerkt.
                            </div>
                            <asp:GridView ID="ListOptions" OnRowCommand="ListOptions_RowCommand" OnPageIndexChanging="ListOptions_PageIndexChanging" CssClass="table table-hover" AllowPaging="true" AutoGenerateColumns="false" DataKeyNames="Optionid" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="NoDisplay" ItemStyle-CssClass="NoDisplay" HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Optionid") %>' ID="lblOptionid" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opties">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Optionname") %>' ID="lblOptionname" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prijs">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Price") %>' ID="lblOptionPrice" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button CommandArgument='<%# ((GridViewRow) Container).DataItemIndex + ";" + "EDIT" %>' runat="server" ID="btnEdit" Text="Bewerken" />
                                            <asp:Button CommandArgument='<%# ((GridViewRow) Container).DataItemIndex + ";" + "DELETE" %>' OnClientClick="return confirm('Bent u zeker dat u deze optie wil verwijderen?')" runat="server" ID="btnDelete" Text="Verwijderen" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div id="divEditOption" runat="server">
                            <asp:Label ID="lblEditOptionNaam" runat="server" class="col-sm-3 control-label">Naam</asp:Label>
                            <asp:TextBox ID="txtEditOptionname" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblEditOptionnameError" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="lblEditOptionPrijs" runat="server" class="col-sm-3 control-label">Prijs</asp:Label>
                            <asp:TextBox ID="txtEditOptionprice" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblEditOptionpriceError" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="lblEditOptioncampus" runat="server" class="col-sm-3 control-label">Beschikbaar in</asp:Label><br />
                            <asp:CheckBoxList ID="cblEditOptioncampus" runat="server" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                            <br />
                            <asp:CheckBox runat="server" ID="chbOptionEnabled" Text="Mogelijk te bestellen" />
                            <br />
                            <asp:Label ID="lblEditAddMeal" runat="server" class="col-sm-4 control-label">Voeg een bestaande optie toe</asp:Label><asp:Label ID="Label1" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlEditAddMeal" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                            <br />
                            <asp:Label ID="lblEditDeleteMeal" runat="server" class="col-sm-5 control-label">Selecteer een te verwijderen optie</asp:Label><br />
                            <asp:DropDownList ID="ddlEditDeleteMeal" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            </asp:DropDownList>
                            <br />

                            <div class="form-group">
                                <div class="col-sm-4">
                                    <asp:Button ID="btnFinishEditOption" OnClick="SaveEditOption" Text="Optie bewerken" runat="server" CssClass="btn btn-success form-control" />
                                </div>
                                <div class="col-sm-4">
                                    <asp:Button ID="btnEditOptionAddMeal" OnClick="AddNewMealToEditOption" Text="Nieuwe maaltijd toevoegen" runat="server" CssClass="btn btn-info form-control" />
                                </div>
                                <div class="col-sm-4">
                                    <asp:Button ID="btnEditOptionCancel" Text="Annuleren" OnClick="CancelOptionEdit" runat="server" CssClass="btn btn-danger form-control" />
                                </div>

                                <br />
                            </div>
                            <br />
                        </div>
                        <div id="divEditOptionNewMeal" runat="server">
                            <asp:Label ID="lblNewMealNaam" runat="server" class="col-sm-3 control-label">Naam</asp:Label>
                            <asp:TextBox ID="txtNewMealNaam" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblNewMealNaamError" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="lblNewMealPrijs" runat="server" class="col-sm-3 control-label">Prijs</asp:Label>
                            <asp:TextBox ID="txtNewMealPrijs" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblNewMealPrijsError" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="lblNewCampus" runat="server" class="col-sm-3 control-label">Selecteer een campus</asp:Label><br />
                            <asp:CheckBoxList ID="cblNewMealCampus" runat="server" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                            <br />
                            <asp:Label ID="lblNewOpties" runat="server" class="col-sm-6 control-label">Voeg een andere optie toe aan de maaltijd</asp:Label><br />
                            <asp:DropDownList ID="ddlNewOpties" runat="server" CssClass="form-control"></asp:DropDownList>
                            <br />

                            <div id="editOptionButtons" class="form-group">
                                <div class="col-sm-6">
                                    <asp:Button ID="btnNewMealToOption" OnClick="SaveNewMealToEditOption" Text="Maaltijd en optie toevoegen" runat="server" CssClass="btn btn-success form-control" />
                                </div>
                                <div class="col-sm-6">
                                    <asp:Button ID="btnBackToEditOption" OnClick="CancelNewMealToEditOption" Text="Terug" runat="server" CssClass="btn btn-danger form-control" />
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
