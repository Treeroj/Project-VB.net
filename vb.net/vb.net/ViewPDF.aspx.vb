Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.testutils
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Partial Class ViewPDF
    Inherits System.Web.UI.Page
    Dim pdf_name As String
    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    Page.Title = "PDF DISPLAY"
    '    pdf_name = Session("pdfname")
    '    Dim embed As String = "<object data=""{0}"" type=""application/pdf"" width=""2000px"" height=""1000px"">"
    '    embed += "If you are unable to view file, you can download from <a href = ""{0}"">here</a>"
    '    embed += " or download <a target = ""_blank"" href = ""http://get.adobe.com/reader/"">Adobe PDF Reader</a> to view the file."
    '    embed += "</object>"
    '    ltEmbed.Text = String.Format(embed, ResolveUrl("~/uploads/" + pdf_name))

    'End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Not Me.IsPostBack Then

            If Session("Id") IsNot Nothing Then
                Dim id As Integer = Convert.ToInt32(Session("Id"))
                Dim embed As String = "<object data=""{0}{1}"" type=""application/pdf"" width=""500px"" height=""300px"">"
                embed += "If you are unable to view file, you can download from <a href = ""{0}{1}&download=1"">here</a>"
                embed += " or download <a target = ""_blank"" href = ""http://get.adobe.com/reader/"">Adobe PDF Reader</a> to view the file."
                embed += "</object>"
                ltEmbed.Text = String.Format(embed, ResolveUrl("~/FileCS.ashx?Id="), id)
            End If
        End If
    End Sub

End Class
