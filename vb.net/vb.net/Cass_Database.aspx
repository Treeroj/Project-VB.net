<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Cass_Database.aspx.vb" Inherits="Cass_Database" %>
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
                        <li class="breadcrumb-item active">Cassette and Mask Database</li>
                    </ol>
                </nav>
            </div>
            <section class="section">
                <div class="row">

                    <div class="col-lg-7">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="text-center">
                                                <h5 class="card-title">Cassette Database</h5>
                                            </div>
                                            <asp:Panel ID="PanelCassetteDB" runat="server">
                                                <div class="table-responsive">
                                                    <div style="max-height: 1000px; overflow-y: auto;">
                                                        <asp:GridView ID="gvCassette" runat="server" ShowHeader="true" AutoGenerateColumns="false" OnRowDataBound="gvCassette_RowDataBound"
                                                            CssClass="table table-hover table-scrollable">
                                                            <Columns>
                                                                <asp:BoundField DataField="NO" HeaderText="หมายเลข" />
                                                                <asp:BoundField DataField="USE" HeaderText="จำนวนการใช้งาน" />
                                                                <asp:BoundField DataField="STATUS" HeaderText="สถานะ" />
                                                                <asp:BoundField DataField="CAUSE" HeaderText="สาเหตุ" />
                                                                <asp:BoundField DataField="STATUSMN" HeaderText="สถานะการซ่อม" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>



                    <div class="col-lg-5">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="pnCassettemain" runat="server">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="text-center">
                                                <h5 class="card-title">Mask Database</h5>
                                            </div>

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
    </form>
</body>
</html>
