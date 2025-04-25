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
Partial Class Cass_Database
    Inherits System.Web.UI.Page
    Dim MT800DB As String = ""
    Dim MCS As String = ""
    Dim PROCESS As String = "CASSETTE"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Load_gvCassette()
        End If
    End Sub
    Private Sub MesgBox(ByVal sMessage As String)
        Dim msg As String
        msg = "<script language='javascript'>"
        msg += "alert('" & sMessage & "');"
        msg += "</script>"
        Response.Write(msg)
    End Sub
    Private Sub Load_gvCassette()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "SELECT NO,USE,STATUS,CAUSE,STATUSMN FROM NVP_SMT_CASSETTE"
        acc += " ORDER BY CASE STATUS"
        acc += " WHEN 'Broken' THEN 1"
        acc += " WHEN 'Overshot' THEN 2"
        acc += " WHEN 'Normal' THEN 3"
        acc += " ELSE 4 END"
        acc += " ,CASE STATUSMN"
        acc += " WHEN 'รอตรวจสอบ' THEN 1"
        acc += " ELSE 2 END,USE DESC"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvCassette.DataSource = dt
            gvCassette.DataBind()
            Session("myGridViewData") = dt
        End Using
    End Sub
    Protected Sub gvCassette_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCassette.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim status As String = DataBinder.Eval(e.Row.DataItem, "STATUS").ToString()
            Dim useValue As Integer = Integer.Parse(DataBinder.Eval(e.Row.DataItem, "USE").ToString())

            ' Set row color based on status
            Select Case status
                'Case "Normal"
                '    e.Row.BackColor = Drawing.Color.Green
                Case "Broken"
                    e.Row.BackColor = Drawing.Color.Orange
                Case "Overshot"
                    e.Row.BackColor = Drawing.Color.Orange
            End Select

            ' Set row color based on use value
            If useValue > 25000 And useValue <= 499999 Then
                e.Row.BackColor = Drawing.Color.Yellow
            ElseIf useValue > 499999 Then
                e.Row.BackColor = Drawing.Color.Orange
            End If

            ' Reorder rows based on status
            'Select Case status
            '    Case "Repair"
            '        gvCassette.Controls(0).Controls.AddAt(0, e.Row)
            '    Case "overshot"
            '        gvCassette.Controls(0).Controls.AddAt(gvCassette.Rows.Count - 2, e.Row)
            '        'Case "NORMAL"
            '        '    gvCassette.Controls(0).Controls.Add(e.Row)
            'End Select

        End If
    End Sub
End Class

