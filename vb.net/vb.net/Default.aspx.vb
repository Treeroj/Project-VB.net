
Partial Class _Default
    Inherits System.Web.UI.Page
    Sub Page_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        If Not Session("uname") Is Nothing Then
            'Response.Redirect("admin")
        End If
    End Sub
End Class
