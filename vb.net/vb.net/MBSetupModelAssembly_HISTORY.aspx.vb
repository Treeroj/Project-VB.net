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
Partial Class MBSetupModelAssembly_HISTORY
    Inherits System.Web.UI.Page
    Dim MT800DB As String = ""
    Dim DWUSER As String = ""
    Dim PROCESS As String = "ASSEMBLY"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Page.Form.Attributes.Add("enctype", "multipart/form-data")
            txtDate.Attributes("prevValue") = txtDate.Text
        End If
    End Sub
    Private Sub MesgBox(ByVal sMessage As String)
        Dim msg As String
        msg = "<script language='javascript'>"
        msg += "alert('" & sMessage & "');"
        msg += "</script>"
        Response.Write(msg)
    End Sub
    Protected Sub tbUser_TextChanged(sender As Object, e As EventArgs)
        Dim strUser As String = String.Empty
        Dim KEY As String = String.Empty
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand("select USERID from NVP_ALLPROCESS_USER where USERID = '" & tbUser.Text & "' and PROCESS = '" & PROCESS & "' or PROCESS = 'ALL'", conn)
        conn.Open()
        Dim rd As OracleDataReader = cmd.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strUser = String.Empty Else strUser = CStr(rd.GetValue(0))
                If strUser = tbUser.Text Then
                    Exit While
                End If
            End While
        Finally
            rd.Close()
        End Try
        conn.Close()
        Select Case tbUser.Text = strUser And tbUser.Text <> String.Empty
            Case True
                Panel7.Visible = True
                Panel2.Visible = True
                PanelMATERIALHIS.Visible = True
                'Panel3.Visible = False
                Panel4.Visible = True
                Load_gvHIS()
                Panel3.Visible = True
                Load_ddlLinePD()
                Load_ddlLine()
                Load_ddlMODEL()
                Load_ddlLOT()
                Load_ddlMAT()
            Case False
                tbUser.Text = String.Empty

                Panel3.Visible = False
                Panel7.Visible = False
                Panel2.Visible = False
                PanelMATERIALHIS.Visible = False
                Panel4.Visible = False
                MesgBox("รหัสของท่านไม่ถูกต้อง")

        End Select
    End Sub
    'Private Sub Load_gvHIS()
    '    Dim conn As New OracleConnection(MT800DB)
    '    Dim cmd As New OracleCommand()
    '    Dim acc As String = "SELECT USERNAME,DAY,TIME,MODEL,LOTNO,MATERIAL,INVOICE,MATERIALLOT,QTY,REELNO,LINE,PRODUCTS FROM NVP_ALLPROCESS_HIS"

    '    acc += " where PROCESS = '" & PROCESS & "'"
    '    acc += " ORDER BY TO_DATE(DAY,'DD-MM-YY') DESC ,TIME DESC"
    '    cmd.CommandText = acc
    '    cmd.Connection = conn
    '    Using sda As New OracleDataAdapter(cmd)
    '        Dim dt As New DataTable()
    '        sda.Fill(dt)

    '        gvHIS.DataSource = dt
    '        gvHIS.DataBind()
    '        Session("myGridViewData") = dt
    '    End Using
    '    Session("ff") = Format(Now, "dd-MMM-yy")
    'End Sub
    Private Sub Load_gvHIS()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "SELECT USERNAME,DAY,TIME, MODEL, LOTNO, MATERIAL, INVOICE, MATERIALLOT, QTY, REELNO, LINE, PRODUCTS FROM NVP_ALLPROCESS_HIS"

        acc += " WHERE PROCESS = '" & PROCESS & "'"
        acc += " ORDER BY TO_DATE(DAY, 'DD-MON-YYYY') DESC, TO_DATE(TIME, 'HH24:MI:SS') DESC"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)

            gvHIS.DataSource = dt
            gvHIS.DataBind()
            Session("myGridViewData") = dt
        End Using
        Session("ff") = Format(Now, "dd-MMM-yy")
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvHIS.PageIndex = e.NewPageIndex
        gvHIS.DataSource = CType(Session("myGridViewData"), DataTable)
        gvHIS.DataBind()
        ApplyFilters()
    End Sub
    Private Sub Load_ddlMODEL()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select DISTINCT MODEL From NVP_ALLPROCESS_HIS where PROCESS = '" & PROCESS & "'"
        If ddlLinePD.SelectedValue <> "" Then
            acc += "and PRODUCTS = '" & ddlLinePD.SelectedValue & "'"
        End If
        If ddlLine.SelectedValue <> "" Then
            acc += "and LINE = '" & ddlLine.SelectedValue & "'"
        End If
        acc += " ORDER BY MODEL ASC"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlMODEL.DataSource = dt
            ddlMODEL.DataTextField = "MODEL"
            ddlMODEL.DataValueField = "MODEL"
            ddlMODEL.DataBind()
            ddlMODEL.Items.Insert(0, New ListItem("(กรุณาเลือก รุ่นงาน)", String.Empty))
        End Using
    End Sub
    Protected Sub ddlMODEL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMODEL.SelectedIndexChanged
        ApplyFilters()
    End Sub
    Private Sub Load_ddlLOT()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select DISTINCT LOTNO From NVP_ALLPROCESS_HIS where PROCESS = '" & PROCESS & "'"
        If ddlLinePD.SelectedValue <> "" Then
            acc += "and PRODUCTS = '" & ddlLinePD.SelectedValue & "'"
        End If
        If ddlLine.SelectedValue <> "" Then
            acc += "and LINE = '" & ddlLine.SelectedValue & "'"
        End If
        acc += " ORDER BY LOTNO ASC"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlLOT.DataSource = dt
            ddlLOT.DataTextField = "LOTNO"
            ddlLOT.DataValueField = "LOTNO"
            ddlLOT.DataBind()
            ddlLOT.Items.Insert(0, New ListItem("(กรุณาเลือก ล็อตงาน)", String.Empty))
        End Using
    End Sub
    Protected Sub ddlLOT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLOT.SelectedIndexChanged
        ApplyFilters()
    End Sub
    Private Sub Load_ddlMAT()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select DISTINCT MATERIAL From NVP_ALLPROCESS_HIS where PROCESS = '" & PROCESS & "'"
        If ddlLinePD.SelectedValue <> "" Then
            acc += "and PRODUCTS = '" & ddlLinePD.SelectedValue & "'"
        End If
        If ddlLine.SelectedValue <> "" Then
            acc += "and LINE = '" & ddlLine.SelectedValue & "'"
        End If
        acc += " ORDER BY MATERIAL ASC"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlMAT.DataSource = dt
            ddlMAT.DataTextField = "MATERIAL"
            ddlMAT.DataValueField = "MATERIAL"
            ddlMAT.DataBind()
            ddlMAT.Items.Insert(0, New ListItem("(กรุณาเลือก วัตถุดิบ)", String.Empty))
        End Using
    End Sub
    Protected Sub ddlMAT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMAT.SelectedIndexChanged
        ApplyFilters()
    End Sub
    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged, TextBox5.TextChanged, TextBox6.TextChanged, txtDate.TextChanged, txtDate1.TextChanged, txtDate1.TextChanged, txtDate.TextChanged
        Load_ddlLinePD()
        ApplyFilters()
    End Sub
    Private Sub Load_ddlLinePD()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select DISTINCT PRODUCTS From NVP_ALLPROCESS_HIS where PROCESS = '" & PROCESS & "'  ORDER BY PRODUCTS ASC"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlLinePD.DataSource = dt
            ddlLinePD.DataTextField = "PRODUCTS"
            ddlLinePD.DataValueField = "PRODUCTS"
            ddlLinePD.DataBind()
            ddlLinePD.Items.Insert(0, New ListItem("(กรุณาเลือก โปรดักส์)", String.Empty))
        End Using
    End Sub
    Protected Sub ddlLinePD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLinePD.SelectedIndexChanged
        Load_ddlLine()
        ApplyFilters()
    End Sub
    Private Sub Load_ddlLine()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select DISTINCT LINE From NVP_ALLPROCESS_HIS where PROCESS = '" & PROCESS & "'"
        If ddlLinePD.SelectedValue <> "" Then
            acc += "and PRODUCTS = '" & ddlLinePD.SelectedValue & "'"
        End If
        acc += " ORDER BY LINE ASC"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlLine.DataSource = dt
            ddlLine.DataTextField = "LINE"
            ddlLine.DataValueField = "LINE"
            ddlLine.DataBind()
            ddlLine.Items.Insert(0, New ListItem("(กรุณาเลือก ไลน์)", String.Empty))
        End Using
    End Sub
    Protected Sub ddlLine_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLine.SelectedIndexChanged
        ApplyFilters()
    End Sub
    Private Sub ApplyFilters()
        Dim filterExpressions As New List(Of String)
        If Not String.IsNullOrEmpty(ddlMODEL.SelectedValue) Then
            filterExpressions.Add("MODEL LIKE '%" & ddlMODEL.SelectedValue & "%'")
            Load_gvHIS()

        End If
        If Not String.IsNullOrEmpty(ddlLOT.SelectedValue) Then
            filterExpressions.Add("LOTNO LIKE '%" & ddlLOT.SelectedValue & "%'")
            Load_gvHIS()

        End If
        If Not String.IsNullOrEmpty(ddlMAT.SelectedValue) Then
            filterExpressions.Add("MATERIAL LIKE '%" & ddlMAT.SelectedValue & "%'")
            Load_gvHIS()

        End If
        If Not String.IsNullOrEmpty(TextBox4.Text) Then
            filterExpressions.Add("INVOICE LIKE '%" & TextBox4.Text & "%'")
            Load_gvHIS()

        End If
        If Not String.IsNullOrEmpty(TextBox5.Text) Then
            filterExpressions.Add("MATERIALLOT LIKE '%" & TextBox5.Text & "%'")
            Load_gvHIS()

        End If
        If Not String.IsNullOrEmpty(TextBox6.Text) Then
            filterExpressions.Add("REELNO LIKE '%" & TextBox6.Text & "%'")
            Load_gvHIS()

        End If

        If Not String.IsNullOrEmpty(ddlLine.SelectedValue) Then
            filterExpressions.Add("LINE LIKE '%" & ddlLine.SelectedValue & "%'")
            Load_gvHIS()

        End If
        If Not String.IsNullOrEmpty(ddlLinePD.SelectedValue) Then
            filterExpressions.Add("PRODUCTS LIKE '%" & ddlLinePD.SelectedValue & "%'")
            Load_gvHIS()

        End If
        Dim selectedDate As String = txtDate.Text
        Dim selectedDate1 As String = txtDate1.Text
        If selectedDate <> String.Empty And selectedDate1 = String.Empty Then
            If Not String.IsNullOrEmpty(selectedDate) Then
                filterExpressions.Add("DAY = '" & selectedDate & "'")
                Load_gvHIS()
            End If
        End If

        'If selectedDate <> String.Empty And selectedDate1 <> String.Empty Then
        '    If Not String.IsNullOrEmpty(selectedDate) Then
        '        If Not String.IsNullOrEmpty(selectedDate1) Then
        '            filterExpressions.Add("DAY >=  '" & selectedDate & "' AND DAY <=  '" & selectedDate1 & "'")
        '            Load_gvHIS()
        '        End If
        '    End If
        'End If

        'If selectedDate <> String.Empty And selectedDate1 <> String.Empty Then
        '    If Not String.IsNullOrEmpty(selectedDate) AndAlso Not String.IsNullOrEmpty(selectedDate1) Then
        '        Try
        '            ' แปลงรูปแบบวันที่ให้เป็น dd-MMM-yyyy สำหรับใช้ในคำสั่ง SQL
        '            Dim formattedDate As String = DateTime.ParseExact(selectedDate, "dd-MMM-yyyy", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy")
        '            Dim formattedDate1 As String = DateTime.ParseExact(selectedDate1, "dd-MMM-yyyy", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy")

        '            ' ใช้ BETWEEN ในคำสั่ง SQL และใช้ TO_DATE() สำหรับ Oracle
        '            filterExpressions.Add("DAY BETWEEN TO_DATE('" & formattedDate & "', 'DD-MON-YYYY') AND TO_DATE('" & formattedDate1 & "', 'DD-MON-YYYY')")
        '            Load_gvHIS()
        '        Catch ex As FormatException
        '            ' จัดการกับข้อผิดพลาด, เช่น บันทึกข้อผิดพลาดหรือแสดงข้อความผิดพลาดที่เข้าใจง่ายให้ผู้ใช้เห็น
        '            ' บล็อกนี้จะถูกทำงานเมื่อกระบวนการแปลงล้มเหลว
        '            Console.WriteLine("รูปแบบวันที่ไม่ถูกต้อง: " & ex.Message)
        '        End Try
        '    End If
        'End If

        If selectedDate <> String.Empty And selectedDate1 <> String.Empty Then
            If Not String.IsNullOrEmpty(selectedDate) AndAlso Not String.IsNullOrEmpty(selectedDate1) Then
                Try
                    Dim formattedDate As String = selectedDate
                    Dim formattedDate1 As String = selectedDate1

                    filterExpressions.Add("DAY >= #" & formattedDate & "# AND DAY <= #" & formattedDate1 & "#")

                    Load_gvHIS()
                Catch ex As FormatException
                    Console.WriteLine("รูปแบบวันที่ไม่ถูกต้อง: " & ex.Message)
                End Try
            End If
        End If

        If txtDate.Text = String.Empty And txtDate.Text = String.Empty And ddlMODEL.SelectedValue = "" And ddlLOT.SelectedValue = "" And ddlMAT.SelectedValue = "" And TextBox4.Text = String.Empty And TextBox5.Text = String.Empty And TextBox6.Text = String.Empty And txtDate.Text = String.Empty And txtDate1.Text = String.Empty And ddlLinePD.SelectedValue = "" And ddlLine.SelectedValue = "" Then

            Load_gvHIS()
        End If
        Dim filterExpression As String = String.Join(" AND ", filterExpressions)

        'Dim dv As New DataView(DirectCast(gvHIS.DataSource, DataTable))
        'dv.RowFilter = filterExpression

        'gvHIS.DataSource = dv
        'gvHIS.DataBind()

        If TypeOf gvHIS.DataSource Is DataTable Then
            Dim dt As DataTable = DirectCast(gvHIS.DataSource, DataTable)
            Dim dv As New DataView(dt)
            dv.RowFilter = filterExpression
            gvHIS.DataSource = dv
            gvHIS.DataBind()
        ElseIf TypeOf gvHIS.DataSource Is DataView Then
            Dim dv As DataView = DirectCast(gvHIS.DataSource, DataView)
            dv.RowFilter = filterExpression
            gvHIS.DataBind()
        Else
        End If

    End Sub
    Protected Sub gvHIS_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvHIS.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim modelCell As TableCell = e.Row.Cells(3) 
            Dim lotnoCell As TableCell = e.Row.Cells(4) 
            Dim materialCell As TableCell = e.Row.Cells(5)
            Dim invoiceCell As TableCell = e.Row.Cells(6)
            Dim materiallotCell As TableCell = e.Row.Cells(7) 
            Dim reelnoCell As TableCell = e.Row.Cells(9)
            Dim lineCell As TableCell = e.Row.Cells(10) 

            Dim modelText As String = modelCell.Text
            Dim lotnoText As String = lotnoCell.Text
            Dim materialText As String = materialCell.Text
            Dim invoiceText As String = invoiceCell.Text
            Dim materiallotText As String = materiallotCell.Text
            Dim reelnoText As String = reelnoCell.Text
            Dim lineText As String = lineCell.Text


            If Not String.IsNullOrEmpty(ddlMODEL.SelectedValue) AndAlso modelText.Contains(ddlMODEL.SelectedValue) Then
                modelCell.ForeColor = System.Drawing.Color.Blue
            Else
                modelCell.ForeColor = System.Drawing.Color.Black
            End If

            If Not String.IsNullOrEmpty(ddlLOT.SelectedValue) AndAlso lotnoText.Contains(ddlLOT.SelectedValue) Then
                lotnoCell.ForeColor = System.Drawing.Color.Blue
            Else
                lotnoCell.ForeColor = System.Drawing.Color.Black
            End If

            If Not String.IsNullOrEmpty(ddlMAT.SelectedValue) AndAlso materialText.Contains(ddlMAT.SelectedValue) Then
                materialCell.ForeColor = System.Drawing.Color.Blue
            Else
                materialCell.ForeColor = System.Drawing.Color.Black
            End If

            If Not String.IsNullOrEmpty(TextBox4.Text) AndAlso invoiceText.Contains(TextBox4.Text) Then
                invoiceCell.ForeColor = System.Drawing.Color.Blue
            Else
                invoiceCell.ForeColor = System.Drawing.Color.Black
            End If

            If Not String.IsNullOrEmpty(TextBox5.Text) AndAlso materiallotText.Contains(TextBox5.Text) Then
                materiallotCell.ForeColor = System.Drawing.Color.Blue
            Else
                materiallotCell.ForeColor = System.Drawing.Color.Black
            End If

            If Not String.IsNullOrEmpty(TextBox6.Text) AndAlso reelnoText.Contains(TextBox6.Text) Then
                reelnoCell.ForeColor = System.Drawing.Color.Blue
            Else
                reelnoCell.ForeColor = System.Drawing.Color.Black
            End If

            If Not String.IsNullOrEmpty(ddlLine.SelectedValue) AndAlso lineText.Contains(ddlLine.SelectedValue) Then
                lineCell.ForeColor = System.Drawing.Color.Blue
            Else
                lineCell.ForeColor = System.Drawing.Color.Black
            End If


        End If
    End Sub
    Private Sub btnExportEC1_ServerClick(sender As Object, e As EventArgs) Handles btnExportEC1.Click
        Dim dt As New DataTable("GridView_Data")
        gvHIS.AllowPaging = False
        ApplyFilters()
        For Each cell As TableCell In gvHIS.HeaderRow.Cells
            dt.Columns.Add(cell.Text)
        Next
        For Each row As GridViewRow In gvHIS.Rows
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
            Response.AddHeader("content-disposition", "attachment;filename=HistoryAssembly.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.[End]()
            End Using
        End Using
    End Sub
End Class
