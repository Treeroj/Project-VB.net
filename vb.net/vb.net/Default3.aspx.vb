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
Partial Class Default3
    Inherits System.Web.UI.Page
    Dim MT800DB As String = ""
    Dim PRASSDB As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            'Dim conn3 As New OracleConnection(PRASSDB)
            'conn3.Open()
            '' สร้างคำสั่ง SQL เพื่อ join ข้อมูลจากตาราง G11 และ G22
            'Dim sql12 As String = "SELECT DEF_MODE FROM DEFECTIVE_MT800"
            '' สร้าง SqlDataAdapter สำหรับดึงข้อมูล
            'Dim adapter12 As New OracleDataAdapter(sql12, conn3)

            '' สร้าง DataSet เพื่อเก็บข้อมูล
            'Dim dataset12 As New DataSet()

            '' นำข้อมูลจาก G11 และ G22 มาเก็บใน DataSet
            'adapter12.Fill(dataset12)

            '' ปิดการเชื่อมต่อ
            'conn3.Close()




            '' สร้างการเชื่อมต่อกับฐานข้อมูล 1
            'Dim conn1 As New OracleConnection(PRASSDB)
            'conn1.Open()
            '' สร้างการเชื่อมต่อกับฐานข้อมูล 2
            'Dim conn2 As New OracleConnection(MT800DB)
            'conn2.Open()
            '' สร้างคำสั่ง SQL เพื่อ join ข้อมูลจากตาราง G11 และ G22
            'Dim sql As String = "SELECT T1.*,T2.* FROM DEFECTIVE_MT800 T1 INNER JOIN PST_DEF_THAI T2 ON T1.DEF_MODE = T2.CODE"
            '' สร้าง SqlDataAdapter สำหรับดึงข้อมูล
            'Dim adapter As New OracleDataAdapter(sql, conn1)

            '' สร้าง DataSet เพื่อเก็บข้อมูล
            'Dim dataset As New DataSet()

            '' นำข้อมูลจาก G11 และ G22 มาเก็บใน DataSet
            'adapter.Fill(dataset)

            '' ปิดการเชื่อมต่อ
            'conn1.Close()
            'conn2.Close()

            Dim conn1 As New OracleConnection(PRASSDB)
            conn1.Open()
            ' สร้างการเชื่อมต่อกับฐานข้อมูล 2
            Dim conn2 As New OracleConnection(MT800DB)
            conn2.Open()
            Dim sql1 As String = "SELECT DEF_MODE FROM DEFECTIVE_MT800  where PRODUCT_NAME = 'DC-MOD' OR  PRODUCT_NAME = 'IONIZER'  group by DEF_MODE having DEF_MODE != '-' order by DEF_MODE asc "
            Dim sql2 As String = "SELECT CODE,THAI FROM PST_DEF_THAI"

            Dim adapter1 As New OracleDataAdapter(sql1, conn1)
            Dim dataSet1 As New DataSet()
            adapter1.Fill(dataset1)

            Dim adapter2 As New OracleDataAdapter(sql2, conn2)
            Dim dataSet2 As New DataSet()
            adapter2.Fill(dataSet2)

            ' รวม DataTable จาก dataSet1 และ dataSet2
            Dim combinedData As New DataTable()
            combinedData.Columns.Add("DEF_MODE_SHOW", GetType(String))
            combinedData.Columns.Add("CODE_SHOW", GetType(String))



            For Each row1 As DataRow In dataSet1.Tables(0).Rows
                combinedData.Rows.Add(row1("DEF_MODE"), Nothing)
            Next

            For Each row2 As DataRow In dataSet2.Tables(0).Rows
                Dim matchingRow = combinedData.Rows.OfType(Of DataRow)().FirstOrDefault(Function(r) r("DEF_MODE_SHOW") = row2("CODE"))
                If matchingRow IsNot Nothing Then
                    'matchingRow("CODE_SHOW") = "111"
                Else

                    combinedData.Rows.Add(row2("CODE"), Nothing)

                End If
            Next

            'For Each row2 As DataRow In dataSet2.Tables(0).Rows
            '    For Each row1 As DataRow In combinedData.Rows
            '        If Not IsDBNull(row1("DEF_MODE_SHOW")) AndAlso row1("DEF_MODE_SHOW").ToString() = row2("CODE").ToString() Then
            '            combinedData.Rows.Add(row2("CODE"), Nothing)
            '            Exit For
            '        End If
            '    Next
            'Next


            For Each row2 As DataRow In dataSet2.Tables(0).Rows
                For Each row1 As DataRow In combinedData.Rows
                    If Not IsDBNull(row1("DEF_MODE_SHOW")) AndAlso row1("DEF_MODE_SHOW").ToString() = row2("CODE").ToString() Then
                        row1("CODE_SHOW") = row2("THAI")
                        Exit For ' หากพบการจับคู่ให้ออกจากลูป
                        'Else
                        '    combinedData.Rows.Add(row2("CODE"), Nothing)
                        '    Exit For
                    End If
                Next
            Next

            ' เพิ่มค่า "555" ถ้าไม่มีการจับคู่
            For Each row1 As DataRow In combinedData.Rows
                If IsDBNull(row1("CODE_SHOW")) Then
                    row1("CODE_SHOW") = " "
                End If
            Next


            'For Each row2 As DataRow In dataSet2.Tables(0).Rows
            '    Dim foundMatch As Boolean = False
            '    For Each matchingRow As DataRow In combinedData.Rows
            '        If matchingRow("DEF_MODE_SHOW") = row2("CODE") Then
            '            matchingRow("CODE_SHOW") = "111"
            '            foundMatch = True
            '            Exit For ' หากพบคู่ค่าตรงกัน จะออกจากลูป
            '        Else
            '            matchingRow("CODE_SHOW") = "11666"
            '            Exit For
            '        End If
            '    Next

            ' ถ้าไม่พบคู่ค่าตรงกันใน combinedData
            'If Not foundMatch Then
            '    ' ใส่ค่า "555" ในคอลัมน์ "CODE" ของ row2
            '    row2("CODE") = "555"
            'End If
            'Next
            ' แสดงข้อมูลใน GridView
            GridView1.DataSource = combinedData
            GridView1.DataBind()

            combinedData.Columns.Add("DEF_MODE_CODE", GetType(String), "IIf(CODE_SHOW = '', DEF_MODE_SHOW, DEF_MODE_SHOW + ' - ' + CODE_SHOW)")

            ddlMcDefectCode1.DataSource = combinedData
            ddlMcDefectCode1.DataTextField = "DEF_MODE_CODE"  ' ระบุคอลัมน์ที่รวมข้อมูล DEF_MODE และ CODE
            ddlMcDefectCode1.DataValueField = "DEF_MODE_SHOW" ' ระบุคอลัมน์ที่เป็นค่าที่เก็บไว้เมื่อเลือก
            ddlMcDefectCode1.DataBind()
        End If


        'MsgBox(Session("num5"))
    End Sub
    Private Sub Button1_ServerClick(sender As Object, e As EventArgs) Handles Button1.ServerClick




        If gvnaja.Rows.Count = 0 Then
            Dim dtResult As New DataTable()
            dtResult.Columns.Add("kuy", GetType(String))
            dtResult.Columns.Add("Name", GetType(String))
            dtResult.Columns.Add("Surname", GetType(String))
            Dim newRow As DataRow = dtResult.NewRow
            newRow("kuy") = "kuy"
            newRow("Name") = "Bob"
            newRow("Surname") = "Smith"
            dtResult.Rows.Add(newRow)
            gvnaja.DataSource = dtResult
            gvnaja.DataBind()
            ViewState("MyDataTable") = dtResult

            Dim insideData As DataTable = CType(ViewState("MyDataTable"), DataTable)
            Dim connectionString As String = MT800DB
            Using connection As New OracleConnection(connectionString)
                connection.Open()
                For Each row As DataRow In insideData.Rows
                    Dim sql As String = "INSERT INTO MATERIALBAR_SMT_HIS (USERHIS,DAY,TIME,MODEL) VALUES (:USERHIS,:sDAY,:sTIME,:sMODEL)"
                    Using command As New OracleCommand(sql, connection)
                        command.Parameters.Add(":USERHIS", row("kuy"))
                        command.Parameters.Add(":sDAY", row("Name"))
                        command.Parameters.Add(":sTIME", row("kuy"))
                        command.Parameters.Add(":sMODEL", row("Surname"))
                        command.ExecuteNonQuery()
                    End Using
                Next
                connection.Close()
            End Using

        Else

            Dim dt As DataTable = CType(ViewState("MyDataTable"), DataTable)
            Dim newRow As DataRow = dt.NewRow()
            newRow("kuy") = "1234"
            newRow("Name") = "1234"
            newRow("Surname") = "1234"
            dt.Rows.Add(newRow)
            gvnaja.DataSource = dt
            gvnaja.DataBind()

            Dim insideData As DataTable = CType(ViewState("MyDataTable"), DataTable)
            Dim connectionString As String = MT800DB
            Using connection As New OracleConnection(connectionString)
                connection.Open()
                For Each row As DataRow In insideData.Rows
                    Dim sql As String = "INSERT INTO MATERIALBAR_SMT_HIS (USERHIS,DAY,TIME,MODEL) VALUES (:USERHIS,:sDAY,:sTIME,:sMODEL)"
                    Using command As New OracleCommand(sql, connection)
                        command.Parameters.Add(":USERHIS", row("kuy"))
                        command.Parameters.Add(":sDAY", row("Name"))
                        command.Parameters.Add(":sTIME", row("kuy"))
                        command.Parameters.Add(":sMODEL", row("Surname"))
                        command.ExecuteNonQuery()
                    End Using
                Next
                connection.Close()
            End Using
        End If



    End Sub
End Class
