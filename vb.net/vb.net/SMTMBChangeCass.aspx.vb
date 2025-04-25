Imports System.Data.OleDb
Imports System.Data
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.TemplateControl
Imports System.Configuration
Imports System.IO
Imports System.IO.TextWriter
Imports System.Web.Security
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Globalization
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Windows.Forms
Imports System.Threading
Imports System.Web.UI.DataVisualization.Charting
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Drawing
Partial Class SMTMBChangeCass
    Inherits System.Web.UI.Page
    Dim PROCESS As String = "SMT"
    Dim STATUS As String = "CHANGECASS"
    Dim MT800DB As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Page.Form.Attributes.Add("enctype", "multipart/form-data")
            PanelMATERIALHIS.Visible = True
            Load_gvcassettehis()
        End If

    End Sub
    Private Sub MesgBox(ByVal sMessage As String)
        Dim msg As String
        msg = "<script language='javascript'>"
        msg += "alert('" & sMessage & "');"
        msg += "</script>"
        Response.Write(msg)
    End Sub
    Protected Sub tbUser_TextChanged(sender As Object, e As EventArgs) Handles tbUser.TextChanged
        Dim strUser As String = String.Empty
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand("select USERID from NVP_ALLPROCESS_USER where USERID = '" & tbUser.Text & "' and PROCESS = '" & PROCESS & "' or PROCESS = 'ALL' ", conn)
        conn.Open()
        Dim rd As OracleDataReader = cmd.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strUser = String.Empty Else strUser = CStr(rd.GetValue(0))
            End While
        Finally
            rd.Close()
        End Try
        conn.Close()
        Select Case tbUser.Text = strUser And tbUser.Text <> String.Empty
            Case True
                PNddlLinePD.Visible = True
                ''PanelMATERIALHIS.Visible = False
                'load_clgvnaja()
                'PanelMATERIALHIS.Visible = False
                Load_ddlLinePD()
            Case False
                PaneNEWCassette.Visible = False
                tbNEWCassette.Text = String.Empty
                PaneCassette.Visible = False
                tbCassette.Text = String.Empty
                Paneshowmat.Visible = False
                tbshowmat.Text = String.Empty
                PaneDDLFEED.Visible = False
                'DDLFEED.SelectedIndex.ToString()
                tbfeed.Text = String.Empty
                DDLCAUSECSS.SelectedIndex.ToString()
                PanelMATERIAL.Visible = False
                PaneDDLCAUSECSS.Visible = False
                tbLot.Text = String.Empty
                tbModel.Text = String.Empty
                'ddlLine.SelectedIndex.ToString()
                tbline.Text = String.Empty
                ddlLinePD.SelectedIndex.ToString()
                tbUser.Text = String.Empty
                PNddlLinePD.Visible = False
                pnLine.Visible = False
                PanetbModel.Visible = False
                PNbutton.Visible = False
                PanetbLot.Visible = False
                PNddlMC.Visible = False
                tbUser.Text = String.Empty
                MesgBox("รหัสพนักงานไม่ถูกต้อง")
        End Select
    End Sub
    Private Sub Load_ddlLinePD()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select PRODUCTS From NVP_ALLPROCESS_PRODUCTS where PROCESS = '" & PROCESS & "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlLinePD.DataSource = dt
            ddlLinePD.DataTextField = "PRODUCTS"
            ddlLinePD.DataValueField = "PRODUCTS"
            ddlLinePD.DataBind()
            ddlLinePD.Items.Insert(0, New ListItem("(กรุณาเลือก โปรดักส์)", String.Empty))
        End Using
    End Sub
    Protected Sub ddlLinePD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLinePD.SelectedIndexChanged
        Select Case ddlLinePD.SelectedIndex = 0
            Case True
                PaneNEWCassette.Visible = False
                tbNEWCassette.Text = String.Empty
                PaneCassette.Visible = False
                tbCassette.Text = String.Empty
                Paneshowmat.Visible = False
                tbshowmat.Text = String.Empty
                PaneDDLFEED.Visible = False
                'DDLFEED.SelectedIndex.ToString()
                tbfeed.Text = String.Empty
                DDLCAUSECSS.SelectedIndex.ToString()
                PanelMATERIAL.Visible = False
                PaneDDLCAUSECSS.Visible = False
                tbLot.Text = String.Empty
                tbModel.Text = String.Empty
                'ddlLine.SelectedIndex.ToString()
                tbline.Text = String.Empty
                pnLine.Visible = False
                PanetbModel.Visible = False
                PNbutton.Visible = False
                PanetbLot.Visible = False
                PNddlMC.Visible = False
            Case False
                pnLine.Visible = True
                Call Load_UesrName()
                'Load_ddlLine()
                PaneCassette.Visible = True
        End Select
    End Sub
    Private Sub Load_UesrName()
        Dim strUser As String = String.Empty
        Dim strUserID As String = String.Empty
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand("select USERNAME,USERID from NVP_ALLPROCESS_USER where USERID = '" & tbUser.Text & "' and PROCESS = '" & PROCESS & "' or PROCESS = 'ALL'", conn)
        conn.Open()
        Dim rd As OracleDataReader = cmd.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strUser = String.Empty Else strUser = CStr(rd.GetValue(0))
                If IsDBNull(rd.GetValue(1)) = True Then strUserID = String.Empty Else strUserID = CStr(rd.GetValue(1))
                If strUserID = tbUser.Text Then
                    Exit While
                End If
            End While
        Finally
            rd.Close()
        End Try
        conn.Close()
        lbshowFRONTENDLINE.InnerText = strUser
        lbshowCode.InnerText = tbUser.Text
    End Sub
    'Private Sub Load_ddlLine()
    '    pnLine.Visible = True
    '    Dim conn As New OracleConnection(MT800DB)
    '    Dim cmd As New OracleCommand()
    '    Dim acc As String = " select LINE From NVP_ALLPROCESS_LINE where PRODUCTS = '" & ddlLinePD.SelectedItem.Value & "' and PROCESS = '" & PROCESS & "'"
    '    cmd.CommandText = acc
    '    cmd.Connection = conn
    '    Using sda As New OracleDataAdapter(cmd)
    '        Dim dt As New DataTable()
    '        sda.Fill(dt)
    '        ddlLine.DataSource = dt
    '        ddlLine.DataTextField = "LINE"
    '        ddlLine.DataValueField = "LINE"
    '        ddlLine.DataBind()
    '        ddlLine.Items.Insert(0, New ListItem("(กรุณาเลือก ไลน์)", String.Empty))
    '    End Using
    'End Sub
    'Protected Sub ddlLine_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLine.SelectedIndexChanged
    '    Select Case ddlLine.SelectedIndex = 0
    '        Case True
    '            PaneNEWCassette.Visible = False
    '            tbNEWCassette.Text = String.Empty
    '            PaneCassette.Visible = False
    '            tbCassette.Text = String.Empty
    '            Paneshowmat.Visible = False
    '            tbshowmat.Text = String.Empty
    '            PaneDDLFEED.Visible = False
    '            DDLFEED.SelectedIndex.ToString()
    '            DDLCAUSECSS.SelectedIndex.ToString()
    '            PanelMATERIAL.Visible = False
    '            PaneDDLCAUSECSS.Visible = False
    '            tbLot.Text = String.Empty
    '            tbModel.Text = String.Empty

    '            PanetbModel.Visible = False
    '            PNbutton.Visible = False
    '            PanetbLot.Visible = False
    '            PNddlMC.Visible = False
    '        Case False
    '            'PanetbModel.Visible = True
    '            'PaneCassette.Visible = True
    '    End Select
    'End Sub
    Protected Sub tbCassette_TextChanged(sender As Object, e As EventArgs) Handles tbCassette.TextChanged
        Select Case tbCassette.Text <> String.Empty
            Case True
                'PanetbModel.Visible = True
                Dim strMODEL As String = String.Empty
                Dim strLINE As String = String.Empty
                Dim strLOTNO As String = String.Empty
                Dim strMACHINE As String = String.Empty
                Dim strFEED As String = String.Empty
                Dim strMATERIAL As String = String.Empty
                Dim strUser As String = String.Empty
                Dim connn As New OracleConnection(MT800DB)
                Dim cmdd As New OracleCommand("select MODEL,LINE,LOTNO,MACHINE,FEED,MATERIAL from (select MODEL,LINE,LOTNO,MACHINE,FEED,MATERIAL, rownum as rn from (select MODEL,LINE,LOTNO,MACHINE,FEED,MATERIAL from NVP_ALLPROCESS_HIS where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "' and CASSETTE = '" & tbCassette.Text & "' ORDER BY TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME DESC)) where rn ='1'", connn)
                connn.Open()
                Dim rd As OracleDataReader = cmdd.ExecuteReader()
                Try
                    While rd.Read()
                        If IsDBNull(rd.GetValue(0)) = True Then tbModel.Text = String.Empty Else tbModel.Text = CStr(rd.GetValue(0))
                        If IsDBNull(rd.GetValue(1)) = True Then tbline.Text = String.Empty Else tbline.Text = CStr(rd.GetValue(1))
                        If IsDBNull(rd.GetValue(2)) = True Then tbLot.Text = String.Empty Else tbLot.Text = CStr(rd.GetValue(2))
                        If IsDBNull(rd.GetValue(3)) = True Then tbmc.Text = String.Empty Else tbmc.Text = CStr(rd.GetValue(3))
                        If IsDBNull(rd.GetValue(4)) = True Then tbfeed.Text = String.Empty Else tbfeed.Text = CStr(rd.GetValue(4))
                        If IsDBNull(rd.GetValue(5)) = True Then tbshowmat.Text = String.Empty Else tbshowmat.Text = CStr(rd.GetValue(5))
                    End While
                Finally
                    rd.Close()
                End Try
                connn.Close()

                If tbModel.Text = String.Empty And tbline.Text = String.Empty And tbLot.Text = String.Empty And tbfeed.Text = String.Empty And tbfeed.Text = String.Empty And tbshowmat.Text = String.Empty Then
                    MesgBox("ไม่พบข้อมูล CASSETTE หมายเลข " & tbCassette.Text & "")
                    tbCassette.Text = String.Empty
                Else
                    PanetbModel.Visible = True
                    PanetbLot.Visible = True
                    PNddlMC.Visible = True

                    PanelMATERIAL.Visible = True
                    PaneDDLFEED.Visible = True
                    Paneshowmat.Visible = True
                    PaneDDLCAUSECSS.Visible = True
                    Load_DDLCAUSECSS()
                End If
                'PaneNEWCassette.Visible = True
                'PNBUT.Visible = True
            Case False
                'PaneNEWCassette.Visible = False
                'tbNEWCassette.Text = String.Empty
        End Select
    End Sub
    'Protected Sub tbModel_TextChanged(sender As Object, e As EventArgs) Handles tbModel.TextChanged
    '    Dim strUser As String = String.Empty
    '    Dim conn As New OracleConnection(MT800DB)
    '    Dim cmd As New OracleCommand("select MODEL from NVP_SMT_MAT where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and MODEL = '" & tbModel.Text & "' and PROCESS = '" & PROCESS & "' ", conn)
    '    conn.Open()
    '    Dim rd As OracleDataReader = cmd.ExecuteReader()
    '    Try
    '        While rd.Read()
    '            If IsDBNull(rd.GetValue(0)) = True Then strUser = String.Empty Else strUser = CStr(rd.GetValue(0))
    '        End While
    '    Finally
    '        rd.Close()
    '    End Try
    '    conn.Close()

    '    Select Case tbModel.Text = strUser And tbModel.Text <> String.Empty
    '        Case True
    '            ddlLine.SelectedValue = ddlLine.SelectedItem.Text
    '            PNbutton.Visible = True
    '        Case False
    '            PaneNEWCassette.Visible = False
    '            tbNEWCassette.Text = String.Empty
    '            PaneCassette.Visible = False
    '            tbCassette.Text = String.Empty
    '            Paneshowmat.Visible = False
    '            tbshowmat.Text = String.Empty
    '            PaneDDLFEED.Visible = False
    '            DDLFEED.SelectedIndex.ToString()
    '            DDLCAUSECSS.SelectedIndex.ToString()
    '            PanelMATERIAL.Visible = False
    '            PaneDDLCAUSECSS.Visible = False
    '            tbLot.Text = String.Empty
    '            tbModel.Text = String.Empty

    '            PNbutton.Visible = False
    '            PanetbLot.Visible = False
    '            PNddlMC.Visible = False
    '    End Select
    'End Sub
    'Private Sub btnFrontMD_ServerClick(sender As Object, e As EventArgs) Handles btnFrontMD.ServerClick
    '    tbModel.Text = tbModel.Text & "-F"
    '    PanetbLot.Visible = True
    '    tbLot.Focus()
    '    'tbLot.Focus()
    'End Sub
    'Private Sub btnBackMD_ServerClick(sender As Object, e As EventArgs) Handles btnBackMD.ServerClick
    '    tbModel.Text = tbModel.Text & "-B"
    '    PanetbLot.Visible = True
    '    tbLot.Focus()
    '    'tbLot.Focus()
    'End Sub
    'Private Sub btnNoneMD_ServerClick(sender As Object, e As EventArgs) Handles btnNoneMD.ServerClick
    '    PanetbLot.Visible = True
    '    tbLot.Focus()
    '    'tbLot.Focus()
    'End Sub
    'Protected Sub tbLot_TextChanged(sender As Object, e As EventArgs) Handles tbLot.TextChanged
    '    Dim strMC As String = String.Empty
    '    Dim connn As New OracleConnection(MT800DB)
    '    Dim cmdd As New OracleCommand("select MODEL from NVP_SMT_MAT where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and MODEL = '" & tbModel.Text & "' and PROCESS = '" & PROCESS & "'", connn)
    '    connn.Open()
    '    Dim rdd As OracleDataReader = cmdd.ExecuteReader()
    '    Try
    '        While rdd.Read()
    '            If IsDBNull(rdd.GetValue(0)) = True Then strMC = String.Empty Else strMC = CStr(rdd.GetValue(0))
    '        End While
    '    Finally
    '        rdd.Close()
    '    End Try
    '    connn.Close()
    '    Session("MCModel") = strMC
    '    Select Case tbModel.Text = strMC And tbLot.Text <> String.Empty
    '        Case True
    '            PNddlMC.Visible = True
    '            Load_ddlLineMC()
    '        Case False
    '            PaneNEWCassette.Visible = False
    '            tbNEWCassette.Text = String.Empty
    '            PaneCassette.Visible = False
    '            tbCassette.Text = String.Empty
    '            Paneshowmat.Visible = False
    '            tbshowmat.Text = String.Empty
    '            PaneDDLFEED.Visible = False
    '            DDLFEED.SelectedIndex.ToString()
    '            DDLCAUSECSS.SelectedIndex.ToString()
    '            PanelMATERIAL.Visible = False
    '            PaneDDLCAUSECSS.Visible = False
    '            tbLot.Text = String.Empty

    '            PNddlMC.Visible = False
    '            MesgBox("ชื่อ Model ไม่พบในฐานข้อมูล")
    '    End Select
    'End Sub
    Private Sub Load_DDLCAUSECSS()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select CAUSE From NVP_SMT_CASSETTE_CAUSE where PROCESS = '" & PROCESS & "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            DDLCAUSECSS.DataSource = dt
            DDLCAUSECSS.DataTextField = "CAUSE"
            DDLCAUSECSS.DataValueField = "CAUSE"
            DDLCAUSECSS.DataBind()
            DDLCAUSECSS.Items.Insert(0, New ListItem("(กรุณาเลือก Cause)", String.Empty))
        End Using
    End Sub
    'Private Sub Load_ddlLineMC()
    '    Dim conn As New OracleConnection(MT800DB)
    '    Dim cmd As New OracleCommand()
    '    Dim acc As String = " select DISTINCT MACHINE From NVP_SMT_MAT where MODEL = '" & tbModel.Text & "' and PROCESS = '" & PROCESS & "' and PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "'"
    '    cmd.CommandText = acc
    '    cmd.Connection = conn
    '    Using sda As New OracleDataAdapter(cmd)
    '        Dim dt As New DataTable()
    '        sda.Fill(dt)
    '        ddlLineMC.DataSource = dt
    '        ddlLineMC.DataTextField = "MACHINE"
    '        ddlLineMC.DataValueField = "MACHINE"
    '        ddlLineMC.DataBind()
    '        ddlLineMC.Items.Insert(0, New ListItem("(กรุณาเลือก เครื่องจักร)", String.Empty))
    '    End Using
    'End Sub
    'Protected Sub ddlLineMC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLineMC.SelectedIndexChanged
    '    Select Case ddlLineMC.SelectedItem.Text = "(กรุณาเลือก เครื่องจักร)"
    '        Case True
    '            PaneNEWCassette.Visible = False
    '            tbNEWCassette.Text = String.Empty
    '            PaneCassette.Visible = False
    '            tbCassette.Text = String.Empty
    '            Paneshowmat.Visible = False
    '            tbshowmat.Text = String.Empty
    '            PaneDDLFEED.Visible = False
    '            DDLFEED.SelectedIndex.ToString()
    '            DDLCAUSECSS.SelectedIndex.ToString()
    '            PanelMATERIAL.Visible = False
    '            PaneDDLCAUSECSS.Visible = False
    '        Case False
    '            Load_DDLCAUSECSS()
    '            PanelMATERIAL.Visible = True
    '            PaneDDLCAUSECSS.Visible = True
    '    End Select

    'End Sub

    Protected Sub DDLCAUSECSS_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLCAUSECSS.SelectedIndexChanged
        Select Case DDLCAUSECSS.SelectedItem.Text = "(กรุณาเลือก Cause)"
            Case True
                'PaneNEWCassette.Visible = False
                'tbNEWCassette.Text = String.Empty
                'PaneCassette.Visible = False
                'tbCassette.Text = String.Empty
                'Paneshowmat.Visible = False
                'tbshowmat.Text = String.Empty
                'PaneDDLFEED.Visible = False
                'DDLFEED.SelectedIndex.ToString()
                PaneNEWCassette.Visible = False

            Case False
                'PaneDDLFEED.Visible = True
                'Load_DDLFEED()
                PaneNEWCassette.Visible = True
        End Select

    End Sub
    Protected Sub tbNEWCassette_TextChanged(sender As Object, e As EventArgs) Handles tbNEWCassette.TextChanged
        Select Case tbNEWCassette.Text <> String.Empty
            Case True
                PNBUT.Visible = True
            Case False
                PNBUT.Visible = False
        End Select
    End Sub
    'Private Sub Load_DDLFEED()
    '    Dim conn As New OracleConnection(MT800DB)
    '    Dim cmd As New OracleCommand()
    '    Dim acc As String = " select FEEDNO from NVP_SMT_MAT where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and MODEL = '" & tbModel.Text & "' and MACHINE = '" + ddlLineMC.SelectedItem.Text + "' and PROCESS = '" & PROCESS & "' ORDER BY FEEDNO asc"
    '    cmd.CommandText = acc
    '    cmd.Connection = conn
    '    Using sda As New OracleDataAdapter(cmd)
    '        Dim dt As New DataTable()
    '        sda.Fill(dt)
    '        DDLFEED.DataSource = dt
    '        DDLFEED.DataTextField = "FEEDNO"
    '        DDLFEED.DataValueField = "FEEDNO"
    '        DDLFEED.DataBind()
    '        DDLFEED.Items.Insert(0, New ListItem("(กรุณาเลือก FEEDNO)", String.Empty))
    '    End Using
    'End Sub
    'Protected Sub DDLFEED_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLFEED.SelectedIndexChanged
    '    Select Case DDLFEED.SelectedIndex = 0
    '        Case True
    '            PaneNEWCassette.Visible = False
    '            tbNEWCassette.Text = String.Empty
    '            PaneCassette.Visible = False
    '            tbCassette.Text = String.Empty
    '            Paneshowmat.Visible = False
    '            tbshowmat.Text = String.Empty

    '        Case False
    '            Dim strMATERIAL As String = String.Empty
    '            Dim connn As New OracleConnection(MT800DB)
    '            Dim cmdd As New OracleCommand("select MATERIAL from NVP_SMT_MAT where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and MODEL = '" & tbModel.Text & "' and MACHINE = '" + ddlLineMC.SelectedItem.Text + "' and PROCESS = '" & PROCESS & "' and FEEDNO = '" + DDLFEED.SelectedItem.Text + "'", connn)
    '            connn.Open()
    '            Dim rd As OracleDataReader = cmdd.ExecuteReader()
    '            Try
    '                While rd.Read()
    '                    If IsDBNull(rd.GetValue(0)) = True Then strMATERIAL = String.Empty Else strMATERIAL = CStr(rd.GetValue(0))
    '                End While
    '            Finally
    '                rd.Close()
    '            End Try
    '            connn.Close()
    '            tbshowmat.Text = strMATERIAL

    '            Paneshowmat.Visible = True
    '            PaneCassette.Visible = True
    '    End Select

    'End Sub

    Private Sub Button1_ServerClick(sender As Object, e As EventArgs) Handles Button1.ServerClick
        If tbNEWCassette.Text <> String.Empty Then
            Dim objConn As New OracleConnection
            Dim strConnString As String = MT800DB
            Dim strSQL As String = "INSERT INTO NVP_SMT_CASSETTE_HIS (USERID,USERNAME,DAY,TIME,PRODUCTS,LINE,MODEL,LOTNO,MACHINE,CAUSE,FEED,MATERIAL,CASSBEFORE,CASSAFTER,STATUS,PROCESS)" +
                " VALUES" +
                " (:sUSERID,:sUSERNAME,:sDAY,:sTIME,:sPRODUCTS,:sLINE,:sMODEL,:sLOTNO,:sMACHINE,:sCAUSE,:sFEED,:sMATERIAL,:sCASSBEFORE,:sCASSAFTER,  :sSTATUS,  :sPROCESS)"
            objConn.ConnectionString = strConnString
            objConn.Open()
            Dim objCmd As New OracleCommand(strSQL, objConn)

            objCmd.Parameters.Add("@sUSERID", lbshowCode.InnerText)
            objCmd.Parameters.Add("@sUSERNAME", lbshowFRONTENDLINE.InnerText)
            objCmd.Parameters.Add("@sDAY", CStr(Format(Now, "dd-MMM-yyyy")))
            objCmd.Parameters.Add("@sTIME", CStr(Format(Now, "HH:mm:ss")))
            objCmd.Parameters.Add("@sPRODUCTS", ddlLinePD.SelectedItem.Value)
            objCmd.Parameters.Add("@sLINE", tbline.Text)
            objCmd.Parameters.Add("@sMODEL", tbModel.Text)
            objCmd.Parameters.Add("@sLOTNO", tbLot.Text)
            objCmd.Parameters.Add("@sMATERIAL", tbmc.Text)
            objCmd.Parameters.Add("@sCAUSE", DDLCAUSECSS.SelectedItem.Value)
            objCmd.Parameters.Add("@sFEED", tbfeed.Text)
            objCmd.Parameters.Add("@sMATERIAL", tbshowmat.Text)
            objCmd.Parameters.Add("@sCASSBEFORE", tbCassette.Text)
            objCmd.Parameters.Add("@sCASSAFTER", tbNEWCassette.Text)
            objCmd.Parameters.Add("@sSTATUS", STATUS)
            objCmd.Parameters.Add("@sPROCESS", PROCESS)

            objCmd.ExecuteNonQuery()
            objConn.Close()
            objConn = Nothing

            Using conn As New OracleConnection(MT800DB)
                conn.Open()
                Dim acc As String = "UPDATE NVP_SMT_CASSETTE SET STATUS = :STATUS , CAUSE = :CAUSE WHERE NO = '" & tbCassette.Text & "'"
                Dim cmd As New OracleCommand(acc, conn)
                cmd.Parameters.Add(":STATUS", "Broken")
                cmd.Parameters.Add(":CAUSE", DDLCAUSECSS.SelectedItem.Value)
                'cmd.Parameters.Add(":STATUS", "Overshot")
                cmd.ExecuteNonQuery()
            End Using

            Load_gvcassettehis()
        End If
    End Sub
    Private Sub Load_gvcassettehis()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "SELECT * FROM NVP_SMT_CASSETTE_HIS where PROCESS = '" & PROCESS & "' and STATUS = '" & STATUS & "' "
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvcassettehis.DataSource = dt
            gvcassettehis.DataBind()
        End Using
    End Sub
End Class
