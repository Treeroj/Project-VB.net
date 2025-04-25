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
Partial Class Default6
    Inherits System.Web.UI.Page
    Dim sql As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Load_ddlFeed()
        End If
    End Sub
    Private Sub Load_ddlFeed()
        Dim conn As New SqlConnection(sql)
        Dim cmd As New SqlCommand()
        Dim acc As String = "SELECT * FROM EfficiencyDB"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvHis.DataSource = dt
            gvHis.DataBind()
        End Using
    End Sub


End Class
