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
Partial Class Cass_MN
    Inherits System.Web.UI.Page
    Dim PROCESS As String = "SMT"
    Dim POSITION As String = "Maintenance"
    Dim MT800DB As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Page.Form.Attributes.Add("enctype", "multipart/form-data")
        End If

    End Sub
    Private Sub MesgBox(ByVal sMessage As String)
        Dim msg As String
        msg = "<script language='javascript'>"
        msg += "alert('" & sMessage & "');"
        msg += "</script>"
        Response.Write(msg)
    End Sub
    Protected Sub tbUser1_TextChanged(sender As Object, e As EventArgs) Handles tbUser1.TextChanged
        Dim strUser As String = String.Empty
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand("select USERID from NVP_ALLPROCESS_USER where USERID = '" & tbUser1.Text & "' and PROCESS = '" & PROCESS & "' or PROCESS = 'ALL' and POSITION = '" & POSITION & "'", conn)
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
        Select Case tbUser1.Text = strUser And tbUser1.Text <> String.Empty
            Case True
                PNddlLinePD1.Visible = True
                Load_ddlLinePD1()
            Case False
                PNddlLinePD1.Visible = False
                tbUser1.Text = String.Empty
                MesgBox("รหัสพนักงานไม่ถูกต้อง")
        End Select
    End Sub
    Private Sub Load_ddlLinePD1()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select PRODUCTS From NVP_ALLPROCESS_PRODUCTS where PROCESS = '" & PROCESS & "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlLinePD1.DataSource = dt
            ddlLinePD1.DataTextField = "PRODUCTS"
            ddlLinePD1.DataValueField = "PRODUCTS"
            ddlLinePD1.DataBind()
            ddlLinePD1.Items.Insert(0, New ListItem("(กรุณาเลือก โปรดักส์)", String.Empty))
        End Using
    End Sub
    Protected Sub ddlLinePD1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLinePD1.SelectedIndexChanged
        Select Case ddlLinePD1.SelectedIndex = 0
            Case True
                PaneCassNumber1.Visible = False
            Case False
                PaneCassNumber1.Visible = True
                Load_ddlCassette1()
        End Select
    End Sub
    Private Sub Load_ddlCassette1()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select NO From NVP_SMT_CASSETTE where PROCESS = '" & PROCESS & "' and PRODUCTS = '" & ddlLinePD1.SelectedItem.Value & "' and CAUSE is not null order by no asc"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlCassette1.DataSource = dt
            ddlCassette1.DataTextField = "NO"
            ddlCassette1.DataValueField = "NO"
            ddlCassette1.DataBind()
            ddlCassette1.Items.Insert(0, New ListItem("(กรุณาเลือก โปรดักส์)", String.Empty))
        End Using
    End Sub
    Protected Sub ddlCassette1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCassette1.SelectedIndexChanged
        Select Case ddlCassette1.SelectedIndex = 0
            Case True
                PanelBroken1.Visible = False
                tbBroken1.Text = String.Empty
                PanelProblem1.Visible = False
            Case False

                Dim strMATERIAL As String = String.Empty
                Dim connn As New OracleConnection(MT800DB)
                Dim cmdd As New OracleCommand("select CAUSE from NVP_SMT_CASSETTE where PRODUCTS = '" & ddlLinePD1.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "' and NO = '" & ddlCassette1.SelectedItem.Text & "'", connn)
                connn.Open()
                Dim rd As OracleDataReader = cmdd.ExecuteReader()
                Try
                    While rd.Read()
                        If IsDBNull(rd.GetValue(0)) = True Then strMATERIAL = String.Empty Else strMATERIAL = CStr(rd.GetValue(0))
                    End While
                Finally
                    rd.Close()
                End Try
                connn.Close()
                tbBroken1.Text = strMATERIAL
                PanelBroken1.Visible = True
                PanelProblem1.Visible = True
            Case False
        End Select
    End Sub
    Protected Sub tbProblem1_TextChanged(sender As Object, e As EventArgs) Handles tbProblem1.TextChanged
        Select Case tbProblem1.Text <> String.Empty
            Case True
                PanelCause1.Visible = True
            Case False
                PanelCause1.Visible = False
        End Select
    End Sub
    Protected Sub tbcause1_TextChanged(sender As Object, e As EventArgs) Handles tbcause1.TextChanged
        Select Case tbcause1.Text <> String.Empty
            Case True
                PanelRecovery1.Visible = True
            Case False
                PanelRecovery1.Visible = False
        End Select
    End Sub
    Protected Sub tbRecovery1_TextChanged(sender As Object, e As EventArgs) Handles tbRecovery1.TextChanged
        Select Case tbRecovery1.Text <> String.Empty
            Case True
                PanelChange1.Visible = True
            Case False
                PanelChange1.Visible = False
        End Select
    End Sub
    Protected Sub tbChange1_TextChanged(sender As Object, e As EventArgs) Handles tbChange1.TextChanged
        Select Case tbChange1.Text <> String.Empty
            Case True
                PanelQTY1.Visible = True
            Case False
                PanelQTY1.Visible = False
        End Select
    End Sub
    Protected Sub tbQTY1_TextChanged(sender As Object, e As EventArgs) Handles tbQTY1.TextChanged
        Select Case tbQTY1.Text <> String.Empty
            Case True
                PanelPassword1.Visible = True
            Case False
                PanelPassword1.Visible = False
        End Select
    End Sub
    Protected Sub tbPassword1_TextChanged(sender As Object, e As EventArgs) Handles tbPassword1.TextChanged
        Select Case tbPassword1.Text <> String.Empty
            Case True
                PNBUT1.Visible = True
            Case False
                PNBUT1.Visible = False
        End Select
    End Sub

    Protected Sub tbUser2_TextChanged(sender As Object, e As EventArgs) Handles tbUser2.TextChanged
        Dim strUser As String = String.Empty
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand("select USERID from NVP_ALLPROCESS_USER where USERID = '" & tbUser2.Text & "' and PROCESS = '" & PROCESS & "' or PROCESS = 'ALL' and POSITION = '" & POSITION & "'", conn)
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
        Select Case tbUser2.Text = strUser And tbUser2.Text <> String.Empty
            Case True
                PNddlLinePD2.Visible = True
                Load_ddlLinePD2()
            Case False
                PNddlLinePD2.Visible = False
                tbUser2.Text = String.Empty
                MesgBox("รหัสพนักงานไม่ถูกต้อง")
        End Select
    End Sub
    Private Sub Load_ddlLinePD2()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select PRODUCTS From NVP_ALLPROCESS_PRODUCTS where PROCESS = '" & PROCESS & "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlLinePD2.DataSource = dt
            ddlLinePD2.DataTextField = "PRODUCTS"
            ddlLinePD2.DataValueField = "PRODUCTS"
            ddlLinePD2.DataBind()
            ddlLinePD2.Items.Insert(0, New ListItem("(กรุณาเลือก โปรดักส์)", String.Empty))
        End Using
    End Sub
    Protected Sub ddlLinePD2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLinePD2.SelectedIndexChanged
        Select Case ddlLinePD2.SelectedIndex = 0
            Case True
                PaneCassNumber2.Visible = False
            Case False
                PaneCassNumber2.Visible = True
                Load_ddlCassette2()
        End Select
    End Sub
    Private Sub Load_ddlCassette2()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select NO From NVP_SMT_CASSETTE where PROCESS = '" & PROCESS & "' and PRODUCTS = '" & ddlLinePD2.SelectedItem.Value & "' and CAUSE is null order by no asc"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlCassette2.DataSource = dt
            ddlCassette2.DataTextField = "NO"
            ddlCassette2.DataValueField = "NO"
            ddlCassette2.DataBind()
            ddlCassette2.Items.Insert(0, New ListItem("(กรุณาเลือก โปรดักส์)", String.Empty))
        End Using
    End Sub
    Protected Sub ddlCassette2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCassette2.SelectedIndexChanged
        Select Case ddlCassette2.SelectedIndex = 0
            Case True
                PanelBroken2.Visible = False
                ddlBroken2.SelectedIndex = 0
                'PanelProblem2.Visible = False
                PanelCause2.Visible = False
            Case False
                Load_ddlBroken2()

                PanelBroken2.Visible = True
                'PanelProblem2.Visible = True
                'PanelCause2.Visible = True
            Case False
        End Select
    End Sub
    Private Sub Load_ddlBroken2()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select CAUSE From NVP_SMT_CASSETTE_CAUSE where PROCESS = '" & PROCESS & "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlBroken2.DataSource = dt
            ddlBroken2.DataTextField = "CAUSE"
            ddlBroken2.DataValueField = "CAUSE"
            ddlBroken2.DataBind()
            ddlBroken2.Items.Insert(0, New ListItem("(กรุณาเลือก อาการเสีย)", String.Empty))
            ddlBroken2.Items.Add("อื่นๆ(Other)")
        End Using
    End Sub
    Protected Sub ddlBroken2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBroken2.SelectedIndexChanged
        Select Case ddlBroken2.SelectedIndex = 0
            Case False
                Select Case ddlBroken2.SelectedValue = "อื่นๆ(Other)"
                    Case True
                        PaneltbBroken2.Visible = True
                        PanelCause2.Visible = False

                    Case False
                        PaneltbBroken2.Visible = False
                        PanelCause2.Visible = True
                End Select
            Case True
                PanelCause2.Visible = False
        End Select
    End Sub
    Protected Sub tbBroken2_TextChanged(sender As Object, e As EventArgs) Handles tbBroken2.TextChanged
        Select Case tbBroken2.Text <> String.Empty
            Case True
                PanelCause2.Visible = True
            Case False
                PanelCause2.Visible = False
        End Select
    End Sub
    'Protected Sub tbProblem2_TextChanged(sender As Object, e As EventArgs) Handles tbProblem2.TextChanged
    '    Select Case tbProblem2.Text <> String.Empty
    '        Case True
    '            PanelCause2.Visible = True
    '        Case False
    '            PanelCause2.Visible = False
    '    End Select
    'End Sub
    Protected Sub tbcause2_TextChanged(sender As Object, e As EventArgs) Handles tbcause2.TextChanged
        Select Case tbcause2.Text <> String.Empty
            Case True
                PanelRecovery2.Visible = True
                Load_ddlRecovery2()
            Case False
                PanelRecovery2.Visible = False
        End Select
    End Sub
    Private Sub Load_ddlRecovery2()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select REPAIR From NVP_SMT_CASSETTE_REPAIR where PROCESS = '" & PROCESS & "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlRecovery2.DataSource = dt
            ddlRecovery2.DataTextField = "REPAIR"
            ddlRecovery2.DataValueField = "REPAIR"
            ddlRecovery2.DataBind()
            ddlRecovery2.Items.Insert(0, New ListItem("(กรุณาเลือก วิธีการแก้ไข)", String.Empty))
            ddlRecovery2.Items.Add("อื่นๆ(Other)")
        End Using
    End Sub
    'Protected Sub tbRecovery2_TextChanged(sender As Object, e As EventArgs) Handles tbRecovery2.TextChanged
    '    Select Case tbRecovery2.Text <> String.Empty
    '        Case True
    '            PanelChange2.Visible = True
    '        Case False
    '            PanelChange2.Visible = False
    '    End Select
    'End Sub

    Protected Sub ddlRecovery2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRecovery2.SelectedIndexChanged
        Select Case ddlRecovery2.SelectedIndex = 0
            Case True
                PanelChange2.Visible = False
            Case False
                PanelChange2.Visible = True
        End Select
    End Sub

    Protected Sub tbChange2_TextChanged(sender As Object, e As EventArgs) Handles tbChange2.TextChanged
        Select Case tbChange2.Text <> String.Empty
            Case True
                PanelQTY2.Visible = True
            Case False
                PanelQTY2.Visible = False
        End Select
    End Sub
    Protected Sub tbQTY2_TextChanged(sender As Object, e As EventArgs) Handles tbQTY2.TextChanged
        Select Case tbQTY2.Text <> String.Empty
            Case True
                PanelPassword2.Visible = True
            Case False
                PanelPassword2.Visible = False
        End Select
    End Sub
    Protected Sub tbPassword2_TextChanged(sender As Object, e As EventArgs) Handles tbPassword2.TextChanged
        Select Case tbPassword2.Text <> String.Empty
            Case True
                PNBUT2.Visible = True
            Case False
                PNBUT2.Visible = False
        End Select
    End Sub
End Class
