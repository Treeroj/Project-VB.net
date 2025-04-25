Imports System.Data
Imports System.Data.OleDb
Imports System.Windows
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types


Partial Class Default8
    Inherits System.Web.UI.Page
    Dim MT800DB As String = ""
    Dim ds As New DataSet()
    Dim connectionString As String = ""
    Dim dataL5 As String = "\\172.16.72.179\lp-dcm\01_DC_CenterFile\02_Data base\Database barcode material\01_PSMOD MATERIAL BARCODE\00_HISTORY DATA BASE\L5.accdb"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Page.Form.Attributes.Add("enctype", "multipart/form-data")
            If Not String.IsNullOrEmpty(dataL5) Then
                connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dataL5};Jet OLEDB:Database Password=2205;"
                LOAdDataGridView1()
            Else
                DataGridView1.DataSource = Nothing
            End If
        End If
    End Sub

    Private Sub selecttext_TextChanged(sender As Object, e As EventArgs) Handles selecttext.TextChanged
        Dim connectData As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select MATERIAL,MODEL from NVP_ALLPROCESS_HIS where MATERIAL like '%" & selecttext.Text & "%'"
        cmd.CommandText = acc
        cmd.Connection = connectData

        Using data As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            data.Fill(dt)

            HIS.DataSource = dt
            HIS.DataBind()

        End Using
    End Sub
    Private Sub LOAdDataGridView1()
        Using connection As New OleDbConnection(connectionString)
            Try
                connection.Open()

                Dim sql As String = "SELECT * FROM tbl_History"
                Dim command As New OleDbCommand(sql, connection)
                Dim adapter As New OleDbDataAdapter(command)
                ds.Clear()
                adapter.Fill(ds, "tbl_History")

                DataGridView1.DataSource = ds.Tables("tbl_History")
            Catch ex As Exception
                MessageBox.Show("เกิดข้อผิดพลาด: " & ex.Message)
            Finally
                connection.Close()
            End Try
        End Using
    End Sub

    Private Sub Loaddata_NVP_ALLPROCESS_HIS()
        'Dim connectData As New OracleConnection(MT800DB)
        'Dim cmd As New OracleCommand()
        'Dim acc As String = "select MATERIAL,MODEL from NVP_ALLPROCESS_HIS where MATERIAL = '" & selecttext.Text & "'"
        'cmd.CommandText = acc
        'cmd.Connection = connectData

        'Using data As New OracleDataAdapter(cmd)
        '    Dim dt As New DataTable()
        '    data.Fill(dt)

        '    HIS.DataSource = dt
        '    HIS.DataBind()

        'End Using

    End Sub






End Class
