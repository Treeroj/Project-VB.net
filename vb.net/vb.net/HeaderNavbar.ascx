55<%@ Control Language="VB" AutoEventWireup="false" CodeFile="HeaderNavbar.ascx.vb" Inherits="HeaderNavbar" %>
<header id="header" class="header fixed-top d-flex align-items-center">
    <div class="d-flex align-items-center justify-content-between">
        <a class="logo d-flex align-items-center">
            <img src="image/logo_head.png" alt="Profile" class="rounded-circle"/>
            <span class="d-none d-lg-block">MT800 NVP</span>
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
                <%--<a clss="nav-link nav-profile d-flex align-items-center pe-0" >               
                <img src="image/without slogan_R.png" alt="Profile" class="rounded-circle" width="20%" />
                </a>--%>

                <a class="nav-link nav-profile d-flex align-items-center pe-0" href="#" data-bs-toggle="dropdown">
                    <img src="image/without slogan_R.png" alt="Profile" class="rounded-circle" width="300%" />
                    <%--<span class="d-none d-md-block ps-2">840PRASS</span>--%>
                </a>

                <%--<a class="nav-link nav-profile d-flex align-items-center pe-0" href="#" data-bs-toggle="dropdown">
                    <img src="image/without slogan_R.png" alt="Profile" class="rounded-circle" width="300%" />
                    <span class="d-none d-md-block ps-2">840PRASS</span>
                </a>--%>
            </li>
        </ul>
    </nav>
</header>


<aside id="sidebar" class="sidebar">



    <ul class="sidebar-nav" id="sidebar-nav">
    
        <li class="nav-item">
            <a href="Home.aspx" class='
                        <% If Request.Url.AbsolutePath.EndsWith("Home.aspx") Then
                    Response.Write("nav-link ")
                Else
                    Response.Write("nav-link collapsed")
                End If %>'>
                <i class="bi bi-grid"></i>
                <span>Home Page</span>
            </a>
        </li>
        <li class="nav-heading" style="font-size: 1rem;">Menu</li>

        <li class="nav-item">
            <%
                Dim Cassette As New List(Of String) From {
                        "Cass_MN.aspx",
                        "Cass_Database.aspx"
                    }%>
             <a href="#Cass_MN-nav" data-bs-toggle="collapse" class='
             <% If Cassette.Any(Function(path) Request.Url.AbsolutePath.EndsWith(path)) Then
                     Response.Write("nav-link")
                 Else
                     Response.Write("nav-link collapsed")
                 End If %>'>
                <i class="bi bi-menu-button-wide"></i>
                <span>Cassette</span>
                <i class="bi bi-chevron-down ms-auto"></i>
             </a>
            <ul id="Cass_MN-nav" data-bs-parent="#sidebar-nav" class='
                                <% If Cassette.Any(Function(path) Request.Url.AbsolutePath.EndsWith(path)) Then
                    Response.Write("nav-content collapse  show")
                Else
                    Response.Write("nav-content collapse")
                End If %>'>
                <li>
                    <% If Request.Url.AbsolutePath.EndsWith("Cass_MN.aspx") Then %>
                    <a href="Cass_MN.aspx " class="active">
                        <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                        <span>Cassette M/N And Repair</span>
                    </a>
                    <% Else %>
                    <a href="Cass_MN.aspx ">
                        <i class="bi bi-circle"></i>
                        <span>Cassette M/N And Repair</span>
                    </a>
                    <% End If %>   
                </li>

                 <li>
                    <% If Request.Url.AbsolutePath.EndsWith("Cass_Database.aspx") Then %>
                    <a href="Cass_Database.aspx " class="active" >
                        <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                        <span>Cassette Database</span>
                    </a>
                    <% Else %>
                    <a href="Cass_Database.aspx " >
                        <i class="bi bi-circle"></i>
                        <span>Cassette Database</span>
                    </a>
                    <% End If %>   
                </li>
            </ul>
        </li>

        <li class="nav-item">
              <li class="nav-heading" style="font-size: 1rem;">MaterialBarcode
                             <img src="<%= ResolveUrl("~/image/Gear.gif") %>" alt="Loading..." width="7%" />
                </li>
                <%
                    Dim SMT As New List(Of String) From {
                                        "SMTMBSetup.aspx",
                                        "SMTMBChangeMat.aspx",
                                        "SMTMBChangeLot.aspx",
                                        "SMTMBChangeCass.aspx",
                                        "MBSetupModelAssembly.aspx",
                                        "MBSetupModelAssembly_HISTORY.aspx",
                                        "ADMIN_ASS_USER.aspx",
                                        "ADMIN_ASS_REGISMODEL.aspx",
                                        "ADMIN_ASS_CHANGEMAT.aspx",
                                        "Chemical_ASS.aspx",
                                        "SMTMBCHIS.aspx",
                                        "SMTMB_ADMIN_MAT.aspx",
                                        "SMTMB_ADMIN_USER.aspx"
        }%>

            <a href="#MaterialBarcodeSystem-nav" data-bs-toggle="collapse" class='
             <% If SMT.Any(Function(path) Request.Url.AbsolutePath.EndsWith(path)) Then
                    Response.Write("nav-link")
                Else
                    Response.Write("nav-link collapsed")
                End If %>'>

                <i class="bi bi-box-seam"></i>
                <span>MaterialBarcodeSystem</span>
                <i class="bi bi-chevron-down ms-auto"></i>
            </a>


            <ul id="MaterialBarcodeSystem-nav" data-bs-parent="#sidebar-nav" class=' 
                                <% If SMT.Any(Function(path) Request.Url.AbsolutePath.EndsWith(path)) Then
                    Response.Write("collapsed collapse show")
                Else
                    Response.Write("collapsed collapse")
                End If %>'>
                <li class="nav-heading" style="font-size: 1rem;">MaterialBarcode SMT 
                             <img src="<%= ResolveUrl("~/image/Gear.gif") %>" alt="Loading..." width="7%" />
                </li>
                <li class="nav-item">
                    <%
                        Dim SMTUsing As New List(Of String) From {
                "SMTMBSetup.aspx",
                "SMTMBChangeMat.aspx",
                "SMTMBChangeLot.aspx",
                "SMTMBChangeCass.aspx",
                "SMTMBCHIS.aspx",
                "SMTMB_ADMIN_MAT.aspx",
                "SMTMB_ADMIN_USER.aspx"
        }%>

                    <a data-bs-toggle="collapse" href="#MaterialBarcodeSystemSMT-nav" class='
                                <% If SMTUsing.Any(Function(path) Request.Url.AbsolutePath.EndsWith(path)) Then
                            Response.Write("nav-link show ")
                        Else
                            Response.Write("nav-link show collapsed")
                        End If %>'>
                        <i class="bi bi-menu-button-wide"></i>
                        <span>MBS (SMT)</span>
                        <i class="bi bi-chevron-down ms-auto"></i>
                    </a>
                    <ul id="MaterialBarcodeSystemSMT-nav" class='
                                <% If SMTUsing.Any(Function(path) Request.Url.AbsolutePath.EndsWith(path)) Then
                            Response.Write("collapsed collapse show")
                        Else
                            Response.Write("collapsed collapse")
                        End If %>'>
                        <li class="nav-item">
                            <a data-bs-toggle="collapse" href="#MaterialBarcodeSystemSMT1-nav" class='
                                <% If SMTUsing.Any(Function(path) Request.Url.AbsolutePath.EndsWith(path)) Then
                                    Response.Write("nav-link show ")
                                Else
                                    Response.Write("nav-link show collapsed")
                                End If %>'>
                                <i class="bi bi-menu-button-wide"></i>
                                <span>SMT Using</span>
                                <i class="bi bi-chevron-down ms-auto"></i>
                            </a>
                            <ul id="MaterialBarcodeSystemSMT1-nav" class='
                                <% If SMTUsing.Any(Function(path) Request.Url.AbsolutePath.EndsWith(path)) Then
                                    Response.Write("nav-content collapse  show")
                                Else
                                    Response.Write("nav-content collapse")
                                End If %>'>
                                <li>
                                    <% If Request.Url.AbsolutePath.EndsWith("SMTMBSetup.aspx") Then %>
                                    <a href="SMTMBSetup.aspx " class="active">
                                        <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                                        <span>Setup Model</span>
                                    </a>
                                    <% Else %>
                                    <a href="SMTMBSetup.aspx ">
                                        <i class="bi bi-circle"></i>
                                        <span>Setup Model</span>
                                    </a>
                                    <% End If %>     
                                </li>
                                <li>
                                    <% If Request.Url.AbsolutePath.EndsWith("SMTMBChangeMat.aspx") Then %>
                                    <a href="SMTMBChangeMat.aspx " class="active">
                                        <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                                        <span>Change Material</span>
                                    </a>
                                    <% Else %>
                                    <a href="SMTMBChangeMat.aspx ">
                                        <i class="bi bi-circle"></i>
                                        <span>Change Material</span>
                                    </a>
                                    <% End If %>   
                                </li>
                                <li>
                                    <% If Request.Url.AbsolutePath.EndsWith("SMTMBChangeLot.aspx") Then %>
                                    <a href="SMTMBChangeLot.aspx " class="active">
                                        <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                                        <span>Change Lot</span>
                                    </a>
                                    <% Else %>
                                    <a href="SMTMBChangeLot.aspx ">
                                        <i class="bi bi-circle"></i>
                                        <span>Change Lot</span>
                                    </a>
                                    <% End If %>   
                                </li>
                                <li>
                                    <% If Request.Url.AbsolutePath.EndsWith("SMTMBChangeCass.aspx") Then %>
                                    <a href="SMTMBChangeCass.aspx " class="active" target="_blank">
                                        <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                                        <span>Change Cassette</span>
                                    </a>
                                    <% Else %>
                                    <a href="SMTMBChangeCass.aspx " target="_blank">
                                        <i class="bi bi-circle"></i>
                                        <span>Change Cassette</span>
                                    </a>
                                    <% End If %>
                                </li>
                            </ul>
                        </li>



                         <li class="nav-item">
                            <a data-bs-toggle="collapse" href="#SMTHistory-nav" class='
                                <% If Request.Url.AbsolutePath.EndsWith("SMTMBCHIS.aspx") Then
                                    Response.Write("nav-link show")
                                Else
                                    Response.Write("nav-link collapsed")
                                End If %>'>
                                <i class="bi bi-menu-button-wide"></i>
                                <span>SMT History</span>
                                <i class="bi bi-chevron-down ms-auto"></i>
                            </a>
                            <ul id="SMTHistory-nav" class='
                                <% If Request.Url.AbsolutePath.EndsWith("SMTMBCHIS.aspx") Then
                                    Response.Write("nav-content collapse  show")
                                Else
                                    Response.Write("nav-content collapse")
                                End If %>'>
                                <li>
                                    <% If Request.Url.AbsolutePath.EndsWith("SMTMBCHIS.aspx") Then %>
                                    <a href="SMTMBCHIS.aspx" class="active">
                                        <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                                        <span>History</span>
                                    </a>
                                    <% Else %>
                                    <a href="SMTMBCHIS.aspx">
                                        <i class="bi bi-circle"></i>
                                        <span>History</span>
                                    </a>
                                    <% End If %>
                                </li>
                            </ul>
                        </li>



                        <li class="nav-item">
                            <a data-bs-toggle="collapse" href="#MBSetupModelSMT-nav" class='
                               <% If Request.Url.AbsolutePath.EndsWith("SMTMB_ADMIN_USER.aspx") Or
Request.Url.AbsolutePath.EndsWith("ADMIN_ASS_REGISMODEL.aspx") Or
Request.Url.AbsolutePath.EndsWith("SMTMB_ADMIN_MAT.aspx") Then
                                    Response.Write("nav-link show")
                                Else
                                    Response.Write("nav-link collapsed")
                                End If %>'>
                                <i class="bi bi-menu-button-wide"></i>
                                <span>ADMIN</span>
                                <i class="bi bi-chevron-down ms-auto"></i>
                            </a>
                            <ul id="MBSetupModelSMT-nav" class='
                                <% If Request.Url.AbsolutePath.EndsWith("SMTMB_ADMIN_USER.aspx") Or
Request.Url.AbsolutePath.EndsWith("ADMIN_ASS_REGISMODEL.aspx") Or
Request.Url.AbsolutePath.EndsWith("SMTMB_ADMIN_MAT.aspx") Then
                                    Response.Write("nav-content collapse  show")
                                Else
                                    Response.Write("nav-content collapse")
                                End If %>'>
                                <li>
                                    <% If Request.Url.AbsolutePath.EndsWith("SMTMB_ADMIN_USER.aspx") Then %>
                                    <a href="SMTMB_ADMIN_USER.aspx" class="active">
                                        <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                                        <span>Register User</span>
                                    </a>
                                    <% Else %>
                                    <a href="SMTMB_ADMIN_USER.aspx">
                                        <i class="bi bi-circle"></i>
                                        <span>Register User</span>
                                    </a>
                                    <% End If %>                                            
                                </li>
                                <li>
                                    <% If Request.Url.AbsolutePath.EndsWith("SMTMB_ADMIN_MAT.aspx") Then %>
                                    <a href="SMTMB_ADMIN_MAT.aspx" class="active">
                                        <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                                        <span>Change Material</span>
                                    </a>
                                    <% Else %>
                                    <a href="SMTMB_ADMIN_MAT.aspx">
                                        <i class="bi bi-circle"></i>
                                        <span>Change Material</span>
                                    </a>
                                    <% End If %>                                           
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li class="nav-heading" style="font-size: 0.95rem;">MaterialBarcode ASSY
                             <img src="<%= ResolveUrl("~/image/Gear.gif") %>" alt="Loading..." width="7%" />
                </li>
                <li class="nav-item">
                    <%
                        Dim ASSY As New List(Of String) From {
                "MBSetupModelAssembly.aspx",
                "MBSetupModelAssembly_HISTORY.aspx",
                "ADMIN_ASS_USER.aspx",
                "ADMIN_ASS_REGISMODEL.aspx",
                "ADMIN_ASS_CHANGEMAT.aspx"
            }%>
                    <a data-bs-toggle="collapse" href="#MaterialBarcodeSystemASS-nav" class='
                                <% If ASSY.Any(Function(path) Request.Url.AbsolutePath.EndsWith(path)) Then
                            Response.Write("nav-link show ")
                        Else
                            Response.Write("nav-link show collapsed")
                        End If %>'>
                        <i class="bi bi-menu-button-wide"></i>
                        <span>MBS (ASSY)</span>
                        <i class="bi bi-chevron-down ms-auto"></i>
                    </a>
                    <ul id="MaterialBarcodeSystemASS-nav" class='
                                <% If ASSY.Any(Function(path) Request.Url.AbsolutePath.EndsWith(path)) Then
                            Response.Write("collapsed collapse show")
                        Else
                            Response.Write("collapsed collapse")
                        End If %>'>
                        <li class="nav-item">
                            <a data-bs-toggle="collapse" href="#MBSetupModelAssembly-nav" class='
                                <% If Request.Url.AbsolutePath.EndsWith("MBSetupModelAssembly.aspx") Then
                                    Response.Write("nav-link show")
                                Else
                                    Response.Write("nav-link collapsed")
                                End If %>'>
                                <i class="bi bi-menu-button-wide"></i>
                                <span>ASSY Record</span>
                                <i class="bi bi-chevron-down ms-auto"></i>
                            </a>
                            <ul id="MBSetupModelAssembly-nav" class='
                                <% If Request.Url.AbsolutePath.EndsWith("MBSetupModelAssembly.aspx") Then
                                    Response.Write("nav-content collapse  show")
                                Else
                                    Response.Write("nav-content collapse")
                                End If %>'>
                                <li>
                                    <% If Request.Url.AbsolutePath.EndsWith("MBSetupModelAssembly.aspx") Then %>
                                    <a href="MBSetupModelAssembly.aspx " class="active">
                                        <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                                        <span>Material Record</span>
                                    </a>
                                    <% Else %>
                                    <a href="MBSetupModelAssembly.aspx ">
                                        <i class="bi bi-circle"></i>
                                        <span>Material Record</span>
                                    </a>
                                    <% End If %>                                              
                                </li>
                            </ul>
                        </li>
                        <li class="nav-item">
                            <a data-bs-toggle="collapse" href="#MBSetupModelAssembly2-nav" class='
                                <% If Request.Url.AbsolutePath.EndsWith("MBSetupModelAssembly_HISTORY.aspx") Then
                                    Response.Write("nav-link show")
                                Else
                                    Response.Write("nav-link collapsed")
                                End If %>'>
                                <i class="bi bi-menu-button-wide"></i>
                                <span>ASSY History</span>
                                <i class="bi bi-chevron-down ms-auto"></i>
                            </a>
                            <ul id="MBSetupModelAssembly2-nav" class='
                                <% If Request.Url.AbsolutePath.EndsWith("MBSetupModelAssembly_HISTORY.aspx") Then
                                    Response.Write("nav-content collapse  show")
                                Else
                                    Response.Write("nav-content collapse")
                                End If %>'>
                                <li>
                                    <% If Request.Url.AbsolutePath.EndsWith("MBSetupModelAssembly_HISTORY.aspx") Then %>
                                    <a href="MBSetupModelAssembly_HISTORY.aspx" class="active">
                                        <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                                        <span>Open History</span>
                                    </a>
                                    <% Else %>
                                    <a href="MBSetupModelAssembly_HISTORY.aspx">
                                        <i class="bi bi-circle"></i>
                                        <span>Open History</span>
                                    </a>
                                    <% End If %>
                                </li>
                            </ul>
                        </li>
                        <li class="nav-item">
                            <a data-bs-toggle="collapse" href="#MBSetupModelAssembly3-nav" class='
                               <% If Request.Url.AbsolutePath.EndsWith("ADMIN_ASS_USER.aspx") Or
Request.Url.AbsolutePath.EndsWith("ADMIN_ASS_REGISMODEL.aspx") Or
Request.Url.AbsolutePath.EndsWith("ADMIN_ASS_CHANGEMAT.aspx") Then
                                    Response.Write("nav-link show")
                                Else
                                    Response.Write("nav-link collapsed")
                                End If %>'>
                                <i class="bi bi-menu-button-wide"></i>
                                <span>ADMIN</span>
                                <i class="bi bi-chevron-down ms-auto"></i>
                            </a>
                            <ul id="MBSetupModelAssembly3-nav" class='
                                <% If Request.Url.AbsolutePath.EndsWith("ADMIN_ASS_USER.aspx") Or
Request.Url.AbsolutePath.EndsWith("ADMIN_ASS_REGISMODEL.aspx") Or
Request.Url.AbsolutePath.EndsWith("ADMIN_ASS_CHANGEMAT.aspx") Then
                                    Response.Write("nav-content collapse  show")
                                Else
                                    Response.Write("nav-content collapse")
                                End If %>'>
                                <li>
                                    <% If Request.Url.AbsolutePath.EndsWith("ADMIN_ASS_USER.aspx") Then %>
                                    <a href="ADMIN_ASS_USER.aspx" class="active">
                                        <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                                        <span>Register User</span>
                                    </a>
                                    <% Else %>
                                    <a href="ADMIN_ASS_USER.aspx">
                                        <i class="bi bi-circle"></i>
                                        <span>Register User</span>
                                    </a>
                                    <% End If %>                                            
                                </li>
                                    <%--<li>
                                             <% If Request.Url.AbsolutePath.EndsWith("ADMIN_ASS_REGISMODEL.aspx") Then %>
                                             <a href="ADMIN_ASS_REGISMODEL.aspx"  class="active">
                                                <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                                                  <span>Register Model</span>
                                            </a>
                                                <% Else %>
                                            <a href="ADMIN_ASS_REGISMODEL.aspx">
                                                <i class="bi bi-circle"></i>
                                              <span>Register Model</span>
                                            </a>
                                                <% End If %>                                          
                                        </li>--%>
                                <li>
                                    <% If Request.Url.AbsolutePath.EndsWith("ADMIN_ASS_CHANGEMAT.aspx") Then %>
                                    <a href="ADMIN_ASS_CHANGEMAT.aspx" class="active">
                                        <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                                        <span>Change Material</span>
                                    </a>
                                    <% Else %>
                                    <a href="ADMIN_ASS_CHANGEMAT.aspx">
                                        <i class="bi bi-circle"></i>
                                        <span>Change Material</span>
                                    </a>
                                    <% End If %>                                           
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li class="nav-heading" style="font-size: 0.95rem;">Chemical System
                             <img src="<%= ResolveUrl("~/image/Gear.gif") %>" alt="Loading..." width="7%" />
                </li>
                <li class="nav-item">
                    <a data-bs-toggle="collapse" href="#ChemicalSystem-nav" class='
                        <% If Request.Url.AbsolutePath.EndsWith("Chemical_ASS.aspx") Then
                            Response.Write("nav-link show ")
                        Else
                            Response.Write("nav-link show collapsed")
                        End If %>'>
                        <i class="bi bi-menu-button-wide"></i>
                        <span>Chemical System</span>
                        <i class="bi bi-chevron-down ms-auto"></i>
                    </a>
                    <ul id="ChemicalSystem-nav" class='
                                <% If Request.Url.AbsolutePath.EndsWith("Chemical_ASS.aspx") Then
                            Response.Write("collapsed collapse show")
                        Else
                            Response.Write("collapsed collapse")
                        End If %>'>
                        <li class="nav-item">
                            <a data-bs-toggle="collapse" href="#ChemicalSystemCHK-nav" class='
                                <% If Request.Url.AbsolutePath.EndsWith("Chemical_ASS.aspx") Then
                                    Response.Write("nav-link show")
                                Else
                                    Response.Write("nav-link collapsed")
                                End If %>'>
                                <i class="bi bi-menu-button-wide"></i>
                                <span>PSMOD</span>
                                <i class="bi bi-chevron-down ms-auto"></i>
                            </a>
                            <ul id="ChemicalSystemCHK-nav" class='
                                <% If Request.Url.AbsolutePath.EndsWith("Chemical_ASS.aspx") Then
                                    Response.Write("nav-content collapse  show")
                                Else
                                    Response.Write("nav-content collapse")
                                End If %>'>
                                <li>
                                    <% If Request.Url.AbsolutePath.EndsWith("Chemical_ASS.aspx") Then %>
                                    <a href="Chemical_ASS.aspx" class="active">
                                        <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                                        <span>Chemical Check</span>
                                    </a>
                                    <% Else %>
                                    <a href="Chemical_ASS.aspx ">
                                        <i class="bi bi-circle"></i>
                                        <span>Chemical Check</span>
                                    </a>
                                    <% End If %>                                              
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>

          
        
        <li class="nav-item">

            <li class="nav-heading" style="font-size: 1rem;">Maintenance
                             <img src="<%= ResolveUrl("~/image/Gear.gif") %>" alt="Loading..." width="7%" />
            </li>

            <%
                Dim Maintenance As New List(Of String) From {
                        "Maintenance_Cause_Problem.aspx"
        }%>
             <a href="#Maintenance-nav" data-bs-toggle="collapse" class='
             <% If Maintenance.Any(Function(path) Request.Url.AbsolutePath.EndsWith(path)) Then
                     Response.Write("nav-link")
                 Else
                     Response.Write("nav-link collapsed")
                 End If %>'>
                <i class="bi bi-menu-button-wide"></i>
                <span>Maintenance</span>
                <i class="bi bi-chevron-down ms-auto"></i>
             </a>
            <ul id="Maintenance-nav" data-bs-parent="#sidebar-nav" class='
                                <% If Maintenance.Any(Function(path) Request.Url.AbsolutePath.EndsWith(path)) Then
                    Response.Write("nav-content collapse  show")
                Else
                    Response.Write("nav-content collapse")
                End If %>'>

                <li>
                    <% If Request.Url.AbsolutePath.EndsWith("Maintenance_Cause_Problem.aspx") Then %>
                    <a href="Maintenance_Cause_Problem.aspx " class="active">
                        <img src="<%= ResolveUrl("~/image/Chasing arrows.gif") %>" alt="Loading..." width="7%" />
                        <span>Machine Cause Problem</span>
                    </a>
                    <% Else %>
                    <a href="Maintenance_Cause_Problem.aspx ">
                        <i class="bi bi-circle"></i>
                        <span>Machine Cause Problem</span>
                    </a>
                    <% End If %>   
                </li>

                
            </ul>
        </li>

    </ul>
</aside>
