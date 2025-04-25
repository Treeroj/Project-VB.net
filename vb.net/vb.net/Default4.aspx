<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default4.aspx.vb" Inherits="Default4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
          <asp:TextBox ID="Record" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true" 
                                                         Width="100%" class="form-control" placeholder="ใส่รุ่น" onblur="__doPostBack('','');" />
             <asp:TextBox ID="ResultLabel" runat="server" autocomplete="off" ForeColor="black" AutoPostBack="true" Visible="true" 
                                                         Width="100%" class="form-control" placeholder="ใส่รุ่น" onblur="__doPostBack('','');" />
            <button id="Button1" runat="server"  type="button" class="btn btn-outline-dark"  >None</button>
        </div>
    </form>
</body>
</html>
