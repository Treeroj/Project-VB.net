Imports System.Data.OleDb
Imports System.Data
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Net
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
Imports System.Diagnostics
'Imports Font = iTextSharp.text.Font
'Imports Font = System.Drawing.Font
'Imports iTextSharp.text
'Imports iTextSharp.text.pdf
'Imports iTextSharp.text.html.simpleparser


Partial Class test
    Inherits System.Web.UI.Page
    Dim MT800DB As String = ""
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            BindGrid()
            Session("1") = "วิธีคีย์ลางานในระบบ@CORE"
        End If
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

    End Sub
    Protected Sub Upload(sender As Object, e As EventArgs)
        Dim filename As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
        Dim contentType As String = FileUpload1.PostedFile.ContentType
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
        'Response.Redirect(Request.Url.AbsoluteUri)
    End Sub


    <System.Web.Services.WebMethod()>
    Public Shared Function GetPDF(ByVal fileId As Integer) As Object
        'Dim id As String = (TryCast(fileId, LinkButton).CommandArgument)
        Dim bytes As Byte()
        Dim fileName As String, contentType As String
        Dim constr As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString

        Using con As New OracleConnection("Data Source=MTL-ORACLE-SVR/MTLA;Persist Security Info=True;User ID=MT800DB;Password=mt800819;")
            Using cmd As New OracleCommand("SELECT NAME,CONTENTTYPE,DATA FROM ADMINTBFILESPDF WHERE NAME ='" & fileId & "'", con)
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

        Return New With {.FileName = fileName, .ContentType = contentType, .Data = bytes}

    End Function


    'Protected Sub loadFile(sender As Object, e As EventArgs)
    '    Dim id As String = (TryCast(sender, LinkButton).CommandArgument)
    '    Dim bytes As Byte()
    '    Dim fileName As String, contentType As String
    '    Using con As New OracleConnection(MT800DB)
    '        Using cmd As New OracleCommand("SELECT NAME,CONTENTTYPE,DATA FROM ADMINTBFILESPDF WHERE NAME ='" & id & "'", con)
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
    '            'con.Close()
    '        End Using
    '    End Using

    '    Return New With {.FileName = fileName, .ContentType = contentType, .Data = bytes}


    'End Sub

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

        'Session("pdfname") = fileName
        'Session("pdfname2") = bytes
        'Session("pdfname3") = id
        ''Dim url = "DisplayImage.aspx?AttachmentGUID=" & fileName
        ''Dim redirect As String = "<script>window.open('" & url & "');</script>"
        ''ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "popup", "window.open('" & url & "','_blank')", True)
        'System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "myFunction", "window.open('ViewDocument.ashx');", True)

        'Response.Clear()
        'Response.AddHeader("Content-Type", "application/pdf")
        'HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" & fileName)
        'Response.BinaryWrite(bytes)
        'Response.TransmitFile(fileName)
        'Response.End()


        'Dim pdfPath As String = Server.MapPath(fileName)
        'Dim client As WebClient = New WebClient()
        'Dim buffer As Byte() = client.DownloadData(pdfPath)
        'Response.ContentType = "application/pdf"
        'Response.AddHeader("content-length", buffer.Length.ToString())
        'Response.BinaryWrite(buffer)


        '        String pdfPath = Server.MapPath("~/SomePDFFile.pdf");
        ''WebClient client = New WebClient();
        '        Byte[] buffer = client.DownloadData(pdfPath);
        'Response.ContentType = "application/pdf";
        'Response.AddHeader("content-length", Buffer.Length.ToString());
        'Response.BinaryWrite(Buffer);

        'Diagnostics.Process.Start("fullpathtofiletoexecute")

        'Response.Write(redirect)


        'Dim id As String = (TryCast(sender, LinkButton).CommandArgument)
        'Session("Id") = id
        'ClientScript.RegisterStartupScript(Me.[GetType](), "open", "window.open('Second.aspx','_blank' );", True)


        'Dim dtime As DateTime = DateTime.Now.ToString()

        'Dim fname As String = "pdffile_" +
        '                  dtime.ToString("yyyyMMddHHmmss") + ".pdf"
        'Dim FilePath As String = Server.MapPath("writereaddata/" & fname)

        'Dim User As WebClient = New WebClient()
        'HttpContext.Current.Response.ContentType = "application/pdf"
        'HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Dim sw As New StringWriter()
        'Dim hw As New HtmlTextWriter(sw)
        'Dim pdfDoc As New Document(PageSize.A4, 10, 10, 8, 2)

        'Dim htmlparser As New HTMLWorker(pdfDoc)
        'PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream)
        'Dim sr As New StringReader(sw.ToString())

        'pdfDoc.Open()
        'Dim pthd As Paragraph = New Paragraph("WELCOME TO PDF FILE")
        'pdfDoc.Add(pthd)
        'htmlparser.Parse(sr)
        'pdfDoc.Close()
        'HttpContext.Current.Response.Write(pdfDoc)
        'HttpContext.Current.Response.End()
    End Sub
    Protected Sub View(ByVal sender As Object, ByVal e As EventArgs)


    End Sub
End Class
