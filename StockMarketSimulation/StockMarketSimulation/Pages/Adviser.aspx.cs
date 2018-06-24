using SMS.Business;
using SMS.Common;
using SMS.Model;
using StockMarketSimulation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockMarketSimulation.Pages
{
    public partial class Adviser : System.Web.UI.Page
    {
        private AdviserBL oAdviserBL = new AdviserBL();
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
                ddlAdviserType.DataSource = oCommon.ComboboxDataBind(typeof(AdviserType));
                ddlAdviserType.DataTextField = "TextField";
                ddlAdviserType.DataValueField = "ValueField";
                ddlAdviserType.DataBind();

                ddlStatus.DataSource = oCommon.ComboboxDataBind(typeof(Status));
                ddlStatus.DataTextField = "TextField";
                ddlStatus.DataValueField = "ValueField";
                ddlStatus.DataBind();

                ddlType.DataSource = oCommon.ComboboxDataBind(typeof(AdviserType));
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
            List<AdviserDTO> oAdviserDTOs = new List<AdviserDTO>();

            oAdviserDTOs = oWebApiCalls.GetAdviserData();
            gvAdviser.DataSource = oAdviserDTOs;
            gvAdviser.DataBind();
        }

        protected void btnAddAdviser_Click(object sender, EventArgs e)
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
                    txtAdviserName.Enabled = true;
                    txtEmail.Enabled = true;
                    txtName.Enabled = true;
                    txtJoinDate.Enabled = true;
                    txtTelephone.Enabled = true;
                    ddlStatus.Enabled = false;
                    ddlStatus.SelectedValue = Convert.ToString((int)Status.Active);

                    break;

                case CommandMood.Edit:
                    txtAddress.Enabled = true;
                    txtAdviserName.Enabled = true;
                    txtEmail.Enabled = true;
                    txtName.Enabled = true;
                    txtJoinDate.Enabled = true;
                    txtTelephone.Enabled = true;
                    ddlStatus.Enabled = false;
                    txtDescription.Disabled = false;
                    ddlType.Enabled = true;
                    txtUserName.Enabled = false;
                    txtPassword.Enabled = true;
                    break;

                case CommandMood.View:
                    txtAddress.Enabled = false;
                    txtAdviserName.Enabled = false;
                    txtEmail.Enabled = false;
                    txtName.Enabled = false;
                    txtJoinDate.Enabled = false;
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

        protected void gvAdviser_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        ViewAdviserData();
                        ControllersHandler(CommandMood.View);
                        HandleButtons(CommandMood.View);
                        break;

                    case "EditData":
                        Session["status"] = "EditData";
                        mvParent.ActiveViewIndex = 1;
                        ViewState["index"] = e.CommandArgument.ToString();
                        Session["index"] = e.CommandArgument.ToString();
                        EditAdviserData();
                        ControllersHandler(CommandMood.Edit);
                        HandleButtons(CommandMood.Edit);
                        break;

                    case "DeleteData":
                        //RecordView(DataEntryMode.Delete);
                        ViewState["index"] = e.CommandArgument.ToString();
                        DeleteAdviserData();
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

        private void DeleteAdviserData()
        {
            bool result = false;
            try
            {
                GridViewRow oGridViewRow = gvAdviser.Rows[Convert.ToInt32(ViewState["index"])];
                Label lblAdviserId = (Label)oGridViewRow.FindControl("lblAdviserId");
                AdviserDTO oAdviserDTO = new AdviserDTO();

                oAdviserDTO.AdviserId = Convert.ToInt32(lblAdviserId.Text);
                oAdviserDTO.Status = (int)Status.Inactive;
                oAdviserDTO.ModifiedUser = Session["UserID"].ToString();
                oAdviserDTO.ModifiedDateTime = DateTime.Now;
                oAdviserDTO.ModifiedMachine = Session["UserMachine"].ToString();

                result = oWebApiCalls.DeleteAdviserData(oAdviserDTO);

                if (result == true)
                {
                    ResetControllers();
                    Messages("Adviser Deleted Successfully!!");
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

        private void EditAdviserData()
        {
            try
            {
                LoadDropDownList();
                GridViewRow oGridViewRow = gvAdviser.Rows[Convert.ToInt32(ViewState["index"])];
                Label lblAdviserId = (Label)oGridViewRow.FindControl("lblAdviserId");
                Session["AdviserId"] = lblAdviserId.Text;
                AdviserDTO oAdviserDTO = new AdviserDTO();
                oAdviserDTO = oWebApiCalls.AdviserSearchById(Convert.ToInt32(lblAdviserId.Text));

                txtName.Text = oAdviserDTO.Name;
                txtAddress.Text = oAdviserDTO.Address;
                txtTelephone.Text = oAdviserDTO.Telephone;
                txtEmail.Text = oAdviserDTO.Email;
                txtDescription.InnerText = oAdviserDTO.Description;
                ddlType.SelectedValue = Convert.ToString((int)oAdviserDTO.Type);
                txtJoinDate.Text = oAdviserDTO.JoinDate.ToString("yyyy-MM-dd");
                //txtNoOfShares.Text = Convert.ToString(oAdviserDTO.NumberOfShares);
                //txtSharePrice.Text = Convert.ToString(oAdviserDTO.SharePrice);
                ddlStatus.SelectedValue = Convert.ToString((int)oAdviserDTO.Status);

                string DecryptedPwd = Cryptography.Encryption.Decrypt(oAdviserDTO.Password, oAdviserDTO.UserName);

                txtUserName.Text = oAdviserDTO.UserName;
                txtPassword.Text = DecryptedPwd;

                ControllersHandler(CommandMood.Edit);

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                throw ex;
            }
        }

        private void ViewAdviserData()
        {
            try
            {
                LoadDropDownList();
                GridViewRow oGridViewRow = gvAdviser.Rows[Convert.ToInt32(ViewState["index"])];
                Label lblAdviserId = (Label)oGridViewRow.FindControl("lblAdviserId");
                Session["AdviserId"] = lblAdviserId.Text;
                AdviserDTO oAdviserDTO = new AdviserDTO();
                oAdviserDTO = oWebApiCalls.AdviserSearchById(Convert.ToInt32(lblAdviserId.Text));

                txtName.Text = oAdviserDTO.Name;
                txtAddress.Text = oAdviserDTO.Address;
                txtTelephone.Text = oAdviserDTO.Telephone;
                txtEmail.Text = oAdviserDTO.Email;
                txtDescription.InnerText = oAdviserDTO.Description;
                ddlType.SelectedValue = Convert.ToString((int)oAdviserDTO.Type);
                txtJoinDate.Text = oAdviserDTO.JoinDate.ToString("yyyy-MM-dd");
                //txtNoOfShares.Text = Convert.ToString(oAdviserDTO.NumberOfShares);
                //txtSharePrice.Text = Convert.ToString(oAdviserDTO.SharePrice);
                ddlStatus.SelectedValue = Convert.ToString((int)oAdviserDTO.Status);

                string DecryptedPwd = Cryptography.Encryption.Decrypt(oAdviserDTO.Password, oAdviserDTO.UserName);

                txtUserName.Text = oAdviserDTO.UserName;
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
            AdviserInsert();
            ControllersHandler(CommandMood.Add);
            HandleButtons(CommandMood.Add);
            mvParent.ActiveViewIndex = 1;
        }

        private void AdviserInsert()
        {
            bool result = false;
            try
            {
                #region Adviser
                AdviserDTO oAdviserDTOs = new AdviserDTO();

                oAdviserDTOs.Name = txtName.Text;
                oAdviserDTOs.Address = txtAddress.Text;
                oAdviserDTOs.Telephone = txtTelephone.Text;
                oAdviserDTOs.Email = txtEmail.Text;
                oAdviserDTOs.Description = txtDescription.InnerText;
                oAdviserDTOs.Type = Convert.ToInt32(ddlType.SelectedValue.ToString());
                oAdviserDTOs.JoinDate = Convert.ToDateTime(txtJoinDate.Text);
                //oAdviserDTOs.NumberOfShares = Convert.ToInt32(txtNoOfShares.Text);
                //oAdviserDTOs.SharePrice = Convert.ToDecimal(txtSharePrice.Text);
                oAdviserDTOs.Status = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
                oAdviserDTOs.UserName = txtUserName.Text;

                string EncryptedPwd = Cryptography.Encryption.Encrypt(txtPassword.Text, txtUserName.Text);

                oAdviserDTOs.Password = EncryptedPwd;
                oAdviserDTOs.CreatedUser = Session["UserID"].ToString();
                oAdviserDTOs.CreatedDateTime = DateTime.Now;
                oAdviserDTOs.CreatedMachine = Session["UserMachine"].ToString();
                oAdviserDTOs.ModifiedUser = Session["UserID"].ToString();
                oAdviserDTOs.ModifiedDateTime = DateTime.Now;
                oAdviserDTOs.ModifiedMachine = Session["UserMachine"].ToString();
                #endregion

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

                #endregion

                AdviserMaintanance oAdviserMaintanance = new AdviserMaintanance();
                oAdviserMaintanance.oAdviserDTO = oAdviserDTOs;
                oAdviserMaintanance.oLoginDTO = oLoginDTOs;

                result = oWebApiCalls.InsertAdviserData(oAdviserMaintanance);

                if (result == true)
                {
                    ResetControllers();
                    Messages("Adviser Inserted Successfully!!");
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
            UpdateAdviserData();
            mvParent.ActiveViewIndex = 0;
            LoadData();
        }

        private void UpdateAdviserData()
        {
            bool result = false;
            try
            {
                AdviserDTO oAdviserDTO = new AdviserDTO();
                oAdviserDTO.AdviserId = Convert.ToInt32(Session["AdviserId"].ToString());
                oAdviserDTO.Name = txtName.Text;
                oAdviserDTO.Address = txtAddress.Text;
                oAdviserDTO.Telephone = txtTelephone.Text;
                oAdviserDTO.Email = txtEmail.Text;
                oAdviserDTO.Description = txtDescription.InnerText;
                oAdviserDTO.Type = Convert.ToInt32(ddlType.SelectedValue.ToString());
                oAdviserDTO.JoinDate = Convert.ToDateTime(txtJoinDate.Text);
                //oAdviserDTO.NumberOfShares = Convert.ToInt32(txtNoOfShares.Text);
                //oAdviserDTO.SharePrice = Convert.ToDecimal(txtSharePrice.Text);
                oAdviserDTO.Status = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
                oAdviserDTO.UserName = txtUserName.Text;

                string EncryptedPwd = Cryptography.Encryption.Encrypt(txtPassword.Text, txtUserName.Text);

                oAdviserDTO.Password = EncryptedPwd;
                oAdviserDTO.ModifiedUser = Session["UserID"].ToString();
                oAdviserDTO.ModifiedDateTime = DateTime.Now;
                oAdviserDTO.ModifiedMachine = Session["UserMachine"].ToString();

                #region Login

                LoginDTO oLoginDTOs = new LoginDTO();

                oLoginDTOs.UserName = txtUserName.Text;
                oLoginDTOs.Password = EncryptedPwd;
                oLoginDTOs.UserType = Convert.ToInt32(Session["UserType"].ToString());
                oLoginDTOs.LoginAttempts = 1;
                oLoginDTOs.ModifiedUser = Session["UserID"].ToString();
                oLoginDTOs.ModifiedDateTime = DateTime.Now;
                oLoginDTOs.ModifiedMachine = Session["UserMachine"].ToString();

                #endregion

                AdviserMaintanance oAdviserMaintanance = new AdviserMaintanance();
                oAdviserMaintanance.oAdviserDTO = oAdviserDTO;
                oAdviserMaintanance.oLoginDTO = oLoginDTOs;

                result = oWebApiCalls.UpdateAdviserData(oAdviserMaintanance);

                if (result == true)
                {
                    ResetControllers();
                    Messages("Adviser Updated Successfully!!");
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
            txtAdviserName.Text = string.Empty;
            txtDescription.InnerText = string.Empty;
            txtEmail.Text = string.Empty;
            txtName.Text = string.Empty;
            txtJoinDate.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            txtUserName.Text = string.Empty;
            ddlAdviserType.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
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
                AdviserDTO oAdviserDTO = new AdviserDTO();
                string adviserName = string.Empty;
                int adviserType = 0;

                if (txtAdviserName.Text.Length > 0)
                {
                    adviserName = txtAdviserName.Text;
                }
                if (ddlAdviserType.SelectedIndex > 0)
                {
                    adviserType = ddlAdviserType.SelectedIndex;
                }

                oAdviserDTO = oWebApiCalls.AdviserSearchByFilters(adviserName, adviserType);

                txtName.Text = oAdviserDTO.Name;
                txtAddress.Text = oAdviserDTO.Address;
                txtTelephone.Text = oAdviserDTO.Telephone;
                txtEmail.Text = oAdviserDTO.Email;
                txtDescription.InnerText = oAdviserDTO.Description;
                ddlType.SelectedValue = Convert.ToString((int)oAdviserDTO.Type);
                txtJoinDate.Text = Convert.ToString(oAdviserDTO.JoinDate);
                ddlStatus.SelectedValue = Convert.ToString((int)oAdviserDTO.Status);

                string DecryptedPwd = Cryptography.Encryption.Decrypt(oAdviserDTO.Password, oAdviserDTO.UserName);

                txtUserName.Text = oAdviserDTO.UserName;
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