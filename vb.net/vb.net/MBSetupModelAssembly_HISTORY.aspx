<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MBSetupModelAssembly_HISTORY.aspx.vb" Inherits="MBSetupModelAssembly_HISTORY" %>

<%@ Register Src="HeaderNavbar.ascx" TagName="HeaderNavbar" TagPrefix="uc" %>

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
    <link href="assets/css/select2.css" rel="stylesheet" type="text/css" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <%-- <script>
         // ตรวจสอบ URL ปัจจุบัน
         var currentURL = window.location.href;
         // เช็คว่า URL ปัจจุบันมี "MBSetupModelAssembly_HISTORY.aspx" หรือไม่
         if (currentURL.includes("MBSetupModelAssembly_HISTORY.aspx")) {
             // เปลี่ยน URL เป็น "HISTORY"
             var newURL = currentURL.replace("MBSetupModelAssembly_HISTORY.aspx", "HISTORY");
             // แทนที่ URL ปัจจุบันด้วย URL ใหม่
             history.replaceState({}, document.title, newURL);
         }
     </script>--%>
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
        <%-- <div id="loading-container">
    <img src="image/fluid-loader.gif" alt="กำลังโหลดข้อมูล"/>
                 <img id="image/fluid-loader.gif" src="image/fluid-loader.gif" alt="กำลังโหลดข้อมูล"/>
  </div>--%>
        <div id="loading-container">
            <img id="loading-gif" src="image/fluid-loader.gif" alt="กำลังโหลดข้อมูล" />
        </div>
        <main id="main" class="main">
            <div class="pagetitle">
                <h1>Material Record for Assembly History</h1>
                <nav>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="http://172.16.72.179/MT800PST-ALLPRODUCT/">MT800</a></li>
                        <li class="breadcrumb-item">MaterialBarcodeSystem</li>
                        <li class="breadcrumb-item active">Assembly History</li>
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
                                        <div class="card-text">รหัสประจำตัว / User code :</div>
                                        <asp:Panel ID="Panel1" runat="server">
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="tbUser" runat="server" autocomplete="off" ForeColor="black" Visible="true" OnTextChanged="tbUser_TextChanged"
                                                    Width="100%" class="form-control" placeholder="ใส่รหัสประจำตัว" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />
                                            </label>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="card">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel3" runat="server" Visible="false">
                                        <div class="card-body">
                                            <div class="card-body">
                                                <div class="text-center">
                                                    <h3 class="card-title">ดาวน์โหลด Excel :</h3>
                                                </div>
                                                <div class="d-grid gap-2 col-6 mx-auto">
                                                    <asp:Button ID="btnExportEC1" runat="server" Text="Export To Excel" type="button" class="btn btn-primary" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="card">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel7" runat="server" Visible="false">
                                        <div class="card-body">
                                            <div class="text-center">
                                                <h3 class="card-title">Search</h3>
                                            </div>
                                            <div class="card-text">
                                                ค้นหาMODEL / Search for MODEL :
                                            <asp:DropDownList ID="ddlMODEL" runat="server" AutoPostBack="true" Width="100%"
                                                CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                            </asp:DropDownList>
                                            </div>
                                            <div class="card-text">
                                                ค้นหาLOTNO / Search for LOTNO :
                                            <asp:DropDownList ID="ddlLOT" runat="server" AutoPostBack="true" Width="100%"
                                                CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                            </asp:DropDownList>
                                            </div>
                                            <div class="card-text">
                                                ค้นหาMATERIAL / Search for MATERIAL :
                                             <asp:DropDownList ID="ddlMAT" runat="server" AutoPostBack="true" Width="100%"
                                                 CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                             </asp:DropDownList>
                                            </div>
                                            <div class="card-text">
                                                ค้นหาINVOICE / Search for INVOICE :
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="TextBox4" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                    Width="100%" class="form-control" placeholder="ใส่ INVOICE สำหรับค้นหา" onblur="__doPostBack('','');" />
                                            </label>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="card">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                                        <div class="card-body">
                                            <div class="text-center">
                                                <h3 class="card-title">Search</h3>
                                            </div>
                                            <div class="card-text">
                                                ค้นหาMATERIALLOT / Search for MATERIALLOT :
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="TextBox5" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                    Width="100%" class="form-control" placeholder="ใส่ MATERIALLOT สำหรับค้นหา" onblur="__doPostBack('','');" />
                                            </label>
                                            </div>
                                            <div class="card-text">
                                                ค้นหาREELNO / Search for REELNO :
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="TextBox6" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                    Width="100%" class="form-control" placeholder="ใส่ REELNO สำหรับค้นหา" onblur="__doPostBack('','');" />
                                            </label>
                                            </div>
                                            <div class="card-text">
                                                ค้นหาLINE / Search for LINE :
                                            <asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="true" Width="100%"
                                                CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                            </asp:DropDownList>
                                            </div>
                                            <div class="card-text">
                                                ค้นหาProduct / Search for Product :
                                             <asp:DropDownList ID="ddlLinePD" runat="server" AutoPostBack="true" Width="100%"
                                                 CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                             </asp:DropDownList>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlLine" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlMODEL" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlLOT" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlMAT" EventName="SelectedIndexChanged" />
                                    <asp:PostBackTrigger ControlID="btnExportEC1" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel4" runat="server" Visible="false">
                                        <div class="card-body">
                                            <div class="text-center">
                                                <h3 class="card-title">Assembly History</h3>
                                            </div>
                                            <table style="width: 70%;">
                                                <tr>
                                                    <td width="35%">
                                                        <div class="row mb-3">
                                                            <label for="inputDate" class="col-sm-2 col-form-label">Start Date</label>
                                                            <div class="col-sm-10">
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control date-input" />
                                                                    <label class="input-group-btn" for="txtDate">
                                                                        <span class="btn btn-default">
                                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                                        </span>
                                                                    </label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td width="35%" class="navbar-left">
                                                        <div class="row mb-3">
                                                            <label for="inputDate" class="col-sm-2 col-form-label">End Date</label>
                                                            <div class="col-sm-10">
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtDate1" runat="server" CssClass="form-control date-input" />
                                                                    <label class="input-group-btn" for="txtDate1">
                                                                        <span class="btn btn-default">
                                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                                        </span>
                                                                    </label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Panel ID="PanelMATERIALHIS" runat="server" Visible="false">
                                                <div class="table-responsive">
                                                    <%--<h5 class="card-title">Assembly History</h5>--%>
                                                    <asp:GridView ID="gvHIS" runat="server" ShowHeader="true" AutoGenerateColumns="false"
                                                        CssClass="table table-hover table-scrollable" OnRowDataBound="gvHIS_RowDataBound"
                                                        OnPageIndexChanging="OnPageIndexChanging" PageSize="100" AllowPaging="true">
                                                        <Columns>
                                                            <asp:BoundField DataField="USERNAME" HeaderText="USERNAME" />
                                                            <asp:BoundField DataField="DAY" HeaderText="DAY" />
                                                            <asp:BoundField DataField="TIME" HeaderText="TIME" />
                                                            <asp:BoundField DataField="MODEL" HeaderText="MODEL" />
                                                            <asp:BoundField DataField="LOTNO" HeaderText="LOTNO" />
                                                            <asp:BoundField DataField="MATERIAL" HeaderText="MATERIAL" />
                                                            <asp:BoundField DataField="INVOICE" HeaderText="INVOICE" />
                                                            <asp:BoundField DataField="MATERIALLOT" HeaderText="MATERIALLOT" />
                                                            <asp:BoundField DataField="QTY" HeaderText="QTY" />
                                                            <asp:BoundField DataField="REELNO" HeaderText="REELNO" />
                                                            <asp:BoundField DataField="PRODUCTS" HeaderText="PRODUCTS" />
                                                            <asp:BoundField DataField="LINE" HeaderText="LINE" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                </div>
            </section>
        </main>
        <a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>
        <script src="assets/js/jquery.min.js" type="text/javascript"></script>
        <script src="assets/js/popper.min.js" type="text/javascript"></script>
        <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>
        <script src="assets/js/apexcharts.min.js" type="text/javascript"></script>
        <script src="assets/js/bootstrap.bundle.min.js" type="text/javascript"></script>
        <script src="assets/js/chart.min.js" type="text/javascript"></script>
        <script src="assets/js/echarts.min.js" type="text/javascript"></script>
        <script src="assets/js/quill.min.js" type="text/javascript"></script>
        <script src="assets/js/simple-datatables.js" type="text/javascript"></script>
        <script src="assets/js/tinymce.min.js" type="text/javascript"></script>
        <script src="assets/js/validate.js" type="text/javascript"></script>
        <script src="assets/js/main.js" type="text/javascript"></script>
        <script src="assets/js/select2.js" type="text/javascript"></script>
    </form>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= txtDate.ClientID %>').datepicker({
                format: 'dd-M-yyyy',
            }).on('changeDate', function (e) {
                var formattedDate = $('#<%= txtDate.ClientID %>').datepicker('getFormattedDate');
                $('#<%= txtDate.ClientID %>').val(formattedDate);
                __doPostBack('', '');
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= txtDate1.ClientID %>').datepicker({
                format: 'dd-M-yyyy',
            }).on('changeDate', function (e) {
                var formattedDate = $('#<%= txtDate1.ClientID %>').datepicker('getFormattedDate');
               $('#<%= txtDate1.ClientID %>').val(formattedDate);
               __doPostBack('', '');
           });
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $('#txtDate').datepicker({
                format: "dd-M-yyyy"
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('#txtDate1').datepicker({
                format: "dd-M-yyyy"
            });
        });
    </script>



    <script type="text/javascript">
        $('.input-group-btn').click(function () {
            $(this).find('.btn').click();
            $('#txtDate').trigger('change');
        });
    </script>
    <%--  <script type="text/javascript">
       document.onreadystatechange = function () {
           if (document.readyState === "complete") {
               // เมื่อหน้าเว็บโหลดเสร็จ
               var loadingContainer = document.getElementById('loading-container');
               loadingContainer.style.display = 'none';
           }
       };
   </script>--%>
    <script type="text/javascript">
        var loadingContainer = document.getElementById('loading-container');
        var loadingGif = document.getElementById('loading-gif');
        // สมมติว่าคุณมีการโหลดข้อมูลในนี้ (ตัวอย่างโค้ดเสมือนเรียก API หรือโหลดข้อมูลจากเซิร์ฟเวอร์)
        setTimeout(function () {
            // เมื่อข้อมูลโหลดเสร็จ
            loadingContainer.style.display = 'none';
            loadingGif.style.display = 'none';
        }, 3000); // ตัวอย่าง: รอ 3 วินาทีแล้วซ่อน GIF
    </script>

    <script type="text/javascript">
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
</body>
</html>

