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
    public partial class Broker : System.Web.UI.Page
    {
        private BrokerBL oBrokerBL = new BrokerBL();
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
                ddlBrokerType.DataSource = oCommon.ComboboxDataBind(typeof(BrokerType));
                ddlBrokerType.DataTextField = "TextField";
                ddlBrokerType.DataValueField = "ValueField";
                ddlBrokerType.DataBind();

                ddlStatus.DataSource = oCommon.ComboboxDataBind(typeof(Status));
                ddlStatus.DataTextField = "TextField";
                ddlStatus.DataValueField = "ValueField";
                ddlStatus.DataBind();

                ddlType.DataSource = oCommon.ComboboxDataBind(typeof(BrokerType));
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
            List<BrokerDTO> oBrokerDTOs = new List<BrokerDTO>();

            oBrokerDTOs = oWebApiCalls.GetBrokerData();
            gvBroker.DataSource = oBrokerDTOs;
            gvBroker.DataBind();
        }

        protected void btnAddBroker_Click(object sender, EventArgs e)
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
                    txtBrokerName.Enabled = true;
                    txtEmail.Enabled = true;
                    txtName.Enabled = true;
                    txtJoinDate.Enabled = true;
                    txtTelephone.Enabled = true;
                    ddlStatus.Enabled = false;
                    ddlStatus.SelectedValue = Convert.ToString((int)Status.Active);

                    break;

                case CommandMood.Edit:
                    txtAddress.Enabled = true;
                    txtBrokerName.Enabled = true;
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
                    txtBrokerName.Enabled = false;
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

        protected void gvBroker_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        ViewBrokerData();
                        ControllersHandler(CommandMood.View);
                        HandleButtons(CommandMood.View);
                        break;

                    case "EditData":
                        Session["status"] = "EditData";
                        mvParent.ActiveViewIndex = 1;
                        ViewState["index"] = e.CommandArgument.ToString();
                        Session["index"] = e.CommandArgument.ToString();
                        EditBrokerData();
                        ControllersHandler(CommandMood.Edit);
                        HandleButtons(CommandMood.Edit);
                        break;

                    case "DeleteData":
                        //RecordView(DataEntryMode.Delete);
                        ViewState["index"] = e.CommandArgument.ToString();
                        DeleteBrokerData();
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

        private void DeleteBrokerData()
        {
            bool result = false;
            try
            {
                GridViewRow oGridViewRow = gvBroker.Rows[Convert.ToInt32(ViewState["index"])];
                Label lblBrokerId = (Label)oGridViewRow.FindControl("lblBrokerId");
                BrokerDTO oBrokerDTO = new BrokerDTO();

                oBrokerDTO.BrokerId = Convert.ToInt32(lblBrokerId.Text);
                oBrokerDTO.Status = (int)Status.Inactive;
                oBrokerDTO.ModifiedUser = Session["UserID"].ToString();
                oBrokerDTO.ModifiedDateTime = DateTime.Now;
                oBrokerDTO.ModifiedMachine = Session["UserMachine"].ToString();

                result = oWebApiCalls.DeleteBrokerData(oBrokerDTO);

                if (result == true)
                {
                    ResetControllers();
                    Messages("Broker Deleted Successfully!!");
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

        private void EditBrokerData()
        {
            try
            {
                LoadDropDownList();
                GridViewRow oGridViewRow = gvBroker.Rows[Convert.ToInt32(ViewState["index"])];
                Label lblBrokerId = (Label)oGridViewRow.FindControl("lblBrokerId");
                Session["BrokerId"] = lblBrokerId.Text;
                BrokerDTO oBrokerDTO = new BrokerDTO();
                oBrokerDTO = oWebApiCalls.BrokerSearchById(Convert.ToInt32(lblBrokerId.Text));

                txtName.Text = oBrokerDTO.Name;
                txtAddress.Text = oBrokerDTO.Address;
                txtTelephone.Text = oBrokerDTO.Telephone;
                txtEmail.Text = oBrokerDTO.Email;
                txtDescription.InnerText = oBrokerDTO.Description;
                ddlType.SelectedValue = Convert.ToString((int)oBrokerDTO.Type);
                txtJoinDate.Text = oBrokerDTO.JoinDate.ToString("yyyy-MM-dd");
                //txtNoOfShares.Text = Convert.ToString(oBrokerDTO.NumberOfShares);
                //txtSharePrice.Text = Convert.ToString(oBrokerDTO.SharePrice);
                ddlStatus.SelectedValue = Convert.ToString((int)oBrokerDTO.Status);

                string DecryptedPwd = Cryptography.Encryption.Decrypt(oBrokerDTO.Password, oBrokerDTO.UserName);

                txtUserName.Text = oBrokerDTO.UserName;
                txtPassword.Text = DecryptedPwd;

                ControllersHandler(CommandMood.Edit);

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                throw ex;
            }
        }

        private void ViewBrokerData()
        {
            try
            {
                LoadDropDownList();
                GridViewRow oGridViewRow = gvBroker.Rows[Convert.ToInt32(ViewState["index"])];
                Label lblBrokerId = (Label)oGridViewRow.FindControl("lblBrokerId");
                Session["BrokerId"] = lblBrokerId.Text;
                BrokerDTO oBrokerDTO = new BrokerDTO();
                oBrokerDTO = oWebApiCalls.BrokerSearchById(Convert.ToInt32(lblBrokerId.Text));

                txtName.Text = oBrokerDTO.Name;
                txtAddress.Text = oBrokerDTO.Address;
                txtTelephone.Text = oBrokerDTO.Telephone;
                txtEmail.Text = oBrokerDTO.Email;
                txtDescription.InnerText = oBrokerDTO.Description;
                ddlType.SelectedValue = Convert.ToString((int)oBrokerDTO.Type);
                txtJoinDate.Text = oBrokerDTO.JoinDate.ToString("yyyy-MM-dd");
                //txtNoOfShares.Text = Convert.ToString(oBrokerDTO.NumberOfShares);
                //txtSharePrice.Text = Convert.ToString(oBrokerDTO.SharePrice);
                ddlStatus.SelectedValue = Convert.ToString((int)oBrokerDTO.Status);

                string DecryptedPwd = Cryptography.Encryption.Decrypt(oBrokerDTO.Password, oBrokerDTO.UserName);

                txtUserName.Text = oBrokerDTO.UserName;
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
            BrokerInsert();
            ControllersHandler(CommandMood.Add);
            HandleButtons(CommandMood.Add);
            mvParent.ActiveViewIndex = 1;
        }

        private void BrokerInsert()
        {
            bool result = false;
            try
            {
                #region Broker
                BrokerDTO oBrokerDTOs = new BrokerDTO();

                oBrokerDTOs.Name = txtName.Text;
                oBrokerDTOs.Address = txtAddress.Text;
                oBrokerDTOs.Telephone = txtTelephone.Text;
                oBrokerDTOs.Email = txtEmail.Text;
                oBrokerDTOs.Description = txtDescription.InnerText;
                oBrokerDTOs.Type = Convert.ToInt32(ddlType.SelectedValue.ToString());
                oBrokerDTOs.JoinDate = Convert.ToDateTime(txtJoinDate.Text);
                //oBrokerDTOs.NumberOfShares = Convert.ToInt32(txtNoOfShares.Text);
                //oBrokerDTOs.SharePrice = Convert.ToDecimal(txtSharePrice.Text);
                oBrokerDTOs.Status = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
                oBrokerDTOs.UserName = txtUserName.Text;

                string EncryptedPwd = Cryptography.Encryption.Encrypt(txtPassword.Text, txtUserName.Text);

                oBrokerDTOs.Password = EncryptedPwd;
                oBrokerDTOs.CreatedUser = Session["UserID"].ToString();
                oBrokerDTOs.CreatedDateTime = DateTime.Now;
                oBrokerDTOs.CreatedMachine = Session["UserMachine"].ToString();
                oBrokerDTOs.ModifiedUser = Session["UserID"].ToString();
                oBrokerDTOs.ModifiedDateTime = DateTime.Now;
                oBrokerDTOs.ModifiedMachine = Session["UserMachine"].ToString(); 
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

                BrokerMaintanance oBrokerMaintanance = new BrokerMaintanance();
                oBrokerMaintanance.oBrokerDTO = oBrokerDTOs;
                oBrokerMaintanance.oLoginDTO = oLoginDTOs;

                result = oWebApiCalls.InsertBrokerData(oBrokerMaintanance);

                if (result == true)
                {
                    ResetControllers();
                    Messages("Broker Inserted Successfully!!");
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
            UpdateBrokerData();
            mvParent.ActiveViewIndex = 0;
            LoadData();
        }

        private void UpdateBrokerData()
        {
            bool result = false;
            try
            {
                #region Broker
                BrokerDTO oBrokerDTO = new BrokerDTO();
                oBrokerDTO.BrokerId = Convert.ToInt32(Session["BrokerId"].ToString());
                oBrokerDTO.Name = txtName.Text;
                oBrokerDTO.Address = txtAddress.Text;
                oBrokerDTO.Telephone = txtTelephone.Text;
                oBrokerDTO.Email = txtEmail.Text;
                oBrokerDTO.Description = txtDescription.InnerText;
                oBrokerDTO.Type = Convert.ToInt32(ddlType.SelectedValue.ToString());
                oBrokerDTO.JoinDate = Convert.ToDateTime(txtJoinDate.Text);
                //oBrokerDTO.NumberOfShares = Convert.ToInt32(txtNoOfShares.Text);
                //oBrokerDTO.SharePrice = Convert.ToDecimal(txtSharePrice.Text);
                oBrokerDTO.Status = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
                oBrokerDTO.UserName = txtUserName.Text;

                string EncryptedPwd = Cryptography.Encryption.Encrypt(txtPassword.Text, txtUserName.Text);

                oBrokerDTO.Password = EncryptedPwd;
                oBrokerDTO.ModifiedUser = Session["UserID"].ToString();
                oBrokerDTO.ModifiedDateTime = DateTime.Now;
                oBrokerDTO.ModifiedMachine = Session["UserMachine"].ToString(); 
                #endregion

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

                BrokerMaintanance oBrokerMaintanance = new BrokerMaintanance();
                oBrokerMaintanance.oBrokerDTO = oBrokerDTO;
                oBrokerMaintanance.oLoginDTO = oLoginDTOs;

                result = oWebApiCalls.UpdateBrokerData(oBrokerMaintanance);

                if (result == true)
                {
                    ResetControllers();
                    Messages("Broker Updated Successfully!!");
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
            txtBrokerName.Text = string.Empty;
            txtDescription.InnerText = string.Empty;
            txtEmail.Text = string.Empty;
            txtName.Text = string.Empty;
            txtJoinDate.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            txtUserName.Text = string.Empty;
            ddlBrokerType.SelectedIndex = 0;
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
    }
}