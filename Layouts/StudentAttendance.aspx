<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentAttendance.aspx.cs" Inherits="UokSemesterSystem.StudentAttendance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="~/css/StudentAttendance.css" />
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title>Student Attendance | UOK</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Attendance" style="float:left; width: 15%;"></div>

        <div class="container">
        <button id="HiddenBTN3" type="button" style="display: none;"></button>
        <div class="headingtop">
            <h1>
                <span style="color: #fff;">Current Semester Attendance</span>
            </h1>
        </div>
        <p id="TopMsg" runat="server" visible="true">The following table contains total percentage of attendance in respective courses.</p>
        <asp:Table ID="stdAttTable" runat="server" CellSpacing="0">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" CssClass="smallcell"><span class="head">Course No.</span></asp:TableCell>
                <asp:TableCell runat="server" CssClass="bigcell"><span class="head">Course Name</span></asp:TableCell>
                <asp:TableCell runat="server" CssClass="bigcell"><span class="head">Teacher Name</span></asp:TableCell>
                <asp:TableCell runat="server" CssClass="smallcell"><span class="head">Attendance (%)</span></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </div>
    </form>

    <script>
        $(function () {
            $("#Nav-Attendance").load("NavigationBar.aspx");
        });

        $(document).ready(function () {
            document.getElementById("HiddenBTN3").click();
        });

        $("#HiddenBTN3").on("click", function () {
            sessionStorage.setItem('bgcolor', 'green');
        });
    </script>
</body>
</html>
