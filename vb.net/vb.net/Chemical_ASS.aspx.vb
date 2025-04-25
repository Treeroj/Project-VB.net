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
Partial Class Chemical_ASS
    Inherits System.Web.UI.Page
    Dim MTLE As String = ""
    Dim MT800DB As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Page.Form.Attributes.Add("enctype", "multipart/form-data")
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
        If tbUser.Text.Count > 7 Then
            Dim strEmpScan As String = tbUser.Text
            Try
                Dim empScanInt As Integer = (strEmpScan - 11779) / 29
                tbUser.Text = empScanInt
            Catch ex As Exception
            End Try
        End If

        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand("select USERID from MATERIALBAR_USERLIST where USERID = '" & tbUser.Text & "' and POSITION = 'CHEMICAL' or POSITION = 'ALL'", conn)
        conn.Open()
        Dim rd As OracleDataReader = cmd.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strUser = String.Empty Else strUser = CStr(rd.GetValue(0))
            End While
        Finally
            rd.Close()
        End Try
        conn.Close()
        Select Case tbUser.Text = strUser And tbUser.Text <> String.Empty
            Case True
                Load_ddlLinePD()
                PNddlLinePD.Visible = True

            Case False
                MesgBox("รหัส " & tbUser.Text & " ของท่านไม่ถูกต้องหรือยังไม่ได้ลงทะเบียนในระบบ")
        End Select

    End Sub
    Private Sub Load_UesrName()
        Dim strUser As String = String.Empty
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand("select USERNAME from MATERIALBAR_USERLIST where USERID = '" & tbUser.Text & "' and POSITION = 'CHEMICAL' or POSITION = 'ALL'", conn)
        conn.Open()
        Dim rd As OracleDataReader = cmd.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strUser = String.Empty Else strUser = CStr(rd.GetValue(0))
            End While
        Finally
            rd.Close()
        End Try
        conn.Close()
        lbshowFRONTENDLINE.InnerText = strUser
        lbshowCode.InnerText = tbUser.Text
    End Sub
    Private Sub Load_ddlLinePD()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select PRODUCT From MATERIALBAR_LINEDCPSMODPD"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlLinePD.DataSource = dt
            ddlLinePD.DataTextField = "PRODUCT"
            ddlLinePD.DataValueField = "PRODUCT"
            ddlLinePD.DataBind()
            ddlLinePD.Items.Insert(0, New ListItem("(กรุณาเลือก โปรดักส์)", String.Empty))
        End Using
    End Sub
    Protected Sub ddlLinePD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLinePD.SelectedIndexChanged
        Select Case ddlLinePD.SelectedIndex = 0
            Case True
                ddlLinePD.SelectedIndex.ToString()
                PNddlLine.Visible = False
                PNlbshow.Visible = False
                PaneChemicalCheck1.Visible = False
                PaneChemicalCheck2.Visible = False
                PaneChemicalCheck3.Visible = False
                PanelChemicalHIS.Visible = False
            Case False
                Call Load_UesrName()
                Load_ddlLine()
                PNddlLine.Visible = True
        End Select
    End Sub
    Private Sub Load_ddlLine()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select LINEPSMODASSY From MATERIALBARASS_LINEPSMOD where PRODUCT = '" & ddlLinePD.SelectedItem.Value & "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlLine.DataSource = dt
            ddlLine.DataTextField = "LINEPSMODASSY"
            ddlLine.DataValueField = "LINEPSMODASSY"
            ddlLine.DataBind()
            ddlLine.Items.Insert(0, New ListItem("(กรุณาเลือก ไลน์)", String.Empty))
        End Using
    End Sub
    Protected Sub ddlLine_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLine.SelectedIndexChanged
        Select Case ddlLine.SelectedIndex = 0
            Case True
                PNlbshow.Visible = False
                PaneChemicalCheck1.Visible = False
                PaneChemicalCheck2.Visible = False
                PaneChemicalCheck3.Visible = False
                PanelChemicalHIS.Visible = False
            Case False
                PNlbshow.Visible = True
                PaneChemicalCheck1.Visible = True
                PaneChemicalCheck2.Visible = True
                PaneChemicalCheck3.Visible = True
                PanelChemicalHIS.Visible = True
                Load_gvModel()
                Load_gvHIS()
        End Select
    End Sub
    Private Sub Load_gvModel()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select MATERIAL,SCM From NVP_CHEMICAL_MAT where LINE = '" & ddlLine.SelectedItem.Text & "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            GridViewModel.DataSource = dt
            GridViewModel.DataBind()
        End Using
    End Sub
    Protected Sub tnMaterial_TextChanged(sender As Object, e As EventArgs)
        Dim Str9 As String = tnMaterial.Text
        Dim count = Str9.Split("|").Count()
        If tnMaterial.Text <> String.Empty Then
            Select Case count = 6
                Case True
                    Dim StrArr(5) As String
                    Dim Str5 As String = tnMaterial.Text
                    StrArr = Str5.Split("|")
                    StrArr(0) = StrArr(0).Replace("|", "")
                    StrArr(1) = StrArr(1).Replace("|", "")
                    StrArr(2) = StrArr(2).Replace("|", "")
                    StrArr(3) = StrArr(3).Replace("|", "")
                    StrArr(4) = StrArr(4).Replace("|", "")
                    StrArr(5) = StrArr(5).Replace("|", "")
                    Session("RSQR") = StrArr(0)
                    Session("RSMat") = StrArr(1)
                    Session("RSMatLot") = StrArr(2)
                    Session("RSQty") = StrArr(3)
                    Session("RSinv") = StrArr(4)
                    Session("RSReelno") = StrArr(5)
                    If Session("RSQR") = "QR" Then
                        Dim strMat As String = String.Empty
                        Dim conMat As New OracleConnection(MT800DB)
                        Dim cmdMat As New OracleCommand("select MATERIAL from NVP_CHEMICAL_MAT where MATERIAL = '" & Session("RSMat") & "' and LINE = '" & ddlLine.SelectedItem.Text & "' and PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "'", conMat)
                        conMat.Open()
                        Dim rdMat As OracleDataReader = cmdMat.ExecuteReader()
                        Try
                            While rdMat.Read()
                                If IsDBNull(rdMat.GetValue(0)) = True Then strMat = String.Empty Else strMat = CStr(rdMat.GetValue(0))
                            End While
                        Finally
                            rdMat.Close()
                        End Try
                        conMat.Close()
                        If strMat = Session("RSMat") Then
                            If IsNumeric(Session("RSQty")) Then
                                Dim chkPAY As Boolean
                                chkPAY = False
                                Dim conn As New OracleConnection(MTLE)
                                Dim cmd As New OracleCommand("select * from IMFR_UT_ASEAN_MCS_LOT_HST@IM_LINK WHERE IMFR_UD_TRAN_CLAS_CD ='PSO' and imfr_ud_label_ID = '" & Session("RSReelno") & "'", conn)
                                conn.Open()
                                Dim rd As OracleDataReader = cmd.ExecuteReader()
                                Try
                                    While rd.Read()
                                        chkPAY = True
                                    End While
                                Finally
                                    rd.Close()
                                End Try
                                conn.Close()
                                If chkPAY = True Then
                                    Session("RSQR") = StrArr(0)
                                    Session("RSMat") = StrArr(1)
                                    Session("RSMatLot") = StrArr(2)
                                    Session("RSQty") = StrArr(3)
                                    Session("RSinv") = StrArr(4)
                                    Session("RSReelno") = StrArr(5)
                                    lbshowQuantity.InnerText = StrArr(3)
                                    lbshowInvoice.InnerText = StrArr(4)
                                    lbshowMaterialLot.InnerText = StrArr(2)
                                    lbshowMat.InnerText = StrArr(1)
                                Else
                                    MesgBox("Material Reelno " & Session("RSReelno") & " นี้ยังไม่ถูกตัดจ่ายในระบบ MCS")
                                    'Session("HISERROR") = "บาร์โค้ดจำนวนที่ไม่ใช่ตัวเลข *" & Session("RSQty") & "* "

                                    CLEAR_TnMat()
                                End If
                            Else
                                MesgBox("จำนวน / Quantity " & Session("RSQty") & " ไม่ใช้ตัวเลข กรุณาตรวจสอบและทำการยิงลาเบลใหม่")
                                Session("HISERROR") = "บาร์โค้ดจำนวนที่ไม่ใช่ตัวเลข *" & Session("RSQty") & "* "

                                CLEAR_TnMat()
                            End If
                        Else
                            MesgBox("บาร์โค้ด " & tnMaterial.Text & "ไม่ถูกต้อง")
                            Session("HISERROR") = "บาร์โค้ดที่ไม่ถูกต้อง *" & tnMaterial.Text & "* "

                            CLEAR_TnMat()
                        End If
                    Else
                        MesgBox("ไม่พบ Chemical_ASS " & Session("RSMat") & " ในระบบกรุณาตรวจสอบหรือแจ้งผู้ดูแลระบบ")
                        Session("HISERROR") = "ไม่มี MATERIAL *" & Session("RSMat") & "* ในระบบ"

                        CLEAR_TnMat()
                    End If
                Case False
                    MesgBox("บาร์โค้ด " & tnMaterial.Text & "ไม่ถูกต้อง")
                    Session("HISERROR") = "มีการแสกนบาร์โค้ดที่ไม่ถูกต้อง *" & tnMaterial.Text & "* "

                    CLEAR_TnMat()
            End Select
        End If
    End Sub
    Private Sub CLEAR_TnMat()
        Session("RSQR") = String.Empty
        Session("RSMat") = String.Empty
        Session("RSMatLot") = String.Empty
        Session("RSQty") = String.Empty
        Session("RSinv") = String.Empty
        Session("RSReelno") = String.Empty
        tnMaterial.Text = String.Empty
        lbshowQuantity.InnerText = String.Empty
        lbshowInvoice.InnerText = String.Empty
        lbshowMaterialLot.InnerText = String.Empty
        lbshowMat.InnerText = String.Empty
    End Sub
    Private Sub Button1_ServerClick(sender As Object, e As EventArgs) Handles Button1.Click
        If tnMaterial.Text <> String.Empty And Session("RSQR") <> String.Empty Then
            Dim objConn As New OracleConnection
            Dim strConnString As String = MT800DB
            Dim strSQL As String = "insert into NVP_ALLPROCESS_HIS" +
"(USERNAME,DAY,TIME,MATERIAL,INVOICE,MATERIALLOT,QTY,REELNO,LINE,PRODUCTS,PROCESS)" +
"values" +
"(:sUSERNAME,:sDAY,:sTIME,:sMATERIAL,:sINVOICE,:sMATERIALLOT,:sQTY,:sREELNO,:sLINE,:sPRODUCTS,:sPROCESS)"
            objConn.ConnectionString = strConnString
            objConn.Open()
            Dim objCmd As New OracleCommand(strSQL, objConn)
            objCmd.Parameters.Add("@sUSERNAME", lbshowFRONTENDLINE.InnerText)
            objCmd.Parameters.Add("@sDAY", CStr(Format(Now, "dd-MMM-yyyy")))
            objCmd.Parameters.Add("@sTIME", CStr(Format(Now, "HH:mm:ss")))
            objCmd.Parameters.Add("@sMATERIAL", Session("RSMat"))
            objCmd.Parameters.Add("@sINVOICE", Session("RSinv"))
            objCmd.Parameters.Add("@sMATERIALLOT", Session("RSMatLot"))
            objCmd.Parameters.Add("@sQTY", Session("RSQty"))
            objCmd.Parameters.Add("@sREELNO", Session("RSReelno"))
            objCmd.Parameters.Add("@sLINE", ddlLine.SelectedItem.Value)
            objCmd.Parameters.Add("@sPRODUCTS", ddlLinePD.SelectedItem.Value)
            objCmd.Parameters.Add("@sPROCESS", "CHEMICAL")
            objCmd.ExecuteNonQuery()
            objConn.Close()
            objConn = Nothing
            Load_gvHIS()
            MesgBox("บันทึกข้อมูลเรียบร้อย")
            Session("RSQR") = String.Empty
            Session("RSMat") = String.Empty
            Session("RSMatLot") = String.Empty
            Session("RSQty") = String.Empty
            Session("RSinv") = String.Empty
            Session("RSReelno") = String.Empty
            tnMaterial.Text = String.Empty
            lbshowQuantity.InnerText = String.Empty
            lbshowInvoice.InnerText = String.Empty
            lbshowMaterialLot.InnerText = String.Empty
            lbshowMat.InnerText = String.Empty
            PNBUT.Visible = False
            tnMaterial.Focus()
        End If
    End Sub
    Private Sub Load_gvHIS()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "SELECT USERNAME,DAY,TIME,MATERIAL,INVOICE,MATERIALLOT,QTY,REELNO,LINE,PRODUCTS FROM NVP_ALLPROCESS_HIS"
        acc += " where LINE = '" & ddlLine.SelectedItem.Text & "' and PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "'"
        acc += " ORDER BY TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME DESC"
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

        Load_gvHIS()
    End Sub
    Private Sub btnExportEC1_ServerClick(sender As Object, e As EventArgs) Handles btnExportEC1.Click
        Dim dt As New DataTable("GridView_Data")
        gvHIS.AllowPaging = False
        Load_gvHIS()
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
