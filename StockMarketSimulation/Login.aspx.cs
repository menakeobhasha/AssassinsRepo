using CloudSecurity;
using SMS.Common;
using StockMarketSimulation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockMarketSimulation
{
    public partial class Login1 : System.Web.UI.Page
    {
        private SecurityClient oSecurityClient = new SecurityClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //ClearSessions();
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUserName.Text.Trim().ToLower();
                string password = txtPassword.Text.Trim();

                //int loginstatus = oSecurityClient.ValidateUser(username, password);

                lblMessage.Visible = false;
                lblMessage.Text = string.Empty;

                UpdateSessions();
                Response.Redirect("~/Pages/Dashboard.aspx");

                //switch (loginstatus)
                //{
                //    case (int)UserValidationStatus.InvalidUser:
                //        lblMessage.Visible = true;
                //        lblMessage.Text = "Invalid User";
                //        break;

                //    case (int)UserValidationStatus.UserInactive:
                //        lblMessage.Visible = true;
                //        lblMessage.Text = "User Inactive";
                //        break;

                //    case (int)UserValidationStatus.InvalidPassword:
                //        lblMessage.Visible = true;
                //        lblMessage.Text = "Invalid EmployeeID & Password";
                //        break;

                //    case (int)UserValidationStatus.MaximumUnsuccessfulAttemptsExceesed:
                //        lblMessage.Visible = true;
                //        lblMessage.Text = "Maximum Unsuccessful Attempts Exceeded, Account Locked!";
                //        break;

                //    case (int)UserValidationStatus.NewUser:
                //        UpdateSessions();
                //        Response.Redirect("~/ChangePassword.aspx?status=force");
                //        break;

                //    case (int)UserValidationStatus.PasswordExpired:
                //        UpdateSessions();
                //        Response.Redirect("~/ChangePassword.aspx?status=force");
                //        break;

                //    case (int)UserValidationStatus.UserAutoInactive:
                //        UpdateSessions();
                //        Response.Redirect("~/ChangePassword.aspx?status=force");
                //        break;

                //    case (int)UserValidationStatus.PasswordReset:
                //        UpdateSessions();
                //        Response.Redirect("~/ChangePassword.aspx?status=force");
                //        break;

                //    case (int)UserValidationStatus.SuccessfullyLoggedIn:
                //        UpdateSessions();
                //        Response.Redirect("~/pages/Dashboard.aspx");
                //        break;
                //}
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Methods

        private void UpdateSessions()
        {
            //Session["UserID"] = txtUserName.Text.Trim();
            Session["UserID"] = txtUserName.Text.Trim();
            Session["UserMachine"] = Request.ServerVariables["REMOTE_ADDR"].ToString();
            //Security202008DTO oSecurity202008DTO = oSecurityClient.GetUser(Session["UserID"].ToString());
            //Session["CompanyID"] = oSecurity202008DTO.column84;
            //Session["EmployeeID"] = oSecurity202008DTO.column86;
            //Session["UserName"] = oSecurity202008DTO.column87;
            Session["UserName"] = "Super User";
            //Session["UserType"] = oSecurity202008DTO.column90;
            //GlobalValues.CompanyId = oSecurity202008DTO.column84;
            GlobalValues.CompanyId = 1;

            //List<Security202006DTO> ofunctionDTOs = new List<Security202006DTO>();
            //ofunctionDTOs.AddRange(oSecurityClient.AccessibleFunctions(Session["UserID"].ToString(), Convert.ToInt32(Session["CompanyID"].ToString()), ((int)ProductList.SMS).ToString()));
            //Session["Menu"] = null;
            //Session["Menu"] = ofunctionDTOs;
            


        }

        private void ClearSessions()
        {
            Session["UserID"] = null;
            Session["UserMachine"] = null;
            Session["CompanyID"] = null;
            Session["EmployeeID"] = null;
            Session["UserName"] = null;
            Session["UserType"] = null;
            Session["Menu"] = null;
        }

        #endregion Methods
    }
}