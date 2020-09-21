<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="departList.aspx.cs" Inherits="UokSemesterSystem.departList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
        <asp:Label ID="Label1" runat="server"  style="font-weight: 700; font-size:x-large; text-decoration: underline" Text="UNIVERSITY OF KARACHI"></asp:Label>
           </center>
            <br />
        <br />
        <asp:Table ID="departTable" runat="server" Width="339px"  style="text-align:center;">
            <asp:TableRow runat="server"  style="font-weight: 600; font-size:medium">
                <asp:TableCell runat="server">DEPARTMENT NAME</asp:TableCell>
                </asp:TableRow>
        </asp:Table>
            
        <div>
        </div>
    </form>
</body>
</html>
