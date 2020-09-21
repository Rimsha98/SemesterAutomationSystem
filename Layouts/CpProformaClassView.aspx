<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CpProformaClassView.aspx.cs" Inherits="UokSemesterSystem.CpProformaClassView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GvClass" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" style="text-align: left" >

                <AlternatingRowStyle BackColor="White" />

                <Columns >
                    
                    <asp:BoundField DataField="ClassName" HeaderText="Class Name"/>
                    <asp:BoundField DataField="ClassSection" HeaderText="Class Section"/>
                    <asp:BoundField DataField="Shift" HeaderText="Shift"/>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="ClassLink" Text="View Result" runat="server" CommandArgument='<%# Eval("ClassID") %>' OnClick="ClassLink_Click"/>
                            
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
