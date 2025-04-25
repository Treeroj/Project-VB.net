<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdminPDF.aspx.vb" Inherits="AdminPDF" %>

<!DOCTYPE html>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.6.347/pdf.min.js"></script>
<link href="https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.6.347/pdf_viewer.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(function () {
        $("[id*=gvFiles] .view").click(function () {
            var fileId = $(this).attr("rel");
            $.ajax({
                type: "POST",
                url: "Default.aspx/GetPDF",
                data: "{fileId: " + fileId + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    LoadPdfFromBlob(r.d.Data);
                }
            });
        });
    });
 
    var pdfjsLib = window['pdfjs-dist/build/pdf'];
    pdfjsLib.GlobalWorkerOptions.workerSrc = 'https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.6.347/pdf.worker.min.js';
    var pdfDoc = null;
    var scale = 1; 
    var resolution = 1; 
    function LoadPdfFromBlob(blob) {
        pdfjsLib.getDocument({ data: blob }).promise.then(function (pdfDoc_) {
            pdfDoc = pdfDoc_;
            var pdf_container = document.getElementById("pdf_container");
            pdf_container.innerHTML = "";
            pdf_container.style.display = "block";
            for (var i = 1; i <= pdfDoc.numPages; i++) {
                RenderPage(pdf_container, i);
            }
        });
    };
    function RenderPage(pdf_container, num) {
        pdfDoc.getPage(num).then(function (page) {
            var canvas = document.createElement('canvas');
            canvas.id = 'pdf-' + num;
            ctx = canvas.getContext('2d');
            pdf_container.appendChild(canvas);
            var spacer = document.createElement("div");
            spacer.style.height = "20px";
            pdf_container.appendChild(spacer);
            var viewport = page.getViewport({ scale: scale });
            canvas.height = resolution * viewport.height;
            canvas.width = resolution * viewport.width;
            var renderContext = {
                canvasContext: ctx,
                viewport: viewport,
                transform: [resolution, 0, 0, resolution, 0, 0]
            };

            page.render(renderContext);
        });
    };
</script>
    <style type="text/css">
    body { font-family: Arial; font-size: 10pt; }
    table { border: 1px solid #ccc; border-collapse: collapse; }
    table th { background-color: #F7F7F7; color: #333; font-weight: bold; }
    table th, table td { padding: 5px; border: 1px solid #ccc; }
    #pdf_container { background: #ccc; text-align: center; display: none; padding: 5px; height: 820px; overflow: auto; }
</style>
</head>
<body>
    <form id="form1" runat="server">
            <asp:FileUpload ID="FileUpload1" runat="server" />
<asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="Upload" />
<hr />
<asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField DataField="Name" HeaderText="File Name" />
        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>               
                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                    CommandArgument='<%# Eval("NAME") %>'></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<hr />
<div id="pdf_container">
</div>
    </form>
</body>
</html>--%>

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
                        allowClear: true
                    });

                });
            });
        }
    </script>
    <script>
    function showDisplay(){
            alert('success');
        }
    </script>
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.6.347/pdf.min.js"></script>
<link href="https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.6.347/pdf_viewer.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(function () {
        $("[id*=gvFiles] .view").click(function () {
            var fileId = $(this).attr("rel");
            $.ajax({
                type: "POST",
                url: "Default.aspx/GetPDF",
                data: "{fileId: " + fileId + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    LoadPdfFromBlob(r.d.Data);
                }
            });
        });
    });
    var pdfjsLib = window['pdfjs-dist/build/pdf'];
    pdfjsLib.GlobalWorkerOptions.workerSrc = 'https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.6.347/pdf.worker.min.js';
    var pdfDoc = null;
    var scale = 1; 
    var resolution = 1; 
    function LoadPdfFromBlob(blob) {
        pdfjsLib.getDocument({ data: blob }).promise.then(function (pdfDoc_) {
            pdfDoc = pdfDoc_;
            var pdf_container = document.getElementById("pdf_container");
            pdf_container.innerHTML = "";
            pdf_container.style.display = "block";
            for (var i = 1; i <= pdfDoc.numPages; i++) {
                RenderPage(pdf_container, i);
            }
        });
    };
    function RenderPage(pdf_container, num) {
        pdfDoc.getPage(num).then(function (page) {
            var canvas = document.createElement('canvas');
            canvas.id = 'pdf-' + num;
            ctx = canvas.getContext('2d');
            pdf_container.appendChild(canvas);
            var spacer = document.createElement("div");
            spacer.style.height = "20px";
            pdf_container.appendChild(spacer);
            var viewport = page.getViewport({ scale: scale });
            canvas.height = resolution * viewport.height;
            canvas.width = resolution * viewport.width;
            var renderContext = {
                canvasContext: ctx,
                viewport: viewport,
                transform: [resolution, 0, 0, resolution, 0, 0]
            };
            page.render(renderContext);
        });
    };
</script>
    <style type="text/css">
    body { font-family: Arial; font-size: 10pt; }
    table { border: 1px solid #ccc; border-collapse: collapse; }
    table th { background-color: #F7F7F7; color: #333; font-weight: bold; }
    table th, table td { padding: 5px; border: 1px solid #ccc; }
    #pdf_container { background: #ccc; text-align: center; display: none; padding: 5px; height: 820px; overflow: auto; }
</style>
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <!-- ======= Header ======= -->
        <header id="header" class="header fixed-top d-flex align-items-center">

            <div class="d-flex align-items-center justify-content-between">
                <a href="index.html" class="logo d-flex align-items-center">
                    <%--<img src="assets/img/logo.png" alt="">--%>
                    <span class="d-none d-lg-block">MT800 NVP.</span>
                </a>
                <i class="bi bi-list toggle-sidebar-btn"></i>
            </div>
            <!-- End Logo -->

            <div class="search-bar">
                <%-- <form class="search-form d-flex align-items-center" method="POST" action="#">
        <input type="text" name="query" placeholder="Search" title="Enter search keyword">
        <button type="submit" title="Search"><i class="bi bi-search"></i></button>
      </form>--%>
            </div>
            <!-- End Search Bar -->
            <nav class="header-nav ms-auto">
                <ul class="d-flex align-items-center">
                    <li class="nav-item d-block d-lg-none">
                        <a class="nav-link nav-icon search-bar-toggle " href="#">
                            <i class="bi bi-search"></i>
                        </a>
                    </li>
                    <!-- End Search Icon-->
                    <li class="nav-item dropdown pe-3">

                        <a class="nav-link nav-profile d-flex align-items-center pe-0" href="#">
                            <img src="image/116496843.jfif" alt="Profile" class="rounded-circle">
                            <span class="d-none d-md-block dropdown-toggle ps-2">By Sudarat Yamprasert</span>
                        </a>
                        <!-- End Profile Iamge Icon -->
<%--                        <ul class="dropdown-menu dropdown-menu-end dropdown-menu-arrow profile">
                            <li class="dropdown-header">
                                <h6>Kevin Anderson</h6>
                                <span>Web Designer</span>
                            </li>
                            <li>
                                <hr class="dropdown-divider">
                            </li>
                            <li>
                                <a class="dropdown-item d-flex align-items-center" href="users-profile.html">
                                    <i class="bi bi-person"></i>
                                    <span>My Profile</span>
                                </a>
                            </li>
                            <li>
                                <hr class="dropdown-divider">
                            </li>
                            <li>
                                <a class="dropdown-item d-flex align-items-center" href="users-profile.html">
                                    <i class="bi bi-gear"></i>
                                    <span>Account Settings</span>
                                </a>
                            </li>
                            <li>
                                <hr class="dropdown-divider">
                            </li>
                            <li>
                                <a class="dropdown-item d-flex align-items-center" href="pages-faq.html">
                                    <i class="bi bi-question-circle"></i>
                                    <span>Need Help?</span>
                                </a>
                            </li>
                            <li>
                                <hr class="dropdown-divider">
                            </li>

                            <li>
                                <a class="dropdown-item d-flex align-items-center" href="#">
                                    <i class="bi bi-box-arrow-right"></i>
                                    <span>Sign Out</span>
                                </a>
                            </li>
                        </ul>--%>
                        <!-- End Profile Dropdown Items -->
                    </li>
                    <!-- End Profile Nav -->

                </ul>
            </nav>
            <!-- End Icons Navigation -->
        </header>
        <!-- End Header -->

        <!-- ======= Sidebar ======= -->
        <aside id="sidebar" class="sidebar">
            <ul class="sidebar-nav" id="sidebar-nav">
                <li class="nav-item">
                    <a class="nav-link collapsed" href="index.html">
                        <i class="bi bi-grid"></i>
                        <span>Dashboard</span>
                    </a>
                </li>
                <!-- End Dashboard Nav -->

<%--                <li class="nav-item">
                    <a class="nav-link collapsed" data-bs-target="#MaterialBarcodeSystem-nav" data-bs-toggle="collapse" href="#">
                        <i class="bi bi-menu-button-wide"></i><span>MaterialBarcodeSystem (SMT)</span><i class="bi bi-chevron-down ms-auto"></i>
                    </a>
                    <ul id="MaterialBarcodeSystem-nav" class="nav-content collapse" data-bs-parent="#sidebar-nav">
                        <li>
                            <a href="MaterialBarcodeSystemSetupModelSMT.aspx">
                                <i class="bi bi-circle"></i><span>Setup Model</span>
                            </a>
                        </li>
                        <li>
                            <a href="MBChangeSMT.aspx">
                                <i class="bi bi-circle"></i><span>Change Material</span>
                            </a>
                        </li>
                        <li>
                            <a href="MaterialBarcodeSystem-badges.html">
                                <i class="bi bi-circle"></i><span>Change Lot</span>
                            </a>
                        </li>
                        <li>
                            <a href="MaterialBarcodeSystem-breadcrumbs.html">
                                <i class="bi bi-circle"></i><span>Change Cassette</span>
                            </a>
                        </li>--%>
                        <%--<li>
            <a href="MaterialBarcodeSystem-buttons.html">
              <i class="bi bi-circle"></i><span>Buttons</span>
            </a>
          </li>
          <li>
            <a href="MaterialBarcodeSystem-cards.html">
              <i class="bi bi-circle"></i><span>Cards</span>
            </a>
          </li>
          <li>
            <a href="MaterialBarcodeSystem-carousel.html">
              <i class="bi bi-circle"></i><span>Carousel</span>
            </a>
          </li>
          <li>
            <a href="MaterialBarcodeSystem-list-group.html">
              <i class="bi bi-circle"></i><span>List group</span>
            </a>
          </li>
          <li>
            <a href="MaterialBarcodeSystem-modal.html">
              <i class="bi bi-circle"></i><span>Modal</span>
            </a>
          </li>
          <li>
            <a href="MaterialBarcodeSystem-tabs.html">
              <i class="bi bi-circle"></i><span>Tabs</span>
            </a>
          </li>
          <li>
            <a href="MaterialBarcodeSystem-pagination.html">
              <i class="bi bi-circle"></i><span>Pagination</span>
            </a>
          </li>
          <li>
            <a href="MaterialBarcodeSystem-progress.html">
              <i class="bi bi-circle"></i><span>Progress</span>
            </a>
          </li>
          <li>
            <a href="MaterialBarcodeSystem-spinners.html">
              <i class="bi bi-circle"></i><span>Spinners</span>
            </a>
          </li>
          <li>
            <a href="MaterialBarcodeSystem-tooltips.html">
              <i class="bi bi-circle"></i><span>Tooltips</span>
            </a>
          </li>--%>
    <%--                </ul>
                </li>--%>
                <!-- End Components Nav -->




                <li class="nav-item">
                    <a class="nav-link" data-bs-target="#Admin-nav" data-bs-toggle="collapse" href="#">
                        <i class="bi bi-journal-text"></i><span>Admin</span><i class="bi bi-chevron-down ms-auto"></i>
                    </a>
                    <ul id="Admin-nav" class="nav-content collapse show" data-bs-parent="#sidebar-nav">
                        <li>
                            <a href="AdminPDF.aspx " class="active">
                                <i class="bi bi-circle"></i><span>Admin</span>
                            </a>
                        </li>
<%--                        <li>
                            <a href="MBSetupModelAssembly.aspx">
                                <i class="bi bi-circle"></i><span>Open History</span>
                            </a>
                        </li>--%>
                        <%--          <li>
            <a href="MaterialBarcodeSystemAssembly.aspx">
              <i class="bi bi-circle"></i><span>Form Editors</span>
            </a>
          </li>
          <li>
            <a href="MaterialBarcodeSystemAssembly.html.aspx">
              <i class="bi bi-circle"></i><span>Form Validation</span>
            </a>
          </li>--%>
                    </ul>
                </li>
                <!-- End Forms Nav -->

              <%--  <li class="nav-item">
                    <a class="nav-link collapsed" data-bs-target="#tables-nav" data-bs-toggle="collapse" href="#">
                        <i class="bi bi-layout-text-window-reverse"></i><span>Tables</span><i class="bi bi-chevron-down ms-auto"></i>
                    </a>
                    <ul id="tables-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
                        <li>
                            <a href="tables-general.html">
                                <i class="bi bi-circle"></i><span>General Tables</span>
                            </a>
                        </li>
                        <li>
                            <a href="tables-data.html">
                                <i class="bi bi-circle"></i><span>Data Tables</span>
                            </a>
                        </li>
                    </ul>
                </li>--%>
                <!-- End Tables Nav -->

              <%--  <li class="nav-item">
                    <a class="nav-link collapsed" data-bs-target="#charts-nav" data-bs-toggle="collapse" href="#">
                        <i class="bi bi-bar-chart"></i><span>Charts</span><i class="bi bi-chevron-down ms-auto"></i>
                    </a>
                    <ul id="charts-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
                        <li>
                            <a href="charts-chartjs.html">
                                <i class="bi bi-circle"></i><span>Chart.js</span>
                            </a>
                        </li>
                        <li>
                            <a href="charts-apexcharts.html">
                                <i class="bi bi-circle"></i><span>ApexCharts</span>
                            </a>
                        </li>
                        <li>
                            <a href="charts-echarts.html">
                                <i class="bi bi-circle"></i><span>ECharts</span>
                            </a>
                        </li>
                    </ul>
                </li>--%>
                <!-- End Charts Nav -->

               <%-- <li class="nav-item">
                    <a class="nav-link collapsed" data-bs-target="#icons-nav" data-bs-toggle="collapse" href="#">
                        <i class="bi bi-gem"></i><span>Icons</span><i class="bi bi-chevron-down ms-auto"></i>
                    </a>
                    <ul id="icons-nav" class="nav-content collapse " data-bs-parent="#sidebar-nav">
                        <li>
                            <a href="icons-bootstrap.html">
                                <i class="bi bi-circle"></i><span>Bootstrap Icons</span>
                            </a>
                        </li>
                        <li>
                            <a href="icons-remix.html">
                                <i class="bi bi-circle"></i><span>Remix Icons</span>
                            </a>
                        </li>
                        <li>
                            <a href="icons-boxicons.html">
                                <i class="bi bi-circle"></i><span>Boxicons</span>
                            </a>
                        </li>
                    </ul>
                </li>--%>
                <!-- End Icons Nav -->

               <%-- <li class="nav-heading">Pages</li>

                <li class="nav-item">
                    <a class="nav-link collapsed" href="users-profile.html">
                        <i class="bi bi-person"></i>
                        <span>Profile</span>
                    </a>
                </li>
                <!-- End Profile Page Nav -->

                <li class="nav-item">
                    <a class="nav-link collapsed" href="pages-faq.html">
                        <i class="bi bi-question-circle"></i>
                        <span>F.A.Q</span>
                    </a>
                </li>
                <!-- End F.A.Q Page Nav -->

                <li class="nav-item">
                    <a class="nav-link collapsed" href="pages-contact.html">
                        <i class="bi bi-envelope"></i>
                        <span>Contact</span>
                    </a>
                </li>
                <!-- End Contact Page Nav -->

                <li class="nav-item">
                    <a class="nav-link collapsed" href="pages-register.html">
                        <i class="bi bi-card-list"></i>
                        <span>Register</span>
                    </a>
                </li>
                <!-- End Register Page Nav -->

                <li class="nav-item">
                    <a class="nav-link collapsed" href="pages-login.html">
                        <i class="bi bi-box-arrow-in-right"></i>
                        <span>Login</span>
                    </a>
                </li>
                <!-- End Login Page Nav -->

                <li class="nav-item">
                    <a class="nav-link collapsed" href="pages-error-404.html">
                        <i class="bi bi-dash-circle"></i>
                        <span>Error 404</span>
                    </a>
                </li>
                <!-- End Error 404 Page Nav -->
                <li class="nav-item">
                    <a class="nav-link collapsed" href="pages-blank.html">
                        <i class="bi bi-file-earmark"></i>
                        <span>Blank</span>
                    </a>
                </li>--%>
                <!-- End Blank Page Nav -->
            </ul>
        </aside>
        <!-- End Sidebar-->




        <main id="main" class="main">
           <%-- <div class="pagetitle">
                <h1>Material Record for Assembly</h1>
                <nav>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="http://172.16.72.179/MT800PST-ALLPRODUCT/">MT800</a></li>
                        <li class="breadcrumb-item">MaterialBarcodeSystem</li>
                        <li class="breadcrumb-item active">Material Record for Assembly</li>
                    </ol>
                </nav>
            </div>--%>
            <section class="section">

                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h5 class="card-title">ระบบ @CORE</h5>
                                      <asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="true"
                                                    CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                </asp:DropDownList>
                                            

                                      <br />

                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                            <asp:Button ID="btnUpload" runat="server" Text="อัพโหลด" OnClick="Upload" />

                                    <label class="custom-field three" style="width: 100%">
                                       </label>

                                              
                                </div>
                            </div>
                        </div>
                </div>

                <div class="row">
                 <div class="col-lg-6">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h5 class="card-title">วิธีการคีย์ลางานในระบบ @CORE</h5>
                                </div>
                                            
                                  

                                            <asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="false"  ShowHeader="false"
                                            CssClass="table table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="" />
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="loadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'  >
                                                                <img src="image/eye.png" width="20" height ="20">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="DownloadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'>
                                                                <img src="image/download.png" width="15" height ="10">
                                                            </asp:LinkButton>
                                                             
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <hr />
                                         <%--   <div id="pdf_container">
                                            </div>   --%>
                            </div>
                        </div>
                </div>
                                 <div class="col-lg-6">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h5 class="card-title">วิธีการขอรับรองเวลา</h5>
                                </div>

                                            <asp:GridView ID="gvFiles2" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                            CssClass="table table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="" />
                                                     <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="loadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'  >
                                                                <img src="image/eye.png" width="20" height ="20">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="DownloadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'>
                                                                <img src="image/download.png" width="15" height ="10">
                                                            </asp:LinkButton>

                                                             
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <hr />
        <%--                                    <div id="pdf_container2">
                                            </div> --%>  
                            </div>
                        </div>
                </div>
</div>
                <div class="row">
                                 <div class="col-lg-6">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h5 class="card-title">วิธีการขอทำล่วงเวลา</h5>
                                </div>

                                            <asp:GridView ID="gvFiles3" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                            CssClass="table table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="ข้อมูล" />
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="loadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'  >
                                                                <img src="image/eye.png" width="20" height ="20">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="DownloadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'>
                                                                <img src="image/download.png" width="15" height ="10">
                                                            </asp:LinkButton>

                                                             
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <hr />
                                          <%--  <div id="pdf_container">
                                            </div>   --%>
                            </div>
                        </div>
                </div>


                                                 <div class="col-lg-6">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h5 class="card-title">วิธีการ RESET PASSWORD</h5>
                                </div>

                                            <asp:GridView ID="gvFiles4" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                            CssClass="table table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="ข้อมูล" />
                                                     <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="loadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'  >
                                                                <img src="image/eye.png" width="20" height ="20">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="DownloadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'>
                                                                <img src="image/download.png" width="15" height ="10">
                                                            </asp:LinkButton>

                                                             
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <hr />
                                          <%--  <div id="pdf_container">
                                            </div>   --%>
                            </div>
                        </div>
                </div>





</div>


                 <div class="row">
                                 <div class="col-lg-6">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h5 class="card-title">วิธีการเปลี่ยน PASSWORD @CORE (PASSWORD หมดอายุ)</h5>
                                </div>

                                            <asp:GridView ID="gvFiles5" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                            CssClass="table table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="ข้อมูล" />
                                                     <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="loadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'  >
                                                                <img src="image/eye.png" width="20" height ="20">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="DownloadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'>
                                                                <img src="image/download.png" width="15" height ="10">
                                                            </asp:LinkButton>

                                                             
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <hr />
                                          <%--  <div id="pdf_container">
                                            </div>   --%>
                            </div>
                        </div>
                </div>


                                                 <div class="col-lg-6">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h5 class="card-title">วิธีการขอเปลี่ยนแปลงที่อยู่</h5>
                                </div>

                                            <asp:GridView ID="gvFiles6" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                            CssClass="table table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="ข้อมูล" />
                                                     <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="loadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'  >
                                                                <img src="image/eye.png" width="20" height ="20">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="DownloadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'>
                                                                <img src="image/download.png" width="15" height ="10">
                                                            </asp:LinkButton>

                                                             
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <hr />
                                          <%--  <div id="pdf_container">
                                            </div>   --%>
                            </div>
                        </div>
                </div>





</div>

                <div class="row">
                                 <div class="col-lg-6">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h5 class="card-title">วิธีการขอเปลี่ยนแปลงข้อมูลส่วนตัว</h5>
                                </div>

                                            <asp:GridView ID="gvFiles7" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                            CssClass="table table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="ข้อมูล" />
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="loadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'  >
                                                                <img src="image/eye.png" width="20" height ="20">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="DownloadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'>
                                                                <img src="image/download.png" width="15" height ="10">
                                                            </asp:LinkButton>

                                                             
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <hr />
                                          <%--  <div id="pdf_container">
                                            </div>   --%>
                            </div>
                        </div>
                </div>


                                                 <div class="col-lg-6">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h5 class="card-title">วิธีการคีย์เบิกค่ารักษาพยาบาล</h5>
                                </div>

                                            <asp:GridView ID="gvFiles8" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                            CssClass="table table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="ข้อมูล" />
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="loadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'  >
                                                                <img src="image/eye.png" width="20" height ="20">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="DownloadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'>
                                                                <img src="image/download.png" width="15" height ="10">
                                                            </asp:LinkButton>

                                                             
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <hr />
                                          <%--  <div id="pdf_container">
                                            </div>   --%>
                            </div>
                        </div>
                </div>





</div>

                <div class="row">
                                 <div class="col-lg-6">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h5 class="card-title">วิธีการคีย์เบิก MONEY GIFT</h5>
                                </div>

                                            <asp:GridView ID="gvFiles9" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                            CssClass="table table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="ข้อมูล" />
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="loadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'  >
                                                                <img src="image/eye.png" width="20" height ="20">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="DownloadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'>
                                                                <img src="image/download.png" width="15" height ="10">
                                                            </asp:LinkButton>

                                                             
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <hr />
                                          <%--  <div id="pdf_container">
                                            </div>   --%>
                            </div>
                        </div>
                </div>

                                                     <div class="col-lg-6">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h5 class="card-title">QR CODE</h5>
                                </div>

                                            <asp:GridView ID="gvFiles10" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                            CssClass="table table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="Name" HeaderText="ข้อมูล" />
                                                     <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="loadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'  >
                                                                <img src="image/eye.png" width="20" height ="20">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="DownloadFile"
                                                                CommandArgument='<%# Eval("NAME") %>'>
                                                                <img src="image/download.png" width="15" height ="10">
                                                            </asp:LinkButton>

                                                             
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <hr />
                                          <%--  <div id="pdf_container">
                                            </div>   --%>
                            </div>
                        </div>
                </div>




</div>
            </section>
        </main>

        <a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>

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

    </form>
</body>
</html>
