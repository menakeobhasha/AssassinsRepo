using SMS.Business;
using SMS.Common;
using SMS.Model;
using StockMarketSimulation.Common;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockMarketSimulation.Pages
{
    public partial class Company : System.Web.UI.Page
    {
        private CompanyBL oCompanyBL = new CompanyBL();
        private SMS.Common.Common oCommon = new SMS.Common.Common(); 
        private WebApiCalls oWebApiCalls = new WebApiCalls();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mvParent.ActiveViewIndex = 0;
                LoadData();
            }
            else
            if (Page.IsPostBack)
                return;
        }

        private void LoadData()
        {
            LoadGrid();
            LoadDropDownList();
            //txtDate.Text = System.DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void LoadDropDownList()
        {
            try
            {
                ddlCompanyType.DataSource = oCommon.ComboboxDataBind(typeof(CompanyType));
                ddlCompanyType.DataTextField = "TextField";
                ddlCompanyType.DataValueField = "ValueField";
                ddlCompanyType.DataBind();

                ddlStatus.DataSource = oCommon.ComboboxDataBind(typeof(Status));
                ddlStatus.DataTextField = "TextField";
                ddlStatus.DataValueField = "ValueField";
                ddlStatus.DataBind();

                ddlType.DataSource = oCommon.ComboboxDataBind(typeof(CompanyType));
                ddlType.DataTextField = "TextField";
                ddlType.DataValueField = "ValueField";
                ddlType.DataBind();

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        private void LoadGrid()
        {
            List<CompanyDTO> oCompanyDTOs = new List<CompanyDTO>();
            
            oCompanyDTOs = oWebApiCalls.GetCompanyData();
            gvCompany.DataSource = oCompanyDTOs;
            gvCompany.DataBind();
        }

        protected void btnAddCompany_Click(object sender, EventArgs e)
        {
            mvParent.ActiveViewIndex = 1;
            LoadDropDownList();
            HandleButtons(CommandMood.Add);
            ControllersHandler(CommandMood.Add);
        }

        // Handle Buttons
        private void HandleButtons(CommandMood oCommandMood)
        {
            switch (oCommandMood)
            {
                case CommandMood.Add:
                    btnAdd.Visible = true;
                    btnCancle.Visible = true;
                    btnClear.Visible = true;
                    btnUpdate.Visible = false;
                    btnEdit.Visible = false;
                    break;

                case CommandMood.Edit:
                    btnAdd.Visible = false;
                    btnCancle.Visible = true;
                    btnClear.Visible = true;
                    btnUpdate.Visible = true;
                    btnEdit.Visible = false;
                    break;

                case CommandMood.View:

                    btnAdd.Visible = false;
                    btnCancle.Visible = true;
                    btnClear.Visible = true;
                    btnUpdate.Visible = false;
                    btnEdit.Visible = true;
                    break;
            }
        }

        //Controllers Handler
        private void ControllersHandler(CommandMood oCommandMood)
        {
            switch (oCommandMood)
            {
                case CommandMood.Add:
                    txtAddress.Enabled = true;
                    txtCompanyName.Enabled = true;
                    txtEmail.Enabled = true;
                    txtName.Enabled = true;
                    txtNoOfShares.Enabled = true;
                    txtSharePrice.Enabled = true;
                    txtTelephone.Enabled = true;
                    ddlStatus.Enabled = false;
                    ddlStatus.SelectedValue = Convert.ToString((int)Status.Active);
                    
                    break;

                case CommandMood.Edit:
                    txtAddress.Enabled = true;
                    txtCompanyName.Enabled = true;
                    txtEmail.Enabled = true;
                    txtName.Enabled = true;
                    txtNoOfShares.Enabled = true;
                    txtSharePrice.Enabled = true;
                    txtTelephone.Enabled = true;
                    ddlStatus.Enabled = false;
                    break;

                case CommandMood.View:
                    txtAddress.Enabled = false;
                    txtCompanyName.Enabled = false;
                    txtEmail.Enabled = false;
                    txtName.Enabled = false;
                    txtNoOfShares.Enabled = false;
                    txtSharePrice.Enabled = false;
                    txtTelephone.Enabled = false;
                    ddlStatus.Enabled = false;
                    break;
            }
        }

        private void Messages(string message)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + message + "');", true);
            }
            catch
            {
            }
        }

        protected void gvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //try
            //{
            //    GridViewRow oGridViewRow = gvRawMaterialListdata.Rows[Convert.ToInt32(e.CommandArgument)];
            //    Label lblRawMaterialDetailNo = (Label)oGridViewRow.FindControl("lblRawMaterialDetailNo");
            //    Label lblLNo = (Label)oGridViewRow.FindControl("lblLNo");
            //    Label lblQuantity = (Label)oGridViewRow.FindControl("lblQuantity");
            //    Label lblMType = (Label)oGridViewRow.FindControl("lblMType");
            //    Label lblRemarks = (Label)oGridViewRow.FindControl("lblRemarks");
            //    List<RawMaterialDetailsDTO> oRawMaterialDetailsDTOs = new List<RawMaterialDetailsDTO>();

            //    switch (e.CommandName)
            //    {

            //        case "DeleteDate":



            //            if (Session["oRawMaterialDetailsDTOs"] != null)
            //            {
            //                oRawMaterialDetailsDTOs = (List<RawMaterialDetailsDTO>)Session["oRawMaterialDetailsDTOs"];
            //            }
            //            RawMaterialDetailsDTO oRawMaterialDetailsDTO = oRawMaterialDetailsDTOs.Find(c => c.RawMaterialDetailNo == Convert.ToInt32(lblRawMaterialDetailNo.Text));
            //            oRawMaterialDetailsDTOs.Remove(oRawMaterialDetailsDTO);
            //            Session["oRawMaterialDetailsDTOs"] = oRawMaterialDetailsDTOs;

            //            LoadRawDataGrid();
            //            mvParent.ActiveViewIndex = 1;
            //            break;
            //        case "EditDate":

            //            ddlMaterialType.SelectedIndex = Convert.ToInt32(lblMType.Text);
            //            txtLotNo.Text = lblLNo.Text;
            //            txtQuentity.Text = lblQuantity.Text;
            //            txtRemarks.InnerText = lblRemarks.Text == string.Empty ? "" : lblRemarks.Text;

            //            if (Session["oRawMaterialDetailsDTOs"] != null)
            //            {
            //                oRawMaterialDetailsDTOs = (List<RawMaterialDetailsDTO>)Session["oRawMaterialDetailsDTOs"];
            //            }
            //            RawMaterialDetailsDTO oRawMaterialDetailsDTOS = oRawMaterialDetailsDTOs.Find(c => c.RawMaterialDetailNo == Convert.ToInt32(lblRawMaterialDetailNo.Text));
            //            oRawMaterialDetailsDTOs.Remove(oRawMaterialDetailsDTOS);
            //            Session["oRawMaterialDetailsDTOs"] = oRawMaterialDetailsDTOs;

            //            LoadRawDataGrid();
            //            mvParent.ActiveViewIndex = 1;
            //            break;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    lblError.Text = ex.Message.ToString();
            //    return;
            //}
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            CompanyInsert();
            ControllersHandler(CommandMood.Add);
            HandleButtons(CommandMood.Add);
            mvParent.ActiveViewIndex = 1;
        }

        private void CompanyInsert()
        {
            bool result = false;
            try
            {
                CompanyDTO oCompanyDTOs = new CompanyDTO();

                oCompanyDTOs.Name = txtName.Text;
                oCompanyDTOs.Address = txtAddress.Text;
                oCompanyDTOs.Telephone = txtTelephone.Text;
                oCompanyDTOs.Email = txtEmail.Text;
                oCompanyDTOs.Description = txtDescription.InnerText;
                oCompanyDTOs.Type = Convert.ToInt32(ddlType.SelectedValue.ToString());
                oCompanyDTOs.NumberOfShares = Convert.ToInt32(txtNoOfShares.Text);
                oCompanyDTOs.SharePrice = Convert.ToDecimal(txtSharePrice.Text);
                oCompanyDTOs.Status = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
                oCompanyDTOs.UserName = txtUserName.Text;
                oCompanyDTOs.Password = txtPassword.Text;
                oCompanyDTOs.CreatedUser = Session["UserID"].ToString();
                oCompanyDTOs.CreatedDateTime = DateTime.Now;
                oCompanyDTOs.CreatedMachine = Session["UserMachine"].ToString();
                oCompanyDTOs.ModifiedUser = Session["UserID"].ToString();
                oCompanyDTOs.ModifiedDateTime = DateTime.Now;
                oCompanyDTOs.ModifiedMachine = Session["UserMachine"].ToString();

                result = oWebApiCalls.InsertCompanyData(oCompanyDTOs);

                if(result==true)
                {
                    Messages("Company Inserted Successfully!!");
                }
                else
                {
                    Messages("Connection Error!");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}