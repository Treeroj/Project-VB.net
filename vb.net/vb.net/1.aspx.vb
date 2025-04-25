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
Partial Class ADMIN_ASS_CHANGEMAT
    Inherits System.Web.UI.Page
    Dim MT800DB As String = ""
    Dim DWUSER As String = ""
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
                Panel5.Visible = True

            Case False
                tbModel.Text = String.Empty
                tbMaterial.Text = String.Empty
                tbSCM.Text = String.Empty
                Panel3.Visible = False
                Panel4.Visible = False
                Panel5.Visible = False
                Panel7.Visible = False
                tbUser.Text = String.Empty
                tbpassword.Text = String.Empty
                PNBUT.Visible = False
                MesgBox("ไม่พบชื่อของท่านในระบบ Admin")
        End Select
    End Sub
    Protected Sub tbpassword_TextChanged(sender As Object, e As EventArgs)
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
                Dim cmd As New OracleCommand("select ASS_CHANGEMAT from MATERIALBAR_ADMIN where PASSWORD = '" & tbpassword.Text & "' and ADMIN = '" & tbUser.Text & "' and ASS_CHANGEMAT = 'Y'", conn)
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
                        Panel3.Visible = True
                        Panel4.Visible = True
                        Panel7.Visible = True
                        Load_gvMODEL()
                    Case False
                        tbModel.Text = String.Empty
                        tbMaterial.Text = String.Empty
                        tbSCM.Text = String.Empty
                        Panel3.Visible = False
                        Panel4.Visible = False
                        Panel7.Visible = False
                        tbUser.Text = String.Empty
                        tbpassword.Text = String.Empty
                        PNBUT.Visible = False
                        MesgBox("ท่านไม่มีสิทธ์ในการแก้ไขหน้านี้ กรุณาตรวจสอบหรือแจ้งผู้ดูแลระบบ")
                End Select

            Case False
                tbModel.Text = String.Empty
                tbMaterial.Text = String.Empty
                tbSCM.Text = String.Empty
                Panel3.Visible = False
                Panel4.Visible = False
                tbUser.Text = String.Empty
                tbpassword.Text = String.Empty
                PNBUT.Visible = False
                MesgBox("รหัส Admin ของท่านไม่ถูกต้อง")
        End Select

    End Sub
    Private Sub Load_gvMODEL()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "SELECT ROWID,MODEL,MATERIAL,SCM FROM MATERIALBARASS_MATERIALBAR"

        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvMODEL.DataSource = dt
            gvMODEL.DataBind()
        End Using
    End Sub
    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, TextBox2.TextChanged, TextBox3.TextChanged
        ApplyFilters()
    End Sub
    Private Sub ApplyFilters()
        Dim filterExpressions As New List(Of String)
        If Not String.IsNullOrEmpty(TextBox1.Text) Then
            filterExpressions.Add("MODEL LIKE '" & TextBox1.Text & "'")
            Load_gvMODEL()
        End If
        If Not String.IsNullOrEmpty(TextBox2.Text) Then
            filterExpressions.Add("MATERIAL LIKE '" & TextBox2.Text & "'")
            Load_gvMODEL()
        End If
        If Not String.IsNullOrEmpty(TextBox3.Text) Then
            filterExpressions.Add("SCM LIKE '" & TextBox3.Text & "'")
            Load_gvMODEL()
        End If
        If TextBox1.Text = String.Empty And TextBox3.Text = String.Empty And TextBox3.Text = String.Empty Then
            Load_gvMODEL()
        End If
        Dim filterExpression As String = String.Join(" AND ", filterExpressions)

        Dim dv As New DataView(DirectCast(gvMODEL.DataSource, DataTable))
        dv.RowFilter = filterExpression

        gvMODEL.DataSource = dv
        gvMODEL.DataBind()
    End Sub

    Protected Sub gvMODEL_RowEditing(sender As Object, e As GridViewEditEventArgs)
        gvMODEL.EditIndex = e.NewEditIndex
        ApplyFilters()
    End Sub

    Protected Sub gvMODEL_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvMODEL.EditIndex = -1
        ApplyFilters()
    End Sub
    Protected Sub gvMODEL_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim row As GridViewRow = gvMODEL.Rows(e.RowIndex)
        Dim customerId As String = gvMODEL.DataKeys(e.RowIndex).Values(0)
        Dim MODEL As String = TryCast(row.Cells(0).Controls(0), TextBox).Text
        Dim MATERIAL As String = TryCast(row.Cells(1).Controls(0), TextBox).Text
        Dim SCM As String = TryCast(row.Cells(2).Controls(0), TextBox).Text

        Dim strMODEL As String = String.Empty
        Dim strMATERIAL As String = String.Empty
        Dim strSCM As String = String.Empty
        Dim conUSERID As New OracleConnection(MT800DB)
        Dim cmdUSERID As New OracleCommand("Select MODEL,MATERIAL,SCM FROM MATERIALBARASS_MATERIALBAR WHERE ROWID = '" & customerId & "'", conUSERID)
        conUSERID.Open()
        Dim rd As OracleDataReader = cmdUSERID.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strMODEL = String.Empty Else strMODEL = CStr(rd.GetValue(0))
                If IsDBNull(rd.GetValue(1)) = True Then strMATERIAL = String.Empty Else strMATERIAL = CStr(rd.GetValue(1))
                If IsDBNull(rd.GetValue(2)) = True Then strSCM = String.Empty Else strSCM = CStr(rd.GetValue(2))

            End While
        Finally
            rd.Close()
        End Try
        conUSERID.Close()

        Using conn As New OracleConnection(MT800DB)
            conn.Open()
            Dim acc As String = "UPDATE MATERIALBARASS_MATERIALBAR SET MODEL = :MODEL, MATERIAL = :MATERIAL , SCM = :SCM WHERE ROWID = '" & customerId & "'"
            Dim cmd As New OracleCommand(acc, conn)
            cmd.Parameters.Add(":MODEL", OracleDbType.Varchar2).Value = MODEL
            cmd.Parameters.Add(":MATERIAL", OracleDbType.Varchar2).Value = MATERIAL
            cmd.Parameters.Add(":SCM", OracleDbType.Varchar2).Value = SCM
            cmd.ExecuteNonQuery()
        End Using

        If strMODEL = MODEL Then
            strMODEL = String.Empty
            MODEL = String.Empty
        End If
        If strMATERIAL = MATERIAL Then
            strMATERIAL = String.Empty
            MATERIAL = String.Empty
        End If
        If strSCM = SCM Then
            strSCM = String.Empty
            SCM = String.Empty
        End If

        Dim objConn As New OracleConnection
        Dim strConnString As String = MT800DB
        Dim strSQL As String = "insert into MATERIALBARASS_MATERIALBAR_CHK" +
"(ADMINCHANGE,DTA,TIME,B_MODEL,A_MODEL,B_MATERIAL,A_MATERIAL,B_SCM,A_SCM)" +
"values" +
"(:sADMINCHANGE,:sDTA,:sTIME,:sB_MODEL,:sA_MODEL,:sB_MATERIAL,:sA_MATERIAL,:sB_SCM,:sA_SCM)"
        objConn.ConnectionString = strConnString
        objConn.Open()
        Dim objCmd As New OracleCommand(strSQL, objConn)
        objCmd.Parameters.Add("@sADMINCHANGE", tbUser.Text)
        objCmd.Parameters.Add("@sDTA", CStr(Format(Now, "dd-MMM-yyyy")))
        objCmd.Parameters.Add("@sTIME", CStr(Format(Now, "H:mm:ss tt")))
        objCmd.Parameters.Add("@sB_MODEL", strMODEL)
        objCmd.Parameters.Add("@sA_MODEL", MODEL)
        objCmd.Parameters.Add("@sB_MATERIAL", strMATERIAL)
        objCmd.Parameters.Add("@sA_MATERIAL", MATERIAL)
        objCmd.Parameters.Add("@sB_SCM", strSCM)
        objCmd.Parameters.Add("@sA_SCM", SCM)
        objCmd.ExecuteNonQuery()
        objConn.Close()
        objConn = Nothing


        gvMODEL.EditIndex = -1
        ApplyFilters()
    End Sub
    Protected Sub gvMODEL_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim customerId As String = gvMODEL.DataKeys(e.RowIndex).Values(0)
        Dim row As GridViewRow = gvMODEL.Rows(e.RowIndex)
        Dim USERID As String = TryCast(row.Cells(0).Controls(0), TextBox).Text
        Dim USERNAME As String = TryCast(row.Cells(1).Controls(0), TextBox).Text
        Dim POSITION As String = TryCast(row.Cells(2).Controls(0), TextBox).Text
        Dim PRODUCT As String = TryCast(row.Cells(3).Controls(0), TextBox).Text
        hfCustomerId.Value = customerId ' Store the customer ID in a hidden field
        ClientScript.RegisterStartupScript(Me.GetType(), "showModal", "$('#confirmDeleteModal').modal('show');", True)
    End Sub
    Protected Sub btnConfirmDelete_Click(sender As Object, e As EventArgs)
        Dim customerId As String = hfCustomerId.Value
        ' Perform the delete operation here
        Using conn As New OracleConnection(MT800DB)
            conn.Open()
            Dim acc As String = "DELETE FROM MATERIALBARASS_MATERIALBAR WHERE ROWID = '" & customerId & "'"
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
    Private Sub Button1_ServerClick(sender As Object, e As EventArgs) Handles Button1.Click
        If tbModel.Text <> String.Empty And tbMaterial.Text <> String.Empty And tbSCM.Text <> String.Empty Then
            Dim objConn As New OracleConnection
            Dim strConnString As String = MT800DB
            Dim strSQL As String = "insert into MATERIALBARASS_MATERIALBAR" +
"(MODEL,MATERIAL,SCM,F5)" +
"values" +
"(:sMODEL,:sMATERIAL,:sSCM,:sF5)"
            objConn.ConnectionString = strConnString
            objConn.Open()
            Dim objCmd As New OracleCommand(strSQL, objConn)
            objCmd.Parameters.Add("@sMODEL", tbModel.Text)
            objCmd.Parameters.Add("@sMATERIAL", tbMaterial.Text)
            objCmd.Parameters.Add("@sSCM", tbSCM.Text)
            objCmd.Parameters.Add("@sF5", "0")
            objCmd.ExecuteNonQuery()
            objConn.Close()
            objConn = Nothing
            MesgBox("บันทึกข้อมูลเรียบร้อย")
            Load_gvMODEL()
            tbModel.Text = String.Empty
            tbMaterial.Text = String.Empty
            tbSCM.Text = String.Empty

            PNBUT.Visible = False
        Else
            MesgBox("กรุณากรอกข้อมูลให้ครบ")
        End If

    End Sub
    Protected Sub tbRegister_TextChanged(sender As Object, e As EventArgs) Handles tbModel.TextChanged, tbMaterial.TextChanged, tbSCM.TextChanged
        If tbModel.Text <> String.Empty And tbMaterial.Text <> String.Empty And tbSCM.Text <> String.Empty Then
            PNBUT.Visible = True
        Else
            PNBUT.Visible = False
        End If
    End Sub
End Class
