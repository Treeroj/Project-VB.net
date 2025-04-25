<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default3.aspx.vb" Inherits="Default3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="ddlMcDefectCode1" runat="server" AutoPostBack="true"
                                                                    CssClass="form-control js-example-placeholder-single" ToolTip="Select" Width="500">
                                                                </asp:DropDownList>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="false" 
                                            CssClass="table table-hover">
                                                <Columns>
                                                   <asp:BoundField DataField="DEF_MODE_SHOW" HeaderText="Feed" />
                                                    <asp:BoundField DataField="CODE_SHOW" HeaderText="Feed" />
                                                </Columns>
                                            </asp:GridView>
           <asp:GridView ID="gvnaja" runat="server" AutoGenerateColumns="false" ShowHeader="false" 
                                            CssClass="table table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="kuy" HeaderText="Feed" />
                                                </Columns>
                                            </asp:GridView>

            <button id="Button1" runat="server"  type="button" class="btn btn-outline-dark"  >None</button>
        </div>
    </form>
</body>
</html>
