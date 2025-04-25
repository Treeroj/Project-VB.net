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
Partial Class MBSetupModelAssembly

    Inherits System.Web.UI.Page
    Dim MT800DB As String = ""
    Dim MTLE As String = ""
    Dim PROCESS As String = "ASSEMBLY"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Page.Form.Attributes.Add("enctype", "multipart/form-data")
            Panel1.Visible = True
            tbModel.Text = String.Empty
        End If

        If tbModel.Text = String.Empty And tbLot.Text = String.Empty And tnMaterial.Text = String.Empty Then
            tbModel.Focus()
        ElseIf tbLot.Text = String.Empty And tbModel.Text <> String.Empty And tnMaterial.Text = String.Empty Then
            tbLot.Focus()
        ElseIf tbLot.Text <> String.Empty And tbModel.Text <> String.Empty And tnMaterial.Text = String.Empty Then
            tnMaterial.Focus()
            'ElseIf tbLot.Text <> String.Empty And tbModel.Text <> String.Empty And tnMaterial.Text <> String.Empty Then
            '    TBshowQuantity.Focus()
        ElseIf tbLot.Text <> String.Empty And tbModel.Text <> String.Empty And tnMaterial.Text <> String.Empty And TBshowQuantity.Text <> String.Empty Then
            Button1.Focus()
        End If

        'MsgBox(Session("Problem"))
    End Sub
    Private Sub MesgBox(ByVal sMessage As String)
        Dim msg As String
        msg = "<script language='javascript'>"
        msg += "alert('" & sMessage & "');"
        msg += "</script>"
        Response.Write(msg)
    End Sub
    Protected Sub tbUser_TextChanged(sender As Object, e As EventArgs)

        pnLine.Visible = False
        PanetbModel.Visible = False

        lbshowFRONTENDLINE.InnerText = String.Empty
        lbshowCode.InnerText = String.Empty
        Session("myGridViewData") = String.Empty
        tbModel.Text = String.Empty
        tbLot.Text = String.Empty
        PanetbLot.Visible = False
        PanelMATERIAL.Visible = False
        Session("RSQR") = String.Empty
        Session("RSMat") = String.Empty
        Session("RSMatLot") = String.Empty
        Session("RSQty") = String.Empty
        Session("RSinv") = String.Empty
        Session("RSReelno") = String.Empty
        tnMaterial.Text = String.Empty
        TBshowQuantity.Text = String.Empty
        lbshowInvoice.InnerText = String.Empty
        lbshowMaterialLot.InnerText = String.Empty
        PanelMATERIALHIS.Visible = False
        PanetbMaterialBAR.Visible = False
        PNBUT.Visible = False
        lbshowMat.InnerText = String.Empty
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
                Load_ddlLinePD()
                PNddlLinePD.Visible = True
            Case False
                ddlLinePD.SelectedIndex.ToString()
                PNddlLinePD.Visible = False
                MesgBox("รหัส " & tbUser.Text & " ของท่านไม่ถูกต้องหรือยังไม่ได้ลงทะเบียนในระบบ")
                pnLine.Visible = False
                PanetbModel.Visible = False
                tbUser.Text = String.Empty
                lbshowFRONTENDLINE.InnerText = String.Empty
                lbshowCode.InnerText = String.Empty
                Session("myGridViewData") = String.Empty
                tbModel.Text = String.Empty
                tbLot.Text = String.Empty
                PanetbLot.Visible = False
                PanelMATERIAL.Visible = False
                Session("RSQR") = String.Empty
                Session("RSMat") = String.Empty
                Session("RSMatLot") = String.Empty
                Session("RSQty") = String.Empty
                Session("RSinv") = String.Empty
                Session("RSReelno") = String.Empty
                tnMaterial.Text = String.Empty
                TBshowQuantity.Text = String.Empty
                lbshowInvoice.InnerText = String.Empty
                lbshowMaterialLot.InnerText = String.Empty
                PanelMATERIALHIS.Visible = False
                PanetbMaterialBAR.Visible = False
                PNBUT.Visible = False
                lbshowMat.InnerText = String.Empty
        End Select
    End Sub
    Private Sub Load_UesrName()
        Dim strUser As String = String.Empty
        Dim strUserID As String = String.Empty
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand("select USERNAME,USERID from NVP_ALLPROCESS_USER where USERID = '" & tbUser.Text & "' and PROCESS = '" & PROCESS & "' or PROCESS = 'ALL'", conn)
        conn.Open()
        Dim rd As OracleDataReader = cmd.ExecuteReader()
        Try
            While rd.Read()
                If IsDBNull(rd.GetValue(0)) = True Then strUser = String.Empty Else strUser = CStr(rd.GetValue(0))
                If IsDBNull(rd.GetValue(1)) = True Then strUserID = String.Empty Else strUserID = CStr(rd.GetValue(1))
                If strUserID = tbUser.Text Then
                    Exit While
                End If
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
        Dim acc As String = " select PRODUCTS From NVP_ALLPROCESS_PRODUCTS where PROCESS = '" & PROCESS & "'"
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
        pnLine.Visible = False
        PanetbModel.Visible = False

        lbshowCode.InnerText = String.Empty
        Session("myGridViewData") = String.Empty
        tbModel.Text = String.Empty
        tbLot.Text = String.Empty
        PanetbLot.Visible = False
        PanelMATERIAL.Visible = False
        Session("RSQR") = String.Empty
        Session("RSMat") = String.Empty
        Session("RSMatLot") = String.Empty
        Session("RSQty") = String.Empty
        Session("RSinv") = String.Empty
        Session("RSReelno") = String.Empty
        tnMaterial.Text = String.Empty
        TBshowQuantity.Text = String.Empty
        lbshowInvoice.InnerText = String.Empty
        lbshowMaterialLot.InnerText = String.Empty
        PanelMATERIALHIS.Visible = False
        PanetbMaterialBAR.Visible = False
        PNBUT.Visible = False
        lbshowMat.InnerText = String.Empty
        Select Case ddlLinePD.SelectedIndex = 0
            Case True
                ddlLinePD.SelectedIndex.ToString()

                pnLine.Visible = False
                PanetbModel.Visible = False
                'tbUser.Text = String.Empty
                lbshowFRONTENDLINE.InnerText = String.Empty
                lbshowCode.InnerText = String.Empty
                Session("myGridViewData") = String.Empty
                tbModel.Text = String.Empty
                tbLot.Text = String.Empty
                PanetbLot.Visible = False
                PanelMATERIAL.Visible = False
                Session("RSQR") = String.Empty
                Session("RSMat") = String.Empty
                Session("RSMatLot") = String.Empty
                Session("RSQty") = String.Empty
                Session("RSinv") = String.Empty
                Session("RSReelno") = String.Empty
                tnMaterial.Text = String.Empty
                TBshowQuantity.Text = String.Empty
                lbshowInvoice.InnerText = String.Empty
                lbshowMaterialLot.InnerText = String.Empty
                PanelMATERIALHIS.Visible = False
                PanetbMaterialBAR.Visible = False
                PNBUT.Visible = False
                lbshowMat.InnerText = String.Empty
            Case False
                pnLine.Visible = True
                Call Load_ddlLine()
                Call Load_UesrName()
        End Select
    End Sub
    Private Sub Load_ddlLine()
        pnLine.Visible = True
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select LINE From NVP_ALLPROCESS_LINE where PRODUCTS = '" & ddlLinePD.SelectedItem.Value & "' and PROCESS = '" & PROCESS & "'"
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
        Select Case ddlLine.SelectedIndex = 0
            Case True
                PanetbModel.Visible = False
                Session("myGridViewData") = String.Empty
                tbModel.Text = String.Empty
                tbLot.Text = String.Empty
                PanetbLot.Visible = False
                PanelMATERIAL.Visible = False
                Session("RSQR") = String.Empty
                Session("RSMat") = String.Empty
                Session("RSMatLot") = String.Empty
                Session("RSQty") = String.Empty
                Session("RSinv") = String.Empty
                Session("RSReelno") = String.Empty
                tnMaterial.Text = String.Empty
                TBshowQuantity.Text = String.Empty
                lbshowInvoice.InnerText = String.Empty
                lbshowMaterialLot.InnerText = String.Empty
                PanelMATERIALHIS.Visible = False
                PanetbMaterialBAR.Visible = False
                PNBUT.Visible = False
                lbshowMat.InnerText = String.Empty
            Case False
                PanetbModel.Visible = True
        End Select
    End Sub
    Protected Sub tbModel_TextChanged(sender As Object, e As EventArgs)

        Session("myGridViewData") = String.Empty
        Dim strMODEL As String = String.Empty
        Dim conMODEL As New OracleConnection(MT800DB)
        Dim cmdMODEL As New OracleCommand("select MODEL from NVP_ASSEMBLY_MAT where MODEL = '" & tbModel.Text & "' and PRODUCTS = '" & ddlLinePD.SelectedItem.Value & "' and PROCESS = '" & PROCESS & "'", conMODEL)
        conMODEL.Open()
        Dim rdMat As OracleDataReader = cmdMODEL.ExecuteReader()
        Try
            While rdMat.Read()
                If IsDBNull(rdMat.GetValue(0)) = True Then strMODEL = String.Empty Else strMODEL = CStr(rdMat.GetValue(0))
            End While
        Finally
            rdMat.Close()
        End Try
        conMODEL.Close()
        Select Case tbModel.Text <> String.Empty And strMODEL = tbModel.Text
            Case True
                PanelMATERIAL.Visible = True
                ddlLine.SelectedValue = ddlLine.SelectedItem.Text
                PanetbLot.Visible = True

                Call Load_gvModel()
                'Call Load_gvModel()
            Case False
                MesgBox("ไม่พบ Model " & tbModel.Text & " ในระบบ กรุณาตรวจสอบหรือแจ้งผู้ดูแลระบบ")
                Session("LOGSTATUS") = "ERROR"
                Session("HISERROR") = "มีการยิง Model ที่ไม่มีในระบบ *" & tbModel.Text & "* "
                Load_HisError()


                Session("myGridViewData") = String.Empty
                tbModel.Text = String.Empty
                tbLot.Text = String.Empty
                PanetbLot.Visible = False
                PanelMATERIAL.Visible = False
                Session("RSQR") = String.Empty
                Session("RSMat") = String.Empty
                Session("RSMatLot") = String.Empty
                Session("RSQty") = String.Empty
                Session("RSinv") = String.Empty
                Session("RSReelno") = String.Empty
                tnMaterial.Text = String.Empty
                TBshowQuantity.Text = String.Empty
                lbshowInvoice.InnerText = String.Empty
                lbshowMaterialLot.InnerText = String.Empty
                PanelMATERIALHIS.Visible = False
                PanetbMaterialBAR.Visible = False
                PNBUT.Visible = False
                lbshowMat.InnerText = String.Empty
        End Select
    End Sub
    Private Sub Load_gvModel()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select MODEL,MATERIAL,SCM From NVP_ASSEMBLY_MAT where MODEL = '" & tbModel.Text & "' and PRODUCTS = '" & ddlLinePD.SelectedItem.Value & "' and PROCESS = '" & PROCESS & "' "
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            GridViewModel.DataSource = dt
            GridViewModel.DataBind()
        End Using
    End Sub
    Protected Sub tbLot_TextChanged(sender As Object, e As EventArgs) Handles tbLot.TextChanged
        Select Case tbLot.Text <> String.Empty
            Case True
                PanelMATERIALHIS.Visible = True
                ddlLine.SelectedValue = ddlLine.SelectedItem.Text
                'PanetbLot.Visible = True
                PanetbMaterialBAR.Visible = True
                Call Load_gvLot()
                Call Load_gvModel()
                'Call Load_gvModel()

            Case False

                Session("myGridViewData") = String.Empty
                Call Load_gvModel()
                tbLot.Text = String.Empty


                Session("RSQR") = String.Empty
                Session("RSMat") = String.Empty
                Session("RSMatLot") = String.Empty
                Session("RSQty") = String.Empty
                Session("RSinv") = String.Empty
                Session("RSReelno") = String.Empty
                tnMaterial.Text = String.Empty
                TBshowQuantity.Text = String.Empty
                lbshowInvoice.InnerText = String.Empty
                lbshowMaterialLot.InnerText = String.Empty
                PanelMATERIALHIS.Visible = False
                PanetbMaterialBAR.Visible = False
                PNBUT.Visible = False
                lbshowMat.InnerText = String.Empty
        End Select

    End Sub
    Private Sub Load_gvLot()

        Select Case tbLot.Text <> String.Empty
            Case True
                Dim conn As New OracleConnection(MT800DB)
                Dim cmd As New OracleCommand()
                Dim acc As String = " select MODEL,LOTNO,MATERIAL,QTY,INVOICE,MATERIALLOT,REELNO From NVP_ALLPROCESS_HIS where MODEL = '" & tbModel.Text & "' and LOTNO = '" & tbLot.Text & "' and PRODUCTS = '" & ddlLinePD.SelectedItem.Value & "' and PROCESS = '" & PROCESS & "'"
                acc += " ORDER BY TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME DESC"
                cmd.CommandText = acc
                cmd.Connection = conn
                Using sda As New OracleDataAdapter(cmd)
                    Dim dt As New DataTable()
                    sda.Fill(dt)
                    GridViewLot.DataSource = dt
                    GridViewLot.DataBind()
                    Session("myGridViewData") = dt
                    ShowMaterialCount.InnerText = dt.Rows.Count
                End Using
            Case False
        End Select
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridViewLot.PageIndex = e.NewPageIndex
        GridViewLot.DataSource = CType(Session("myGridViewData"), DataTable)
        GridViewLot.DataBind()
        Call Load_gvLot()
    End Sub
    Protected Sub tnMaterial_TextChanged(sender As Object, e As EventArgs) Handles tnMaterial.TextChanged
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

                    Dim StrArr6() As String
                    StrArr6 = Str5.Split("|")
                    For i = 0 To UBound(StrArr6)
                        Session(StrArr6(i)) = StrArr6(i)
                    Next



                    If Session("RSQR") = "QR" Then
                        Dim strMat As String = String.Empty
                        Dim conMat As New OracleConnection(MT800DB)
                        Dim cmdMat As New OracleCommand("select MATERIAL from NVP_ASSEMBLY_MAT where MODEL = '" & tbModel.Text & "' and MATERIAL = '" & Session("RSMat") & "'", conMat)
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

                            Dim strReelno As String = String.Empty
                            Dim strModel As String = String.Empty
                            Dim strLot As String = String.Empty
                            Dim conReelno As New OracleConnection(MT800DB)
                            Dim cmdReelno As New OracleCommand("select REELNO,MODEL,LOTNO from NVP_ALLPROCESS_HIS where MODEL = '" & tbModel.Text & "' and REELNO = '" & Session("RSReelno") & "'and LOTNO = '" & tbLot.Text & "' ", conReelno)
                            conReelno.Open()
                            Dim rdReelno As OracleDataReader = cmdReelno.ExecuteReader()
                            Try
                                While rdReelno.Read()
                                    If IsDBNull(rdReelno.GetValue(0)) = True Then strReelno = String.Empty Else strReelno = CStr(rdReelno.GetValue(0))
                                    If IsDBNull(rdReelno.GetValue(1)) = True Then strModel = String.Empty Else strModel = CStr(rdReelno.GetValue(1))
                                    If IsDBNull(rdReelno.GetValue(2)) = True Then strLot = String.Empty Else strLot = CStr(rdReelno.GetValue(2))
                                End While
                            Finally
                                rdReelno.Close()
                            End Try
                            conReelno.Close()
                            If strReelno <> Session("RSReelno") Then
                                If IsNumeric(Session("RSQty")) Then
                                    Dim chkPAY As Boolean
                                    chkPAY = False
                                    Dim conn As New OracleConnection(MTLE)
                                    Dim cmd As New OracleCommand("select T1.IMFR_SD_LABEL_ID , T2.PAYOUT from imfr_UT_ASEAN_MCS_LOT_FILE@IM_LINK T1 left join (select IMFR_UD_LABEL_ID,'ALREADY PAYOUT' as PAYOUT from imfr_UT_ASEAN_MCS_LOT_HST@IM_LINK where IMFR_UD_TRAN_CLAS_CD = 'PSO') T2 on T1.IMFR_SD_LABEL_ID = T2.IMFR_UD_LABEL_ID where T1.IMFR_UD_AVAIL_QTY + T1.IMFR_UD_HLD_QTY + T1.IMFR_UD_ALLO_QTY = 0 and T1.imfr_sd_label_id = '" & Session("RSReelno") & "'", conn)
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
                                        PanelMATERIALHIS.Visible = True
                                        TBshowQuantity.Text = StrArr(3)
                                        lbshowInvoice.InnerText = StrArr(4)
                                        lbshowMaterialLot.InnerText = StrArr(2)
                                        lbshow.Visible = True
                                        lbshowMat.InnerText = StrArr(1)
                                        TBshowQuantity.Focus()
                                        PNBUT.Visible = True
                                    Else
                                        MesgBox("Material Reelno " & Session("RSReelno") & " นี้ยังไม่ถูกตัดจ่ายในระบบ MCS")
                                        Session("HISERROR") = "Material Reelno *" & Session("RSReelno") & "* นี้ยังไม่ถูกตัดจ่ายในระบบ MCS"
                                        Session("LOGSTATUS") = "ERROR"
                                        Load_HisError()
                                        CLEAR_TnMat()
                                    End If
                                Else
                                    MesgBox("จำนวน / Quantity " & Session("RSQty") & " ไม่ใช่ตัวเลข กรุณาตรวจสอบและทำการยิงลาเบลใหม่")
                                Session("HISERROR") = "บาร์โค้ดจำนวนที่ไม่ใช่ตัวเลข *" & Session("RSQty") & "* "
                                Session("LOGSTATUS") = "ERROR"
                                Load_HisError()
                                CLEAR_TnMat()
                            End If
                            'ElseIf strReelno = Session("RSReelno") And strLot <> String.Empty And tbLot.Text <> strLot Then
                            '    MesgBox("หมายเลข ReelNo " & Session("RSR0eelno") & " ถูกยิงใน Lot " & strLot & " เรียบร้อยแล้ว กรุณาตรวจสอบและทำการยิงลาเบลใหม่")
                            '    Session("HISERROR") = "หมายเลข ReelNo *" & Session("RSReelno") & "* ซ้ำ กับ Lot *" & strLot & "* "
                            '    Session("LOGSTATUS") = "ERROR"
                            '    Load_HisError()
                            '    CLEAR_TnMat()
                        Else
                            MesgBox("หมายเลข ReelNo " & Session("RSReelno") & " ซ้ำกันในระบบโปรตรวจสอบหรือแจ้งผู้ดูแลระบบ")
                            Session("HISERROR") = "หมายเลข ReelNo ซ้ำกัน *" & Session("RSReelno") & "* "
                            Session("LOGSTATUS") = "ERROR"
                            Load_HisError()
                            CLEAR_TnMat()
                        End If
                    Else
                        MesgBox("ไม่พบ MATERIAL " & Session("RSMat") & " ใน Model นี้กรุณาตรวจสอบหรือแจ้งผู้ดูแลระบบ")
                            Session("HISERROR") = "ไม่มี MATERIAL *" & Session("RSMat") & "* ใน Model *" & tbModel.Text & "* "
                            Session("LOGSTATUS") = "ERROR"
                            Load_HisError()
                            CLEAR_TnMat()
                        End If
                    Else
                        MesgBox("บาร์โค้ด " & tnMaterial.Text & "ไม่ถูกต้อง")
                        Session("HISERROR") = "บาร์โค้ดที่ไม่ถูกต้อง *" & tnMaterial.Text & "* "
                        Session("LOGSTATUS") = "ERROR"
                        Load_HisError()
                        CLEAR_TnMat()
                    End If
                Case False
                    MesgBox("บาร์โค้ด " & tnMaterial.Text & "ไม่ถูกต้อง")
                    Session("HISERROR") = "มีการแสกนบาร์โค้ดที่ไม่ถูกต้อง *" & tnMaterial.Text & "* "
                    Session("LOGSTATUS") = "ERROR"
                    Load_HisError()
                    CLEAR_TnMat()
            End Select
        End If
    End Sub
    Protected Sub TBshowQuantity_TextChanged(sender As Object, e As EventArgs) Handles TBshowQuantity.TextChanged
        If TBshowQuantity.Text = Session("RSQty") Then
        Else
            Session("HISERROR") = "มีการเปลี่ยนจำนวนจาก *" & Session("RSQty") & "* เป็น *" & TBshowQuantity.Text & "*"
            Session("LOGSTATUS") = "CHANGEQTY"
            Load_HisError()
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
        TBshowQuantity.Text = String.Empty
        lbshowInvoice.InnerText = String.Empty
        lbshowMaterialLot.InnerText = String.Empty
        lbshowMat.InnerText = String.Empty
        PNBUT.Visible = False
        lbshow.Visible = False
        TBshowQuantity.Text = String.Empty
        tnMaterial.Focus()
    End Sub
    Private Sub Load_HisError()
        Dim objConn As New OracleConnection
        Dim strConnString As String = MT800DB
        Dim strSQL As String = "insert into NVP_ALLPROCESS_LOGERROR" +
"(USERID,USERNAME,DAY,TIME,HISERROR,MODEL,LOT,PRODUCTS,PROCESS,STATUS)" +
"values" +
"(:sUSERID,:sUSERNAME,:sDAY,:sTIME,:sHISERROR,:sMODEL,:sLOT,:sPRODUCTS,:sPROCESS,:sSTATUS)"
        objConn.ConnectionString = strConnString
        objConn.Open()
        Dim objCmd As New OracleCommand(strSQL, objConn)
        objCmd.Parameters.Add("@sUSERID", tbUser.Text)
        If lbshowFRONTENDLINE.InnerText = String.Empty Then
            objCmd.Parameters.Add("@sUSERNAME", "")
        Else
            objCmd.Parameters.Add("@sUSERNAME", lbshowFRONTENDLINE.InnerText)
        End If
        objCmd.Parameters.Add("@sDAY", CStr(Format(Now, "dd-MMM-yyyy")))
        objCmd.Parameters.Add("@sTIME", CStr(Format(Now, "HH:mm:ss")))
        objCmd.Parameters.Add("@sHISERROR", Session("HISERROR"))
        If tbModel.Text = String.Empty Then
            objCmd.Parameters.Add("@sMODEL", "")
        Else
            objCmd.Parameters.Add("@sMODEL", tbModel.Text)
        End If
        If tbLot.Text = String.Empty Then
            objCmd.Parameters.Add("@sLOT", "")
        Else
            objCmd.Parameters.Add("@sLOT", tbLot.Text)
        End If
        objCmd.Parameters.Add("@sPRODUCTS", ddlLinePD.SelectedItem.Value)
        objCmd.Parameters.Add("@sPROCESS", PROCESS)
        objCmd.Parameters.Add("@sSTATUS", Session("LOGSTATUS"))
        objCmd.ExecuteNonQuery()
        objConn.Close()
        objConn = Nothing
        Session("LOGSTATUS") = String.Empty
        Session("HISERROR") = String.Empty
    End Sub
    Protected Sub GridViewModel_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' ดึงค่า MATERIAL จากแถวปัจจุบันใน GridViewModel
            Dim materialInGridViewModel As String = e.Row.Cells(1).Text ' (แก้ตำแหน่งคอลัมน์ตามที่เหมาะสม)

            ' ดึงข้อมูลจาก GridViewLot ที่เก็บใน Session
            Dim dt As DataTable = TryCast(Session("myGridViewData"), DataTable)

            If dt IsNot Nothing Then
                ' ตรวจสอบว่า MATERIAL เหมือนกันในทั้งสองตาราง
                Dim matchingRows() As DataRow = dt.Select("MATERIAL = '" & materialInGridViewModel & "'")

                ' ถ้ามี MATERIAL เหมือนกันใน GridViewLot ก็เปลี่ยนสีแถวใน GridViewModel เป็นสีแดง
                If matchingRows.Length > 0 Then
                    e.Row.BackColor = Drawing.Color.FromArgb(140, 231, 255)
                Else
                    e.Row.BackColor = Drawing.Color.White
                End If
            End If
        End If
    End Sub
    Private Sub Button1_ServerClick(sender As Object, e As EventArgs) Handles Button1.Click
        If tnMaterial.Text <> String.Empty And Session("RSQR") <> String.Empty Then
            If IsNumeric(TBshowQuantity.Text) Then
                Dim objConn As New OracleConnection
                Dim strConnString As String = MT800DB
                Dim strSQL As String = "insert into NVP_ALLPROCESS_HIS" +
"(USERID,USERNAME,DAY,TIME,MODEL,LOTNO,MATERIAL,INVOICE,MATERIALLOT,QTY,REELNO,LINE,PRODUCTS,PROCESS)" +
"values" +
"(:sUSERID,:sUSERNAME,:sDAY,:sTIME,:sMODEL,:sLOTNO,:sMATERIAL,:sINVOICE,:sMATERIALLOT,:sQTY,:sREELNO,:sLINE,:sPRODUCTS,:sPROCESS)"
                objConn.ConnectionString = strConnString
                objConn.Open()
                Dim objCmd As New OracleCommand(strSQL, objConn)
                objCmd.Parameters.Add("@sUSERID", lbshowCode.InnerText)
                objCmd.Parameters.Add("@sUSERNAME", lbshowFRONTENDLINE.InnerText)
                objCmd.Parameters.Add("@sDAY", CStr(Format(Now, "dd-MMM-yyyy")))
                objCmd.Parameters.Add("@sTIME", CStr(Format(Now, "HH:mm:ss")))
                objCmd.Parameters.Add("@sMODEL", tbModel.Text)
                objCmd.Parameters.Add("@sLOTNO", tbLot.Text)
                objCmd.Parameters.Add("@sMATERIAL", Session("RSMat"))
                objCmd.Parameters.Add("@sINVOICE", Session("RSinv"))
                objCmd.Parameters.Add("@sMATERIALLOT", Session("RSMatLot"))
                objCmd.Parameters.Add("@sQTY", TBshowQuantity.Text)
                objCmd.Parameters.Add("@sREELNO", Session("RSReelno"))
                objCmd.Parameters.Add("@sLINE", ddlLine.SelectedItem.Value)
                objCmd.Parameters.Add("@sPRODUCTS", ddlLinePD.SelectedItem.Value)
                objCmd.Parameters.Add("@sPROCESS", PROCESS)

                objCmd.ExecuteNonQuery()

                objConn.Close()

                objConn = Nothing

                Load_gvLot()
                Load_gvModel()
                Session("RSQR") = String.Empty
                Session("RSMat") = String.Empty
                Session("RSMatLot") = String.Empty
                Session("RSQty") = String.Empty
                Session("RSinv") = String.Empty
                Session("RSReelno") = String.Empty
                tnMaterial.Text = String.Empty
                TBshowQuantity.Text = String.Empty
                lbshowInvoice.InnerText = String.Empty
                lbshowMaterialLot.InnerText = String.Empty
                lbshowMat.InnerText = String.Empty
                PNBUT.Visible = False
                lbshow.Visible = False
                TBshowQuantity.Text = String.Empty
                tnMaterial.Focus()
            Else
                MesgBox("จำนวน / Quantity " & TBshowQuantity.Text & " ไม่ใช่ตัวเลข กรุณาตรวจสอบและทำการยิงลาเบลใหม่")
                Session("HISERROR") = "บาร์โค้ดจำนวนที่ไม่ใช่ตัวเลข *" & TBshowQuantity.Text & "* "
                Session("LOGSTATUS") = "ERROR"
                Load_HisError()
                'CLEAR_TnMat()
            End If
        End If
    End Sub
    Private Sub Button2_ServerClick(sender As Object, e As EventArgs) Handles Button2.ServerClick
        Session("RSQR") = String.Empty
        Session("RSMat") = String.Empty
        Session("RSMatLot") = String.Empty
        Session("RSQty") = String.Empty
        Session("RSinv") = String.Empty
        Session("RSReelno") = String.Empty
        tnMaterial.Text = String.Empty
        tbUser.Text = String.Empty
        pnLine.Visible = False
        lbshowCode.InnerText = String.Empty
        lbshowFRONTENDLINE.InnerText = String.Empty
        ddlLine.SelectedIndex.ToString()
        ddlLinePD.SelectedIndex = 0
        PanetbModel.Visible = False
        tbModel.Text = String.Empty
        PanetbLot.Visible = False
        tbLot.Text = String.Empty
        PanetbMaterialBAR.Visible = False
        TBshowQuantity.Text = String.Empty
        lbshowInvoice.InnerText = String.Empty
        lbshowMaterialLot.InnerText = String.Empty
        lbshowMat.InnerText = String.Empty
        PNBUT.Visible = False
        PanelMATERIAL.Visible = False
        PNddlLinePD.Visible = False
    End Sub

End Class

