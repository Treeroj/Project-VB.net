<%@ Page Language="VB" AutoEventWireup="false" CodeFile="resultdata.aspx.vb" Inherits="resultdata" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h5 class="card-title">MATERIAL HIS</h5>
             <asp:GridView ID="DataGridview1" runat="server" ShowHeader="true" AutoGenerateColumns="false"   PageSize="10"
                                             >                                            
                                                <Columns>
                                                        <asp:BoundField DataField="MODEL" HeaderText="Model" />
                                                        <asp:BoundField DataField="LOTNO" HeaderText="LotNo" />
                                                        <asp:BoundField DataField="MACHINE" HeaderText="Machine" />
                                                        <asp:BoundField DataField="FEED" HeaderText="Feed" />
                                                        <asp:BoundField DataField="COUNTER" HeaderText="Counter" />
                                                        <asp:BoundField DataField="MATERIAL" HeaderText="Material" />
                                                        <asp:BoundField DataField="QTY" HeaderText="Qty" />
                                                        <asp:BoundField DataField="INVOICE" HeaderText="Invoice" />
                                                        <asp:BoundField DataField="MATERIALLOT" HeaderText="MaterialLot" />
                                                        <asp:BoundField DataField="CASSETTE" HeaderText="Cassette" />
                                                        <asp:BoundField DataField="REELNO" HeaderText="REELNO" />
                                                </Columns>
                                            </asp:GridView>
          <asp:GridView ID="GridView11" runat="server" ShowHeader="true" AutoGenerateColumns="false"   PageSize="10" Visible="false"
                                             >                                            
                                                <Columns>
                                                        <asp:BoundField DataField="MODEL" HeaderText="Model" />
                                                        <asp:BoundField DataField="LOTNO" HeaderText="Model" />
                                                    <asp:BoundField DataField="LOTNO" HeaderText="Model" />
                                                </Columns>
                                            </asp:GridView>
          
           <asp:GridView ID="GridView2" runat="server" ShowHeader="true" AutoGenerateColumns="false"   PageSize="10"
                                             >                                            
                                                <Columns>
                                                        <asp:BoundField DataField="MODEL" HeaderText="Model" />
                                                        <asp:BoundField DataField="LOTNO" HeaderText="Model" />
                                                    <asp:BoundField DataField="LOTNO" HeaderText="Model" />
                                                </Columns>
                                            </asp:GridView>
        </div>
    </form>
</body>
</html>
