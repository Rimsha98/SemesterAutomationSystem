<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendancePage.aspx.cs" Inherits="UokSemesterSystem.AttendancePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link  rel="stylesheet" runat="server" media="screen"  href="~/css/AttendancePage.css" />
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title>Classes List | UOK</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Attendance" style="float:left; width: 15%;"></div>

        <div style="float: left; width: 85%; height: 100vh; overflow-y:scroll; overflow-x: hidden;">
            <div class="headingtop">
            <h1>
                <span style="color: #fff;">Classes List</span>
            </h1>
            </div>
            <p class="parah">Following is a list of classes you are assigned. </p>
        <asp:Table ID="classesTable" runat="server" CellSpacing="0">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" CssClass="cellclass" >Class</asp:TableCell>
                <asp:TableCell runat="server" CssClass="cell">Shift</asp:TableCell>
                <asp:TableCell runat="server" CssClass="cell">Section</asp:TableCell>
                <asp:TableCell runat="server" CssClass="cell">Course #</asp:TableCell>
                <asp:TableCell runat="server" CssClass="cellbig">Course Title</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
            <button id="AttendancePagebtn" type="button" style="display: none;"></button>
        </div>
    </form>

    <script>
    $(function () {
        $("#Nav-Attendance").load("NavigationBar.aspx");
    });

        $(document).ready(function () {
            document.getElementById("AttendancePagebtn").click();
        });

        $("#AttendancePagebtn").on("click", function () {
            sessionStorage.setItem("AttendancePage", "loaded");
        });
        </script>
</body>
</html>
