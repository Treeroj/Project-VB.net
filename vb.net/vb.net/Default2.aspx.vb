'Imports System.Net
'Imports Font = System.Drawing.Font
'Imports iTextSharp.text
'Imports iTextSharp.text.pdf
'Imports iTextSharp.text.html.simpleparser
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports System.Data

Partial Class Default2
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
    Protected Sub btnReadPDF_Click(sender As Object, e As EventArgs) Handles btnReadPDF.ServerClick
        Dim pdfPath As String = ("\\172.16.72.179\lp-dcm\01_DC_CenterFile\LCR\2023\202309 Sep' 2023\MPD6D782\MPD6D782-B_39005_4_1pdf.pdf")
        Dim pdfReader As New PdfReader(pdfPath)

        Dim text2 As String = ReadTextFromPDFModel(pdfReader)
        Dim text3 As String = ReadTextFromPDFLotNo(pdfReader)
        Dim text4 As String = ReadTextFromPDFSidePCB(pdfReader)
        Dim text5 As String = ReadTextFromPDFLine(pdfReader)
        Dim text6 As String = ReadTextFromPDFOpratorInspectionID(pdfReader)
        Dim text7 As String = ReadTextFromPDFInspectionTime(pdfReader)

        Dim text8 As String = ReadTextFromPDFTotalchip(pdfReader)
        Dim text As String = ReadTextFromPDFCheckG(pdfReader)
        Dim text9 As String = ReadTextFromPDFCheckNG(pdfReader)
        Dim text10 As String = ReadTextFromPDFPassratio(pdfReader)
        Dim text11 As String = ReadTextFromPDFOpratormachineID(pdfReader)
        Dim text12 As String = ReadTextFromPDFInspectionDate(pdfReader)

        ' แสดงข้อมูลใน TextBox
        TextBox1.Text = text2
        TextBox2.Text = text3
        TextBox3.Text = text4
        TextBox4.Text = text5
        TextBox5.Text = text6
        TextBox6.Text = text7

        TextBox7.Text = text8
        txtCheckG.Text = text + " Pcs."
        TextBox8.Text = text9
        TextBox9.Text = text10
        TextBox10.Text = text11
        TextBox11.Text = text12
        Dim dt As DataTable = ReadTableFromPDF(pdfReader)
        GridViewDetail.DataSource = dt
        GridViewDetail.DataBind()

        pdfReader.Close()
    End Sub
    Dim text As New StringBuilder()
    Private Function ReadTextFromPDF(pdfReader As PdfReader) As String

        For page As Integer = 1 To pdfReader.NumberOfPages
            Dim strategy As New SimpleTextExtractionStrategy()
            Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
            text.Append(currentText)
        Next
   

        Return text.ToString()
    End Function
    Private Function ReadTextFromPDFCheckG(pdfReader As PdfReader) As String

        For page As Integer = 1 To pdfReader.NumberOfPages
            Dim strategy As New SimpleTextExtractionStrategy()
            Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
            text.Append(currentText)
        Next

        Dim startIndex As Integer = text.ToString().IndexOf("Check G   :")
        If startIndex >= 0 Then
            Dim endIndex As Integer = text.ToString().IndexOf("pcs.", startIndex)
            If endIndex >= 0 Then
                Return text.ToString().Substring(startIndex, endIndex - startIndex).Trim()
            End If
        End If

        Return ""
    End Function

    Private Function ReadTextFromPDFModel(pdfReader As PdfReader) As String

        For page As Integer = 1 To pdfReader.NumberOfPages
            Dim strategy As New SimpleTextExtractionStrategy()
            Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
            text.Append(currentText)
        Next

        Dim startIndex As Integer = text.ToString().IndexOf("Model :")
        If startIndex >= 0 Then
            Dim endIndex As Integer = text.ToString().IndexOf("Total chip", startIndex)
            If endIndex >= 0 Then
                Return text.ToString().Substring(startIndex, endIndex - startIndex).Trim()
            End If
        End If

        Return ""
    End Function
    Private Function ReadTextFromPDFLotNo(pdfReader As PdfReader) As String

        For page As Integer = 1 To pdfReader.NumberOfPages
            Dim strategy As New SimpleTextExtractionStrategy()
            Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
            text.Append(currentText)
        Next

        Dim startIndex As Integer = text.ToString().IndexOf("Lot No. :")
        If startIndex >= 0 Then
            Dim endIndex As Integer = text.ToString().IndexOf("Check G", startIndex)
            If endIndex >= 0 Then
                Return text.ToString().Substring(startIndex, endIndex - startIndex).Trim()
            End If
        End If

        Return ""
    End Function

    Private Function ReadTextFromPDFSidePCB(pdfReader As PdfReader) As String

        For page As Integer = 1 To pdfReader.NumberOfPages
            Dim strategy As New SimpleTextExtractionStrategy()
            Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
            text.Append(currentText)
        Next

        Dim startIndex As Integer = text.ToString().IndexOf("Side PCB :")
        If startIndex >= 0 Then
            Dim endIndex As Integer = text.ToString().IndexOf("Check NG", startIndex)
            If endIndex >= 0 Then
                Return text.ToString().Substring(startIndex, endIndex - startIndex).Trim()
            End If
        End If

        Return ""
    End Function

    Private Function ReadTextFromPDFLine(pdfReader As PdfReader) As String

        For page As Integer = 1 To pdfReader.NumberOfPages
            Dim strategy As New SimpleTextExtractionStrategy()
            Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
            text.Append(currentText)
        Next

        Dim startIndex As Integer = text.ToString().IndexOf("Line :")
        If startIndex >= 0 Then
            Dim endIndex As Integer = text.ToString().IndexOf("Pass ratio", startIndex)
            If endIndex >= 0 Then
                Return text.ToString().Substring(startIndex, endIndex - startIndex).Trim()
            End If
        End If

        Return ""
    End Function
    Private Function ReadTextFromPDFOpratorInspectionID(pdfReader As PdfReader) As String

        For page As Integer = 1 To pdfReader.NumberOfPages
            Dim strategy As New SimpleTextExtractionStrategy()
            Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
            text.Append(currentText)
        Next

        Dim startIndex As Integer = text.ToString().IndexOf("Oprator Inspection ID :")
        If startIndex >= 0 Then
            Dim endIndex As Integer = text.ToString().IndexOf("Oprator machine ID :", startIndex)
            If endIndex >= 0 Then
                Return text.ToString().Substring(startIndex, endIndex - startIndex).Trim()
            End If
        End If

        Return ""
    End Function
    Private Function ReadTextFromPDFInspectionTime(pdfReader As PdfReader) As String

        For page As Integer = 1 To pdfReader.NumberOfPages
            Dim strategy As New SimpleTextExtractionStrategy()
            Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
            text.Append(currentText)
        Next

        Dim startIndex As Integer = text.ToString().IndexOf("Inspection  Time :")
        If startIndex >= 0 Then
            Dim endIndex As Integer = text.ToString().IndexOf("Inspection Date :", startIndex)
            If endIndex >= 0 Then
                Return text.ToString().Substring(startIndex, endIndex - startIndex).Trim()
            End If
        End If

        Return ""
    End Function
    Private Function ReadTextFromPDFTotalchip(pdfReader As PdfReader) As String

        For page As Integer = 1 To pdfReader.NumberOfPages
            Dim strategy As New SimpleTextExtractionStrategy()
            Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
            text.Append(currentText)
        Next

        Dim startIndex As Integer = text.ToString().IndexOf("Total chip :")
        If startIndex >= 0 Then
            Dim endIndex As Integer = text.ToString().IndexOf("Lot No. :", startIndex)
            If endIndex >= 0 Then
                Return text.ToString().Substring(startIndex, endIndex - startIndex).Trim()
            End If
        End If

        Return ""
    End Function
    Private Function ReadTextFromPDFCheckNG(pdfReader As PdfReader) As String

        For page As Integer = 1 To pdfReader.NumberOfPages
            Dim strategy As New SimpleTextExtractionStrategy()
            Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
            text.Append(currentText)
        Next

        Dim startIndex As Integer = text.ToString().IndexOf("Check NG :")
        If startIndex >= 0 Then
            Dim endIndex As Integer = text.ToString().IndexOf("Line : ", startIndex)
            If endIndex >= 0 Then
                Return text.ToString().Substring(startIndex, endIndex - startIndex).Trim()
            End If
        End If

        Return ""
    End Function
    Private Function ReadTextFromPDFPassratio(pdfReader As PdfReader) As String

        For page As Integer = 1 To pdfReader.NumberOfPages
            Dim strategy As New SimpleTextExtractionStrategy()
            Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
            text.Append(currentText)
        Next

        Dim startIndex As Integer = text.ToString().IndexOf("Pass ratio :")
        If startIndex >= 0 Then
            Dim endIndex As Integer = text.ToString().IndexOf("Oprator Inspection ID :", startIndex)
            If endIndex >= 0 Then
                Return text.ToString().Substring(startIndex, endIndex - startIndex).Trim()
            End If
        End If

        Return ""
    End Function
    Private Function ReadTextFromPDFOpratormachineID(pdfReader As PdfReader) As String

        For page As Integer = 1 To pdfReader.NumberOfPages
            Dim strategy As New SimpleTextExtractionStrategy()
            Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
            text.Append(currentText)
        Next

        Dim startIndex As Integer = text.ToString().IndexOf("Oprator machine ID :")
        If startIndex >= 0 Then
            Dim endIndex As Integer = text.ToString().IndexOf("Inspection  Time :", startIndex)
            If endIndex >= 0 Then
                Return text.ToString().Substring(startIndex, endIndex - startIndex).Trim()
            End If
        End If

        Return ""
    End Function
    Private Function ReadTextFromPDFInspectionDate(pdfReader As PdfReader) As String

        For page As Integer = 1 To pdfReader.NumberOfPages
            Dim strategy As New SimpleTextExtractionStrategy()
            Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
            text.Append(currentText)
        Next

        Dim startIndex As Integer = text.ToString().IndexOf("Inspection Date :")
        If startIndex >= 0 Then
            Dim endIndex As Integer = text.ToString().IndexOf("Detail", startIndex)
            If endIndex >= 0 Then
                Return text.ToString().Substring(startIndex, endIndex - startIndex).Trim()
            End If
        End If

        Return ""
    End Function
    Private Function ReadTableFromPDF(pdfReader As PdfReader) As DataTable
        Dim table As New DataTable()
        table.Columns.Add("Step")
        table.Columns.Add("Mode")
        table.Columns.Add("Position")
        table.Columns.Add("EPCode")
        table.Columns.Add("Tolerance")
        table.Columns.Add("Min")
        table.Columns.Add("Max")
        table.Columns.Add("Result")
        table.Columns.Add("Unit")
        table.Columns.Add("Judgment")

        Dim text As String = ReadTextFromPDF(pdfReader)
        Dim lines() As String = text.Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)

        Dim data As String = lines(0) 
        Dim inspectionData() As String = data.Split(New String() {"*** Inspection result data ***"}, StringSplitOptions.RemoveEmptyEntries)

        ' เลือกข้อมูลการตรวจสอบจากอาร์เรย์
        Dim inspectionText As String = inspectionData(4) 

        Dim tableStartIndex As Integer = inspectionText.IndexOf("Step Mode Position EP-code Tolerance Min Max Result Unit Judgment")
        If tableStartIndex >= 0 Then
            Dim tableData As String = inspectionText.Substring(tableStartIndex)
            Dim lines1() As String = tableData.Split(vbLf)

            Dim startDetail As Boolean = True
            'For i As Integer = 1 To lines1.Length - 1
            '    Dim line As String = lines1(i)
            '    line = line.Replace(" ~ ", ToString) 

            '    Dim columns() As String = lines1(i).Split(" "c)
            '    If columns.Length >= 10 Then
            '        table.Rows.Add(columns(0), columns(1), columns(2), columns(3), columns(4), columns(5), columns(7), columns(8), columns(9), columns(10))
            '    End If
            'Next

            For i As Integer = 1 To lines1.Length - 1
                Dim line As String = lines1(i)


                If line.StartsWith("Step Mode Position EP-code Tolerance Min Max Result Unit Judgment") Then
                    startDetail = True
                    Continue For
                End If


                'If String.IsNullOrWhiteSpace(line) OrElse line.StartsWith("LCR version") OrElse line.StartsWith("Detail") Then
                '    Exit For
                'End If



                If startDetail Then
                    line = line.Replace(" ~ ", " ") 
                    Dim columns() As String = line.Split(" "c)

                    ' ตรวจสอบว่าคอลัมน์ "Judgment" มีค่า "LCR" หรือไม่
                    If columns.Length >= 11 AndAlso columns(9).Contains("LCR") Then
                        Dim columnsedit() As String = columns(9).Split("LCR")
                        If columns.Length >= 10 Then
                            ' เพิ่มข้อมูลลงใน DataTable โดยใช้คอลัมน์ที่มีค่า "LCR"
                            table.Rows.Add(columns(0), columns(1), columns(2), columns(3), columns(4), columns(5), columns(6), columns(7), columns(8), columnsedit(0))
                        End If
                    ElseIf columns.Length >= 10 AndAlso columns(5).Contains("~") Then
                        Dim columnsedit() As String = columns(5).Split("~")
                        If columns.Length >= 10 Then
                            ' เพิ่มข้อมูลลงใน DataTable โดยใช้คอลัมน์ที่มีค่า "LCR"
                            table.Rows.Add(columns(0), columns(1), columns(2), columns(3), columns(4), columnsedit(0), columns(6), columns(7), columns(8), columns(9))
                        End If
                    Else
                        If columns.Length >= 10 Then
                            ' เพิ่มข้อมูลลงใน DataTable โดยใช้คอลัมน์ปกติ
                            table.Rows.Add(columns(0), columns(1), columns(2), columns(3), columns(4), columns(5), columns(6), columns(7), columns(8), columns(9))
                        End If
                    End If


                End If
            Next


        End If

        Return table
    End Function

End Class
