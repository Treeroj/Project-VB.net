<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
              
            <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true"></asp:TextBox>
            <asp:TextBox ID="TextBox2" runat="server" ReadOnly="true"></asp:TextBox>
            <asp:TextBox ID="TextBox3" runat="server" ReadOnly="true"></asp:TextBox>
            <asp:TextBox ID="TextBox4" runat="server" ReadOnly="true"></asp:TextBox>
            <asp:TextBox ID="TextBox5" runat="server" ReadOnly="true" Width="200"></asp:TextBox>
            <asp:TextBox ID="TextBox6" runat="server" ReadOnly="true" Width="200"></asp:TextBox>                   
            <br />
            <br />
            <asp:TextBox ID="TextBox7" runat="server" ReadOnly="true"></asp:TextBox>
            <asp:TextBox ID="txtCheckG" runat="server" ReadOnly="true"></asp:TextBox>
            <asp:TextBox ID="TextBox8" runat="server" ReadOnly="true"></asp:TextBox>
            <asp:TextBox ID="TextBox9" runat="server" ReadOnly="true"></asp:TextBox>
            <asp:TextBox ID="TextBox10" runat="server" ReadOnly="true" Width="200"></asp:TextBox>
            <asp:TextBox ID="TextBox11" runat="server" ReadOnly="true" Width="200"></asp:TextBox>
            <%--<iframe title="Defective_MT800" width="100%" height="1000" src="https://app.powerbi.com/reportEmbed?reportId=ea9d0776-29b9-4f83-a814-f4e41ca47bb2&autoAuth=true&ctid=afff1096-7fd8-4cdd-879a-7db50a47287a" frameborder="0" allowFullScreen="true"></iframe>--%>
<asp:GridView ID="GridViewDetail" runat="server" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField DataField="Step" HeaderText="Step" />
        <asp:BoundField DataField="Mode" HeaderText="Mode" />
        <asp:BoundField DataField="Position" HeaderText="Position" />
        <asp:BoundField DataField="EPCode" HeaderText="EP-code" />
        <asp:BoundField DataField="Tolerance" HeaderText="Tolerance" />
        <asp:BoundField DataField="Min" HeaderText="Min" />
        <asp:BoundField DataField="Max" HeaderText="Max" />
        <asp:BoundField DataField="Result" HeaderText="Result" />
        <asp:BoundField DataField="Unit" HeaderText="Unit" />
        <asp:BoundField DataField="Judgment" HeaderText="Judgment" />
    </Columns>
</asp:GridView>
            <button id="btnReadPDF" runat="server"  type="button" class="btn btn-outline-dark"  >None</button>
        </div>
    </form>
</body>
</html>
