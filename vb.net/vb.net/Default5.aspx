<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default5.aspx.vb" Inherits="Default5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" Width="500"></asp:TextBox>
            <br/>
            <br/>
            <asp:TextBox ID="TextBox2" runat="server" ReadOnly="true" Width="500"></asp:TextBox>
            <br/>
            <br/>
            <asp:TextBox ID="TextBox3" runat="server" ReadOnly="true" Width="500"></asp:TextBox>
            <br/>
            <br/>
            <asp:TextBox ID="TextBox4" runat="server" ReadOnly="true" Width="500"></asp:TextBox>
            <br/>
            <br/>
            <asp:TextBox ID="TextBox5" runat="server" ReadOnly="true" Width="500"></asp:TextBox>
            <br/>
            <br/>
            <asp:TextBox ID="TextBox6" runat="server" ReadOnly="true" Width="500"></asp:TextBox>
            <br/>
            <br/>
            <asp:TextBox ID="TextBox7" runat="server" ReadOnly="true" Width="500"></asp:TextBox>
            <br/>
            <br/>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True"></asp:GridView>

            <button id="Button1" runat="server"  type="button" class="btn btn-outline-dark"  >None</button>
        </div>
    </form>
</body>
</html>
