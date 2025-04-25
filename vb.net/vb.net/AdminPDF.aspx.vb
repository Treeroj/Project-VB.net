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
Imports Oracle.DataAccess.Client.OracleParameterCollection
Imports Oracle.DataAccess.Client.OracleConnection
Imports System.Windows.Forms
Imports System.Threading
Imports System.Web.UI.DataVisualization.Charting
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Drawing
Partial Class AdminPDF
    Inherits System.Web.UI.Page

    Dim MT800DB As String = "Data Source=MTL-ORACLE-SVR/MTLA;Persist Security Info=True;User ID=MT800DB;Password=mt800819;"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            BindGrid()
            BindGrid2()
            BindGrid3()
            BindGrid4()
            BindGrid5()
            BindGrid6()
            BindGrid7()
            BindGrid8()
            BindGrid9()
            BindGrid10()
            Load_ddlLine()
            'Response.Write("<script>alert('my message'); ")
            'Response.Write("window.location='AdminPDF.aspx'</script>")
        End If
    End Sub
    Private Sub MesgBox(ByVal sMessage As String)

        Dim msg As String
        msg = "<script language='javascript'>"
        msg += "alert('" & sMessage & "');"
        msg += "</script>"
        Response.Write(msg)


    End Sub
    Private Sub Load_ddlLine()

        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select DATA From ADMINDDLPDF"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlLine.DataSource = dt
            ddlLine.DataTextField = "DATA"
            ddlLine.DataValueField = "DATA"
            ddlLine.DataBind()
            ddlLine.Items.Insert(0, New ListItem("(กรุณาเลือกหัวข้อที่จะลง)", String.Empty))

        End Using

        Session("1") = "วิธีคีย์ลางานในระบบ@CORE"
        Session("2") = "วิธีขอรับรองเวลา"
        Session("3") = "วิธีการขอทำล่วงเวลา"
        Session("4") = "วิธีการ RESET PASSWORD"
        Session("5") = "วิธีการเปลี่ยน PASSWORD @CORE (PASSWORD หมดอายุ)"
        Session("6") = "วิธีการขอเปลี่ยนแปลงที่อยู่"
        Session("7") = "วิธีการขอเปลี่ยนแปลงข้อมูลส่วนตัว"
        Session("8") = "วิธีการคีย์เบิกค่ารักษาพยาบาล"
        Session("9") = "วิธีการคีย์เบิก MONEY GIFT"
        Session("10") = "QR CODE"
    End Sub
    Private Sub BindGrid()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " Select ID, NAME FROM ADMINTBFILESPDF WHERE DATA2 ='" + Session("1") + "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            conn.Open()
            gvFiles.DataSource = cmd.ExecuteReader()
            gvFiles.DataBind()
            conn.Close()
        End Using
        Call Load_ddlLine()
    End Sub
    Private Sub BindGrid2()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " SELECT ID, NAME FROM ADMINTBFILESPDF WHERE DATA2 ='" + Session("2") + "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            conn.Open()
            gvFiles2.DataSource = cmd.ExecuteReader()
            gvFiles2.DataBind()

            conn.Close()
        End Using
        Call Load_ddlLine()
    End Sub
    Private Sub BindGrid3()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " SELECT ID, NAME FROM ADMINTBFILESPDF WHERE DATA2 ='" + Session("3") + "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            conn.Open()
            gvFiles3.DataSource = cmd.ExecuteReader()
            gvFiles3.DataBind()

            conn.Close()
        End Using
        Call Load_ddlLine()
    End Sub
    Private Sub BindGrid4()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " SELECT ID, NAME FROM ADMINTBFILESPDF WHERE DATA2 ='" + Session("4") + "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            conn.Open()
            gvFiles4.DataSource = cmd.ExecuteReader()
            gvFiles4.DataBind()

            conn.Close()
        End Using
        Call Load_ddlLine()
    End Sub
    Private Sub BindGrid5()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " SELECT ID, NAME FROM ADMINTBFILESPDF WHERE DATA2 ='" + Session("5") + "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            conn.Open()
            gvFiles5.DataSource = cmd.ExecuteReader()
            gvFiles5.DataBind()

            conn.Close()
        End Using
        Call Load_ddlLine()
    End Sub
    Private Sub BindGrid6()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " SELECT ID, NAME FROM ADMINTBFILESPDF WHERE DATA2 ='" + Session("6") + "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            conn.Open()
            gvFiles6.DataSource = cmd.ExecuteReader()
            gvFiles6.DataBind()

            conn.Close()
        End Using
        Call Load_ddlLine()
    End Sub
    Private Sub BindGrid7()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " SELECT ID, NAME FROM ADMINTBFILESPDF WHERE DATA2 ='" + Session("7") + "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            conn.Open()
            gvFiles7.DataSource = cmd.ExecuteReader()
            gvFiles7.DataBind()

            conn.Close()
        End Using
        Call Load_ddlLine()
    End Sub
    Private Sub BindGrid8()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " SELECT ID, NAME FROM ADMINTBFILESPDF WHERE DATA2 ='" + Session("8") + "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            conn.Open()
            gvFiles8.DataSource = cmd.ExecuteReader()
            gvFiles8.DataBind()

            conn.Close()
        End Using
        Call Load_ddlLine()
    End Sub
    Private Sub BindGrid9()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " SELECT ID, NAME FROM ADMINTBFILESPDF WHERE DATA2 ='" + Session("9") + "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            conn.Open()
            gvFiles9.DataSource = cmd.ExecuteReader()
            gvFiles9.DataBind()

            conn.Close()
        End Using
        Call Load_ddlLine()
    End Sub
    Private Sub BindGrid10()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " SELECT ID, NAME FROM ADMINTBFILESPDF WHERE DATA2 ='" + Session("10") + "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            conn.Open()
            gvFiles10.DataSource = cmd.ExecuteReader()
            gvFiles10.DataBind()

            conn.Close()
        End Using
        Call Load_ddlLine()
    End Sub
    'Protected Sub Upload(sender As Object, e As EventArgs)
    Protected Sub Upload(sender As Object, e As EventArgs)

        Dim filename As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
        Dim contentType As String = FileUpload1.PostedFile.ContentType
        Select Case ddlLine.SelectedItem.Value <> "" And filename <> ""
            Case True
                Using fs As Stream = FileUpload1.PostedFile.InputStream
                    Using br As New BinaryReader(fs)
                        Dim bytes As Byte() = br.ReadBytes(DirectCast(fs.Length, Long))
                        Dim constr As String = "Data Source=MTL-ORACLE-SVR/MTLA;Persist Security Info=True;User ID=MT800DB;Password=mt800819;"
                        'Dim constr As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
                        Using con As New OracleConnection
                            Dim query As String = "INSERT INTO ADMINTBFILESPDF" +
                        "(NAME,CONTENTTYPE,DATA,DATA2)" +
                        "values" +
                         "(:sNAME,:sCONTENTTYPE,:sDATA,:sDATA2)"
                            Using cmd As New OracleCommand(query, con)
                                con.ConnectionString = constr
                                cmd.Connection = con
                                cmd.Parameters.Add("@NAME", filename)
                                cmd.Parameters.Add("@CONTENTTYPE", contentType)
                                cmd.Parameters.Add("@DATA", bytes)
                                cmd.Parameters.Add("@DATA2", ddlLine.SelectedItem.Text)
                                con.Open()
                                cmd.ExecuteNonQuery()
                                con.Close()
                                Response.Write("<script>alert('ลงข้อมูลครบแล้วขอบคุณค่ะ'); ")
                                Response.Write("window.location='AdminPDF.aspx'</script>")
                                'MesgBox("กรุณาลงข้อมูลให้เครบ")
                                'ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "showDisplay();", True)
                                'System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "showDisplay();", True)
                            End Using
                        End Using
                    End Using
                End Using
            Case False

                'Response.Write("<script>alert('กรุณาลงข้อมูลให้ครบ'); ")
                'Response.Write("window.location='AdminPDF.aspx'</script>")
                MesgBox("กรุณาลงข้อมูลให้ครบ")

        End Select
        'Response.Redirect(Request.Url.AbsoluteUri)
    End Sub
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        'Dim message As String = "Order Placed Successfully."
        'Dim sb As New System.Text.StringBuilder()
        'sb.Append("<script type = 'text/javascript'>")
        'sb.Append("window.onload=function(){")
        'sb.Append("alert('")
        'sb.Append(message)
        'sb.Append("')};")
        'sb.Append("</script>")
        'ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", sb.ToString())
        'MesgBox("กรุณาลงข้อมูลให้เครบ")
        'Response.Write("<script>alert('my message'); ")
        'Response.Write("window.location='AdminPDF.aspx'</script>")
    End Sub
    Private Sub error1()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
        "javascript:alert('Total Fee Amount set for this student is - ... Enter Concession Amount lesser than the..!');", True)
    End Sub
    '<System.Web.Services.WebMethod()>
    'Public Shared Function GetPDF(ByVal fileId As String) As Object

    '    Dim bytes As Byte()
    '    Dim fileName As String, contentType As String
    '    Using con As New OracleConnection("Data Source=MTL-ORACLE-SVR/MTLA;Persist Security Info=True;User ID=MT800DB;Password=mt800819;")
    '        Using cmd As New OracleCommand("SELECT NAME,CONTENTTYPE,DATA FROM ADMINTBFILESPDF WHERE NAME ='" & fileId & "'", con)
    '            'cmd.CommandText =
    '            'cmd.Parameters.Add("@NAME", id)
    '            cmd.Connection = con
    '            con.Open()
    '            Using sdr As OracleDataReader = cmd.ExecuteReader()
    '                sdr.Read()
    '                bytes = DirectCast(sdr("Data"), Byte())
    '                contentType = sdr("ContentType").ToString()
    '                fileName = sdr("Name").ToString()
    '            End Using
    '            con.Close()
    '        End Using
    '    End Using
    '    Return New With {.FileName = fileName, .ContentType = contentType, .Data = bytes}
    'End Function
    Protected Sub DownloadFile(sender As Object, e As EventArgs)
        Dim id As String = (TryCast(sender, LinkButton).CommandArgument)
        Dim bytes As Byte()
        Dim fileName As String, contentType As String
        Using con As New OracleConnection(MT800DB)
            Using cmd As New OracleCommand("SELECT NAME,CONTENTTYPE,DATA FROM ADMINTBFILESPDF WHERE NAME ='" & id & "'", con)
                'cmd.CommandText =
                'cmd.Parameters.Add("@NAME", id)
                cmd.Connection = con
                con.Open()
                Using sdr As OracleDataReader = cmd.ExecuteReader()
                    sdr.Read()
                    bytes = DirectCast(sdr("Data"), Byte())
                    contentType = sdr("ContentType").ToString()
                    fileName = sdr("Name").ToString()
                End Using
                con.Close()
            End Using
        End Using
        Response.Clear()
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = contentType
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.End()
    End Sub
    Protected Sub loadFile(sender As Object, e As EventArgs)

        Dim id As String = (TryCast(sender, LinkButton).CommandArgument)
        Dim bytes As Byte()
        Dim fileName As String, contentType As String
        Using con As New OracleConnection(MT800DB)
            Using cmd As New OracleCommand("SELECT NAME,CONTENTTYPE,DATA FROM ADMINTBFILESPDF WHERE NAME ='" & id & "'", con)
                'cmd.CommandText =
                'cmd.Parameters.Add("@NAME", id)
                cmd.Connection = con
                con.Open()
                Using sdr As OracleDataReader = cmd.ExecuteReader()
                    sdr.Read()
                    bytes = DirectCast(sdr("Data"), Byte())
                    contentType = sdr("ContentType").ToString()
                    fileName = sdr("Name").ToString()
                End Using
                con.Close()
            End Using
        End Using
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf")
        HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" & fileName)
        HttpContext.Current.Response.BinaryWrite(bytes)
        HttpContext.Current.Response.End()

    End Sub
End Class
