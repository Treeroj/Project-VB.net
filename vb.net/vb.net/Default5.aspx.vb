Imports System.Data
Imports System.IO
Imports Microsoft.VisualBasic.FileIO

Partial Class Default5
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' ระบุที่อยู่ของไฟล์ .csv
        End If
    End Sub
    Protected Sub Button1_ServerClick(sender As Object, e As EventArgs) Handles Button1.ServerClick
        ' ระบุชื่อไฟล์ CSV ที่คุณต้องการอ่าน
        Dim csvFilePath As String = ("\\172.16.72.179\sta\Treeroj\datatest\LotPartsLog\Y42027\LotPartsLog+H8D4000-F+Y42027+20230926070224.csv")

        ' สร้าง DataTable เพื่อเก็บข้อมูลจาก CSV
        Dim dt As New DataTable()

        ' สร้างคอลัมน์ใน DataTable
        'dt.Columns.Add("Parts Num")
        'dt.Columns.Add("Parts Name")
        'dt.Columns.Add("Parts Comment")
        'dt.Columns.Add("Parts ID")

        ' ใช้ TextFieldParser ในการอ่านข้อมูลจากไฟล์ CSV
        Using parser As New TextFieldParser(csvFilePath)
            parser.TextFieldType = FieldType.Delimited
            parser.SetDelimiters(",")

            ' ข้ามบรรทัดหัวข้อคอลัมน์
            'parser.ReadLine()

            Dim isReading As Boolean = False
            Dim partsNumRowCount As Integer = 0


            ' อ่านข้อมูลและเพิ่มแถวใน DataTable
            While Not parser.EndOfData
                Dim fields As New List(Of String)(parser.ReadFields())
                'Dim partsNumIndex As Integer = Array.IndexOf(fields, "Parts Num")

                ' ตรวจสอบว่าคอลัมน์ที่ต้องการมีข้อมูลหรือไม่
                'If fields.Length >= 11 Then

                If Array.IndexOf(fields.ToArray(), "Machine Serial") >= 0 Then
                    Dim fieldCount As Integer = fields.Count
                    If fieldCount = 1 Then
                        TextBox1.Text = "ไม่พบข้อมูล Machine Serial"
                    ElseIf fieldCount = 2 Then
                        TextBox1.Text = fields(0) & " : " & fields(1)
                    ElseIf fieldCount = 3 Then
                        TextBox1.Text = "มีข้อมูลผิดปกติ"
                    Else
                        TextBox1.Text = "ไม่ตรงเงื่อนไขใด ๆ"
                    End If
                End If

                If Array.IndexOf(fields.ToArray(), "Machine Model") >= 0 Then
                    Dim fieldCount As Integer = fields.Count
                    If fieldCount = 1 Then
                        TextBox2.Text = "ไม่พบข้อมูล Machine Model"
                    ElseIf fieldCount = 2 Then
                        TextBox2.Text = fields(0) & " : " & fields(1)
                    ElseIf fieldCount = 3 Then
                        TextBox2.Text = "มีข้อมูลผิดปกติ"
                    Else
                        TextBox2.Text = "ไม่ตรงเงื่อนไขใด ๆ"
                    End If
                End If

                If Array.IndexOf(fields.ToArray(), "Machine Name") >= 0 Then
                    Dim fieldCount As Integer = fields.Count
                    If fieldCount = 1 Then
                        TextBox3.Text = "ไม่พบข้อมูล Machine Name"
                    ElseIf fieldCount = 2 Then
                        TextBox3.Text = fields(0) & " : " & fields(1)
                    ElseIf fieldCount = 3 Then
                        TextBox3.Text = "มีข้อมูลผิดปกติ"
                    Else
                        TextBox3.Text = "ไม่ตรงเงื่อนไขใด ๆ"
                    End If
                End If

                Dim startIndex As Integer = Array.IndexOf(fields.ToArray(), "Board Name")
                If startIndex >= 0 Then
                    Dim endIndex As Integer = Array.IndexOf(fields.ToArray(), "Production Lot", startIndex)
                    If endIndex >= 0 Then
                        Dim resultBF As String = String.Join(" ", fields.ToArray(), startIndex, endIndex - startIndex - 1)
                        Dim result As String = String.Join(" ", fields.ToArray(), startIndex + 1, endIndex - startIndex - 1)
                        TextBox4.Text = resultBF & " : " & result.Trim()
                    End If
                End If

                Dim startIndexProductionLot As Integer = Array.IndexOf(fields.ToArray(), "Production Lot")
                If startIndexProductionLot >= 0 Then
                    Dim endIndexProductionLot As Integer = Array.IndexOf(fields.ToArray(), "Production Model", startIndexProductionLot)
                    If endIndexProductionLot >= 0 Then
                        Dim resultBF As String = String.Join(" ", fields.ToArray(), startIndexProductionLot, endIndexProductionLot - startIndexProductionLot - 1)
                        Dim result As String = String.Join(" ", fields.ToArray(), startIndexProductionLot + 1, endIndexProductionLot - startIndexProductionLot - 1)
                        TextBox5.Text = resultBF & " : " & result.Trim()
                    End If
                End If

                Dim startIndexProductionModel As Integer = Array.IndexOf(fields.ToArray(), "Production Model")
                If startIndexProductionModel >= 0 Then
                    Dim endIndexProductionModel As Integer = Array.IndexOf(fields.ToArray(), "Surface Info", startIndexProductionModel)
                    If endIndexProductionModel >= 0 Then
                        Dim resultBF As String = String.Join(" ", fields.ToArray(), startIndexProductionModel, endIndexProductionModel - startIndexProductionModel - 1)
                        Dim result As String = String.Join(" ", fields.ToArray(), startIndexProductionModel + 1, endIndexProductionModel - startIndexProductionModel - 1)
                        TextBox6.Text = resultBF & " : " & result.Trim()
                    End If
                End If

                Dim startIndexSurfaceInfo As Integer = Array.IndexOf(fields.ToArray(), "Surface Info")
                If startIndexSurfaceInfo >= 0 Then
                    Dim resultBF As String = fields(startIndexSurfaceInfo)
                    Dim result As String = fields(startIndexSurfaceInfo + 1)
                    TextBox7.Text = resultBF & " : " & result.Trim()
                End If

                ' ตรวจสอบว่าคอลัมน์ "Parts Num" มีข้อมูลหรือไม่
                If Array.IndexOf(fields.ToArray(), "Parts Num") >= 0 Then
                    isReading = True ' เริ่มการอ่านข้อมูล "Parts Num"
                    partsNumRowCount = 0
                    For Each columnName As String In fields
                        dt.Columns.Add(columnName)

                    Next
                End If

                If isReading Then
                    partsNumRowCount += 1
                    ' เพิ่มข้อมูลเพิ่มเติมใน List(Of String)
                    'For i As Integer = 0 To 3 ' เพิ่มข้อมูลเพิ่มเติมในคอลัมน์ที่ 4, 5, 6, และ 7
                    '    fields.Add("AdditionalData" & (i + 1))
                    'Next
                    ' ตรวจสอบเงื่อนไขการออกจากลูป (ถ้าคุณต้องการให้อ่านจำนวนแถวที่คุณต้องการ)
                    If partsNumRowCount >= 2 Then ' ให้อ่าน 2 แถวเพิ่มเติมหลังจากพบ "Parts Num"
                        dt.Rows.Add(fields.ToArray()) ' เพิ่มข้อมูลจาก List(Of String) ลงใน DataTable
                    End If
                    ' เพิ่มข้อมูลลงใน DataTable

                End If
            End While
        End Using

        ' กำหนด DataTable เป็นแหล่งข้อมูลของ GridView
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

End Class
