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
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    'Dim DB As String = ""
    Dim DB As String = ""
    Dim MT800DB As String = ""
    Dim DWUSER As String = ""
    Dim PRASSDB As String = ""

    '---- Spread action variable
    Dim strDepartment As String = "MT800"
    Dim strFactory As String = "17SA10"
    Dim strProductDF As String = "TKANTC"
    '----


    Private lnkPstList As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Page.Form.Attributes.Add("enctype", "multipart/form-data")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "none", "<script>$('#showAnnounce').modal('show');</script>", False)
            ddlAnnSection.Items.Insert(0, New ListItem("SECTION", String.Empty))
            ddlAnnLossCd.Items.Insert(0, New ListItem("LOSS-CODE", String.Empty))
            pnPstResp.Visible = False
            pnPstSupp.Visible = False
            pnPstFin.Visible = False
            'pnPstReport.Visible = False
            pnPstCancelOrder.Visible = False
            'pnPstReportSearch.Visible = True
            'lbPstReportSearch.Text = String.Empty
            AnnRdoProblem.Visible = False
            ddlMcAlarmCode1.Visible = False
            ddlMcDefectCode1.Visible = False
            tbAnnRemark.Visible = False

        End If

        'MsgBox(Session("ProblemMC"))

    End Sub
    'Protected Sub Page_init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
    'pnAnnMcId.Visible = True
    'pnAnnMcResult.Visible = False
    'tbAnnMcQr.Text = String.Empty : If Me.IsPostBack Then tbAnnMcQr.Attributes("value") = tbAnnMcQr.Text
    'tbAnnMc.Text = String.Empty
    'tbAnnBlock.Text = String.Empty
    'lbAnnMc.Text = String.Empty
    'lbAnnDep.Text = String.Empty
    'lbAnnFact.Text = String.Empty
    'lbAnnEvent.Text = String.Empty
    'ddlAnnSection.Items.Clear()
    'ddlAnnSection.Items.Insert(0, New ListItem("SECTION", String.Empty))
    'ddlAnnLossCd.Items.Clear()
    'ddlAnnLossCd.Items.Insert(0, New ListItem("LOSS-CODE", String.Empty))
    'Call load_gvEvent()
    'tbAnnRemark.Text = String.Empty
    'End Sub()
    '------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    '------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    '---------- Prompt Service Team (Passengers) ----------
    Protected Sub tbAnnMcQr_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbAnnMcQr.TextChanged
        If Me.IsPostBack Then tbAnnMcQr.Attributes("value") = tbAnnMcQr.Text
        lbAnnMc.Text = String.Empty
        tbAnnMc.Text = String.Empty
        tbAnnBlock.Text = String.Empty
        lbAnnEvent.Text = String.Empty
        Dim conn As New OracleConnection(DB)
        Dim cmd As New OracleCommand("select MACHINE,DEPARTMENT,FACTORY from PSTM0001 where MACHINEID='" + tbAnnMcQr.Text + "'", conn)
        conn.Open()
        Dim reader As OracleDataReader = cmd.ExecuteReader()
        Try
            While reader.Read()
                If IsDBNull(reader.GetValue(0)) = True Then lbAnnMc.Text = String.Empty Else lbAnnMc.Text = CStr(reader.GetValue(0)).Replace("&nbsp;", String.Empty)
                If IsDBNull(reader.GetValue(1)) = True Then lbAnnDep.Text = String.Empty Else lbAnnDep.Text = CStr(reader.GetValue(1)).Replace("&nbsp;", String.Empty)
                If IsDBNull(reader.GetValue(2)) = True Then lbAnnFact.Text = String.Empty Else lbAnnFact.Text = CStr(reader.GetValue(2)).Replace("&nbsp;", String.Empty)
            End While
        Finally
            reader.Close()
        End Try
        Select Case lbAnnMc.Text
            Case String.Empty
                tbAnnMcQr.Text = String.Empty : If Me.IsPostBack Then tbAnnMcQr.Attributes("value") = tbAnnMcQr.Text
                pnAnnMcId.Visible = True
                pnAnnMcResult.Visible = False
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alertMeaasge", "alert('ไม่พบข้อมูล QR นี้\nกรุณาตรวจสอบภาษาที่ใช้\nหรือติดต่อผู้ดูแลระบบ')", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "none", "<script>$('#showAnnounce').modal('show');</script>", False)
                Exit Sub
            Case Else
                pnAnnMcId.Visible = False
                pnAnnMcResult.Visible = True
                Dim arr() As String = lbAnnMc.Text.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
                Select Case CInt(arr.Length.ToString)
                    Case 1
                        tbAnnMc.Text = arr(0).ToString
                        tbAnnBlock.Text = "-"
                    Case 2
                        tbAnnMc.Text = arr(0).ToString
                        tbAnnBlock.Text = arr(1).ToString
                End Select
                Call load_ddlAnnSection()
                Call load_ddlAnnLossCd()
                Call load_gvEvent()

                'B'Boss test new annount project
                Session("AnnMcName") = arr(1)
                Dim arr2() As String = Session("AnnMcName").Split({"."}, StringSplitOptions.RemoveEmptyEntries)
                Session("AnnMcName2") = arr2(0)


                '-----------------------------------Add test new annount project--------------------------------------------------------------------------------
                Call AnnDdlMcAlmCode()
                Call ddlMcDefectCode()

                '-----------------------------------------------------------------------------------------------------------------------------------------------

        End Select
        ddlMcAlarmCode1.Visible = False
        ddlMcDefectCode1.Visible = False

    End Sub
    Private Sub load_ddlAnnSection()
        ddlAnnSection.Items.Clear()
        Dim connect As New OracleConnection(DB)
        Dim command As New OracleCommand()
        Dim acc As String = "select SECTION"
        acc += " from PSTM0001"
        acc += " where DEPARTMENT is not null"
        Select Case lbAnnMc.Text
            Case String.Empty : acc += " and DEPARTMENT = 'XXXXX'"
            Case Else
                acc += " and DEPARTMENT = '" + strDepartment + "'  and FACTORY = '" + strFactory + "'"
                acc += " and SECTION is not null"
        End Select
        acc += " group by SECTION"
        acc += " order by SECTION asc"
        command.CommandText = acc
        command.Connection = connect
        Using sda As New OracleDataAdapter(command)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlAnnSection.DataSource = dt
            ddlAnnSection.DataValueField = "SECTION"
            ddlAnnSection.DataTextField = "SECTION"
            ddlAnnSection.DataBind()
        End Using
        For i As Integer = 0 To ddlAnnSection.Items.Count - 1
            Select Case Right(ddlAnnSection.Items(i).Value, 2)
                Case "70" : ddlAnnSection.SelectedIndex = i : Exit For
            End Select
        Next
    End Sub
    Protected Sub ddlAnnSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAnnSection.SelectedIndexChanged
        Call load_ddlAnnLossCd()
        Call load_gvEvent()
    End Sub
    Private Sub load_ddlAnnLossCd()
        ddlAnnLossCd.Items.Clear()
        Dim connect As New OracleConnection(DB)
        Dim command As New OracleCommand()
        Dim acc As String = "select DISTINCT LOSSCODE "
        acc += ",LOSSCODE || ' : ' || MEAN as LC"
        acc += ",LOSSCODE || '|' || case when ANNOUNCE is null then '-' else ANNOUNCE end as LA"
        acc += " from PSTM0001"
        acc += " where DEPARTMENT is not null"
        Select Case lbAnnMc.Text
            Case String.Empty : acc += " and DEPARTMENT = 'XXXXX'"
            Case Else
                acc += " and DEPARTMENT = '" + strDepartment + "'  and FACTORY = '" + strFactory + "'"
                acc += " and SECTION='" + ddlAnnSection.SelectedItem.Value + "'"
                acc += " and MACHINE='" + lbAnnMc.Text + "'"
                acc += " and LOSSCODE is not null"
                acc += " and SPECIAL is null"
        End Select
        acc += " order by LOSSCODE asc"
        command.CommandText = acc
        command.Connection = connect
        Using sda As New OracleDataAdapter(command)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlAnnLossCd.DataSource = dt
            ddlAnnLossCd.DataValueField = "LA"
            ddlAnnLossCd.DataTextField = "LC"
            ddlAnnLossCd.DataBind()
        End Using
        For i As Integer = 0 To ddlAnnLossCd.Items.Count - 1
            Dim arr() As String = ddlAnnLossCd.Items(i).Value.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
            Select Case arr(0)
                Case "WR"
                    tbAnnRemark.Visible = False
                    AnnRdoProblem.Visible = True
                    ddlMcAlarmCode1.Visible = False
                    ddlMcDefectCode1.Visible = False
                    ddlAnnLossCd.SelectedIndex = i : Exit For
            End Select
        Next
        'ddlAnnLossCd.Items.Insert(0, New ListItem("LOSS-CODE", String.Empty))
        'ddlAnnLossCd.SelectedIndex = 0
    End Sub
    Private Sub ddlAnnLossCd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAnnLossCd.SelectedIndexChanged
        btnAnnEnter.Visible = False
        Call load_gvEvent()
        Call load_RdoProblem()
        'Call load_tbAnnRemark()
    End Sub
    'Private Sub load_tbAnnRemark()
    '    Select Case ddlAnnLossCd.SelectedValue = "PM|-"
    '        Case True
    '            tbAnnRemark.Visible = True
    '            tbAnnRemark.Text = String.Empty
    '        Case False
    '            tbAnnRemark.Visible = False
    '    End Select
    'End Sub
    Protected Sub gvEvent_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvEvent.SelectedIndexChanged

        btnAnnEnter.Visible = True
        AnnRdoProblem.Visible = False
        AnnRdoProblem.ClearSelection()
        ddlMcAlarmCode1.Visible = False
        ddlMcDefectCode1.Visible = False
        tbAnnRemark.Visible = False

    End Sub

    Private Sub load_RdoProblem()
        Select Case ddlAnnLossCd.SelectedValue = "WR|-"
            Case True
                AnnRdoProblem.Visible = True
                ddlMcAlarmCode1.Visible = False
                ddlMcDefectCode1.Visible = False
                AnnRdoProblem.ClearSelection()
                tbAnnRemark.Visible = False
            Case False
                AnnRdoProblem.Visible = False
                ddlMcAlarmCode1.Visible = False
                ddlMcDefectCode1.Visible = False
                tbAnnRemark.Visible = True
        End Select
    End Sub
    Protected Sub AnnRdoProblem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AnnRdoProblem.SelectedIndexChanged
        'B'Boss test new annount project
        Dim strMC As String = String.Empty
        Dim connstrMC As New OracleConnection(MT800DB)
        Dim cmdstrMC As New OracleCommand()
        Dim accstrMC As String = "select * from ALARMTKANTF where MACHINE = '" & Session("AnnMcName2") & "'"
        cmdstrMC.CommandText = accstrMC
        cmdstrMC.Connection = connstrMC
        connstrMC.Open()
        Dim rd As OracleDataReader = cmdstrMC.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strMC = String.Empty Else strMC = CStr(rd.GetValue(0))
            End While
        Finally
            rd.Close()
        End Try

        '-----------------------------------Add test new annount project--------------------------------------------------------------------------------
        Select Case AnnRdoProblem.SelectedIndex
            Case 0
                Select Case strMC = Session("AnnMcName2")
                    Case True
                        tbAnnRemark.Visible = False
                        ddlMcAlarmCode1.Visible = True
                        ddlMcDefectCode1.Visible = False
                        ddlMcAlarmCode1.SelectedIndex = 0
                        ddlMcDefectCode1.SelectedIndex = 0
                        tbAnnRemark.Text = String.Empty

                    Case False
                        tbAnnRemark.Visible = True
                        ddlMcAlarmCode1.Visible = False
                        ddlMcDefectCode1.Visible = False
                        'ddlMcAlarmCode1.SelectedIndex = 0
                        'ddlMcDefectCode1.SelectedIndex = 0
                        tbAnnRemark.Text = String.Empty
                End Select
            Case 1
                Select Case strMC = Session("AnnMcName2")
                    Case True
                        tbAnnRemark.Visible = False
                        ddlMcAlarmCode1.Visible = False
                        ddlMcDefectCode1.Visible = True
                        ddlMcAlarmCode1.SelectedIndex = 0
                        ddlMcDefectCode1.SelectedIndex = 0
                        tbAnnRemark.Text = String.Empty
                    Case False
                        tbAnnRemark.Visible = False
                        ddlMcAlarmCode1.Visible = False
                        ddlMcDefectCode1.Visible = True
                        'ddlMcAlarmCode1.SelectedIndex = 0
                        'ddlMcDefectCode1.SelectedIndex = 0
                        tbAnnRemark.Text = String.Empty
                End Select
            Case Else
                Select Case strMC = Session("AnnMcName2")
                    Case True
                        tbAnnRemark.Visible = True
                        ddlMcAlarmCode1.Visible = False
                        ddlMcDefectCode1.Visible = False
                        ddlMcAlarmCode1.SelectedIndex = 0
                        ddlMcDefectCode1.SelectedIndex = 0
                        tbAnnRemark.Text = String.Empty
                    Case False
                        tbAnnRemark.Visible = True
                        ddlMcAlarmCode1.Visible = False
                        ddlMcDefectCode1.Visible = False
                        'ddlMcAlarmCode1.SelectedIndex = 0
                        'ddlMcDefectCode1.SelectedIndex = 0
                        tbAnnRemark.Text = String.Empty
                End Select
        End Select
        '------------------------------------------------------------------------------------------------------------------------------------------------
    End Sub
    '-----------------------------------Add test new annount project 8/4/2022--------------------------------------------------------------------------------
    Protected Sub tbAnnRemark_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbAnnRemark.TextChanged
        Select Case tbAnnRemark.Text = ""
            Case True
                btnAnnEnter.Visible = False
            Case False
                btnAnnEnter.Visible = True
        End Select
    End Sub
    Protected Sub ddlMcDefectCode1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMcDefectCode1.SelectedIndexChanged
        Select Case ddlMcDefectCode1.SelectedValue = ""
            Case True
                btnAnnEnter.Visible = False
            Case False
                btnAnnEnter.Visible = True
        End Select
    End Sub
    Protected Sub ddlMcAlarmCode1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMcAlarmCode1.SelectedIndexChanged
        Select Case ddlMcAlarmCode1.SelectedValue = ""
            Case True
                btnAnnEnter.Visible = False
            Case False
                btnAnnEnter.Visible = True
        End Select
    End Sub
    '------------------------------------------------------------------------------------------------------------------------------------------------
    Private Sub AnnDdlMcAlmCode()

        Timer1.Enabled = False

        'B'Boss test new annount project
        Dim strMC1 As String = String.Empty
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select * from ALARMTKANTF where MACHINE = '" & Session("AnnMcName2") & "'order by ALARMCODE"
        cmd.CommandText = acc
        cmd.Connection = conn
        conn.Open()
        Dim rd As OracleDataReader = cmd.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strMC1 = String.Empty Else strMC1 = CStr(rd.GetValue(0))
            End While
        Finally
            rd.Close()
        End Try
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlMcAlarmCode1.DataSource = dt
            ddlMcAlarmCode1.DataTextField = "ALARMCODE"
            ddlMcAlarmCode1.DataValueField = "ALARMCODE"
            ddlMcAlarmCode1.DataBind()

        End Using
        Select Case strMC1 = Session("AnnMcName2")
            Case True
                tbAnnRemark.Visible = False
                ddlMcAlarmCode1.Visible = True
                ddlMcAlarmCode1.Items.Insert(0, New ListItem("(กรุณาเลือก Code)", String.Empty))
            Case False
                If ddlAnnLossCd.SelectedValue <> "WR|-" Then
                    ddlMcAlarmCode1.Visible = False
                    tbAnnRemark.Visible = True
                End If
        End Select
    End Sub
    Private Sub ddlMcDefectCode()
        '-----------------------------------Add test new annount project--------------------------------------------------------------------------------
        Timer1.Enabled = False

        Dim conn As New OracleConnection(PRASSDB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select DEF_MODE From DEFECTIVE_MT800 where PRODUCT_NAME = '" + strProductDF + "' group by DEF_MODE having DEF_MODE != '-' order by DEF_MODE asc "
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlMcDefectCode1.DataSource = dt
            ddlMcDefectCode1.DataTextField = "DEF_MODE"
            ddlMcDefectCode1.DataValueField = "DEF_MODE"
            ddlMcDefectCode1.DataBind()
            ddlMcDefectCode1.Items.Insert(0, New ListItem("(กรุณาเลือก Code)", String.Empty))
        End Using
        '------------------------------------------------------------------------------------------------------------------------------------------------
    End Sub

    Private Sub load_gvEvent()
        Try
            Dim connect As New OracleConnection(DB)
            Dim cmd As New OracleCommand()
            Dim acc As String = "select EVENT"
            acc += " from PSTM0001"
            acc += " where EVENT is not null"
            Select Case lbAnnMc.Text
                Case String.Empty
                    acc += " and MACHINE='XXXXXXXXXX'"
                Case Else
                    acc += " and MACHINE='" + lbAnnMc.Text + "'"
                    Dim Sepa() As String = {"|"}
                    Dim str As String = ddlAnnLossCd.SelectedItem.Value
                    Dim arr() As String = str.Split(Sepa, StringSplitOptions.RemoveEmptyEntries)
                    acc += " and LOSSCODE='" + arr(0).ToString + "'"
            End Select
            acc += " order by EVENT asc"
            cmd.CommandText = acc
            cmd.Connection = connect
            Using sda As New OracleDataAdapter(cmd)
                Dim dt As New DataTable()
                sda.Fill(dt)
                gvEvent.DataSource = dt
                gvEvent.DataBind()
            End Using
            Select Case CInt(gvEvent.Rows.Count)
                Case 0 'nothing
                Case > 0
                    For i As Integer = 0 To gvEvent.Rows.Count - 1
                        Select Case i Mod 2
                            Case 0 : gvEvent.Rows(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#EAECEE")
                        End Select
                    Next
            End Select
        Catch ex As Exception 'nothing

        End Try
    End Sub
    Protected Sub EventRowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        lbAnnEvent.Text = String.Empty
        Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
        Dim row As GridViewRow = gvEvent.Rows(rowIndex)
        lbAnnEvent.Text = row.Cells(1).Text
        For i As Integer = 0 To gvEvent.Rows.Count - 1
            Select Case gvEvent.Rows(i).Cells(1).Text
                Case lbAnnEvent.Text
                    gvEvent.Rows(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#525B5C")
                    gvEvent.Rows(i).ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFF")
                Case Else
                    gvEvent.Rows(i).ForeColor = System.Drawing.ColorTranslator.FromHtml("#000")
                    Select Case i Mod 2
                        Case 0 : gvEvent.Rows(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#EAECEE")
                        Case Else : gvEvent.Rows(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF")
                    End Select
            End Select
        Next
    End Sub
    Private Sub btnAnnEnter_ServerClick(sender As Object, e As EventArgs) Handles btnAnnEnter.ServerClick
        Timer1.Enabled = True
        If lbAnnMc.Text = String.Empty Then Exit Sub
        Dim arr() As String = ddlAnnLossCd.SelectedItem.Value.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
        Dim chk As String
        Dim conn As New OracleConnection(DB)
        Dim cmd As New OracleCommand("select ROWID from PSTH0001 where DEPARTMENT='" + lbAnnDep.Text + "' and FACTORY='" + lbAnnFact.Text + "' and SECTION='" + ddlAnnSection.SelectedItem.Value + "' and MACHINE='" + lbAnnMc.Text + "' and LOSSCODE='" + arr(0) + "' and PSTFIN is null", conn)
        conn.Open()
        Dim reader As OracleDataReader = cmd.ExecuteReader()
        Try
            While reader.Read()
                If IsDBNull(reader.GetValue(0)) = True Then chk = String.Empty Else chk = CStr(reader.GetValue(0)).Replace("&nbsp;", String.Empty)
            End While
        Finally
            reader.Close()
        End Try
        Select Case chk
            Case String.Empty 'nothing
            Case Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alertMeaasge", "alert('ระบบพบข้อมูล\nDepartment : " + lbAnnDep.Text + ", Factory : " + lbAnnFact.Text + "\nMachine : " + lbAnnMc.Text + ", Loss code : " + arr(0) + "\nกรุณาตรวจสอบ')", True)
                pnAnnMcId.Visible = True
                pnAnnMcResult.Visible = False
                tbAnnMcQr.Text = String.Empty : If Me.IsPostBack Then tbAnnMcQr.Attributes("value") = tbAnnMcQr.Text
                tbAnnMc.Text = String.Empty
                tbAnnBlock.Text = String.Empty
                lbAnnMc.Text = String.Empty
                lbAnnDep.Text = String.Empty
                lbAnnFact.Text = String.Empty
                lbAnnEvent.Text = String.Empty
                ddlAnnSection.Items.Clear()
                ddlAnnSection.Items.Insert(0, New ListItem("SECTION", String.Empty))
                ddlAnnLossCd.Items.Clear()
                ddlAnnLossCd.Items.Insert(0, New ListItem("LOSS-CODE", String.Empty))

                tbAnnRemark.Visible = False
                AnnRdoProblem.Visible = False
                ddlMcAlarmCode1.Visible = False
                ddlMcDefectCode1.Visible = False
                AnnRdoProblem.ClearSelection()

                Call load_gvEvent()
                tbAnnRemark.Text = String.Empty
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "none", "<script>$('#showAnnounce').modal('show');</script>", False)
                Exit Sub
        End Select
        Dim objConn As New OracleConnection
        Dim strConnString As String = DB
        Dim strSQL As String = "insert into PSTH0001(DEPARTMENT,FACTORY,SECTION,MACHINE,LOSSCODE,EVENT,TIMEREQ)values(:sDep,:sFact,:sSection,:sMc,:sLossCd,:sEvent,:sTimeReq)"
        objConn.ConnectionString = strConnString
        objConn.Open()
        Dim objCmd As New OracleCommand(strSQL, objConn)
        objCmd.Parameters.Add("@sDep", lbAnnDep.Text)
        objCmd.Parameters.Add("@sFact", lbAnnFact.Text)
        objCmd.Parameters.Add("@sSection", ddlAnnSection.SelectedItem.Value)
        objCmd.Parameters.Add("@sMc", lbAnnMc.Text)
        objCmd.Parameters.Add("@sLossCd", arr(0))


        '-----------------------------------Edit test new annount project-------------------------------------------------------------------------------
        'Select Case tbAnnRemark.Text
        '    Case String.Empty : objCmd.Parameters.Add("@sEvent", lbAnnEvent.Text)
        '    Case Else : objCmd.Parameters.Add("@sEvent", lbAnnEvent.Text & "|" & tbAnnRemark.Text)
        'End Select
        '------------------------------------------------------------------------------------------------------------------------------------------------


        '-----------------------------------Add test new annount project-------------------------------------------------------------------------------
        Dim strMC As String = String.Empty
        Dim connstrMC As New OracleConnection(MT800DB)
        Dim cmdstrMC As New OracleCommand()
        Dim accstrMC As String = "select * from ALARMTKANTF where MACHINE = '" & Session("AnnMcName2") & "'"
        cmdstrMC.CommandText = accstrMC
        cmdstrMC.Connection = connstrMC
        connstrMC.Open()
        Dim rd As OracleDataReader = cmdstrMC.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strMC = String.Empty Else strMC = CStr(rd.GetValue(0))
            End While
        Finally
            rd.Close()
        End Try


        Select Case AnnRdoProblem.SelectedIndex
            Case 0
                Select Case strMC = Session("AnnMcName2")
                    Case True
                        objCmd.Parameters.Add("@sEvent", "เครื่อง Alarm " & "|" & ddlMcAlarmCode1.SelectedItem.Text)
                    Case False
                        objCmd.Parameters.Add("@sEvent", "เครื่อง Alarm " & "|" & tbAnnRemark.Text)
                End Select
            Case 1
                Select Case objCmd.Parameters.Add("@sEvent", "เกิดงานเสีย Defective Mode " & "|" & ddlMcDefectCode1.SelectedItem.Text)
                End Select
            Case 2
                Select Case objCmd.Parameters.Add("@sEvent", "เครื่องค้างไม่อลาม" & "|" & tbAnnRemark.Text)
                End Select
            Case Else

                'แก้ไข EVENT ไม่ขึ้น
                Select Case tbAnnRemark.Text
                    Case String.Empty : objCmd.Parameters.Add("@sEvent", "Event" & "|" & lbAnnEvent.Text)
                    Case Else : objCmd.Parameters.Add("@sEvent", tbAnnRemark.Text)
                End Select

                'Select Case objCmd.Parameters.Add("@sEvent",  tbAnnRemark.Text)
                'End Select

        End Select
        objCmd.Parameters.Add("@sTimeReq", CStr(Format(Now, "g")))
        objCmd.ExecuteNonQuery()
        objConn.Close()
        objConn = Nothing
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alertMeaasge", "alert('ส่งข้อมูลเรียบร้อย\nขอบคุณครับ')", True)
        pnAnnMcId.Visible = True
        pnAnnMcResult.Visible = False
        tbAnnMcQr.Text = String.Empty : If Me.IsPostBack Then tbAnnMcQr.Attributes("value") = tbAnnMcQr.Text
        tbAnnMc.Text = String.Empty
        tbAnnBlock.Text = String.Empty
        lbAnnMc.Text = String.Empty
        lbAnnDep.Text = String.Empty
        lbAnnFact.Text = String.Empty
        lbAnnEvent.Text = String.Empty
        ddlAnnSection.Items.Clear()
        ddlAnnSection.Items.Insert(0, New ListItem("SECTION", String.Empty))
        ddlAnnLossCd.Items.Clear()
        ddlAnnLossCd.Items.Insert(0, New ListItem("LOSS-CODE", String.Empty))
        Call load_gvEvent()
        tbAnnRemark.Text = String.Empty


        '-----------------------------------Add test new annount project--------------------------------------------------------------------------------
        AnnRdoProblem.Visible = False
        ddlMcAlarmCode1.Visible = False
        ddlMcDefectCode1.Visible = False
        AnnRdoProblem.ClearSelection()
        '------------------------------------------------------------------------------------------------------------------------------------------------



        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "none", "<script>$('#showAnnounce').modal('show');</script>", False)


    End Sub
    Private Sub btnCloseAnn_ServerClick(sender As Object, e As EventArgs) Handles btnCloseAnn.ServerClick
        pnAnnMcId.Visible = True
        tbAnnMcQr.Text = String.Empty : If Me.IsPostBack Then tbAnnMcQr.Attributes("value") = tbAnnMcQr.Text
        pnAnnMcResult.Visible = False
        tbAnnMc.Text = String.Empty
        tbAnnBlock.Text = String.Empty
        lbAnnMc.Text = String.Empty
        lbAnnDep.Text = String.Empty
        lbAnnFact.Text = String.Empty
        ddlAnnSection.Items.Clear()
        ddlAnnSection.Items.Insert(0, New ListItem("SECTION", String.Empty))
        ddlAnnLossCd.Items.Clear()
        ddlAnnLossCd.Items.Insert(0, New ListItem("LOSS-CODE", String.Empty))
        tbAnnRemark.Text = String.Empty

    End Sub
    '------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    '------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    '---------- Prompt Service Team ----------
    Private Sub btnShowPst_Load(sender As Object, e As EventArgs) Handles btnShowPst.Load
        Call load_gvPstStation()
        Call load_gvPst()
        Call PstProcess()
        '-----------------------------------------------
        'Call load_gvPstReport()
    End Sub
    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick '***** Loop 10 sec. *****
        Call load_gvPstStation()
        Call load_gvPst()
        Call PstProcess()
        '-----------------------------------------------
        'Call load_gvPstReport()
    End Sub
    Private Sub load_gvPstStation()
        Dim dte As DateTime = Today.AddHours(7)
        If CInt(Format(Now, "HHmm")) < 700 Then dte = Today.AddDays(-1).AddHours(19)
        If CInt(Format(Now, "HHmm")) >= 1900 Then dte = Today.AddHours(19)
        Dim weekOfyear = DateTimeFormatInfo.CurrentInfo.Calendar.GetWeekOfYear(DateTime.Today.AddYears(-1), DateTimeFormatInfo.CurrentInfo.CalendarWeekRule, DayOfWeek.Sunday) - 1
        Dim conn As New OracleConnection(DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select to_char(to_date(T1.DTE_LOGIN,'MM/DD/YYYY HH:MI AM'),'HH24:MI') as DTE"
        acc += ",substr(T1.EMPNAME, 1,instr(T1.EMPNAME,' ') - 1) || '.' || substr(T1.EMPNAME,instr(T1.EMPNAME,' ') + 1,3)"
        acc += " || ' [' || case when T1.POSITION='CHIEF' then 'CHI' when T1.POSITION='SUB.MAINTENANCE' then 'SMN' when T1.POSITION='MAINTENANCE' then 'MN' end || ']'"
        acc += " || '|' || case when T1.STATUS is null then 'OUT' else T1.STATUS end"
        acc += " || '|' || T1.EMPNAME as NAME"
        acc += " from PSTM0002 T1"
        acc += " where T1.DEPARTMENT='" + strDepartment + "' and T1.FACTORY='" + strFactory + "'"
        Select Case CInt(Format(Now, "HHmm"))
            Case 700 To 1859
                Dim day As String
                Select Case CInt(weekOfyear)
                    Case 0, 1, 4, 5, 8, 9, 12, 13, 16, 17, 20, 21, 24, 25, 28, 29, 32, 33, 36, 37, 40, 41, 44, 45, 48, 49, 52 : day = "A"
                    Case 2, 3, 6, 7, 10, 11, 14, 15, 18, 19, 22, 23, 26, 27, 30, 31, 34, 35, 38, 39, 42, 43, 46, 47, 50, 51 : day = "B"
                End Select
                acc += " and T1.SHIFT in('D','" + day + "')"
            Case Is >= 1900, Is <= 659
                Dim night As String
                Select Case CInt(weekOfyear)
                    Case 0, 1, 4, 5, 8, 9, 12, 13, 16, 17, 20, 21, 24, 25, 28, 29, 32, 33, 36, 37, 40, 41, 44, 45, 48, 49, 52 : night = "B"
                    Case 2, 3, 6, 7, 10, 11, 14, 15, 18, 19, 22, 23, 26, 27, 30, 31, 34, 35, 38, 39, 42, 43, 46, 47, 50, 51 : night = "A"
                End Select
                acc += " and T1.SHIFT in('" + night + "')"
            Case Else
                acc += " and T1.SHIFT='nothing'"
        End Select
        acc += " order by T1.STATUS asc, T1.POSITION desc, to_number(T1.EMPCODE) asc"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvPstStation.DataSource = dt
            gvPstStation.DataBind()
        End Using
        acc = "select T1.MACHINE || '+' || case when T1.EVENT is null then '-' else T1.EVENT end as DETAIL"
        acc += ",to_char(to_date(T1.TIMEREQ,'MM/DD/YYYY HH:MI AM'),'HH24:MI,DD-MON-YY') as DTE"
        acc += ",T1.LOSSCODE"
        acc += ",T2.TIMER"
        acc += ",case when T1.PSTSELE is null then '-' else T1.PSTSELE end"
        acc += " || '+' || case when T1.PSTRESP is null then '-' else T1.PSTRESP end"
        acc += " || '+' || case when T1.PSTSUPP is null then '-' else T1.PSTSUPP end as NAME"
        acc += ",T3.IMGPST"
        acc += " from PSTH0001 T1"
        acc += " left join (Select MACHINE,LOSSCODE,case when EVENT is null then '-' else EVENT end as ENT,TIMEREQ"
        acc += ",to_char(sum(round((to_date(to_char(SYSDATE,'MM/DD/YYYY HH:MI AM'),'MM/DD/YYYY HH:MI AM') - to_date(TIMEREQ,'MM/DD/YYYY HH:MI AM')) * (60 * 24),2)),'99,999') as TIMER"
        acc += " from PSTH0001"
        acc += " where PSTFIN is null"
        acc += " group by MACHINE,LOSSCODE,EVENT,TIMEREQ) T2"
        acc += " on T1.MACHINE=T2.MACHINE and T1.LOSSCODE=T2.LOSSCODE and T1.TIMEREQ=T2.TIMEREQ"
        acc += " left join PSTM0002 T3 on case when T1.PSTRESP is null then T1.PSTSELE else T1.PSTRESP end =T3.EMPNAME"
        acc += " where T1.DEPARTMENT='" + strDepartment + "' and T1.FACTORY='" + strFactory + "'"
        acc += " and T1.PSTFIN is null"
        acc += " order by to_date(T1.TIMEREQ,'MM/DD/YYYY HH:MI AM') desc"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvPstStationChk.DataSource = dt
            gvPstStationChk.DataBind()
        End Using
    End Sub
    Private Sub load_gvPst()
        Dim conn As New OracleConnection(DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select to_char(to_date(T1.TIMEREQ,'MM/DD/YYYY HH:MI AM'),'DD-MON-YY, HH24:MI')"
        acc += " || '~' || T1.MACHINE"
        acc += " || '~' || T1.LOSSCODE"
        acc += " || '~' || case when T1.EVENT is null then '-' else T1.EVENT end"
        acc += " || '!' || T2.TIMER"
        acc += " || '!' || case when T1.PSTSELE is null then '-' else substr(T1.PSTSELE, 1,instr(T1.PSTSELE,' ') - 1) || '.' || substr(T1.PSTSELE,instr(T1.PSTSELE,' ') + 1,3) end"
        acc += " || '~' || case when T1.PSTRESP is null then '-' else substr(T1.PSTRESP, 1,instr(T1.PSTRESP,' ') - 1) || '.' || substr(T1.PSTRESP,instr(T1.PSTRESP,' ') + 1,3) end"
        acc += " || '~' || case when T1.PSTSUPP is null then '-' else T1.PSTSUPP end"
        acc += " || '~' || case when T1.PSTRESP is null then '-' else T1.PSTRESP end"
        acc += " || '~' || case when T1.TIMESUPP_2 is null then '-' else T1.TIMESUPP_2 end"
        acc += " || '~' || T1.ROWID as DETAIL"
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        acc += ", T3.IMGPST"
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        acc += " from PSTH0001 T1"
        acc += " left join (Select MACHINE,LOSSCODE,case when EVENT Is null then '-' else EVENT end as ENT,TIMEREQ"
        acc += ",to_char(sum(round((to_date(to_char(SYSDATE,'MM/DD/YYYY HH:MI AM'),'MM/DD/YYYY HH:MI AM')"
        acc += " - to_date(TIMEREQ,'MM/DD/YYYY HH:MI AM')) * (60 * 24),2)),'99,999') as TIMER"
        acc += " from PSTH0001"
        acc += " where PSTFIN is null"
        acc += " group by MACHINE,LOSSCODE,EVENT,TIMEREQ) T2"
        acc += " on T1.MACHINE=T2.MACHINE and T1.LOSSCODE=T2.LOSSCODE and T1.TIMEREQ=T2.TIMEREQ"
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        'acc += " full outer join PSTM0002 T3"
        'acc += " on case when T1.PSTRESP is null then T1.PSTSELE else T1.PSTRESP end=T3.EMPNAME"
        acc += " left join (select EMPNAME,IMGPST from PSTM0002 where DEPARTMENT='" + strDepartment + "' and FACTORY='" + strFactory + "') T3"
        acc += " On Case When T1.PSTRESP Is null Then T1.PSTSELE Else T1.PSTRESP End =T3.EMPNAME"
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        acc += " where T1.DEPARTMENT='" + strDepartment + "' and T1.FACTORY='" + strFactory + "'"
        acc += " and T1.PSTFIN is null"
        acc += " order by T1.PSTRESP nulls first,to_date(T1.TIMEREQ,'MM/DD/YYYY HH:MI AM') asc"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvPst.DataSource = dt
            gvPst.DataBind()
            gvPstCnt.DataSource = dt
            gvPstCnt.DataBind()
        End Using
    End Sub
    Protected Sub PstBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)



        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim bytes As Byte() = TryCast(TryCast(e.Row.DataItem, DataRowView)("IMGPST"), Byte())
            If bytes IsNot Nothing Then
                Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                TryCast(e.Row.FindControl("imgPst"), WebControls.Image).ImageUrl = Convert.ToString("data:image/png;base64,") & base64String
            End If
            On Error Resume Next
            Dim arrMain() As String = e.Row.Cells(0).Text.Split({"!"}, StringSplitOptions.RemoveEmptyEntries)
            Dim arr() As String = arrMain(0).Split({"~"}, StringSplitOptions.RemoveEmptyEntries)
            Dim strReq As String = arr(0).ToString
            Dim strMc As String = arr(1).ToString
            Dim strLossCd As String = arr(2).ToString
            Dim strEvent As String = arr(3).ToString
            e.Row.Cells(0).Text = "Req. : " & strReq
            arr = strMc.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
            Select Case CInt(arr.Length.ToString)
                Case 1 : e.Row.Cells(0).Text = e.Row.Cells(0).Text & "<br/><font color='blue'>" & arr(0) & "</font>"
                Case 2 : e.Row.Cells(0).Text = e.Row.Cells(0).Text & "<br/><font color='blue'>" & arr(0) & "(" & arr(1) & ")</font>"
            End Select
            e.Row.Cells(0).Text = e.Row.Cells(0).Text & "<br/>Code : " & strLossCd
            Select Case CInt(arrMain(1).Replace(",", String.Empty))
                Case >= 30 : e.Row.Cells(0).Text = e.Row.Cells(0).Text & ", Timer : <span class=" & "blinking>" & arrMain(1) & "</span><span class=" & "blinking> min</span>"
                Case Else : e.Row.Cells(0).Text = e.Row.Cells(0).Text & ", Timer : " & arrMain(1) & " min"
            End Select
            Select Case strEvent
                Case <> "-"
                    arr = strEvent.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
                    e.Row.Cells(0).Text = e.Row.Cells(0).Text & "<br/>Event : "
                    For ii As Integer = 0 To CInt(arr.Length.ToString) - 1
                        Select Case ii
                            Case 0 : e.Row.Cells(0).Text = e.Row.Cells(0).Text & arr(ii)
                            Case > 0 : e.Row.Cells(0).Text = e.Row.Cells(0).Text & ",&nbsp;" & arr(ii)
                        End Select
                    Next
            End Select
            arr = arrMain(2).Split({"~"}, StringSplitOptions.RemoveEmptyEntries)
            Dim btnLinkPstResp As LinkButton = e.Row.FindControl("lnkPstResp")
            Dim btnLinkPstSupp As LinkButton = e.Row.FindControl("lnkPstSupp")
            Dim btnLinkPstFin As LinkButton = e.Row.FindControl("lnkPstFin")
            Dim btnLinkPstCls As LinkButton = e.Row.FindControl("lnkPstCls")
            btnLinkPstResp.ForeColor = System.Drawing.Color.OrangeRed
            btnLinkPstSupp.ForeColor = System.Drawing.Color.Green
            btnLinkPstFin.ForeColor = System.Drawing.Color.Blue
            btnLinkPstCls.ForeColor = System.Drawing.Color.Black
            Select Case True
                Case arr(1) <> "-"
                    e.Row.Cells(0).Text = e.Row.Cells(0).Text & "</br><font style='font-size:0px'>|" & arr(5) & "|</font>Status : <font color='green'>SUPP</font>"
                    e.Row.Cells(0).Text = e.Row.Cells(0).Text & "</br>P.S.T. : <font color='blue'>" & arr(1) & "</font>"
                    btnLinkPstResp.Visible = False
                    btnLinkPstSupp.Visible = True
                    btnLinkPstFin.Visible = True
                    btnLinkPstCls.Visible = False
                Case arr(0) <> "-"
                    e.Row.Cells(0).Text = e.Row.Cells(0).Text & "</br><font style='font-size:0px'>|" & arr(5) & "|</font>Status : <span class=" & "blinking>WAIT</span>"
                    e.Row.Cells(0).Text = e.Row.Cells(0).Text & "</br>P.S.T. : <font color='blue'>" & arr(0) & "</font>"
                    btnLinkPstResp.Visible = True
                    btnLinkPstSupp.Visible = False
                    btnLinkPstFin.Visible = False
                    btnLinkPstCls.Visible = False
                Case Else
                    e.Row.Cells(0).Text = e.Row.Cells(0).Text & "</br><font style='font-size:0px'>|" & arr(5) & "|</font>Status : <span class=" & "blinking>WAIT</span>"
                    e.Row.Cells(0).Text = e.Row.Cells(0).Text & "</br>P.S.T. : " & String.Empty
                    btnLinkPstResp.Visible = True
                    btnLinkPstSupp.Visible = False
                    btnLinkPstFin.Visible = False
                    btnLinkPstCls.Visible = False
            End Select
        End If
    End Sub
    Private Sub PstProcess()
        Dim cntStation, cntBusy As Integer
        For i As Integer = 0 To gvPstStation.Rows.Count - 1
            Select Case i Mod 2
                Case 0 : gvPstStation.Rows(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#EAECEE")
            End Select
            Dim arr() As String = gvPstStation.Rows(i).Cells(1).Text.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
            gvPstStation.Rows(i).Cells(1).Text = arr(0)
            Select Case arr(1)
                Case "OUT"
                    gvPstStation.Rows(i).Cells(0).Text = gvPstStation.Rows(i).Cells(0).Text
                    gvPstStation.Rows(i).Cells(1).Text = gvPstStation.Rows(i).Cells(1).Text
                    gvPstStation.Rows(i).Cells(2).Text = gvPstStation.Rows(i).Cells(2).Text
                    gvPstStation.Rows(i).Cells(0).ForeColor = Drawing.Color.DimGray
                    gvPstStation.Rows(i).Cells(1).ForeColor = Drawing.Color.DimGray
                    gvPstStation.Rows(i).Cells(2).ForeColor = Drawing.Color.DimGray
                Case "IN"
                    cntStation = cntStation + 1
                    For ii As Integer = 0 To gvPstCnt.Rows.Count - 1
                        Dim arrPst() As String = gvPstCnt.Rows(ii).Cells(0).Text.Split({"~"}, StringSplitOptions.RemoveEmptyEntries)
                        Select Case arr(2) = arrPst(6)
                            Case True : cntBusy = cntBusy + 1
                        End Select
                        Try
                            Dim arrPstSupp() As String = arrPst(5).Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
                            For iii As Integer = 0 To CInt(arrPstSupp.Length.ToString) - 1 Step 3
                                If arrPstSupp(iii + 2) = "-" And arr(2) = arrPstSupp(iii) Then cntBusy = cntBusy + 1
                            Next
                        Catch ex As Exception
                        End Try
                    Next
                    For ii As Integer = 0 To gvPstStationChk.Rows.Count - 1
                        Dim arrMc() As String = gvPstStationChk.Rows(ii).Cells(0).Text.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
                        Dim arrChkPst() As String = gvPstStationChk.Rows(ii).Cells(4).Text.Split({"+"}, StringSplitOptions.RemoveEmptyEntries)
                        If arr(2) = arrChkPst(0) And arrChkPst(1) = "-" Then
                            gvPstStation.Rows(i).Cells(2).Text = "<span><i class='far fa-1x fa-clock'></i> " & arrMc(0) & "</span>"
                        End If
                        If arr(2) = arrChkPst(1) Then
                            gvPstStation.Rows(i).Cells(2).Text = "<span><i class='fas fa-1x fa-tools'></i> " & arrMc(0) & "</span>"
                            Exit For
                        End If
                    Next
                    For ii As Integer = 0 To gvPstStationChk.Rows.Count - 1
                        Dim arrMc() As String = gvPstStationChk.Rows(ii).Cells(0).Text.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
                        Dim arrChkPst() As String = gvPstStationChk.Rows(ii).Cells(4).Text.Split({"+"}, StringSplitOptions.RemoveEmptyEntries)
                        Try
                            Dim arrPstSupp() As String = arrChkPst(2).Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
                            For iii As Integer = 0 To CInt(arrPstSupp.Length.ToString) - 1 Step 3
                                If arrPstSupp(iii + 2) = "-" And arr(2) = arrPstSupp(iii) Then
                                    gvPstStation.Rows(i).Cells(2).Text = "<span><i class='fas fa-1x fa-hands-helping'></i> " & arrMc(0) & "</span>"
                                    Exit For
                                End If
                            Next
                        Catch ex As Exception
                            'Nothing
                        End Try
                    Next
            End Select
        Next
        lbAvaiPst.InnerText = CStr(cntStation - cntBusy)
        lbBusyPst.InnerText = CStr(cntBusy)
        Select Case CInt(gvPst.Rows.Count)
            Case 0  'Nothing
            Case > 0
                For i As Integer = 0 To gvPst.Rows.Count - 1
                    Select Case i Mod 2
                        Case 0 : gvPst.Rows(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#EAECEE")
                    End Select
                Next
        End Select
    End Sub
    Protected Sub PstListRowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        lbPstId.Text = String.Empty
        Dim strDte, strMc, strLossCd, strEvent, strPstSelect, strPstRespone, strPstSupport As String
        Dim row As GridViewRow = gvPst.Rows(Convert.ToInt32(e.CommandArgument))
        Dim arr() As String = row.Cells(0).Text.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
        Dim strId As String = arr(1)
        lbPstId.Text = strId
        Dim conn As New OracleConnection(DB)
        Dim cmd As New OracleCommand("select to_char(to_date(T1.TIMEREQ,'MM/DD/YYYY HH:MI AM'),'DD-MON-YY, HH24:MI'),T1.MACHINE,T1.LOSSCODE" +
                                    ",T2.MEAN,T1.EVENT,T1.PSTSELE,T1.PSTRESP,T1.PSTSUPP" +
                                    " from PSTH0001 T1" +
                                    " left join (select LOSSCODE, MEAN from PSTM0001 group by LOSSCODE,MEAN) T2 on T1.LOSSCODE=T2.LOSSCODE" +
                                    " where T1.ROWID='" & strId & "'", conn)
        conn.Open()
        Dim reader As OracleDataReader = cmd.ExecuteReader()
        Try
            While reader.Read()
                strDte = CStr(reader.GetValue(0))
                arr = CStr(reader.GetValue(1)).Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
                Select Case CInt(arr.Length.ToString)
                    Case 1 : strMc = arr(0)
                    Case 2 : strMc = arr(0) & " (" & arr(1) & ")"
                End Select
                strLossCd = CStr(reader.GetValue(2)) & " (" & CStr(reader.GetValue(3)) & ")"
                If IsDBNull(reader.GetValue(4)) = True Then
                    strEvent = String.Empty
                Else
                    arr = CStr(reader.GetValue(4)).Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
                    Select Case CInt(arr.Length.ToString)
                        Case 1 : strEvent = arr(0)
                        Case 2 : strEvent = arr(0) & ", " & arr(1)
                    End Select
                End If
                If IsDBNull(reader.GetValue(5)) = True Then strPstSelect = "-" Else strPstSelect = CStr(reader.GetValue(5))
                If IsDBNull(reader.GetValue(6)) = True Then strPstRespone = String.Empty Else strPstRespone = CStr(reader.GetValue(6))
                If IsDBNull(reader.GetValue(7)) = True Then strPstSupport = String.Empty Else strPstSupport = CStr(reader.GetValue(7))
            End While
        Finally
            reader.Close()
        End Try
        Select Case e.CommandName
            Case "Select" '----- Respond -----
                gvPst.Visible = False
                pnPstResp.Visible = True
                lbPstRespDte.InnerText = strDte
                lbPstRespMc.InnerText = strMc
                lbPstRespLossCd.InnerText = strLossCd
                lbPstRespEvent.InnerText = strEvent
                lbPstRespSelect.InnerText = strPstSelect
                lbChkPstResp.Visible = False
                Dim arrchkmc() As String = strMc.Split({" "}, StringSplitOptions.RemoveEmptyEntries)

                Session("arrchkmcLine") = arrchkmc(0)

                Dim i As Integer
                i = InStr(arrchkmc(1), "(")
                arrchkmc(1) = Trim(Mid(arrchkmc(1), i + 1, Len(arrchkmc(1))))

                i = Len(arrchkmc(1))
                arrchkmc(1) = Trim(Mid(arrchkmc(1), 1, i - 1))

                'Dim arrchkmc3() As String = arrchkmc(0).Split({" "}, StringSplitOptions.RemoveEmptyEntries)
                'Dim arrchkmc2() As String = arrchkmc(1).Split({")"}, StringSplitOptions.RemoveEmptyEntries)
                'Dim arrchkmcName As String = strMc.Split({"("}, StringSplitOptions.RemoveEmptyEntries)

                Session("chkMC") = Session("arrchkmcLine") & "_" & arrchkmc(1)

            Case "Select1" '----- Support -----
                gvPst.Visible = False
                pnPstSupp.Visible = True
                lbPstSuppDte.InnerText = strDte
                lbPstSuppMc.InnerText = strMc
                lbPstSuppLossCd.InnerText = strLossCd
                lbPstSuppEvent.InnerText = strEvent
                lbPstSuppSelect.InnerText = strPstSelect
                lbPstSuppResp.InnerText = strPstRespone
                Dim arrSupp() As String = strPstSupport.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To CInt(arrSupp.Length.ToString) - 1 Step 3
                    If arrSupp(i + 2) = "-" Then lbPstSupp.InnerText = lbPstSupp.InnerText & ", " & arrSupp(i)
                Next
                lbPstSupp.InnerText = Mid(lbPstSupp.InnerText, 3, Len(lbPstSupp.InnerText))
                lbPstSuppChk.InnerText = strPstSupport
                lbChkPstSupp.Visible = False
            Case "Select2" '----- Finish -----
                gvPst.Visible = False
                pnPstFin.Visible = True
                lbPstFinDte.InnerText = strDte
                lbPstFinMc.InnerText = strMc
                lbPstFinLossCd.InnerText = strLossCd
                lbPstFinEvent.InnerText = strEvent
                lbPstFinSelect.InnerText = strPstSelect
                lbPstFinResp.InnerText = strPstRespone
                lbChkPstFin.Visible = False
                Dim arrSupp() As String = strPstSupport.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To CInt(arrSupp.Length.ToString) - 1 Step 3
                    If arrSupp(i + 2) = "-" Then lbPstFinSupp.InnerText = lbPstFinSupp.InnerText & ", " & arrSupp(i)
                Next
                lbPstFinSupp.InnerText = Mid(lbPstFinSupp.InnerText, 3, Len(lbPstFinSupp.InnerText))

                arr = strLossCd.Split({"("}, StringSplitOptions.RemoveEmptyEntries)
                Session("LossCdWR") = arr(0)
                Select Case Trim(arr(0).ToString)
                    Case "WR"
                        pnMnRecord.Visible = True

                        Dim arrstrEvent() As String = strEvent.Split({","}, StringSplitOptions.RemoveEmptyEntries)
                        If strEvent.Contains(",") Then
                            Session("ProblemMC") = arrstrEvent(0) & "|" & arrstrEvent(1)
                        Else
                            Session("ProblemMC") = arrstrEvent(0) & "|" & "???"
                        End If
                        Dim arrMCFin() As String = strMc.Split({"("}, StringSplitOptions.RemoveEmptyEntries)
                        Dim arrMCFin1() As String = arrMCFin(1).Split({")"}, StringSplitOptions.RemoveEmptyEntries)
                        Dim arrMCFin3() As String = arrMCFin(0).Split({" "}, StringSplitOptions.RemoveEmptyEntries)
                        Session("MCFin") = arrMCFin3(0) & "|" & arrMCFin1(0)
                        Dim arrLossCd() As String = strLossCd.Split({"("}, StringSplitOptions.RemoveEmptyEntries)
                        Session("LossCd") = arrLossCd(0)
                        Dim arrSuppstrMc() As String = strMc.Split({"("}, StringSplitOptions.RemoveEmptyEntries)
                        Dim arrSuppstrMc1() As String = arrSuppstrMc(1).Split({")"}, StringSplitOptions.RemoveEmptyEntries)
                        Dim arrSuppstrMc2() As String = arrSuppstrMc(0).Split({" "}, StringSplitOptions.RemoveEmptyEntries)
                        Session("ChrMcName") = arrSuppstrMc2(0) & "|" & arrSuppstrMc1(0)
                    Case Else
                        pnMnRecord.Visible = False
                End Select
            Case "Select3" '----- Cancel order -----
                gvPst.Visible = False
                pnPstCancelOrder.Visible = True
                lbPstCancelDte.InnerText = strDte
                lbPstCancelMc.InnerText = strMc
                lbPstCancelLossCd.InnerText = strLossCd
                lbPstCancelEvent.InnerText = strEvent
                lbChkPstCancel.Visible = False
        End Select
        Call load_ddlChrNicPB()
    End Sub
    Private Sub tbPstResp_TextChanged(sender As Object, e As EventArgs) Handles tbPstResp.TextChanged
        lbChkPstResp.Visible = False
        Dim strEmpCd As String = String.Empty
        Select Case CInt(Len(tbPstResp.Text))
            Case 10 : strEmpCd = CStr((CLng(tbPstResp.Text) - 11779) / 29)
            Case Else : strEmpCd = CStr(CLng(tbPstResp.Text))
        End Select
        Dim strEmpName As String = String.Empty
        Dim conn As New OracleConnection(DB)
        Dim cmd As New OracleCommand("select EMPNAME from PSTM0002 where to_number(EMPCODE)='" & strEmpCd & "'", conn)
        conn.Open()
        Dim reader As OracleDataReader = cmd.ExecuteReader()
        Try
            While reader.Read()
                If IsDBNull(reader.GetValue(0)) = True Then strEmpName = String.Empty Else strEmpName = CStr(reader.GetValue(0))
            End While
        Finally
            reader.Close()
        End Try
        Dim connChkPst As New OracleConnection(DB)
        Dim cmdChkPst As New OracleCommand()
        Dim acc As String = "select MACHINE"
        acc += ",case when PSTSELE is null then '-' else PSTSELE end"
        acc += ",case when PSTRESP is null then '-' else PSTRESP end"
        acc += ",case when PSTSUPP is null then '-' else PSTSUPP end"
        acc += " from PSTH0001"
        acc += " where DEPARTMENT='" + strDepartment + "' and FACTORY='" + strFactory + "'"
        acc += " and PSTFIN is null and ROWID not in ('" + lbPstId.Text + "')"
        cmdChkPst.CommandText = acc
        cmdChkPst.Connection = connChkPst
        Using sda As New OracleDataAdapter(cmdChkPst)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvChkPstResp.DataSource = dt
            gvChkPstResp.DataBind()
        End Using
        For i As Integer = 0 To gvChkPstResp.Rows.Count - 1
            Dim strMc As String
            Dim arr() As String = gvChkPstResp.Rows(i).Cells(0).Text.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
            Select Case CInt(arr.Length.ToString)
                Case 1 : strMc = arr(0)
                Case 2 : strMc = arr(0) & " (" & arr(1) & ")"
            End Select
            Select Case True
                Case gvChkPstResp.Rows(i).Cells(2).Text = "-"
                    If gvChkPstResp.Rows(i).Cells(1).Text = strEmpName Then
                        lbChkPstResp.Visible = True
                        lbChkPstResp.InnerText = "พบรายชื่อท่านที่ไลน์ " & strMc & " ไม่สามารถบันทึกระบบได้"
                        Exit Sub
                    End If
                Case gvChkPstResp.Rows(i).Cells(1).Text = "-"
                    If gvChkPstResp.Rows(i).Cells(2).Text = strEmpName Then
                        lbChkPstResp.Visible = True
                        lbChkPstResp.InnerText = "พบรายชื่อท่านที่ไลน์ " & strMc & " ไม่สามารถบันทึกระบบได้"
                        Exit Sub
                    End If
                Case gvChkPstResp.Rows(i).Cells(1).Text <> "-" And gvChkPstResp.Rows(i).Cells(2).Text <> "-"
                    If gvChkPstResp.Rows(i).Cells(2).Text = strEmpName Then
                        lbChkPstResp.Visible = True
                        lbChkPstResp.InnerText = "พบรายชื่อท่านที่ไลน์ " & strMc & " ไม่สามารถบันทึกระบบได้"
                        Exit Sub
                    End If
                Case gvChkPstResp.Rows(i).Cells(1).Text = "-" And gvChkPstResp.Rows(i).Cells(2).Text = "-"
            End Select
            If gvChkPstResp.Rows(i).Cells(3).Text <> "-" Then
                arr = gvChkPstResp.Rows(i).Cells(3).Text.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
                For ii As Integer = 0 To CInt(arr.Length.ToString) - 1 Step 3
                    Select Case arr(ii + 2) = "-"
                        Case True
                            If arr(ii) = strEmpName Then
                                lbChkPstResp.Visible = True
                                lbChkPstResp.InnerText = "พบรายชื่อท่านที่ไลน์ " & strMc & " ไม่สามารถบันทึกระบบได้"
                                Exit Sub
                            End If
                    End Select
                Next
            End If
        Next
        lbPstResp.InnerText = strEmpName
    End Sub
    Private Sub btnPstBackResp_ServerClick(sender As Object, e As EventArgs) Handles btnPstBackResp.ServerClick
        Select Case lbPstResp.InnerText <> String.Empty And lbChkPstMachine.InnerText <> String.Empty
            Case True
                Dim connect As New OracleConnection(DB)
                Dim command As New OracleCommand()
                command.Connection = connect
                connect.Open()
                command.CommandText = "update PSTH0001 set PSTRESP='" + lbPstResp.InnerText + "',TIMERESP='" + CStr(Format(Now, "g")) + "' where ROWID='" + lbPstId.Text + "'"
                command.ExecuteNonQuery()
                connect.Close()
        End Select
        lbPstId.Text = String.Empty
        gvPst.Visible = True
        pnPstResp.Visible = False
        lbPstRespDte.InnerText = String.Empty
        lbPstRespMc.InnerText = String.Empty
        lbPstRespLossCd.InnerText = String.Empty
        lbPstRespEvent.InnerText = String.Empty
        lbPstRespSelect.InnerText = String.Empty
        lbPstResp.InnerText = String.Empty
        lbChkPstResp.Visible = False
        Call load_gvPstStation()
        Call load_gvPst()
        Call PstProcess()
    End Sub
    Private Sub tbPstSupp_TextChanged(sender As Object, e As EventArgs) Handles tbPstSupp.TextChanged
        lbChkPstSupp.Visible = False
        Dim strEmpCd As String = String.Empty
        Select Case CInt(Len(tbPstSupp.Text))
            Case 10 : strEmpCd = CStr((CLng(tbPstSupp.Text) - 11779) / 29)
            Case Else : strEmpCd = CStr(CLng(tbPstSupp.Text))
        End Select
        Dim strEmpName As String = String.Empty
        Dim conn As New OracleConnection(DB)
        Dim cmd As New OracleCommand("select EMPNAME from PSTM0002 where to_number(EMPCODE)='" & strEmpCd & "'", conn)
        conn.Open()
        Dim reader As OracleDataReader = cmd.ExecuteReader()
        Try
            While reader.Read()
                If IsDBNull(reader.GetValue(0)) = True Then strEmpName = String.Empty Else strEmpName = CStr(reader.GetValue(0))
            End While
        Finally
            reader.Close()
        End Try
        Dim connChkPst As New OracleConnection(DB)
        Dim cmdChkPst As New OracleCommand()
        Dim acc As String = "select MACHINE"
        acc += ",case when PSTSELE is null then '-' else PSTSELE end"
        acc += ",case when PSTRESP is null then '-' else PSTRESP end"
        acc += ",case when PSTSUPP is null then '-' else PSTSUPP end"
        acc += " from PSTH0001"
        acc += " where DEPARTMENT='" + strDepartment + "' and FACTORY='" + strFactory + "'"
        acc += " and PSTFIN is null and ROWID not in ('" + lbPstId.Text + "')"
        cmdChkPst.CommandText = acc
        cmdChkPst.Connection = connChkPst
        Using sda As New OracleDataAdapter(cmdChkPst)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvChkPstChkSupp.DataSource = dt
            gvChkPstChkSupp.DataBind()
        End Using
        Dim arr() As String
        For i As Integer = 0 To gvChkPstChkSupp.Rows.Count - 1
            Dim strMc As String
            arr = gvChkPstChkSupp.Rows(i).Cells(0).Text.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
            Select Case CInt(arr.Length.ToString)
                Case 1 : strMc = arr(0)
                Case 2 : strMc = arr(0) & " (" & arr(1) & ")"
            End Select
            Select Case True
                Case gvChkPstChkSupp.Rows(i).Cells(2).Text = "-"
                    If gvChkPstChkSupp.Rows(i).Cells(1).Text = strEmpName Then
                        lbChkPstSupp.Visible = True
                        lbChkPstSupp.InnerText = "พบรายชื่อท่านที่ไลน์ " & strMc & " ไม่สามารถบันทึกระบบได้"
                        Exit Sub
                    End If
                Case gvChkPstChkSupp.Rows(i).Cells(1).Text = "-"
                    If gvChkPstChkSupp.Rows(i).Cells(2).Text = strEmpName Then
                        lbChkPstSupp.Visible = True
                        lbChkPstSupp.InnerText = "พบรายชื่อท่านที่ไลน์ " & strMc & " ไม่สามารถบันทึกระบบได้"
                        Exit Sub
                    End If
                Case gvChkPstChkSupp.Rows(i).Cells(1).Text <> "-" And gvChkPstChkSupp.Rows(i).Cells(2).Text <> "-"
                    If gvChkPstChkSupp.Rows(i).Cells(2).Text = strEmpName Then
                        lbChkPstSupp.Visible = True
                        lbChkPstSupp.InnerText = "พบรายชื่อท่านที่ไลน์ " & strMc & " ไม่สามารถบันทึกระบบได้"
                        Exit Sub
                    End If
                Case gvChkPstChkSupp.Rows(i).Cells(1).Text = "-" And gvChkPstChkSupp.Rows(i).Cells(2).Text = "-"
            End Select
            If gvChkPstChkSupp.Rows(i).Cells(3).Text <> "-" Then
                arr = gvChkPstChkSupp.Rows(i).Cells(3).Text.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
                For ii As Integer = 0 To CInt(arr.Length.ToString) - 1 Step 3
                    Select Case arr(ii + 2) = "-"
                        Case True
                            If arr(ii) = strEmpName Then
                                lbChkPstSupp.Visible = True
                                lbChkPstSupp.InnerText = "พบรายชื่อท่านที่ไลน์ " & strMc & " ไม่สามารถบันทึกระบบได้"
                                Exit Sub
                            End If
                    End Select
                Next
            End If
        Next
        arr = lbPstSuppChk.InnerText.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
        Select Case CInt(arr.Length.ToString)
            Case 0
                lbPstSuppChk.InnerText = strEmpName & "|" & CStr(Format(Now, "g")) & "|-|"
                lbPstSupp.InnerText = strEmpName
            Case Is > 0
                Dim chk As Boolean = False
                For i As Integer = 0 To CInt(arr.Length.ToString) - 1 Step 3
                    If strEmpName = arr(i) And arr(i + 2) = "-" Then
                        arr(i + 2) = CStr(Format(Now, "g"))
                        chk = True
                        Exit For
                    End If
                Next
                Select Case chk
                    Case True
                        lbPstSuppChk.InnerText = String.Empty
                        For i As Integer = 0 To CInt(arr.Length.ToString) - 1
                            lbPstSuppChk.InnerText = lbPstSuppChk.InnerText & arr(i) & "|"
                        Next
                    Case False
                        lbPstSuppChk.InnerText = String.Empty
                        For i As Integer = 0 To CInt(arr.Length.ToString) - 1
                            lbPstSuppChk.InnerText = lbPstSuppChk.InnerText & arr(i) & "|"
                        Next
                        lbPstSuppChk.InnerText = lbPstSuppChk.InnerText & strEmpName & "|" & CStr(Format(Now, "g")) & "|-|"
                End Select
        End Select
        Dim arrSupp() As String = lbPstSuppChk.InnerText.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
        lbPstSupp.InnerText = String.Empty
        For i As Integer = 0 To CInt(arrSupp.Length.ToString) - 1 Step 3
            If arrSupp(i + 2) = "-" Then lbPstSupp.InnerText = lbPstSupp.InnerText & ", " & arrSupp(i)
        Next
        lbPstSupp.InnerText = Mid(lbPstSupp.InnerText, 3, Len(lbPstSupp.InnerText))
    End Sub
    Private Sub btnPstBackSupp_ServerClick(sender As Object, e As EventArgs) Handles btnPstBackSupp.ServerClick
        'Select Case lbPstSupp.InnerText <> String.Empty
        'Case True
        Dim connect As New OracleConnection(DB)
                Dim command As New OracleCommand()
                command.Connection = connect
                connect.Open()
                command.CommandText = "update PSTH0001 set PSTSUPP='" + lbPstSuppChk.InnerText + "' where ROWID='" + lbPstId.Text + "'"
                command.ExecuteNonQuery()
                connect.Close()
        'End Select
        lbPstId.Text = String.Empty
        gvPst.Visible = True
        pnPstSupp.Visible = False
        lbPstSuppDte.InnerText = String.Empty
        lbPstSuppMc.InnerText = String.Empty
        lbPstSuppLossCd.InnerText = String.Empty
        lbPstSuppEvent.InnerText = String.Empty
        lbPstSuppSelect.InnerText = String.Empty
        lbPstSupp.InnerText = String.Empty
        Call load_gvPstStation()
        Call load_gvPst()
        Call PstProcess()
    End Sub
    Private Sub tbPstFin_TextChanged(sender As Object, e As EventArgs) Handles tbPstFin.TextChanged
        lbChkPstFin.Visible = False
        Dim strEmpCd As String = String.Empty
        Select Case CInt(Len(tbPstFin.Text))
            Case 10 : strEmpCd = CStr((CLng(tbPstFin.Text) - 11779) / 29)
            Case Else : strEmpCd = CStr(CLng(tbPstFin.Text))
        End Select
        Dim strEmpName As String = String.Empty
        Dim conn As New OracleConnection(DB)
        Dim cmd As New OracleCommand("select EMPNAME from PSTM0002 where to_number(EMPCODE)='" & strEmpCd & "'", conn)
        conn.Open()
        Dim reader As OracleDataReader = cmd.ExecuteReader()
        Try
            While reader.Read()
                If IsDBNull(reader.GetValue(0)) = True Then strEmpName = String.Empty Else strEmpName = CStr(reader.GetValue(0))
            End While
        Finally
            reader.Close()
        End Try
        Dim connChkPst As New OracleConnection(DB)
        Dim cmdChkPst As New OracleCommand()
        Dim acc As String = "select MACHINE"
        acc += ",case when PSTRESP is null then '-' else PSTRESP end"
        acc += ",case when PSTSUPP is null then '-' else PSTSUPP end"
        acc += " from PSTH0001"
        acc += " where ROWID in ('" + lbPstId.Text + "')"
        cmdChkPst.CommandText = acc
        cmdChkPst.Connection = connChkPst
        Using sda As New OracleDataAdapter(cmdChkPst)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvChkPstFin.DataSource = dt
            gvChkPstFin.DataBind()
        End Using
        Dim chk As Boolean = False
        Select Case gvChkPstFin.Rows(0).Cells(1).Text = strEmpName
            Case True : chk = True
        End Select
        If gvChkPstFin.Rows(0).Cells(2).Text <> "-" Then
            Dim arr() As String = gvChkPstFin.Rows(0).Cells(2).Text.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To CInt(arr.Length.ToString) - 1 Step 3
                Select Case arr(i) = strEmpName
                    Case True : chk = True
                End Select
            Next
        End If
        Select Case chk
            Case False
                lbChkPstFin.Visible = True
                lbChkPstFin.InnerText = "ไม่พบรายชื่อของท่านในการแก้ไขปัญหาเครื่องจักร"
                Exit Sub
        End Select
        lbPstFin.InnerText = strEmpName
    End Sub
    '---------- Treeroj Add 01/06/2022 PST Finished OPERATOR ------------------------------------------------------------------------------------------
    Private Sub tbPstFinOp_TextChanged(sender As Object, e As EventArgs) Handles tbPstFinOp.TextChanged
        lbChkPstFin.Visible = False
        lbPstFinOp.InnerText = String.Empty
        Dim strOpCd, strEmpName As String
        Select Case Len(tbPstFinOp.Text)
            Case 4 : strOpCd = "0" & tbPstFinOp.Text
            Case 5 : strOpCd = tbPstFinOp.Text
            Case 10
                strOpCd = CStr((CLng(tbPstFinOp.Text) - 11779) / 29)
                Select Case Len(strOpCd)
                    Case 4 : strOpCd = "0" & strOpCd
                    Case 5 : strOpCd = strOpCd
                End Select
        End Select
        '--------------------------------------------------------------------------------------------------------------
        Dim conEmpName As New OracleConnection(DWUSER)
        Dim cmEmpName As New OracleCommand("select EMP_NAME from mv_hr_emp_act where EMP_CD='" + Trim(strOpCd) + "'", conEmpName)
        conEmpName.Open()
        Dim readerEmpName As OracleDataReader = cmEmpName.ExecuteReader()
        '--------------------------------------------------------------------------------------------------------------
        Dim strEmpNameMN As String = String.Empty
        Dim connEmpNameMN As New OracleConnection(DB)
        Dim cmdEmpNameMN As New OracleCommand("select EMPNAME from PSTM0002 where to_number(EMPCODE)='" & strOpCd & "'", connEmpNameMN)
        connEmpNameMN.Open()
        Dim readerEmpNameMN As OracleDataReader = cmdEmpNameMN.ExecuteReader()
        '--------------------------------------------------------------------------------------------------------------
        Try
            While readerEmpName.Read()
                If IsDBNull(readerEmpName.GetValue(0)) = True Then strEmpName = String.Empty Else strEmpName = CStr(readerEmpName.GetValue(0))
            End While
        Finally
            readerEmpName.Close()
        End Try
        '--------------------------------------------------------------------------------------------------------------
        Try
            While readerEmpNameMN.Read()
                If IsDBNull(readerEmpNameMN.GetValue(0)) = True Then strEmpNameMN = String.Empty Else strEmpNameMN = CStr(readerEmpNameMN.GetValue(0))
            End While
        Finally
            readerEmpNameMN.Close()
        End Try
        '--------------------------------------------------------------------------------------------------------------
        Select Case strEmpName = strEmpNameMN
            Case True
                lbPstFinOp.InnerText = String.Empty
                lbChkPstFin.Visible = True
                lbChkPstFin.InnerText = "ไม่พบรายชื่อของท่านในฐานข้อมูลหรือรหัสซ้ำ"
            Case False
                lbPstFinOp.InnerText = strEmpName
        End Select
        '--------------------------------------------------------------------------------------------------------------
    End Sub
    Private Sub btnPstBackFin_ServerClick(sender As Object, e As EventArgs) Handles btnPstBackFin.ServerClick

        '--------------------------------------------
        Select Case Trim(Session("LossCdWR").ToString)
            Case "WR"
                Dim strProplem, strDetail, strCause, strAdjust, strRecovery1, strRecovery2, strRecovery3, strRecovery4, strRecovery5, strComment As String
                '-- Chronic problem addition ( Read DB ) ----
                Dim strMC As String = String.Empty
                Dim connChr As New OracleConnection(MT800DB)
                Dim cmdChr As New OracleCommand()
                Dim accChr As String = "select * from CHRONIC_PROBLEM where MACHINE = '" & Session("ChrMcName") & "' Order by TOPIC "
                cmdChr.CommandText = accChr
                cmdChr.Connection = connChr
                connChr.Open()
                Dim rdChr As OracleDataReader = cmdChr.ExecuteReader()
                Try
                    While rdChr.Read()
                        If IsDBNull(rdChr.GetValue(3)) = True Then strMC = String.Empty Else strMC = CStr(rdChr.GetValue(3))
                    End While
                Finally
                    rdChr.Close()
                End Try

                Select Case lbPstFin.InnerText <> String.Empty
                    Case False
                        Exit Select
                    Case True
                        Select Case lbPstFinOp.InnerText <> String.Empty
                            Case False
                                Exit Select
                            Case True
                                Select Case Me.imgProblem.HasFile
                                    Case False
                                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alertMeaasge", "alert('กรุณาแนบรูปปัญหาที่พบ')", True)
                                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "none", "<script>$('#showPst').modal('show');</script>", False)
                                        Exit Sub
                                    Case True
                                        Select Case ddlChrNicPB.SelectedValue = "อื่นๆ(Other)"
                                            Case True
                                                If tbOtherProblem.Text = String.Empty Then
                                                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alertMeaasge", "alert('ไม่พบข้อมูล ปํญหา(หัวข้อที่2)')", True)
                                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "none", "<script>$('#showPst').modal('show');</script>", False)
                                                    Exit Sub
                                                End If
                                                strDetail = tbOtherProblem.Text
                                            Case False
                                                If ddlChrNicPB.SelectedItem.Text = "กรุณาเลือกปํญหา" Then
                                                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alertMeaasge", "alert('ไม่พบข้อมูล ปํญหา(หัวข้อที่2)')", True)
                                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "none", "<script>$('#showPst').modal('show');</script>", False)
                                                    Exit Sub
                                                End If
                                                strDetail = ddlChrNicPB.SelectedItem.Text
                                        End Select
                                        Select Case tbCause.Text
                                            Case String.Empty
                                                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alertMeaasge", "alert('ไม่พบข้อมูล สาเหตุของปัญหา~Failure Cause')", True)
                                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "none", "<script>$('#showPst').modal('show');</script>", False)
                                                Exit Sub
                                            Case Else
                                                strCause = tbCause.Text
                                        End Select
                                        Select Case rblAdjust.SelectedIndex
                                            Case 0
                                                If tbAdjustMc1.Text = String.Empty Or tbAdjustMc2.Text = String.Empty Or tbAdjustMc3.Text = String.Empty Then
                                                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alertMeaasge", "alert('ข้อมูลไม่ครบ กรุณาตรวจสอบ\n-ปรับอะไรไป?\n-ก่อนการแก้ไขเป็นยังไง\n-หลังการแก้ไขเป็นยังไง')", True)
                                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "none", "<script>$('#showPst').modal('show');</script>", False)
                                                    Exit Sub
                                                End If
                                                strAdjust = rblAdjust.SelectedItem.Text & "|" & Trim(tbAdjustMc1.Text) & "|" & Trim(tbAdjustMc2.Text) & "|" & Trim(tbAdjustMc3.Text)
                                            Case 1, 2
                                                strAdjust = rblAdjust.SelectedItem.Text
                                        End Select
                                        If tbRecovery1.Text = String.Empty And tbRecovery2.Text = String.Empty And tbRecovery3.Text = String.Empty And tbRecovery4.Text = String.Empty And tbRecovery5.Text = String.Empty Then
                                            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alertMeaasge", "alert('ไม่พบข้อมูล วิธีการแก้ไขปัญหา~Recovery method')", True)
                                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "none", "<script>$('#showPst').modal('show');</script>", False)
                                            Exit Sub
                                        End If
                                        strRecovery1 = tbRecovery1.Text
                                        strRecovery2 = tbRecovery2.Text
                                        strRecovery3 = tbRecovery3.Text
                                        strRecovery4 = tbRecovery4.Text
                                        strRecovery5 = tbRecovery5.Text
                                        strComment = tbComment.Text
                                        Dim iwidth As Integer
                                        Dim iheight As Integer
                                        Select Case imgProblem.PostedFile.ContentLength
                                            Case Is > 200000
                                                Try
                                                    Dim hfc As HttpFileCollection = Request.Files
                                                    If hfc.Count > 0 Then
                                                        Dim hpf As HttpPostedFile = hfc(0)
                                                        If hpf.ContentLength > 0 Then
                                                            Dim sImageName As String = hpf.FileName
                                                            hpf.SaveAs(Server.MapPath("~/imgTKANTF/" & Path.GetFileName(sImageName)))
                                                            Dim bitmap As New Bitmap(Server.MapPath("~/imgTKANTF/" & Path.GetFileName(hpf.FileName)))
                                                            'iwidth = bitmap.Width
                                                            'iheight = bitmap.Height
                                                            iwidth = 400 'bitmap.Width
                                                            iheight = 400 'bitmap.Height
                                                            bitmap.Dispose()
                                                            Dim objOptImage As System.Drawing.Image = New System.Drawing.Bitmap(iwidth, iheight, System.Drawing.Imaging.PixelFormat.Format16bppRgb555)
                                                            Using objImg As System.Drawing.Image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("~/imgTKANTF/" & sImageName))
                                                                Using oGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(objOptImage)
                                                                    With oGraphic
                                                                        Dim oRectangle As New System.Drawing.Rectangle(0, 0, iwidth, iheight)
                                                                        .DrawImage(objImg, oRectangle)
                                                                    End With
                                                                End Using
                                                                objOptImage.Save(HttpContext.Current.Server.MapPath("~/imgTKANTFOTM/" & sImageName), System.Drawing.Imaging.ImageFormat.Jpeg)
                                                                '---- Spread action variable
                                                                Dim Mystream As New FileStream("//172.16.72.179/mt800pst/MT800PST-F17-1ST-SA10/imgTKANTFOTM/" & sImageName, FileMode.Open)
                                                                Dim filesize As Long = Mystream.Length
                                                                Dim imbByte(filesize) As Byte
                                                                Mystream.Read(imbByte, 0, filesize)
                                                                Dim ExtType As String = System.IO.Path.GetExtension(imgProblem.PostedFile.FileName).ToLower()
                                                                Dim strMIME As String = Nothing
                                                                Select Case ExtType
                                                                    Case ".jpg", ".jpeg", ".jpe" : strMIME = "image/jpeg"
                                                                    Case ".png" : strMIME = "image/png"
                                                                    Case Else
                                                                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alertMeaasge", "alert('โปรแกรมไม่รองรับไฟล์รูปนี้\nกรุณาตรวจสอบ')", True)
                                                                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "none", "<script>$('#showPst').modal('show');</script>", False)
                                                                        Exit Sub
                                                                End Select
                                                                Dim objConn As New OracleConnection
                                                                Dim strConnString As String = MT800DB
                                                                Dim strSQL As String = "insert into TKANTF" +
                                                            "(ID,DTE,IMG,IMGSIDE,PROBLEM,DETAIL,CAUSE,ADJUSTMC,RECOVERY1,RECOVERY2,RECOVERY3,RECOVERY4,RECOVERY5,CMT,MC,LOSSCODE,EVENT)" +
                                                            "values" +
                                                            "(:sId,:sDte,:sImg,:sImgSide,:sProblem,:sDetail,:sCause,:sAdjust,:sRecover1,:sRecover2,:sRecover3,:sRecover4,:sRecover5,:sCmt,:sMc,:sLossCd,:sEvent)"
                                                                objConn.ConnectionString = strConnString
                                                                objConn.Open()
                                                                Dim objCmd As New OracleCommand(strSQL, objConn)
                                                                objCmd.Parameters.Add("@sId", lbPstId.Text)
                                                                objCmd.Parameters.Add("@sDte", CStr(Format(Now, "g")))
                                                                objCmd.Parameters.Add("@sImg", imbByte)
                                                                objCmd.Parameters.Add("@sImgSide", CStr(Mystream.Length))
                                                                objCmd.Parameters.Add("@sProblem", Session("ProblemMC"))
                                                                objCmd.Parameters.Add("@sDetail", strDetail)
                                                                objCmd.Parameters.Add("@sCause", strCause)
                                                                objCmd.Parameters.Add("@sAdjust", strAdjust)
                                                                objCmd.Parameters.Add("@sRecover1", strRecovery1)
                                                                objCmd.Parameters.Add("@sRecover2", strRecovery2)
                                                                objCmd.Parameters.Add("@sRecover3", strRecovery3)
                                                                objCmd.Parameters.Add("@sRecover4", strRecovery4)
                                                                objCmd.Parameters.Add("@sRecover5", strRecovery5)
                                                                objCmd.Parameters.Add("@sCmt", strComment)
                                                                objCmd.Parameters.Add("@sMc", Session("MCFin"))
                                                                objCmd.Parameters.Add("@sLossCd", Session("LossCd"))
                                                                objCmd.Parameters.Add("@sEvent", Session("ProblemMC"))
                                                                objCmd.ExecuteNonQuery()
                                                                objConn.Close()
                                                                objConn = Nothing
                                                                objImg.Dispose()
                                                            End Using
                                                            objOptImage.Dispose()
                                                        End If
                                                    End If
                                                Catch ex As Exception
                                                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alertMeaasge", "alert('รูปมีขนาดเกิน 2,400 X 2,400 Pixel\nกรุณาแก้ไขขนาดรูปและลงข้อมูลอีกครั้ง')", True)
                                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "none", "<script>$('#showPst').modal('show');</script>", False)
                                                    Exit Sub
                                                End Try
                                            Case Else
                                                Dim imbByte(imgProblem.PostedFile.InputStream.Length) As Byte
                                                imgProblem.PostedFile.InputStream.Read(imbByte, 0, imbByte.Length)
                                                Dim ExtType As String = System.IO.Path.GetExtension(imgProblem.PostedFile.FileName).ToLower()
                                                Dim strMIME As String = Nothing
                                                Select Case ExtType
                                                    Case ".jpg", ".jpeg", ".jpe" : strMIME = "image/jpeg"
                                                    Case ".png" : strMIME = "image/png"
                                                    Case Else
                                                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alertMeaasge", "alert('โปรแกรมไม่รองรับไฟล์รูปนี้\nกรุณาตรวจสอบ')", True)
                                                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "none", "<script>$('#showPst').modal('show');</script>", False)
                                                        Exit Sub
                                                End Select
                                                Dim objConn As New OracleConnection
                                                Dim strConnString As String = MT800DB
                                                Dim strSQL As String = "insert into TKANTF" +
                                                    "(ID,DTE,IMG,IMGSIDE,PROBLEM,DETAIL,CAUSE,ADJUSTMC,RECOVERY1,RECOVERY2,RECOVERY3,RECOVERY4,RECOVERY5,CMT,MC,LOSSCODE,EVENT)" +
                                                    "values" +
                                                    "(:sId,:sDte,:sImg,:sImgSide,:sProblem,:sDetail,:sCause,:sAdjust,:sRecover1,:sRecover2,:sRecover3,:sRecover4,:sRecover5,:sCmt,:sMc,:sLossCd,:sEvent)"
                                                objConn.ConnectionString = strConnString
                                                objConn.Open()
                                                Dim objCmd As New OracleCommand(strSQL, objConn)
                                                objCmd.Parameters.Add("@sId", lbPstId.Text)
                                                objCmd.Parameters.Add("@sDte", CStr(Format(Now, "g")))
                                                objCmd.Parameters.Add("@sImg", imbByte)
                                                objCmd.Parameters.Add("@sImgSide", CStr(imgProblem.PostedFile.ContentLength))
                                                objCmd.Parameters.Add("@sProblem", Session("ProblemMC"))
                                                objCmd.Parameters.Add("@sDetail", strDetail)
                                                objCmd.Parameters.Add("@sCause", strCause)
                                                objCmd.Parameters.Add("@sAdjust", strAdjust)
                                                objCmd.Parameters.Add("@sRecover1", strRecovery1)
                                                objCmd.Parameters.Add("@sRecover2", strRecovery2)
                                                objCmd.Parameters.Add("@sRecover3", strRecovery3)
                                                objCmd.Parameters.Add("@sRecover4", strRecovery4)
                                                objCmd.Parameters.Add("@sRecover5", strRecovery5)
                                                objCmd.Parameters.Add("@sCmt", strComment)
                                                objCmd.Parameters.Add("@sMc", Session("MCFin"))
                                                objCmd.Parameters.Add("@sLossCd", Session("LossCd"))
                                                objCmd.Parameters.Add("@sEvent", Session("ProblemMC"))
                                                objCmd.ExecuteNonQuery()
                                                objConn.Close()
                                                objConn = Nothing
                                        End Select
                                        Dim conn As New OracleConnection(DB)
                                        Dim cmd As New OracleCommand()
                                        cmd.Connection = conn
                                        conn.Open()
                                        cmd.CommandText = "update PSTH0001 set REPORT='Y' where ROWID='" & lbPstId.Text & "'"
                                        cmd.ExecuteNonQuery()
                                        conn.Close()


                                        Dim connect As New OracleConnection(DB)
                                        Dim command As New OracleCommand()
                                        command.Connection = connect
                                        connect.Open()
                                        command.CommandText = "update PSTH0001 set PSTFIN='" + lbPstFin.InnerText + "',TIMEFIN='" + CStr(Format(Now, "g")) + "' where ROWID='" + lbPstId.Text + "'"
                                        command.ExecuteNonQuery()
                                        command.CommandText = "update PSTM0002 set DTE_SUPPORT='" + CStr(Format(Now.AddMinutes(1), "g")) + "' where EMPNAME='" + lbPstFin.InnerText + "'"
                                        command.ExecuteNonQuery()
                                        connect.Close()
                                End Select
                        End Select
                End Select
            Case Else
                Select Case lbPstFin.InnerText <> String.Empty And lbPstFinOp.InnerText <> String.Empty
                    Case True
                        Dim connect As New OracleConnection(DB)
                        Dim command As New OracleCommand()
                        command.Connection = connect
                        connect.Open()
                        command.CommandText = "update PSTH0001 set PSTFIN='" + lbPstFin.InnerText + "',TIMEFIN='" + CStr(Format(Now, "g")) + "' where ROWID='" + lbPstId.Text + "'"
                        command.ExecuteNonQuery()
                        command.CommandText = "update PSTM0002 set DTE_SUPPORT='" + CStr(Format(Now.AddMinutes(1), "g")) + "' where EMPNAME='" + lbPstFin.InnerText + "'"
                        command.ExecuteNonQuery()
                        connect.Close()
                End Select
        End Select


        'lbPstReportId.Text = String.Empty
        'lbPstReportDte.Text = String.Empty
        tbOtherProblem.Text = String.Empty
        tbCause.Text = String.Empty
        rblAdjust.SelectedIndex = 0
        pnAdjustMc.Visible = True
        tbAdjustMc1.Text = String.Empty
        tbAdjustMc2.Text = String.Empty
        tbAdjustMc3.Text = String.Empty
        tbRecovery1.Text = String.Empty
        tbRecovery2.Text = String.Empty
        tbRecovery3.Text = String.Empty
        tbRecovery4.Text = String.Empty
        tbRecovery5.Text = String.Empty
        tbComment.Text = String.Empty
        'lbPstReportMc.Text = String.Empty
        'lbPstReportLossCd.Text = String.Empty
        'lbPstReportEvent.Text = String.Empty
        'gvPstReport.Visible = True
        'pnPstReport.Visible = False
        'pnPstReportSearch.Visible = True

        lbPstId.Text = String.Empty
        gvPst.Visible = True
        pnPstFin.Visible = False
        lbPstFinDte.InnerText = String.Empty
        lbPstFinMc.InnerText = String.Empty
        lbPstFinLossCd.InnerText = String.Empty
        lbPstFinEvent.InnerText = String.Empty
        lbPstFinSelect.InnerText = String.Empty
        lbPstFinResp.InnerText = String.Empty
        lbPstFinSupp.InnerText = String.Empty
        lbPstFin.InnerText = String.Empty
        lbPstFinOp.InnerText = String.Empty '---------- Treeroj Add 01/06/2022 PST Finished OPERATOR (lbPstFinOp.InnerText = String.Empty)----
        lbChkPstFin.Visible = False
        Call load_gvPstStation()
        Call load_gvPst()
        Call PstProcess()
        '-----------------------------------------------
        'Call load_gvPstReport()

        Timer1.Enabled = True
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "none", "<script>$('#showPst').modal('show');</script>", False)
    End Sub
    Private Sub tbPstCancel_TextChanged(sender As Object, e As EventArgs) Handles tbPstCancel.TextChanged
        lbChkPstCancel.Visible = False
        lbPstCancel.InnerText = String.Empty
        Dim strEmpCd As String = String.Empty
        Select Case CInt(Len(tbPstCancel.Text))
            Case 10 : strEmpCd = CStr((CLng(tbPstCancel.Text) - 11779) / 29)
            Case Else : strEmpCd = CStr(CLng(tbPstCancel.Text))
        End Select
        Dim strEmpName As String = String.Empty
        Dim conn As New OracleConnection(DB)
        Dim cmd As New OracleCommand("select EMPNAME from PSTM0002 where to_number(EMPCODE)='" & strEmpCd & "'", conn)
        conn.Open()
        Dim reader As OracleDataReader = cmd.ExecuteReader()
        Try
            While reader.Read()
                If IsDBNull(reader.GetValue(0)) = True Then strEmpName = String.Empty Else strEmpName = CStr(reader.GetValue(0))
            End While
        Finally
            reader.Close()
        End Try
        Select Case strEmpName <> String.Empty
            Case True
                lbPstCancel.InnerText = strEmpName
            Case False
                lbPstCancel.InnerText = String.Empty
                lbChkPstCancel.Visible = True
                lbChkPstCancel.InnerText = "ไม่พบรายชื่อของท่านในฐานข้อมูล PST"
        End Select
    End Sub
    Private Sub tbPstCancelOp_TextChanged(sender As Object, e As EventArgs) Handles tbPstCancelOp.TextChanged
        lbChkPstCancel.Visible = False
        lbPstCancelOp.InnerText = String.Empty
        Dim strOpCd, strEmpName As String
        Select Case Len(tbPstCancelOp.Text)
            Case 4 : strOpCd = "0" & tbPstCancelOp.Text
            Case 5 : strOpCd = tbPstCancelOp.Text
            Case 10
                strOpCd = CStr((CLng(tbPstCancelOp.Text) - 11779) / 29)
                Select Case Len(strOpCd)
                    Case 4 : strOpCd = "0" & strOpCd
                    Case 5 : strOpCd = strOpCd
                End Select
        End Select
        Dim conn As New OracleConnection(DWUSER)
        Dim cmd As New OracleCommand("select EMP_NAME from mv_hr_emp_act where EMP_CD='" + Trim(strOpCd) + "'", conn)
        conn.Open()
        Dim rd As OracleDataReader = cmd.ExecuteReader()
        '---------- Treeroj add ------------------------------------------------------------------------------------------
        Dim strEmpNameMN As String = String.Empty
        Dim connn As New OracleConnection(DB)
        Dim cmdd As New OracleCommand("select EMPNAME from PSTM0002 where to_number(EMPCODE)='" & strOpCd & "'", connn)
        connn.Open()
        Dim reader As OracleDataReader = cmdd.ExecuteReader()
        '--------------------------------------------------------------------------------------------------------------
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strEmpName = String.Empty Else strEmpName = CStr(rd.GetValue(0))
            End While
        Finally
            rd.Close()
        End Try
        '---------- Treeroj add ------------------------------------------------------------------------------------------
        Try
            While reader.Read()
                If IsDBNull(reader.GetValue(0)) = True Then strEmpNameMN = String.Empty Else strEmpNameMN = CStr(reader.GetValue(0))
            End While
        Finally
            reader.Close()
        End Try
        '--------------------------------------------------------------------------------------------------------------
        '---------- Treeroj revise ------------------------------------------------------------------------------------------
        'Select Case strEmpName <> String.Empty
        'Case True
        'lbPstCancelOp.InnerText = strEmpName
        'Case False
        'lbPstCancelOp.InnerText = String.Empty
        'lbChkPstCancel.Visible = True
        'lbChkPstCancel.InnerText = "ไม่พบรายชื่อพนักงานในฐานข้อมูล"
        'End Select
        '--------------------------------------------------------------------------------------------------------------

        '---------- Treeroj add ------------------------------------------------------------------------------------------
        Select Case strEmpName = strEmpNameMN
            Case True
                lbPstCancelOp.InnerText = String.Empty
                lbChkPstCancel.Visible = True
                lbChkPstCancel.InnerText = "ไม่พบรายชื่อของท่านในฐานข้อมูลหรือรหัสซ้ำ"
            Case False

                lbPstCancelOp.InnerText = strEmpName
        End Select
        '--------------------------------------------------------------------------------------------------------------
    End Sub
    Private Sub btnPstBackCancel_ServerClick(sender As Object, e As EventArgs) Handles btnPstBackCancel.ServerClick
        If lbPstCancel.InnerText <> String.Empty And lbPstCancelOp.InnerText <> String.Empty Then
            Dim connect As New OracleConnection(DB)
            Dim command As New OracleCommand()
            command.Connection = connect
            connect.Open()
            command.CommandText = "delete PSTH0001 where ROWID='" + lbPstId.Text + "'"
            command.ExecuteNonQuery()
            command.CommandText = "insert into PSTH0003(DEPARTMENT,FACTORY,MACHINE,PSTCANCEL,OPCONFIRM,DTE)" +
                                "values" +
                                "('" + strDepartment + "','" + strFactory + "','" + lbPstCancelMc.InnerText + "','" + lbPstCancel.InnerText + "','" + lbPstCancelOp.InnerText + "','" + CStr(Format(Now, "g")) + "')"
            command.ExecuteNonQuery()
            connect.Close()
        End If
        lbPstId.Text = String.Empty
        gvPst.Visible = True
        pnPstCancelOrder.Visible = False
        lbPstCancelDte.InnerText = String.Empty
        lbPstCancelMc.InnerText = String.Empty
        lbPstCancelLossCd.InnerText = String.Empty
        lbPstCancelEvent.InnerText = String.Empty
        lbPstCancel.InnerText = String.Empty
        lbPstCancelOp.InnerText = String.Empty
        lbChkPstCancel.Visible = False
        Call load_gvPstStation()
        Call load_gvPst()
        Call PstProcess()
        '-----------------------------------------------
        'Call load_gvPstReport()
    End Sub
    '------------------------------------ Treeroj cancel 11/10/2022 PST report ----------------------------------------------------
    'Private Sub tbPstReportSearch_TextChanged(sender As Object, e As EventArgs) Handles tbPstReportSearch.TextChanged
    '    lbPstReportSearch.Text = String.Empty
    '    Dim strEmpCd As String = String.Empty
    '    Select Case CInt(Len(tbPstReportSearch.Text))
    '        Case 10 : strEmpCd = CStr((CLng(tbPstReportSearch.Text) - 11779) / 29)
    '        Case Else : strEmpCd = CStr(CLng(tbPstReportSearch.Text))
    '    End Select
    '    Dim strEmpName As String = String.Empty
    '    Dim conn As New OracleConnection(DB)
    '    Dim cmd As New OracleCommand("select EMPNAME from PSTM0002 where to_number(EMPCODE)='" & strEmpCd & "'", conn)
    '    conn.Open()
    '    Dim reader As OracleDataReader = cmd.ExecuteReader()
    '    Try
    '        While reader.Read()
    '            If IsDBNull(reader.GetValue(0)) = True Then strEmpName = String.Empty Else strEmpName = CStr(reader.GetValue(0))
    '        End While
    '    Finally
    '        reader.Close()
    '    End Try
    '    lbPstReportSearch.Text = strEmpName
    '    Call load_gvPstReport()
    'End Sub
    '----------------------------------------------------------------------------------------------------------------

    '------------------------------------ Treeroj cancel 11/10/2022 PST report ----------------------------------------------------
    'Private Sub load_gvPstReport()
    '    Dim conn As New OracleConnection(DB)
    '    Dim cmd As New OracleCommand()
    '    Dim acc As String = "select to_char(to_date(T1.TIMEFIN,'MM/DD/YYYY HH:MI AM'),'DD-MON-YY, HH24:MI')"
    '    acc += " || '~' || T1.MACHINE"
    '    acc += " || '~' || T1.LOSSCODE"
    '    acc += " || '~' || case when T1.EVENT is null then '-' else T1.EVENT end"
    '    acc += " || '!' || T2.TIMER"
    '    acc += " || '!' || case when T1.PSTFIN is null then '-' else substr(T1.PSTFIN, 1,instr(T1.PSTFIN,' ') - 1) || '.' || substr(T1.PSTFIN,instr(T1.PSTFIN,' ') + 1,3) end"
    '    acc += " || '~' || T1.ROWID"
    '    acc += " || '~' || T1.TIMEREQ"
    '    acc += " || '~' || T1.MACHINE"
    '    acc += " || '~' || T1.LOSSCODE"
    '    acc += " || '~' || case when T1.EVENT is null then '-' else T1.EVENT end as DETAIL"
    '    acc += " from PSTH0001 T1"
    '    acc += " left join (Select MACHINE,LOSSCODE,case when EVENT Is null then '-' else EVENT end as ENT,TIMEFIN"
    '    acc += ",to_char(sum(round((to_date(to_char(SYSDATE,'MM/DD/YYYY HH:MI AM'),'MM/DD/YYYY HH:MI AM')"
    '    acc += " - to_date(TIMEFIN,'MM/DD/YYYY HH:MI AM')) * (60 * 24),2)),'99,999') as TIMER"
    '    acc += " from PSTH0001"
    '    acc += " where PSTFIN is not null"
    '    acc += " and REPORT is null"
    '    acc += " group by MACHINE,LOSSCODE,EVENT,TIMEFIN) T2"
    '    acc += " on T1.MACHINE=T2.MACHINE and T1.LOSSCODE=T2.LOSSCODE and T1.TIMEFIN=T2.TIMEFIN"
    '    acc += " where T1.DEPARTMENT='" + strDepartment + "' and T1.FACTORY='" + strFactory + "'"
    '    acc += " and T1.TIMEFIN is not null"
    '    acc += " and T1.REPORT is null"
    '    acc += " and T1.LOSSCODE in ('WR')"
    '    Select Case lbPstReportSearch.Text <> String.Empty
    '        Case True : acc += " and T1.PSTFIN='" + lbPstReportSearch.Text + "'"
    '        Case False : acc += " and to_date(T1.TIMEFIN,'MM/DD/YYYY HH:MI AM') >= sysdate-1"
    '    End Select
    '    acc += " order by to_date(T1.TIMEFIN,'MM/DD/YYYY HH:MI AM') asc"
    '    cmd.CommandText = acc
    '    cmd.Connection = conn
    '    Using sda As New OracleDataAdapter(cmd)
    '        Dim dt As New DataTable()
    '        sda.Fill(dt)
    '        gvPstReport.DataSource = dt
    '        gvPstReport.DataBind()
    '    End Using
    '    For i As Integer = 0 To gvPstReport.Rows.Count - 1
    '        Select Case i Mod 2
    '            Case 0 : gvPstReport.Rows(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#EAECEE")
    '        End Select
    '        Dim arrMain() As String = gvPstReport.Rows(i).Cells(0).Text.Split({"!"}, StringSplitOptions.RemoveEmptyEntries)
    '        Dim arrSub1() As String = arrMain(0).Split({"~"}, StringSplitOptions.RemoveEmptyEntries)
    '        gvPstReport.Rows(i).Cells(0).Text = "Job finished : " & arrSub1(0)
    '        Dim arrSub1Mc() As String = arrSub1(1).Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
    '        Select Case CInt(arrSub1Mc.Length.ToString)
    '            Case 1 : gvPstReport.Rows(i).Cells(0).Text = gvPstReport.Rows(i).Cells(0).Text & "</br>Machine : " & arrSub1Mc(0)
    '            Case 2 : gvPstReport.Rows(i).Cells(0).Text = gvPstReport.Rows(i).Cells(0).Text & "</br>Machine : " & arrSub1Mc(0) & "(" & arrSub1Mc(1) & ")"
    '        End Select
    '        Select Case arrSub1(3) <> "-"
    '            Case True
    '                Dim arrSub1Event() As String = arrSub1(3).Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
    '                gvPstReport.Rows(i).Cells(0).Text = gvPstReport.Rows(i).Cells(0).Text & "<br/>Event : "
    '                For ii As Integer = 0 To CInt(arrSub1Event.Length.ToString) - 1
    '                    Select Case ii
    '                        Case 0 : gvPstReport.Rows(i).Cells(0).Text = gvPstReport.Rows(i).Cells(0).Text & arrSub1Event(ii)
    '                        Case > 0 : gvPstReport.Rows(i).Cells(0).Text = gvPstReport.Rows(i).Cells(0).Text & ",&nbsp;" & arrSub1Event(ii)
    '                    End Select
    '                Next
    '        End Select
    '        Dim arrSub3() As String = arrMain(2).Split({"~"}, StringSplitOptions.RemoveEmptyEntries)
    '        gvPstReport.Rows(i).Cells(0).Text = gvPstReport.Rows(i).Cells(0).Text & "</br>By PST : " & arrSub3(0)

    '        Select Case CInt(arrMain(1).Replace(",", String.Empty))
    '            Case >= 30 : gvPstReport.Rows(i).Cells(0).Text = gvPstReport.Rows(i).Cells(0).Text & "</br>Report delay : <span class=" & "blinking>" & arrMain(1) & "</span> min"
    '            Case Else : gvPstReport.Rows(i).Cells(0).Text = gvPstReport.Rows(i).Cells(0).Text & "</br>Report delay : " & arrMain(1) & " min"
    '        End Select
    '        gvPstReport.Rows(i).Cells(0).Text = gvPstReport.Rows(i).Cells(0).Text & "<font style='font-size:0px'>!" & arrSub3(1) & "!" & arrSub3(2) & "!" & arrSub3(3) & "!" & arrSub3(4) & "!" & arrSub3(5) & "!-</font>"
    '    Next
    'End Sub
    '----------------------------------------------------------------------------------------------------------------
    '------------------------------------ Treeroj cancel 11/10/2022 PST report ----------------------------------------------------
    'Protected Sub PstReportListRowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
    '    Dim row As GridViewRow = gvPstReport.Rows(Convert.ToInt32(e.CommandArgument))
    '    Dim arr() As String = row.Cells(0).Text.Split({"!"}, StringSplitOptions.RemoveEmptyEntries)
    '    lbPstReportId.Text = arr(1)
    '    lbPstReportDte.Text = arr(2)
    '    lbPstReportMc.Text = arr(3)
    '    lbPstReportLossCd.Text = arr(4)
    '    lbPstReportEvent.Text = arr(5)
    '    Select Case e.CommandName
    '        Case "Select" '----- Report -----
    '            gvPstReport.Visible = False
    '            pnPstReportSearch.Visible = False
    '            pnPstReport.Visible = True
    '    End Select

    '    Dim arrr() As String = lbPstReportMc.Text.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
    '    Session("mcname") = arrr(1)
    '    IbMcshow.Text = Session("mcname").ToString
    '    Session("mcline") = arrr(0)
    '    IbLineshow.Text = Session("mcline").ToString


    '    Dim arrRSPname() As String = arr(0).Split({"By PST :"}, StringSplitOptions.RemoveEmptyEntries)
    '    Dim arrRSPname1() As String = arrRSPname(1).Split({"Report"}, StringSplitOptions.RemoveEmptyEntries)
    '    Session("RSPnameshow") = arrRSPname1(0)
    '    Ibnameshow.Text = Session("RSPnameshow").ToString

    '    Dim arrrr() As String = Session("mcname").Split({"."}, StringSplitOptions.RemoveEmptyEntries)
    '    Session("ddlmcname") = arrrr(0)

    '    Call load_ddlChrNicPB()

    'End Sub
    '----------------------------------------------------------------------------------------------------------------
    Protected Sub rblAdjust_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblAdjust.SelectedIndexChanged
        Select Case rblAdjust.SelectedIndex
            Case 0
                pnAdjustMc.Visible = True
                tbAdjustMc1.Text = String.Empty
                tbAdjustMc2.Text = String.Empty
                tbAdjustMc3.Text = String.Empty
            Case Else
                pnAdjustMc.Visible = False
                tbAdjustMc1.Text = String.Empty
                tbAdjustMc2.Text = String.Empty
                tbAdjustMc3.Text = String.Empty
        End Select
    End Sub
    Private Sub btnMnHistory_ServerClick(sender As Object, e As EventArgs) Handles btnMnHistory.ServerClick
        Timer1.Enabled = False
        tbMnHisBeginDte.Text = CStr(Format(Today.AddDays(0), "dd-MMM-yyyy"))
        tbMnHisUntilDte.Text = CStr(Format(Today, "dd-MMM-yyyy"))
        tbMnHisSearch.Text = String.Empty
        Call load_gvMnHis()
        Call load_gvPstHis()
        Call ProcessHis()
    End Sub
    Private Sub load_gvMnHis()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select to_char(to_date(DTE,'MM/DD/YYYY HH:MI AM'),'DD-MON-YY, HH24:MI') as SDTE"
        acc += ",IMG"
        acc += ",MC || '!' || LOSSCODE || '!' || EVENT || '!' || PROBLEM || '!' || DETAIL || '!' || CAUSE as DB1"
        acc += ",ADJUSTMC as DB2"
        acc += ",case when RECOVERY1 is null then '-' else RECOVERY1 end"
        acc += " || '|' || case when RECOVERY2 is null then '-' else RECOVERY2 end"
        acc += " || '|' || case when RECOVERY3 is null then '-' else RECOVERY3 end"
        acc += " || '|' || case when RECOVERY4 is null then '-' else RECOVERY4 end"
        acc += " || '|' || case when RECOVERY5 is null then '-' else RECOVERY5 end"
        acc += " || '|' || case when CMT is null then '-' else CMT end as DB3"
        acc += ",ID"
        acc += " from TKANTF"
        acc += " where to_char(to_date(DTE,'MM/DD/YYYY HH:MI AM'),'YYYYMMDDHH24MI')>=:db1"
        acc += " and to_char(to_date(DTE,'MM/DD/YYYY HH:MI AM'),'YYYYMMDDHH24MI')<=:db2"
        cmd.Parameters.Add(New OracleParameter("db1", Format(CDate(tbMnHisBeginDte.Text), "yyyyMMddHHmm")))
        cmd.Parameters.Add(New OracleParameter("db2", Format(CDate(tbMnHisUntilDte.Text).AddHours(23).AddMinutes(59), "yyyyMMddHHmm")))
        If tbMnHisSearch.Text <> String.Empty Then
            Dim arrSearch() As String = UCase(Trim(tbMnHisSearch.Text)).Split({"+"}, StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To CInt(arrSearch.Length.ToString) - 1
                acc += " and (upper(PROBLEM) like '%" + arrSearch(i) + "%'"
                acc += " or upper(DETAIL) like '%" + arrSearch(i) + "%'"
                acc += " or upper(CAUSE) like '%" + arrSearch(i) + "%'"
                acc += " or upper(ADJUSTMC) like '%" + arrSearch(i) + "%'"
                acc += " or upper(RECOVERY1) like '%" + arrSearch(i) + "%'"
                acc += " or upper(RECOVERY2) like '%" + arrSearch(i) + "%'"
                acc += " or upper(RECOVERY3) like '%" + arrSearch(i) + "%'"
                acc += " or upper(RECOVERY4) like '%" + arrSearch(i) + "%'"
                acc += " or upper(RECOVERY5) like '%" + arrSearch(i) + "%'"
                acc += " or upper(CMT) like '%" + arrSearch(i) + "%'"
                acc += " or upper(MC) like '%" + arrSearch(i) + "%'"
                acc += " or upper(LOSSCODE) like '%" + arrSearch(i) + "%'"
                acc += " or upper(EVENT) like '%" + arrSearch(i) + "%')"
            Next
        End If
        acc += " order by to_date(DTE,'MM/DD/YYYY HH:MI AM') desc"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvMnHis.DataSource = dt
            gvMnHis.DataBind()
        End Using
    End Sub
    Protected Sub imgPstBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Try
                Dim bytes As Byte() = TryCast(TryCast(e.Row.DataItem, DataRowView)("IMG"), Byte())
                Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                TryCast(e.Row.FindControl("imgPst"), WebControls.Image).ImageUrl = Convert.ToString("data:image/png;base64,") & base64String
            Catch ex As Exception
                e.Row.Cells(1).Text = String.Empty
            End Try
        End If
    End Sub
    Private Sub load_gvPstHis()
        Dim conn As New OracleConnection(DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select ROWID"
        acc += ",round((to_date(case when TIMESELE is null then TIMERESP else TIMESELE end,'MM/DD/YYYY HH:MI AM') - to_date(TIMEREQ,'MM/DD/YYYY HH:MI AM')) * (60 * 24),2) as WS"
        acc += ",case when PSTSELE is null then '-' else PSTSELE end as PSTWS"
        acc += ",round((to_date(TIMERESP,'MM/DD/YYYY HH:MI AM') - to_date(case when TIMESELE is null then TIMERESP else TIMESELE end,'MM/DD/YYYY HH:MI AM')) * (60 * 24),2) as WSPST"
        acc += ",case when PSTRESP is null then '-' else PSTRESP end as PSTWR"
        acc += ",round((to_date(TIMEFIN,'MM/DD/YYYY HH:MI AM') - to_date(TIMERESP,'MM/DD/YYYY HH:MI AM')) * (60 * 24),2) as WR"
        acc += ",case when PSTFIN is null then '-' else PSTFIN end as PSTFN"
        acc += " from PSTH0001"
        acc += " where REPORT is not null"
        acc += " and to_char(to_date(TIMEREQ,'MM/DD/YYYY HH:MI AM'),'YYYYMMDDHH24MI')>=:db1 and to_char(to_date(TIMEREQ,'MM/DD/YYYY HH:MI AM'),'YYYYMMDDHH24MI')<=:db2"
        cmd.Parameters.Add(New OracleParameter("db1", Format(CDate(tbMnHisBeginDte.Text), "yyyyMMddHHmm")))
        cmd.Parameters.Add(New OracleParameter("db2", Format(CDate(tbMnHisUntilDte.Text).AddHours(23).AddMinutes(59), "yyyyMMddHHmm")))
        acc += " and DEPARTMENT='" + strDepartment + "' "
        'And FACTORY='" + strFactory + "'
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvPstHis.DataSource = dt
            gvPstHis.DataBind()
        End Using
    End Sub
    Private Sub ProcessHis()
        For i As Integer = 0 To gvMnHis.Rows.Count - 1
            Dim arr() As String = gvMnHis.Rows(i).Cells(2).Text.Split({"!"}, StringSplitOptions.RemoveEmptyEntries)
            Dim arrMc() As String = arr(0).Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
            Select Case CInt(arrMc.Length.ToString)
                Case 1 : gvMnHis.Rows(i).Cells(2).Text = "Machine : " & arrMc(0)
                Case 2 : gvMnHis.Rows(i).Cells(2).Text = "Machine : " & arrMc(0) & " (" & arrMc(1) & ")"
            End Select
            gvMnHis.Rows(i).Cells(2).Text = gvMnHis.Rows(i).Cells(2).Text & "</br>Loss code : " & arr(1)
            'Dim arrEvent() As String = arr(2).Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
            'Select Case CInt(arrEvent.Length.ToString)
            '    Case 1 : gvMnHis.Rows(i).Cells(2).Text = gvMnHis.Rows(i).Cells(2).Text & "</br>Event : " & arrEvent(0)
            '    Case 2 : gvMnHis.Rows(i).Cells(2).Text = gvMnHis.Rows(i).Cells(2).Text & "</br>Event : " & arrEvent(0) & ", " & arrEvent(1)
            'End Select
            Dim arrProblem() As String = arr(3).Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
            Select Case CInt(arrProblem.Length.ToString)
                Case 1 : gvMnHis.Rows(i).Cells(2).Text = gvMnHis.Rows(i).Cells(2).Text & "</br>Problem : " & arrProblem(0)
                Case 2 : gvMnHis.Rows(i).Cells(2).Text = gvMnHis.Rows(i).Cells(2).Text & "</br>Problem : " & arrProblem(0) & " (" & arrProblem(1) & ")"
            End Select
            gvMnHis.Rows(i).Cells(2).Text = gvMnHis.Rows(i).Cells(2).Text & "</br>Detail : " & arr(4)
            gvMnHis.Rows(i).Cells(2).Text = gvMnHis.Rows(i).Cells(2).Text & "</br>Failure cause : " & arr(5)
            arr = gvMnHis.Rows(i).Cells(3).Text.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
            Select Case CInt(arr.Length.ToString)
                Case 1 : gvMnHis.Rows(i).Cells(3).Text = arr(0)
                Case 4
                    gvMnHis.Rows(i).Cells(3).Text = arr(0)
                    gvMnHis.Rows(i).Cells(3).Text = gvMnHis.Rows(i).Cells(3).Text & "</br> - ปรับอะไรไป? : " & arr(1)
                    gvMnHis.Rows(i).Cells(3).Text = gvMnHis.Rows(i).Cells(3).Text & "</br> - ก่อนการแก้ไขเป็นยังไง : " & arr(2)
                    gvMnHis.Rows(i).Cells(3).Text = gvMnHis.Rows(i).Cells(3).Text & "</br> - หลังการแก้ไขเป็นยังไง : " & arr(3)
            End Select
            arr = gvMnHis.Rows(i).Cells(4).Text.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
            gvMnHis.Rows(i).Cells(4).Text = String.Empty
            Dim cntItem As Integer = 0
            For ii As Integer = 0 To 4
                Select Case arr(ii) = "-"
                    Case True 'Nothing
                    Case False
                        cntItem = cntItem + 1
                        Select Case cntItem
                            Case 1 : gvMnHis.Rows(i).Cells(4).Text = cntItem & ". " & arr(ii)
                            Case 2 To 5 : gvMnHis.Rows(i).Cells(4).Text = gvMnHis.Rows(i).Cells(4).Text & "</br>" & cntItem & ". " & arr(ii)
                        End Select
                End Select
            Next
            Select Case arr(5) <> "-"
                Case True
                    gvMnHis.Rows(i).Cells(4).Text = gvMnHis.Rows(i).Cells(4).Text & "</br></br>Comment : " & arr(5)
            End Select
            For ii As Integer = 0 To gvPstHis.Rows.Count - 1
                Select Case gvMnHis.Rows(i).Cells(5).Text = gvPstHis.Rows(ii).Cells(0).Text
                    Case True
                        gvMnHis.Rows(i).Cells(5).Text = "Waiting time : " & CStr(CLng(gvPstHis.Rows(ii).Cells(1).Text) + CLng(gvPstHis.Rows(ii).Cells(3).Text)) & " min."
                        gvMnHis.Rows(i).Cells(5).Text = gvMnHis.Rows(i).Cells(5).Text & "</br>PST > System : " & gvPstHis.Rows(ii).Cells(2).Text & " (" & gvPstHis.Rows(ii).Cells(3).Text & ")"
                        gvMnHis.Rows(i).Cells(5).Text = gvMnHis.Rows(i).Cells(5).Text & "</br>Reparing time : " & gvPstHis.Rows(ii).Cells(5).Text & " min."
                        gvMnHis.Rows(i).Cells(5).Text = gvMnHis.Rows(i).Cells(5).Text & "</br>PST > Respond : " & gvPstHis.Rows(ii).Cells(4).Text
                        gvMnHis.Rows(i).Cells(5).Text = gvMnHis.Rows(i).Cells(5).Text & "</br>PST > Finish job : " & gvPstHis.Rows(ii).Cells(6).Text
                        Exit For
                End Select
            Next
        Next
    End Sub
    Private Sub tbMnHisBeginDte_TextChanged(sender As Object, e As EventArgs) Handles tbMnHisBeginDte.TextChanged
        Call load_gvMnHis()
        Call load_gvPstHis()
        Call ProcessHis()
    End Sub
    Private Sub tbMnHisUntilDte_TextChanged(sender As Object, e As EventArgs) Handles tbMnHisUntilDte.TextChanged
        Call load_gvMnHis()
        Call load_gvPstHis()
        Call ProcessHis()
    End Sub
    Private Sub tbMnHisSearch_TextChanged(sender As Object, e As EventArgs) Handles tbMnHisSearch.TextChanged
        Call load_gvMnHis()
        Call load_gvPstHis()
        Call ProcessHis()
    End Sub
    Private Sub btnShowAnn_Init(sender As Object, e As EventArgs) Handles btnShowAnn.Init

        Timer1.Enabled = False
    End Sub
    Private Sub btnShowPst_Init(sender As Object, e As EventArgs) Handles btnShowPst.Init
        Timer1.Enabled = True
    End Sub
    Private Sub btnShowPst2_Init(sender As Object, e As EventArgs) Handles btnShowPst2.Init
        Timer1.Enabled = True
    End Sub
    Private Sub load_ddlChrNicPB()

        Timer1.Enabled = False

        Dim strMC As String = String.Empty
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select * from CHRONIC_PROBLEM where MACHINE = '" & Session("ChrMcName") & "' Order by TOPIC "

        cmd.CommandText = acc
        cmd.Connection = conn

        conn.Open()
        Dim rd As OracleDataReader = cmd.ExecuteReader()

        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(3)) = True Then strMC = String.Empty Else strMC = CStr(rd.GetValue(3))
            End While
        Finally
            rd.Close()
        End Try

        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlChrNicPB.DataSource = dt
            ddlChrNicPB.DataTextField = "TOPIC"
            ddlChrNicPB.DataValueField = "TOPIC"
            ddlChrNicPB.DataBind()


        End Using
        'Session("strmcEIEI") = strMC

        tbOtherProblem.Visible = False
        ddlChrNicPB.Visible = True
        ddlChrNicPB.Items.Insert(0, New ListItem("กรุณาเลือกปํญหา", String.Empty))
        ddlChrNicPB.Items.Add("อื่นๆ(Other)")

    End Sub
    Protected Sub ddlChrNicPB_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlChrNicPB.SelectedIndexChanged

        Select Case ddlChrNicPB.SelectedValue = "อื่นๆ(Other)"
            Case True
                tbOtherProblem.Visible = True
            Case False
                tbOtherProblem.Visible = False
        End Select
    End Sub
    '------------------------------------ Treeroj Add 11/10/2022 PST Finished Machine ----------------------------------------------------
    Private Sub tbPstMachine_TextChanged(sender As Object, e As EventArgs) Handles tbPstMachine.TextChanged
        lbChkPstResp.Visible = False
        lbChkPstMachine.InnerText = String.Empty

        Dim strMACHINEChk As String = String.Empty
        Dim connMACHINEChk As New OracleConnection(DB)
        Dim cmdMACHINEChk As New OracleCommand("select MACHINEID from PSTM0001 where MACHINEID='" & Session("chkMC") & "'", connMACHINEChk)
        connMACHINEChk.Open()
        Dim readerMACHINEChk As OracleDataReader = cmdMACHINEChk.ExecuteReader()
        '--------------------------------------------------------------------------------------------------------------
        Try
            While readerMACHINEChk.Read()
                If IsDBNull(readerMACHINEChk.GetValue(0)) = True Then strMACHINEChk = String.Empty Else strMACHINEChk = CStr(readerMACHINEChk.GetValue(0))
            End While
        Finally
            readerMACHINEChk.Close()
        End Try

        Select Case tbPstMachine.Text = strMACHINEChk
            Case True
                lbChkPstMachine.InnerText = strMACHINEChk
            Case False
                lbChkPstMachine.InnerText = String.Empty
                lbChkPstResp.Visible = True
                lbChkPstResp.InnerText = "ไม่พบชื่อเครื่องจักรหรือชื่อเครื่องจักรไม่ตรงกัน"
        End Select

    End Sub
    '-------------------------------------------------------------------------------------------------------------------------------
End Class

