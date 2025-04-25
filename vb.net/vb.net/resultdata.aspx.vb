Imports System.Data.OleDb
'Imports System.Data.Odbc
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
Partial Class resultdata
    Inherits System.Web.UI.Page
    Dim MT800DB As String = ""
    Dim A4DL2 As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Call Load_ddlLine()
            Load_GVHIS()

        End If
    End Sub

    'Private Sub Load_ddlLine()
    '    Dim conectionstring As String = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=\\172.16.72.179\LP-DCM\01_DC_CenterFile\02_Data base\Database barcode material\01_PSMOD MATERIAL BARCODE\00_HISTORY DATA BASE\L4-2.accdb;Jet OLEDB:Database Password=2205;"
    '    Dim connection As New OleDbConnection(conectionstring)


    '    connection.Open()

    '        Dim query As String = "select * from tbl_History"
    '        Dim Adapter As New OleDbDataAdapter(query, connection)

    '        'Dim dataset As New DataSet()
    '        'adapter.Fill(dataset, "tbl_History")

    '        'DataGridview1.DataSource = dataset.Tables("tbl_History")
    '        'DataGridview1.DataBind()
    '        Dim DataTable As New DataTable()
    '        Adapter.Fill(DataTable)
    '        DataGridview1.DataSource = DataTable
    '        DataGridview1.DataBind()






    '    connection.Close()

    'End Sub
    Private Sub Load_GVHIS()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select LINE,PRODUCT,MACHINE,FEED,MODEL,LOTNO"
        acc += " from MATERIALBAR_SMT_HIS"

        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()

            sda.Fill(dt)
            GridView11.DataSource = dt
            GridView11.DataBind()

            ViewState("MyDataTable") = dt
        End Using
    End Sub

    'Private Sub Load_GVHIS2()
    '    Dim objDS As New DataSet
    '    Dim objDT As New DataTable

    '    objDS.Tables.Add(objDT)
    '    Dim objConn As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\172.16.72.179\LP-DCM\01_DC_CenterFile\02_Data base\Database barcode material\01_PSMOD MATERIAL BARCODE\00_HISTORY DATA BASE\L4-2.accdb;Jet OLEDB:Database Password=2205;"
    '    conn.ConnectionString = objConn
    '    Dim objDA As New OleDb.OleDbDataAdapter("select * from tbl_History", conn)
    '    objDA.Fill(objDT)
    '    DataGridview1.DataSource = objDT.DefaultView


    'End Sub


    'Private Sub Load_ddlLine()
    '    Dim conectionstring As String = "Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=\\172.16.72.179\LP-DCM\01_DC_CenterFile\02_Data base\Database barcode material\01_PSMOD MATERIAL BARCODE\00_HISTORY DATA BASE\L4-2.accdb;Uid=;Pwd=2205;"
    '    Dim connection As New OdbcConnection(conectionstring)


    '    connection.Open()
    '    Dim query As String = "select * from tbl_History"
    '    Dim Adapter As New OdbcDataAdapter(query, connection)
    '    Dim DataTable As New DataTable()
    '    Adapter.Fill(DataTable)
    '    DataGridview1.DataSource = DataTable
    '    DataGridview1.DataBind()

    '    connection.Close()

    'End Sub


    Private Sub Load_ddlLine()
        ' กำหนดชื่อ DSN ที่คุณตั้งค่าในระบบของคุณ
        ' กำหนด Connection String
        Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\172.16.72.179\LP-DCM\01_DC_CenterFile\02_Data base\Database barcode material\01_PSMOD MATERIAL BARCODE\00_HISTORY DATA BASE\L4-2.accdb;Jet OLEDB:Database Password=2205;"

            ' สร้าง Connection
            Using connection As New OleDbConnection(connectionString)
                Try
                    connection.Open()

                ' สร้าง SQL Command
                Dim sql As String = "SELECT * FROM tbl_History" ' ใช้ชื่อตาราง DFG
                Dim command As New OleDbCommand(sql, connection)

                    ' สร้าง DataAdapter
                    Dim adapter As New OleDbDataAdapter(command)

                    ' สร้าง DataSet เพื่อเก็บข้อมูล
                    Dim dataset As New DataSet()

                    ' เติมข้อมูลจากฐานข้อมูลลงใน DataSet
                    adapter.Fill(dataset)

                ' กำหนด DataGridView ให้แสดงข้อมูล
                DataGridview1.DataSource = dataset.Tables(0)
                DataGridview1.DataBind()
            Catch ex As Exception
                Response.Write("เกิดข้อผิดพลาด: " & ex.Message)
            Finally
                    ' ปิด Connection เมื่อเสร็จสิ้นการใช้งาน
                    connection.Close()
                End Try
            End Using
        End Sub
    End Class





