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
                    txtDescription.Disabled = false;
                    ddlType.Enabled = true;
                    txtUserName.Enabled = false;
                    txtPassword.Enabled = true;
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
                    txtDescription.Disabled = true;
                    ddlType.Enabled = false;
                    txtUserName.Enabled = false;
                    txtPassword.Enabled = false;
                    break;
            }
        }

        private void Messages(string message)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "messageBox", "alert('" + message + "');", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowInformationMesage('" + message + "');", true);
            }
            catch
            {
            }
        }

        protected void gvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "ViewData":
                        Session["status"] = "ViewData";
                        mvParent.ActiveViewIndex = 1;
                        ViewState["index"] = e.CommandArgument.ToString();
                        Session["index"] = e.CommandArgument.ToString();
                        ViewCompanyData();
                        ControllersHandler(CommandMood.View);
                        HandleButtons(CommandMood.View);
                        break;

                    case "EditData":
                        Session["status"] = "EditData";
                        mvParent.ActiveViewIndex = 1;
                        ViewState["index"] = e.CommandArgument.ToString();
                        Session["index"] = e.CommandArgument.ToString();
                        EditCompanyData();
                        ControllersHandler(CommandMood.Edit);
                        HandleButtons(CommandMood.Edit);
                        break;

                    case "DeleteData":
                        //RecordView(DataEntryMode.Delete);
                        ViewState["index"] = e.CommandArgument.ToString();
                        DeleteCompanyData();
                        ResetControllers();
                        LoadData();
                        break;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                return;
            }
        }

        private void DeleteCompanyData()
        {
            bool result = false;
            try
            {
                GridViewRow oGridViewRow = gvCompany.Rows[Convert.ToInt32(ViewState["index"])];
                Label lblCompanyId = (Label)oGridViewRow.FindControl("lblCompanyId");
                CompanyDTO oCompanyDTO = new CompanyDTO();

                oCompanyDTO.CompanyId = Convert.ToInt32(lblCompanyId.Text);
                oCompanyDTO.Status = (int)Status.Inactive;
                oCompanyDTO.ModifiedUser = Session["UserID"].ToString();
                oCompanyDTO.ModifiedDateTime = DateTime.Now;
                oCompanyDTO.ModifiedMachine = Session["UserMachine"].ToString();

                result = oWebApiCalls.DeleteCompanyData(oCompanyDTO);

                if (result == true)
                {
                    ResetControllers();
                    Messages("Company Deleted Successfully!!");
                }
                else
                {
                    Messages("Connection Error!");
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                throw ex;
            }
        }

        private void EditCompanyData()
        {
            try
            {
                LoadDropDownList();
                GridViewRow oGridViewRow = gvCompany.Rows[Convert.ToInt32(ViewState["index"])];
                Label lblCompanyId = (Label)oGridViewRow.FindControl("lblCompanyId");
                Session["CompanyId"] = lblCompanyId.Text;
                CompanyDTO oCompanyDTO = new CompanyDTO();
                oCompanyDTO = oWebApiCalls.CompanySearchById(Convert.ToInt32(lblCompanyId.Text));

                txtName.Text = oCompanyDTO.Name;
                txtAddress.Text = oCompanyDTO.Address;
                txtTelephone.Text = oCompanyDTO.Telephone;
                txtEmail.Text = oCompanyDTO.Email;
                txtDescription.InnerText = oCompanyDTO.Description;
                ddlType.SelectedValue = Convert.ToString((int)oCompanyDTO.Type);
                txtNoOfShares.Text = Convert.ToString(oCompanyDTO.NumberOfShares);
                txtSharePrice.Text = Convert.ToString(oCompanyDTO.SharePrice);
                ddlStatus.SelectedValue = Convert.ToString((int)oCompanyDTO.Status);

                string DecryptedPwd = Cryptography.Encryption.Decrypt(oCompanyDTO.Password, oCompanyDTO.UserName);

                txtUserName.Text = oCompanyDTO.UserName;
                txtPassword.Text = DecryptedPwd;

                ControllersHandler(CommandMood.Edit);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                throw ex;
            }
        }

        private void ViewCompanyData()
        {
            try
            {
                LoadDropDownList();
                GridViewRow oGridViewRow = gvCompany.Rows[Convert.ToInt32(ViewState["index"])];
                Label lblCompanyId = (Label)oGridViewRow.FindControl("lblCompanyId");
                Session["CompanyId"] = lblCompanyId.Text;
                CompanyDTO oCompanyDTO = new CompanyDTO();
                oCompanyDTO = oWebApiCalls.CompanySearchById(Convert.ToInt32(lblCompanyId.Text));

                txtName.Text = oCompanyDTO.Name;
                txtAddress.Text = oCompanyDTO.Address;
                txtTelephone.Text = oCompanyDTO.Telephone;
                txtEmail.Text = oCompanyDTO.Email;
                txtDescription.InnerText = oCompanyDTO.Description;
                ddlType.SelectedValue = Convert.ToString((int)oCompanyDTO.Type);
                txtNoOfShares.Text = Convert.ToString(oCompanyDTO.NumberOfShares);
                txtSharePrice.Text = Convert.ToString(oCompanyDTO.SharePrice);
                ddlStatus.SelectedValue = Convert.ToString((int)oCompanyDTO.Status);

                string DecryptedPwd = Cryptography.Encryption.Decrypt(oCompanyDTO.Password, oCompanyDTO.UserName);

                txtUserName.Text = oCompanyDTO.UserName;
                txtPassword.Text = DecryptedPwd;

                ControllersHandler(CommandMood.View);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                throw ex;
            }
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
                #region Company

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

                string EncryptedPwd = Cryptography.Encryption.Encrypt(txtPassword.Text, txtUserName.Text);

                oCompanyDTOs.Password = EncryptedPwd;
                oCompanyDTOs.CreatedUser = Session["UserID"].ToString();
                oCompanyDTOs.CreatedDateTime = DateTime.Now;
                oCompanyDTOs.CreatedMachine = Session["UserMachine"].ToString();
                oCompanyDTOs.ModifiedUser = Session["UserID"].ToString();
                oCompanyDTOs.ModifiedDateTime = DateTime.Now;
                oCompanyDTOs.ModifiedMachine = Session["UserMachine"].ToString();

                #endregion Company

                #region Login

                LoginDTO oLoginDTOs = new LoginDTO();

                oLoginDTOs.UserName = txtUserName.Text;
                oLoginDTOs.Password = EncryptedPwd;
                oLoginDTOs.UserType = Convert.ToInt32(Session["UserType"].ToString());
                oLoginDTOs.LoginAttempts = 1;
                oLoginDTOs.CreatedUser = Session["UserID"].ToString();
                oLoginDTOs.CreatedDateTime = DateTime.Now;
                oLoginDTOs.CreatedMachine = Session["UserMachine"].ToString();
                oLoginDTOs.ModifiedUser = Session["UserID"].ToString();
                oLoginDTOs.ModifiedDateTime = DateTime.Now;
                oLoginDTOs.ModifiedMachine = Session["UserMachine"].ToString();

                #endregion Login

                CompanyMaintanance oCompanyMaintanance = new CompanyMaintanance();
                oCompanyMaintanance.oCompanyDTO = oCompanyDTOs;
                oCompanyMaintanance.oLoginDTO = oLoginDTOs;

                result = oWebApiCalls.InsertCompanyData(oCompanyMaintanance);

                if (result == true)
                {
                    ResetControllers();
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            mvParent.ActiveViewIndex = 1;
            ControllersHandler(CommandMood.Edit);
            HandleButtons(CommandMood.Edit);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCompanyData();
            mvParent.ActiveViewIndex = 0;
            LoadData();
        }

        private void UpdateCompanyData()
        {
            bool result = false;
            try
            {
                CompanyDTO oCompanyDTOs = new CompanyDTO();
                oCompanyDTOs.CompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
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

                string EncryptedPwd = Cryptography.Encryption.Encrypt(txtPassword.Text, txtUserName.Text);

                oCompanyDTOs.Password = EncryptedPwd;
                oCompanyDTOs.ModifiedUser = Session["UserID"].ToString();
                oCompanyDTOs.ModifiedDateTime = DateTime.Now;
                oCompanyDTOs.ModifiedMachine = Session["UserMachine"].ToString();

                #region Login

                LoginDTO oLoginDTOs = new LoginDTO();

                oLoginDTOs.UserName = txtUserName.Text;
                oLoginDTOs.Password = EncryptedPwd;
                oLoginDTOs.UserType = Convert.ToInt32(Session["UserType"].ToString());
                oLoginDTOs.LoginAttempts = 1;
                oLoginDTOs.ModifiedUser = Session["UserID"].ToString();
                oLoginDTOs.ModifiedDateTime = DateTime.Now;
                oLoginDTOs.ModifiedMachine = Session["UserMachine"].ToString();

                #endregion Login

                CompanyMaintanance oCompanyMaintanance = new CompanyMaintanance();
                oCompanyMaintanance.oCompanyDTO = oCompanyDTOs;
                oCompanyMaintanance.oLoginDTO = oLoginDTOs;

                result = oWebApiCalls.UpdateCompanyData(oCompanyMaintanance);

                if (result == true)
                {
                    ResetControllers();
                    Messages("Company Updated Successfully!!");
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

        private void ResetControllers()
        {
            txtAddress.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            txtDescription.InnerText = string.Empty;
            txtEmail.Text = string.Empty;
            txtName.Text = string.Empty;
            txtNoOfShares.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtSharePrice.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            txtUserName.Text = string.Empty;
            ddlCompanyType.SelectedIndex = 0;
            //ddlStatus.SelectedIndex = 0;
            ddlType.SelectedIndex = 0;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ResetControllers();
                mvParent.ActiveViewIndex = 1;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            try
            {
                mvParent.ActiveViewIndex = 0;
                LoadData();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CompanyDTO oCompanyDTO = new CompanyDTO();
                string companyName = string.Empty;
                int companyType = 0;

                if (txtCompanyName.Text.Length > 0)
                {
                    companyName = txtCompanyName.Text;
                }
                if (ddlCompanyType.SelectedIndex > 0)
                {
                    companyType = ddlCompanyType.SelectedIndex;
                }

                oCompanyDTO = oWebApiCalls.CompanySearchByFilters(companyName, companyType);

                txtName.Text = oCompanyDTO.Name;
                txtAddress.Text = oCompanyDTO.Address;
                txtTelephone.Text = oCompanyDTO.Telephone;
                txtEmail.Text = oCompanyDTO.Email;
                txtDescription.InnerText = oCompanyDTO.Description;
                ddlType.SelectedValue = Convert.ToString((int)oCompanyDTO.Type);
                txtNoOfShares.Text = Convert.ToString(oCompanyDTO.NumberOfShares);
                txtSharePrice.Text = Convert.ToString(oCompanyDTO.SharePrice);
                ddlStatus.SelectedValue = Convert.ToString((int)oCompanyDTO.Status);

                string DecryptedPwd = Cryptography.Encryption.Decrypt(oCompanyDTO.Password, oCompanyDTO.UserName);

                txtUserName.Text = oCompanyDTO.UserName;
                txtPassword.Text = DecryptedPwd;

                ControllersHandler(CommandMood.View);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }
    }
}