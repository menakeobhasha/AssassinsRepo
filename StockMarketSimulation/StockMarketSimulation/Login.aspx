<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="StockMarketSimulation.Login1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="login-box-body">
        <p class="login-box-msg">Sign in to start your session</p>
        <form id="form1" runat="server">
            <div class="form-group has-feedback">
                <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Username" AutoCompleteType="Disabled"></asp:TextBox>
                <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
            </div>
            <div class="form-group has-feedback">
                <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="Password" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
                <span class="glyphicon glyphicon-lock form-control-feedback"></span>
            </div>
            <div class="row">
                <div class="col-xs-8">
                    &nbsp;
                </div>
                <!-- /.col -->
                <div class="social-auth-links text-center col-xs-12">
                    <asp:Button ID="btnLogin" runat="server" class="btn btn-primary btn-block btn-flat" Text="Sign In" OnClientClick="return AttachValidation();" OnClick="btnLogin_Click" />
                </div>
                <!-- /.col -->
                <div class="social-auth-links text-center col-xs-12">
                    <p>OR </p>
                    <asp:Button ID="btnCreateAccount" runat="server" class="btn btn-block btn-facebook btn-flat" Text="Create Account" OnClick="btnCreateAccount_Click" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <asp:Label ID="lblMessage" runat="server" class="error-content"></asp:Label>
                </div>
            </div>

        </form>

    </div>
    <script type="text/javascript">
        function AttachValidation() {
            $('#<% = txtUserName.ClientID %>').addClass('validate[required]');
            $('#<% = txtPassword.ClientID %>').addClass('validate[required]');

            $("#form1").validationEngine('attach', { promptPosition: "topRight", scroll: false });
            var valid = $("#form1").validationEngine('validate');
            var vars = $("#form1").serialize();
            if (valid == true) {
                $("#form1").validationEngine('detach');
            } else {
                $("#form1").validationEngine('attach', { promptPosition: "topRight", scroll: false });
                return false;
            }
        }

        function DetachValidation() {
            $("#form1").validationEngine('detach');
        }
    </script>
</asp:Content>
