<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Cass_MN.aspx.vb" Inherits="Cass_MN" %>

<%@ Register Src="HeaderNavbar.ascx" TagName="HeaderNavbar" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
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
    <link href="assets/css/select2.css" rel="stylesheet" type="text/css" />


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
                        <li class="breadcrumb-item">MT800</li>
                        <li class="breadcrumb-item">Cassette</li>
                        <li class="breadcrumb-item active">Cassette Maintenance And Repair</li>
                    </ol>
                </nav>
            </div>
            <section class="section">
                <div class="row">

                    <div class="col-lg-6">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>

                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="text-center">
                                                <h5 class="card-title">Cassette Maintenance.</h5>
                                            </div>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td width="30%">
                                                        <div class="card-text">รหัสประจำตัว / User code :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <asp:Panel ID="PanetbUser1" runat="server">
                                                            <label class="custom-field three" style="width: 100%">
                                                                <asp:TextBox ID="tbUser1" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                    Width="100%" class="form-control" placeholder="ใส่รหัสพนักงาน" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />
                                                            </label>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>

                                            <table style="width: 100%;">
                                                <asp:Panel ID="PNddlLinePD1" runat="server" Visible="false">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">ผลิตภัณฑ์ / Product :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />                                                        
                                                            <asp:DropDownList ID="ddlLinePD1" runat="server" AutoPostBack="true" Width="100%"
                                                                CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                            </asp:DropDownList>                                                        
                                                    </td>
                                                </tr>
                                               </asp:Panel>
                                            </table>

                                            <table style="width: 100%;">
                                                <asp:Panel ID="PaneCassNumber1" runat="server"  Visible="false">
                                                <tr>

                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">Cassette Number :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />                                                   
                                                            <asp:DropDownList ID="ddlCassette1" runat="server" AutoPostBack="true" Width="100%"
                                                                CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                            </asp:DropDownList>      
                                                    </td>
                                                </tr>
                                                    </asp:Panel>
                                            </table>

                                            <table style="width: 100%;">
                                                <asp:Panel ID="PanelBroken1" runat="server"  Visible="false">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">Broken :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />                                                      
                                                            <label class="custom-field three" style="width: 100%">
                                                                <asp:TextBox ID="tbBroken1" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
                                                                    Width="100%" class="form-control" placeholder="" onblur="__doPostBack('','');" ReadOnly="true" />
                                                            </label>                                                      
                                                    </td>
                                                </tr>
                                                    </asp:Panel>
                                            </table>

                                            <table style="width: 100%;">
                                                <asp:Panel ID="PanelProblem1" runat="server"  Visible="false">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">Problem :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />                                                        
                                                            <asp:TextBox ID="tbProblem1" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                Width="100%" class="form-control" placeholder="ใส่ปัญหา" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />                                                        
                                                    </td>
                                                </tr>
                                                    </asp:Panel>
                                            </table>

                                            <table style="width: 100%;">
                                                <asp:Panel ID="PanelCause1" runat="server"  Visible="false">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">Cause :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />                                                       
                                                            <asp:TextBox ID="tbcause1" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                Width="100%" class="form-control" placeholder="ใส่สาเหตุ" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />                                                        
                                                    </td>
                                                </tr>
                                                    </asp:Panel>
                                            </table>

                                            <table style="width: 100%;">
                                         <asp:Panel ID="PanelRecovery1" runat="server"  Visible="false">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">Recovery :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />
                                                            <asp:TextBox ID="tbRecovery1" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                Width="100%" class="form-control" placeholder="ใส่วิธีการแก้ไข" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />          
                                                    </td>
                                                </tr>
                                             </asp:Panel>
                                            </table>

                                            <table style="width: 100%;">
                                                <asp:Panel ID="PanelChange1" runat="server"  Visible="false">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">Change :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />
                                                            <asp:TextBox ID="tbChange1" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                Width="100%" class="form-control" placeholder="ใส่อุปกรณ์ที่เปลี่ยน (ถ้ามี)" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />                                                        
                                                    </td>
                                                </tr>
                                                    </asp:Panel>
                                            </table>

                                            <table style="width: 100%;">
                                                <asp:Panel ID="PanelQTY1" runat="server"  Visible="false">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">QTY :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />
                                                            <asp:TextBox ID="tbQTY1" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                Width="100%" class="form-control" placeholder="ใส่จำนวนอุปกรณ์ที่เปลี่ยน" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />                                                    
                                                    </td>
                                                </tr>
                                                     </asp:Panel>
                                            </table>

                                            <table style="width: 100%;">
                                                <asp:Panel ID="PanelPassword1" runat="server"  Visible="false">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">Password :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />
                                                            <asp:TextBox ID="tbPassword1" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                Width="100%" class="form-control" placeholder="ใส่รหัส" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />
                                                    </td>
                                                </tr>
                                                     </asp:Panel>
                                            </table>

                                            <asp:Panel ID="PNBUT1" runat="server" class="text-center"  Visible="false">
                                                <br />
                                                <button id="Button1" runat="server" type="button" class="btn btn-outline-dark">ยืนยันการลงข้อมูล</button>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>



                    <div class="col-lg-6">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <Triggers>
                            </Triggers>
                            <ContentTemplate>
                               <asp:Panel ID="Panel2" runat="server">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="text-center">
                                                <h5 class="card-title">Cassette Repair.</h5>
                                            </div>

                                            <table style="width: 100%;">
                                                <tr>
                                                    <td width="30%">
                                                        <div class="card-text">รหัสประจำตัว / User code :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <asp:Panel ID="PanetbUser2" runat="server">
                                                            <label class="custom-field three" style="width: 100%">
                                                                <asp:TextBox ID="tbUser2" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                    Width="100%" class="form-control" placeholder="ใส่รหัสพนักงาน" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />
                                                            </label>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>

                                            <table style="width: 100%;">
                                                <asp:Panel ID="PNddlLinePD2" runat="server">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">ผลิตภัณฑ์ / Product :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />
                                                        
                                                            <asp:DropDownList ID="ddlLinePD2" runat="server" AutoPostBack="true" Width="100%"
                                                                CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                            </asp:DropDownList>
                                                        
                                                    </td>
                                                </tr>
                                                    </asp:Panel>
                                            </table>

                                            <table style="width: 100%;">
                                                <asp:Panel ID="PaneCassNumber2" runat="server">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">Cassette Number :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />
                                                        
                                                            <asp:DropDownList ID="ddlCassette2" runat="server" AutoPostBack="true" Width="100%"
                                                                CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                            </asp:DropDownList>
                                                       
                                                    </td>
                                                </tr>
                                                     </asp:Panel>
                                            </table>

                                            <table style="width: 100%;">
                                                <asp:Panel ID="PanelBroken2"  runat="server">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">Broken :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />
                                                        
                                                        
                                                        <asp:DropDownList ID="ddlBroken2" runat="server" AutoPostBack="true" Width="100%"
                                                                CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                            </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                    </asp:Panel>
                                            </table>

                                            <table style="width: 100%;">
                                                <asp:Panel ID="PaneltbBroken2"  runat="server" Visible="false">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">Broken Other :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />
                                                        
                                                        
                                                        <label class="custom-field three" style="width: 100%">
                                                                <asp:TextBox ID="tbBroken2" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                    Width="100%" class="form-control" placeholder="ระบุอาการเสียอื่นๆ" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />
                                                            </label>
                                                    </td>
                                                </tr>
                                                    </asp:Panel>
                                            </table>
                                            

                                           <%-- <table style="width: 100%;">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">Problem :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />
                                                        <asp:Panel ID="PanelProblem2" runat="server">

                                                            <asp:TextBox ID="tbProblem2" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                Width="100%" class="form-control" placeholder="ใส่ปัญหา" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>--%>

                                            <table style="width: 100%;">
                                                <asp:Panel ID="PanelCause2" runat="server"  Visible="false">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">Cause :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />
                                                        
                                                            <asp:TextBox ID="tbcause2" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                Width="100%" class="form-control" placeholder="ระบุสาเหตุ" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />
                                                       
                                                    </td>
                                                </tr>
                                                     </asp:Panel>
                                            </table>

                                            <table style="width: 100%;">
                                                 <asp:Panel ID="PanelRecovery2" runat="server">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">Recovery :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />
                                                       
                                                          <%--  <asp:TextBox ID="tbRecovery2" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                Width="100%" class="form-control" placeholder="ใส่วิธีการแก้ไข" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />--%>
                                                        <asp:DropDownList ID="ddlRecovery2" runat="server" AutoPostBack="true" Width="100%"
                                                                CssClass="form-control js-example-placeholder-single" ToolTip="Select">
                                                            </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                     </asp:Panel>
                                            </table>

                                            <table style="width: 100%;">
                                                 <asp:Panel ID="PanelChange2" runat="server">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">Change :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />
                                                       
                                                            <asp:TextBox ID="tbChange2"  runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                Width="100%" class="form-control" placeholder="ใส่อุปกรณ์ที่เปลี่ยน (ถ้ามี)" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />
                                                        
                                                    </td>
                                                </tr>
                                                     </asp:Panel>
                                            </table>

                                            <table style="width: 100%;">
                                                 <asp:Panel ID="PanelQTY2" runat="server">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">QTY :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />
                                                       
                                                            <asp:TextBox ID="tbQTY2" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                Width="100%" class="form-control" placeholder="ใส่จำนวนอุปกรณ์ที่เปลี่ยน" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />
                                                        
                                                    </td>
                                                </tr>
                                                     </asp:Panel>
                                            </table>

                                            <table style="width: 100%;">
                                                <asp:Panel ID="PanelPassword2" runat="server">
                                                <tr>
                                                    <td width="30%">
                                                        <br />
                                                        <div class="card-text">Password :</div>
                                                    </td>
                                                    <td width="70%" class="navbar-left">
                                                        <br />
                                                        
                                                            <asp:TextBox ID="tbPassword2" runat="server" autocomplete="off" ForeColor="black" Visible="true" AutoPostBack="true"
                                                                Width="100%" class="form-control" placeholder="ใส่รหัส" onblur="if(this.value.trim() !== '') {__doPostBack('','');}" />
                                                        
                                                    </td>
                                                </tr>
                                                    </asp:Panel>
                                            </table>

                                            <asp:Panel ID="PNBUT2" runat="server" class="text-center">
                                                <br />
                                                <button id="Button2" runat="server" type="button" class="btn btn-outline-dark">ยืนยันการลงข้อมูล</button>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
        <script src="assets/js/select2.js" type="text/javascript"></script>

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
    </form>
</body>
</html>
