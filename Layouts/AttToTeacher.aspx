<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttToTeacher.aspx.cs" Inherits="UokSemesterSystem.viewAttToTeacher" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link  rel="stylesheet" runat="server" media="screen"  href="~/css/AttToTeacher.css" />
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title>Class Attendance | UOK</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Attendance" style="float:left; width: 15%;"></div>

        <div style="float: left; width: 85%; height: 100vh; overflow-y:scroll; overflow-x: hidden; text-align: center;">
            <div style="margin-top: 5vw; font-family: Calibri; font-weight: 600; font-size: 2vw;">
                 
                <asp:Label ID="Label1" runat="server" Text="UNIVERSITY OF KARACHI"></asp:Label>
                <br /><br />
                <p style="font-size: 1.6vw; "><label>Course Number: </label><asp:Label ID="cNo" runat="server" Text="Label" Font-Bold="false"></asp:Label></p>
                <p style="font-size: 1.6vw; "><label>Course Title: </label><asp:Label ID="cName" runat="server" Text="Label" Font-Bold="false"></asp:Label></p>
                <p style="font-size: 1.6vw; "><label>Class: </label><asp:Label ID="className" runat="server" Text="Label" Font-Bold="false"></asp:Label></p>
            </div>
            <asp:Table ID="AttTView" runat="server" CellSpacing="0">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" CssClass="cellsmall">S.no.</asp:TableCell>
                <asp:TableCell runat="server" CssClass="cellmedium">Roll No.</asp:TableCell>
                <asp:TableCell runat="server" CssClass="cellbig">Student Name</asp:TableCell>
                <asp:TableCell runat="server" CssClass="cellbig">Father's Name</asp:TableCell>
            </asp:TableRow>
          
        </asp:Table>
            <div style="width: 90%;  margin: 0 auto; text-align: right; ">
                <asp:Button ID="Back" runat="server" Text="Back" OnClick="Back_Click"/>
                <button id="AttToTeacherbtn" type="button" style="display: none;"></button>
            </div>
            <br /><br />
        </div>
    </form>

    <script>
    $(function () {
        $("#Nav-Attendance").load("NavigationBar.aspx");
    });

        $(document).ready(function () {
            document.getElementById("AttToTeacherbtn").click();
        });

        $("#AttToTeacherbtn").on("click", function () {
            sessionStorage.setItem("AttToTeacher", "loaded");
        });
        </script>
</body>
</html>


