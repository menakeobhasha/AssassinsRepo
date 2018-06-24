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
    public partial class Player : System.Web.UI.Page
    {
        private PlayerBL oPlayerBL = new PlayerBL();
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
                ddlPlayerType.DataSource = oCommon.ComboboxDataBind(typeof(PlayerType));
                ddlPlayerType.DataTextField = "TextField";
                ddlPlayerType.DataValueField = "ValueField";
                ddlPlayerType.DataBind();

                ddlStatus.DataSource = oCommon.ComboboxDataBind(typeof(Status));
                ddlStatus.DataTextField = "TextField";
                ddlStatus.DataValueField = "ValueField";
                ddlStatus.DataBind();

                ddlType.DataSource = oCommon.ComboboxDataBind(typeof(PlayerType));
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
            List<PlayerDTO> oPlayerDTOs = new List<PlayerDTO>();

            oPlayerDTOs = oWebApiCalls.GetPlayerData();
            gvPlayer.DataSource = oPlayerDTOs;
            gvPlayer.DataBind();
        }

        protected void btnAddPlayer_Click(object sender, EventArgs e)
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
                    txtPlayerName.Enabled = true;
                    txtEmail.Enabled = true;
                    txtName.Enabled = true;
                    txtJoinDate.Enabled = true;
                    txtTelephone.Enabled = true;
                    ddlStatus.Enabled = false;
                    ddlStatus.SelectedValue = Convert.ToString((int)Status.Active);

                    break;

                case CommandMood.Edit:
                    txtAddress.Enabled = true;
                    txtPlayerName.Enabled = true;
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
                    txtPlayerName.Enabled = false;
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

        protected void gvPlayer_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        ViewPlayerData();
                        ControllersHandler(CommandMood.View);
                        HandleButtons(CommandMood.View);
                        break;

                    case "EditData":
                        Session["status"] = "EditData";
                        mvParent.ActiveViewIndex = 1;
                        ViewState["index"] = e.CommandArgument.ToString();
                        Session["index"] = e.CommandArgument.ToString();
                        EditPlayerData();
                        ControllersHandler(CommandMood.Edit);
                        HandleButtons(CommandMood.Edit);
                        break;

                    case "DeleteData":
                        //RecordView(DataEntryMode.Delete);
                        ViewState["index"] = e.CommandArgument.ToString();
                        DeletePlayerData();
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

        private void DeletePlayerData()
        {
            bool result = false;
            try
            {
                GridViewRow oGridViewRow = gvPlayer.Rows[Convert.ToInt32(ViewState["index"])];
                Label lblPlayerId = (Label)oGridViewRow.FindControl("lblPlayerId");
                PlayerDTO oPlayerDTO = new PlayerDTO();

                oPlayerDTO.PlayerId = Convert.ToInt32(lblPlayerId.Text);
                oPlayerDTO.Status = (int)Status.Inactive;
                oPlayerDTO.ModifiedUser = Session["UserID"].ToString();
                oPlayerDTO.ModifiedDateTime = DateTime.Now;
                oPlayerDTO.ModifiedMachine = Session["UserMachine"].ToString();

                result = oWebApiCalls.DeletePlayerData(oPlayerDTO);

                if (result == true)
                {
                    ResetControllers();
                    Messages("Player Deleted Successfully!!");
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

        private void EditPlayerData()
        {
            try
            {
                LoadDropDownList();
                GridViewRow oGridViewRow = gvPlayer.Rows[Convert.ToInt32(ViewState["index"])];
                Label lblPlayerId = (Label)oGridViewRow.FindControl("lblPlayerId");
                Session["PlayerId"] = lblPlayerId.Text;
                PlayerDTO oPlayerDTO = new PlayerDTO();
                oPlayerDTO = oWebApiCalls.PlayerSearchById(Convert.ToInt32(lblPlayerId.Text));

                txtName.Text = oPlayerDTO.Name;
                txtAddress.Text = oPlayerDTO.Address;
                txtTelephone.Text = oPlayerDTO.Telephone;
                txtEmail.Text = oPlayerDTO.Email;
                txtDescription.InnerText = oPlayerDTO.Description;
                ddlType.SelectedValue = Convert.ToString((int)oPlayerDTO.Type);
                txtJoinDate.Text = oPlayerDTO.JoinDate.ToString("yyyy-MM-dd");
                //txtNoOfShares.Text = Convert.ToString(oPlayerDTO.NumberOfShares);
                //txtSharePrice.Text = Convert.ToString(oPlayerDTO.SharePrice);
                ddlStatus.SelectedValue = Convert.ToString((int)oPlayerDTO.Status);

                string DecryptedPwd = Cryptography.Encryption.Decrypt(oPlayerDTO.Password, oPlayerDTO.UserName);

                txtUserName.Text = oPlayerDTO.UserName;
                txtPassword.Text = DecryptedPwd;

                ControllersHandler(CommandMood.Edit);

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                throw ex;
            }
        }

        private void ViewPlayerData()
        {
            try
            {
                LoadDropDownList();
                GridViewRow oGridViewRow = gvPlayer.Rows[Convert.ToInt32(ViewState["index"])];
                Label lblPlayerId = (Label)oGridViewRow.FindControl("lblPlayerId");
                Session["PlayerId"] = lblPlayerId.Text;
                PlayerDTO oPlayerDTO = new PlayerDTO();
                oPlayerDTO = oWebApiCalls.PlayerSearchById(Convert.ToInt32(lblPlayerId.Text));

                txtName.Text = oPlayerDTO.Name;
                txtAddress.Text = oPlayerDTO.Address;
                txtTelephone.Text = oPlayerDTO.Telephone;
                txtEmail.Text = oPlayerDTO.Email;
                txtDescription.InnerText = oPlayerDTO.Description;
                ddlType.SelectedValue = Convert.ToString((int)oPlayerDTO.Type);
                txtJoinDate.Text = oPlayerDTO.JoinDate.ToString("yyyy-MM-dd");
                //txtNoOfShares.Text = Convert.ToString(oPlayerDTO.NumberOfShares);
                //txtSharePrice.Text = Convert.ToString(oPlayerDTO.SharePrice);
                ddlStatus.SelectedValue = Convert.ToString((int)oPlayerDTO.Status);

                string DecryptedPwd = Cryptography.Encryption.Decrypt(oPlayerDTO.Password, oPlayerDTO.UserName);

                txtUserName.Text = oPlayerDTO.UserName;
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
            PlayerInsert();
            ControllersHandler(CommandMood.Add);
            HandleButtons(CommandMood.Add);
            mvParent.ActiveViewIndex = 1;
        }

        private void PlayerInsert()
        {
            bool result = false;
            try
            {
                PlayerDTO oPlayerDTOs = new PlayerDTO();

                oPlayerDTOs.Name = txtName.Text;
                oPlayerDTOs.Address = txtAddress.Text;
                oPlayerDTOs.Telephone = txtTelephone.Text;
                oPlayerDTOs.Email = txtEmail.Text;
                oPlayerDTOs.Description = txtDescription.InnerText;
                oPlayerDTOs.Type = Convert.ToInt32(ddlType.SelectedValue.ToString());
                oPlayerDTOs.JoinDate = Convert.ToDateTime(txtJoinDate.Text);
                //oPlayerDTOs.NumberOfShares = Convert.ToInt32(txtNoOfShares.Text);
                //oPlayerDTOs.SharePrice = Convert.ToDecimal(txtSharePrice.Text);
                oPlayerDTOs.Status = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
                oPlayerDTOs.UserName = txtUserName.Text;

                string EncryptedPwd = Cryptography.Encryption.Encrypt(txtPassword.Text, txtUserName.Text);

                oPlayerDTOs.Password = EncryptedPwd;
                oPlayerDTOs.CreatedUser = Session["UserID"].ToString();
                oPlayerDTOs.CreatedDateTime = DateTime.Now;
                oPlayerDTOs.CreatedMachine = Session["UserMachine"].ToString();
                oPlayerDTOs.ModifiedUser = Session["UserID"].ToString();
                oPlayerDTOs.ModifiedDateTime = DateTime.Now;
                oPlayerDTOs.ModifiedMachine = Session["UserMachine"].ToString();

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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            mvParent.ActiveViewIndex = 1;
            ControllersHandler(CommandMood.Edit);
            HandleButtons(CommandMood.Edit);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdatePlayerData();
            mvParent.ActiveViewIndex = 0;
            LoadData();
        }

        private void UpdatePlayerData()
        {
            bool result = false;
            try
            {
                #region Player
                PlayerDTO oPlayerDTO = new PlayerDTO();
                oPlayerDTO.PlayerId = Convert.ToInt32(Session["PlayerId"].ToString());
                oPlayerDTO.Name = txtName.Text;
                oPlayerDTO.Address = txtAddress.Text;
                oPlayerDTO.Telephone = txtTelephone.Text;
                oPlayerDTO.Email = txtEmail.Text;
                oPlayerDTO.Description = txtDescription.InnerText;
                oPlayerDTO.Type = Convert.ToInt32(ddlType.SelectedValue.ToString());
                oPlayerDTO.JoinDate = Convert.ToDateTime(txtJoinDate.Text);
                //oPlayerDTO.NumberOfShares = Convert.ToInt32(txtNoOfShares.Text);
                //oPlayerDTO.SharePrice = Convert.ToDecimal(txtSharePrice.Text);
                oPlayerDTO.Status = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
                oPlayerDTO.UserName = txtUserName.Text;

                string EncryptedPwd = Cryptography.Encryption.Encrypt(txtPassword.Text, txtUserName.Text);

                oPlayerDTO.Password = EncryptedPwd;
                oPlayerDTO.ModifiedUser = Session["UserID"].ToString();
                oPlayerDTO.ModifiedDateTime = DateTime.Now;
                oPlayerDTO.ModifiedMachine = Session["UserMachine"].ToString(); 
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

                PlayerMaintanance oPlayerMaintanance = new PlayerMaintanance();
                oPlayerMaintanance.oPlayerDTO = oPlayerDTO;
                oPlayerMaintanance.oLoginDTO = oLoginDTOs;

                result = oWebApiCalls.UpdatePlayerData(oPlayerMaintanance);

                if (result == true)
                {
                    ResetControllers();
                    Messages("Player Updated Successfully!!");
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
            txtPlayerName.Text = string.Empty;
            txtDescription.InnerText = string.Empty;
            txtEmail.Text = string.Empty;
            txtName.Text = string.Empty;
            txtJoinDate.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            txtUserName.Text = string.Empty;
            ddlPlayerType.SelectedIndex = 0;
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