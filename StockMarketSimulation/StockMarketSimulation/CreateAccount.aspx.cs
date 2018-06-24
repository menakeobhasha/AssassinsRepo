using SMS.Common;
using SMS.Model;
using StockMarketSimulation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockMarketSimulation
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        private SMS.Common.Common oCommon = new SMS.Common.Common();
        private WebApiCalls oWebApiCalls = new WebApiCalls();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mvParent.ActiveViewIndex = 0;
                loaddata();
            }
            else
            if (Page.IsPostBack)
                return;
        }

        private void loaddata()
        {
            LoadDropDownList();
        }

        private void LoadDropDownList()
        {
            #region Common
            ddlUserType.DataSource = oCommon.ComboboxDataBind(typeof(UserType));
            ddlUserType.DataTextField = "TextField";
            ddlUserType.DataValueField = "ValueField";
            ddlUserType.DataBind(); 
            #endregion

            #region Company
            ddlStatus.DataSource = oCommon.ComboboxDataBind(typeof(Status));
            ddlStatus.DataTextField = "TextField";
            ddlStatus.DataValueField = "ValueField";
            ddlStatus.DataBind();

            ddlType.DataSource = oCommon.ComboboxDataBind(typeof(CompanyType));
            ddlType.DataTextField = "TextField";
            ddlType.DataValueField = "ValueField";
            ddlType.DataBind(); 
            #endregion            

            #region Adviser
            ddlAStatus.DataSource = oCommon.ComboboxDataBind(typeof(Status));
            ddlAStatus.DataTextField = "TextField";
            ddlAStatus.DataValueField = "ValueField";
            ddlAStatus.DataBind();

            ddlAType.DataSource = oCommon.ComboboxDataBind(typeof(CompanyType));
            ddlAType.DataTextField = "TextField";
            ddlAType.DataValueField = "ValueField";
            ddlAType.DataBind();
            #endregion

            #region Broker
            ddlBStatus.DataSource = oCommon.ComboboxDataBind(typeof(Status));
            ddlBStatus.DataTextField = "TextField";
            ddlBStatus.DataValueField = "ValueField";
            ddlBStatus.DataBind();

            ddlBType.DataSource = oCommon.ComboboxDataBind(typeof(CompanyType));
            ddlBType.DataTextField = "TextField";
            ddlBType.DataValueField = "ValueField";
            ddlBType.DataBind();
            #endregion

            #region Player
            ddlPStatus.DataSource = oCommon.ComboboxDataBind(typeof(Status));
            ddlPStatus.DataTextField = "TextField";
            ddlPStatus.DataValueField = "ValueField";
            ddlPStatus.DataBind();

            ddlPType.DataSource = oCommon.ComboboxDataBind(typeof(CompanyType));
            ddlPType.DataTextField = "TextField";
            ddlPType.DataValueField = "ValueField";
            ddlPType.DataBind();
            #endregion
        }

        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Type = Convert.ToInt32(ddlUserType.SelectedValue);
            ControllersHandler(CommandMood.Add);
            switch (Type)
            {
                case (int)UserType.Company:
                    mvParent.ActiveViewIndex = 1;
                    break;
                case (int)UserType.Broker:
                    mvParent.ActiveViewIndex = 2;
                    break;
                case (int)UserType.Adviser:
                    mvParent.ActiveViewIndex = 3;
                    break;
                case (int)UserType.Player:
                    mvParent.ActiveViewIndex = 4;
                    break;
            }
        }

        protected void btnCompanyAdd_Click(object sender, EventArgs e)
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
                oCompanyDTOs.CreatedUser = txtName.Text;
                oCompanyDTOs.CreatedDateTime = DateTime.Now;
                oCompanyDTOs.CreatedMachine = Request.ServerVariables["REMOTE_ADDR"].ToString();
                oCompanyDTOs.ModifiedUser = txtName.Text;
                oCompanyDTOs.ModifiedDateTime = DateTime.Now;
                oCompanyDTOs.ModifiedMachine = Request.ServerVariables["REMOTE_ADDR"].ToString();
                #endregion

                #region Login

                LoginDTO oLoginDTOs = new LoginDTO();

                oLoginDTOs.UserName = txtUserName.Text;
                oLoginDTOs.Password = EncryptedPwd;
                oLoginDTOs.UserType = Convert.ToInt32(ddlUserType.SelectedValue.ToString());
                oLoginDTOs.LoginAttempts = 1;
                oLoginDTOs.CreatedUser = txtName.Text;
                oLoginDTOs.CreatedDateTime = DateTime.Now;
                oLoginDTOs.CreatedMachine = Request.ServerVariables["REMOTE_ADDR"].ToString();
                oLoginDTOs.ModifiedUser = txtName.Text;
                oLoginDTOs.ModifiedDateTime = DateTime.Now;
                oLoginDTOs.ModifiedMachine = Request.ServerVariables["REMOTE_ADDR"].ToString();

                #endregion

                CompanyMaintanance oCompanyMaintanance = new CompanyMaintanance();
                oCompanyMaintanance.oCompanyDTO = oCompanyDTOs;
                oCompanyMaintanance.oLoginDTO = oLoginDTOs;


                result = oWebApiCalls.InsertCompanyData(oCompanyMaintanance);

                if (result == true)
                {
                    Messages("Company Inserted Successfully!!");
                    ResetControllers();
                    Response.Redirect("~/Login.aspx");
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

        //Controllers Handler
        private void ControllersHandler(CommandMood oCommandMood)
        {
            switch (oCommandMood)
            {
                #region Company
                case CommandMood.Add:
                    #region Company
                    txtAddress.Enabled = true;
                    txtEmail.Enabled = true;
                    txtName.Enabled = true;
                    txtNoOfShares.Enabled = true;
                    txtSharePrice.Enabled = true;
                    txtTelephone.Enabled = true;
                    ddlStatus.Enabled = false;
                    ddlStatus.SelectedValue = Convert.ToString((int)Status.Active);
                    #endregion

                    #region Adviser

                    txtAAddress.Enabled = true;
                    txtAEmail.Enabled = true;
                    txtAName.Enabled = true;
                    dtpAJoinDate.Enabled = true;
                    txtATelephone.Enabled = true;
                    ddlAStatus.Enabled = false;
                    ddlAStatus.SelectedValue = Convert.ToString((int)Status.Active);

                    #endregion

                    #region Broker

                    txtBAddress.Enabled = true;
                    txtBEmail.Enabled = true;
                    txtBName.Enabled = true;
                    dtpBJoinDate.Enabled = true;
                    txtBTelephone.Enabled = true;
                    ddlBStatus.Enabled = false;
                    ddlBStatus.SelectedValue = Convert.ToString((int)Status.Active);

                    #endregion

                    #region Player

                    txtPAddress.Enabled = true;
                    txtPEmail.Enabled = true;
                    txtPName.Enabled = true;
                    dtpPJoinDate.Enabled = true;
                    txtPTelephone.Enabled = true;
                    ddlPStatus.Enabled = false;
                    ddlPStatus.SelectedValue = Convert.ToString((int)Status.Active);
                    
                    #endregion

                    break;

                case CommandMood.Edit:
                    #region Company
                    txtAddress.Enabled = true;
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
                    #endregion

                    #region Adviser

                    txtAAddress.Enabled = true;
                    txtAEmail.Enabled = true;
                    txtAName.Enabled = true;
                    dtpAJoinDate.Enabled = true;
                    txtATelephone.Enabled = true;
                    ddlAStatus.Enabled = false;
                    txtADescription.Disabled = false;
                    ddlAType.Enabled = true;
                    txtAUserName.Enabled = false;
                    txtAPassword.Enabled = true;

                    #endregion

                    #region Broker

                    txtAddress.Enabled = true;
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

                    #endregion

                    #region Player

                    txtAddress.Enabled = true;
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

                    #endregion
                    break;

                case CommandMood.View:
                    #region Company
                    txtAddress.Enabled = false;
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
                    #endregion

                    #region Adviser

                    txtAddress.Enabled = false;
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

                    #endregion

                    #region Broker

                    txtAddress.Enabled = false;
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

                    #endregion

                    #region Player

                    txtAddress.Enabled = false;
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

                    #endregion
                    break; 
                    #endregion
            }
        }

        private void ResetControllers()
        {
            #region Company
            txtAddress.Text = string.Empty;
            txtDescription.InnerText = string.Empty;
            txtEmail.Text = string.Empty;
            txtName.Text = string.Empty;
            txtNoOfShares.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtSharePrice.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            //txtUserName.Text = string.Empty;
            ddlUserType.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddlType.SelectedIndex = 0;
            #endregion

            #region Adviser
            txtAAddress.Text = string.Empty;
            txtADescription.InnerText = string.Empty;
            txtAEmail.Text = string.Empty;
            txtAName.Text = string.Empty;
            txtAPassword.Text = string.Empty;
            dtpAJoinDate.Text = string.Empty;
            txtATelephone.Text = string.Empty;
            ddlAStatus.SelectedIndex = 0;
            ddlAType.SelectedIndex = 0;
            #endregion

            #region Broker
            txtBAddress.Text = string.Empty;
            txtBDescription.InnerText = string.Empty;
            txtBEmail.Text = string.Empty;
            txtBName.Text = string.Empty;
            txtBPassword.Text = string.Empty;
            dtpBJoinDate.Text = string.Empty;
            txtBSharePrice.Text = string.Empty;
            txtBTelephone.Text = string.Empty;
            ddlBStatus.SelectedIndex = 0;
            ddlBType.SelectedIndex = 0;
            #endregion

            #region Player
            txtPAddress.Text = string.Empty;
            txtPDescription.InnerText = string.Empty;
            txtPEmail.Text = string.Empty;
            txtPName.Text = string.Empty;
            txtPPassword.Text = string.Empty;
            dtpPJoinDate.Text = string.Empty;
            txtPTelephone.Text = string.Empty;
            ddlPStatus.SelectedIndex = 0;
            ddlPType.SelectedIndex = 0;
            #endregion
        }

        protected void btnAAdd_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {
                #region Adviser
                AdviserDTO oAdviserDTOs = new AdviserDTO();

                oAdviserDTOs.Name = txtAName.Text;
                oAdviserDTOs.Address = txtAAddress.Text;
                oAdviserDTOs.Telephone = txtATelephone.Text;
                oAdviserDTOs.Email = txtAEmail.Text;
                oAdviserDTOs.Description = txtADescription.InnerText;
                oAdviserDTOs.Type = Convert.ToInt32(ddlAType.SelectedValue.ToString());
                oAdviserDTOs.JoinDate = Convert.ToDateTime(dtpAJoinDate.Text);
                oAdviserDTOs.Status = Convert.ToInt32(ddlAStatus.SelectedValue.ToString());
                oAdviserDTOs.UserName = txtAUserName.Text;

                string EncryptedPwd = Cryptography.Encryption.Encrypt(txtAPassword.Text, txtAUserName.Text);

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

                oLoginDTOs.UserName = txtAUserName.Text;
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

        protected void btnBrokerAdd_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {
                #region Broker
                BrokerDTO oBrokerDTOs = new BrokerDTO();

                oBrokerDTOs.Name = txtBName.Text;
                oBrokerDTOs.Address = txtBAddress.Text;
                oBrokerDTOs.Telephone = txtBTelephone.Text;
                oBrokerDTOs.Email = txtBEmail.Text;
                oBrokerDTOs.Description = txtBDescription.InnerText;
                oBrokerDTOs.Type = Convert.ToInt32(ddlBType.SelectedValue.ToString());
                oBrokerDTOs.JoinDate = Convert.ToDateTime(dtpBJoinDate.Text);
                oBrokerDTOs.Status = Convert.ToInt32(ddlBStatus.SelectedValue.ToString());
                oBrokerDTOs.UserName = txtBUserName.Text;

                string EncryptedPwd = Cryptography.Encryption.Encrypt(txtBPassword.Text, txtBUserName.Text);

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

                oLoginDTOs.UserName = txtBUserName.Text;
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

        protected void btnPAdd_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {
                PlayerDTO oPlayerDTOs = new PlayerDTO();

                oPlayerDTOs.Name = txtPName.Text;
                oPlayerDTOs.Address = txtPAddress.Text;
                oPlayerDTOs.Telephone = txtPTelephone.Text;
                oPlayerDTOs.Email = txtPEmail.Text;
                oPlayerDTOs.Description = txtPDescription.InnerText;
                oPlayerDTOs.Type = Convert.ToInt32(ddlPType.SelectedValue.ToString());
                oPlayerDTOs.JoinDate = Convert.ToDateTime(dtpPJoinDate.Text);                
                oPlayerDTOs.Status = Convert.ToInt32(ddlPStatus.SelectedValue.ToString());
                oPlayerDTOs.UserName = txtUserName.Text;

                string EncryptedPwd = Cryptography.Encryption.Encrypt(txtPPassword.Text, txtPUserName.Text);

                oPlayerDTOs.Password = EncryptedPwd;
                oPlayerDTOs.CreatedUser = Session["UserID"].ToString();
                oPlayerDTOs.CreatedDateTime = DateTime.Now;
                oPlayerDTOs.CreatedMachine = Session["UserMachine"].ToString();
                oPlayerDTOs.ModifiedUser = Session["UserID"].ToString();
                oPlayerDTOs.ModifiedDateTime = DateTime.Now;
                oPlayerDTOs.ModifiedMachine = Session["UserMachine"].ToString();

                #region Login

                LoginDTO oLoginDTOs = new LoginDTO();

                oLoginDTOs.UserName = txtPUserName.Text;
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

                PlayerMaintanance oPlayerMaintanance = new PlayerMaintanance();
                oPlayerMaintanance.oPlayerDTO = oPlayerDTOs;
                oPlayerMaintanance.oLoginDTO = oLoginDTOs;

                result = oWebApiCalls.InsertPlayerData(oPlayerMaintanance);

                if (result == true)
                {
                    ResetControllers();
                    Messages("Player Inserted Successfully!!");
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

        protected void btnCompanyClear_Click(object sender, EventArgs e)
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

        protected void btnCompanyCancel_Click(object sender, EventArgs e)
        {
            try
            {
                mvParent.ActiveViewIndex = 0;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void btnBrokerClear_Click(object sender, EventArgs e)
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

        protected void btnBrokerCancel_Click(object sender, EventArgs e)
        {
            try
            {
                mvParent.ActiveViewIndex = 0;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void btnAClear_Click(object sender, EventArgs e)
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

        protected void btnACancel_Click(object sender, EventArgs e)
        {
            try
            {
                mvParent.ActiveViewIndex = 0;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }

        protected void btnPClear_Click(object sender, EventArgs e)
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

        protected void btnPCancel_Click(object sender, EventArgs e)
        {
            try
            {
                mvParent.ActiveViewIndex = 0;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
            }
        }
    }
}