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
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports ClosedXML.Excel
Imports Control = System.Web.UI.Control

Partial Class SMTMBCHIS
    Inherits System.Web.UI.Page

    Dim MT800DB As String = ""
    Dim MCS As String = ""
    Dim PROCESS As String = "SMT"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
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
        Dim cmd As New OracleCommand("select USERID from NVP_ALLPROCESS_USER where USERID = '" & tbUser.Text & "' and PROCESS = '" & PROCESS & "'", conn)
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

                Load_ddlLinePD()
            Case False
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
    'Private Sub Load_ddlLine()
    '    Dim conn As New OracleConnection(MT800DB)
    '    Dim cmd As New OracleCommand()
    '    Dim acc As String = " select LINEDCPSMOD From MATERIALBAR_LINEDCPSMOD where PRODUCT ='" & ddlLinePD.SelectedItem.Text & "'"
    '    cmd.CommandText = acc
    '    cmd.Connection = conn
    '    Using sda As New OracleDataAdapter(cmd)
    '        Dim dt As New DataTable()
    '        sda.Fill(dt)
    '        ddlLine.DataSource = dt
    '        ddlLine.DataTextField = "LINEDCPSMOD"
    '        ddlLine.DataValueField = "LINEDCPSMOD"
    '        ddlLine.DataBind()
    '        ddlLine.Items.Insert(0, New ListItem("(กรุณาเลือก โปรดักส์)", String.Empty))
    '    End Using
    'End Sub
    Protected Sub ddlLinePD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLinePD.SelectedIndexChanged
        Select Case ddlLinePD.SelectedIndex = 0
            Case True
                PanelMATERIALHIS.Visible = False
                Panel2.Visible = False
                Panel3.Visible = False
                Panel4.Visible = False
            Case False
                PanelMATERIALHIS.Visible = True
                Load_ddlFeed()
                Panel2.Visible = True
                Panel3.Visible = True
                Panel4.Visible = True
                'Load_ddlLine()
        End Select
    End Sub
    Private Sub Load_ddlFeed()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "SELECT * FROM NVP_ALLPROCESS_HIS where PRODUCTS ='" & ddlLinePD.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "' "
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvHis.DataSource = dt
            gvHis.DataBind()
        End Using
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvHis.PageIndex = e.NewPageIndex
        Me.ApplyFilters()
    End Sub

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, TextBox2.TextChanged, TextBox3.TextChanged, TextBox4.TextChanged, TextBox5.TextChanged, TextBox6.TextChanged, TextBox7.TextChanged, TextBox8.TextChanged, TextBox9.TextChanged, TextBox10.TextChanged
        ApplyFilters()
    End Sub

    Private Sub ApplyFilters()

        Dim filterExpressions As New List(Of String)

        If Not String.IsNullOrEmpty(TextBox1.Text) Then
            filterExpressions.Add("MODEL LIKE '%" & TextBox1.Text & "%'")
            Load_ddlFeed()
        End If

        If Not String.IsNullOrEmpty(TextBox2.Text) Then
            filterExpressions.Add("LOTNO LIKE '%" & TextBox2.Text & "%'")
            Load_ddlFeed()
        End If

        If Not String.IsNullOrEmpty(TextBox3.Text) Then
            filterExpressions.Add("MATERIAL LIKE '%" & TextBox3.Text & "%'")
            Load_ddlFeed()
        End If

        If Not String.IsNullOrEmpty(TextBox4.Text) Then
            filterExpressions.Add("INVOICE LIKE '%" & TextBox4.Text & "%'")
            Load_ddlFeed()
        End If

        If Not String.IsNullOrEmpty(TextBox5.Text) Then
            filterExpressions.Add("DAY LIKE '%" & TextBox5.Text & "%'")
            Load_ddlFeed()
        End If

        If Not String.IsNullOrEmpty(TextBox6.Text) Then
            filterExpressions.Add("MATERIALLOT LIKE '%" & TextBox6.Text & "%'")
            Load_ddlFeed()
        End If

        If Not String.IsNullOrEmpty(TextBox7.Text) Then
            filterExpressions.Add("CASSETTE LIKE '%" & TextBox7.Text & "%'")
            Load_ddlFeed()
        End If

        If Not String.IsNullOrEmpty(TextBox8.Text) Then
            filterExpressions.Add("LINE LIKE '%" & TextBox8.Text & "%'")
            Load_ddlFeed()
        End If

        If Not String.IsNullOrEmpty(TextBox9.Text) Then
            filterExpressions.Add("USERNAME LIKE '%" & TextBox9.Text & "%'")
            Load_ddlFeed()
        End If

        If Not String.IsNullOrEmpty(TextBox10.Text) Then
            filterExpressions.Add("REELNO LIKE '%" & TextBox10.Text & "%'")
            Load_ddlFeed()
        End If

        If TextBox1.Text = String.Empty And TextBox2.Text = String.Empty And TextBox3.Text = String.Empty And TextBox4.Text = String.Empty And
        TextBox5.Text = String.Empty And TextBox6.Text = String.Empty And TextBox7.Text = String.Empty Then
            Load_ddlFeed()
        End If

        Dim filterExpression As String = String.Join(" AND ", filterExpressions)

        Dim dv As New DataView(DirectCast(gvHis.DataSource, DataTable))
        dv.RowFilter = filterExpression

        gvHis.DataSource = dv
        gvHis.DataBind()
    End Sub
    Private Sub btnExportEC1_ServerClick(sender As Object, e As EventArgs) Handles btnExportEC1.Click
        Dim dt As New DataTable("GridView_Data")
        gvHis.AllowPaging = False
        ApplyFilters()
        For Each cell As TableCell In gvHis.HeaderRow.Cells
            dt.Columns.Add(cell.Text)
        Next
        For Each row As GridViewRow In gvHis.Rows
            dt.Rows.Add()
            For i As Integer = 0 To row.Cells.Count - 1
                dt.Rows(dt.Rows.Count - 1)(i) = row.Cells(i).Text
            Next
        Next
        Using wb As New XLWorkbook
            wb.Worksheets.Add(dt)
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=HistorySMT.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.[End]()
            End Using
        End Using
    End Sub
End Class
