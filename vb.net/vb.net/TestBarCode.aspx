<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TestBarCode.aspx.vb" Inherits="TestBarCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
        <div>
            <h1>Barcode Generator</h1>
            <div>
                <asp:TextBox ID="txtBarcodeData" runat="server" placeholder="Enter data for barcode"></asp:TextBox><br />
                 <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter data for barcode"></asp:TextBox><br />
                <asp:Button ID="btnGenerateBarcode" runat="server" Text="Generate Barcode" OnClick="btnGenerateBarcode_Click" />
            </div>
            <div>
                <asp:Image ID="imgBarcode" runat="server" />
                 <asp:Image ID="Image1" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
