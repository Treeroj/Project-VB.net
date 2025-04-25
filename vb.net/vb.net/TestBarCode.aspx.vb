Imports System.Drawing
Imports System.Drawing.Imaging
Imports ZXing
Partial Class TestBarCode
    Inherits System.Web.UI.Page
    Protected Sub btnGenerateBarcode_Click(sender As Object, e As EventArgs) Handles btnGenerateBarcode.Click
        Dim dataToEncode As String = txtBarcodeData.Text.Trim()
        dataToEncode = ToAscii(dataToEncode)
        Dim barcodeWriter As New BarcodeWriter()
        barcodeWriter.Format = BarcodeFormat.CODE_128 ' สามารถเปลี่ยนเป็น CODE_39 หรือรูปแบบอื่นๆ ตามที่คุณต้องการ
        Dim bitmap As Bitmap = barcodeWriter.Write(dataToEncode)
        imgBarcode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(BitmapToBytes(bitmap))
    End Sub

    Private Function ToAscii(input As String) As String
        Dim asciiBytes() As Byte = Encoding.ASCII.GetBytes(input)
        Dim asciiString As String = Encoding.ASCII.GetString(asciiBytes)
        Return asciiString
    End Function

    Private Function BitmapToBytes(bitmap As Bitmap) As Byte()
        Dim stream As New System.IO.MemoryStream()
        bitmap.Save(stream, Imaging.ImageFormat.Png)
        Return stream.ToArray()
    End Function
End Class
