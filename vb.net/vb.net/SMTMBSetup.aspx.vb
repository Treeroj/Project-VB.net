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
Partial Class SMTMBSetup
    Inherits System.Web.UI.Page
    Dim MT800DB As String = ""
    Dim MCS As String = ""
    Dim PROCESS As String = "SMT"
    Dim MTLE As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Page.Form.Attributes.Add("enctype", "multipart/form-data")
            Panel1.Visible = True
        End If

        If tnMaterial.Text = String.Empty And tbCassetteSh.Text = String.Empty Then

            tnMaterial.Focus()

        ElseIf tbCassetteSh.Text = String.Empty And tnMaterial.Text <> String.Empty Then

            tbCassetteSh.Focus()

        End If

        'MsgBox(Session("num5"))
    End Sub
    Private Sub MesgBox(ByVal sMessage As String)
        Dim msg As String
        msg = "<script language='javascript'>"
        msg += "alert('" & sMessage & "');"
        msg += "</script>"
        Response.Write(msg)
    End Sub
    Protected Sub tbUser_TextChanged(sender As Object, e As EventArgs) Handles tbUser.TextChanged
        Dim strUser As String = String.Empty
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand("select USERID from NVP_ALLPROCESS_USER where USERID = '" & tbUser.Text & "' and PROCESS = '" & PROCESS & "' or PROCESS = 'ALL' ", conn)
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
                Session("PCBLM") = 1
                PNddlLinePD.Visible = True
                PanelMATERIALHIS.Visible = False
                ddlLinePD.Focus()
                load_clgvnaja()
                Load_ddlLinePD()
            Case False
                'pnLine = 1 UP------------------------------------------------------------------------------------
                'รหัสประจำตัว / User code : TB            
                tbUser.Text = String.Empty
                'โปรดักส์ / Product : PN------------------------------------------------------------------------------
                PNddlLinePD.Visible = False
                'โปรดักส์ / Product : DDL
                ddlLinePD.SelectedIndex.ToString()

                'pnLine = 1 DONW------------------------------------------------------------------------------------
                pnLine.Visible = False
                'รหัสพนักงาน / Code : LB
                lbshowCode.InnerText = String.Empty
                'ชื่อ / Name : LB
                lbshowFRONTENDLINE.InnerText = String.Empty
                'เวลา / TIME : LB
                lbshowTime.InnerText = String.Empty
                'วันที่ / Date : LB
                lbshowDATE.InnerText = String.Empty
                'โปรดักส์ / Product : LB
                lbshowFRONTENDPD.InnerText = String.Empty
                'ไลน์ / Line : PN-------------------------------------------------------------------------------------
                PNlbshowLine.Visible = False
                'ไลน์ / Line : LB
                lbshowLine.InnerText = String.Empty
                'งานรุ่น / Model : PN----------------------------------------------------------------------------------
                PNModelShow.Visible = False
                'งานรุ่น / Model : LB
                lbshowModel.InnerText = String.Empty
                'หมายเลขล็อต / LOT NO : PN-----------------------------------------------------------------------------
                PNlbshowLot.Visible = False
                'หมายเลขล็อต / LOT NO : LB
                lbshowLot.InnerText = String.Empty
                'เครื่องจักร /MACHINE : PN-----------------------------------------------------------------------------
                PNlbshowMC.Visible = False
                'เครื่องจักร /MACHINE : LB
                lbshowMC.InnerText = String.Empty
                'ฟีด / FEED : PN-----------------------------------------------------------------------------
                PNlbshowFeed.Visible = False
                'ฟีด / FEED : LB
                lbshowFeeddd.InnerText = String.Empty
                'วัสดุ / MATERIAL : PN-----------------------------------------------------------------------------
                PNlbshowMATERIAL.Visible = False
                'วัสดุ / MATERIAL : LB
                lbshowMATERIAL.InnerText = String.Empty

                'PanelMATERIAL = 2 ------------------------------------------------------------------------------------
                PanelMATERIAL.Visible = False
                'ไลน์ / Line : DDL
                ddlLine.SelectedIndex.ToString()
                'งานรุ่น / Model : PN-----------------------------------------------------------------------------
                PanetbModel.Visible = False
                'งานรุ่น / Model : TB
                tbModel.Text = String.Empty
                'PNbutton FBN: PN-----------------------------------------------------------------------------
                PNbutton.Visible = False
                'ล็อตงาน / Lot No : PN-----------------------------------------------------------------------------
                PanetbLot.Visible = False
                'ล็อตงาน / Lot No : TB
                tbLot.Text = String.Empty
                'เครื่องจักร / Machine : PN-----------------------------------------------------------------------------
                PNddlMC.Visible = False
                'เครื่องจักร / Machine : DDL
                ddlLineMC.SelectedIndex.ToString()
                'หมายเลขฟิด / Feed : PN-----------------------------------------------------------------------------
                PNFeed.Visible = False
                'หมายเลขฟิด / Feed : GV
                gvFiles3.Visible = False
                'วัสดุ / MATERIAL : PN-----------------------------------------------------------------------------
                Panel4.Visible = False
                'วัสดุ / MATERIAL : LB
                A2.InnerText = String.Empty
                'จำนวน / Quantity : PN-----------------------------------------------------------------------------
                Panel3.Visible = False
                'จำนวน / Quantity : LB
                A1.InnerText = String.Empty


                'pnMaterialmain = 3 ------------------------------------------------------------------------------------
                pnMaterialmain.Visible = False

                'กรุณายิงบาร์โค้ด  : PN-------------------------------------------------------------------------------
                pnMaterial.Visible = False
                'กรุณายิงบาร์โค้ด  : TB
                tnMaterial.Text = String.Empty
                'วัตถุดิบ / Material : LB
                lbshowMat.InnerText = String.Empty
                'จำนวน / Quantity : LB
                lbshowQty.InnerText = String.Empty
                'อินวอยซ์ / Invoice : LB
                lbshowinv.InnerText = String.Empty
                'ล็อตวัตถุ / Material Lot : LB
                lbshowMatLot.InnerText = String.Empty
                'คาสเซ็ท / Cassette : PN-------------------------------------------------------------------------------
                PNCassetteSh.Visible = False
                'คาสเซ็ท / Cassette : TB
                tbCassetteSh.Text = String.Empty
                'ยืนยัน : BT-------------------------------------------------------------------------------
                PNBUT.Visible = False

                'จำนวน / Quantity : SS
                Session("RSQty") = String.Empty
                'อินวอยซ์ / Invoice : SS
                Session("RSinv") = String.Empty
                'ล็อตวัตถุ / Material Lot : SS
                Session("RSMatLot") = String.Empty
                'RSReelno : SS
                Session("RSReelno") = String.Empty


                MesgBox("รหัสพนักงานไม่ถูกต้อง")
        End Select
    End Sub
    Private Sub Load_ddlLinePD()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select PRODUCTS From NVP_ALLPROCESS_PRODUCTS where PROCESS = '" & PROCESS & "' "
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
    Private Sub Load_UesrName()
        Dim strUser As String = String.Empty
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand("select USERNAME from NVP_ALLPROCESS_USER where USERID = '" & tbUser.Text & "' and PROCESS = '" & PROCESS & "' or PROCESS = 'ALL' ", conn)
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
        'Session("RSCode") = tbUser.Text
        'Session("RSName") = strUser
    End Sub
    Protected Sub ddlLinePD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLinePD.SelectedIndexChanged
        Select Case ddlLinePD.SelectedIndex = 0
            Case True

                pnLine.Visible = False
                lbshowCode.InnerText = String.Empty = String.Empty
                lbshowTime.InnerText = String.Empty
                lbshowDATE.InnerText = String.Empty
                lbshowFRONTENDPD.InnerText = String.Empty
                PNlbshowLine.Visible = False
                lbshowLine.InnerText = String.Empty
                PNModelShow.Visible = False
                lbshowModel.InnerText = String.Empty
                PNlbshowLot.Visible = False
                lbshowLot.InnerText = String.Empty
                PNlbshowMC.Visible =
                lbshowFRONTENDLINE.InnerText = False
                lbshowMC.InnerText = String.Empty
                PNlbshowFeed.Visible = False
                lbshowFeeddd.InnerText = String.Empty
                PNlbshowMATERIAL.Visible = False
                lbshowMATERIAL.InnerText = String.Empty

                PanelMATERIAL.Visible = False
                ddlLine.SelectedIndex.ToString()
                PanetbModel.Visible = False
                tbModel.Text = String.Empty
                PNbutton.Visible = False
                PanetbLot.Visible = False
                tbLot.Text = String.Empty
                PNddlMC.Visible = False
                ddlLineMC.SelectedIndex.ToString()
                PNFeed.Visible = False
                gvFiles3.Visible = False
                Panel4.Visible = False
                A2.InnerText = String.Empty
                Panel3.Visible = False
                A1.InnerText = String.Empty

                pnMaterialmain.Visible = False

                pnMaterial.Visible = False
                tnMaterial.Text = String.Empty
                lbshowMat.InnerText = String.Empty
                lbshowQty.InnerText = String.Empty
                lbshowinv.InnerText = String.Empty
                lbshowMatLot.InnerText = String.Empty
                PNCassetteSh.Visible = False
                tbCassetteSh.Text = String.Empty
                PNBUT.Visible = False

                Session("RSQty") = String.Empty
                Session("RSinv") = String.Empty
                Session("RSMatLot") = String.Empty
                Session("RSReelno") = String.Empty

                PanelMATERIALHIS.Visible = False
            Case False
                ddlLine.Focus()
                lbshowTime.InnerText = CStr(Format(Now, "HH:mm:ss"))
                lbshowDATE.InnerText = CStr(Format(Now, "dd-MMM-yyyy"))
                Call Load_UesrName()
                Call Load_ddlLine()
                lbshowFRONTENDPD.InnerText = ddlLinePD.SelectedItem.Text
                pnLine.Visible = True
                PanelMATERIAL.Visible = True
        End Select
    End Sub
    Private Sub Load_ddlLine()
        pnLine.Visible = True
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select LINE From NVP_ALLPROCESS_LINE where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "'"
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

                PNlbshowLine.Visible = False
                lbshowLine.InnerText = String.Empty
                PNModelShow.Visible = False
                lbshowModel.InnerText = String.Empty
                PNlbshowLot.Visible = False
                lbshowLot.InnerText = String.Empty
                PNlbshowMC.Visible = False
                lbshowMC.InnerText = String.Empty
                PNlbshowFeed.Visible = False
                lbshowFeeddd.InnerText = String.Empty
                PNlbshowMATERIAL.Visible = False
                lbshowMATERIAL.InnerText = String.Empty

                PanetbModel.Visible = False
                tbModel.Text = String.Empty
                PNbutton.Visible = False
                PanetbLot.Visible = False
                tbLot.Text = String.Empty
                PNddlMC.Visible = False
                ddlLineMC.SelectedIndex.ToString()
                PNFeed.Visible = False
                gvFiles3.Visible = False
                Panel4.Visible = False
                A2.InnerText = String.Empty
                Panel3.Visible = False
                A1.InnerText = String.Empty

                pnMaterialmain.Visible = False

                pnMaterial.Visible = False
                tnMaterial.Text = String.Empty
                lbshowMat.InnerText = String.Empty
                lbshowQty.InnerText = String.Empty
                lbshowinv.InnerText = String.Empty
                lbshowMatLot.InnerText = String.Empty
                PNCassetteSh.Visible = False
                tbCassetteSh.Text = String.Empty
                PNBUT.Visible = False

                Session("RSQty") = String.Empty
                Session("RSinv") = String.Empty
                Session("RSMatLot") = String.Empty
                Session("RSReelno") = String.Empty

                PanelMATERIALHIS.Visible = False
            Case False
                lbshowLine.InnerText = ddlLine.Text
                PNlbshowLine.Visible = True
                PanetbModel.Visible = True
                tbModel.Focus()
                'tbModel.Focus()
        End Select
    End Sub
    Protected Sub tbModel_TextChanged(sender As Object, e As EventArgs) Handles tbModel.TextChanged
        Dim strUser As String = String.Empty
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand("select MODEL from NVP_SMT_MAT where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and MODEL = '" & tbModel.Text & "' and PROCESS = '" & PROCESS & "' ", conn)
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

        Select Case tbModel.Text = strUser And tbModel.Text <> String.Empty
            Case True

                'PanelMATERIAL.Visible = True
                ddlLine.SelectedValue = ddlLine.SelectedItem.Text
                PNbutton.Visible = True


            Case False
                MesgBox("ไม่พบในฐานข้อมูล")
                PNModelShow.Visible = False
                lbshowModel.InnerText = String.Empty
                PNlbshowLot.Visible = False
                lbshowLot.InnerText = String.Empty
                PNlbshowMC.Visible = False
                lbshowMC.InnerText = String.Empty
                PNlbshowFeed.Visible = False
                lbshowFeeddd.InnerText = String.Empty
                PNlbshowMATERIAL.Visible = False
                lbshowMATERIAL.InnerText = String.Empty

                PNbutton.Visible = False
                PanetbLot.Visible = False
                tbLot.Text = String.Empty
                PNddlMC.Visible = False
                ddlLineMC.SelectedIndex.ToString()
                PNFeed.Visible = False
                gvFiles3.Visible = False
                Panel4.Visible = False
                A2.InnerText = String.Empty
                Panel3.Visible = False
                A1.InnerText = String.Empty

                pnMaterialmain.Visible = False

                pnMaterial.Visible = False
                tnMaterial.Text = String.Empty
                lbshowMat.InnerText = String.Empty
                lbshowQty.InnerText = String.Empty
                lbshowinv.InnerText = String.Empty
                lbshowMatLot.InnerText = String.Empty
                PNCassetteSh.Visible = False
                tbCassetteSh.Text = String.Empty
                PNBUT.Visible = False

                Session("RSQty") = String.Empty
                Session("RSinv") = String.Empty
                Session("RSMatLot") = String.Empty
                Session("RSReelno") = String.Empty

                PanelMATERIALHIS.Visible = False

        End Select
    End Sub

    Private Sub btnFrontMD_ServerClick(sender As Object, e As EventArgs) Handles btnFrontMD.ServerClick
        lbshowModel.InnerText = tbModel.Text & "-F"
        tbModel.Text = tbModel.Text & "-F"
        PanetbLot.Visible = True
        tbLot.Focus()
        'tbLot.Focus()
    End Sub
    Private Sub btnBackMD_ServerClick(sender As Object, e As EventArgs) Handles btnBackMD.ServerClick
        lbshowModel.InnerText = tbModel.Text & "-B"

        tbModel.Text = tbModel.Text & "-B"
        PanetbLot.Visible = True
        tbLot.Focus()
        'tbLot.Focus()
    End Sub
    Private Sub btnNoneMD_ServerClick(sender As Object, e As EventArgs) Handles btnNoneMD.ServerClick
        lbshowModel.InnerText = tbModel.Text
        PanetbLot.Visible = True
        tbLot.Focus()
        'tbLot.Focus()

    End Sub
    Protected Sub tbLot_TextChanged(sender As Object, e As EventArgs) Handles tbLot.TextChanged
        Dim strMC As String = String.Empty
        Dim connn As New OracleConnection(MT800DB)
        Dim cmdd As New OracleCommand("select MODEL from NVP_SMT_MAT where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and MODEL = '" + lbshowModel.InnerText + "' and PROCESS = '" & PROCESS & "'", connn)
        connn.Open()
        Dim rdd As OracleDataReader = cmdd.ExecuteReader()
        Try
            While rdd.Read()
                If IsDBNull(rdd.GetValue(0)) = True Then strMC = String.Empty Else strMC = CStr(rdd.GetValue(0))
            End While
        Finally
            rdd.Close()
        End Try
        connn.Close()
        Session("MCModel") = strMC
        Select Case lbshowModel.InnerText = strMC And tbLot.Text <> String.Empty
            Case True
                PanelMATERIAL.Visible = True
                PNddlMC.Visible = True
                PNlbshowLot.Visible = True
                lbshowLot.InnerText = tbLot.Text
                Call Load_ddlLineMC()
                gvFiles3.Visible = True
                pnMaterial.Visible = True
                ddlLineMC.Focus()
            Case False

                PNFeed.Visible = False
                Panel4.Visible = False
                Panel3.Visible = False

                PNddlMC.Visible = False

                PNbutton.Visible = False
                lbshowModel.InnerText = String.Empty
                MesgBox("ชื่อ Model ไม่พบในฐานข้อมูล")
                tbLot.Text = String.Empty
                tbModel.Text = String.Empty
                PanetbLot.Visible = False
        End Select
    End Sub
    Private Sub Load_ddlLineMC()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select DISTINCT MACHINE From NVP_SMT_MAT where MODEL = '" + lbshowModel.InnerText + "' and PROCESS = '" & PROCESS & "' and PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            ddlLineMC.DataSource = dt
            ddlLineMC.DataTextField = "MACHINE"
            ddlLineMC.DataValueField = "MACHINE"
            ddlLineMC.DataBind()
            ddlLineMC.Items.Insert(0, New ListItem("(กรุณาเลือก เครื่องจักร)", String.Empty))
        End Using
    End Sub
    Protected Sub ddlLineMC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLineMC.SelectedIndexChanged
        Select Case ddlLineMC.SelectedItem.Text = "(กรุณาเลือก เครื่องจักร)"
            Case True
                pnMaterialmain.Visible = False
                PNFeed.Visible = False
                PNlbshowMATERIAL.Visible = False
                PNlbshowFeed.Visible = False
                PNlbshowMC.Visible = False
                lbshowMC.InnerText = String.Empty
            Case False
                'gvnaja.Columns.Clear()
                Panel4.Visible = True
                Panel3.Visible = True
                pnMaterialmain.Visible = True
                PNFeed.Visible = True
                PNlbshowMATERIAL.Visible = True
                PNlbshowFeed.Visible = True
                PNlbshowMC.Visible = True
                lbshowMC.InnerText = ddlLineMC.Text
                Session("numCU") = 1
                Session("PCBLM") = 1
                Call Load_ddlFeed()
                Call Load_ddlFeedC()
                'Load_ddlFeed2()
                'tnMaterial.Focus()

                Select Case gvFiles3.Rows.Count = 0
                    Case True
                        MesgBox("ชื่อ Model ไม่พบในฐานข้อมูล")
                        Exit Sub
                    Case False

                End Select
        End Select

    End Sub
    Private Sub Load_ddlFeedC()
        'Dim strMC As String = String.Empty
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        'where rn ='" & Session("num") & "'
        Dim acc As String = "select FEEDNO from (select FEEDNO, rownum as rn from (select FEEDNO from NVP_SMT_MAT where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and MODEL = '" + lbshowModel.InnerText + "' and MACHINE = '" + ddlLineMC.SelectedItem.Text + "' and PROCESS = '" & PROCESS & "' ORDER BY FEEDNO asc)) "
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            Session("num2") = dt.Rows.Count
        End Using
        A1.InnerText = Session("num2")
    End Sub
    Private Sub Load_ddlFeed()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Select Case Session("numCU") = 1
            Case False
                Dim acc As String = "select FEEDNO from (select FEEDNO, rownum as rn from (select FEEDNO from NVP_SMT_MAT where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and MODEL = '" + lbshowModel.InnerText + "' and MACHINE = '" + ddlLineMC.SelectedItem.Text + "' and PROCESS = '" & PROCESS & "' ORDER BY FEEDNO asc)) where rn ='" & Session("numCU") & "'"
                cmd.CommandText = acc
                cmd.Connection = conn
                Using sda As New OracleDataAdapter(cmd)
                    Dim dt As New DataTable()
                    sda.Fill(dt)
                    gvFiles3.DataSource = dt
                    gvFiles3.DataBind()
                End Using

                Dim strUser As String = String.Empty
                Dim connn As New OracleConnection(MT800DB)
                Dim cmdd As New OracleCommand("select MATERIAL from (select MATERIAL, rownum as rn from (select MATERIAL from NVP_SMT_MAT where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and MODEL = '" + lbshowModel.InnerText + "' and MACHINE = '" + ddlLineMC.SelectedItem.Text + "' and PROCESS = '" & PROCESS & "' ORDER BY FEEDNO asc)) where rn ='" & Session("numCU") & "'", connn)
                connn.Open()
                Dim rd As OracleDataReader = cmdd.ExecuteReader()
                Try
                    While rd.Read()
                        If IsDBNull(rd.GetValue(0)) = True Then strUser = String.Empty Else strUser = CStr(rd.GetValue(0))
                    End While
                Finally
                    rd.Close()
                End Try
                connn.Close()

                A2.InnerText = strUser
                lbshowMATERIAL.InnerText = strUser

                Dim strFeed As String = String.Empty
                Dim connnn As New OracleConnection(MT800DB)
                Dim cmddd As New OracleCommand("select FEEDNO from (select FEEDNO, rownum as rn from (select FEEDNO from NVP_SMT_MAT where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and MODEL = '" + lbshowModel.InnerText + "' and MACHINE = '" + ddlLineMC.SelectedItem.Text + "' and PROCESS = '" & PROCESS & "' ORDER BY FEEDNO asc)) where rn ='" & Session("numCU") & "'", connnn)
                connnn.Open()
                Dim rdd As OracleDataReader = cmddd.ExecuteReader()
                Try
                    While rdd.Read()
                        If IsDBNull(rdd.GetValue(0)) = True Then strFeed = String.Empty Else strFeed = CStr(rdd.GetValue(0))
                    End While
                Finally
                    rdd.Close()
                End Try
                connnn.Close()

                lbshowFeeddd.InnerText = strFeed

                'Dim strBin As String = String.Empty
                'Dim conBin As New OracleConnection(MCS)
                'Dim cmdBin As New OracleCommand("select * from IMFR_UT_ASEAN_MCS_LOT_HST@IM_LINK WHERE IMFR_UD_TRAN_CLAS_CD ='PSO' and imfr_ud_mat_part_no = '" & strUser & "' and IMFR_UD_REQ_NO = '" & lbshowLot.InnerText & "'", conBin)
                'conBin.Open()
                'Dim rdBin As OracleDataReader = cmdBin.ExecuteReader()
                'Try
                '    While rdBin.Read()
                '        If IsDBNull(rdBin.GetValue(0)) = True Then strBin = String.Empty Else strBin = CStr(rdBin.GetValue(0))
                '    End While
                'Finally
                '    rdBin.Close()
                'End Try
                'conBin.Close()


                Call Load_ddlFeedC()
            Case True
                Dim acc As String = "select FEEDNO from (select FEEDNO, rownum as rn from (select FEEDNO from NVP_SMT_MAT where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and MODEL = '" + lbshowModel.InnerText + "' and MACHINE = '" + ddlLineMC.SelectedItem.Text + "' and PROCESS = '" & PROCESS & "' ORDER BY FEEDNO asc)) where rn ='1'"
                cmd.CommandText = acc
                cmd.Connection = conn
                Using sda As New OracleDataAdapter(cmd)
                    Dim dt As New DataTable()
                    sda.Fill(dt)
                    gvFiles3.DataSource = dt
                    gvFiles3.DataBind()
                End Using

                Dim strMATERIAL As String = String.Empty
                Dim connn As New OracleConnection(MT800DB)
                Dim cmdd As New OracleCommand("select MATERIAL from (select MATERIAL, rownum as rn from (select MATERIAL from NVP_SMT_MAT where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and MODEL = '" + lbshowModel.InnerText + "' and MACHINE = '" + ddlLineMC.SelectedItem.Text + "' and PROCESS = '" & PROCESS & "' ORDER BY FEEDNO asc)) where rn ='1'", connn)
                connn.Open()
                Dim rd As OracleDataReader = cmdd.ExecuteReader()
                Try
                    While rd.Read()
                        If IsDBNull(rd.GetValue(0)) = True Then strMATERIAL = String.Empty Else strMATERIAL = CStr(rd.GetValue(0))
                    End While
                Finally
                    rd.Close()
                End Try
                connn.Close()
                A2.InnerText = strMATERIAL
                lbshowMATERIAL.InnerText = strMATERIAL
                Call Load_ddlFeedC()

                Dim strFeed As String = String.Empty
                Dim connnn As New OracleConnection(MT800DB)
                Dim cmddd As New OracleCommand("select FEEDNO from (select FEEDNO, rownum as rn from (select FEEDNO from NVP_SMT_MAT where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and MODEL = '" + lbshowModel.InnerText + "' and MACHINE = '" + ddlLineMC.SelectedItem.Text + "' and PROCESS = '" & PROCESS & "' ORDER BY FEEDNO asc)) where rn ='1'", connnn)
                connnn.Open()
                Dim rdd As OracleDataReader = cmddd.ExecuteReader()
                Try
                    While rdd.Read()
                        If IsDBNull(rdd.GetValue(0)) = True Then strFeed = String.Empty Else strFeed = CStr(rdd.GetValue(0))
                    End While
                Finally
                    rdd.Close()
                End Try
                connnn.Close()
                lbshowFeeddd.InnerText = strFeed

                'Dim strBin As String = String.Empty
                'Dim conBin As New OracleConnection(MCS)
                'Dim cmdBin As New OracleCommand("select IMFR_UD_LOC_CD from IMFR_UT_ASEAN_MCS_LOT_HST@IM_LINK WHERE IMFR_UD_TRAN_CLAS_CD ='PSO' and imfr_ud_mat_part_no = '" & A2.InnerText & "' and IMFR_UD_REQ_NO = '" & lbshowLot.InnerText & "'", conBin)
                'conBin.Open()
                'Dim rdBin As OracleDataReader = cmdBin.ExecuteReader()
                'Try
                '    While rdBin.Read()
                '        If IsDBNull(rdBin.GetValue(0)) = True Then strBin = String.Empty Else strBin = CStr(rdBin.GetValue(0))
                '    End While
                'Finally
                '    rdBin.Close()
                'End Try
                'conBin.Close()

        End Select
    End Sub
    Protected Sub tnMaterial_TextChanged(sender As Object, e As EventArgs) Handles tnMaterial.TextChanged
        'Select Case tnMaterial.Text <> String.Empty And tnMaterial.Text.Contains("|")
        '    Case True
        '        'If Not tnMaterial.Text.Contains("|") Then
        '        '    MesgBox("ท่านยิงบาร์โค้ดไม่ถูกต้อง111")
        '        'Else
        '        'End If
        '        Dim StrArr(6) As String
        '        Dim Str As String = tnMaterial.Text
        '        StrArr = Str.Split("|")
        '        StrArr(0) = StrArr(0).Replace("|", "")
        '        Session("RSQR") = StrArr(0)
        '        Select Case Session("RSQR") <> String.Empty
        '            Case True
        '                Dim StrArr1(6) As String
        '                Dim Str1 As String = tnMaterial.Text
        '                StrArr1 = Str1.Split("|")
        '                StrArr1(1) = StrArr1(1).Replace("|", "")
        '                Session("RSMat") = StrArr1(1)
        '                Select Case Session("RSMat") <> String.Empty And Session("RSMat") = lbshowMATERIAL.InnerText
        '                    Case True
        '                        Dim StrArr2(6) As String
        '                        Dim Str2 As String = tnMaterial.Text
        '                        StrArr2 = Str2.Split("|")
        '                        StrArr2(2) = StrArr2(2).Replace("|", "")
        '                        Session("RSMatLot") = StrArr2(2)
        '                        Select Case Session("RSMatLot") <> String.Empty
        '                            Case True
        '                                Dim StrArr3(6) As String
        '                                Dim Str3 As String = tnMaterial.Text
        '                                StrArr3 = Str3.Split("|")
        '                                StrArr3(3) = StrArr3(3).Replace("|", "")
        '                                Session("RSQty") = StrArr3(3)
        '                                Select Case Session("RSQty") <> String.Empty
        '                                    Case True
        '                                        Dim StrArr4(6) As String
        '                                        Dim Str4 As String = tnMaterial.Text
        '                                        StrArr4 = Str4.Split("|")
        '                                        StrArr4(4) = StrArr4(4).Replace("|", "")
        '                                        Session("RSinv") = StrArr4(4)
        '                                        Select Case Session("RSinv") <> String.Empty
        '                                            Case True
        '                                                Dim StrArr5(6) As String
        '                                                Dim Str5 As String = tnMaterial.Text
        '                                                StrArr5 = Str5.Split("|")
        '                                                StrArr5(5) = StrArr5(5).Replace("|", "")
        '                                                Session("RSReelno") = StrArr5(5)
        '                                                Select Case Session("RSReelno") <> String.Empty
        '                                                    Case True
        '                                                        Session("RSQR") = StrArr(0)
        '                                                        Session("RSMat") = StrArr1(1)
        '                                                        Session("RSMatLot") = StrArr2(2)
        '                                                        Session("RSQty") = StrArr3(3)
        '                                                        Session("RSinv") = StrArr4(4)
        '                                                        Session("RSReelno") = StrArr5(5)
        '                                                        lbshowMat.InnerText = StrArr1(1)
        '                                                        lbshowQty.InnerText = StrArr3(3)
        '                                                        lbshowinv.InnerText = StrArr4(4)
        '                                                        lbshowMatLot.InnerText = StrArr2(2)
        '                                                        PNCassetteSh.Visible = True
        '                                                        PanelMATERIALHIS.Visible = True
        '                                                    Case False
        '                                                        lbshowMat.InnerText = String.Empty
        '                                                        lbshowQty.InnerText = String.Empty
        '                                                        lbshowinv.InnerText = String.Empty
        '                                                        lbshowMatLot.InnerText = String.Empty
        '                                                        PNCassetteSh.Visible = False
        '                                                        tbCassetteSh.Text = String.Empty
        '                                                        tnMaterial.Text = String.Empty
        '                                                        PanelMATERIALHIS.Visible = False
        '                                                        MesgBox("ท่านยิงบาร์โค้ดไม่ถูกต้อง")
        '                                                End Select
        '                                            Case False
        '                                                lbshowMat.InnerText = String.Empty
        '                                                lbshowQty.InnerText = String.Empty
        '                                                lbshowinv.InnerText = String.Empty
        '                                                lbshowMatLot.InnerText = String.Empty
        '                                                PNCassetteSh.Visible = False
        '                                                tbCassetteSh.Text = String.Empty
        '                                                tnMaterial.Text = String.Empty
        '                                                MesgBox("ท่านยิงบาร์โค้ดไม่ถูกต้อง")
        '                                        End Select
        '                                    Case False
        '                                        lbshowMat.InnerText = String.Empty
        '                                        lbshowQty.InnerText = String.Empty
        '                                        lbshowinv.InnerText = String.Empty
        '                                        lbshowMatLot.InnerText = String.Empty
        '                                        PNCassetteSh.Visible = False
        '                                        tbCassetteSh.Text = String.Empty
        '                                        tnMaterial.Text = String.Empty
        '                                        MesgBox("ท่านยิงบาร์โค้ดไม่ถูกต้อง")
        '                                End Select
        '                            Case False
        '                                lbshowMat.InnerText = String.Empty
        '                                lbshowQty.InnerText = String.Empty
        '                                lbshowinv.InnerText = String.Empty
        '                                lbshowMatLot.InnerText = String.Empty
        '                                PNCassetteSh.Visible = False
        '                                tbCassetteSh.Text = String.Empty
        '                                tnMaterial.Text = String.Empty
        '                                MesgBox("ท่านยิงบาร์โค้ดไม่ถูกต้อง")
        '                        End Select
        '                    Case False
        '                        lbshowMat.InnerText = String.Empty
        '                        lbshowQty.InnerText = String.Empty
        '                        lbshowinv.InnerText = String.Empty
        '                        lbshowMatLot.InnerText = String.Empty
        '                        PNCassetteSh.Visible = False
        '                        tbCassetteSh.Text = String.Empty
        '                        tnMaterial.Text = String.Empty
        '                        MesgBox("ท่านยิงบาร์โค้ดไม่ถูกต้อง")
        '                End Select
        '            Case False
        '                lbshowMat.InnerText = String.Empty
        '                lbshowQty.InnerText = String.Empty
        '                lbshowinv.InnerText = String.Empty
        '                lbshowMatLot.InnerText = String.Empty
        '                PNCassetteSh.Visible = False
        '                tbCassetteSh.Text = String.Empty
        '                tnMaterial.Text = String.Empty
        '                MesgBox("ท่านยิงบาร์โค้ดไม่ถูกต้อง")
        '        End Select
        '    Case False
        '        tnMaterial.Text = String.Empty
        '        MesgBox("ท่านยิงบาร์โค้ดไม่ถูกต้อง")
        'End Select


        Dim Str9 As String = tnMaterial.Text
        Dim count = Str9.Split("|").Count()
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
                Session("RSReelnoCount") = StrArr(5).Count

                If IsNumeric(Session("RSQty")) Then
                    Select Case Session("RSMat") <> String.Empty And Session("RSMat") = lbshowMATERIAL.InnerText
                        Case True

                            If Session("RSReelnoCount") = 19 Or Session("RSReelnoCount") = 23 Or Session("RSReelno") = "TESTADMIN" Then
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
                                If chkPAY = True Or Session("RSReelno") = "TESTADMIN" Then


                                    Session("RSQR") = StrArr(0)
                                    Session("RSMat") = StrArr(1)
                                    Session("RSMatLot") = StrArr(2)
                                    Session("RSQty") = StrArr(3)
                                    Session("RSinv") = StrArr(4)
                                    Session("RSReelno") = StrArr(5)
                                    lbshowMat.InnerText = StrArr(1)
                                    lbshowQty.InnerText = StrArr(3)
                                    lbshowinv.InnerText = StrArr(4)
                                    lbshowMatLot.InnerText = StrArr(2)

                                    'If ddlLineMC.SelectedItem.Text = "PCB LM." Then

                                    'Else

                                    'End If
                                    'If ddlLineMC.SelectedItem.Text = "RH" Then

                                    'Else

                                    'End If
                                    'If ddlLineMC.SelectedItem.Text = "AVK" Then

                                    'Else

                                    'End If

                                    If ddlLineMC.SelectedItem.Text = "PCB LM." Then
                                        PanelMATERIALHIS.Visible = True
                                        pnVDO.Visible = True
                                        lbshowTime.InnerText = CStr(Format(Now, "HH:mm:ss"))
                                        lbshowDATE.InnerText = CStr(Format(Now, "dd-MMM-yyyy"))
                                        If gvnaja.Rows.Count = 0 Then
                                            load_gvnaja()
                                            lbshowMat.InnerText = String.Empty
                                            lbshowQty.InnerText = String.Empty
                                            lbshowinv.InnerText = String.Empty
                                            lbshowMatLot.InnerText = String.Empty
                                            tnMaterial.Text = String.Empty
                                            'tbCassetteSh.Text = String.Empty
                                            'PNCassetteSh.Visible = False
                                            PNBUT.Visible = False
                                            'tbCassetteSh.Focus()
                                        Else
                                            load_gvnaja2()
                                            'tbCassetteSh.Focus()
                                            lbshowMat.InnerText = String.Empty
                                            lbshowQty.InnerText = String.Empty
                                            lbshowinv.InnerText = String.Empty
                                            lbshowMatLot.InnerText = String.Empty
                                            tnMaterial.Text = String.Empty
                                            'tbCassetteSh.Text = String.Empty
                                            'PNCassetteSh.Visible = False
                                            PNBUT.Visible = False
                                        End If

                                        If ddlLineMC.SelectedItem.Text = "PCB LM." And Session("gvnajaCount") = 3 Then

                                            PNBUT.Visible = True
                                            pnLine.Visible = False
                                            PanelMATERIAL.Visible = False
                                            pnMaterial.Visible = False
                                        Else

                                            If ddlLineMC.SelectedItem.Text = "PCB LM." And Session("gvnajaCount") = Session("PCBLM") Then
                                                Dim i As Integer
                                                For i = Session("PCBLM") To 3
                                                    Session("PCBLM") = i + 1

                                                    'Load_ddlFeed()
                                                    Exit For
                                                Next
                                                Exit Sub
                                            Else

                                            End If
                                        End If
                                    Else
                                        PNCassetteSh.Visible = True
                                        PanelMATERIALHIS.Visible = True
                                        pnVDO.Visible = True
                                        tbCassetteSh.Focus()
                                    End If


                                Else
                                    MesgBox("Material Reelno " & Session("RSReelno") & " นี้ยังไม่ถูกตัดจ่ายในระบบ MCS")
                                    Session("HISERROR") = "Material Reelno *" & Session("RSReelno") & "* นี้ยังไม่ถูกตัดจ่ายในระบบ MCS"
                                    Session("LOGSTATUS") = "ERROR"
                                    Load_HisError()
                                    lbshowMat.InnerText = String.Empty
                                    lbshowQty.InnerText = String.Empty
                                    lbshowinv.InnerText = String.Empty
                                    lbshowMatLot.InnerText = String.Empty
                                    PNCassetteSh.Visible = False
                                    tbCassetteSh.Text = String.Empty
                                    tnMaterial.Text = String.Empty
                                    PanelMATERIALHIS.Visible = False
                                End If
                            Else
                                MesgBox("จำนวนหมายเลข Reelno " & Session("RSReelno") & " ไม่ถูกต้อง กรุณาตรวจสอบหรือแจ้งผู้ดูแลระบบ")
                                Session("HISERROR") = "จำนวนหมายเลข Reelno " & Session("RSReelno") & " ไม่ถูกต้อง กรุณาตรวจสอบหรือแจ้งผู้ดูแลระบบ"
                                Session("LOGSTATUS") = "ERROR"
                                Load_HisError()

                                lbshowMat.InnerText = String.Empty
                                lbshowQty.InnerText = String.Empty
                                lbshowinv.InnerText = String.Empty
                                lbshowMatLot.InnerText = String.Empty
                                PNCassetteSh.Visible = False
                                tbCassetteSh.Text = String.Empty
                                tnMaterial.Text = String.Empty
                                PanelMATERIALHIS.Visible = False
                            End If
                        Case False
                            'MesgBox("หมายเลข MATERIAL ไม่ถูกต้องกรุณาตรวจสอบ")
                            MesgBox("ไม่พบ MATERIAL " & Session("RSMat") & " ใน Model นี้หรือไม่ได้ยิงบาร์โค้ดตามลำดับ กรุณาตรวจสอบหรือแจ้งผู้ดูแลระบบ")
                            Session("HISERROR") = "ไม่มี MATERIAL *" & Session("RSMat") & "* ใน Model *" & tbModel.Text & "* หรือไม่ได้ยิงบาร์โค้ดตามลำดับ"
                            Session("LOGSTATUS") = "ERROR"
                            Load_HisError()
                            lbshowMat.InnerText = String.Empty
                            lbshowQty.InnerText = String.Empty
                            lbshowinv.InnerText = String.Empty
                            lbshowMatLot.InnerText = String.Empty
                            PNCassetteSh.Visible = False
                            tbCassetteSh.Text = String.Empty
                            tnMaterial.Text = String.Empty
                            PanelMATERIALHIS.Visible = False
                    End Select

                Else

                    MesgBox("จำนวน / Quantity " & Session("RSQty") & " ไม่ใช้ตัวเลข กรุณาตรวจสอบและทำการยิงลาเบลใหม่")
                    Session("HISERROR") = "บาร์โค้ดจำนวนที่ไม่ใช่ตัวเลข *" & Session("RSQty") & "* "
                    Session("LOGSTATUS") = "ERROR"
                    Load_HisError()
                    lbshowMat.InnerText = String.Empty
                    lbshowQty.InnerText = String.Empty
                    lbshowinv.InnerText = String.Empty
                    lbshowMatLot.InnerText = String.Empty
                    PNCassetteSh.Visible = False
                    tbCassetteSh.Text = String.Empty
                    tnMaterial.Text = String.Empty
                    PanelMATERIALHIS.Visible = False
                End If
            Case False

                'MesgBox("ท่านยิงบาร์โค้ดไม่ถูกต้อง")
                MesgBox("บาร์โค้ด " & tnMaterial.Text & "ไม่ถูกต้อง")
                Session("HISERROR") = "มีการแสกนบาร์โค้ดที่ไม่ถูกต้อง *" & tnMaterial.Text & "* "
                Session("LOGSTATUS") = "ERROR"
                Load_HisError()
                lbshowMat.InnerText = String.Empty
                lbshowQty.InnerText = String.Empty
                lbshowinv.InnerText = String.Empty
                lbshowMatLot.InnerText = String.Empty
                PNCassetteSh.Visible = False
                tbCassetteSh.Text = String.Empty
                tnMaterial.Text = String.Empty
                PanelMATERIALHIS.Visible = False
        End Select
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
    Protected Sub tbCassetteSh_TextChanged(sender As Object, e As EventArgs) Handles tbCassetteSh.TextChanged
        Dim strCASSETTE As String = String.Empty
        Dim conCASSETTE As New OracleConnection(MT800DB)
        Dim cmdCASSETTE As New OracleCommand("select USE from NVP_SMT_CASSETTE WHERE NO = '" & tbCassetteSh.Text & "' and PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "'", conCASSETTE)
        conCASSETTE.Open()
        Dim rdCASSETTE As OracleDataReader = cmdCASSETTE.ExecuteReader()
        Try
            While rdCASSETTE.Read()
                If IsDBNull(rdCASSETTE.GetValue(0)) = True Then strCASSETTE = String.Empty Else strCASSETTE = CStr(rdCASSETTE.GetValue(0))
            End While
        Finally
            rdCASSETTE.Close()
        End Try
        conCASSETTE.Close()
        Dim CASSETTESUM As Int64
        If strCASSETTE = String.Empty Then
        Else
            CASSETTESUM = Int(strCASSETTE) + Session("RSQty")
        End If
        Dim strMAXCASS As String = String.Empty
        Dim conMAXCASS As New OracleConnection(MT800DB)
        Dim cmdMAXCASS As New OracleCommand("select DATA from NVP_SMT_CASSETTE_MAX WHERE ITEM = 'CRITERIA' and PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "' ", conMAXCASS)
        conMAXCASS.Open()
        Dim rdMAXCASS As OracleDataReader = cmdMAXCASS.ExecuteReader()
        Try
            While rdMAXCASS.Read()
                If IsDBNull(rdMAXCASS.GetValue(0)) = True Then strMAXCASS = String.Empty Else strMAXCASS = CStr(rdMAXCASS.GetValue(0))
            End While
        Finally
            rdMAXCASS.Close()
        End Try
        conMAXCASS.Close()

        'Session("strCASSETTE") = strCASSETTE
        'Session("CASSETTESUM") = CASSETTESUM
        'Session("strMAXCASS") = strMAXCASS

        If IsNumeric(tbCassetteSh.Text) Then
            If strCASSETTE = String.Empty Then
                MesgBox("ไม่พบข้อมูล Cassette ของหมายเลข " & tbCassetteSh.Text & " กรุณาติดต่อผู้ดูแลระบบหรือเจ้าหน้าที่")
                tbCassetteSh.Text = String.Empty
            Else
                If Int(CASSETTESUM) >= Int(strMAXCASS) Then
                    MesgBox("จำนวน Cassette ของหมายเลข " & tbCassetteSh.Text & " เกินกำหนดกรุณาติดต่อ MT840")
                    tbCassetteSh.Text = String.Empty
                Else
                    Select Case tbCassetteSh.Text = String.Empty
                        Case True
                            MesgBox("กรุณากรอก Cassette")
                        Case False
                            If gvnaja.Rows.Count = 0 Then
                                Call load_gvnaja()
                                lbshowMat.InnerText = String.Empty
                                lbshowQty.InnerText = String.Empty
                                lbshowinv.InnerText = String.Empty
                                lbshowMatLot.InnerText = String.Empty
                                tnMaterial.Text = String.Empty
                                tbCassetteSh.Text = String.Empty
                                PNCassetteSh.Visible = False
                                PNBUT.Visible = False
                                tbCassetteSh.Focus()
                            Else
                                Call load_gvnaja2()
                                tbCassetteSh.Focus()
                                lbshowMat.InnerText = String.Empty
                                lbshowQty.InnerText = String.Empty
                                lbshowinv.InnerText = String.Empty
                                lbshowMatLot.InnerText = String.Empty
                                tnMaterial.Text = String.Empty
                                tbCassetteSh.Text = String.Empty
                                PNCassetteSh.Visible = False
                                PNBUT.Visible = False
                            End If
                            Select Case Session("num2") = Session("gvnajaCount")
                                Case True
                                    PNBUT.Visible = True
                                    pnLine.Visible = False
                                    PanelMATERIAL.Visible = False
                                    pnMaterial.Visible = False
                            End Select
                            Select Case Session("numCU") = Session("gvnajaCount")
                                Case True
                                    Dim i As Integer
                                    For i = Session("numCU") To Session("num2") - 1
                                        Session("numCU") = i + 1
                                        lbshowTime.InnerText = CStr(Format(Now, "HH:mm:ss"))
                                        lbshowDATE.InnerText = CStr(Format(Now, "dd-MMM-yyyy"))
                                        Call Load_ddlFeed()
                                        Exit For
                                    Next
                                    Exit Sub
                                Case False
                                    Call Load_ddlFeed()
                            End Select
                    End Select
                End If
            End If
        Else
            MesgBox("Cassette ต้องเป็นตัวเลขเท่านั้น")
            tbCassetteSh.Text = String.Empty
        End If
    End Sub
    Private Sub Button1_ServerClick(sender As Object, e As EventArgs) Handles Button1.ServerClick
        If Session("num2") = Session("gvnajaCount") Then

            Call load_GVDATACass()
            Call load_GVDATA()
            'pnLine = 1 UP------------------------------------------------------------------------------------
            'รหัสประจำตัว / User code : TB            
            tbUser.Text = String.Empty
            'โปรดักส์ / Product : PN------------------------------------------------------------------------------
            PNddlLinePD.Visible = False
            'โปรดักส์ / Product : DDL
            ddlLinePD.SelectedIndex.ToString()

            'pnLine = 1 DONW------------------------------------------------------------------------------------
            pnLine.Visible = False
            'รหัสพนักงาน / Code : LB
            lbshowCode.InnerText = String.Empty
            'ชื่อ / Name : LB
            lbshowFRONTENDLINE.InnerText = String.Empty
            'เวลา / TIME : LB
            lbshowTime.InnerText = String.Empty
            'วันที่ / Date : LB
            lbshowDATE.InnerText = String.Empty
            'โปรดักส์ / Product : LB
            lbshowFRONTENDPD.InnerText = String.Empty
            'ไลน์ / Line : PN-------------------------------------------------------------------------------------
            PNlbshowLine.Visible = False
            'ไลน์ / Line : LB
            lbshowLine.InnerText = String.Empty
            'งานรุ่น / Model : PN----------------------------------------------------------------------------------
            PNModelShow.Visible = False
            'งานรุ่น / Model : LB
            lbshowModel.InnerText = String.Empty
            'หมายเลขล็อต / LOT NO : PN-----------------------------------------------------------------------------
            PNlbshowLot.Visible = False
            'หมายเลขล็อต / LOT NO : LB
            lbshowLot.InnerText = String.Empty
            'เครื่องจักร /MACHINE : PN-----------------------------------------------------------------------------
            PNlbshowMC.Visible = False
            'เครื่องจักร /MACHINE : LB
            lbshowMC.InnerText = String.Empty
            'ฟีด / FEED : PN-----------------------------------------------------------------------------
            PNlbshowFeed.Visible = False
            'ฟีด / FEED : LB
            lbshowFeeddd.InnerText = String.Empty
            'วัสดุ / MATERIAL : PN-----------------------------------------------------------------------------
            PNlbshowMATERIAL.Visible = False
            'วัสดุ / MATERIAL : LB
            lbshowMATERIAL.InnerText = String.Empty
            'PanelMATERIAL = 2 ------------------------------------------------------------------------------------
            PanelMATERIAL.Visible = False
            'ไลน์ / Line : DDL
            ddlLine.SelectedIndex.ToString()
            'งานรุ่น / Model : PN-----------------------------------------------------------------------------
            PanetbModel.Visible = False
            'งานรุ่น / Model : TB
            tbModel.Text = String.Empty
            'PNbutton FBN: PN-----------------------------------------------------------------------------
            PNbutton.Visible = False
            'ล็อตงาน / Lot No : PN-----------------------------------------------------------------------------
            PanetbLot.Visible = False
            'ล็อตงาน / Lot No : TB
            tbLot.Text = String.Empty
            'เครื่องจักร / Machine : PN-----------------------------------------------------------------------------
            PNddlMC.Visible = False
            'เครื่องจักร / Machine : DDL
            ddlLineMC.SelectedIndex.ToString()
            'หมายเลขฟิด / Feed : PN-----------------------------------------------------------------------------
            PNFeed.Visible = False
            'หมายเลขฟิด / Feed : GV
            gvFiles3.Visible = False
            'วัสดุ / MATERIAL : PN-----------------------------------------------------------------------------
            Panel4.Visible = False
            'วัสดุ / MATERIAL : LB
            A2.InnerText = String.Empty
            'จำนวน / Quantity : PN-----------------------------------------------------------------------------
            Panel3.Visible = False
            'จำนวน / Quantity : LB
            A1.InnerText = String.Empty

            'pnMaterialmain = 3 ------------------------------------------------------------------------------------
            pnMaterialmain.Visible = False

            'กรุณายิงบาร์โค้ด  : PN-------------------------------------------------------------------------------
            pnMaterial.Visible = False
            'กรุณายิงบาร์โค้ด  : TB
            tnMaterial.Text = String.Empty
            'วัตถุดิบ / Material : LB
            lbshowMat.InnerText = String.Empty
            'จำนวน / Quantity : LB
            lbshowQty.InnerText = String.Empty
            'อินวอยซ์ / Invoice : LB
            lbshowinv.InnerText = String.Empty
            'ล็อตวัตถุ / Material Lot : LB
            lbshowMatLot.InnerText = String.Empty
            'คาสเซ็ท / Cassette : PN-------------------------------------------------------------------------------
            PNCassetteSh.Visible = False
            'คาสเซ็ท / Cassette : TB
            tbCassetteSh.Text = String.Empty
            'ยืนยัน : BT-------------------------------------------------------------------------------
            PNBUT.Visible = False

            'จำนวน / Quantity : SS
            Session("RSQty") = String.Empty
            'อินวอยซ์ / Invoice : SS
            Session("RSinv") = String.Empty
            'ล็อตวัตถุ / Material Lot : SS
            Session("RSMatLot") = String.Empty
            'RSReelno : SS
            Session("RSReelno") = String.Empty
        ElseIf ddlLineMC.SelectedItem.Text = "PCB LM." And Session("gvnajaCount") = 3 Then
            Call load_GVDATA()
            'pnLine = 1 UP------------------------------------------------------------------------------------
            'รหัสประจำตัว / User code : TB            
            tbUser.Text = String.Empty
            'โปรดักส์ / Product : PN------------------------------------------------------------------------------
            PNddlLinePD.Visible = False
            'โปรดักส์ / Product : DDL
            ddlLinePD.SelectedIndex.ToString()

            'pnLine = 1 DONW------------------------------------------------------------------------------------
            pnLine.Visible = False
            'รหัสพนักงาน / Code : LB
            lbshowCode.InnerText = String.Empty
            'ชื่อ / Name : LB
            lbshowFRONTENDLINE.InnerText = String.Empty
            'เวลา / TIME : LB
            lbshowTime.InnerText = String.Empty
            'วันที่ / Date : LB
            lbshowDATE.InnerText = String.Empty
            'โปรดักส์ / Product : LB
            lbshowFRONTENDPD.InnerText = String.Empty
            'ไลน์ / Line : PN-------------------------------------------------------------------------------------
            PNlbshowLine.Visible = False
            'ไลน์ / Line : LB
            lbshowLine.InnerText = String.Empty
            'งานรุ่น / Model : PN----------------------------------------------------------------------------------
            PNModelShow.Visible = False
            'งานรุ่น / Model : LB
            lbshowModel.InnerText = String.Empty
            'หมายเลขล็อต / LOT NO : PN-----------------------------------------------------------------------------
            PNlbshowLot.Visible = False
            'หมายเลขล็อต / LOT NO : LB
            lbshowLot.InnerText = String.Empty
            'เครื่องจักร /MACHINE : PN-----------------------------------------------------------------------------
            PNlbshowMC.Visible = False
            'เครื่องจักร /MACHINE : LB
            lbshowMC.InnerText = String.Empty
            'ฟีด / FEED : PN-----------------------------------------------------------------------------
            PNlbshowFeed.Visible = False
            'ฟีด / FEED : LB
            lbshowFeeddd.InnerText = String.Empty
            'วัสดุ / MATERIAL : PN-----------------------------------------------------------------------------
            PNlbshowMATERIAL.Visible = False
            'วัสดุ / MATERIAL : LB
            lbshowMATERIAL.InnerText = String.Empty
            'PanelMATERIAL = 2 ------------------------------------------------------------------------------------
            PanelMATERIAL.Visible = False
            'ไลน์ / Line : DDL
            ddlLine.SelectedIndex.ToString()
            'งานรุ่น / Model : PN-----------------------------------------------------------------------------
            PanetbModel.Visible = False
            'งานรุ่น / Model : TB
            tbModel.Text = String.Empty
            'PNbutton FBN: PN-----------------------------------------------------------------------------
            PNbutton.Visible = False
            'ล็อตงาน / Lot No : PN-----------------------------------------------------------------------------
            PanetbLot.Visible = False
            'ล็อตงาน / Lot No : TB
            tbLot.Text = String.Empty
            'เครื่องจักร / Machine : PN-----------------------------------------------------------------------------
            PNddlMC.Visible = False
            'เครื่องจักร / Machine : DDL
            ddlLineMC.SelectedIndex.ToString()
            'หมายเลขฟิด / Feed : PN-----------------------------------------------------------------------------
            PNFeed.Visible = False
            'หมายเลขฟิด / Feed : GV
            gvFiles3.Visible = False
            'วัสดุ / MATERIAL : PN-----------------------------------------------------------------------------
            Panel4.Visible = False
            'วัสดุ / MATERIAL : LB
            A2.InnerText = String.Empty
            'จำนวน / Quantity : PN-----------------------------------------------------------------------------
            Panel3.Visible = False
            'จำนวน / Quantity : LB
            A1.InnerText = String.Empty

            'pnMaterialmain = 3 ------------------------------------------------------------------------------------
            pnMaterialmain.Visible = False

            'กรุณายิงบาร์โค้ด  : PN-------------------------------------------------------------------------------
            pnMaterial.Visible = False
            'กรุณายิงบาร์โค้ด  : TB
            tnMaterial.Text = String.Empty
            'วัตถุดิบ / Material : LB
            lbshowMat.InnerText = String.Empty
            'จำนวน / Quantity : LB
            lbshowQty.InnerText = String.Empty
            'อินวอยซ์ / Invoice : LB
            lbshowinv.InnerText = String.Empty
            'ล็อตวัตถุ / Material Lot : LB
            lbshowMatLot.InnerText = String.Empty
            'คาสเซ็ท / Cassette : PN-------------------------------------------------------------------------------
            PNCassetteSh.Visible = False
            'คาสเซ็ท / Cassette : TB
            tbCassetteSh.Text = String.Empty
            'ยืนยัน : BT-------------------------------------------------------------------------------
            PNBUT.Visible = False

            'จำนวน / Quantity : SS
            Session("RSQty") = String.Empty
            'อินวอยซ์ / Invoice : SS
            Session("RSinv") = String.Empty
            'ล็อตวัตถุ / Material Lot : SS
            Session("RSMatLot") = String.Empty
            'RSReelno : SS
            Session("RSReelno") = String.Empty
        Else
            MesgBox("ลงข้อมูลไม่ครบกรุณาลงข้อมูลใหม่และแจ้งผู้ดูแลระบบ")
        End If

        'Select Case ddlLineMC.SelectedItem.Text = "PCB LM." And Session("gvnajaCount") = 3
        '    Case True

        '    Case False
        '        MesgBox("ลงข้อมูลไม่ครบกรุณาลงข้อมูลใหม่และแจ้งผู้ดูแลระบบ")
        'End Select

    End Sub
    Private Sub load_gvnaja()
        Dim dtResult As New DataTable()
        dtResult.Columns.Add("Name", GetType(String))
        dtResult.Columns.Add("Date", GetType(String))
        dtResult.Columns.Add("Time", GetType(String))
        dtResult.Columns.Add("Model", GetType(String))
        dtResult.Columns.Add("LotNo", GetType(String))
        dtResult.Columns.Add("Material", GetType(String))
        dtResult.Columns.Add("Invoice", GetType(String))
        dtResult.Columns.Add("MaterialLot", GetType(String))
        dtResult.Columns.Add("Qty", GetType(String))
        dtResult.Columns.Add("ReelNo", GetType(String))
        dtResult.Columns.Add("Line", GetType(String))
        dtResult.Columns.Add("Product", GetType(String))
        dtResult.Columns.Add("Mode1", GetType(String))
        dtResult.Columns.Add("Machine", GetType(String))
        dtResult.Columns.Add("Feed", GetType(String))
        dtResult.Columns.Add("Cassette", GetType(String))
        dtResult.Columns.Add("PROCESS", GetType(String))
        dtResult.Columns.Add("USERID", GetType(String))

        Dim newRow As DataRow = dtResult.NewRow
        newRow("Name") = lbshowFRONTENDLINE.InnerText
        newRow("Date") = lbshowDATE.InnerText
        newRow("Time") = lbshowTime.InnerText
        newRow("Model") = lbshowModel.InnerText
        newRow("LotNo") = lbshowLot.InnerText
        newRow("Material") = lbshowMATERIAL.InnerText
        newRow("Invoice") = Session("RSinv")
        newRow("MaterialLot") = Session("RSMatLot")
        newRow("Qty") = Session("RSQty")
        newRow("ReelNo") = Session("RSReelno")
        newRow("Line") = lbshowLine.InnerText
        newRow("Product") = lbshowFRONTENDPD.InnerText
        newRow("Mode1") = "SetupModel"
        newRow("Machine") = lbshowMC.InnerText
        newRow("Feed") = lbshowFeeddd.InnerText
        If ddlLineMC.SelectedItem.Text = "PCB LM." Then
            newRow("Cassette") = ""
        Else
            newRow("Cassette") = tbCassetteSh.Text
        End If
        newRow("PROCESS") = PROCESS
        newRow("USERID") = lbshowCode.InnerText
        dtResult.Rows.Add(newRow)
        gvnaja.DataSource = dtResult
        Session("gvnajaCount") = dtResult.Rows.Count
        gvnaja.DataBind()
        ViewState("MyDataTable") = dtResult
    End Sub
    Private Sub load_checkcassgvnaja()

    End Sub
    Private Sub load_clgvnaja()
        Dim dtResult As New DataTable()
        dtResult.Columns.Add("Name", GetType(String))
        dtResult.Columns.Add("Date", GetType(String))
        dtResult.Columns.Add("Time", GetType(String))
        dtResult.Columns.Add("Model", GetType(String))
        dtResult.Columns.Add("LotNo", GetType(String))
        dtResult.Columns.Add("Material", GetType(String))
        dtResult.Columns.Add("Invoice", GetType(String))
        dtResult.Columns.Add("MaterialLot", GetType(String))
        dtResult.Columns.Add("Qty", GetType(String))
        dtResult.Columns.Add("ReelNo", GetType(String))
        dtResult.Columns.Add("Line", GetType(String))
        dtResult.Columns.Add("Product", GetType(String))
        dtResult.Columns.Add("Mode1", GetType(String))
        dtResult.Columns.Add("Machine", GetType(String))
        dtResult.Columns.Add("Feed", GetType(String))
        dtResult.Columns.Add("Cassette", GetType(String))
        dtResult.Columns.Add("PROCESS", GetType(String))
        dtResult.Columns.Add("USERID", GetType(String))
        gvnaja.DataSource = dtResult

        gvnaja.DataBind()
    End Sub
    Private Sub load_gvnaja2()
        Dim dt As DataTable = CType(ViewState("MyDataTable"), DataTable)
        Dim newRow As DataRow = dt.NewRow()
        newRow("Name") = lbshowFRONTENDLINE.InnerText
        newRow("Date") = lbshowDATE.InnerText
        newRow("Time") = lbshowTime.InnerText
        newRow("Model") = lbshowModel.InnerText
        newRow("LotNo") = lbshowLot.InnerText
        newRow("Material") = lbshowMATERIAL.InnerText
        newRow("Invoice") = Session("RSinv")
        newRow("MaterialLot") = Session("RSMatLot")
        newRow("Qty") = Session("RSQty")
        newRow("ReelNo") = Session("RSReelno")
        newRow("Line") = lbshowLine.InnerText
        newRow("Product") = lbshowFRONTENDPD.InnerText
        newRow("Mode1") = "SetupModel"
        newRow("Machine") = lbshowMC.InnerText
        newRow("Feed") = lbshowFeeddd.InnerText
        If ddlLineMC.SelectedItem.Text = "PCB LM." Then
            newRow("Cassette") = ""
        Else
            newRow("Cassette") = tbCassetteSh.Text
        End If
        newRow("PROCESS") = PROCESS
        newRow("USERID") = lbshowCode.InnerText
        dt.Rows.Add(newRow)
        gvnaja.DataSource = dt
        Session("gvnajaCount") = dt.Rows.Count
        gvnaja.DataBind()
    End Sub
    Private Sub load_GVDATA()
        Dim insideData As DataTable = CType(ViewState("MyDataTable"), DataTable)
        Dim connectionString As String = MT800DB
        Using connection As New OracleConnection(connectionString)
            connection.Open()
            For Each row As DataRow In insideData.Rows
                Dim sql As String = "INSERT INTO NVP_ALLPROCESS_HIS (USERNAME,DAY,TIME,MODEL,LOTNO,MATERIAL,INVOICE,MATERIALLOT,QTY,REELNO,LINE,PRODUCTS,MODE1,MACHINE,FEED,CASSETTE,PROCESS,USERID) VALUES (:sUSERNAME,:sDAY,:sTIME,:sMODEL,:sLOTNO,:sMATERIAL,:sINVOICE,:sMATERIALLOT,:sQTY,:sREELNO,:sLINE,:sPRODUCTS,:sMODE1,:sMACHINE,:sFEED,:sCASSETTE,:sPROCESS,:sUSERID)"
                Using command As New OracleCommand(sql, connection)
                    command.Parameters.Add(":sUSERNAME", row("Name"))
                    command.Parameters.Add(":sDAY", row("Date"))
                    command.Parameters.Add(":sTIME", row("Time"))
                    command.Parameters.Add(":sMODEL", row("Model"))
                    command.Parameters.Add(":sLOTNO", row("LotNo"))
                    command.Parameters.Add(":sMATERIAL", row("Material"))
                    command.Parameters.Add(":sINVOICE", row("Invoice"))
                    command.Parameters.Add(":sMATERIALLOT", row("MaterialLot"))
                    command.Parameters.Add(":sQTY", row("Qty"))
                    command.Parameters.Add(":sREELNO", row("ReelNo"))
                    command.Parameters.Add(":sLINE", row("Line"))
                    command.Parameters.Add(":sPRODUCTS", row("Product"))
                    command.Parameters.Add(":sMODE1", row("Mode1"))
                    command.Parameters.Add(":sMACHINE", row("Machine"))
                    command.Parameters.Add(":sFEED", row("Feed"))
                    If ddlLineMC.SelectedItem.Text = "PCB LM." Then

                        command.Parameters.Add(":sCASSETTE", "")

                    Else
                        command.Parameters.Add(":sCASSETTE", row("Cassette"))
                    End If
                    command.Parameters.Add(":sPROCESS", row("PROCESS"))
                    command.Parameters.Add(":sUSERID", row("USERID"))
                    command.ExecuteNonQuery()
                End Using
            Next
            connection.Close()
        End Using
    End Sub
    Private Sub load_GVDATACass()
        Dim insideData As DataTable = CType(ViewState("MyDataTable"), DataTable)
        Dim connectionString As String = MT800DB
        Using connection As New OracleConnection(connectionString)
            connection.Open()
            For Each row As DataRow In insideData.Rows
                Dim sql As String = "UPDATE NVP_SMT_CASSETTE SET USE = USE + :sQty WHERE NO = :sCassette and PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "'"
                Using command As New OracleCommand(sql, connection)
                    command.Parameters.Add(":sQty", row("Qty"))
                    command.Parameters.Add(":sCassette", row("Cassette"))
                    command.ExecuteNonQuery()
                End Using
            Next
            connection.Close()
        End Using
    End Sub
    Private Sub load_GVDATAMatel()
        Dim insideData As DataTable = CType(ViewState("MyDataTable"), DataTable)
        Dim connectionString As String = MT800DB
        Using connection As New OracleConnection(connectionString)
            connection.Open()
            For Each row As DataRow In insideData.Rows
                Dim sql As String = "UPDATE NVP_SMT_CASSETTE SET USE = USE + :sQty WHERE NO = :sCassette and PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "'"
                Using command As New OracleCommand(sql, connection)
                    command.Parameters.Add(":sQty", row("Qty"))
                    command.Parameters.Add(":sCassette", row("Cassette"))
                    command.ExecuteNonQuery()
                End Using
            Next
            connection.Close()
        End Using
    End Sub

    'Private Sub Load_ddlFeed2()
    '    Dim conn As New OracleConnection(MT800DB)
    '    Dim cmd As New OracleCommand()
    '    Dim acc As String = "select MATERIALBAR_SMT_MATERIAL.MATERIAL,MATERIALBAR_SMT_MATERIAL.FEEDNO,MATERIALBAR_SMT_BIN.BINNO"
    '    acc += " from MATERIALBAR_SMT_MATERIAL INNER JOIN MATERIALBAR_SMT_BIN "
    '    acc += " ON MATERIALBAR_SMT_BIN.MATERIAL = MATERIALBAR_SMT_MATERIAL.MATERIAL"
    '    acc += " where MATERIALBAR_SMT_MATERIAL.PRODUCT = '" & ddlLinePD.SelectedItem.Text & "'"
    '    acc += " and MATERIALBAR_SMT_MATERIAL.MODEL = '" + lbshowModel.InnerText + "'"
    '    acc += " and MATERIALBAR_SMT_MATERIAL.MACHINE = '" + ddlLineMC.SelectedItem.Text + "'"
    '    acc += " ORDER BY MATERIALBAR_SMT_MATERIAL.FEEDNO asc"
    '    cmd.CommandText = acc
    '    cmd.Connection = conn
    '    Using sda As New OracleDataAdapter(cmd)
    '        conn.Open()
    '        GridView1.DataSource = cmd.ExecuteReader()
    '        GridView1.DataBind()
    '        conn.Close()
    '    End Using
    'End Sub

    'Private Sub chkPayMat()
    '    Dim conn As New OracleConnection(MCS)
    '    Dim cmd As New OracleCommand()
    '    Dim acc As String = "select T1.IMFR_SD_LABEL_ID , T2.PAYOUT from imfr_UT_ASEAN_MCS_LOT_FILE@IM_LINK T1 "
    '    acc += "left join (select IMFR_UD_LABEL_ID,'ALREADY PAYOUT' as PAYOUT "
    '    acc += "from imfr_UT_ASEAN_MCS_LOT_HST@IM_LINK where IMFR_UD_TRAN_CLAS_CD = 'PSO') "
    '    acc += "T2 on T1.IMFR_SD_LABEL_ID = T2.IMFR_UD_LABEL_ID where T1.IMFR_UD_AVAIL_QTY + T1.IMFR_UD_HLD_QTY + "
    '    acc += "T1.IMFR_UD_ALLO_QTY = 0"
    '    cmd.CommandText = acc
    '    cmd.Connection = conn
    '    Using sda As New OracleDataAdapter(cmd)
    '        Dim dt As New DataTable()
    '        sda.Fill(dt)
    '        gvFiles999.DataSource = dt
    '        gvFiles999.DataBind()
    '    End Using
    'End Sub

End Class
