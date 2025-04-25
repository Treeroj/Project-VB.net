<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SMTMBChangeCass.aspx.vb" Inherits="SMTMBChangeCass" %>
<%@ Register Src="HeaderNavbar.ascx" TagName="HeaderNavbar" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" charset="UTF-8"/>

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


    <title></title>
</head>
    <body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <uc:HeaderNavbar ID="header" runat="server" />

        <main id="main" class="main">
            <div class="pagetitle">
                <h1>Mode : Change Cassette SMT</h1>
                <nav>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="http://172.16.72.179/MT800PST-ALLPRODUCT/">MT800</a></li>
                        <li class="breadcrumb-item">MaterialBarcodeSystem</li>
                        <li class="breadcrumb-item active">Mode : Change Cassette SMT</li>
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
                                    </Triggers>
                                    <ContentTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td width="40%">
                                                    <div class="card-text">รหัสประจำตัว / User code :</div>
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <label class="custom-field three" style="width: 100%">
                                                            <asp:TextBox ID="tbUser" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                Width="100%" class="form-control" placeholder="ใส่รหัสพนักงาน" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />
                                
                                                        </label>
                                                    </asp:Panel>
                                                </td>
                                                <td width="60%" class="navbar-left">
                                                    <asp:Panel ID="PNddlLinePD" runat="server" Visible="false">
                                                        <div class="card-text">ผลิตภัณฑ์ / Product :</div>
                                                        <label class="custom-field three" style="width: 100%">
                                                            <asp:DropDownList ID="ddlLinePD" runat="server" AutoPostBack="true"
                                                                CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                            </asp:DropDownList>
                                                        </label>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                 <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                <%--<asp:AsyncPostBackTrigger ControlID="ddlLine" EventName="SelectedIndexChanged" />--%>
                                 <asp:AsyncPostBackTrigger ControlID="btnFrontMD" EventName="ServerClick" />
                                <asp:AsyncPostBackTrigger ControlID="btnBackMD" EventName="ServerClick" />
                                <asp:AsyncPostBackTrigger ControlID="btnNoneMD" EventName="ServerClick" />
                                <%--<asp:AsyncPostBackTrigger ControlID="ddlLineMC" EventName="SelectedIndexChanged" />--%>
                                <asp:AsyncPostBackTrigger ControlID="tbCassette" EventName="TextChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="pnLine" runat="server" Visible="false">
                                    <div class="card">
                                        <div class="card-body">
                                            </br>
                                             <table style="width: 100%;">
                                                <tr>
                                                    <td width="30%">
                                                        <div class="card-text">รหัสพนักงาน / Code :</div>
                                                        <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                            <li><a id="lbshowCode" class="navbar-item" runat="server"></a></li>
                                                        </ul>
                                                    </td>
                                                    <td width="30%" class="navbar-left">
                                                        <ul class="nav navbar-nav navbar-right navbar-expand-lg navbar-expand-xs navbar-expand-md navbar-expand">
                                                            <div class="card-text">ชื่อ / Name :</div>
                                                            <li><a id="lbshowFRONTENDLINE" class="navbar-item" runat="server"></a>
                                                    </td>
                                                     <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                    <td width="40%" class="navbar-left">
                                                        

                                                    </td>
                                                         </asp:Panel>
                                                </tr>
                                            </table>
                                            </br>                                           
                                            
                                            <asp:Panel ID="PaneCassette" runat="server" Visible="false">
                                                <div class="card-text">คาสเซ็ทเดิม / Previous Cassette :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="tbCassette" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true" 
                                                         Width="100%" class="form-control" placeholder="ใส่คาสเซ็ท" onblur="__doPostBack('','');" />
                                                </label>
                                            </asp:Panel>

                                            </br>
                                            <asp:Panel ID="PanetbModel" runat="server" Visible="false">
                                                <div class="card-text">งานรุ่น / Model :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="tbModel" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true" 
                                                         Width="100%" class="form-control" placeholder="ใส่ งานรุ่น / Model" onblur="__doPostBack('','');" ReadOnly="true"/>
                                                </label>
                                            </asp:Panel>
                                            </br>
                                            <asp:Panel ID="PNbutton" runat="server"  class="text-center" Visible="false">
                                            <button id="btnFrontMD" runat="server"  type="button" class="btn btn-outline-dark"  >Front</button>
                                            <button id="btnBackMD" runat="server"  type="button" class="btn btn-outline-dark"  >Back</button> 
                                            <button id="btnNoneMD" runat="server"  type="button" class="btn btn-outline-dark"  >None</button>
                                            </asp:Panel>  
                                             
                                            <asp:Panel ID="PanetbLot" runat="server" Visible="false">
                                                <div class="card-text">ล็อตงาน / Lot No :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="tbLot" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true" 
                                                         Width="100%" class="form-control" placeholder="ใส่รุ่น" onblur="__doPostBack('','');" ReadOnly="true"/>
                                                </label>
                                            </asp:Panel>
                                            </br>
                                            <asp:Panel ID="PNddlMC" runat="server" Visible="false">
                                                <div class="card-text">เครื่องจักร / Machine :</div>
                                                <label class="custom-field three" style="width: 100%">
                                               <%-- <asp:DropDownList ID="ddlLineMC" runat="server" AutoPostBack="true"
                                                    CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                </asp:DropDownList>--%>
                                                    <asp:TextBox ID="tbmc" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true" 
                                                         Width="100%" class="form-control" placeholder="ใส่รุ่น" onblur="__doPostBack('','');" ReadOnly="true" />
                                            </label>
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
                                <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                <%-- <asp:AsyncPostBackTrigger ControlID="ddlLine" EventName="SelectedIndexChanged" />--%>
                                <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="tbCassette" EventName="TextChanged" />
                                <%-- <asp:AsyncPostBackTrigger ControlID="ddlLineMC" EventName="SelectedIndexChanged" />--%>
                                <asp:AsyncPostBackTrigger ControlID="DDLCAUSECSS" EventName="SelectedIndexChanged" />
                                <%--<asp:AsyncPostBackTrigger ControlID="DDLFEED" EventName="SelectedIndexChanged" />--%>
                                <asp:AsyncPostBackTrigger ControlID="tbNEWCassette" EventName="TextChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="PanelMATERIAL" runat="server" Visible="false">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="text-center">
                                                <h3 class="card-title">Genaral Information</h3>
                                            </div>
                                            <asp:Panel ID="PaneDDLFEED" runat="server" Visible="false">
                                                <div class="card-text">หมายเลขฟิต / Feed :</div>
                                                <%--<asp:DropDownList ID="DDLFEED" runat="server" AutoPostBack="true"
                                                                CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                    </asp:DropDownList>--%>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="tbfeed" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                        Width="100%" class="form-control" placeholder="ใส่คาสเซ็ท" onblur="__doPostBack('','');" ReadOnly="true" />
                                                </label>
                                            </asp:Panel>
                                            </br>
                                             <asp:Panel ID="Paneshowmat" runat="server" Visible="false">
                                                 <div class="card-text">วัตถุดิบ / Material :</div>
                                                 <label class="custom-field three" style="width: 100%">
                                                     <asp:TextBox ID="tbshowmat" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                         Width="100%" class="form-control" placeholder="ใส่คาสเซ็ท" onblur="__doPostBack('','');" ReadOnly="true" />
                                                 </label>
                                             </asp:Panel>

                                            </br>
                                            <div class="card-text">ไลน์ / Line :</div>
                                            <label class="custom-field three" style="width: 100%">
                                                <%--<asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="true"
                                                                CssClass="form-control js-example-placeholder-single" ToolTip="Select" >
                                                            </asp:DropDownList>--%>
                                                <asp:TextBox ID="tbline" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                    Width="100%" class="form-control" placeholder="ใส่คาสเซ็ท" onblur="__doPostBack('','');" ReadOnly="true" />
                                            </label>
                                            </br>
                                            </br>
                                         <asp:Panel ID="PaneDDLCAUSECSS" runat="server" Visible="false">
                                             <div class="card-text">อาการ / Item :</div>
                                             <label class="custom-field three" style="width: 100%">
                                                 <asp:DropDownList ID="DDLCAUSECSS" runat="server" AutoPostBack="true"
                                                     CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                 </asp:DropDownList>
                                             </label>
                                         </asp:Panel>
                                            </br>
                                     <asp:Panel ID="PaneNEWCassette" runat="server" Visible="false">
                                         <div class="card-text">คาสเซ็ทใหม่ / New Cassette :</div>
                                         <label class="custom-field three" style="width: 100%">
                                             <asp:TextBox ID="tbNEWCassette" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                 Width="100%" class="form-control" placeholder="ใส่คาสเซ็ท" onblur="__doPostBack('','');" />
                                         </label>
                                     </asp:Panel>
                                            </br>
                                            <asp:Panel ID="PNBUT" runat="server" class="text-center" Visible="false">
                                                <button id="Button1" runat="server" type="button" class="btn btn-outline-dark">ยืนยันการลงข้อมูล</button>
                                            </asp:Panel>
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
                            <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="PanelMATERIALHIS" runat="server" Visible="false">
                                <div class="card">
                                    <div class="card-body">
                                        <h5 class="card-title">Change Cassette History</h5>
                                        <asp:GridView ID="gvcassettehis" runat="server" ShowHeader="true" AutoGenerateColumns="false"
                                            CssClass="table table-hover">
                                            <Columns>
                                                <asp:BoundField DataField="USERID" HeaderText="USERID" />
                                                <asp:BoundField DataField="USERNAME" HeaderText="USERNAME" />
                                                <asp:BoundField DataField="DAY" HeaderText="DAY" />
                                                <asp:BoundField DataField="TIME" HeaderText="TIME" />
                                                <asp:BoundField DataField="PRODUCTS" HeaderText="PRODUCTS" />
                                                <asp:BoundField DataField="LINE" HeaderText="LINE" />
                                                <asp:BoundField DataField="MODEL" HeaderText="MODEL" />
                                                <asp:BoundField DataField="LOTNO" HeaderText="LOTNO" />
                                                <asp:BoundField DataField="MACHINE" HeaderText="MACHINE" />
                                                <asp:BoundField DataField="CAUSE" HeaderText="CAUSE" />
                                                <asp:BoundField DataField="FEED" HeaderText="FEED" />
                                                <asp:BoundField DataField="MATERIAL" HeaderText="MATERIAL" />
                                                <asp:BoundField DataField="CASSBEFORE" HeaderText="BEFORE" />
                                                <asp:BoundField DataField="CASSAFTER" HeaderText="AFTER" />
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