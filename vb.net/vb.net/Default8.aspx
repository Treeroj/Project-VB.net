<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default8.aspx.vb" Inherits="Default8" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">


<head runat="server">
    <title>Hello</title>
</head>



<body>


    <form id="form1" runat="server">

        <div>
            <h1>Hello</h1>

            <asp:textbox runat="server" ID="selecttext" AutoPostBack="true" >

            </asp:textbox>
            
            
            <asp:GridView ID="HIS" runat="server" AutoGenerateColumns="false" Visible="false" >
                <Columns>
                    <asp:BoundField DataField="MATERIAL" HeaderText="MAT" />
                    <asp:BoundField DataField="MODEL" HeaderText="1" />
                </Columns>
            </asp:GridView>

            <asp:GridView ID="DataGridView1" runat="server" AutoGenerateColumns="false" >
                <Columns>
                    <asp:BoundField DataField="MATERIAL" HeaderText="MAT" />
                    <asp:BoundField DataField="MODEL" HeaderText="1" />
                </Columns>
            </asp:GridView>


        </div>
    </form>



</body>
</html>
