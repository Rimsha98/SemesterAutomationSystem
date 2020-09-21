<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NavigationBar.aspx.cs" Inherits="UokSemesterSystem.NavigationBar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="~/css/NavigationBar.css" />
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
        <div class="Navbar">
            <div class="ProfilePic">
                <asp:Image ID="ProfilePicNav" runat="server" ImageUrl="~/Img/img1.jpg"></asp:Image><br />
                <h1 class="studentnamenav" runat="server" id="namenav">NAME HERE</h1>
            </div>

            <div class="OutsideDIV">
                <div class="InnerDIV"></div>
                <asp:Button ID="ProfileNav" class="navButton" runat="server" OnClick="BtnProfile_Click"/>
                <div class="test"></div>
                <asp:Button ID="TimeTableNav" class="navButton" runat="server" Text="Time Table" OnClick="BtnTimeTable_Click" />
                <div class="test"></div>
                <asp:Button ID="AttendanceNav" class="navButton" runat="server" Text="Attendance"  OnClick="BtnAttendance_Click"/>
                <div class="test"></div>
                <asp:Button ID="ResultsNav" class="navButton" runat="server" Text="Results"  OnClick="BtnResult_Click"/>
                <div class="test"></div>
                <asp:Button ID="SettingsNav" class="navButton" runat="server" Text="Account Settings" OnClick="BtnSettings_Click"/>
                <div class="test"></div>
                <asp:Button ID="LogoutNav" class="navButton" runat="server" Text="Logout" OnClick="BtnLogout_Click"/>
            </div>
        </div>
    </form>

    <script>

        if (sessionStorage.getItem('button') === 'clicked') {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ProfileNav").css({ backgroundColor: "#1D8AB5" });
        }

        if (sessionStorage.getItem('color') === 'red') {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#TimeTableNav").css({ backgroundColor: "#1D8AB5" });
        }

        if (sessionStorage.getItem('bgcolor') === 'green') {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#AttendanceNav").css({ backgroundColor: "#1D8AB5" });
           
        }

        if (sessionStorage.getItem('font') === 'Helvetica') {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ResultsNav").css({ backgroundColor: "#1D8AB5" });

        }

        if (sessionStorage.getItem("test1") === "lorem") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#SettingsNav").css({ backgroundColor: "#1D8AB5" });

        }

        if (sessionStorage.getItem("teacherprofile") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ProfileNav").css({ backgroundColor: "#1D8AB5" });

        }

        if (sessionStorage.getItem("teachertt") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#TimeTableNav").css({ backgroundColor: "#1D8AB5" });

        }

        if (sessionStorage.getItem("AttendancePage") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#AttendanceNav").css({ backgroundColor: "#1D8AB5" });
        }

        if (sessionStorage.getItem("AttToTeacher") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#AttendanceNav").css({ backgroundColor: "#1D8AB5" });
        }

        if (sessionStorage.getItem("Attendance") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#AttendanceNav").css({ backgroundColor: "#1D8AB5" });
        }
        if (sessionStorage.getItem("ResultPage") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ResultsNav").css({ backgroundColor: "#1D8AB5" });
        }
        if (sessionStorage.getItem("Result") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ResultsNav").css({ backgroundColor: "#1D8AB5" });
        }
        if (sessionStorage.getItem("FinalViewResult") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ResultsNav").css({ backgroundColor: "#1D8AB5" });
        }

        if (sessionStorage.getItem("chairmanprofile") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ProfileNav").css({ backgroundColor: "#1D8AB5" });
        }

        if (sessionStorage.getItem("createTT") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#TimeTableNav").css({ backgroundColor: "#1D8AB5" });
        }
        if (sessionStorage.getItem("AttToAdmin") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#AttendanceNav").css({ backgroundColor: "#1D8AB5" });
        }

        if (sessionStorage.getItem("ClassPageCP") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ResultsNav").css({ backgroundColor: "#1D8AB5" });
        }
        if (sessionStorage.getItem("CourseList") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ResultsNav").css({ backgroundColor: "#1D8AB5" });
        }
        if (sessionStorage.getItem("ResultViewCP") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ResultsNav").css({ backgroundColor: "#1D8AB5" });
        }
        if (sessionStorage.getItem("classProformaCP") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ResultsNav").css({ backgroundColor: "#1D8AB5" });
        }
        if (sessionStorage.getItem("AttendanceSession") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#AttendanceNav").css({ backgroundColor: "#1D8AB5" });
        }

        if (sessionStorage.getItem("ResultSession") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ResultsNav").css({ backgroundColor: "#1D8AB5" });
        }
        sessionStorage.removeItem('button');
        sessionStorage.removeItem('color');
        sessionStorage.removeItem('bgcolor');
        sessionStorage.removeItem('font');
        sessionStorage.removeItem("test1");
        sessionStorage.removeItem("teacherprofile");
        sessionStorage.removeItem("teachertt");
        sessionStorage.removeItem("AttendancePage");
        sessionStorage.removeItem("AttToTeacher");
        sessionStorage.removeItem("Attendance");
        sessionStorage.removeItem("ResultPage");
        sessionStorage.removeItem("Result");
        sessionStorage.removeItem("FinalViewResult");
        sessionStorage.removeItem("chairmanprofile");
        sessionStorage.removeItem("createTT");
        sessionStorage.removeItem("AttToAdmin");
        sessionStorage.removeItem("ClassPageCP");
        sessionStorage.removeItem("CourseList");
        sessionStorage.removeItem("ResultViewCP");
        sessionStorage.removeItem("classProformaCP");
        sessionStorage.removeItem("AttendanceSession");
        sessionStorage.removeItem("ResultSession");

        $("#LogoutNav").on("click", function () {
            sessionStorage.removeItem("Chairperson");
        });

    </script>
</body>
</html>
