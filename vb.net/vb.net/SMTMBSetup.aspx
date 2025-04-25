<%@ Page Language="VB" Debug="true" AutoEventWireup="false" CodeFile="SMTMBSetup.aspx.vb" Inherits="SMTMBSetup" %>
<%@ Register Src="HeaderNavbar.ascx" TagName="HeaderNavbar" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--<meta charset="UTF-8" />--%>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" ,charset="UTF-8"/>
    <%--<a href="AdminPDF.aspx">AdminPDF.aspx</a>--%>
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
    <%--<script>
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



       


        <main id="main" class="main">
             
            <div class="pagetitle">
                <h1>Mode : Setup Model SMT</h1>
                <nav>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="http://172.16.72.179/MT800PST-ALLPRODUCT/">MT800</a></li>
                        <li class="breadcrumb-item">MaterialBarcodeSystem</li>
                        <li class="breadcrumb-item active">Mode : Setup Model SMT</li>
                    </ol>
                </nav>
            </div>
            <section class="section">
                <div class="row">
                    <div class="col-lg-4">
                        <div class="card">
                            <div class="card-body">
                                <div class="text-center">
                                    <h3 class="card-title">Genaral Information</h3>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="ServerClick" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="card-text">รหัสประจำตัว / User code :</div>
                                        <asp:Panel ID="Panel1" runat="server">
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="tbUser" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true" 
                                                     Width="100%" class="form-control" placeholder="ใส่รหัสพนักงาน" onblur="__doPostBack('','');" />
                                               <%-- <script type="text/javascript">
                                                    $(document).ready(function () {
                                                        window.history.replaceState('', '', window.location.href)
                                                    });
                                                </script>--%>
                                            </label>
                                            </asp:Panel>
                                            <asp:Panel ID="PNddlLinePD" runat="server" Visible="false">
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
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlLineMC" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tbModel" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tbLot" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="btnFrontMD" EventName="ServerClick" />
                                <asp:AsyncPostBackTrigger ControlID="btnBackMD" EventName="ServerClick" />
                                <asp:AsyncPostBackTrigger ControlID="btnNoneMD" EventName="ServerClick" />
                                <asp:AsyncPostBackTrigger ControlID="ddlLine" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="ServerClick" />
                                <asp:AsyncPostBackTrigger ControlID="tbCassetteSh" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tnMaterial" EventName="TextChanged" />
                               <%-- <asp:AsyncPostBackTrigger ControlID="tbCassetteSh" EventName="Load" />--%>
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="pnLine" runat="server" Visible ="false">
                                    <div class="card">
                                        <div class="card-body" <%--style="height: 350px;"--%>>
                                            </br>
                                            <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li>รหัสพนักงาน / Code :<a id="lbshowCode" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                            
                                            <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li>ชื่อ / Name :<a id="lbshowFRONTENDLINE" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                             <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li>เวลา / Time :<a id="lbshowTime" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                             <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li>วันที่ / Date :<a id="lbshowDATE" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                             
                                            <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li>ผลิตภัณฑ์ / Product :<a id="lbshowFRONTENDPD" class="navbar-item" runat="server"></a></li>
                                            </ul>

                                            <asp:Panel ID="PNlbshowLine" runat="server" Visible ="false" >
                                            <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li>ไลน์ / Line :<a id="lbshowLine" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                             </asp:Panel>

                                             <asp:Panel ID="PNModelShow" runat="server" Visible ="false" >
                                            <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li>รุ่นงาน / Model :<a id="lbshowModel" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                             </asp:Panel>
                                             <asp:Panel ID="PNlbshowLot" runat="server" Visible ="false">
                                            <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li>หมายเลขล็อต / Lot No :<a id="lbshowLot" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                             </asp:Panel>
                                            <asp:Panel ID="PNlbshowMC" runat="server" Visible ="false">
                                            <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li>เครื่องจักร / Machine :<a id="lbshowMC" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                            </asp:Panel>
                                            <asp:Panel ID="PNlbshowFeed" runat="server" Visible ="false">
                                            <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li>หมายเลขฟีด / Feed :<a id="lbshowFeeddd" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                            </asp:Panel>
                                            <asp:Panel ID="PNlbshowMATERIAL" runat="server" Visible ="false">
                                            <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li>วัตถุดิบ / Material :<a id="lbshowMATERIAL" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-lg-4">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" EnableViewState ="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tbModel" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="btnFrontMD" EventName="ServerClick" />
                                <asp:AsyncPostBackTrigger ControlID="btnBackMD" EventName="ServerClick" />
                                <asp:AsyncPostBackTrigger ControlID="btnNoneMD" EventName="ServerClick" />
                                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="ServerClick" />
                                <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlLineMC" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tbCassetteSh" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tnMaterial" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlLine" EventName="SelectedIndexChanged" />
                               <%-- <asp:AsyncPostBackTrigger ControlID="tbCassetteSh" EventName="Load" />--%>
                               <%-- <asp:AsyncPostBackTrigger ControlID="gvFiles3" EventName="DataBound" />--%>
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="PanelMATERIAL" runat="server" Visible="false">
                                    <div class="card">
                                        <div class="card-body" >
                                            <div class="text-center">
                                    <h3 class="card-title">Genaral Information</h3>
                                </div>
                                            <div class="card-text">ไลน์ / Line :</div>
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="true"
                                                    CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                </asp:DropDownList>
                                            </label>
                                            <asp:Panel ID="PanetbModel" runat="server" Visible="false">
                                                <div class="card-text">รุ่นงาน / Model :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="tbModel" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true" 
                                                         Width="100%" class="form-control" placeholder="ใส่ งานรุ่น / Model" onblur="__doPostBack('','');" />
                                                </label>
                                            </asp:Panel>
                                            </br>
                                            <asp:Panel ID="PNbutton" runat="server" Visible="false"  class="text-center">
                                                <%--<asp:Button ID="btnFrontMD" runat="server" Text="Front" type="button" class="btn btn-primary" />
                                                <asp:Button ID="btnBackMD" runat="server" Text="Back" type="button" class="btn btn-primary" />
                                                <asp:Button ID="btnNoneMD" runat="server" Text="None" type="button" class="btn btn-primary" />--%>
                                            <button id="btnFrontMD" runat="server"  type="button" class="btn btn-outline-dark"  >Front</button>
                                            <button id="btnBackMD" runat="server"  type="button" class="btn btn-outline-dark"  >Back</button> 
                                            <button id="btnNoneMD" runat="server"  type="button" class="btn btn-outline-dark"  >None</button>
                                            </asp:Panel>  
                                            <asp:Panel ID="PanetbLot" runat="server" Visible="false" >
                                                <div class="card-text">ล็อตงาน / Lot No :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="tbLot" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true" 
                                                         Width="100%" class="form-control" placeholder="ใส่ ล็อตงาน / Lot No" onblur="__doPostBack('','');" />
                                                </label>
                                            </asp:Panel>  
                                             <asp:Panel ID="PNddlMC" runat="server" Visible="false">
                                                <div class="card-text">เครื่องจักร / Machine :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                <asp:DropDownList ID="ddlLineMC" runat="server" AutoPostBack="true"
                                                    CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                </asp:DropDownList>
                                            </label>
                                            </asp:Panel>
                                              </br>
                                             <asp:Panel ID="PNFeed" runat="server" Visible="false">
                                                <div class="card-text">หมายเลขฟีด / Feed :</div>
                                                  <asp:GridView ID="gvFiles3" runat="server" AutoGenerateColumns="false" ShowHeader="false" 
                                            CssClass="table table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="FEEDNO" HeaderText="Feed" />
                                                </Columns>
                                            </asp:GridView>
                                                  <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li><a id="lbshowFeed" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                              <%-- <label class="custom-field three" style="width: 100%">
                                                <asp:DropDownList ID="ddlFeed" runat="server" AutoPostBack="true"
                                                    CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                </asp:DropDownList>
                                            </label>--%>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel4" runat="server" visible="false" >
                                            <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li>วัตถุดิบ / Material :<a id="A2" class="navbar-item" runat="server" ></a></li>
                                            </ul>
                                             </asp:Panel>
                                             <asp:Panel ID="Panel3" runat="server" visible="false">
                                            <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                <li>จำนวน / Quantity :<a id="A1" class="navbar-item" runat="server"></a></li>
                                            </ul>
                                             </asp:Panel>
                                              </br>
                                                <%--<asp:Panel ID="Panel5" runat="server">
                                                <div class="card-text">หมายเลขฟิต / Feed :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="TextBox3" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true" 
                                                        required Width="100%" class="form-control" placeholder="ใส่หมายเลขฟิต" onblur="__doPostBack('','');" />
                                                </label>
                                            </asp:Panel>
                                            </br>--%>
                                             <%--<asp:Panel ID="PNCassette" runat="server" Visible="false">
                                                <div class="card-text">คาสเซ็ท / Cassette :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="tbCassette" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true" 
                                                         Width="100%" class="form-control" placeholder="ใส่คาสเซ็ท" onblur="__doPostBack('','');" />
                                                </label>
                                            </asp:Panel>--%>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-lg-4">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tnMaterial" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tbCassetteSh" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlLineMC" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="ServerClick" />
                                <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlLine" EventName="SelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="pnMaterialmain" runat="server" Visible="false" >
                                    <div class="card">
                                        <div class="card-body"> 
                                            <div class="text-center">
                                    <h5 class="card-title">Material</h5>
                                </div>
                                            <asp:Panel ID="pnMaterial" runat="server">
                                                <div class="card-text">กรุณายิงบาร์โค้ด  :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                   <asp:TextBox ID="tnMaterial" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                        Width="100%" class="form-control bg-warning" placeholder="กรุณายิงบาร์โค้ด Material" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />
                                                </label>
                                                </br>
                                             <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                 <li>วัตถุดิบ / Material :<a id="lbshowMat" class="navbar-item" runat="server"></a></li>
                                             </ul>
                                                <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                    <li>จำนวน / Quantity :<a id="lbshowQty" class="navbar-item" runat="server"></a></li>
                                                </ul>

                                                <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                    <li>อินวอยซ์ / Invoice :<a id="lbshowinv" class="navbar-item" runat="server"></a></li>
                                                </ul>

                                                <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                    <li>ล็อตวัตถุ / Material Lot :<a id="lbshowMatLot" class="navbar-item" runat="server"></a></li>
                                                </ul>
                                                </br>
                                            </asp:Panel>
                                            <asp:Panel ID="pnVDO" runat="server" Visible="false">
                                                                        <video controls="controls" autoplay width="0%">
                                                                            <%--<source src="PSTANN.mp4" type="video/mp4" />--%>
                                                                            <source src="buttonSTM.mp3" />
                                                                            <%--Treeroj Add sound PST 07 June 22--%>
                                                                        </video>
                                             </asp:Panel>
                                             <asp:Panel ID="PNCassetteSh" runat="server" Visible="false" >
                                                <div class="card-text">คาสเซ็ท / Cassette :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="tbCassetteSh" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true" 
                                                         Width="100%" class="form-control bg-warning" placeholder="ใส่คาสเซ็ท" onblur="if(this.value.trim() !== '') {__doPostBack('','');}"/>
                                                </label>                
                                              </br>
                                              </br>
                                                 </asp:Panel>
                                            <asp:Panel ID="PNBUT" runat="server" class="text-center" visible="false">
                                            <button id="Button1" runat="server"  type="button" class="btn btn-outline-dark"  >ยืนยันการลงข้อมูล</button>
                                            </asp:Panel>  
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                   <%--  <div class="col-lg-3">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" EnableViewState ="true">
                            <Triggers>        
                                <asp:AsyncPostBackTrigger ControlID="ddlLineMC" EventName="SelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="Panel3" runat="server">
                                    <div class="card">
                                        <div class="card-body" >
                                            <h5 class="card-title ">Machine</h5>
                                            
                                             <asp:Panel ID="Panel8" runat="server" >
                                                <div class="card-text">หมายเลขฟิด / Feed :</div>

                                                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="false" 
                                            CssClass="table table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="MATERIAL" HeaderText="Feed" />
                                                </Columns>
                                                      <Columns>
                                                    <asp:BoundField DataField="FEEDNO" HeaderText="Feed" />
                                                </Columns>
                                                         <Columns>
                                                    <asp:BoundField DataField="BINNO" HeaderText="Feed" />
                                                </Columns>
                                            </asp:GridView>
                                                  </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>         --%>       


                </div>
                <div class="col-lg-12">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="ServerClick" />
                            <asp:AsyncPostBackTrigger ControlID="tnMaterial" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlLine" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="PanelMATERIALHIS" runat="server" Visible="false" >
                                <div class="card">
                                    <div class="card-body">
                                        <h5 class="card-title">MATERIAL HIS</h5>
                                         <asp:GridView ID="gvnaja" runat="server" ShowHeader="true" AutoGenerateColumns="false"
                                            CssClass="table table-hover">
                                                   <Columns>
                                                        <asp:BoundField DataField="Model" HeaderText="Model" />
                                                        <asp:BoundField DataField="LotNo" HeaderText="LotNO" />
                                                        <asp:BoundField DataField="Machine" HeaderText="Machine" />
                                                        <asp:BoundField DataField="Feed" HeaderText="Feed" />
                                                        <asp:BoundField DataField="Material" HeaderText="Material" />
                                                        <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                        <asp:BoundField DataField="Invoice" HeaderText="Invoice" />
                                                        <asp:BoundField DataField="MaterialLot" HeaderText="MaterialLot" />
                                                        <asp:BoundField DataField="Cassette" HeaderText="Cassette" />
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
