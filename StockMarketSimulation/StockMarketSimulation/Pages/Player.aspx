<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Player.aspx.cs" Inherits="StockMarketSimulation.Pages.Player" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $("[id$=txtDateOfBirth]").val($.datepicker.formatDate("yy-mm-dd", new Date()));
        //    $("[id$=txtDateOfJoined]").val($.datepicker.formatDate("yy-mm-dd", new Date()));

        //})
        function pageLoad() {
            $("[id$=txtDateOfBirth]").datepicker({
                'format': "yyyy-mm-dd",
                changeYear: false,
                changeMonth: false,
                autoclose: true
            }).attr('readonly', 'readonly');

            $("[id$=txtJoinDate]").datepicker({
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
            <asp:PostBackTrigger ControlID="btnEdit" />
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnUpdate" />
            <asp:PostBackTrigger ControlID="btnClear" />
            <asp:PostBackTrigger ControlID="btnCancle" />
        </Triggers>
        <ContentTemplate>
            <asp:MultiView ID="mvParent" runat="server">
                <asp:View ID="View1" runat="server">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Player Maintenance</h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Player Name:</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtPlayerName" runat="server" CssClass="form-control" class="control-label" AutoPostBack="true" onkeydown="return ((event.keyCode>=48 && event.keyCode<=57) || (event.keyCode>=96 && event.keyCode<=105) || (event.keyCode==8 || event.keyCode==9));"></asp:TextBox>
                                </div>
                                <label class="col-sm-1 control-label">Player Type:</label>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="ddlPlayerType" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:Button ID="btnAddPlayer" runat="server" Text="Add Record" CssClass="btn btn-success" OnClick="btnAddPlayer_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:GridView ID="gvPlayer" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" AllowPaging="True" OnRowCommand="gvPlayer_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Player Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlayerId" runat="server" Text='<%# Bind("PlayerId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Player Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Player Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType" runat="server" Text='<%# Enum.GetName(typeof(SMS.Common.PlayerType),Convert.ToInt32(Eval("Type")))%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Telephone">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelLname" runat="server" Text='<%# Bind("Telephone") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Join Date" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJoinDate" runat="server" Text='<%# Bind("JoinDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument="<%# Container.DisplayIndex %>" CommandName="ViewData" ImageUrl="~/dist/img/icon_view.png" />
                                                </ItemTemplate>
                                                <ItemStyle Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument="<%# Container.DisplayIndex %>" CommandName="EditData" ImageUrl="~/dist/img/icon_edit.png" />
                                                </ItemTemplate>
                                                <ItemStyle Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton3" runat="server" CommandArgument="<%# Container.DisplayIndex %>" CommandName="DeleteData" ImageUrl="~/dist/img/icon_delete.png" />
                                                </ItemTemplate>
                                                <ItemStyle Width="1%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle CssClass="alt" />
                                        <EmptyDataTemplate>
                                            No Records Available.
                                        </EmptyDataTemplate>
                                        <PagerStyle CssClass="cpb-pagination" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Player Maintenance</h3>
                        </div>
                        <div class="box-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Name:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Address:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Telephone:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtTelephone" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Email:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Description:</label>
                                    <div class="col-sm-4">
                                        <textarea id="txtDescription" class="form-control" runat="server"></textarea>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Type:</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Join Date</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtJoinDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>                                
                                <div class="form-group" runat="server" id="divPaymentType">
                                    <label class="col-sm-2 control-label">Status:</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">User Name:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Password:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-success" OnClick="btnEdit_Click" />
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-success" OnClientClick="return Validate()" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success" OnClientClick="return Validate()" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-success" Text="Clear" OnClick="btnClear_Click" />
                            <asp:Button ID="btnCancle" runat="server" Text="Cancel" CssClass="btn btn-success" OnClick="btnCancle_Click" />
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">

        $(document).ready(function () {
            Validate = function () {
                $('#<% = txtName.ClientID %>').addClass('validate[required]');
                $('#<% = txtDescription.ClientID %>').addClass(' validate[required]');
                $('#<% = txtEmail.ClientID %>').addClass('validate[required]');
                $('#<% = txtJoinDate.ClientID %>').addClass('validate[required]');
                $('#<% = txtTelephone.ClientID %>').addClass('validate[required]');
                $('#<% = txtUserName.ClientID %>').addClass('validate[required]');
                $('#<% = txtPassword.ClientID %>').addClass('validate[required]');
                $('#<% = txtPassword.ClientID %>').addClass('validate[required]');
                $('#<% = ddlType.ClientID %>').addClass('validate[required]');
                $('#<% = ddlStatus.ClientID %>').addClass('validate[required]');

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

            $(function () {
                $('[id*=btnEdit]').bind("click", function () {
                    $("#form1").validationEngine('detach');

                });
                $('[id*=btnClear]').bind("click", function () {
                    $("#form1").validationEngine('detach');

                });
                $('[id*=btnCancle]').bind("click", function () {
                    $("#form1").validationEngine('detach');

                });
            });
        });
        
    </script>
</asp:Content>
