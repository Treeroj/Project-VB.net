<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PrassMonitoring.aspx.vb" Inherits="PrassMonitoring" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <%--    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/bootstrap/css/bootstrap-toggle.min.css" rel="stylesheet" type="text/css" />
    <script src="assets/jquery/jquery.min.js" type="text/javascript"></script>
    <script src="assets/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/bootstrap/js/bootstrap-toggle.min.js" type="text/javascript"></script>
    <link href="assets/fontawesome/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/fontawesome/css/fontawesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/css/PST.css" rel="stylesheet" type="text/css" />
    <link href="Styles/css/select2.css" rel="stylesheet" type="text/css" />
    <script src="Styles/js/select2.js" type="text/javascript"></script>--%>

    <%--<script src="assets/libs/jquery/dist/jquery.min.js" type="text/javascript"></script>
    <script src="assets/libs/bootstrap/dist/js/bootstrap.bundle.min.js" type="text/javascript"></script>
    <script src="assets/js/sidebarmenu.js" type="text/javascript"></script>
    <script src="assets/js/app.min.js" type="text/javascript"></script>
    <script src="assets/libs/apexcharts/dist/apexcharts.min.js" type="text/javascript"></script>
    <script src="assets/libs/simplebar/dist/simplebar.js" type="text/javascript"></script>
    <script src="assets/js/dashboard.js" type="text/javascript"></script>
    <link rel="shortcut icon" type="image/png" href="imgPST/mrt_logo.png" />
    <link rel="stylesheet" href="assets/css/styles.min.css" />
    <link rel="Stylesheet" href="assets/css/CSSGV.css" />--%>
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
    <script src="assets/js/apexcharts.min.js" type="text/javascript"></script>
        <script src="assets/js/bootstrap.bundle.min.js" type="text/javascript"></script>
        <script src="assets/js/chart.min.js" type="text/javascript"></script>
        <script src="assets/js/echarts.min.js" type="text/javascript"></script>
        <script src="assets/js/quill.min.js" type="text/javascript"></script>
        <script src="assets/js/simple-datatables.js" type="text/javascript"></script>
        <script src="assets/js/tinymce.min.js" type="text/javascript"></script>
        <script src="assets/js/validate.js" type="text/javascript"></script>
        <script src="assets/js/main.js" type="text/javascript"></script>
        <script src="assets/js/datatables.min.js" type="text/javascript"></script>

    <title>PrassMonitoring</title>



</head>
<body>
    <form id="form1" runat="server" method="post" novalidate>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Timer ID="Timer1" runat="server" Interval="20000" Enabled="false"></asp:Timer>
        <div style="font-family: 'Century Gothic'">
            <!--  Body Wrapper -->
            <div class="page-wrapper" id="main-wrapper" data-layout="vertical" data-navbarbg="skin6" data-sidebartype="full"
                data-sidebar-position="fixed" data-header-position="fixed">
                <!-- Sidebar Start -->
                <aside class="left-sidebar">
                    <!-- Sidebar scroll-->
                    <div>
                        <div class="brand-logo d-flex align-items-center justify-content-between">
                            <%--ChangePrass MT800--%>
                            <a href="./MasterPage.master" class="text-nowrap logo-img">
                                <img src="imgPST/mrt_logo.png" width="220" alt="" />
                            </a>
                            <div class="close-btn d-xl-none d-block sidebartoggler cursor-pointer" id="sidebarCollapse">
                                <i class="ti ti-x fs-8"></i>
                            </div>
                        </div>
                        <!-- Sidebar navigation-->
                        <nav class="sidebar-nav scroll-sidebar" data-simplebar="">
                            <ul id="sidebarnav">

                                <li class="mb-1">
        <button class="btn btn-toggle align-items-center rounded" data-bs-toggle="collapse" data-bs-target="#home-collapse" aria-expanded="true">
          Home
        </button>
        <div class="collapse show" id="home-collapse" style="">
          <ul class="btn-toggle-nav list-unstyled fw-normal pb-1 small">
            <li><a href="#" class="link-dark rounded">Overview</a></li>
            <li><a href="#" class="link-dark rounded">Updates</a></li>
            <li><a href="#" class="link-dark rounded">Reports</a></li>
          </ul>
        </div>
      </li>


                                <li class="nav-small-cap">
                                    <i class="ti ti-dots nav-small-cap-icon fs-4"></i>
                                    <span class="hide-menu">Home</span>
                                </li>
                                <li class="sidebar-item">
                                    <%--ChangePrass MT800--%>
                                    <a class="sidebar-link" href="http://172.16.72.179/MT800PST-ALLPRODUCT/" aria-expanded="false">
                                        <span>
                                            <i class="ti ti-layout-dashboard"></i>
                                        </span>
                                        <%--ChangePrass MT800--%>
                                        <span class="hide-menu">MT800</span>
                                    </a>
                                </li>
                                <li class="nav-small-cap">
                                    <i class="ti ti-dots nav-small-cap-icon fs-4"></i>
                                    <span class="hide-menu">UI COMPONENTS</span>
                                </li>
                                <li class="sidebar-item">
                                    <%--ChangePrass MT800--%>
                                    <a class="sidebar-link" href="PrassMonitoring.aspx" aria-expanded="false">
                                        <span>
                                            <i class="ti ti-article"></i>
                                        </span>
                                        <%--ChangePrass MT800--%>
                                        <span class="hide-menu">PrassMonitoring</span>
                                    </a>
                                </li>
                                <li class="sidebar-item">
                                    <%--ChangePrass MT800--%>
                                    <a class="sidebar-link" href="800PST.aspx" aria-expanded="false">
                                        <span>
                                            <i class="ti ti-alert-circle"></i>
                                        </span>
                                        <%--ChangePrass MT800--%>
                                        <span class="hide-menu">MT800PST</span>
                                    </a>
                                </li>
                                <li class="sidebar-item">
                                    <a class="sidebar-link" href="./ui-card.html" aria-expanded="false">
                                        <span>
                                            <i class="ti ti-cards"></i>
                                        </span>
                                        <span class="hide-menu">Card</span>
                                    </a>
                                </li>
                                <li class="sidebar-item">
                                    <a class="sidebar-link" href="./ui-forms.html" aria-expanded="false">
                                        <span>
                                            <i class="ti ti-file-description"></i>
                                        </span>
                                        <span class="hide-menu">Forms</span>
                                    </a>
                                </li>
                                <li class="sidebar-item">
                                    <a class="sidebar-link" href="./ui-typography.html" aria-expanded="false">
                                        <span>
                                            <i class="ti ti-typography"></i>
                                        </span>
                                        <span class="hide-menu">Typography</span>
                                    </a>
                                </li>
                                <li class="nav-small-cap">
                                    <i class="ti ti-dots nav-small-cap-icon fs-4"></i>
                                    <span class="hide-menu">AUTH</span>
                                </li>
                                <li class="sidebar-item">
                                    <a class="sidebar-link" href="./authentication-login.html" aria-expanded="false">
                                        <span>
                                            <i class="ti ti-login"></i>
                                        </span>
                                        <span class="hide-menu">Login</span>
                                    </a>
                                </li>
                                <li class="sidebar-item">
                                    <a class="sidebar-link" href="./authentication-register.html" aria-expanded="false">
                                        <span>
                                            <i class="ti ti-user-plus"></i>
                                        </span>
                                        <span class="hide-menu">Register</span>
                                    </a>
                                </li>
                                <li class="nav-small-cap">
                                    <i class="ti ti-dots nav-small-cap-icon fs-4"></i>
                                    <span class="hide-menu">EXTRA</span>
                                </li>
                                <li class="sidebar-item">
                                    <a class="sidebar-link" href="./icon-tabler.html" aria-expanded="false">
                                        <span>
                                            <i class="ti ti-mood-happy"></i>
                                        </span>
                                        <span class="hide-menu">Icons</span>
                                    </a>
                                </li>
                                <li class="sidebar-item">
                                    <a class="sidebar-link" href="./sample-page.html" aria-expanded="false">
                                        <span>
                                            <i class="ti ti-aperture"></i>
                                        </span>
                                        <span class="hide-menu">Sample Page</span>
                                    </a>
                                </li>
                            </ul>
                            <div class="unlimited-access hide-menu bg-light-primary position-relative mb-7 mt-5 rounded">
                                <div class="d-flex">
                                    <div class="unlimited-access-title me-3">
                                        <h6 class="fw-semibold fs-4 mb-6 text-dark w-85">Upgrade to pro</h6>
                                        <a href="https://adminmart.com/product/modernize-bootstrap-5-admin-template/" target="_blank" class="btn btn-primary fs-2 fw-semibold lh-sm">Buy Pro</a>
                                    </div>
                                    <div class="unlimited-access-img">
                                        <img src="../assets/images/backgrounds/rocket.png" alt="" class="img-fluid">
                                    </div>
                                </div>
                            </div>
                        </nav>
                        <!-- End Sidebar navigation -->
                    </div>
                    <!-- End Sidebar scroll-->
                </aside>
                <!--  Sidebar End -->
                <!--  Main wrapper -->
                <div class="body-wrapper">
                    <!--  Header Start -->
                    <header class="app-header">
                        <nav class="navbar navbar-expand-lg navbar-light">
                            <ul class="navbar-nav">
                                <li class="nav-item d-block d-xl-none">
                                    <a class="nav-link sidebartoggler nav-icon-hover" id="headerCollapse" href="javascript:void(0)">
                                        <i class="ti ti-menu-2"></i>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link nav-icon-hover" href="javascript:void(0)">
                                        <i class="ti ti-bell-ringing"></i>
                                        <div class="notification bg-primary rounded-circle"></div>
                                    </a>
                                </li>
                            </ul>
                            <div class="navbar-collapse justify-content-end px-0" id="navbarNav">
                                <ul class="navbar-nav flex-row ms-auto align-items-center justify-content-end">
                                    <a href="https://adminmart.com/product/modernize-free-bootstrap-admin-dashboard/" target="_blank" class="btn btn-primary">Download Free</a>
                                    <li class="nav-item dropdown">
                                        <a class="nav-link nav-icon-hover" href="javascript:void(0)" id="drop2" data-bs-toggle="dropdown"
                                            aria-expanded="false">
                                            <img src="assets/images/profile/user-1.jpg" alt="" width="35" height="35" class="rounded-circle">
                                        </a>
                                        <div class="dropdown-menu dropdown-menu-end dropdown-menu-animate-up" aria-labelledby="drop2">
                                            <div class="message-body">
                                                <a href="javascript:void(0)" class="d-flex align-items-center gap-2 dropdown-item">
                                                    <i class="ti ti-user fs-6"></i>
                                                    <p class="mb-0 fs-3">My Profile</p>
                                                </a>
                                                <a href="javascript:void(0)" class="d-flex align-items-center gap-2 dropdown-item">
                                                    <i class="ti ti-mail fs-6"></i>
                                                    <p class="mb-0 fs-3">My Account</p>
                                                </a>
                                                <a href="javascript:void(0)" class="d-flex align-items-center gap-2 dropdown-item">
                                                    <i class="ti ti-list-check fs-6"></i>
                                                    <p class="mb-0 fs-3">My Task</p>
                                                </a>
                                                <a href="./authentication-login.html" class="btn btn-outline-primary mx-3 mt-2 d-block">Logout</a>
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </nav>
                    </header>
                    <!--  Header End -->
                    <div class="container-fluid">
                        <div class="container-fluid">
                            <div class="card">
                                <div class="card-body">
                                    <h5 class="card-title fw-semibold mb-4">PrassMonitoring</h5>
                                    <div class="card">
                                        <div class="card-body p-4">
                                            <h1>ข้อมูลการผลิต</h1>
                                            </br>
                            <h2>ล็อตการผลิต</h2>
                                            </br>
                    <%--Change prass Data--%>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnPrasslot" EventName="ServerClick" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:Panel ID="pnPrasslot" runat="server" Visible="true">
                                                        <label class="custom-field three" style="width: 100%">
                                                            <asp:TextBox ID="tbPrasslot" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                                required Width="100%" class="form-control" placeholder="กรุณากรอกหมายเลข Lot" />
                                                        </label>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            </br>
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <a id="btnPrasslot" type="button" runat="server" class="btn btnEnter" enableviewstate="False" onclick="javascript:clsAnn();">ยืนยัน Lot No.</a>
                            <script type="text/javascript">
                                function clsAnn() {

                                }
                            </script>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnPrasslot" EventName="ServerClick" />
                                            <asp:AsyncPostBackTrigger ControlID="tbRegisMcSearch" EventName="TextChanged" />
                                            <asp:PostBackTrigger ControlID="btnExport" />
                                            <asp:PostBackTrigger ControlID="btnExportEC1" />
                                            <asp:PostBackTrigger ControlID="btnExportEC2" />
                                            <asp:AsyncPostBackTrigger ControlID="GridViewDF" EventName="SelectedIndexChanged" />


                                            <%--<asp:AsyncPostBackTrigger ControlID="btnExport" EventName="Click" />--%>
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Panel ID="pnPrass" runat="server" Visible="false">
                                                <div class="card">
                                                    <div class="card-body p-4">

                                                        <h5 class="card-title fw-semibold mb-4">

                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                <Triggers>
                                                                    <%-- <asp:PostBackTrigger ControlID="btnAnnEnter" />--%>
                                                                </Triggers>
                                                                <ContentTemplate>
                                                                    </br>
                                        <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                            <li>Lot no. <a id="lbshowFRONTENDLINE" class="navbar-item" runat="server" onclick="javascript:MnHistoryOpennn();"></a></li>
                                        </ul>
                                                                    </br>
                                        <asp:TextBox ID="tbRegisMcSearch" runat="server" autocomplete="off" ForeColor="Blue" AutoPostBack="true"
                                            name="Name" title=" " Visible="true" required Width="100%" class="form-control" placeholder="ค้นหาข้อมูล" />
                                                                    </br>
                                                        <h5>Defective </h5>
                                                                    </br>
                                        <asp:GridView ID="GridViewDF" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                            CssClass="mydatagrid" PagerStyle-CssClass="pager"
                                            HeaderStyle-CssClass="header" RowStyle-CssClass="rows"
                                            OnPageIndexChanging="OnPageIndexChanging" PageSize="10" OnSelectedIndexChanged="GridViewDF_SelectedIndexChanged">
                                            <Columns>
                                                <asp:BoundField DataField="PRODUCT_NAME" HeaderText="PRODUCT NAME"></asp:BoundField>
                                                <asp:BoundField DataField="MURATA_TYPE" HeaderText="MODEL"></asp:BoundField>
                                                <asp:BoundField DataField="LINE" HeaderText="LINE"></asp:BoundField>
                                                <asp:BoundField DataField="PROCESS_NAME" HeaderText="PROCESS"></asp:BoundField>
                                                <asp:BoundField DataField="FINISH_PROCESS_DATE" HeaderText="DATA"></asp:BoundField>
                                                <asp:BoundField DataField="DEF_MODE" HeaderText="Defective"></asp:BoundField>
                                                <asp:BoundField DataField="DEF_QTY" HeaderText="DEF_QTY"></asp:BoundField>
                                                <asp:BoundField DataField="EMP_CODE" HeaderText="EMP_CODE"></asp:BoundField>
                                                <asp:ButtonField Text="Select" ControlStyle-CssClass="btn btn-primary" CommandName="Select" ItemStyle-Width="50" />
                                            </Columns>
                                        </asp:GridView>
                                                                    </br>         
                                                 <asp:Button ID="btnExport" runat="server" Text="Export To Excel" type="button" class="btn btn-primary" />
                                                                    </br> 
                                                        </br> 
                                                        </br> 
                                                <asp:Label ID="lblEmployeeDetails" runat="server"></asp:Label>
                                                                    </br> 
                                                        </br>                                                  
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <asp:Panel ID="pnELECTRICALUnit1" runat="server" Visible="false">
                                                                <h5>ELECTRICAL Unit1 </h5>
                                                                <asp:GridView ID="GridViewEC1" runat="server" AutoGenerateColumns="true" AllowPaging="true"
                                                                    CssClass="mydatagrid" PagerStyle-CssClass="pager"
                                                                    HeaderStyle-CssClass="header" RowStyle-CssClass="rows"
                                                                    OnPageIndexChanging="OnPageIndexChanging1" PageSize="10" OnSelectedIndexChanged="GridViewDF_SelectedIndexChanged">
                                                                </asp:GridView>
                                                                </br>  
                                                <asp:Button ID="btnExportEC1" runat="server" Text="Export To Excel" type="button" class="btn btn-primary" />
                                                                </br> 
                                                </br> 
                                                </br>           
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnELECTRICALUnit2" runat="server" Visible="false">
                                                                <h5>ELECTRICAL Unit2 </h5>
                                                                <asp:GridView ID="GridViewEC2" runat="server" AutoGenerateColumns="true" AllowPaging="true"
                                                                    CssClass="mydatagrid" PagerStyle-CssClass="pager"
                                                                    HeaderStyle-CssClass="header" RowStyle-CssClass="rows"
                                                                    OnPageIndexChanging="OnPageIndexChanging2" PageSize="10">
                                                                </asp:GridView>
                                                                </br>  
                                                <asp:Button ID="btnExportEC2" runat="server" Text="Export To Excel" type="button" class="btn btn-primary" />
                                                            </asp:Panel>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
