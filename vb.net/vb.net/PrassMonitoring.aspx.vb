
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
Imports Excel = Microsoft.Office.Interop.Excel
Imports ClosedXML.Excel
Imports ExcelAutoFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat


Partial Class PrassMonitoring
    Inherits System.Web.UI.Page
    Dim DB As String = ""
    Dim DWUSER As String = ""
    Dim PRASSDB As String = ""

    Dim STAECU1 As String = String.Empty
    Dim STAECU2 As String = String.Empty

    Dim STAFADJI1U1 As String = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F101/"
    Dim STAFADJI1U2 As String = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F102/"
    Dim STAFADJI1U3 As String = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F103/"
    Dim STAFADJI1U4 As String = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F104/"
    Dim STAFADJI1U5 As String = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F105/"
    Dim STAFADJI1U6 As String = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F106/"

    Dim STAFADJI2U1 As String = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F201/"
    Dim STAFADJI2U2 As String = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F202/"
    Dim STAFADJI2U3 As String = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F203/"
    Dim STAFADJI2U4 As String = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F204/"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Me.load_GridViewDF()
        End If
        'MsgBox(Session("mySession"))
    End Sub
    Private Sub btnPrasslot_ServerClick(sender As Object, e As EventArgs) Handles btnPrasslot.ServerClick
        Dim strLOT As String = String.Empty
        Dim connstrLOT As New OracleConnection(PRASSDB)
        Dim cmdstrLOT As New OracleCommand()
        Dim accstrLOT As String = "select LOTNO from M_DEFECTIVE_MT800 where LOTNO = '" & tbPrasslot.Text & "'"
        cmdstrLOT.CommandText = accstrLOT
        cmdstrLOT.Connection = connstrLOT
        connstrLOT.Open()
        Dim rd As OracleDataReader = cmdstrLOT.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strLOT = String.Empty Else strLOT = CStr(rd.GetValue(0))
            End While
        Finally
            rd.Close()
        End Try
        Select Case tbPrasslot.Text = strLOT And tbPrasslot.Text <> String.Empty
            Case True
                Session("showFRONTENDLINE") = tbPrasslot.Text
                lbshowFRONTENDLINE.InnerText = Session("showFRONTENDLINE")
                btnExport.Visible = True
                GridViewEC2.Visible = False
                GridViewEC1.Visible = False
                Session("PRODUCT") = String.Empty
                Session("MODEL") = String.Empty
                Session("LINE") = String.Empty
                Session("PROCESS") = String.Empty
                lblEmployeeDetails.Text = String.Empty
                pnPrass.Visible = True
                btnExport.Visible = False
                btnExportEC1.Visible = False
                btnExportEC2.Visible = False
                pnELECTRICALUnit1.Visible = False
                pnELECTRICALUnit2.Visible = False
            Case False
                GridViewEC2.Visible = False
                GridViewEC1.Visible = False
                Session("PRODUCT") = String.Empty
                Session("MODEL") = String.Empty
                Session("LINE") = String.Empty
                Session("PROCESS") = String.Empty
                lblEmployeeDetails.Text = String.Empty
                pnPrass.Visible = False
        End Select

        Me.load_GridViewDF()
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridViewDF.PageIndex = e.NewPageIndex
        Me.load_GridViewDF()
        Me.load_GridViewEC1()
        Me.load_GridViewEC2()
    End Sub
    Protected Sub OnPageIndexChanging1(sender As Object, e1 As GridViewPageEventArgs)
        GridViewEC1.PageIndex = e1.NewPageIndex
        Me.load_GridViewDF()
        Me.load_GridViewEC1()
        Me.load_GridViewEC2()
    End Sub
    Protected Sub OnPageIndexChanging2(sender As Object, e2 As GridViewPageEventArgs)
        GridViewEC2.PageIndex = e2.NewPageIndex
        Me.load_GridViewDF()
        Me.load_GridViewEC1()
        Me.load_GridViewEC2()
    End Sub
    Private Sub tbRegisMcSearch_TextChanged(sender As Object, e As EventArgs) Handles tbRegisMcSearch.TextChanged
        Me.load_GridViewDF()
    End Sub
    Private Sub load_GridViewDF()
        btnExport.Visible = True
        Dim conn As New OracleConnection(PRASSDB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select PRODUCT_NAME ,MURATA_TYPE, LINE ,PROCESS_NAME	,FINISH_PROCESS_DATE, DEF_MODE ,DEF_QTY ,EMP_CODE from M_DEFECTIVE_MT800  where LOTNO='" + lbshowFRONTENDLINE.InnerText + "'"
        Select Case tbRegisMcSearch.Text = String.Empty
            Case True 'nothing
            Case False
                Dim arrSearch() As String = UCase(Trim(tbRegisMcSearch.Text)).Split({"+"}, StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To CInt(arrSearch.Length.ToString) - 1
                    acc += " and ( PRODUCT_NAME like '%" + arrSearch(i) + "%'"
                    acc += " or MURATA_TYPE like '%" + arrSearch(i) + "%'"
                    acc += " or LINE like '%" + arrSearch(i) + "%'"
                    acc += " or PROCESS_NAME like '%" + arrSearch(i) + "%'"
                    acc += " or FINISH_PROCESS_DATE like '%" + arrSearch(i) + "%'"
                    acc += " or DEF_MODE like '%" + arrSearch(i) + "%'"
                    acc += " or DEF_QTY like '%" + arrSearch(i) + "%'"
                    acc += " or EMP_CODE like '%" + arrSearch(i) + "%')"
                Next
        End Select
        acc += " order by DEF_QTY desc"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            GridViewDF.DataSource = dt
            GridViewDF.DataBind()
        End Using
    End Sub
    Protected Sub GridViewDF_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridViewDF.SelectedIndexChanged
        GridViewEC2.Visible = True
        GridViewEC1.Visible = True
        Me.load_GridViewDF()
        Select Case lblEmployeeDetails.Text = String.Empty
            Case True
                Try

                    Dim strString As String = Session("showFRONTENDLINE")
                    Session("LOTNO") = Left(strString, 7)

                    Session("PRODUCT") = GridViewDF.SelectedRow.Cells(0).Text
                    Session("MODEL") = GridViewDF.SelectedRow.Cells(1).Text
                    Session("LINE") = GridViewDF.SelectedRow.Cells(2).Text
                    Session("PROCESS") = GridViewDF.SelectedRow.Cells(3).Text
                    lblEmployeeDetails.Text = "<b>PRODUCT:</b> " & Session("PRODUCT") & " </br><b>MODEL:</b> " & Session("MODEL") & " </br><b>LINE:</b> " & Session("LINE") & " </br><b>PROCESS:</b> " & Session("PROCESS")
                Catch ex As Exception
                    Throw
                End Try
                Select Case Session("LINE")
                    Case "I1"
                        Dim strString As String = Session("showFRONTENDLINE")
                        Session("LOTNO") = Left(strString, 7)
                        pnELECTRICALUnit1.Visible = True
                        pnELECTRICALUnit2.Visible = True
                        btnExportEC1.Visible = True
                        btnExportEC2.Visible = True
                        Session("STAECU1") = "//172.16.72.179/sta/002_Electrical checking/Data measuring/E101/"
                        Session("STAECU2") = "//172.16.72.179/sta/002_Electrical checking/Data measuring/E102/"

                        Session("Fadjust1unit1") = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F101/"
                        Session("Fadjust1unit2") = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F102/"
                        Session("Fadjust1unit3") = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F103/"
                        Session("Fadjust1unit4") = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F104/"
                        Session("Fadjust1unit5") = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F105/"
                        Session("Fadjust1unit6") = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F106/"
                        Me.load_GridViewEC1()
                        Me.load_GridViewEC2()
                    Case "I2"
                        Dim strString As String = Session("showFRONTENDLINE")
                        Session("LOTNO") = Left(strString, 7)
                        pnELECTRICALUnit1.Visible = True
                        pnELECTRICALUnit2.Visible = True
                        btnExportEC1.Visible = True
                        btnExportEC2.Visible = True
                        Session("STAECU1") = "//172.16.72.179/sta/002_Electrical checking/Data measuring/E201/"
                        Session("STAECU2") = "//172.16.72.179/sta/002_Electrical checking/Data measuring/E202/"

                        Session("Fadjust2unit1") = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F201/"
                        Session("Fadjust2unit2") = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F202/"
                        Session("Fadjust2unit3") = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F203/"
                        Session("Fadjust2unit4") = "//172.16.72.179/sta/001_Ferquence alignment/Data measuring/F204/"
                        Me.load_GridViewEC1()
                        Me.load_GridViewEC2()
                    Case Else

                End Select
            Case False

        End Select
    End Sub
    Private Sub btnExport_ServerClick(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim dt As New DataTable("GridView_Data")
        GridViewDF.AllowPaging = False
        Me.load_GridViewDF()
        For Each cell As TableCell In GridViewDF.HeaderRow.Cells
            dt.Columns.Add(cell.Text)
        Next
        For Each row As GridViewRow In GridViewDF.Rows
            dt.Rows.Add()
            For i As Integer = 0 To row.Cells.Count - 1
                dt.Rows(dt.Rows.Count - 1)(i) = row.Cells(i).Text
            Next
        Next
        Using wb As New XLWorkbook
            wb.Worksheets.Add(dt)
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=GridView.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.[End]()
            End Using
        End Using
    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As System.Web.UI.Control)

    End Sub
    Private Sub load_GridViewEC1()
        Dim dt As DataTable
        '*** DataTable ***'
        dt = ReadCSVE1()  '*** Convert CSV to DataTable ***'
        '*** BindData to Repeater ***'
        GridViewEC1.DataSource = dt
        GridViewEC1.DataBind()
    End Sub
    '*** CSV & DataTable ***'
    Function ReadCSVE1() As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        '*** Column ***'
        dt.Columns.Add("No.")
        dt.Columns.Add("Date")
        dt.Columns.Add("Time")
        dt.Columns.Add("Trap F(kHz)")
        dt.Columns.Add("Trap A(dB)")
        dt.Columns.Add("Center A(dB)")
        dt.Columns.Add("Judge")
        dt.Columns.Add("Count")
        Dim StrWer As StreamReader
        Dim strLine As String
        Dim rowCounter As Integer = 0
        StrWer = IO.File.OpenText((Session("STAECU1")) & "E_201_" & Session("MODEL") & "_" & Session("LOTNO") & ".csv")
        Do Until StrWer.EndOfStream
            strLine = StrWer.ReadLine()
            If Trim(strLine) <> "" Then
                '*** Rows ***'
                dr = dt.NewRow
                If rowCounter > 2 Then
                    dr("No.") = Split(strLine, ",")(0)
                    dr("Date") = Split(strLine, ",")(1)
                    dr("Time") = Split(strLine, ",")(2)
                    dr("Trap F(kHz)") = Split(strLine, ",")(3)
                    dr("Trap A(dB)") = Split(strLine, ",")(4)
                    dr("Center A(dB)") = Split(strLine, ",")(5)
                    dr("Judge") = Split(strLine, ",")(6)
                    dr("Count") = Split(strLine, ",")(7)
                    dt.Rows.Add(dr)
                End If

                rowCounter = rowCounter + 1
            End If
        Loop
        StrWer.Close()
        Return dt '*** Return DataTable ***'


    End Function
    Private Sub load_GridViewEC2()

        Dim dt As DataTable
        '*** DataTable ***'
        dt = ReadCSVE2()  '*** Convert CSV to DataTable ***'
        '*** BindData to Repeater ***'
        GridViewEC2.DataSource = dt
        GridViewEC2.DataBind()


    End Sub
    '*** CSV & DataTable ***'
    Function ReadCSVE2() As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        '*** Column ***'
        dt.Columns.Add("No.")
        dt.Columns.Add("Date")
        dt.Columns.Add("Time")
        dt.Columns.Add("Trap F(kHz)")
        dt.Columns.Add("Trap A(dB)")
        dt.Columns.Add("Center A(dB)")
        dt.Columns.Add("Judge")
        dt.Columns.Add("Count")
        Dim StrWer As StreamReader
        Dim strLine As String
        Dim rowCounter As Integer = 0
        StrWer = File.OpenText((Session("STAECU2")) & "E_202_" & Session("MODEL") & "_" & Session("LOTNO") & ".csv")
        Do Until StrWer.EndOfStream
            strLine = StrWer.ReadLine()
            If Trim(strLine) <> "" Then
                '*** Rows ***'
                dr = dt.NewRow
                If rowCounter > 1 Then
                    dr("No.") = Split(strLine, ",")(0)
                    dr("Date") = Split(strLine, ",")(1)
                    dr("Time") = Split(strLine, ",")(2)
                    dr("Trap F(kHz)") = Split(strLine, ",")(3)
                    dr("Trap A(dB)") = Split(strLine, ",")(4)
                    dr("Center A(dB)") = Split(strLine, ",")(5)
                    dr("Judge") = Split(strLine, ",")(6)
                    dr("Count") = Split(strLine, ",")(7)
                    dt.Rows.Add(dr)
                End If
                rowCounter = rowCounter + 1
            End If
        Loop
        StrWer.Close()
        Return dt '*** Return DataTable ***'

    End Function

    Private Sub btnExportEC1_ServerClick(sender As Object, e As EventArgs) Handles btnExportEC1.Click
        Dim dt As New DataTable("GridView_Data")
        GridViewEC1.AllowPaging = False
        Me.load_GridViewEC1()
        For Each cell As TableCell In GridViewEC1.HeaderRow.Cells
            dt.Columns.Add(cell.Text)
        Next
        For Each row As GridViewRow In GridViewEC1.Rows
            dt.Rows.Add()
            For i As Integer = 0 To row.Cells.Count - 1
                dt.Rows(dt.Rows.Count - 1)(i) = row.Cells(i).Text
            Next
        Next
        Using wb As New XLWorkbook
            wb.Worksheets.Add(dt)
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=GridView.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.[End]()
            End Using
        End Using
    End Sub
    Private Sub btnExportEC2_ServerClick(sender As Object, e As EventArgs) Handles btnExportEC2.Click
        Dim dt As New DataTable("GridView_Data")
        GridViewEC2.AllowPaging = False
        Me.load_GridViewEC2()
        For Each cell As TableCell In GridViewEC2.HeaderRow.Cells
            dt.Columns.Add(cell.Text)
        Next
        For Each row As GridViewRow In GridViewEC2.Rows
            dt.Rows.Add()
            For i As Integer = 0 To row.Cells.Count - 1
                dt.Rows(dt.Rows.Count - 1)(i) = row.Cells(i).Text
            Next
        Next
        Using wb As New XLWorkbook
            wb.Worksheets.Add(dt)
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=GridView.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.[End]()
            End Using
        End Using
    End Sub
End Class
