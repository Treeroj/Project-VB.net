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
Partial Class ADMIN_ASS_REGISUSER
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
                tbEmployeeId.Text = String.Empty
                tbEmployeeName.Text = String.Empty
                Panel3.Visible = False
                Panel4.Visible = False
                Panel5.Visible = False
                Panel6.Visible = False
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
                Dim cmd As New OracleCommand("select ASS_REGISUSER from MATERIALBAR_ADMIN where PASSWORD = '" & tbpassword.Text & "' and ADMIN = '" & tbUser.Text & "' and ASS_REGISUSER = 'Y'", conn)
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
                        Load_gvUSER()
                    Case False
                        tbEmployeeId.Text = String.Empty
                        tbEmployeeName.Text = String.Empty
                        Panel3.Visible = False
                        Panel4.Visible = False
                        Panel6.Visible = False
                        Panel7.Visible = False
                        tbUser.Text = String.Empty
                        tbpassword.Text = String.Empty
                        PNBUT.Visible = False
                        MesgBox("ท่านไม่มีสิทธ์ในการแก้ไขหน้านี้ กรุณาตรวจสอบหรือแจ้งผู้ดูแลระบบ")
                End Select

            Case False
                tbEmployeeId.Text = String.Empty
                tbEmployeeName.Text = String.Empty
                Panel3.Visible = False
                Panel4.Visible = False
                Panel6.Visible = False
                tbUser.Text = String.Empty
                tbpassword.Text = String.Empty
                PNBUT.Visible = False
                MesgBox("รหัส Admin ของท่านไม่ถูกต้อง")
        End Select

    End Sub
    Private Sub Load_gvUSER()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "SELECT ROWID,USERID,USERNAME,POSITION,PRODUCT,KEYHIS FROM MATERIALBAR_USERLIST where POSITION ='ASSEMBLY' and PRODUCT = 'PSMOD'"
        acc += "ORDER BY USERID asc"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            gvUSER.DataSource = dt
            gvUSER.DataBind()
        End Using
    End Sub
    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, TextBox2.TextChanged
        ApplyFilters()
    End Sub
    Private Sub ApplyFilters()
        Dim filterExpressions As New List(Of String)
        If Not String.IsNullOrEmpty(TextBox1.Text) Then
            filterExpressions.Add("USERID LIKE '%" & TextBox1.Text & "%'")
            Load_gvUSER()
        End If
        If Not String.IsNullOrEmpty(TextBox2.Text) Then
            filterExpressions.Add("USERNAME LIKE '%" & TextBox2.Text & "%'")
            Load_gvUSER()
        End If
        If TextBox1.Text = String.Empty Then
            Load_gvUSER()
        End If
        Dim filterExpression As String = String.Join(" AND ", filterExpressions)

        Dim dv As New DataView(DirectCast(gvUSER.DataSource, DataTable))
        dv.RowFilter = filterExpression

        gvUSER.DataSource = dv
        gvUSER.DataBind()
    End Sub

    Protected Sub gvUSER_RowEditing(sender As Object, e As GridViewEditEventArgs)
        gvUSER.EditIndex = e.NewEditIndex
        ApplyFilters()
    End Sub

    Protected Sub gvUSER_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvUSER.EditIndex = -1
        ApplyFilters()
    End Sub
    Protected Sub gvUSER_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim row As GridViewRow = gvUSER.Rows(e.RowIndex)
        Dim customerId As String = gvUSER.DataKeys(e.RowIndex).Values(0)
        Dim USERID As String = TryCast(row.Cells(0).Controls(0), TextBox).Text
        Dim USERNAME As String = TryCast(row.Cells(1).Controls(0), TextBox).Text
        Dim POSITION As String = TryCast(row.Cells(2).Controls(0), TextBox).Text
        Dim PRODUCT As String = TryCast(row.Cells(3).Controls(0), TextBox).Text
        Dim KEYHIS As String = TryCast(row.Cells(4).Controls(0), TextBox).Text


        Dim strUSERID As String = String.Empty
        Dim strUSERNAME As String = String.Empty
        Dim strPOSITION As String = String.Empty
        Dim strPRODUCT As String = String.Empty
        Dim strKEYHIS As String = String.Empty
        Dim conUSERID As New OracleConnection(MT800DB)
        Dim cmdUSERID As New OracleCommand("Select USERID,USERNAME,POSITION,PRODUCT,KEYHIS FROM MATERIALBAR_USERLIST WHERE ROWID = '" & customerId & "'", conUSERID)
        conUSERID.Open()
        Dim rd As OracleDataReader = cmdUSERID.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strUSERID = String.Empty Else strUSERID = CStr(rd.GetValue(0))
                If IsDBNull(rd.GetValue(1)) = True Then strUSERNAME = String.Empty Else strUSERNAME = CStr(rd.GetValue(1))
                If IsDBNull(rd.GetValue(2)) = True Then strPOSITION = String.Empty Else strPOSITION = CStr(rd.GetValue(2))
                If IsDBNull(rd.GetValue(3)) = True Then strPRODUCT = String.Empty Else strPRODUCT = CStr(rd.GetValue(3))
                If IsDBNull(rd.GetValue(4)) = True Then strKEYHIS = String.Empty Else strKEYHIS = CStr(rd.GetValue(4))
            End While
        Finally
            rd.Close()
        End Try
        conUSERID.Close()



        Using conn As New OracleConnection(MT800DB)
            conn.Open()
            Dim acc As String = "UPDATE MATERIALBAR_USERLIST SET USERID = :USERID, USERNAME = :USERNAME , POSITION = :POSITION, PRODUCT = :PRODUCT , KEYHIS = :KEYHIS where ROWID = '" & customerId & "'"
            Dim cmd As New OracleCommand(acc, conn)
            cmd.Parameters.Add(":USERID", OracleDbType.Varchar2).Value = USERID
            cmd.Parameters.Add(":USERNAME", OracleDbType.Varchar2).Value = USERNAME
            cmd.Parameters.Add(":POSITION", OracleDbType.Varchar2).Value = POSITION
            cmd.Parameters.Add(":PRODUCT", OracleDbType.Varchar2).Value = PRODUCT
            cmd.Parameters.Add(":KEYHIS", OracleDbType.Varchar2).Value = KEYHIS
            cmd.ExecuteNonQuery()
        End Using

        If strUSERID = USERID Then
            strUSERID = String.Empty
            USERID = String.Empty
        End If
        If strUSERNAME = USERNAME Then
            strUSERNAME = String.Empty
            USERNAME = String.Empty
        End If
        If strPOSITION = POSITION Then
            strPOSITION = String.Empty
            POSITION = String.Empty
        End If
        If strPRODUCT = PRODUCT Then
            strPRODUCT = String.Empty
            PRODUCT = String.Empty
        End If
        If strKEYHIS = KEYHIS Then
            strKEYHIS = String.Empty
            KEYHIS = String.Empty
        End If

        Dim objConn As New OracleConnection
        Dim strConnString As String = MT800DB
        Dim strSQL As String = "insert into MATERIALBAR_USERLIST_CHK" +
"(ADMINCHANGE,DTA,TIME,B_USERID,A_USERID,B_USERNAME,A_USERNAME,B_POSITION,A_POSITION,B_PRODUCT,A_PRODUCT,B_KEYHIS,A_KEYHIS)" +
"values" +
"(:sADMINCHANGE,:sDTA,:sTIME,:sB_USERID,:sA_USERID,:sB_USERNAME,:sA_USERNAME,:sB_POSITION,:sA_POSITION,:sB_PRODUCT,:sA_PRODUCT,:sB_KEYHIS,:sA_KEYHIS)"
        objConn.ConnectionString = strConnString
        objConn.Open()
        Dim objCmd As New OracleCommand(strSQL, objConn)
        objCmd.Parameters.Add("@sADMINCHANGE", tbUser.Text)
        objCmd.Parameters.Add("@sDTA", CStr(Format(Now, "dd-MMM-yyyy")))
        objCmd.Parameters.Add("@sTIME", CStr(Format(Now, "H:mm:ss tt")))
        objCmd.Parameters.Add("@sB_USERID", strUSERID)
        objCmd.Parameters.Add("@sA_USERID", USERID)
        objCmd.Parameters.Add("@sB_USERNAME", strUSERNAME)
        objCmd.Parameters.Add("@sA_USERNAME", USERNAME)
        objCmd.Parameters.Add("@sB_POSITION", strPOSITION)
        objCmd.Parameters.Add("@sA_POSITION", POSITION)
        objCmd.Parameters.Add("@sB_PRODUCT", strPRODUCT)
        objCmd.Parameters.Add("@sA_PRODUCT", PRODUCT)
        objCmd.Parameters.Add("@sB_KEYHIS", strKEYHIS)
        objCmd.Parameters.Add("@sA_KEYHIS", KEYHIS)
        objCmd.ExecuteNonQuery()
        objConn.Close()
        objConn = Nothing

        gvUSER.EditIndex = -1
        ApplyFilters()
    End Sub
    Protected Sub gvUSER_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim customerId As String = gvUSER.DataKeys(e.RowIndex).Values(0)
        Dim row As GridViewRow = gvUSER.Rows(e.RowIndex)
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

        Dim strUSERID As String = String.Empty
        Dim strUSERNAME As String = String.Empty
        Dim strPOSITION As String = String.Empty
        Dim strPRODUCT As String = String.Empty
        Dim strKEYHIS As String = String.Empty
        Dim conUSERID As New OracleConnection(MT800DB)
        Dim cmdUSERID As New OracleCommand("Select USERID,USERNAME,POSITION,PRODUCT,KEYHIS FROM MATERIALBAR_USERLIST WHERE ROWID = '" & customerId & "'", conUSERID)
        conUSERID.Open()
        Dim rd As OracleDataReader = cmdUSERID.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strUSERID = String.Empty Else strUSERID = CStr(rd.GetValue(0))
                If IsDBNull(rd.GetValue(1)) = True Then strUSERNAME = String.Empty Else strUSERNAME = CStr(rd.GetValue(1))
                If IsDBNull(rd.GetValue(2)) = True Then strPOSITION = String.Empty Else strPOSITION = CStr(rd.GetValue(2))
                If IsDBNull(rd.GetValue(3)) = True Then strPRODUCT = String.Empty Else strPRODUCT = CStr(rd.GetValue(3))
                If IsDBNull(rd.GetValue(4)) = True Then strKEYHIS = String.Empty Else strKEYHIS = CStr(rd.GetValue(4))
            End While
        Finally
            rd.Close()
        End Try
        conUSERID.Close()

        Dim objConn As New OracleConnection
        Dim strConnString As String = MT800DB
        Dim strSQL As String = "insert into MATERIALBAR_USERLIST_DELETE" +
"(ADMINCHANGE,DTA,TIME,USERID,USERNAME,POSITION,PRODUCT,KEYHIS)" +
"values" +
"(:sADMINCHANGE,:sDTA,:sTIME,:sUSERID,:sUSERNAME,:sPOSITION,:sPRODUCT,:sKEYHIS)"
        objConn.ConnectionString = strConnString
        objConn.Open()
        Dim objCmd As New OracleCommand(strSQL, objConn)
        objCmd.Parameters.Add("@sADMINCHANGE", tbUser.Text)
        objCmd.Parameters.Add("@sDTA", CStr(Format(Now, "dd-MMM-yyyy")))
        objCmd.Parameters.Add("@sTIME", CStr(Format(Now, "H:mm:ss tt")))
        objCmd.Parameters.Add("@sUSERID", strUSERID)
        objCmd.Parameters.Add("@sUSERNAME", strUSERNAME)
        objCmd.Parameters.Add("@sPOSITION", strPOSITION)
        objCmd.Parameters.Add("@sPRODUCT", strPRODUCT)
        objCmd.Parameters.Add("@sKEYHIS", strKEYHIS)

        objCmd.ExecuteNonQuery()
        objConn.Close()
        objConn = Nothing

        Using conn As New OracleConnection(MT800DB)
            conn.Open()
            Dim acc As String = "DELETE FROM MATERIALBAR_USERLIST WHERE ROWID = '" & customerId & "'"
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
        If tbEmployeeId.Text <> String.Empty And tbEmployeeName.Text <> String.Empty Then
            Dim objConn As New OracleConnection
            Dim strConnString As String = MT800DB
            Dim strSQL As String = "insert into MATERIALBAR_USERLIST" +
"(USERID,USERNAME,POSITION,PRODUCT)" +
"values" +
"(:sUSERID,:sUSERNAME,:sPOSITION,:sPRODUCT)"
            objConn.ConnectionString = strConnString
            objConn.Open()
            Dim objCmd As New OracleCommand(strSQL, objConn)
            objCmd.Parameters.Add("@sUSERID", tbEmployeeId.Text)
            objCmd.Parameters.Add("@sUSERNAME", tbEmployeeName.Text)
            objCmd.Parameters.Add("@sPOSITION", "ASSEMBLY")
            objCmd.Parameters.Add("@sPRODUCT", "PSMOD")
            objCmd.ExecuteNonQuery()
            objConn.Close()
            objConn = Nothing
            MesgBox("บันทึกข้อมูลเรียบร้อย")
            Load_gvUSER()
            tbEmployeeId.Text = String.Empty
            tbEmployeeName.Text = String.Empty
            Panel6.Visible = False
            PNBUT.Visible = False
        Else
            MesgBox("กรุณากรอกข้อมูลให้ครบ")
        End If

    End Sub
    Protected Sub tbEmployeeId_TextChanged(sender As Object, e As EventArgs) Handles tbEmployeeId.TextChanged
        Dim strtbEmployeeId As String = String.Empty
        Dim connstrtbEmployeeId As New OracleConnection(DWUSER)
        Dim cmdstrtbEmployeeId As New OracleCommand("select EMP_NAME from mv_hr_emp_act where EMP_CD = '" & tbEmployeeId.Text & "'", connstrtbEmployeeId)
        connstrtbEmployeeId.Open()
        Dim rdEmployeeId As OracleDataReader = cmdstrtbEmployeeId.ExecuteReader()
        Try
            While rdEmployeeId.Read()
                If IsDBNull(rdEmployeeId.GetValue(0)) = True Then strtbEmployeeId = String.Empty Else strtbEmployeeId = CStr(rdEmployeeId.GetValue(0))
            End While
        Finally
            rdEmployeeId.Close()
        End Try
        connstrtbEmployeeId.Close()
        If strtbEmployeeId = String.Empty Then
            MesgBox("ไม่พบรหัส " & tbEmployeeId.Text & " ในฐานข้อมูล")
            tbEmployeeId.Text = String.Empty
            tbEmployeeName.Text = String.Empty
            Panel6.Visible = False
            PNBUT.Visible = False
        Else
            Dim strChkid As String = String.Empty
            Dim connChkid As New OracleConnection(MT800DB)
            Dim cmdChkid As New OracleCommand("select USERNAME from MATERIALBAR_USERLIST where USERNAME = '" & strtbEmployeeId & "'", connChkid)
            connChkid.Open()
            Dim rdChkid As OracleDataReader = cmdChkid.ExecuteReader()
            Try
                While rdChkid.Read()
                    If IsDBNull(rdChkid.GetValue(0)) = True Then strChkid = String.Empty Else strChkid = CStr(rdChkid.GetValue(0))
                End While
            Finally
                rdChkid.Close()
            End Try
            connChkid.Close()

            If strtbEmployeeId = strChkid Then
                MesgBox("ชื่อ " & strtbEmployeeId & " และรหัสพนักงาน " & tbEmployeeId.Text & " ถูกลงทะเบียนแล้วกรุณาตรวจสอบ")
                tbEmployeeId.Text = String.Empty
                tbEmployeeName.Text = String.Empty
                Panel6.Visible = False
                PNBUT.Visible = False
            Else
                tbEmployeeName.Text = strtbEmployeeId
                Panel6.Visible = True
                PNBUT.Visible = True
            End If
        End If
    End Sub
End Class
