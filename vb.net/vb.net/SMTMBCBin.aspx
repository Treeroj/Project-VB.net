<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SMTMBCBin.aspx.vb" Inherits="SMTMBCBin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
        <header id="header" class="header fixed-top d-flex align-items-center">
            <div class="d-flex align-items-center justify-content-between">
                <a href="index.html" class="logo d-flex align-items-center">
                    <span class="d-none d-lg-block">MT800 NVP.</span>
                    <img src="<%= ResolveUrl("~/image/Gear.gif") %>" alt="Loading..." width="10%" />
                </a>
                <i class="bi bi-list toggle-sidebar-btn"></i>
            </div>
            <div class="search-bar">
            </div>
            <nav class="header-nav ms-auto">
                <ul class="d-flex align-items-center">
                    <li class="nav-item d-block d-lg-none">
                        <a class="nav-link nav-icon search-bar-toggle " href="#">
                            <i class="bi bi-search"></i>
                        </a>
                    </li>
                    <li class="nav-item dropdown pe-3">
                        <a class="nav-link nav-profile d-flex align-items-center pe-0" href="#" data-bs-toggle="dropdown">
                            <img src="image/116496843.jfif" alt="Profile" class="rounded-circle" />
                            <span class="d-none d-md-block dropdown-toggle ps-2">840PRASS เซิ่นเจิ้น</span>
                        </a>
                    </li>
                </ul>
            </nav>
        </header>
        <aside id="sidebar" class="sidebar">
            <ul class="sidebar-nav" id="sidebar-nav">
                <li class="nav-item">
                    <a class="nav-link collapsed" href="index.html">
                        <i class="bi bi-grid"></i>
                        <span>Dashboard</span>
                    </a>
                </li>
                <li class="nav-heading" style="font-size: 1rem;">SMT</li>
                <li class="nav-item">
                    <a class="nav-link  collapsed" data-bs-target="#MaterialBarcodeSystem-nav" data-bs-toggle="collapse" href="#">
                        <i class="bi bi-menu-button-wide"></i>
                        <span>MaterialBarcodeSystem (SMT)</span><i class="bi bi-chevron-down ms-auto"></i>
                    </a>
                    <ul id="MaterialBarcodeSystem-nav" class="nav-content collapse" data-bs-parent="#sidebar-nav">
                        <li>
                            <a href="SMTMBSetup.aspx" class="active">
                                <i class="bi bi-circle"></i>
                                <span>Setup Model</span>
                            </a>
                        </li>
                        <li>
                            <a href="SMTMBChangeMat.aspx">
                                <i class="bi bi-circle"></i><span>Change Material</span>
                            </a>
                        </li>
                        <li>
                            <a href="SMTMBChangeLot.aspx">
                                <i class="bi bi-circle"></i><span>Change Lot</span>
                            </a>
                        </li>
                        <li>
                            <a href="SMTMBChangeCass.aspx">
                                <i class="bi bi-circle"></i><span>Change Cassette</span>
                            </a>
                        </li>
                    </ul>
                </li>

                <%--add--%>
                <li class="nav-item">
                    <a class="nav-link  collapsed" data-bs-target="#SMTHistory-nav" data-bs-toggle="collapse" href="#">
                        <i class="bi bi-menu-button-wide"></i>
                        <span>SMT History</span><i class="bi bi-chevron-down ms-auto"></i>
                    </a>
                    <ul id="SMTHistory-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
                        <li>
                            <a href="SMTMBCHIS.aspx">
                                <i class="bi bi-circle"></i>
                                <span>History</span>
                            </a>
                        </li>
                    </ul>
                </li>
                <li class="nav-item">
                    <a class="nav-link " data-bs-target="#SMTBin-nav" data-bs-toggle="collapse" href="#">
                        <i class="bi bi-menu-button-wide"></i>
                        <span>SMT Change And Edit</span><i class="bi bi-chevron-down ms-auto"></i>
                    </a>
                    <ul id="SMTBin-nav" class="nav-content collapse show" data-bs-parent="#sidebar-nav">
                        <li>
                            <a href="SMTMBCBin.aspx" class="active">
                                <i class="bi bi-circle"></i>
                                <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                                <span>Binno Change and Edit</span>
                            </a>
                        </li>
                    </ul>
                </li>


                <li class="nav-heading" style="font-size: 1rem;">Assembly</li>

                <li class="nav-item">
                    <a class="nav-link  collapsed" data-bs-target="#MBSetupModelAssembly-nav" data-bs-toggle="collapse" href="#">

                        <i class="bi bi-journal-text"></i><span>MaterialBarcodeSystem (Assembly)</span><i class="bi bi-chevron-down ms-auto"></i>
                    </a>
                    <ul id="MBSetupModelAssembly-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
                        <li>
                            <a href="MBSetupModelAssembly.aspx ">
                                <i class="bi bi-circle"></i><span>Material Record for Assembly</span>
                            </a>
                        </li>
                        <li>
                            <a href="MBSetupModelAssembly.aspx">
                                <i class="bi bi-circle"></i><span>Open History</span>
                            </a>
                        </li>
                    </ul>
                </li>

            </ul>
        </aside>

        <main id="main" class="main">
            <div class="pagetitle">
                <h1>History : SMT</h1>
                <nav>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="http://172.16.72.179/MT800PST-ALLPRODUCT/">MT800</a></li>
                        <li class="breadcrumb-item">MaterialBarcodeSystem</li>
                        <li class="breadcrumb-item active">History : SMT </li>
                    </ol>
                </nav>
            </div>
            <section class="section">
                <%-- <div class="text-center" id="loading" >
                <img src="<%= ResolveUrl("~/image/fluid-loader.gif") %>" alt="Loading..." width="10%"  />
            </div>--%>
                <div class="row">

                    <div class="col-lg-4">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h3 class="card-title">Genaral Information</h3>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="tbpassword" EventName="TextChanged" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="card-text">ไอดีแอดมิน / AdminID :</div>
                                        <asp:Panel ID="Panel1" runat="server">
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="tbUser" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                    Width="100%" class="form-control" placeholder="ใส่รหัสพนักงาน" onblur="__doPostBack('','');" />
                                            </label>
                                            <div class="card-text">รหัสแอดมิน / AdminPassword :</div>
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="tbpassword" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                    Width="100%" class="form-control" placeholder="ใส่รหัสพนักงาน" onblur="__doPostBack('','');" />
                                            </label>
                                        </asp:Panel>
                                        </br>
                                            <asp:Panel ID="PNddlLinePD" runat="server" Visible="false">
                                                <div class="card-text">โปรดักส์ / Product :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:DropDownList ID="ddlLinePD" runat="server" AutoPostBack="true"
                                                        CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                    </asp:DropDownList>
                                                </label>
                                            </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-body">

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="tbpassword" EventName="TextChanged" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel3" runat="server" Visible="true">
                                            <div class="card-body">
                                                <div class="text-center">
                                                    <h1 class="card-title">ดาวน์โหลด Excel :</h1>
                                                </div>
                                                <div class="d-grid gap-2 col-6 mx-auto">
                                                    <asp:Button ID="btnExportEC1" runat="server" Text="Export To Excel" type="button" class="btn btn-primary" />
                                                </div>
                                            </div>
                                        </asp:Panel>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-8">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h3 class="card-title">Genaral Information</h3>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="tbpassword" EventName="TextChanged" />
                                    </Triggers>
                                    <ContentTemplate>

                                        <asp:Panel ID="Panel2" runat="server">
                                            <div class="card-text">โปรดักส์ / Product :</div>
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"
                                                    CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                </asp:DropDownList>
                                            </label>
                                        </asp:Panel>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>


                        <div class="card">
                            <div class="card-body">

                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="tbpassword" EventName="TextChanged" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel4" runat="server">
                                            <div class="card-body">
                                                <div class="text-center">
                                                    <h3 class="card-title">Select Information</h3>
                                                </div>
                                                <div class="card-text">MATERIAL :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="TextBox1" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true"
                                                        Width="100%" class="form-control" placeholder="ค้นหา MATERIAL" onblur="__doPostBack('','');" />
                                                </label>
                                                <br />
                                                <div class="card-text">BINNO :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="TextBox2" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true"
                                                        Width="100%" class="form-control" placeholder="ค้นหา BINNO" onblur="__doPostBack('','');" />
                                                </label>
                                            </div>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>


                    </div>



                    <div class="col-lg-12">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tbpassword" EventName="TextChanged" />
                                <asp:PostBackTrigger ControlID="btnConfirmDelete" />
                                 <asp:PostBackTrigger ControlID="btnConCfirmDelete" />
                                <asp:PostBackTrigger ControlID="btnExportEC1" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="PanelMATERIALHIS" runat="server">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="table-responsive">
                                                <h5 class="card-title">MATERIAL HIS</h5>
                                                <asp:GridView ID="gvBin" runat="server" ShowHeader="true" AutoGenerateColumns="false"
                                                    CssClass="table table-hover table-scrollable"
                                                    OnRowEditing="gvBin_RowEditing" OnRowUpdating="gvBin_RowUpdating" OnRowCancelingEdit="gvBin_RowCancelingEdit" OnRowDeleting="gvBin_RowDeleting" OnRowCommand="gvBin_RowCommand"
                                                    DataKeyNames="ROWID">
                                                    <Columns>
                                                        <asp:BoundField DataField="MATERIAL" HeaderText="Material" />
                                                        <asp:BoundField DataField="BINNO" HeaderText="BinNo" />
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
                                                                 <asp:Button ID="btnConCfirmDelete" runat="server" Text="ไม่" CssClass="btn btn-default" OnClick="btnConfirmCDelete_Click" data-dismiss="modal"/>
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
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
        <%-- <script src="assets/js/datatables.min.js" type="text/javascript"></script>--%>
    </form>
</body>
</html>


