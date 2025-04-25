Imports System.IO
Partial Class Default4
    Inherits System.Web.UI.Page
    Private Sub Button1_ServerClick(sender As Object, e As EventArgs) Handles Button1.ServerClick
        Dim searchText As String = Record.Text.Trim() ' Get user input from the TextBox
        Dim iniFilePath As String = Server.MapPath("test01.ini") ' Path to the .ini file
        ' Check if the .ini file exists
        If File.Exists(iniFilePath) Then
            Dim lines As String() = File.ReadAllLines(iniFilePath)
            ' Search for the specific data
            For Each line As String In lines
                If line.StartsWith(searchText & ",") Then
                    ResultLabel.Text = line ' Display the found data in a label
                    Exit For ' Exit the loop once the data is found
                End If
            Next
            Dim StrArr3(3) As String
            Dim Str3 As String = ResultLabel.Text
            StrArr3 = Str3.Split(",")
            StrArr3(0) = StrArr3(0).Replace(",", "")
            StrArr3(1) = StrArr3(1).Replace(",", "")
            StrArr3(2) = StrArr3(2).Replace(",", "")
            Session("1") = StrArr3(0)
            Session("2") = StrArr3(1)
            Session("3") = StrArr3(2)
            ' If the data is not found, display a message
            If String.IsNullOrEmpty(ResultLabel.Text) Then
                ResultLabel.Text = "Data not found."
            End If
        Else
            ResultLabel.Text = "The .ini file does not exist."
        End If
    End Sub
End Class

