<%@ Page Title="" Language="C#" MasterPageFile="~/Register.Master" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="StockMarketSimulation.CreateAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $("[id$=txtDateOfBirth]").val($.datepicker.formatDate("yy-mm-dd", new Date()));
        //    $("[id$=txtDateOfJoined]").val($.datepicker.formatDate("yy-mm-dd", new Date()));

        //})
        function pageLoad() {
            $("[id$=dtpBJoinDate]").datepicker({
                'format': "yyyy-mm-dd",
                changeYear: false,
                changeMonth: false,
                autoclose: true
            }).attr('readonly', 'readonly');

            $("[id$=dtpAJoinDate]").datepicker({
                'format': "yyyy-mm-dd",
                changeYear: false,
                changeMonth: false,
                autoclose: true
            }).attr('readonly', 'readonly');

             $("[id$=dtpPJoinDate]").datepicker({
                'format': "yyyy-mm-dd",
                changeYear: false,
                changeMonth: false,
                autoclose: true
            }).attr('readonly', 'readonly');

        }

        function ConfirmOnDelete() {
            if (confirm("Are you sure you want to delete?") == true)
                return true;
            else
                return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCompanyAdd" />
            <asp:PostBackTrigger ControlID="btnCompanyClear" />
            <asp:PostBackTrigger ControlID="btnCompanyCancel" />

            <asp:PostBackTrigger ControlID="btnBrokerAdd" />
            <asp:PostBackTrigger ControlID="btnBrokerClear" />
            <asp:PostBackTrigger ControlID="btnBrokerCancel" />

            <asp:PostBackTrigger ControlID="btnAAdd" />
            <asp:PostBackTrigger ControlID="btnAClear" />
            <asp:PostBackTrigger ControlID="btnACancel" />

            <asp:PostBackTrigger ControlID="btnPAdd" />
            <asp:PostBackTrigger ControlID="btnPClear" />
            <asp:PostBackTrigger ControlID="btnPCancel" />
        </Triggers>
        <ContentTemplate>
            <asp:MultiView ID="mvParent" runat="server">
                <asp:View ID="View1" runat="server">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Account Type Selection</h3>
                        </div>
                        <div class="box-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">User Type</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                                    </div>
                                    <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-success" Text="Select" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Company Maintenance</h3>
                        </div>
                        <div class="box-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Name:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Address:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Telephone:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtTelephone" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Email:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Description:</label>
                                    <div class="col-sm-4">
                                        <textarea id="txtDescription" class="form-control" runat="server"></textarea>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Type:</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Number Of Shares:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtNoOfShares" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Share Price:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtSharePrice" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group" runat="server" id="divPaymentType">
                                    <label class="col-sm-4 control-label">Status:</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">User Name:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Password:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer fa-align-center">
                            <asp:Button ID="btnCompanyAdd" runat="server" Text="Add" CssClass="btn btn-success" OnClientClick="return Validate()" OnClick="btnCompanyAdd_Click" />
                            <asp:Button ID="btnCompanyClear" runat="server" CssClass="btn btn-success" Text="Clear" OnClick="btnCompanyClear_Click" />
                            <asp:Button ID="btnCompanyCancel" runat="server" Text="Cancel" CssClass="btn btn-success" OnClick="btnCompanyCancel_Click" />
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View3" runat="server">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Broker Maintenance</h3>
                        </div>
                        <div class="box-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Name:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtBName" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Address:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtBAddress" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Telephone:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtBTelephone" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Email:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtBEmail" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Description:</label>
                                    <div class="col-sm-4">
                                        <textarea id="txtBDescription" class="form-control" runat="server"></textarea>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Type:</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlBType" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Join Date:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="dtpBJoinDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Share Price:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtBSharePrice" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group" runat="server" id="div1">
                                    <label class="col-sm-4 control-label">Status:</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlBStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">User Name:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtBUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Password:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtBPassword" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnBrokerAdd" runat="server" Text="Add" CssClass="btn btn-success" OnClientClick="return Validate()" OnClick="btnBrokerAdd_Click" />
                            <asp:Button ID="btnBrokerClear" runat="server" CssClass="btn btn-success" Text="Clear" OnClick="btnBrokerClear_Click" />
                            <asp:Button ID="btnBrokerCancel" runat="server" Text="Cancel" CssClass="btn btn-success" OnClick="btnBrokerCancel_Click" />
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View4" runat="server">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Adviser Maintenance</h3>
                        </div>
                        <div class="box-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Name:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtAName" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Address:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtAAddress" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Telephone:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtATelephone" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Email:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtAEmail" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Description:</label>
                                    <div class="col-sm-4">
                                        <textarea id="txtADescription" class="form-control" runat="server"></textarea>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Type:</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlAType" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Join Date:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="dtpAJoinDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group" runat="server" id="div2">
                                    <label class="col-sm-4 control-label">Status:</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlAStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">User Name:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtAUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Password:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtAPassword" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnAAdd" runat="server" Text="Add" CssClass="btn btn-success" OnClientClick="return Validate()" OnClick="btnAAdd_Click" />
                            <asp:Button ID="btnAClear" runat="server" CssClass="btn btn-success" Text="Clear" OnClick="btnAClear_Click" />
                            <asp:Button ID="btnACancel" runat="server" Text="Cancel" CssClass="btn btn-success" OnClick="btnACancel_Click" />
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View5" runat="server">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Player Maintenance</h3>
                        </div>
                        <div class="box-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Name:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtPName" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Address:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtPAddress" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Telephone:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtPTelephone" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Email:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtPEmail" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Description:</label>
                                    <div class="col-sm-4">
                                        <textarea id="txtPDescription" class="form-control" runat="server"></textarea>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Type:</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlPType" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Join Date:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="dtpPJoinDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group" runat="server" id="div3">
                                    <label class="col-sm-4 control-label">Status:</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlPStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">User Name:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtPUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Password:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtPPassword" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnPAdd" runat="server" Text="Add" CssClass="btn btn-success" OnClientClick="return Validate()" OnClick="btnPAdd_Click" />
                            <asp:Button ID="btnPClear" runat="server" CssClass="btn btn-success" Text="Clear" OnClick="btnPClear_Click" />
                            <asp:Button ID="btnPCancel" runat="server" Text="Cancel" CssClass="btn btn-success" OnClick="btnPCancel_Click" />
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
