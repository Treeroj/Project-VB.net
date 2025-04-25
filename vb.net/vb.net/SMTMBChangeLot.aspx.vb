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
Partial Class SMTMBChangeLot
    Inherits System.Web.UI.Page
    Dim PROCESS As String = "SMT"
    Dim MT800DB As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Page.Form.Attributes.Add("enctype", "multipart/form-data")
        End If
        'If TextBox1.Text = String.Empty Then
        '    TextBox1.Focus()
        'End If
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
                'PanelMATERIALHIS.Visible = False
                load_clgvnaja()
                PanelMATERIALHIS.Visible = False
                Load_ddlLinePD()
            Case False
                load_clgvnaja()
                PanelMATERIALHIS.Visible = False
                tbUser.Text = String.Empty
                MesgBox("รหัสพนักงานไม่ถูกต้อง")
        End Select
    End Sub
    Private Sub Load_ddlLinePD()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select PRODUCTS From NVP_ALLPROCESS_PRODUCTS where PROCESS = '" & PROCESS & "'"
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
    Private Sub Load_UesrName()
        Dim strUser As String = String.Empty
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand("select USERNAME from NVP_ALLPROCESS_USER where USERID = '" & tbUser.Text & "' and PROCESS = '" & PROCESS & "' or PROCESS = 'ALL' ", conn)
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

        lbshowFRONTENDLINE.InnerText = strUser
        lbshowCode.InnerText = tbUser.Text
        'Session("RSCode") = tbUser.Text
        'Session("RSName") = strUser
    End Sub
    Protected Sub ddlLinePD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLinePD.SelectedIndexChanged
        Select Case ddlLinePD.SelectedIndex = 0
            Case True
                pnLine.Visible = False
                PanelMATERIAL.Visible = False
            Case False
                lbshowTime.InnerText = CStr(Format(Now, "HH:mm:ss"))
                lbshowDATE.InnerText = CStr(Format(Now, "dd-MMM-yyyy"))
                Load_UesrName()
                Load_ddlLine()
                'Load_PreviousModel()
                lbshowFRONTENDPD.InnerText = ddlLinePD.SelectedItem.Text
                pnLine.Visible = True
                PanelMATERIAL.Visible = True

        End Select
    End Sub
    Private Sub Load_ddlLine()
        pnLine.Visible = True
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select LINE From NVP_ALLPROCESS_LINE where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlLine.DataSource = dt
            ddlLine.DataTextField = "LINE"
            ddlLine.DataValueField = "LINE"
            ddlLine.DataBind()
            ddlLine.Items.Insert(0, New ListItem("(กรุณาเลือก ไลน์)", String.Empty))
        End Using
    End Sub
    Protected Sub ddlLine_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLine.SelectedIndexChanged
        Select Case ddlLine.SelectedIndex = 0
            Case True
            Case False
                'Load_PreviousModel()
                'Load_PreviousLOTNOTB()
                'Load_PreviousModelTB()
                Load_GVLOT()
                Load_GVMODEL()
                Load_DDLMC()
                GridViewPNMODEL.Visible = True
                lbshowPreviousLOTNOPN.Visible = True
                PanetbModel.Visible = True
                PanetbLot.Visible = True
                CurrentModelPN.Visible = True
        End Select

    End Sub
    Private Sub Load_GVLOT()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select LOTNO from (select LOTNO, rownum as rn from (select LOTNO from NVP_ALLPROCESS_HIS where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and LINE = '" & ddlLine.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "' ORDER BY TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME DESC)) where rn ='1'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvFiles3.DataSource = dt
            gvFiles3.DataBind()
        End Using

        Dim strUser As String = String.Empty
        Dim connn As New OracleConnection(MT800DB)
        Dim cmdd As New OracleCommand("select LOTNO from (select LOTNO, rownum as rn from (select LOTNO from NVP_ALLPROCESS_HIS where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and LINE = '" & ddlLine.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "' ORDER BY TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME DESC)) where rn ='1'", connn)
        connn.Open()
        Dim rd As OracleDataReader = cmdd.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strUser = String.Empty Else strUser = CStr(rd.GetValue(0))
            End While
        Finally
            rd.Close()
        End Try
        connn.Close()

        PreviousLotLB.InnerText = strUser
    End Sub
    Private Sub load_clGVLOT()
        Dim dtResult As New DataTable()
        dtResult.Columns.Add("LOTNO", GetType(String))
        gvFiles3.DataSource = dtResult
        gvFiles3.DataBind()
    End Sub

    Private Sub Load_GVMODEL()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select MODEL from (select MODEL, rownum as rn from (select MODEL from NVP_ALLPROCESS_HIS where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and LINE = '" & ddlLine.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "' ORDER BY TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME DESC)) where rn ='1'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            GridView1.DataSource = dt
            GridView1.DataBind()
        End Using

        Dim strUser As String = String.Empty
        Dim connn As New OracleConnection(MT800DB)
        Dim cmdd As New OracleCommand("select MODEL from (select MODEL, rownum as rn from (select MODEL from NVP_ALLPROCESS_HIS where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and LINE = '" & ddlLine.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "' ORDER BY TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME DESC)) where rn ='1'", connn)
        connn.Open()
        Dim rd As OracleDataReader = cmdd.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strUser = String.Empty Else strUser = CStr(rd.GetValue(0))
            End While
        Finally
            rd.Close()
        End Try
        connn.Close()

        PreviousModelLB.InnerText = strUser
    End Sub
    Private Sub load_clGVMODEL()
        Dim dtResult As New DataTable()
        dtResult.Columns.Add("MODEL", GetType(String))
        GridView1.DataSource = dtResult
        GridView1.DataBind()
    End Sub

    Private Sub Load_DDLMC()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select DISTINCT MACHINE From NVP_ALLPROCESS_HIS where MODEL = '" + PreviousModelLB.InnerText + "' and LOTNO = '" + PreviousLotLB.InnerText + "' and PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlLineMC.DataSource = dt
            ddlLineMC.DataTextField = "MACHINE"
            ddlLineMC.DataValueField = "MACHINE"
            ddlLineMC.DataBind()
            ddlLineMC.Items.Insert(0, New ListItem("(กรุณาเลือก เครื่องจักร)", String.Empty))
        End Using
    End Sub
    Protected Sub CurrentModel_TextChanged(sender As Object, e As EventArgs) Handles CurrentModel.TextChanged

        Select Case CurrentModel.Text = String.Empty
            Case True

                MesgBox("กรุณากรอก Current Model")
                CurrentLotPN.Visible = False
            Case False
                CurrentLotPN.Visible = True

        End Select

    End Sub
    Protected Sub CurrentLot_TextChanged(sender As Object, e As EventArgs) Handles CurrentLot.TextChanged

        Select Case CurrentLot.Text = String.Empty
            Case True
                MesgBox("กรุณากรอก Current Lot")
                PNddlMC.Visible = False
            Case False
                If PreviousLotLB.InnerText = CurrentLot.Text Then
                    MesgBox("ล็อตงานห้ามซ้ำกัน")
                    CurrentLot.Text = String.Empty
                Else
                    PNddlMC.Visible = True
                End If
        End Select

    End Sub
    Protected Sub ddlLineMC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLineMC.SelectedIndexChanged
        Select Case ddlLineMC.SelectedItem.Text = "(กรุณาเลือก เครื่องจักร)"
            Case True

            Case False

                LBCurrentModel.InnerText = CurrentModel.Text
                LBCurrentLot.InnerText = CurrentLot.Text
                PNPreviousCurrent.Visible = True
                Load_GVHIS()
                PanelMATERIALHIS.Visible = True

                PNBUT.Visible = True
        End Select

    End Sub
    Private Sub Load_GVHIS()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select MATERIAL,INVOICE,MATERIALLOT,QTY,REELNO,LINE,PRODUCTS,MACHINE,FEED,CASSETTE,COUNTER"
        acc += " from NVP_ALLPROCESS_HIS"
        acc += " where MODEL = '" + PreviousModelLB.InnerText + "' and LOTNO = '" + PreviousLotLB.InnerText + "' and MACHINE='" + ddlLineMC.SelectedItem.Text + "' and PROCESS = '" & PROCESS & "' and PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "'"
        acc += " ORDER BY TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME DESC"

        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()

            Dim dataColumnNAME As DataColumn = New DataColumn("Name", GetType(String))
            dataColumnNAME.DefaultValue = lbshowFRONTENDLINE.InnerText
            dt.Columns.Add(dataColumnNAME)

            Dim dataColumnDAY As DataColumn = New DataColumn("Date", GetType(String))
            dataColumnDAY.DefaultValue = CStr(Format(Now, "dd-MMM-yyyy"))
            dt.Columns.Add(dataColumnDAY)

            Dim dataColumnTIME As DataColumn = New DataColumn("Time", GetType(String))
            dataColumnTIME.DefaultValue = CStr(Format(Now, "HH:mm:ss"))
            dt.Columns.Add(dataColumnTIME)

            Dim dataColumnMODEL As DataColumn = New DataColumn("Model", GetType(String))
            dataColumnMODEL.DefaultValue = CurrentModel.Text
            dt.Columns.Add(dataColumnMODEL)

            Dim dataColumn1LOTNO As DataColumn = New DataColumn("LotNo", GetType(String))
            dataColumn1LOTNO.DefaultValue = CurrentLot.Text
            dt.Columns.Add(dataColumn1LOTNO)

            Dim dataColumn1MODE1 As DataColumn = New DataColumn("Mode1", GetType(String))
            dataColumn1MODE1.DefaultValue = "ChangeLOT"
            dt.Columns.Add(dataColumn1MODE1)

            Dim dataColumnPROCESS As DataColumn = New DataColumn("PROCESS", GetType(String))
            dataColumnPROCESS.DefaultValue = PROCESS
            dt.Columns.Add(dataColumnPROCESS)

            Dim dataColumnUSERHIS As DataColumn = New DataColumn("USERID", GetType(String))
            dataColumnUSERHIS.DefaultValue = tbUser.Text
            dt.Columns.Add(dataColumnUSERHIS)

            sda.Fill(dt)

            ' ใช้ LINQ เพื่อเลือกข้อมูลล่าสุดสำหรับแต่ละ Feed

            'Dim latestFeedData = dt.AsEnumerable().GroupBy(Function(row) row.Field(Of String)("FEED")).
            'Select(Function(group) group.First()).CopyToDataTable()

            Dim latestFeedData = dt.AsEnumerable().GroupBy(Function(row) row.Field(Of String)("FEED")).
            Select(Function(group) group.OrderByDescending(Function(row) row.Field(Of String)("Date")).First()).CopyToDataTable()


            GridView11.DataSource = latestFeedData
            GridView11.DataBind()

            ViewState("MyDataTable") = latestFeedData
        End Using
    End Sub

    Private Sub Button1_ServerClick(sender As Object, e As EventArgs) Handles Button1.ServerClick

        Call load_GVDATA()

        tbUser.Text = String.Empty
        PNddlLinePD.Visible = False
        ddlLinePD.SelectedIndex.ToString()

        pnLine.Visible = False
        lbshowCode.InnerText = String.Empty
        lbshowFRONTENDLINE.InnerText = String.Empty
        lbshowTime.InnerText = String.Empty
        lbshowDATE.InnerText = String.Empty
        lbshowFRONTENDPD.InnerText = String.Empty
        lbshowLine.InnerText = String.Empty

        PanelMATERIAL.Visible = False
        ddlLine.SelectedIndex.ToString()
        PanetbModel.Visible = False
        GridViewPNMODEL.Visible = False
        Call load_clGVMODEL()
        PanetbLot.Visible = False
        Call load_clGVLOT()
        lbshowPreviousLOTNOPN.Visible = False
        lbshowPreviousLOTNO.InnerText = String.Empty
        CurrentModelPN.Visible = False
        CurrentModel.Text = String.Empty
        CurrentLotPN.Visible = False
        CurrentLot.Text = String.Empty

        PNddlMC.Visible = False
        ddlLineMC.SelectedIndex.ToString()
        PNPreviousCurrent.Visible = False
        PreviousModelLB.InnerText = String.Empty
        PreviousLotLB.InnerText = String.Empty
        LBCurrentModel.InnerText = String.Empty
        LBCurrentLot.InnerText = String.Empty
        PNBUT.Visible = False


    End Sub
    Private Sub load_GVDATA()
        Dim insideData As DataTable = CType(ViewState("MyDataTable"), DataTable)
        Dim connectionString As String = MT800DB
        Using connection As New OracleConnection(connectionString)
            connection.Open()
            For Each row As DataRow In insideData.Rows
                Dim sql As String = "INSERT INTO NVP_ALLPROCESS_HIS (USERNAME,DAY,TIME,MODEL,LOTNO,MATERIAL,INVOICE,MATERIALLOT,QTY,REELNO,LINE,PRODUCTS,MODE1,MACHINE,FEED,CASSETTE,COUNTER,PROCESS,USERID) VALUES (:sUSERNAME,:sDAY,:sTIME,:sMODEL,:sLOTNO,:sMATERIAL,:sINVOICE,:sMATERIALLOT,:sQTY,:sREELNO,:sLINE,:sPRODUCTS,:sMODE1,:sMACHINE,:sFEED,:sCASSETTE,:sCOUNTER,:sPROCESS,:sUSERID)"
                Using command As New OracleCommand(sql, connection)
                    'command.Parameters.Add(":suser", row("Code"))
                    'command.Parameters.Add(":sDAY", row("Date"))
                    'command.Parameters.Add(":sTIME", row("Time"))
                    'command.Parameters.Add(":sMODEL", row("Model"))
                    'command.Parameters.Add(":sLOTNO", row("LotNo"))
                    'command.Parameters.Add(":sMATERIAL", row("MATERIAL"))
                    'command.Parameters.Add(":sINVOICE", row("INVOICE"))
                    'command.Parameters.Add(":sMATERIALLOT", row("MATERIALLOT"))
                    'command.Parameters.Add(":sQTY", row("QTY"))
                    'command.Parameters.Add(":sREELNO", row("REELNO"))
                    'command.Parameters.Add(":sLINE", row("LINE"))
                    'command.Parameters.Add(":sPRODUCT", row("PRODUCT"))
                    'command.Parameters.Add(":sMODE1", row("MODE1"))
                    'command.Parameters.Add(":sMACHINE", row("MACHINE"))
                    'command.Parameters.Add(":sFEED", row("FEED"))
                    'command.Parameters.Add(":sCASSETTE", row("CASSETTE"))

                    command.Parameters.Add(":sUSERNAME", row("Name"))
                    command.Parameters.Add(":sDAY", row("Date"))
                    command.Parameters.Add(":sTIME", row("Time"))
                    command.Parameters.Add(":sMODEL", row("Model"))
                    command.Parameters.Add(":sLOTNO", row("LotNo"))
                    command.Parameters.Add(":sMATERIAL", row("MATERIAL"))
                    command.Parameters.Add(":sINVOICE", row("INVOICE"))
                    command.Parameters.Add(":sMATERIALLOT", row("MATERIALLOT"))
                    command.Parameters.Add(":sQTY", row("QTY"))
                    command.Parameters.Add(":sREELNO", row("REELNO"))
                    command.Parameters.Add(":sLINE", row("LINE"))
                    command.Parameters.Add(":sPRODUCTS", row("PRODUCTS"))
                    command.Parameters.Add(":sMODE1", row("MODE1"))
                    command.Parameters.Add(":sMACHINE", row("MACHINE"))
                    command.Parameters.Add(":sFEED", row("FEED"))
                    command.Parameters.Add(":sCASSETTE", row("CASSETTE"))
                    command.Parameters.Add(":sCOUNTER", row("COUNTER"))
                    command.Parameters.Add(":sPROCESS", row("PROCESS"))
                    command.Parameters.Add(":sUSERID", row("USERID"))
                    command.ExecuteNonQuery()
                End Using
            Next
            connection.Close()
        End Using
    End Sub

    Private Sub load_clgvnaja()
        Dim dtResult As New DataTable()
        dtResult.Columns.Add("Name", GetType(String))
        dtResult.Columns.Add("Date", GetType(String))
        dtResult.Columns.Add("Time", GetType(String))
        dtResult.Columns.Add("Model", GetType(String))
        dtResult.Columns.Add("LotNo", GetType(String))

        dtResult.Columns.Add("MATERIAL", GetType(String))
        dtResult.Columns.Add("INVOICE", GetType(String))
        dtResult.Columns.Add("MATERIALLOT", GetType(String))
        dtResult.Columns.Add("QTY", GetType(String))
        dtResult.Columns.Add("REELNO", GetType(String))


        dtResult.Columns.Add("LINE", GetType(String))
        dtResult.Columns.Add("PRODUCTS", GetType(String))
        dtResult.Columns.Add("MODE1", GetType(String))
        dtResult.Columns.Add("MACHINE", GetType(String))
        dtResult.Columns.Add("FEED", GetType(String))

        dtResult.Columns.Add("CASSETTE", GetType(String))
        dtResult.Columns.Add("COUNTER", GetType(String))
        dtResult.Columns.Add("PROCESS", GetType(String))
        dtResult.Columns.Add("USERID", GetType(String))
        GridView11.DataSource = dtResult
        GridView11.DataBind()
    End Sub



End Class
