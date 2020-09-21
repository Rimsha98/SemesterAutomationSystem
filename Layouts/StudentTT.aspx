<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentTT.aspx.cs" Inherits="UokSemesterSystem.StudentTT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="~/css/StudentTT.css" />
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title>Student TimeTable | UOK</title>
</head>

<body>
    <form id="form1" runat="server">
        <div id="Nav-TimeTable" style="float:left; width: 15%;"></div>
         
        <div class="container">

            <div class="headingtop">
            <h1 id="TimeTableHeadTop">
                <span style="color: #fff;">Current Semester TimeTable</span>
            </h1>
            </div>
            <button id="HiddenBTN2" type="button" style="display: none;"></button>

            <!--------------- Time Table --------------->
            <asp:Table ID="ClassSchedule" runat="server">
                <asp:TableRow>
                    <asp:TableCell CssClass="head" ColumnSpan="5" BackColor="#D1E8F1">
                        <asp:Label ID="classname" runat="server"></asp:Label>
                        <asp:Label ID="classshift" runat="server"></asp:Label>
                        <asp:Label ID="classsection" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow ID="MondayTime" runat="server">
                    <asp:TableCell RowSpan="2" CssClass="LeftDays">
                        <asp:Label runat="server">Monday</asp:Label>
                    </asp:TableCell>
                    <asp:TableCell CssClass="time"><asp:Label runat="server">TIME</asp:Label></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="MondayCourses" runat="server">
                    <asp:TableCell CssClass="LeftDays"><asp:Label runat="server">COURSE</asp:Label></asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow ID="TuesdayTime" runat="server">
                    <asp:TableCell RowSpan="2" CssClass="LeftDays"><asp:Label runat="server">Tuesday</asp:Label></asp:TableCell>
                    <asp:TableCell CssClass="time"><asp:Label runat="server">TIME</asp:Label></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="TuesdayCourses" runat="server">
                    <asp:TableCell CssClass="LeftDays"><asp:Label runat="server">COURSE</asp:Label></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="WednesdayTime" runat="server">
                    <asp:TableCell RowSpan="2" CssClass="LeftDays"><asp:Label runat="server">Wednesday</asp:Label></asp:TableCell>
                    <asp:TableCell CssClass="time"><asp:Label runat="server">TIME</asp:Label></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="WednesdayCourses" runat="server">
                    <asp:TableCell CssClass="LeftDays"><asp:Label runat="server">COURSE</asp:Label></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="ThursdayTime" runat="server">
                    <asp:TableCell RowSpan="2" CssClass="LeftDays"><asp:Label runat="server">Thursday</asp:Label></asp:TableCell>
                    <asp:TableCell CssClass="time"><asp:Label runat="server">TIME</asp:Label></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="ThursdayCourses" runat="server">
                    <asp:TableCell CssClass="LeftDays"><asp:Label runat="server">COURSE</asp:Label></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="FridayTime" runat="server">
                    <asp:TableCell RowSpan="2" CssClass="LeftDays"><asp:Label runat="server">Friday</asp:Label></asp:TableCell>
                    <asp:TableCell CssClass="time"><asp:Label runat="server">TIME</asp:Label></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="FridayCourses" runat="server">
                    <asp:TableCell CssClass="LeftDays"><asp:Label runat="server">COURSE</asp:Label></asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <!--------------- Course Table --------------->
            <div class="coursetable"></div>
            <asp:Table ID="CoursesList" runat="server">
                <asp:TableRow>
                    <asp:TableCell runat="server" CssClass="ListCell" BackColor="#f0f0f0">
                        <asp:Label runat="server">Course #</asp:Label>
                    </asp:TableCell>

                    <asp:TableCell runat="server" CssClass="ListCell2" BackColor="#f0f0f0">
                        <asp:Label runat="server">Course Title</asp:Label>
                    </asp:TableCell>

                    <asp:TableCell runat="server" CssClass="ListCell3" BackColor="#f0f0f0">
                        <asp:Label runat="server">Hours</asp:Label>
                    </asp:TableCell>

                    <asp:TableCell runat="server" CssClass="ListCell4" BackColor="#f0f0f0">
                        <asp:Label runat="server">Teacher</asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br /><br />
        </div>
    </form>

    <script>
    $(function () {
        $("#Nav-TimeTable").load("NavigationBar.aspx");
    });

    $(document).ready(function () {
        document.getElementById("HiddenBTN2").click();
    });

    $("#HiddenBTN2").on("click", function () {
        sessionStorage.setItem('color', 'red');
    });

    </script>
</body>
</html>
