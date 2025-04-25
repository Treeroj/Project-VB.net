<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SMTMBCHIS.aspx.vb" Inherits="SMTMBCHIS" %>
<%@ Register Src="HeaderNavbar.ascx" TagName="HeaderNavbar" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
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
    <%-- <script>
         var currentURL = window.location.href;
         if (currentURL.includes("MBSetupModelAssembly_HISTORY.aspx")) {
             var newURL = currentURL.replace("MBSetupModelAssembly_HISTORY.aspx", "HISTORY");
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
      width: 200px;
      height: 200px;
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
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="card-text">รหัสประจำตัว / User code :</div>
                                        <asp:Panel ID="Panel1" runat="server">
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="tbUser" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true"
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
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                                        <div class="card-body">
                                            <div class="text-center">
                                                <h3 class="card-title">ดาวน์โหลด Excel :</h3>
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
                    <div class="col-lg-4">
                        <div class="card">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel3" runat="server" Visible="false">
                                        <div class="card-body">
                                            <div class="text-center">
                                                <h3 class="card-title">Select Information</h3>
                                            </div>
                                            <div class="card-text">LINE :</div>
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="TextBox8" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true"
                                                    Width="100%" class="form-control" placeholder="ค้นหา LINE" onblur="__doPostBack('','');" />
                                            </label>
                                            <br />
                                            <div class="card-text">USERHIS :</div>
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="TextBox9" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true"
                                                    Width="100%" class="form-control" placeholder="ค้นหา USERHIS" onblur="__doPostBack('','');" />

                                                <div class="card-text">MODEL :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="TextBox1" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true"
                                                        Width="100%" class="form-control" placeholder="ค้นหา MODEL" onblur="__doPostBack('','');" />
                                                </label>
                                                <br />
                                                <div class="card-text">LOTNO :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="TextBox2" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true"
                                                        Width="100%" class="form-control" placeholder="ค้นหา LOTNO" onblur="__doPostBack('','');" />
                                                </label>
                                                <br />
                                                <div class="card-text">REELNO :</div>
                                                <label class="custom-field three" style="width: 100%">
                                                    <asp:TextBox ID="TextBox10" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true"
                                                        Width="100%" class="form-control" placeholder="ค้นหา REELNO" onblur="__doPostBack('','');" />
                                                </label>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="card">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel4" runat="server" Visible="false">
                                        <div class="card-body">
                                            <div class="text-center">
                                                <h3 class="card-title">Select Information</h3>
                                            </div>
                                            <div class="card-text">MATERIAL :</div>
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="TextBox3" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true"
                                                    Width="100%" class="form-control" placeholder="ค้นหา MATERIAL" onblur="__doPostBack('','');" />
                                            </label>
                                            <br />
                                            <div class="card-text">INVOICE :</div>
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="TextBox4" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true"
                                                    Width="100%" class="form-control" placeholder="ค้นหา INVOICE" onblur="__doPostBack('','');" />
                                            </label>
                                            <br />
                                            <div class="card-text">DAY :</div>
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="TextBox5" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true"
                                                    Width="100%" class="form-control" placeholder="ค้นหา DAY" onblur="__doPostBack('','');" />
                                            </label>
                                            <br />
                                            <div class="card-text">MATERIALLOT :</div>
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="TextBox6" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true"
                                                    Width="100%" class="form-control" placeholder="ค้นหา MATERIALLOT" onblur="__doPostBack('','');" />
                                            </label>
                                            <br />
                                            <div class="card-text">CASSETTE :</div>
                                            <label class="custom-field three" style="width: 100%">
                                                <asp:TextBox ID="TextBox7" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true"
                                                    Width="100%" class="form-control" placeholder="ค้นหา CASSETTE" onblur="__doPostBack('','');" />
                                            </label>
                                            <br />
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
                            <asp:AsyncPostBackTrigger ControlID="tbUser" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="TextBox1" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="TextBox2" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="TextBox3" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="TextBox4" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="TextBox5" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="TextBox6" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="TextBox7" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="TextBox8" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="TextBox9" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="TextBox10" EventName="TextChanged" />
                            <asp:PostBackTrigger ControlID="btnExportEC1" />
                            <asp:AsyncPostBackTrigger ControlID="ddlLinePD" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="PanelMATERIALHIS" runat="server" Visible="false">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="table-responsive">
                                            <h5 class="card-title">MATERIAL HIS</h5>
                                            <asp:GridView ID="gvHis" runat="server" ShowHeader="true" AutoGenerateColumns="false"
                                                CssClass="table table-hover table-scrollable" Font-Size="10" Width="100%" PageSize="8" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging">
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
                                                    <asp:BoundField DataField="LINE" HeaderText="LINE" />
                                                    <asp:BoundField DataField="PRODUCTS" HeaderText="PRODUCTS" />
                                                    <asp:BoundField DataField="MODE1" HeaderText="MODE1" />
                                                    <asp:BoundField DataField="MACHINE" HeaderText="MACHINE" />
                                                    <asp:BoundField DataField="FEED" HeaderText="FEED" />
                                                    <asp:BoundField DataField="CASSETTE" HeaderText="CASSETTE" />
                                                    <asp:BoundField DataField="COUNTER" HeaderText="COUNTER" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <style>
                                            .table-scrollable {
                                                overflow-x: auto;
                                            }
                                        </style>
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
