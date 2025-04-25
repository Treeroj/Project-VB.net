<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SMTMB_ADMIN_MAT.aspx.vb" Inherits="SMTMB_ADMIN_MAT" %>

<%@ Register Src="HeaderNavbar.ascx" TagName="HeaderNavbar" TagPrefix="uc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <link href="https://fonts.gstatic.com" rel="preconnect" />
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Nunito:300,300i,400,400i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/bootstrap-icons.css" rel="stylesheet" />
    <link href="assets/css/boxicons.min.css" rel="stylesheet" />
    <link href="assets/css/quill.snow.css" rel="stylesheet" />
    <link href="assets/css/quill.bubble.css" rel="stylesheet" />
    <link href="assets/css/remixicon.css" rel="stylesheet" />
    <link href="assets/css/simple-datatables.css" rel="stylesheet" />
    <link href="assets/css/style.css" rel="stylesheet" />
    <link href="assets/css/datatables.min.css" rel="stylesheet" />

        <style type="text/css">
    #loading-container {
      position: fixed;
      top: 0;
      left: 0;
      width: 100%;
      height: 100%;
      background-color: rgba(255, 255, 255, 0.8);
      display: flex;
      justify-content: center;
      align-items: center;
      z-index: 9999;
    }

    #loading-container img {
      width: 200px; /* ปรับขนาดตามที่คุณต้องการ */
      height: 200px; /* ปรับขนาดตามที่คุณต้องการ */
    }
  </style>

    <script>
        function pageLoad(sender, args) {
            $(document).ready(function () {
                $(function () {
                    $(".js-example-placeholder-single").select2({
                        /*placeholder: "กรุณาเลือก Code",*/
                        allowClear: true
                    });

                });
            });
        }
    </script>
    <script type="text/javascript">
        window.onload = function () {
            document.getElementById("loading").style.display = "none";
        };

        window.onbeforeunload = function () {
            document.getElementById("loading").style.display = "block";
        };

        function showLoading() {
            document.getElementById("loading").style.display = "block";
        }

    </script>

    <title></title>
</head>
<body>


      <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <uc:HeaderNavbar ID="header" runat="server" />
        <main id="main" class="main">
            <div class="pagetitle">
                <h1>Material Record for SMT Admin Change Material</h1>
                <nav>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="http://172.16.72.179/MT800PST-ALLPRODUCT/">MT800</a></li>
                        <li class="breadcrumb-item">MaterialBarcodeSystem</li>
                        <li class="breadcrumb-item active">Change Material</li>
                    </ol>
                </nav>
            </div>
            <section class="section">
                <div class="row">
                    <div class="col-lg-4">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h3 class="card-title">Login Admin Change Material</h3>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="card-text">ไอดีแอดมิน / AdminID :</div>
                                        <asp:Panel ID="Panel1" runat="server">
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="tbUser" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                    Width="100%" class="form-control" placeholder="ใส่ ID Admin" onblur="__doPostBack('','');" />
                                            </label>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel5" runat="server" Visible="false">
                                            <div class="card-text">รหัสแอดมิน / AdminPassword :</div>
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="tbpassword" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true" TextMode="Password"
                                                    Width="100%" class="form-control" placeholder="ใส่ PasswordAdmin" onblur="__doPostBack('','');" OnTextChanged="tbpassword_TextChanged" />
                                            </label>
                                        </asp:Panel>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="card">
                            <asp:Panel ID="Panel7" runat="server" Visible="false">
                                <div class="card-body">
                                    <div class="text-center">
                                        <h3 class="card-title">Search</h3>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="card-text">
                                                ค้นหา MODEL / Search MODEL :
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="TextBox1" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                    Width="100%" class="form-control" placeholder="ใส่ MODEL สำหรับค้นหา" onblur="__doPostBack('','');" />
                                            </label>
                                            </div>
                                            </br>
                                                <div class="card-text">
                                                    ค้นหา MATERIAL / Search MATERIAL :
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="TextBox2" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                    Width="100%" class="form-control" placeholder="ใส่ MATERIAL สำหรับค้นหา" onblur="__doPostBack('','');" />
                                            </label>
                                                </div>
                                            </br>
                                            <asp:Panel ID="PNddlLinePD" runat="server">
                                            <div class="card-text">ผลิตภัณฑ์ / Product :</div>
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:DropDownList ID="ddlLinePD" runat="server" AutoPostBack="true"
                                                    CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                </asp:DropDownList>
                                            </label>
                                        </asp:Panel>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                                        <div class="col-lg-4">
                        <div class="card">
                            <asp:Panel ID="Panel11" runat="server" Visible="false">
                                <div class="card-body">
                                    <div class="text-center">
                                        <h3 class="card-title">Search</h3>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="card-text">
                                                ค้นหา MACHINE / Search MACHINE :
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="TextBox4" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                    Width="100%" class="form-control" placeholder="ใส่ MACHINE สำหรับค้นหา" onblur="__doPostBack('','');" />
                                            </label>
                                            </div>
                                            </br>
                                                <div class="card-text">
                                                    ค้นหา FEEDNO / Search FEEDNO :
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="TextBox5" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                    Width="100%" class="form-control" placeholder="ใส่ FEEDNO สำหรับค้นหา" onblur="__doPostBack('','');" />
                                            </label>
                                                </div>
                                            </br>
                                               <%-- <div class="card-text">
                                                    ค้นหา PRODUCT / Search PRODUCT :
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="TextBox6" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                    Width="100%" class="form-control" placeholder="ใส่ PRODUCT สำหรับค้นหา" onblur="__doPostBack('','');" />
                                            </label>
                                                </div>--%>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                    <div class="col-lg-4">
                        <asp:Panel ID="Panel3" runat="server" Visible="false">
                            <div class="card">

                                <div class="card-body">

                                    <div class="text-center">

                                        <h3 class="card-title">Register Model and Material</h3>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />

                                            <asp:PostBackTrigger ControlID="Button1" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel2" runat="server">
                                                <div class="card-text">Model :</div>

                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="tbModel" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                        Width="100%" class="form-control" placeholder="ใส่ Model" onblur="__doPostBack('','');" />
                                                </label>
                                                </br>
                                            </br>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel6" runat="server">
                                                <div class="card-text">Material :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="tbMaterial" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                        Width="100%" class="form-control" placeholder="ใส่ Material" onblur="__doPostBack('','');" />
                                                </label>

                                            </asp:Panel>
                                            </br>
                                         <asp:Panel ID="Panel8" runat="server">
                                             <div class="card-text">SCM :</div>
                                             <label class="custom-field three" style="width: 100%">
                                                 <asp:TextBox ID="tbSCM" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                     Width="100%" class="form-control" placeholder="ใส่ SCM" onblur="__doPostBack('','');" />
                                             </label>

                                         </asp:Panel>
                                            </br>                                          

                                        <asp:Panel ID="PNBUT" runat="server" class="text-center" Visible="false">
                                            <%--<button id="Button1" runat="server"  type="button" class="btn btn-outline-dark"  >ยืนยันการลงข้อมูล</button>--%>
                                            <asp:Button ID="Button1" runat="server" Text="ยืนยันการลงข้อมูล" type="button" class="btn btn-primary" />
                                        </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>

                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <asp:Panel ID="Panel4" runat="server" Visible="false">
                                <div class="card-body">
                                    <div class="text-center">
                                        <h3 class="card-title">Model and Material</h3>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                            <asp:PostBackTrigger ControlID="btnConfirmDelete" />
                                            <asp:PostBackTrigger ControlID="btnConCfirmDelete" />
                                            <asp:PostBackTrigger ControlID="Button1" />
                                        </Triggers>
                                        <ContentTemplate>

                                            <asp:Panel ID="PanelMATERIALHIS" runat="server">

                                                <div class="table-responsive">


                                                    <h5 class="card-title">MAT HIS</h5>
                                                    <asp:GridView ID="gvMODEL" runat="server" ShowHeader="true" AutoGenerateColumns="false"
                                                        CssClass="table table-hover table-scrollable"
                                                        OnRowEditing="gvMODEL_RowEditing" OnRowUpdating="gvMODEL_RowUpdating" OnRowCancelingEdit="gvMODEL_RowCancelingEdit" OnRowDeleting="gvMODEL_RowDeleting"
                                                        DataKeyNames="ROWID"
                                                        OnPageIndexChanging="OnPageIndexChanging" PageSize="20"
                                                        AllowPaging="true">
                                                        <Columns>
                                                            <asp:BoundField DataField="MACHINE" HeaderText="MACHINE" />
                                                            <asp:BoundField DataField="FEEDNO" HeaderText="FEEDNO" />
                                                            <asp:BoundField DataField="MODEL" HeaderText="MODEL" />
                                                            <asp:BoundField DataField="MATERIAL" HeaderText="MATERIAL" />
                                                            <asp:BoundField DataField="PRODUCTS" HeaderText="PRODUCTS" />
                                                            <asp:BoundField DataField="PROCESS" HeaderText="PROCESS" />
                                                            <asp:TemplateField HeaderText="Update">
                                                                <ItemTemplate>
                                                                    <asp:Button runat="server" Text="Edit" CommandName="Edit" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Button runat="server" Text="Update" CommandName="Update" />
                                                                    <asp:Button runat="server" Text="Cancel" CommandName="Cancel" />
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Button runat="server" Text="Insert" CommandName="Insert" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:Button runat="server" Text="Delete" CommandName="Delete" OnClientClick='<%# "return showDeleteConfirmation(""" + Eval("ROWID").ToString() + """);" %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:HiddenField ID="hfCustomerId" runat="server" />
                                                    <div id="confirmDeleteModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
                                                        <div class="modal-dialog">
                                                            <div class="modal-content">
                                                                <div class="modal-header">
                                                                    <h4 class="modal-title">ยืนยันการลบข้อมูล</h4>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <p>คุณต้องการลบข้อมูล ใช่/Yes หรือ ไม่/No?</p>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <asp:Button ID="btnConfirmDelete" runat="server" Text="ใช่" CssClass="btn btn-danger" OnClick="btnConfirmDelete_Click" />
                                                                    <asp:Button ID="btnConCfirmDelete" runat="server" Text="ไม่" CssClass="btn btn-default" OnClick="btnConfirmCDelete_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-- JavaScript Function to Show Delete Confirmation Modal -->
                                                    <script type="text/javascript">
                                                        function showDeleteConfirmation(rowId) {
                                                            document.getElementById('<%= hfCustomerId.ClientID %>').value = rowId;
                                                            confirmDeleteModal = $('#confirmDeleteModal');
                                                            confirmDeleteModal.modal('show');
                                                            return false; // Prevent postback
                                                        }
                                                    </script>
                                                </div>


                                            </asp:Panel>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <asp:Panel ID="Panel9" runat="server" Visible="false">
                                <div class="card-body">
                                    <div class="text-center">
                                        <h3 class="card-title">Change History</h3>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                            <asp:PostBackTrigger ControlID="btnConfirmDelete" />
                                            <asp:PostBackTrigger ControlID="btnConCfirmDelete" />
                                            <asp:PostBackTrigger ControlID="Button1" />

                                        </Triggers>
                                        <ContentTemplate>

                                            <asp:Panel ID="Panel10" runat="server">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvHIS" runat="server" ShowHeader="true" AutoGenerateColumns="false"
                                                        CssClass="table table-hover table-scrollable">
                                                        <Columns>
                                                            <asp:BoundField DataField="ADMINCHANGE" HeaderText="ADMINCHANGE" />
                                                            <asp:BoundField DataField="DAY" HeaderText="DAY" />
                                                            <asp:BoundField DataField="TIME" HeaderText="TIME" />
                                                            <asp:BoundField DataField="MODEL" HeaderText="MODEL" />

                                                            <asp:BoundField DataField="MATERIAL" HeaderText="MATERIAL" />
                                                            <asp:BoundField DataField="FEEDNO" HeaderText="FEEDNO" />
                                                            <asp:BoundField DataField="MACHINE" HeaderText="MACHINE" />
                                                            <asp:BoundField DataField="STATUT" HeaderText="STATUT" />
                                                            <asp:BoundField DataField="PRODUCTS" HeaderText="PRODUCTS" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>


                                            </asp:Panel>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </section>
        </main>



            <a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>

        <script src="assets/js/jquery.min.js" type="text/javascript"></script>
        <script src="assets/js/popper.min.js" type="text/javascript"></script>
        <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>


        <script src="assets/js/apexcharts.min.js" type="text/javascript"></script>
        <script src="assets/js/bootstrap.bundle.min.js" type="text/javascript"></script>
        <script src="assets/js/chart.min.js" type="text/javascript"></script>
        <script src="assets/js/echarts.min.js" type="text/javascript"></script>
        <script src="assets/js/quill.min.js" type="text/javascript"></script>
        <script src="assets/js/simple-datatables.js" type="text/javascript"></script>
        <script src="assets/js/tinymce.min.js" type="text/javascript"></script>
        <script src="assets/js/validate.js" type="text/javascript"></script>
        <script src="assets/js/main.js" type="text/javascript"></script>

    </form>
        <div id="loading-container">
    <img src="image/fluid-loader.gif" alt="กำลังโหลดข้อมูล"/>
  </div>

    <script type="text/javascript">
        window.addEventListener('load', function () {
            // เมื่อหน้าเว็บโหลดเสร็จ
            var loadingContainer = document.getElementById('loading-container');
            loadingContainer.style.display = 'none';
        });
    </script>
</body>
</html>
