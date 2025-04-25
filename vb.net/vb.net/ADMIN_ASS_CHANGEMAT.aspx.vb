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
    Dim PROCESS As String = "ASSEMBLY"
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

        If tbUser.Text.Count > 7 Then


            Dim strEmpScan As String = tbUser.Text
            Try
                Dim empScanInt As Integer = (strEmpScan - 11779) / 29

                tbUser.Text = empScanInt
            Catch ex As Exception
            End Try

        End If

        Dim strAdmin As String = String.Empty
        Dim connAdmin As New OracleConnection(MT800DB)
        Dim cmdAdmin As New OracleCommand("select USERADMIN from NVP_ALLPROCESS_ADMIN where USERADMIN = '" & tbUser.Text & "'", connAdmin)
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
                Panel9.Visible = False
                MesgBox("ไม่พบชื่อของท่านในระบบ Admin")
        End Select
    End Sub
    Protected Sub tbpassword_TextChanged(sender As Object, e As EventArgs)
        Dim strUserPassword As String = String.Empty
        Dim connPassword As New OracleConnection(MT800DB)
        Dim cmdPassword As New OracleCommand("select PASSWORD from NVP_ALLPROCESS_ADMIN where PASSWORD = '" & tbpassword.Text & "' and USERADMIN = '" & tbUser.Text & "'", connPassword)
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
                Dim cmd As New OracleCommand("select ASS_CHANGEMAT from NVP_ALLPROCESS_ADMIN where PASSWORD = '" & tbpassword.Text & "' and USERADMIN = '" & tbUser.Text & "' and ASS_CHANGEMAT = 'Y' ", conn)
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
                        'Panel3.Visible = True
                        Panel4.Visible = True
                        Panel7.Visible = True
                        Panel9.Visible = True
                        Load_gvMODEL()
                        Load_gvHIS()
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
                        Panel9.Visible = False
                        MesgBox("ท่านไม่มีสิทธ์ในการแก้ไขหน้านี้ กรุณาตรวจสอบหรือแจ้งผู้ดูแลระบบ")
                End Select

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
                Panel9.Visible = False
                Panel5.Visible = False
                MesgBox("รหัส Admin ของท่านไม่ถูกต้อง")
        End Select

    End Sub
    Private Sub Load_gvMODEL()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "SELECT ROWID,MODEL,MATERIAL,SCM,PRODUCTS,PROCESS FROM NVP_ASSEMBLY_MAT"

        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvMODEL.DataSource = dt
            gvMODEL.DataBind()
            Session("myGridViewData") = dt
        End Using
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvMODEL.PageIndex = e.NewPageIndex
        gvMODEL.DataSource = CType(Session("myGridViewData"), DataTable)
        gvMODEL.DataBind()
        ApplyFilters()
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
            filterExpressions.Add("PRODUCTS LIKE '" & TextBox3.Text & "'")
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
        Dim PRODUCTS As String = TryCast(row.Cells(3).Controls(0), TextBox).Text
        Dim PROCESS As String = TryCast(row.Cells(4).Controls(0), TextBox).Text


        Dim strMODEL As String = String.Empty
        Dim strMATERIAL As String = String.Empty
        Dim strSCM As String = String.Empty
        Dim strPRODUCTS As String = String.Empty
        Dim strPROCESS As String = String.Empty



        Dim conUSERID As New OracleConnection(MT800DB)
        Dim cmdUSERID As New OracleCommand("Select MODEL,MATERIAL,SCM,PRODUCTS,PROCESS FROM NVP_ASSEMBLY_MAT WHERE ROWID = '" & customerId & "'", conUSERID)
        conUSERID.Open()
        Dim rd As OracleDataReader = cmdUSERID.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strMODEL = String.Empty Else strMODEL = CStr(rd.GetValue(0))
                If IsDBNull(rd.GetValue(1)) = True Then strMATERIAL = String.Empty Else strMATERIAL = CStr(rd.GetValue(1))
                If IsDBNull(rd.GetValue(2)) = True Then strSCM = String.Empty Else strSCM = CStr(rd.GetValue(2))
                If IsDBNull(rd.GetValue(3)) = True Then strPRODUCTS = String.Empty Else strPRODUCTS = CStr(rd.GetValue(3))
                If IsDBNull(rd.GetValue(4)) = True Then strPROCESS = String.Empty Else strPROCESS = CStr(rd.GetValue(4))
            End While
        Finally
            rd.Close()
        End Try
        conUSERID.Close()

        Using conn As New OracleConnection(MT800DB)
            conn.Open()
            Dim acc As String = "UPDATE NVP_ASSEMBLY_MAT SET MODEL = :MODEL, MATERIAL = :MATERIAL , SCM = :SCM ,  PRODUCTS = :PRODUCTS,  PROCESS = :PROCESS WHERE ROWID = '" & customerId & "'"
            Dim cmd As New OracleCommand(acc, conn)
            cmd.Parameters.Add(":MODEL", OracleDbType.Varchar2).Value = MODEL
            cmd.Parameters.Add(":MATERIAL", OracleDbType.Varchar2).Value = MATERIAL
            cmd.Parameters.Add(":SCM", OracleDbType.Varchar2).Value = SCM
            cmd.Parameters.Add(":PRODUCTS", OracleDbType.Varchar2).Value = PRODUCTS
            cmd.Parameters.Add(":PROCESS", OracleDbType.Varchar2).Value = PROCESS
            cmd.ExecuteNonQuery()
        End Using

        Dim strMODELRE As String = String.Empty
        Dim strMATERIALRE As String = String.Empty
        Dim strSCMRE As String = String.Empty
        Dim strPRODUCTSRE As String = String.Empty
        Dim strPROCESSRE As String = String.Empty

        If strMODEL = MODEL Then
            strMODELRE = strMODEL
        Else
            strMODELRE = "แก้ไขจาก '" & strMODEL & "' เป็น '" & MODEL & "'"
        End If

        If strMATERIAL = MATERIAL Then
            strMATERIALRE = strMATERIAL
        Else
            strMATERIALRE = "แก้ไขจาก '" & strMATERIAL & "' เป็น '" & MATERIAL & "'"
        End If

        If strSCM = SCM Then
            strSCMRE = strSCM
        Else
            strSCMRE = "แก้ไขจาก '" & strSCM & "' เป็น '" & SCM & "'"
        End If

        If strPRODUCTS = PRODUCTS Then
            strPRODUCTSRE = strPRODUCTS
        Else
            strPRODUCTSRE = "แก้ไขจาก '" & strPRODUCTS & "' เป็น '" & PRODUCTS & "'"
        End If


        If strPROCESS = PROCESS Then
            strPROCESSRE = strPROCESS
        Else
            strPROCESSRE = "แก้ไขจาก '" & strPROCESS & "' เป็น '" & PROCESS & "'"
        End If

        If strMODEL = MODEL And strMATERIAL = MATERIAL And strSCM = SCM And strPRODUCTS = PRODUCTS And strPROCESS = PROCESS Then
        Else

            Dim objConn As New OracleConnection
            Dim strConnString As String = MT800DB
            Dim strSQL As String = "insert into NVP_ALLPROCESS_LOG_CHANGEMAT" +
    "(ADMINCHANGE,DAY,TIME,MODEL,MATERIAL,SCM,F5,STATUT,PROCESSCHANGE,PRODUCTS,PROCESS)" +
    "values" +
    "(:sADMINCHANGE,:sDAY,:sTIME,:sMODEL,:sMATERIAL,:sSCM,:sF5,:sSTATUT,:sPROCESSCHANGE,:sPRODUCTS,:sPROCESS)"

            objConn.ConnectionString = strConnString
            objConn.Open()
            Dim objCmd As New OracleCommand(strSQL, objConn)
            objCmd.Parameters.Add("@sADMINCHANGE", tbUser.Text)
            objCmd.Parameters.Add("@sDAY", CStr(Format(Now, "dd-MMM-yyyy")))
            objCmd.Parameters.Add("@sTIME", CStr(Format(Now, "HH:mm:ss")))
            objCmd.Parameters.Add("@sMODEL", strMODELRE)
            objCmd.Parameters.Add("@sMATERIAL", strMATERIALRE)
            objCmd.Parameters.Add("@sSCM", strSCMRE)
            objCmd.Parameters.Add("@sF5", "0")
            objCmd.Parameters.Add("@sSTATUT", "EDIT")
            objCmd.Parameters.Add("@PROCESSCHANGE", strPROCESSRE)
            objCmd.Parameters.Add("@sPRODUCTS", strPRODUCTSRE)
            objCmd.Parameters.Add("@sPROCESS", PROCESS)
            objCmd.ExecuteNonQuery()
            objConn.Close()
            objConn = Nothing
            Load_gvHIS()
        End If
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

        Dim strMODEL As String = String.Empty
        Dim strMATERIAL As String = String.Empty
        Dim strSCM As String = String.Empty
        Dim strF5 As String = String.Empty
        Dim strPRODUCTS As String = String.Empty
        Dim strPROCESS As String = String.Empty
        Dim conUSERID As New OracleConnection(MT800DB)
        Dim cmdUSERID As New OracleCommand("Select MODEL,MATERIAL,SCM,F5,PRODUCTS,PROCESS FROM NVP_ASSEMBLY_MAT WHERE ROWID = '" & customerId & "'", conUSERID)
        conUSERID.Open()
        Dim rd As OracleDataReader = cmdUSERID.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strMODEL = String.Empty Else strMODEL = CStr(rd.GetValue(0))
                If IsDBNull(rd.GetValue(1)) = True Then strMATERIAL = String.Empty Else strMATERIAL = CStr(rd.GetValue(1))
                If IsDBNull(rd.GetValue(2)) = True Then strSCM = String.Empty Else strSCM = CStr(rd.GetValue(2))
                If IsDBNull(rd.GetValue(3)) = True Then strF5 = String.Empty Else strF5 = CStr(rd.GetValue(3))
                If IsDBNull(rd.GetValue(4)) = True Then strPRODUCTS = String.Empty Else strPRODUCTS = CStr(rd.GetValue(4))
                If IsDBNull(rd.GetValue(5)) = True Then strPROCESS = String.Empty Else strPROCESS = CStr(rd.GetValue(5))
            End While
        Finally
            rd.Close()
        End Try
        conUSERID.Close()

        Dim objConn As New OracleConnection
        Dim strConnString As String = MT800DB
        Dim strSQL As String = "insert into NVP_ALLPROCESS_LOG_CHANGEMAT" +
    "(ADMINCHANGE,DAY,TIME,MODEL,MATERIAL,SCM,F5,STATUT,PROCESSCHANGE,PRODUCTS,PROCESS)" +
    "values" +
    "(:sADMINCHANGE,:sDAY,:sTIME,:sMODEL,:sMATERIAL,:sSCM,:sF5,:sSTATUT,:sPROCESSCHANGE,:sPRODUCTS,:sPROCESS)"
        objConn.ConnectionString = strConnString
        objConn.Open()
        Dim objCmd As New OracleCommand(strSQL, objConn)
        objCmd.Parameters.Add("@sADMINCHANGE", tbUser.Text)
        objCmd.Parameters.Add("@sDAY", CStr(Format(Now, "dd-MMM-yyyy")))
        objCmd.Parameters.Add("@sTIME", CStr(Format(Now, "HH:mm:ss")))
        objCmd.Parameters.Add("@sMODEL", strMODEL)
        objCmd.Parameters.Add("@sMATERIAL", strMATERIAL)
        objCmd.Parameters.Add("@sSCM", strSCM)
        objCmd.Parameters.Add("@sF5", strF5)
        objCmd.Parameters.Add("@sSTATUT", "DELETE")
        objCmd.Parameters.Add("@PROCESSCHANGE", strPROCESS)
        objCmd.Parameters.Add("@sPRODUCTS", strPRODUCTS)
        objCmd.Parameters.Add("@sPROCESS", PROCESS)
        objCmd.ExecuteNonQuery()
        objConn.Close()
        objConn = Nothing

        ' Perform the delete operation here
        Using conn As New OracleConnection(MT800DB)
            conn.Open()
            Dim acc As String = "DELETE FROM NVP_ASSEMBLY_MAT WHERE ROWID = '" & customerId & "'"
            Dim cmd As New OracleCommand(acc, conn)
            cmd.ExecuteNonQuery()
        End Using
        ' Close the modal
        ClientScript.RegisterStartupScript(Me.GetType(), "closeModal", "$('#confirmDeleteModal').modal('hide');", True)
        ApplyFilters()
        Load_gvHIS()
    End Sub
    Protected Sub btnConfirmCDelete_Click(sender As Object, e As EventArgs)
        ClientScript.RegisterStartupScript(Me.GetType(), "closeModal", "$('#confirmDeleteModal').modal('hide');", True)
        ApplyFilters()
        Load_gvHIS()
    End Sub
    Private Sub Button1_ServerClick(sender As Object, e As EventArgs) Handles Button1.Click
        If tbModel.Text <> String.Empty And tbMaterial.Text <> String.Empty And tbSCM.Text <> String.Empty Then
            Dim objConn As New OracleConnection
            Dim strConnString As String = MT800DB
            Dim strSQL As String = "insert into NVP_ASSEMBLY_MAT" +
"(MODEL,MATERIAL,SCM,F5)" +
"values" +
"(:sMODEL,:sMATERIAL,:sSCM,:sF5)"

            Dim strSQLCHK As String = "insert into NVP_ALLPROCESS_LOG_CHANGEMAT" +
    "(ADMINCHANGE,DAY,TIME,MODEL,MATERIAL,SCM,F5,STATUT,PROCESS)" +
    "values" +
    "(:sADMINCHANGE,:sDAY,:sTIME,:sMODEL,:sMATERIAL,:sSCM,:sF5,:sSTATUT,:sPROCESS)"

            objConn.ConnectionString = strConnString
            objConn.Open()
            Dim objCmd As New OracleCommand(strSQL, objConn)
            objCmd.Parameters.Add("@sMODEL", tbModel.Text)
            objCmd.Parameters.Add("@sMATERIAL", tbMaterial.Text)
            objCmd.Parameters.Add("@sSCM", tbSCM.Text)
            objCmd.Parameters.Add("@sF5", "0")
            objCmd.ExecuteNonQuery()


            Dim objCmdCHK As New OracleCommand(strSQLCHK, objConn)
            objCmdCHK.Parameters.Add("@sADMINCHANGE", tbUser.Text)
            objCmdCHK.Parameters.Add("@sDAY", CStr(Format(Now, "dd-MMM-yyyy")))
            objCmdCHK.Parameters.Add("@sTIME", CStr(Format(Now, "HH:mm:ss")))
            objCmdCHK.Parameters.Add("@sMODEL", tbModel.Text)
            objCmdCHK.Parameters.Add("@sMATERIAL", tbMaterial.Text)
            objCmdCHK.Parameters.Add("@sSCM", tbSCM.Text)
            objCmdCHK.Parameters.Add("@sF5", "0")
            objCmdCHK.Parameters.Add("@sSTATUT", "INSERT")
            objCmdCHK.Parameters.Add("@sPROCESS", PROCESS)
            objCmdCHK.ExecuteNonQuery()
            objConn.Close()
            objConn = Nothing


            MesgBox("บันทึกข้อมูลเรียบร้อย")
            Load_gvMODEL()
            tbModel.Text = String.Empty
            tbMaterial.Text = String.Empty
            tbSCM.Text = String.Empty

            PNBUT.Visible = False
            Load_gvHIS()
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
    Private Sub Load_gvHIS()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "SELECT ADMINCHANGE,DAY,TIME,MODEL,MATERIAL,SCM,F5,STATUT FROM NVP_ALLPROCESS_LOG_CHANGEMAT where PROCESS = '" & PROCESS & "'"
        acc += " ORDER BY TO_DATE(DAY, 'DD-Mon-YYYY') DESC, TIME DESC"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvHIS.DataSource = dt
            gvHIS.DataBind()
        End Using
    End Sub

End Class

