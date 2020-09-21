<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChairPersonTT.aspx.cs" Inherits="UokSemesterSystem.ChairPersonTT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="~/css/TeacherTT.css" />
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>

    <style>
        .backcell { padding: 0.5vw; }
        .CTable { width: 90%; text-align: center; }
        .cell8, .cell10, .cell12, .cell15, .cell33 { 
            background-color: #D1E8F1;
            padding: 0.5vw;
        }

        .tcell {  }
        .tsrow { border: none; }
        .cell8 { width: 8%; }
        .cell10 { width: 10%; }
        .cell33 { width: 30%; }
        .cell12 { width: 12%; }
        .cell15 { width: 16%; }
        .tablerow { border: 0.1vw solid #cccccc;  }
        .headingtop {
    margin: 0 auto;
    width: 70%;
    text-align: center;
    margin-top: 5%;
    margin-bottom: 3%;
    border-bottom: 0.1vw solid #a9a9a9;
    padding: 0;
}

    .headingtop h1 {
        font-size: 1.5vw;
        text-align: center;
        margin: 0 auto;
        padding: 0.2vw;
        font-family: Calibri;
        font-weight: 600;
        background-color: #1D8AB5;
        width: 60%;
        height: 100%;
    }

    .Selection {
    width: 70%;
    border-bottom: 0.1vw solid #a9a9a9;
    margin: 0 auto;
    margin-top: 7%;
    text-align: center;
}

.SelectionCSS{
    border: none;
    background-color: #64C5EB;
    color: #fff;
    font-size: 1.2vw;
    font-weight: 700;
    text-align: center;
    width: 15vw;
    height: 2.5vw;
    cursor: pointer;
    outline: none;
}

.SelectionCSS:hover {
    background-color: #1D8AB5 !important;
}

.test3 {
    border: 0.1vw solid white;
    float: left;
    width: 15.5vw;
}
    </style>
</head>
<body>
      <form id="form1" runat="server">

          <!--------------- Navigation Bar -------------->
        <div id="Nav-TimeTable" style="float:left; width: 15%;"></div>
        <div id="Nav-Admin" style="float: left; width: 15%;"></div>

        <div style="float: left; width: 85%; height: 100vh; overflow-y:scroll; overflow-x: hidden;">
            <div class="Selection">
              <button id="MorningBTN" class="SelectionCSS" type="button" onclick="DisplayMorningTT()">Morning Schedule</button>
              <button id="EveningBTN" class="SelectionCSS" type="button" onclick="DisplayEveningTT()">Evening Schedule</button>
          </div>
            
            <div id="MorningDIV">
            <asp:Table ID="TeacherCoursesMorning" runat="server" CssClass="CTable">
                <asp:TableRow CssClass="tsrow">
                    <asp:TableCell ColumnSpan="8">
                        <h2 style="float: left;">MORNING SCHEDULE</h2>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow CssClass="tablerow">
                    <asp:TableCell CssClass="cell10"><b>DAY</b></asp:TableCell>
                    <asp:TableCell CssClass="cell15"><b>TEACHER</b></asp:TableCell>
                    <asp:TableCell CssClass="cell8"><b>CLASS</b></asp:TableCell>
                    <asp:TableCell CssClass="cell8"><b>SECTION</b></asp:TableCell>
                    <asp:TableCell CssClass="cell8"><b>COURSE #</b></asp:TableCell>
                    <asp:TableCell CssClass="cell33"><b>COURSE TITLE</b></asp:TableCell>
                    <asp:TableCell CssClass="cell12"><b>TIME</b></asp:TableCell>
                    <asp:TableCell CssClass="cell8" style="border-right: 0.1vw solid #cccccc;"><b>ROOM #</b></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br /><br /><br /><br /><br /><br />
            </div>

            <div id="EveningDIV" style="display: none;">
            <asp:Table ID="TeacherCoursesEvening" runat="server" CssClass="CTable">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="8">
                        <h2 style="float: left;">EVENING SCHEDULE</h2>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow CssClass="tablerow">
                    <asp:TableCell CssClass="cell8"><b>DAY</b></asp:TableCell>
                    <asp:TableCell CssClass="cell15"><b>TEACHER</b></asp:TableCell>
                    <asp:TableCell CssClass="cell8"><b>CLASS</b></asp:TableCell>
                    <asp:TableCell CssClass="cell8"><b>SECTION</b></asp:TableCell>
                    <asp:TableCell CssClass="cell8"><b>COURSE #</b></asp:TableCell>
                    <asp:TableCell CssClass="cell33"><b>COURSE TITLE</b></asp:TableCell>
                    <asp:TableCell CssClass="cell12"><b>TIME</b></asp:TableCell>
                    <asp:TableCell CssClass="cell8" style="border-right: 0.1vw solid #cccccc;"><b>ROOM #</b></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
             <button id="HiddenBTN1" type="button" style="display: none;"></button>
            <br /><br /><br /><br /><br /><br />
            </div>
        </div>
    </form>

    <script>
        $(function () {
            if (sessionStorage.getItem("Admin") === "Profile") {
                $("#Nav-Admin").load("AdminNavBar.aspx");
            }
            else
                $("#Nav-TimeTable").load("NavigationBar.aspx");
        });

        $(document).ready(function () {
            document.getElementById("HiddenBTN1").click();
            document.getElementById("MorningBTN").style.backgroundColor = "#1D8AB5";
        });

        $("#HiddenBTN1").on("click", function () {
            sessionStorage.setItem('ChairPersonTT', 'loaded');
        });


        var morning = document.getElementById("MorningDIV");
        var evening = document.getElementById("EveningDIV");

        function DisplayMorningTT() {
            if (morning.style.display == "none") {
                morning.style.display = "block";
                document.getElementById("MorningBTN").style.backgroundColor = "#1D8AB5";
                evening.style.display = "none";
                document.getElementById("EveningBTN").style.backgroundColor = "#64C5EB";


            } else {
                morning.style.display = "block";
                document.getElementById("MorningBTN").style.backgroundColor = "#1D8AB5";
                evening.style.display = "none";
                document.getElementById("EveningBTN").style.backgroundColor = "#64C5EB";

            }

        }

        function DisplayEveningTT() {
            if (evening.style.display == "none") {
                evening.style.display = "block";
                document.getElementById("EveningBTN").style.backgroundColor = "#1D8AB5";
                morning.style.display = "none";
                document.getElementById("MorningBTN").style.backgroundColor = "#64C5EB";

            }
            else {
                evening.style.display = "block";
                document.getElementById("EveningBTN").style.backgroundColor = "#1D8AB5";
                morning.style.display = "none";
                document.getElementById("MorningBTN").style.backgroundColor = "#64C5EB";

            }
        }
    </script>
</body>
</html>
