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
Imports TextBox = System.Web.UI.WebControls.TextBox
Imports Label = System.Web.UI.WebControls.Label

Partial Class SMTMBCBin
    Inherits System.Web.UI.Page
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
    Protected Sub tbUser_TextChanged(sender As Object, e As EventArgs) Handles tbUser.TextChanged
        Dim strAdmin As String = String.Empty
        Dim connAdmin As New OracleConnection(MT800DB)
        Dim cmdAdmin As New OracleCommand("select ADMIN from MATERIALBAR_ADMIN where ADMIN = '" & tbUser.Text & "'", connAdmin)
        connAdmin.Open()
        Dim rdAldmin As OracleDataReader = cmdAdmin.ExecuteReader()
        Try
            While rdAldmin.Read()
                If IsDBNull(rdAldmin.GetValue(0)) = True Then strAdmin = String.Empty Else strAdmin = CStr(rdAldmin.GetValue(0))
            End While
        Finally
            rdAldmin.Close()
        End Try
        connAdmin.Close()
        Select Case tbUser.Text = strAdmin And tbUser.Text <> String.Empty
            Case True


            Case False
                tbUser.Text = String.Empty
                tbpassword.Text = String.Empty
                MesgBox("ไม่พบชื่อของท่านในระบบ Admin")
        End Select
    End Sub

    Protected Sub tbpassword_TextChanged(sender As Object, e As EventArgs) Handles tbpassword.TextChanged

        Dim strUserPassword As String = String.Empty
        Dim connPassword As New OracleConnection(MT800DB)
        Dim cmdPassword As New OracleCommand("select PASSWORD from MATERIALBAR_ADMIN where PASSWORD = '" & tbpassword.Text & "' and ADMIN = '" & tbUser.Text & "'", connPassword)
        connPassword.Open()
        Dim rdPassword As OracleDataReader = cmdPassword.ExecuteReader()
        Try
            While rdPassword.Read()
                If IsDBNull(rdPassword.GetValue(0)) = True Then strUserPassword = String.Empty Else strUserPassword = CStr(rdPassword.GetValue(0))
            End While
        Finally
            rdPassword.Close()
        End Try
        connPassword.Close()
        Select Case tbpassword.Text = strUserPassword And tbpassword.Text <> String.Empty
            Case True

                Dim strUser As String = String.Empty
                Dim conn As New OracleConnection(MT800DB)
                Dim cmd As New OracleCommand("select SMT_BIN from MATERIALBAR_ADMIN where PASSWORD = '" & tbpassword.Text & "' and ADMIN = '" & tbUser.Text & "' and SMT_BIN = 'Y'", conn)
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
                Select Case strUser = "Y"
                    Case True
                        Load_ddlLinePD()

                    Case False
                        tbUser.Text = String.Empty
                        tbpassword.Text = String.Empty
                        MesgBox("ท่านไม่มีสิทธ์ในการแก้ไขหน้านี้ กรุณาตรวจสอบหรือแจ้งผู้ดูแลระบบ")
                End Select

            Case False
                tbUser.Text = String.Empty
                tbpassword.Text = String.Empty
                MesgBox("รหัส Admin ของท่านไม่ถูกต้อง")
        End Select

    End Sub
    Private Sub Load_ddlLinePD()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select PRODUCT From MATERIALBAR_LINEDCPSMODPD "
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            DropDownList1.DataSource = dt
            DropDownList1.DataTextField = "PRODUCT"
            DropDownList1.DataValueField = "PRODUCT"
            DropDownList1.DataBind()
            DropDownList1.Items.Insert(0, New ListItem("(กรุณาเลือก โปรดักส์)", String.Empty))
        End Using
    End Sub
    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        Select Case DropDownList1.SelectedIndex = 0
            Case True

            Case False
                Load_gvBin()
        End Select
    End Sub
    Private Sub Load_gvBin()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "SELECT ROWID,MATERIAL,BINNO FROM MATERIALBAR_SMT_BIN where PRODUCT ='" & DropDownList1.SelectedItem.Text & "'"
        acc += "ORDER BY BINNO asc"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvBin.DataSource = dt
            gvBin.DataBind()
        End Using
    End Sub
    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, TextBox2.TextChanged
        ApplyFilters()
    End Sub

    Private Sub ApplyFilters()

        Dim filterExpressions As New List(Of String)

        If Not String.IsNullOrEmpty(TextBox1.Text) Then
            filterExpressions.Add("MATERIAL LIKE '%" & TextBox1.Text & "%'")
            Load_gvBin()
        End If
        If Not String.IsNullOrEmpty(TextBox2.Text) Then
            filterExpressions.Add("BINNO LIKE '%" & TextBox2.Text & "%'")
            Load_gvBin()
        End If
        If TextBox1.Text = String.Empty And TextBox2.Text = String.Empty Then
            Load_gvBin()
        End If

        Dim filterExpression As String = String.Join(" AND ", filterExpressions)

        Dim dv As New DataView(DirectCast(gvBin.DataSource, DataTable))
        dv.RowFilter = filterExpression

        gvBin.DataSource = dv
        gvBin.DataBind()
    End Sub

    Protected Sub gvBin_RowEditing(sender As Object, e As GridViewEditEventArgs)
        gvBin.EditIndex = e.NewEditIndex
        ApplyFilters()
    End Sub

    Protected Sub gvBin_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvBin.EditIndex = -1
        ApplyFilters()
    End Sub

    Protected Sub gvBin_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim row As GridViewRow = gvBin.Rows(e.RowIndex)
        Dim customerId As String = gvBin.DataKeys(e.RowIndex).Values(0)
        Dim name As String = TryCast(row.Cells(0).Controls(0), TextBox).Text
        Dim country As String = TryCast(row.Cells(1).Controls(0), TextBox).Text

        Using conn As New OracleConnection(MT800DB)
            conn.Open()
            Dim acc As String = "UPDATE MATERIALBAR_SMT_BIN SET BINNO = :BinNo, MATERIAL = :Material WHERE ROWID = '" & customerId & "'"
            Dim cmd As New OracleCommand(acc, conn)
            cmd.Parameters.Add(":BinNo", OracleDbType.Varchar2).Value = country
            cmd.Parameters.Add(":Material", OracleDbType.Varchar2).Value = name
            cmd.ExecuteNonQuery()
        End Using

        gvBin.EditIndex = -1
        ApplyFilters()
    End Sub
    'Protected Sub gvBin_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
    '    Dim row As GridViewRow = gvBin.Rows(e.RowIndex)
    '    Dim customerId As String = gvBin.DataKeys(e.RowIndex).Values(0)
    '    If MessageBox.Show(String.Format("คุณต้องการลบข้อมูล ใช่/Yes หรือ ไม่/No"), "ยืนยันการลบข้อมูล", MessageBoxButtons.YesNo) = DialogResult.Yes Then
    '        Using conn As New OracleConnection(MT800DB)
    '            conn.Open()
    '            Dim acc As String = "DELETE FROM MATERIALBAR_SMT_BIN WHERE ROWID = '" & customerId & "'"
    '            Dim cmd As New OracleCommand(acc, conn)
    '            cmd.ExecuteNonQuery()
    '        End Using
    '    End If
    '    ApplyFilters()
    'End Sub
    Protected Sub gvBin_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim customerId As String = gvBin.DataKeys(e.RowIndex).Values(0)

        Dim row As GridViewRow = gvBin.Rows(e.RowIndex)
        Dim name As String = TryCast(row.Cells(0).Controls(0), TextBox).Text
        Dim country As String = TryCast(row.Cells(1).Controls(0), TextBox).Text


        hfCustomerId.Value = customerId ' Store the customer ID in a hidden field
        ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "$('#confirmDeleteModal').modal('show');", True)
    End Sub

    Protected Sub btnConfirmDelete_Click(sender As Object, e As EventArgs)
        Dim customerId As String = hfCustomerId.Value
        ' Perform the delete operation here
        Using conn As New OracleConnection(MT800DB)
            conn.Open()
            Dim acc As String = "DELETE FROM MATERIALBAR_SMT_BIN WHERE ROWID = '" & customerId & "'"
            Dim cmd As New OracleCommand(acc, conn)
            cmd.ExecuteNonQuery()
        End Using
        ' Close the modal
        ClientScript.RegisterStartupScript(Me.GetType(), "closeModal", "$('#confirmDeleteModal').modal('hide');", True)
        ApplyFilters()
    End Sub
    Protected Sub btnConfirmCDelete_Click(sender As Object, e As EventArgs)
        ClientScript.RegisterStartupScript(Me.GetType(), "closeModal", "$('#confirmDeleteModal').modal('hide');", True)
        ApplyFilters()
    End Sub
    Protected Sub gvBin_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Insert" Then
            Dim material As String = DirectCast(gvBin.FooterRow.FindControl("txtNewMaterial"), TextBox).Text
            Dim binNo As String = DirectCast(gvBin.FooterRow.FindControl("txtNewBinNo"), TextBox).Text

            Using conn As New OracleConnection(MT800DB)
                conn.Open()
                Dim acc As String = "INSERT INTO MATERIALBAR_SMT_BIN (MATERIAL, BINNO, PRODUCT) VALUES (@Material, @BinNo, @Product)"
                Dim cmd As New OracleCommand(acc, conn)
                cmd.Parameters.Add("@Material", material)
                cmd.Parameters.Add("@BinNo", binNo)
                cmd.Parameters.Add("@Product", DropDownList1.SelectedItem.Text)
                cmd.ExecuteNonQuery()
            End Using

            ApplyFilters()
        End If
    End Sub


End Class
