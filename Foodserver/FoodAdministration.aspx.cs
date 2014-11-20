using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

public partial class Admin : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["type"] != "admin")
        {
            Response.Write("<script type='text/javascript'>alert('U hebt geen toegang tot deze pagina.');</script>");
            System.Threading.Thread.Sleep(2000);
            Response.Redirect("Default.aspx");
            Server.Transfer("Default.aspx");
        }
        if (!IsPostBack)
        {
            //administratietab op active zetten wanneer je voor het eerst op de pagina komt
            Btn_Adminsitratie_Click(sender, e);
            divNewMeal.Visible = true;
            divNewOptionMeal.Visible = false;
            divNewOption.Visible = true;
            divNewOptionNewMeal.Visible = false;

            divAlert.Visible = false;
            divNewOptionAlert.Visible = false;
            divEditPagina.Visible = false;
            divEditNewOption.Visible = false;
            divEditConfirmation.Visible = false;

            divEditOption.Visible = false;
            divEditOptionConfirmation.Visible = false;
            divEditOptionNewMeal.Visible = false;
        }
    }

    private void resetAddOptionPage()
    {
        divNewOptionAlert.Visible = false;
        txtNewOptionname.Text = "";
        txtNewOptionprice.Text = "";
        lblNewOptionnameError.Text = "";
        lblNewOptionpriceError.Text = "";
        lblOptionNewMealError.Text = "";
        lblOptionNewMealPriceError.Text = "";
    }

    private void resetAddMealPage()
    {
        divAlert.Visible = false;
        txtMealNaam.Text = "";
        txtMealPrijs.Text = "";
        lblMealNaamError.Text = "";
        lblMealPrijsError.Text = "";
        lblOptionNaamError.Text = "";
        lblOptionPrijsError.Text = "";
    }
    private void resetEditPage()
    {
        divEditConfirmation.Visible = false;
        lblEditMealnameError.Text = "";
        lblEditMealpriceError.Text = "";
        lblEditNewOptionNaamError.Text = "";
        lblEditNewOptionPrijsError.Text = "";
    }
    private void resetEditOptionPage()
    {
        divEditOptionConfirmation.Visible = false;
        lblEditOptionnameError.Text = "";
        lblEditOptionpriceError.Text = "";
        lblNewMealNaamError.Text = "";
        lblNewMealPrijsError.Text = "";
    }

    private void addOptionConfirmation()
    {
        divNewOptionAlert.Visible = true;
        resetAddOptionPage();
        addNewOptionPage.Visible = false;
        divNewOptionNewMeal.Visible = false;
        adminIndex.Visible = true;
    }


    protected void BtnAddNewMealPage_Click(object sender, EventArgs e)
    {
        adminIndex.Visible = false;
        addNewMealPage.Visible = true;
        addNewOptionPage.Visible = false;
        divNewMeal.Visible = true;
        divNewOptionMeal.Visible = false;
    }

    protected void BtnAddNewOptionPage_Click(object sender, EventArgs e)
    {
        adminIndex.Visible = false;
        addNewMealPage.Visible = false;
        addNewOptionPage.Visible = true;
        divNewOption.Visible = true;
        divNewOptionNewMeal.Visible = false;
    }

    //Navigatie - tabs

    protected void Btn_Adminsitratie_Click(object sender, EventArgs e)
    {
        overviewAdmin.Visible = true;
        overviewMaaltijden.Visible = false;
        overviewOpties.Visible = false;
        AddCheckboxCampus(cblMealCampus);
        AddCheckboxCampus(cblNewOptioncampus);

        adminIndex.Visible = true;
        addNewMealPage.Visible = false;
        addNewOptionPage.Visible = false;

        li_Administratie.Attributes.Add("class", "active");
        li_OverzichtMaaltijd.Attributes.Add("class", "");
        li_OverzichtOptie.Attributes.Add("class", "");
        headerTitle.InnerHtml = "Administratie maaltijden";
        resetAddMealPage();
        resetAddOptionPage();

        //Om te vermijden dat er data te kort is (wanneer net toegevoed)
        ddlOpties.Items.Clear();
        ddlOpties.DataSource = Database.DatabaseInstance.GetVisibleOptions();
        ddlOpties.DataBind();

        ddlNewAddMeal.Items.Clear();
        ddlNewAddMeal.Items.Add(new ListItem() { Value = "-1", Text = "Selecteer een toe te voegen maaltijd" });
        ddlNewAddMeal.DataSource = Database.DatabaseInstance.GetVisibleMeals();
        ddlNewAddMeal.DataBind();

        PopulateListMeals();
        PopulateListOptions();
    }
    protected void Btn_OverzichtMaaltijd_Click(object sender, EventArgs e)
    {
        overviewAdmin.Visible = false;
        overviewMaaltijden.Visible = true;
        overviewOpties.Visible = false;
        li_Administratie.Attributes.Add("class", "");
        li_OverzichtMaaltijd.Attributes.Add("class", "active");
        li_OverzichtOptie.Attributes.Add("class", "");
        headerTitle.InnerHtml = "Overzicht maaltijden";
        divOverzichtMaaltijd.Visible = true;
        divEditPagina.Visible = false;
        divEditNewOption.Visible = false;
        divEditConfirmation.Visible = false;
        resetEditPage();

        //Om te vermijden dat er data te kort is (wanneer net toegevoed)
        PopulateListMeals();
    }
    protected void Btn_OverzichtOptie_Click(object sender, EventArgs e)
    {
        overviewAdmin.Visible = false;
        overviewMaaltijden.Visible = false;
        overviewOpties.Visible = true;
        li_Administratie.Attributes.Add("class", "");
        li_OverzichtMaaltijd.Attributes.Add("class", "");
        li_OverzichtOptie.Attributes.Add("class", "active");
        headerTitle.InnerHtml = "Overzicht opties";
        divOverzichtOpties.Visible = true;
        divEditOption.Visible = false;
        divEditOptionConfirmation.Visible = false;
        divEditOptionNewMeal.Visible = false;
        resetAddNewMealToOption();

        //Om te vermijden dat er data te kort is (wanneer net toegevoed)
        PopulateListOptions();
        
    }

    //Foodadminstratie -- nieuwe maaltijd & optie toevoegen
    protected void Btn_AddNewMeal_Click(object sender, EventArgs e)
    {
        resetAddMealPage();
        if (ValidateNaam(txtMealNaam) && ValidatePrijs(txtMealPrijs))
        {
            string mealName = txtMealNaam.Text;
            decimal mealPrice = 0.0M;
            decimal.TryParse(txtMealPrijs.Text, out mealPrice);
            List<string> campusList = new List<string>();

            campusList = GetSelectedCampussen(cblMealCampus);

            int newOption = Database.DatabaseInstance.GetOptionByOptionName(ddlOpties.SelectedValue).Optionid;
            Meal newMeal = new Meal(mealName, mealPrice, true, campusList);

            AddNewMealWithOption(newMeal, newOption);
            Btn_Adminsitratie_Click(sender, e);
            divAlert.Visible = true;
            divNewOptionAlert.Visible = false;
        }
        else if (!ValidateNaam(txtMealNaam))
        {
            lblMealNaamError.Visible = true;
            lblMealNaamError.Text = "Gelieve een naam in te vullen";
            lblMealNaamError.Style.Add("color", "red");
        }
        else if (!ValidatePrijs(txtMealPrijs))
        {
            lblMealPrijsError.Visible = true;
            lblMealPrijsError.Text = "Gelieve een geldige prijs in te vullen";
            lblMealPrijsError.Style.Add("color", "red");
        }
    }
    protected void Btn_Next_Click(object sender, EventArgs e)
    {
        if (ValidateNaam(txtMealNaam) && ValidatePrijs(txtMealPrijs))
        {
            divNewMeal.Visible = false;
            divNewOptionMeal.Visible = true;
        }
        else if (!ValidateNaam(txtMealNaam))
        {
            lblMealNaamError.Visible = true;
            lblMealNaamError.Text = "Gelieve een naam in te geven";
            lblMealNaamError.Style.Add("color", "red");
        }
        else if (!ValidatePrijs(txtMealPrijs))
        {
            lblMealPrijsError.Visible = true;
            lblMealPrijsError.Text = "Gelieve een geldige prijs in te geven";
            lblMealPrijsError.Style.Add("color", "red");
        }
    }
    protected void Btn_AddNewOption_Click(object sender, EventArgs e)
    {
        resetAddOptionPage();
        if (ValidateNaam(txtOptionNaam) && ValidatePrijs(txtOptionPrijs))
        {
            string mealName = txtMealNaam.Text;
            decimal mealPrice = 0.0M;
            decimal.TryParse(txtMealPrijs.Text, out mealPrice);
            List<string> selectedCampus = GetSelectedCampussen(cblMealCampus);
            Meal newMeal = new Meal(mealName, mealPrice, true, selectedCampus);

            string optionName = txtOptionNaam.Text;
            decimal optionPrice = 0.0M;
            decimal.TryParse(txtOptionPrijs.Text, out optionPrice);

            Database.DatabaseInstance.insertOptions(optionName, true, optionPrice, selectedCampus);
            int newOptionId = Database.DatabaseInstance.GetLatestOption();
            AddNewMealWithOption(newMeal, newOptionId);

            //resetten van het venster met confirmatie
            Btn_Adminsitratie_Click(sender, e);
            divAlert.Visible = true;
            divNewOptionAlert.Visible = false;
        }
        else if (!ValidateNaam(txtOptionNaam))
        {
            lblOptionNaamError.Visible = true;
            lblOptionNaamError.Text = "Gelieve een naam in te vullen";
            lblOptionNaamError.Style.Add("color", "red");
        }
        else if (!ValidatePrijs(txtOptionPrijs))
        {
            lblOptionPrijsError.Visible = true;
            lblOptionPrijsError.Text = "Gelieve een geldige prijs in te vullen";
            lblOptionPrijsError.Style.Add("color", "red");
        }
    }

    protected void BtnNewOptionAddMeal_Click(object sender, EventArgs e)
    {
        if (ValidateNaam(txtNewOptionname) && ValidatePrijs(txtNewOptionprice))
        {
            divNewOption.Visible = false;
            divNewOptionNewMeal.Visible = true;
        }
        else if (!ValidateNaam(txtNewOptionname))
        {
            lblNewOptionnameError.Visible = true;
            lblNewOptionnameError.Text = "Gelieve een naam in te vullen";
            lblNewOptionnameError.Style.Add("color", "red");
        }
        else if (!ValidatePrijs(txtNewOptionprice))
        {
            lblMealPrijsError.Visible = true;
            lblNewOptionnameError.Text = "Gelieve een geldige prijs in te vullen";
            lblMealPrijsError.Style.Add("color", "red");
        }
    }

    protected void Btn_CancelAddOption_Click(object sender, EventArgs e)
    {
        txtOptionNaam.Text = "";
        txtOptionPrijs.Text = "";
        divNewMeal.Visible = true;
        divNewOptionMeal.Visible = false;
    }

    //Nieuwe optie toevoegen
    protected void btn_SaveNewOption_Click(object sender, EventArgs e)
    {
        resetAddOptionPage();
        if (ValidateNaam(txtNewOptionname) && ValidatePrijs(txtNewOptionprice))
        {
            string optionName = txtNewOptionname.Text;
            decimal optionPrice = 0.0M;
            decimal.TryParse(txtNewOptionprice.Text, out optionPrice);
            List<string> campusList = new List<string>();

            campusList = GetSelectedCampussen(cblNewOptioncampus);
            Option newOption = new Option(optionName, true, optionPrice, campusList);

            if (ddlNewAddMeal.SelectedValue != "-1")
            {
                int addMeal = Database.DatabaseInstance.GetMealByMealname(ddlNewAddMeal.SelectedValue).Mealid;
                AddNewOptionWithMeal(newOption, addMeal);
            }
            else
            {
                Database.DatabaseInstance.insertOptions(newOption.Optionname, newOption.Enabled, newOption.Price, newOption.Campussen);
            }
              
            Btn_Adminsitratie_Click(sender, e);
            divNewOptionAlert.Visible = true;
        }
        else if (!ValidateNaam(txtNewOptionname))
        {
            lblNewOptionnameError.Visible = true;
            lblNewOptionnameError.Text = "Gelieve een naam in te vullen";
            lblNewOptionnameError.Style.Add("color", "red");
        }
        else if (!ValidatePrijs(txtNewOptionprice))
        {
            lblNewOptionpriceError.Visible = true;
            lblNewOptionpriceError.Text = "Gelieve een geldige prijs in te vullen";
            lblNewOptionpriceError.Style.Add("color", "red");
        }
    }

    //overzicht Maaltijd & Opties
    protected void ListMeals_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ListMeals.PageIndex = e.NewPageIndex;
        PopulateListMeals();
    }
    protected void ListOptions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ListOptions.PageIndex = e.NewPageIndex;
        PopulateListOptions();
    }



    //Custom validation is nodig, omdat de tabnavigatie anders niet werkt
    private bool ValidateNaam(TextBox txtNaam)
    {
        return !string.IsNullOrEmpty(txtNaam.Text);
    }

    private bool ValidatePrijs(TextBox txtPrijs)
    {
        decimal price = 0M;

        if (!String.IsNullOrEmpty(txtPrijs.Text) && decimal.TryParse(txtPrijs.Text, out price))
        {
            return true;
        }
        return false;
    }

    private void AddNewOptionWithMeal(Option option, int mealId)
    {
        Database.DatabaseInstance.insertOptions(option.Optionname, option.Enabled, option.Price, option.Campussen);
        int addedOption = Database.DatabaseInstance.GetLatestOption();
        Database.DatabaseInstance.insertMeal_options(mealId, addedOption);
    }

    private void AddNewMealWithOption(Meal newMeal, int optionId)
    {
        Database.DatabaseInstance.insertMeal(newMeal.Mealname, newMeal.Price, newMeal.Enabled, newMeal.CampusList);

        int addedMeal = Database.DatabaseInstance.GetLatestMeal();

        Database.DatabaseInstance.insertMeal_options(addedMeal, optionId);
    }

    private void PopulateListMeals()
    {
        ListMeals.DataSource = Database.DatabaseInstance.GetVisibleMealsByCampus((string)Session["campus"]);
        ListMeals.DataBind();
    }
    private void PopulateListOptions()
    {
        ListOptions.DataSource = Database.DatabaseInstance.GetVisibleOptionsByCampus((string)Session["campus"]);
        ListOptions.DataBind();
    }

    protected void ListMeals_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string[] args = new string[2];
        args = e.CommandArgument.ToString().Split(';');
        int rowIndex = Convert.ToInt32(args[0]);
        

        if (args.Length > 1)
        {
            Session["editMeal"] = Database.DatabaseInstance.GetVisibleMealsByCampus((string)Session["campus"])[rowIndex];
            if (args[1].Equals("EDIT"))
                StartEditMeal((Meal)Session["editMeal"]);
            else if (args[1].Equals("DELETE"))
                StartDeleteMeal((Meal)Session["editMeal"]);
        }
    }

    private void StartDeleteMeal(Meal editMeal)
    {
        Database.DatabaseInstance.SetMealInivisble(editMeal.Mealid);
        PopulateListMeals();
    }

    #region EditMeals
    private void StartEditMeal(Meal meal)
    {
        divOverzichtMaaltijd.Visible = false;
        
        txtEditNewOption.Text = "";
        txtEditNewPrijs.Text = "";

        txtEditMealname.Text = meal.Mealname;
        txtEditMealprice.Text = meal.Price.ToString();
        AddCheckboxCampus(cblEditMealcampus);
        FillEditCheckbox(meal);
        chbEnabled.Checked = meal.Enabled;
        ddlEditDeleteOpties.Items.Clear();
        ddlEditDeleteOpties.Items.Add(new ListItem { Value = "-1", Text = "Selecteer een te verwijderen optie" });
        ddlEditDeleteOpties.DataSource = Database.DatabaseInstance.GetOptionsForMeal(meal.Mealid,Session["campus"].ToString());
        ddlEditDeleteOpties.DataBind();
        ddlEditAddOption.Items.Clear();
        ddlEditAddOption.Items.Add(new ListItem { Value = "-1", Text = "Selecteer een toe te voegen optie" });
        ddlEditAddOption.DataSource = Database.DatabaseInstance.GetVisibleOptions();
        ddlEditAddOption.DataBind();
        divEditPagina.Visible = true;
        resetEditPage();
    }

    private void AddCheckboxCampus(CheckBoxList cblList)
    {
        cblList.Items.Clear();
        List<String> listCampus = Database.DatabaseInstance.GetCampus();
        foreach (string campus in listCampus)
        {
            cblList.Items.Add(new ListItem() { Value = campus, Text = campus });
        }
    }

    private void FillEditCheckbox(Meal meal)
    {
        List<string> listCampus = Database.DatabaseInstance.GetCampussenByMealId(meal.Mealid);

        foreach (string campus in listCampus)
        {
            for (int i = 0; i < cblEditMealcampus.Items.Count; i++)
            {
                if (campus.Equals(cblEditMealcampus.Items[i].Text))
                    cblEditMealcampus.Items[i].Selected = true;
            }
        }
    }
    private void FillEditCheckbox(Option option)
    {
        List<string> listCampus = Database.DatabaseInstance.GetCampussenByOptionid(option.Optionid);

        foreach (string campus in listCampus)
        {
            for (int i = 0; i < cblEditOptioncampus.Items.Count; i++)
            {
                if (campus.Equals(cblEditOptioncampus.Items[i].Text))
                    cblEditOptioncampus.Items[i].Selected = true;
            }
        }
    }

    private void EditConfirmation()
    {
        divEditPagina.Visible = false; 
        divEditNewOption.Visible = false;
        divEditConfirmation.Visible = true;
        PopulateListMeals();
        divOverzichtMaaltijd.Visible = true;
    }

    protected void SaveEditMeal(object sender, EventArgs e)
    {
        resetEditPage();
        if (!string.IsNullOrEmpty(txtEditMealname.Text) && ValidatePrijs(txtEditMealprice))
        {
            Meal editMeal = (Meal)Session["editMeal"];
            int addOptionid = Database.DatabaseInstance.GetOptionByOptionName(ddlEditAddOption.SelectedValue).Optionid;
            if (!IsAlreadyLinked(editMeal.Mealid, addOptionid))
            {
                int mealId = editMeal.Mealid;
                string mealName = txtEditMealname.Text;

                string newPrice = txtEditMealprice.Text;
                decimal mealPrice = 0.0M;
                decimal.TryParse(newPrice, out mealPrice);

                List<string> selectedCampus = GetSelectedCampussen(cblEditMealcampus);
                List<string> unselectedCampus = GetUnselectedCampussen(cblEditMealcampus);
                bool enabled = chbEnabled.Checked;

                AddSelectedOption(editMeal.Mealid, addOptionid);
                DeleteSelectedOption(editMeal);

                Database.DatabaseInstance.updateMeal(mealId, mealName, mealPrice, enabled,  selectedCampus);
                Database.DatabaseInstance.DeleteMealCampusLink(mealId, unselectedCampus);
                //goback with confirmation
                EditConfirmation();
            }

            else
            {
                txtEditMealprice.Text = editMeal.Price.ToString();
                lblEditAddOptionError.Text = "Deze optie is al gelinkt aan deze maaltijd";
                lblEditAddOptionError.Style.Add("color", "red");
            }

        }
        if (string.IsNullOrEmpty(txtEditMealname.Text))
        {
            lblEditMealnameError.Text = "Gelieve een geldige naam in te geven";
            lblEditMealnameError.Style.Add("color", "red");
            txtEditMealname.Text = "";
        }
        if (!ValidatePrijs(txtEditMealprice))
        {
            lblEditMealpriceError.Text = "Gelieve een geldige prijs in te geven";
            lblEditMealpriceError.Style.Add("color", "red");
            txtEditMealprice.Text = "";
        }
    }



    private void AddSelectedOption(int mealid, int optionid)
    {
        if (ddlEditAddOption.SelectedValue != "-1" && ddlEditAddMeal.SelectedValue != "-1")
        {
            lblEditAddOptionError.Text = "";
            Database.DatabaseInstance.insertMeal_options(mealid, optionid);
        }
    }

    private bool IsAlreadyLinked(int mealid, int optionid)
    {
        bool isLinked = false;
        List<Option> linkedOptions = Database.DatabaseInstance.GetOptionsForMeal(mealid, Session["campus"].ToString());
        for (int i = 0; i < linkedOptions.Count; i++)
        {
            if (optionid == linkedOptions[i].Optionid)
            {
                isLinked = true;
            }
        }

        return isLinked;
    }

    private void DeleteSelectedOption(Meal meal)
    {
        if (ddlEditDeleteOpties.SelectedValue != "-1")
        {
            int optionId = Database.DatabaseInstance.GetOptionByOptionName(ddlEditDeleteOpties.SelectedValue).Optionid;
            Database.DatabaseInstance.deleteMeal_options(meal.Mealid, optionId);
        }
    }

    protected void InitAddOptionToEdit(Object sender, EventArgs e)
    {
        divEditPagina.Visible = false;
        divEditNewOption.Visible = true;
    }

    protected void AddOptionToEdit(Object sender, EventArgs e)
    {
        resetEditPage();
        if (ValidateNaam(txtEditNewOption) && ValidatePrijs(txtEditNewPrijs))
        {
            Meal meal = (Meal)Session["editMeal"];
            string optionName = txtEditNewOption.Text;
            decimal optionPrice = 0.0M;
            decimal.TryParse(txtEditNewPrijs.Text, out optionPrice);

            Database.DatabaseInstance.insertOptions(optionName, true, optionPrice, meal.CampusList);
            int addedOption = Database.DatabaseInstance.GetLatestOption();
            Database.DatabaseInstance.insertMeal_options(meal.Mealid, addedOption);
            EditConfirmation();
            
        }
        if (!ValidateNaam(txtEditNewOption))
        {
            lblEditNewOptionNaamError.Text = "Gelieve een geldige naam in te geven";
            lblEditNewOptionNaamError.Style.Add("color", "red");
            txtEditNewOption.Text = "";
        }
        if (!ValidatePrijs(txtEditNewPrijs))
        {
            lblEditNewOptionPrijsError.Text = "Gelieve een geldige prijs in te geven";
            lblEditNewOptionPrijsError.Style.Add("color", "red");
            txtEditNewPrijs.Text = "";
        }
    }

    protected void CancelEditMeal(object sender, EventArgs e)
    {
        divEditPagina.Visible = false;
        divOverzichtMaaltijd.Visible = true;
    }

    protected void BackToEditMeal(object sender, EventArgs e)
    {
        txtEditNewOption.Text = "";
        txtEditNewPrijs.Text = "";
        divEditNewOption.Visible = false;
        divEditPagina.Visible = true;
    }

    private List<string> GetSelectedCampussen(CheckBoxList cblList)
    {
        List<string> campusList = new List<string>();

        foreach (ListItem campus in cblList.Items)
        {
            if (campus.Selected)
            {
                campusList.Add(campus.Value);
            }
        }

        return campusList;
    }
    private List<string> GetUnselectedCampussen(CheckBoxList cblList)
    {
        List<string> campusList = new List<string>();

        foreach (ListItem campus in cblList.Items)
        {
            if (!campus.Selected)
            {
                campusList.Add(campus.Text);
            }
        }

        return campusList;
    }
    #endregion Editmeals

    #region EditOptions
    protected void ListOptions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string[] args = new string[2];
        args = e.CommandArgument.ToString().Split(';');
        int rowIndex = Convert.ToInt32(args[0]);


        if (args.Length > 1)
        {
            Session["editOption"] = Database.DatabaseInstance.GetVisibleOptionsByCampus((string)Session["campus"])[rowIndex];
            if (args[1].Equals("EDIT"))
                StartEditOption((Option)Session["editOption"]);
            else if (args[1].Equals("DELETE"))
                StartDeleteOption((Option)Session["editOption"]);
        }
    }

    private void StartEditOption(Option option)
    {
        divOverzichtOpties.Visible = false;

        txtNewMealNaam.Text = "";
        txtNewMealPrijs.Text = "";

        txtEditOptionname.Text = option.Optionname;
        txtEditOptionprice.Text = option.Price.ToString();
        AddCheckboxCampus(cblEditOptioncampus);
        FillEditCheckbox(option);
        chbOptionEnabled.Checked = option.Enabled;
        ddlEditDeleteMeal.Items.Clear();
        ddlEditDeleteMeal.Items.Add(new ListItem { Value = "-1", Text = "Selecteer een te verwijderen maaltijd" });
        ddlEditDeleteMeal.DataSource = Database.DatabaseInstance.GetMealsForOption(option.Optionid, Session["campus"].ToString());
        ddlEditDeleteMeal.DataBind();
        ddlEditAddMeal.Items.Clear();
        ddlEditAddMeal.Items.Add(new ListItem { Value = "-1", Text = "Selecteer een toe te voegen maaltijd" });
        ddlEditAddMeal.DataSource = Database.DatabaseInstance.GetVisibleMeals();
        ddlEditAddMeal.DataBind();
        divEditOption.Visible = true;
        resetEditPage();
    }

    protected void SaveEditOption(object sender, EventArgs e)
    {
        resetEditOptionPage();
        if (!string.IsNullOrEmpty(txtEditOptionname.Text) && ValidatePrijs(txtEditOptionprice))
        {
            Option editOption = (Option)Session["editOption"];
            int addMealid = Database.DatabaseInstance.GetMealByMealname(ddlEditAddMeal.SelectedValue).Mealid;
            if (!IsAlreadyLinked(addMealid, editOption.Optionid))
            {
                int optionId = editOption.Optionid;
                string optionName = txtEditOptionname.Text;

                string newPrice = txtEditOptionprice.Text;
                decimal optionPrice = 0.0M;
                decimal.TryParse(newPrice, out optionPrice);

                List<string> selectedCampus = GetSelectedCampussen(cblEditOptioncampus);
                List<string> unselectedCampus = GetUnselectedCampussen(cblEditOptioncampus);
                bool enabled = chbOptionEnabled.Checked;

                AddSelectedOption(addMealid, editOption.Optionid);

                DeleteSelectedMeal(editOption);
                Database.DatabaseInstance.updateOption(optionId, optionName, enabled, optionPrice, selectedCampus);
                Database.DatabaseInstance.DeleteOptionCampusLink(optionId, unselectedCampus);
                //goback with confirmation
                EditOptionConfirmation();
            }
        }
        else if(string.IsNullOrEmpty(txtEditOptionname.Text))
        {
            lblEditOptionnameError.Text = "Gelieve een naam in te geven";
            lblEditOptionnameError.Style.Add("color", "red");
        }
        else if (ValidatePrijs(txtEditOptionprice))
        {
            lblEditOptionpriceError.Text = "Gelieve een geldige prijs in te geven";
            lblEditOptionpriceError.Style.Add("color", "red");

        }
    }

    private void DeleteSelectedMeal(Option option)
    {
        if (ddlEditDeleteMeal.SelectedValue != "-1")
        {
            int mealid = Database.DatabaseInstance.GetMealByMealname(ddlEditDeleteMeal.SelectedValue).Mealid;
            Database.DatabaseInstance.deleteMeal_options(mealid, option.Optionid);
        }
    }

    private void EditOptionConfirmation()
    {
        divEditOption.Visible = false;
        divEditOptionNewMeal.Visible = false;
        divEditOptionConfirmation.Visible = true;
        PopulateListOptions();
        divOverzichtOpties.Visible = true;
    }

    protected void CancelOptionEdit(object sender, EventArgs e)
    {
        resetEditOptionPage();
        divEditOption.Visible = false;
        divOverzichtOpties.Visible = true;
    }

    protected void AddNewMealToEditOption(object sender, EventArgs e)
    {
        resetEditOptionPage();
        if(ValidateNaam(txtEditOptionname) && ValidatePrijs(txtEditOptionprice))
        {
            AddCheckboxCampus(cblNewMealCampus);
            ddlNewOpties.Items.Clear();
            ddlNewOpties.Items.Add(new ListItem { Value = "-1", Text = "Selecteer een toe te voegen optie" });
            ddlNewOpties.DataSource = Database.DatabaseInstance.GetVisibleOptions();
            ddlNewOpties.DataBind();
            divEditOption.Visible = false;
            divEditOptionNewMeal.Visible = true;
        }
        else if (!ValidateNaam(txtEditOptionname))
        {
            lblEditOptionnameError.Text = "Gelieve een naam in te geven";
            lblEditOptionnameError.Style.Add("color", "red");
        }
        else if (!ValidatePrijs(txtEditOptionprice))
        {
            lblEditOptionpriceError.Text = "Gelieve een geldige prijs in te geven";
            lblEditOptionpriceError.Style.Add("color", "red");

        }
    }

    protected void CancelNewMealToEditOption(object sender, EventArgs e)
    {
        resetAddNewMealToOption();
        divEditOption.Visible = true;
        divEditOptionNewMeal.Visible = false;
        txtNewMealNaam.Text = "";
        txtNewMealPrijs.Text = "";
        lblNewMealNaamError.Text = "";
        lblNewMealPrijsError.Text = "";
    }

    protected void SaveNewMealToEditOption(object sender, EventArgs e)
    {
        resetAddNewMealToOption();
        if (ValidateNaam(txtNewMealNaam) && ValidatePrijs(txtNewMealPrijs))
        {
            decimal mealPrice = 0M;
            decimal.TryParse(txtNewMealPrijs.Text, out mealPrice);
            List<string> campusList = GetSelectedCampussen(cblNewMealCampus);
            Meal newMeal = new Meal(txtNewMealNaam.Text, mealPrice, true, campusList);
            
            Database.DatabaseInstance.insertMeal(newMeal.Mealname, newMeal.Price, newMeal.Enabled, newMeal.CampusList);
            int newMealid = Database.DatabaseInstance.GetLatestMeal();

            if (ddlNewOpties.SelectedValue != "-1")
            {
                Option addOption = Database.DatabaseInstance.GetOptionByOptionName(ddlNewOpties.SelectedValue); 
                Database.DatabaseInstance.insertMeal_options(newMealid, addOption.Optionid);
            }

            Option editOption = (Option)Session["editOption"];
            Database.DatabaseInstance.insertMeal_options(newMealid, editOption.Optionid);
            SaveEditOption(sender, e);
        }
        if (!ValidateNaam(txtNewMealNaam))
        {
            lblNewMealNaamError.Text = "Gelieve een naam in te geven";
            lblNewMealNaamError.Style.Add("color", "red");
            txtNewMealNaam.Text = "";
        }
        if (!ValidatePrijs(txtNewMealPrijs))
        {
            lblNewMealPrijsError.Text = "Gelieve een geldige prijs in te geven";
            lblNewMealPrijsError.Style.Add("color", "red");
            txtNewMealPrijs.Text = "";
        }
    }

    private void resetAddNewMealToOption()
    {
        lblNewMealNaamError.Text = "";
        lblNewMealPrijsError.Text = "";
    }

    private void StartDeleteOption(Option option)
    {
        Database.DatabaseInstance.setOptionInvisible(option.Optionid);
        PopulateListOptions();
    }
    #endregion EditOptions

    protected void BtnCancelNewMeal_Click(object sender, EventArgs e)
    {
        adminIndex.Visible = true;
        resetAddMealPage();
        resetAddOptionPage();
        addNewMealPage.Visible = false;
        addNewOptionPage.Visible = false;
    }
}