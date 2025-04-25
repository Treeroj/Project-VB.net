
<%@ Page Language="VB" Debug="true" AutoEventWireup="false" CodeFile="MBSetupModelAssemblyTEST.aspx.vb" Inherits="MBSetupModelAssemblyTEST" %>

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
      width: 200px; 
      height: 200px; 
    }
  </style>

   <%-- <script>
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
    </script>--%>


    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
         <uc:HeaderNavbar ID="header" runat="server" />
       <%-- <div id="loading-container">
    <img src="image/fluid-loader.gif" alt="กำลังโหลดข้อมูล"/>
  </div>--%>
        <div id="loading-container">
    <img id="loading-gif" src="image/fluid-loader.gif" alt="กำลังโหลดข้อมูล" />
  </div>
        <main id="main" class="main">
            <div class="pagetitle">
                <h1>Material Record for Assembly</h1>
                <nav>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="http://172.16.72.179/MT800PST-ALLPRODUCT/">MT800</a></li>
                        <li class="breadcrumb-item">MaterialBarcodeSystem</li>
                        <li class="breadcrumb-item active">Material Record for Assembly</li>
                    </ol>
                </nav>
            </div>
            <section class="section">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h3 class="card-title">Genaral Information</h3>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                       <%-- <asp:PostBackTrigger ControlID="Button2" />--%>
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="card-text">รหัสประจำตัว / User code :</div>                                        
                                        <asp:Panel ID="Panel1" runat="server">
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="tbUser" runat="server" autocomplete="off" ForeColor="black" Visible="true" OnTextChanged="tbUser_TextChanged"
                                                    Width="100%" class="form-control" placeholder="ใส่รหัสพนักงาน" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />                                                                                                
                                            </label>
                                        </asp:Panel>
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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlLine" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tbModel" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tbLot" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tnMaterial" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                <asp:PostBackTrigger ControlID="Button1" />
                                <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                                <%--<asp:PostBackTrigger ControlID="Button2" />--%>
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="pnLine" runat="server" Visible="false">
                                    <div class="card">
                                        <div class="card-body">
                                            </br>
                                            <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li>รหัสพนักงาน / Code :<a id="lbshowCode" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                            </br>
                                            <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li>ชื่อ / Name :<a id="lbshowFRONTENDLINE" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                            </br>
                                            <div class="card-text">ไลน์ / Line :</div>
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="true"
                                                    CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                </asp:DropDownList>
                                            </label>
                                            <asp:Panel ID="PanetbModel" runat="server" Visible="false">
                                                <div class="card-text">งานรุ่น / Model :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="tbModel" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true" OnTextChanged="tbModel_TextChanged"
                                                         Width="100%" class="form-control" placeholder="ใส่ งานรุ่น / Model"  onblur="if(this.value.trim() !== '') {__doPostBack('','');}"/>
                                                </label>
                                            </asp:Panel>
                                            <asp:Panel ID="PanetbLot" runat="server" Visible="false">
                                                <div class="card-text">ล็อตงาน / Lot No :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <ASP:TextBox ID="tbLot" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                        Width="100%" class="form-control" placeholder="ใส่ ล็อตงาน / Lot No" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" OnKeyPress="handleKeyPress();" />
                                                </label>
                                            </asp:Panel>
                                            <ASP:Panel ID="PanetbMaterialBAR" runat="server" Visible="false">
                                                <div class="card-text text-primary">Material :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <ASP:TextBox ID = "tnMaterial" runat="server" autocomplete="off" ForeColor="black" Visible="true" OnTextChanged="tnMaterial_TextChanged"
                                                        Width="100%" class="form-control bg-warning" placeholder="กรุณายิงบาร์โค้ด Material"  onblur="if(this.value.trim() !== '') {__doPostBack('','');}" OnKeyPress="handleKeyPresstnMaterial();"/>
                                                    <style>
                                                        #tnMaterialplaceholder{
                                                            color: white;
                                                        }
                                                    </style>
                                                </label>
                                                </br>
                                                </br>
                                                <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li> วัตถุดิบ / Material :<a id = "lbshowMat" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                                </br>                                              
                                                <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li> จำนวน / Quantity :<a id = "lbshowQuantity" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                                </br>
                                                <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li> อินวอยซ์ / Invoice :<a id = "lbshowInvoice" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                                </br>
                                                <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li> ล็อควัตถุ / Material Lot :<a id = "lbshowMaterialLot" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                                 </asp:Panel>                                             
                                              </br>
                                              </br>
                                            <ASP:Panel ID = "PNBUT" runat="server" Class="text-center" visible="false">
                                            <%--<button id="Button1" runat="server"  type="button" class="btn btn-outline-dark"  >ยืนยันการลงข้อมูล</button>--%>
                                                <asp:Button ID="Button1" runat="server" Text="ยืนยันการลงข้อมูล" type="button" class="btn btn-primary" />
                                            </asp:Panel>  
                                        </div>
                                    </div>
                                </asp:Panel>                                            
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-lg-6">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlLine" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                 <asp:AsyncPostBackTrigger ControlID="tbModel" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tbLot" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                 <asp:PostBackTrigger ControlID="Button1" />
                                <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                                <%--<asp:PostBackTrigger ControlID="Button2" />--%>
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="PanelMATERIAL" runat="server" Visible="false" >
                                    <div class="card">
                                        <div class="card-body">
                                            </br>
                                            <asp:Panel ID="Panel2" runat="server" class="text-right " >                                               
                                                <asp:Button ID="Button2" runat="server" Text="ล้างข้อมูล" type="button" class="btn btn-primary float-end" />
                                            </asp:Panel>
                                            </br>
                                            <h5 class="card-title">MATERIAL</h5>
                                             
                                            <asp:GridView ID="GridViewModel" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridViewModel_RowDataBound"
                                                CssClass="table table-striped">
                                                <Columns>
                                                    <asp:BoundField DataField="MODEL" HeaderText="MODEL"></asp:BoundField>
                                                    <asp:BoundField DataField="MATERIAL" HeaderText="MATERIAL"></asp:BoundField>
                                                    <asp:BoundField DataField="SCM" HeaderText="SCM"></asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-lg-12">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlLine" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="tbModel" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="tbLot" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="Button1" />
                            <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                           <%-- <asp:PostBackTrigger ControlID="Button2" />--%>
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="PanelMATERIALHIS" runat="server" Visible="false">
                                <div class="card">
                                    <div class="card-body">
                                        <h5 class="card-title">MATERIAL HIS</h5>
                                        
                                        <asp:GridView ID="GridViewLot" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                            CssClass="table table-striped"
                                            OnPageIndexChanging="OnPageIndexChanging" PageSize="100">
                                            <Columns>
                                                <asp:BoundField DataField="MODEL" HeaderText="MODEL"></asp:BoundField>
                                                <asp:BoundField DataField="LOTNO" HeaderText="LOTNO"></asp:BoundField>
                                                <asp:BoundField DataField="MATERIAL" HeaderText="MATERIAL"></asp:BoundField>
                                                <asp:BoundField DataField="QTY" HeaderText="QTY"></asp:BoundField>
                                                <asp:BoundField DataField="INVOICE" HeaderText="INVOICE"></asp:BoundField>
                                                <asp:BoundField DataField="MATERIALLOT" HeaderText="MATERIALLOT"></asp:BoundField>
                                                <asp:BoundField DataField="REELNO" HeaderText="REELNO"></asp:BoundField>
                                                <%--<asp:ButtonField Text="Select" ControlStyle-CssClass="btn btn-primary" CommandName="Select" ItemStyle-Width="50" />--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </section>
        </main>

        <script type="text/javascript">
            var lastKeyPressTime = new Date();
            function handleKeyPress() {
                var currentTime = new Date();
                if (currentTime - lastKeyPressTime > 100) {
                    document.getElementById('<%= tbLot.ClientID %>').value = "";
                }
                lastKeyPressTime = currentTime;
            }
        </script>

        <script type="text/javascript">
            var lastKeyPressTimetnMaterial = new Date();
            function handleKeyPresstnMaterial() {
                var currentTimetnMaterial = new Date();
                if (currentTimetnMaterial - lastKeyPressTimetnMaterial > 200) {
                    document.getElementById('<%= tnMaterial.ClientID %>').value = "";
                }
                lastKeyPressTimetnMaterial = currentTimetnMaterial;
            }
        </script>

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
    
     <script type="text/javascript">
         var loadingContainer = document.getElementById('loading-container');
         var loadingGif = document.getElementById('loading-gif');

         // สมมติว่าคุณมีการโหลดข้อมูลในนี้ (ตัวอย่างโค้ดเสมือนเรียก API หรือโหลดข้อมูลจากเซิร์ฟเวอร์)
         setTimeout(function () {
             // เมื่อข้อมูลโหลดเสร็จ
             loadingContainer.style.display = 'none';
             loadingGif.style.display = 'none';
         }, 1000); // ตัวอย่าง: รอ 3 วินาทีแล้วซ่อน GIF
     </script>
    

   <%-- <script type="text/javascript">
        window.addEventListener('load', function () {
            // เมื่อหน้าเว็บโหลดเสร็จ
            var loadingContainer = document.getElementById('loading-container');
            loadingContainer.style.display = 'none';
        });
    </script>--%>
</body>
</html>
