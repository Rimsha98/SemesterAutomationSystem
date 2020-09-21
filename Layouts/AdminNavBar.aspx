<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminNavBar.aspx.cs" Inherits="UokSemesterSystem.AdminNavBar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title></title>
    <style>
        * { box-sizing: border-box; }
body {
    padding: 0;
    margin: 0;
    font-family: 'Segoe UI';
    overflow: hidden;
}
        .Navbar{
            background-color:#4b4b4b;
            height: 100vh;
            text-align: center;
        }
        .navButton {
            position: fixed;
            height: 3.5vw;
            width: 15%;
            font-size: 1.2vw;
            color: #fff;
            background-color: #4b4b4b;
            border: none;
            cursor: pointer;
            padding: 1.1vw 4vw 1.1vw 4vw;
            text-align: left;
            outline: none;
            
        }
        #ProfileNav { background: url('../appImages/profile.png'); background-position: left 1vw center;
            background-repeat: no-repeat;
            background-size: 2.7vw 2.7vw;}
        #TimeTableNav { background: url('../appImages/time.png'); background-position: left 1.6vw center;
            background-repeat: no-repeat;
            background-size: 1.4vw 1.4vw;}
        #AttendanceNav { background: url('../appImages/attendance.png'); background-position: left 1.3vw center;
            background-repeat: no-repeat;
            background-size: 1.9vw 1.9vw;}
        #ResultsNav { background: url('../appImages/results.png'); background-position: left 1.3vw center;
            background-repeat: no-repeat;
            background-size: 1.9vw 1.9vw;}
        #SettingsNav { background: url('../appImages/settings.png'); background-position: left 1.4vw center;
            background-repeat: no-repeat;
            background-size: 1.7vw 1.7vw;}
        #LogoutNav { background: url('../appImages/logout.png'); background-position: left 1.3vw center;
            background-repeat: no-repeat;
            background-size: 1.9vw 1.9vw;
        }
        #CreateNav { background: url('../appImages/add.png'); background-position: left 1.5vw center;
            background-repeat: no-repeat;
            background-size: 1.6vw 1.6vw;
        }
        #InsertNav { background: url('../appImages/pencil.png'); background-position: left 1.5vw center;
            background-repeat: no-repeat;
            background-size: 1.6vw 1.6vw;
        }
        #EditNav { background: url('../appImages/profile.png'); background-position: left 1vw center;
            background-repeat: no-repeat;
            background-size: 2.7vw 2.7vw;
        }
            .navButton:hover, #ProfileNav:hover, #TimeTableNav:hover, #AttendanceNav:hover,
            #ResultsNav:hover, #SettingsNav:hover, #LogoutNav:hover, #CreateNav:hover, #EditNav:hover {
                background-color: #1D8AB5 !important;
            }

        .ProfilePic {
            background-color: #4b4b4b;
            width: 100%;
            height: 20vh;
        }
        #ProfilePicNav {
            border-radius: 50%;
            height: 6vw;
            width: 6vw;
            background-color: #fcfcfc;
            object-fit: cover;
            margin-top: 1.2vw;
        }
        .test {
            margin-top: 3.4vw;
            height: 0.1vw;
        }
        .studentnamenav {
            width: 10vw;
            max-width: 10vw;
            font-size: 1.3vw;
            font-weight: bold;
            color: #fff;
            margin: 0 auto;
            text-transform: uppercase;
        }

        h1 { font-size: 1.2vw; color: #fff; font-family: 'Segoe UI'; margin-top: 0;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Navbar">
            <div class="ProfilePic" style="text-align: center;">
                <asp:Image ID="ProfilePicNav" runat="server" ImageUrl="~/Img/admin.jpg"></asp:Image><br />
                <h1 class="studentnamenav" runat="server" id="namenav">ADMINISTRATOR</h1>
            </div>
            <div style="height: 80vh; text-align: left; ">
                <div style="margin-top: 0.5vw; height: 0.1vw;"></div>
            <asp:Button ID="ProfileNav" class="navButton" runat="server" Text="Admin Profile" OnClick="BtnProfile_Click"/>
            <div class="test"></div>
                <asp:Button ID="InsertNav" class="navButton" runat="server" Text="Student Records" OnClick="BtnInsert_Click"/>
            <div class="test"></div>
                <asp:Button ID="EditNav" class="navButton" runat="server" Text="Teacher Records" OnClick="BtnEdit_Click"/>
            <div class="test"></div>
                <asp:Button ID="CreateNav" class="navButton" runat="server" Text="Create Accounts" OnClick="BtnCreate_Click"/>
            <div class="test"></div>
            <asp:Button ID="TimeTableNav" class="navButton" runat="server" Text="Time Table" OnClick="BtnTimeTable_Click"  />
                <div class="test"></div>
            <asp:Button ID="ResultsNav" class="navButton" runat="server" Text="Results" OnClick="BtnResults_Click"/>
                <div class="test"></div>
            <asp:Button ID="SettingsNav" class="navButton" runat="server" Text="Account Settings" OnClick="BtnSettings_Click" />
                <div class="test"></div>
            <asp:Button ID="LogoutNav" class="navButton" runat="server" Text="Logout" OnClick="BtnLogout_Click"/>
                </div>
            <button id="bb" style="display: none" type="button"></button>
        </div>
        
    </form>

    <script>
        $("#LogoutNav").on("click", function () {
            sessionStorage.removeItem("Admin");
        });

        if (sessionStorage.getItem("Admin") === "Profile") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ProfileNav").css({ backgroundColor: "#1D8AB5" });
        }
        sessionStorage.removeItem("AdminProfile");

        if (sessionStorage.getItem("AdminTT") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#TimeTableNav").css({ backgroundColor: "#1D8AB5" });
        }
        sessionStorage.removeItem("AdminTT");

        if (sessionStorage.getItem("CreateAccount") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#CreateNav").css({ backgroundColor: "#1D8AB5" });
        }
        sessionStorage.removeItem("CreateAccount");

        if (sessionStorage.getItem("StudentReg") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#InsertNav").css({ backgroundColor: "#1D8AB5" });
        }
        sessionStorage.removeItem("StudentReg");

        if (sessionStorage.getItem("TeacherReg") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#InsertNav").css({ backgroundColor: "#1D8AB5" });
        }
        sessionStorage.removeItem("TeacherReg");

        if (sessionStorage.getItem("ChairPersonTT") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#TimeTableNav").css({ backgroundColor: "#1D8AB5" });
        }
        sessionStorage.removeItem("ChairPersonTT");



        if (sessionStorage.getItem("CPViewResult") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ResultsNav").css({ backgroundColor: "#1D8AB5" });
        }
        sessionStorage.removeItem("CPViewResult");

        if (sessionStorage.getItem("AdminResult") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ResultsNav").css({ backgroundColor: "#1D8AB5" });
        }
        sessionStorage.removeItem("AdminResult");

        if (sessionStorage.getItem("IndvProforma") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ResultsNav").css({ backgroundColor: "#1D8AB5" });
        }
        sessionStorage.removeItem("IndvProforma");

        if (sessionStorage.getItem("classProformaAdmin") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#ResultsNav").css({ backgroundColor: "#1D8AB5" });
        }
        sessionStorage.removeItem("classProformaAdmin");

        if (sessionStorage.getItem("AdminSettings") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#SettingsNav").css({ backgroundColor: "#1D8AB5" });
        }
        sessionStorage.removeItem("AdminSettings");

        if (sessionStorage.getItem("TeacherRecords") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#EditNav").css({ backgroundColor: "#1D8AB5" });
        }
        sessionStorage.removeItem("TeacherRecords");

        if (sessionStorage.getItem("StudentRecords") === "loaded") {
            $(".navButton").css({ backgroundColor: "#4b4b4b" });
            $("#InsertNav").css({ backgroundColor: "#1D8AB5" });
        }
        sessionStorage.removeItem("StudentRecords");

        $("#InsertNav").on("click", function () {
            if (localStorage.getItem("hasCodeRunBefore") === null) {
                /** Your code here. **/
                localStorage.setItem("hasCodeRunBefore", true);
                localStorage.setItem('isSnameError', true);
                localStorage.setItem('isFnameError', true);
                localStorage.setItem('isEnumError', true);
                localStorage.setItem('isRnumError', true);
                localStorage.setItem('isYearError', true);
                localStorage.setItem('isEmailError', true);

                localStorage.setItem("hasTeacherCodeRunBefore", true);
                localStorage.setItem('isTnameError', true);
                localStorage.setItem('isContactError', true);
                localStorage.setItem('isDegreeError', true);
                localStorage.setItem('isTemailError', true);
            }
            else {
                localStorage.setItem("hasCodeRunBefore", false);
                localStorage.setItem("hasTeacherCodeRunBefore", false);
            }
        });


        $("#EditNav").on("click", function () {
            if (localStorage.getItem("hasCodeRunBefore") === null) {
                /** Your code here. **/
                localStorage.setItem("hasCodeRunBefore", true);
                localStorage.setItem('isSnameError', true);
                localStorage.setItem('isFnameError', true);
                localStorage.setItem('isEnumError', true);
                localStorage.setItem('isRnumError', true);
                localStorage.setItem('isYearError', true);
                localStorage.setItem('isEmailError', true);

                localStorage.setItem("hasTeacherCodeRunBefore", true);
                localStorage.setItem('isTnameError', true);
                localStorage.setItem('isContactError', true);
                localStorage.setItem('isDegreeError', true);
                localStorage.setItem('isTemailError', true);
            }
            else {
                localStorage.setItem("hasCodeRunBefore", false);
                localStorage.setItem("hasTeacherCodeRunBefore", false);
            }
        });

        localStorage.removeItem("hasCodeRunBefore");
        localStorage.removeItem("hasTeacherCodeRunBefore");
    </script>
</body>
</html>
