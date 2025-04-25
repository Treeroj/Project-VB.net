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
Partial Class SMTMBChangeMat
    Inherits System.Web.UI.Page
    Dim PROCESS As String = "SMT"
    Dim MT800DB As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Page.Form.Attributes.Add("enctype", "multipart/form-data")


        End If
        'If TextBox1.Text = String.Empty Then
        '    TextBox1.Focus()
        'End If
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
                PNddlLinePD.Visible = True
                ''PanelMATERIALHIS.Visible = False
                'load_clgvnaja()
                'PanelMATERIALHIS.Visible = False
                Load_ddlLinePD()
                load_clGVHIS()
                PanelMATERIALHIS.Visible = False
            Case False
                'load_clgvnaja()
                'PanelMATERIALHIS.Visible = False
                tbUser.Text = String.Empty
                MesgBox("รหัสพนักงานไม่ถูกต้อง")
        End Select
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
                PanelMATERIAL.Visible = False

            Case False
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
                PanetbModel.Visible = False
                PNlbshowLine.Visible = False
                PNddlMC.Visible = False
                PanetbLot.Visible = False
            Case False
                PNddlMC.Visible = True
                PanetbLot.Visible = True
                lbshowLine.InnerText = ddlLine.Text
                PNlbshowLine.Visible = True
                PanetbModel.Visible = True
                Load_GVMODEL()
                Load_GVLOT()
                Load_DDLMC()
                If lbshowModel.InnerText = String.Empty And lbshowLot.InnerText = String.Empty Then
                    PanetbModel.Visible = False
                    PNlbshowLine.Visible = False
                    PNddlMC.Visible = False
                    PanetbLot.Visible = False
                    lbshowModel.InnerText = String.Empty
                    lbshowLot.InnerText = String.Empty
                End If
        End Select
    End Sub

    Private Sub Load_GVMODEL()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select MODEL from (select MODEL, rownum as rn from (select MODEL from NVP_ALLPROCESS_HIS where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and LINE = '" & ddlLine.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "' ORDER BY TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME DESC)) where rn ='1'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            GridView1.DataSource = dt
            GridView1.DataBind()
        End Using

        Dim strUser As String = String.Empty
        Dim connn As New OracleConnection(MT800DB)
        Dim cmdd As New OracleCommand("select MODEL from (select MODEL, rownum as rn from (select MODEL from NVP_ALLPROCESS_HIS where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and LINE = '" & ddlLine.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "' ORDER BY TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME DESC)) where rn ='1'", connn)
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

        Session("Load_GVMODEL") = strUser
        lbshowModel.InnerText = Session("Load_GVMODEL")
        PNModelShow.Visible = True
    End Sub
    Private Sub Load_GVLOT()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select LOTNO from (select LOTNO, rownum as rn from (select LOTNO from NVP_ALLPROCESS_HIS where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and LINE = '" & ddlLine.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "' ORDER BY TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME DESC)) where rn ='1'"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            GridView2.DataSource = dt
            GridView2.DataBind()
        End Using

        Dim strUser As String = String.Empty
        Dim connn As New OracleConnection(MT800DB)
        Dim cmdd As New OracleCommand("select LOTNO from (select LOTNO, rownum as rn from (select LOTNO from NVP_ALLPROCESS_HIS where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and LINE = '" & ddlLine.SelectedItem.Text & "' and PROCESS = '" & PROCESS & "' ORDER BY TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME DESC)) where rn ='1'", connn)
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

        Session("Load_GVLOT") = strUser
        lbshowLot.InnerText = Session("Load_GVLOT")
        PNlbshowLot.Visible = True
    End Sub
    Private Sub Load_DDLMC()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select DISTINCT MACHINE From NVP_ALLPROCESS_HIS where MODEL = '" + Session("Load_GVMODEL") + "' and LOTNO = '" + Session("Load_GVLOT") + "' and PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "'  and PROCESS = '" & PROCESS & "'"
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
    Protected Sub ddlLineMC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLineMC.SelectedIndexChanged
        Select Case ddlLineMC.SelectedIndex = 0
            Case True
                lbshowMC.InnerText = String.Empty
                PNlbshowMC.Visible = False
                PNCounter.Visible = False
            Case False
                lbshowMC.InnerText = ddlLineMC.Text
                PNlbshowMC.Visible = True
                PNCounter.Visible = True
        End Select
    End Sub
    Protected Sub TBCounter_TextChanged(sender As Object, e As EventArgs) Handles TBCounter.TextChanged
        Select Case TBCounter.Text = String.Empty
            Case True
                PNFeed.Visible = False

            Case False
                PNFeed.Visible = True
                Load_DDLFeed()
        End Select
    End Sub
    Protected Sub DDLFeed_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLFeed.SelectedIndexChanged
        Select Case ddlLineMC.SelectedIndex = 0
            Case True

            Case False

                Dim strFEED As String = String.Empty
                Dim strREELNO As String = String.Empty
                Dim strMATERIAL As String = String.Empty
                Dim strINVOICE As String = String.Empty
                Dim strMATERIALLOT As String = String.Empty
                Dim conn As New OracleConnection(MT800DB)
                Dim cmd As New OracleCommand("select FEED,REELNO,MATERIAL,INVOICE,MATERIALLOT from (select FEED,REELNO,MATERIAL,INVOICE,MATERIALLOT ,rownum as rn from (select FEED,REELNO,MATERIAL,INVOICE,MATERIALLOT from NVP_ALLPROCESS_HIS where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and LINE = '" & ddlLine.SelectedItem.Text & "' and MACHINE = '" & ddlLineMC.SelectedItem.Text & "' and FEED = '" & DDLFeed.SelectedItem.Text & "' ORDER BY TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME DESC)) where rn ='1'", conn)
                conn.Open()

                Dim rd As OracleDataReader = cmd.ExecuteReader()
                Try
                    While rd.Read()

                        If IsDBNull(rd.GetValue(0)) = True Then strFEED = String.Empty Else strFEED = CStr(rd.GetValue(0))

                        If IsDBNull(rd.GetValue(1)) = True Then strREELNO = String.Empty Else strREELNO = CStr(rd.GetValue(1))

                        If IsDBNull(rd.GetValue(2)) = True Then strMATERIAL = String.Empty Else strMATERIAL = CStr(rd.GetValue(2))

                        If IsDBNull(rd.GetValue(3)) = True Then strINVOICE = String.Empty Else strINVOICE = CStr(rd.GetValue(3))

                        If IsDBNull(rd.GetValue(4)) = True Then strMATERIALLOT = String.Empty Else strMATERIALLOT = CStr(rd.GetValue(4))

                    End While
                Finally
                    rd.Close()
                End Try
                conn.Close()

                Session("Load_FEED") = strFEED
                Session("Load_strMATERIALLOT") = strMATERIALLOT
                Session("Load_strREELNO") = strREELNO
                Session("Load_strMATERIAL") = strMATERIAL
                Session("Load_strINVOICE") = strINVOICE
                Select Case DDLFeed.SelectedItem.Text = strFEED And DDLFeed.SelectedItem.Text <> String.Empty
                    Case True
                        Load_LBMAT()
                        PNSHOWFEED.Visible = True
                        PNlbshowFeed.Visible = True
                        lbshowFeeddd.InnerText = Session("Load_FEED")
                        PNlbshowMATERIAL.Visible = True
                        PNMAINMAT.Visible = True
                    Case False
                        PNMAINMAT.Visible = False
                        PNSHOWFEED.Visible = False
                        'TBFeed.Text = String.Empty
                        PNlbshowFeed.Visible = False
                        lbshowFeeddd.InnerText = String.Empty
                        lbshowFeeddd1.InnerText = String.Empty
                        lbshowMATERIAL.InnerText = String.Empty
                        MesgBox("ไม่พบข้อมูล")
                End Select
        End Select
    End Sub
    Private Sub Load_DDLFeed()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = " select FEED from (select FEED from (select FEED from NVP_ALLPROCESS_HIS where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and LINE = '" & ddlLine.SelectedItem.Text & "' and MACHINE = '" & ddlLineMC.SelectedItem.Text & "' ORDER BY TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME asc))"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()
            sda.Fill(dt)
            DDLFeed.DataSource = dt
            DDLFeed.DataTextField = "FEED"
            DDLFeed.DataValueField = "FEED"
            DDLFeed.DataBind()
            DDLFeed.Items.Insert(0, New ListItem("(กรุณาเลือก เครื่องจักร)", String.Empty))
        End Using
    End Sub


    'Protected Sub TBFeed_TextChanged(sender As Object, e As EventArgs) Handles TBFeed.TextChanged

    '    Dim strFEED As String = String.Empty
    '    Dim strREELNO As String = String.Empty
    '    Dim strMATERIAL As String = String.Empty
    '    Dim strINVOICE As String = String.Empty
    '    Dim strMATERIALLOT As String = String.Empty
    '    Dim conn As New OracleConnection(MT800DB)
    '    Dim cmd As New OracleCommand("select FEED,REELNO,MATERIAL,INVOICE,MATERIALLOT from (select FEED,REELNO,MATERIAL,INVOICE,MATERIALLOT ,rownum as rn from (select FEED,REELNO,MATERIAL,INVOICE,MATERIALLOT from NVP_ALLPROCESS_HIS where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and LINE = '" & ddlLine.SelectedItem.Text & "' and MACHINE = '" & ddlLineMC.SelectedItem.Text & "' and FEED = '" & TBFeed.Text & "' ORDER BY TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME DESC)) where rn ='1'", conn)
    '    conn.Open()

    '    Dim rd As OracleDataReader = cmd.ExecuteReader()
    '    Try
    '        While rd.Read()

    '            If IsDBNull(rd.GetValue(0)) = True Then strFEED = String.Empty Else strFEED = CStr(rd.GetValue(0))

    '            If IsDBNull(rd.GetValue(1)) = True Then strREELNO = String.Empty Else strREELNO = CStr(rd.GetValue(1))

    '            If IsDBNull(rd.GetValue(2)) = True Then strMATERIAL = String.Empty Else strMATERIAL = CStr(rd.GetValue(2))

    '            If IsDBNull(rd.GetValue(3)) = True Then strINVOICE = String.Empty Else strINVOICE = CStr(rd.GetValue(3))

    '            If IsDBNull(rd.GetValue(4)) = True Then strMATERIALLOT = String.Empty Else strMATERIALLOT = CStr(rd.GetValue(4))

    '        End While
    '    Finally
    '        rd.Close()
    '    End Try
    '    conn.Close()

    '    Session("Load_FEED") = strFEED
    '    Session("Load_strMATERIALLOT") = strMATERIALLOT
    '    Session("Load_strREELNO") = strREELNO
    '    Session("Load_strMATERIAL") = strMATERIAL
    '    Session("Load_strINVOICE") = strINVOICE
    '    Select Case TBFeed.Text = strFEED And TBFeed.Text <> String.Empty
    '        Case True
    '            Load_LBMAT()
    '            PNSHOWFEED.Visible = True
    '            PNlbshowFeed.Visible = True
    '            lbshowFeeddd.InnerText = Session("Load_FEED")
    '            PNlbshowMATERIAL.Visible = True
    '            PNMAINMAT.Visible = True
    '        Case False
    '            PNMAINMAT.Visible = False
    '            PNSHOWFEED.Visible = False
    '            TBFeed.Text = String.Empty
    '            PNlbshowFeed.Visible = False
    '            lbshowFeeddd.InnerText = String.Empty
    '            lbshowFeeddd1.InnerText = String.Empty
    '            lbshowMATERIAL.InnerText = String.Empty
    '            MesgBox("ไม่พบข้อมูล")
    '    End Select
    'End Sub
    Private Sub Load_LBMAT()
        Dim strFeed As String = String.Empty
        Dim connnn As New OracleConnection(MT800DB)
        Dim cmddd As New OracleCommand("select MATERIAL from NVP_ALLPROCESS_HIS where PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "' and LINE = '" & ddlLine.SelectedItem.Text & "' and MACHINE = '" & ddlLineMC.SelectedItem.Text & "' and FEED = '" & Session("Load_FEED") & "'", connnn)
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

        lbshowFeeddd1.InnerText = strFeed
        lbshowMATERIAL.InnerText = strFeed
    End Sub
    Protected Sub tnMaterial_TextChanged(sender As Object, e As EventArgs) Handles tnMaterial.TextChanged
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
                Select Case Session("RSMat") <> String.Empty And Session("RSMat") = Session("Load_strMATERIAL")
                    Case True
                        If Session("Load_strINVOICE") = Session("RSinv") And Session("RSinv") <> String.Empty Then
                            If Session("Load_strMATERIALLOT") = Session("RSMatLot") And Session("RSMatLot") <> String.Empty Then
                                If Session("Load_strREELNO") = Session("RSReelno") And Session("RSReelno") <> String.Empty Then
                                    lbshowMat.InnerText = StrArr(1)
                                    lbshowQty.InnerText = StrArr(3)
                                    lbshowinv.InnerText = StrArr(4)
                                    lbshowMatLot.InnerText = StrArr(2)
                                    'PanelMATERIALHIS.Visible = True
                                    pnMaterialNEW.Visible = True
                                Else
                                    MesgBox("หมายเลข REELNO ไม่ถูกต้องกรุณาตรวจสอบ")
                                    lbshowMat.InnerText = String.Empty
                                    lbshowQty.InnerText = String.Empty
                                    lbshowinv.InnerText = String.Empty
                                    lbshowMatLot.InnerText = String.Empty
                                    tnMaterial.Text = String.Empty
                                    pnMaterialNEW.Visible = False
                                End If
                            Else
                                MesgBox("หมายเลข MATERIALLOT ไม่ถูกต้องกรุณาตรวจสอบ")
                                lbshowMat.InnerText = String.Empty
                                lbshowQty.InnerText = String.Empty
                                lbshowinv.InnerText = String.Empty
                                lbshowMatLot.InnerText = String.Empty
                                tnMaterial.Text = String.Empty
                                pnMaterialNEW.Visible = False
                            End If
                        Else
                            MesgBox("หมายเลข INVOICE ไม่ถูกต้องกรุณาตรวจสอบ")
                            lbshowMat.InnerText = String.Empty
                            lbshowQty.InnerText = String.Empty
                            lbshowinv.InnerText = String.Empty
                            lbshowMatLot.InnerText = String.Empty
                            tnMaterial.Text = String.Empty
                            pnMaterialNEW.Visible = False
                        End If
                    Case False
                        MesgBox("หมายเลข MATERIAL ไม่ถูกต้องกรุณาตรวจสอบ")
                        lbshowMat.InnerText = String.Empty
                        lbshowQty.InnerText = String.Empty
                        lbshowinv.InnerText = String.Empty
                        lbshowMatLot.InnerText = String.Empty
                        tnMaterial.Text = String.Empty
                        pnMaterialNEW.Visible = False
                        'PanelMATERIALHIS.Visible = False
                End Select
            Case False
                lbshowMat.InnerText = String.Empty
                lbshowQty.InnerText = String.Empty
                lbshowinv.InnerText = String.Empty
                lbshowMatLot.InnerText = String.Empty
                tnMaterial.Text = String.Empty
                tnMaterial.Text = String.Empty
                'PanelMATERIALHIS.Visible = False
                MesgBox("ท่านยิงบาร์โค้ดไม่ถูกต้อง")
        End Select
    End Sub
    Protected Sub cle_tnMaterialNEW()
        lbshowMatNEW.InnerText = String.Empty
        lbshowQtyNEW.InnerText = String.Empty
        lbshowinvNEW.InnerText = String.Empty
        lbshowMatLotNEW.InnerText = String.Empty
        tnMaterialNEW.Text = String.Empty
        PNCassetteSh.Visible = False
    End Sub
    Protected Sub Load_logError()

    End Sub

    Protected Sub tnMaterialNEW_TextChanged(sender As Object, e As EventArgs) Handles tnMaterialNEW.TextChanged
        Dim Str9NEW As String = tnMaterialNEW.Text
        Dim countNEW = Str9NEW.Split("|").Count()
        Select Case countNEW = 6
            Case True
                Dim StrArrNEW(5) As String
                Dim Str5NEW As String = tnMaterialNEW.Text
                StrArrNEW = Str5NEW.Split("|")
                StrArrNEW(0) = StrArrNEW(0).Replace("|", "")
                StrArrNEW(1) = StrArrNEW(1).Replace("|", "")
                StrArrNEW(2) = StrArrNEW(2).Replace("|", "")
                StrArrNEW(3) = StrArrNEW(3).Replace("|", "")
                StrArrNEW(4) = StrArrNEW(4).Replace("|", "")
                StrArrNEW(5) = StrArrNEW(5).Replace("|", "")
                Session("RSQRNEW") = StrArrNEW(0)
                Session("RSMatNEW") = StrArrNEW(1)
                Session("RSMatLotNEW") = StrArrNEW(2)
                Session("RSQtyNEW") = StrArrNEW(3)
                Session("RSinvNEW") = StrArrNEW(4)
                Session("RSReelnoNEW") = StrArrNEW(5)
                Select Case Session("RSMatNEW") <> String.Empty And Session("RSMatNEW") = Session("Load_strMATERIAL")
                    Case True
                        Select Case Session("RSMatLot") <> Session("RSMatLotNEW") Or ddlLineMC.SelectedItem.Text = "RH" Or ddlLineMC.SelectedItem.Text = "AVK"
                            Case True
                                Select Case Session("RSReelno") <> Session("RSReelnoNEW")
                                    Case True
                                        'Load_GVHIS()
                                        lbshowMatNEW.InnerText = StrArrNEW(1)
                                        lbshowQtyNEW.InnerText = StrArrNEW(3)
                                        lbshowinvNEW.InnerText = StrArrNEW(4)
                                        lbshowMatLotNEW.InnerText = StrArrNEW(2)
                                        PNCassetteSh.Visible = True
                                    Case False
                                        cle_tnMaterialNEW()
                                        MesgBox("Reel No ซ้ำกันโปรดตรวจสอบ")
                                End Select
                            Case False
                                cle_tnMaterialNEW()
                                MesgBox("Material Lot ซ้ำกันโปรดตรวจสอบ")
                        End Select
                    Case False
                        cle_tnMaterialNEW()
                        MesgBox("หมายเลข MATERIAL ไม่ถูกต้องกรุณาตรวจสอบ")
                End Select
            Case False
                cle_tnMaterialNEW()
                MesgBox("ท่านยิงบาร์โค้ดไม่ถูกต้อง")
        End Select
    End Sub
    Private Sub Load_GVHIS()
        Dim conn As New OracleConnection(MT800DB)
        Dim cmd As New OracleCommand()
        Dim acc As String = "select LINE,PRODUCTS,MACHINE,FEED,MODEL,LOTNO"
        acc += " from NVP_ALLPROCESS_HIS"
        acc += " where MODEL = '" + Session("Load_GVMODEL") + "' and LOTNO = '" + Session("Load_GVLOT") + "' and MACHINE='" + ddlLineMC.SelectedItem.Text + "' and FEED = '" & Session("Load_FEED") & "' and PRODUCTS = '" & ddlLinePD.SelectedItem.Text & "'"
        acc += " ORDER BY FEED asc, TO_DATE(DAY,'DD.MM.YYYY') DESC ,TIME DESC"
        cmd.CommandText = acc
        cmd.Connection = conn
        Using sda As New OracleDataAdapter(cmd)
            Dim dt As New DataTable()

            Dim dataColumnNAME As DataColumn = New DataColumn("Name", GetType(String))
            dataColumnNAME.DefaultValue = lbshowFRONTENDLINE.InnerText
            dt.Columns.Add(dataColumnNAME)

            Dim dataColumnDAY As DataColumn = New DataColumn("Date", GetType(String))
            dataColumnDAY.DefaultValue = CStr(Format(Now, "dd-MMM-yyyy"))
            dt.Columns.Add(dataColumnDAY)

            Dim dataColumnTIME As DataColumn = New DataColumn("Time", GetType(String))
            dataColumnTIME.DefaultValue = CStr(Format(Now, "HH:mm:ss"))
            dt.Columns.Add(dataColumnTIME)

            Dim dataColumnMATERIAL As DataColumn = New DataColumn("MATERIAL", GetType(String))
            dataColumnMATERIAL.DefaultValue = Session("RSMatNEW")
            dt.Columns.Add(dataColumnMATERIAL)

            Dim dataColumnINVOICE As DataColumn = New DataColumn("INVOICE", GetType(String))
            dataColumnINVOICE.DefaultValue = Session("RSinvNEW")
            dt.Columns.Add(dataColumnINVOICE)

            Dim dataColumMATERIALLOT As DataColumn = New DataColumn("MATERIALLOT", GetType(String))
            dataColumMATERIALLOT.DefaultValue = Session("RSMatLotNEW")
            dt.Columns.Add(dataColumMATERIALLOT)

            Dim dataColumnQTY As DataColumn = New DataColumn("QTY", GetType(String))
            dataColumnQTY.DefaultValue = Session("RSQtyNEW")
            dt.Columns.Add(dataColumnQTY)

            Dim dataColumnREELNO As DataColumn = New DataColumn("REELNO", GetType(String))
            dataColumnREELNO.DefaultValue = Session("RSReelnoNEW")
            dt.Columns.Add(dataColumnREELNO)

            Dim dataColumn1MODE1 As DataColumn = New DataColumn("Mode1", GetType(String))
            dataColumn1MODE1.DefaultValue = "ChangeMaterial"
            dt.Columns.Add(dataColumn1MODE1)

            Dim dataColumnCASSETTE As DataColumn = New DataColumn("CASSETTE", GetType(String))
            dataColumnCASSETTE.DefaultValue = tbCassetteSh.Text
            dt.Columns.Add(dataColumnCASSETTE)

            Dim dataColumnCOUNTER As DataColumn = New DataColumn("COUNTER", GetType(String))
            dataColumnCOUNTER.DefaultValue = TBCounter.Text
            dt.Columns.Add(dataColumnCOUNTER)

            Dim dataColumnPROCESS As DataColumn = New DataColumn("PROCESS", GetType(String))
            dataColumnPROCESS.DefaultValue = PROCESS
            dt.Columns.Add(dataColumnPROCESS)

            Dim dataColumnUSERHIS As DataColumn = New DataColumn("USERID", GetType(String))
            dataColumnUSERHIS.DefaultValue = tbUser.Text
            dt.Columns.Add(dataColumnUSERHIS)

            sda.Fill(dt)

            Dim latestFeedData = dt.AsEnumerable().GroupBy(Function(row) row.Field(Of String)("FEED")).
            Select(Function(group) group.OrderByDescending(Function(row) row.Field(Of String)("Date")).First()).CopyToDataTable()

            GridView11.DataSource = latestFeedData
            GridView11.DataBind()

            ViewState("MyDataTable") = latestFeedData
        End Using
    End Sub
    Private Sub load_clGVHIS()
        Dim dt As New DataTable()
        'dt.Columns.Add("Code", GetType(String))
        'dt.Columns.Add("Name", GetType(String))
        'dt.Columns.Add("Time", GetType(String))
        'dt.Columns.Add("Date", GetType(String))
        'dt.Columns.Add("Product", GetType(String))
        'dt.Columns.Add("Line", GetType(String))
        'dt.Columns.Add("Model", GetType(String))
        'dt.Columns.Add("LotNo", GetType(String))
        'dt.Columns.Add("Machine", GetType(String))
        'dt.Columns.Add("Feed", GetType(String))
        'dt.Columns.Add("Material", GetType(String))
        'dt.Columns.Add("Qty", GetType(String))
        'dt.Columns.Add("Invoice", GetType(String))
        'dt.Columns.Add("MaterialLot", GetType(String))
        'dt.Columns.Add("Cassette", GetType(String))
        'dt.Columns.Add("ReelNo", GetType(String))
        'dt.Columns.Add("Mode1", GetType(String))


        dt.Columns.Add("Name", GetType(String))
        dt.Columns.Add("Date", GetType(String))
        dt.Columns.Add("Time", GetType(String))
        dt.Columns.Add("Model", GetType(String))
        dt.Columns.Add("LotNo", GetType(String))
        dt.Columns.Add("Material", GetType(String))
        dt.Columns.Add("Invoice", GetType(String))
        dt.Columns.Add("MaterialLot", GetType(String))
        dt.Columns.Add("Qty", GetType(String))
        dt.Columns.Add("ReelNo", GetType(String))
        dt.Columns.Add("Line", GetType(String))
        dt.Columns.Add("Product", GetType(String))
        dt.Columns.Add("Mode1", GetType(String))
        dt.Columns.Add("Machine", GetType(String))
        dt.Columns.Add("Feed", GetType(String))
        dt.Columns.Add("Cassette", GetType(String))
        dt.Columns.Add("COUNTER", GetType(String))
        dt.Columns.Add("PROCESS", GetType(String))
        dt.Columns.Add("USERID", GetType(String))

        GridView11.DataSource = dt
        GridView11.DataBind()
    End Sub
    Protected Sub tbCassetteSh_TextChanged(sender As Object, e As EventArgs) Handles tbCassetteSh.TextChanged
        load_clGVHIS()
        Select Case TBCounter.Text = String.Empty

            Case True
                pnMaterialNEW.Visible = False
                PanelMATERIALHIS.Visible = False
                PNBUT.Visible = False
            Case False
                Load_GVHIS()
                PNBUT.Visible = True
                PanelMATERIALHIS.Visible = True
                pnMaterialNEW.Visible = True
        End Select
    End Sub

    Private Sub Button1_ServerClick(sender As Object, e As EventArgs) Handles Button1.ServerClick
        Select Case tbCassetteSh.Text <> String.Empty
            Case True

                Call load_GVDATA()

                PNBUT.Visible = False

                PNCassetteSh.Visible = False
                tbCassetteSh.Text = String.Empty

                lbshowMatLotNEW.InnerText = String.Empty
                lbshowinvNEW.InnerText = String.Empty
                lbshowQtyNEW.InnerText = String.Empty
                lbshowMatNEW.InnerText = String.Empty
                tnMaterialNEW.Text = String.Empty
                pnMaterialNEW.Visible = False

                lbshowMatLot.InnerText = String.Empty
                lbshowinv.InnerText = String.Empty
                lbshowQty.InnerText = String.Empty
                lbshowMat.InnerText = String.Empty
                tnMaterial.Text = String.Empty
                pnMaterial.Visible = False

                PNMAINMAT.Visible = False


                lbshowFeeddd1.InnerText = String.Empty
                PNSHOWFEED.Visible = False

                'TBFeed.Text = String.Empty
                PNFeed.Visible = False

                TBCounter.Text = String.Empty
                PNCounter.Visible = False

                ddlLineMC.SelectedIndex.ToString()
                PNddlMC.Visible = False

                load_GridView2()
                PanetbLot.Visible = False
                load_GridView1()
                GridViewPNMODEL.Visible = False

                PanetbModel.Visible = False

                ddlLine.SelectedIndex.ToString()

                PanelMATERIAL.Visible = False


                lbshowMATERIAL.InnerText = String.Empty
                PNlbshowMATERIAL.Visible = False
                lbshowFeeddd.InnerText = String.Empty
                PNlbshowFeed.Visible = False
                lbshowMC.InnerText = String.Empty
                PNlbshowMC.Visible = False
                lbshowLot.InnerText = String.Empty
                PNlbshowLot.Visible = False
                lbshowModel.InnerText = String.Empty
                PNModelShow.Visible = False
                lbshowLine.InnerText = String.Empty
                PNlbshowLine.Visible = False
                lbshowFRONTENDPD.InnerText = String.Empty
                lbshowDATE.InnerText = String.Empty
                lbshowTime.InnerText = String.Empty
                lbshowFRONTENDLINE.InnerText = String.Empty
                lbshowCode.InnerText = String.Empty
                pnLine.Visible = False

                ddlLinePD.SelectedIndex.ToString()
                PNddlLinePD.Visible = False
                tbUser.Text = String.Empty
        End Select
    End Sub
    Private Sub load_GridView2()
        Dim dtResult As New DataTable()
        dtResult.Columns.Add("LOTNO", GetType(String))
        GridView2.DataSource = dtResult
        GridView2.DataBind()
    End Sub
    Private Sub load_GridView1()
        Dim dtResult As New DataTable()
        dtResult.Columns.Add("MODEL", GetType(String))
        GridView1.DataSource = dtResult
        GridView1.DataBind()
    End Sub
    Private Sub load_GVDATA()
        Dim insideData As DataTable = CType(ViewState("MyDataTable"), DataTable)
        Dim connectionString As String = MT800DB
        Using connection As New OracleConnection(connectionString)
            connection.Open()
            For Each row As DataRow In insideData.Rows
                Dim sql As String = "INSERT INTO NVP_ALLPROCESS_HIS (USERNAME,DAY,TIME,MODEL,LOTNO,MATERIAL,INVOICE,MATERIALLOT,QTY,REELNO,LINE,PRODUCTS,MODE1,MACHINE,FEED,CASSETTE,COUNTER,PROCESS,USERID) VALUES (:sUSERNAME,:sDAY,:sTIME,:sMODEL,:sLOTNO,:sMATERIAL,:sINVOICE,:sMATERIALLOT,:sQTY,:sREELNO,:sLINE,:sPRODUCTS,:sMODE1,:sMACHINE,:sFEED,:sCASSETTE,:sCOUNTER,:sPROCESS,:sUSERID)"
                Using command As New OracleCommand(sql, connection)
                    'command.Parameters.Add(":suser", row("Code"))
                    'command.Parameters.Add(":sDAY", row("Date"))
                    'command.Parameters.Add(":sTIME", row("Time"))
                    'command.Parameters.Add(":sMODEL", row("MODEL"))
                    'command.Parameters.Add(":sLOTNO", row("LOTNO"))
                    'command.Parameters.Add(":sMATERIAL", row("MATERIAL"))
                    'command.Parameters.Add(":sINVOICE", row("INVOICE"))
                    'command.Parameters.Add(":sMATERIALLOT", row("MATERIALLOT"))
                    'command.Parameters.Add(":sQTY", row("QTY"))
                    'command.Parameters.Add(":sREELNO", row("REELNO"))
                    'command.Parameters.Add(":sLINE", row("LINE"))
                    'command.Parameters.Add(":sPRODUCT", row("PRODUCT"))
                    'command.Parameters.Add(":sMODE1", row("MODE1"))
                    'command.Parameters.Add(":sMACHINE", row("MACHINE"))
                    'command.Parameters.Add(":sFEED", row("FEED"))
                    'command.Parameters.Add(":sCASSETTE", row("CASSETTE"))



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
                    command.Parameters.Add(":sPRODUCTS", row("PRODUCTS"))
                    command.Parameters.Add(":sMODE1", row("Mode1"))
                    command.Parameters.Add(":sMACHINE", row("Machine"))
                    command.Parameters.Add(":sFEED", row("Feed"))
                    command.Parameters.Add(":sCASSETTE", row("Cassette"))
                    command.Parameters.Add(":sCOUNTER", row("COUNTER"))
                    command.Parameters.Add(":sPROCESS", row("PROCESS"))
                    command.Parameters.Add(":sUSERID", row("USERID"))
                    command.ExecuteNonQuery()
                End Using
            Next
            connection.Close()
        End Using
    End Sub
End Class
