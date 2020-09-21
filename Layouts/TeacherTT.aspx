<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherTT.aspx.cs" Inherits="UokSemesterSystem.TeacherTT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="~/css/TeacherTT.css" />
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title>Teacher TimeTable | UOK</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-TimeTable" style="float:left; width: 15%;"></div>
        <div style="float: left; width: 85%; height: 100vh; overflow-y:scroll; overflow-x: hidden;">
            <div class="headingtop">
            <h1>
                <span style="color: #fff;">Current Semester TimeTable</span>
            </h1>
            </div>
            <h1 id="nocourses" runat="server" visible="false">You do not have any courses assigned to you.</h1>
            <asp:Table ID="TeacherCoursesMorning" runat="server" CssClass="CTable">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="6">
                        <h2>MORNING SCHEDULE</h2>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow CssClass="tablerow">
                    <asp:TableCell CssClass="thcell"><b>DAY</b></asp:TableCell>
                    <asp:TableCell CssClass="thcell"><b>CLASS</b></asp:TableCell>
                    <asp:TableCell CssClass="thcell"><b>SECTION</b></asp:TableCell>
                    <asp:TableCell CssClass="thcell"><b>COURSE #</b></asp:TableCell>
                    <asp:TableCell CssClass="thcell"><b>COURSE TITLE</b></asp:TableCell>
                    <asp:TableCell CssClass="thcell"><b>TIME</b></asp:TableCell>
                    <asp:TableCell CssClass="thcell" style="border-right: 0.1vw solid #cccccc;"><b>ROOM #</b></asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <br />
            <asp:Table ID="TeacherCoursesEvening" runat="server" CssClass="CTable">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="6">
                        <h2>EVENING SCHEDULE</h2>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow CssClass="tablerow">
                    <asp:TableCell CssClass="thcell"><b>DAY</b></asp:TableCell>
                    <asp:TableCell CssClass="thcell"><b>CLASS</b></asp:TableCell>
                    <asp:TableCell CssClass="thcell"><b>SECTION</b></asp:TableCell>
                    <asp:TableCell CssClass="thcell"><b>COURSE #</b></asp:TableCell>
                    <asp:TableCell CssClass="thcell"><b>COURSE TITLE</b></asp:TableCell>
                    <asp:TableCell CssClass="thcell"><b>TIME</b></asp:TableCell>
                    <asp:TableCell CssClass="thcell"><b>ROOM #</b></asp:TableCell>
                </asp:TableRow>

            </asp:Table>
            <br /><br /><br />
            <button id="teacherttbtn" type="button" style="display: none;"></button>
        </div>
    </form>

    <script>
    $(function () {
        $("#Nav-TimeTable").load("NavigationBar.aspx");
    });

        $(document).ready(function () {
            document.getElementById("teacherttbtn").click();
        });

        $("#teacherttbtn").on("click", function () {
            sessionStorage.setItem("teachertt","loaded");
        });

        </script>
</body>
</html>
