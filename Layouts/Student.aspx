<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="UokSemesterSystem.Student" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="~/css/StudentProfile.css" />
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title>Student Profile | UOK</title>
</head>

<body>
    <form id="form1" runat="server">
        <!--------------- Navigation Bar -------------->
        <div id="Nav-Profile" style="float:left; width: 15%;"></div>

        <!--------------- Right Panel -------------->
        <div class="content">
            <div class="Selection">
                <div class="test2"><button id="OverviewBTN" class="SelectionCSS" type="button" onclick="DisplayOverview()">Profile Overview</button></div>
                <div class="test2"><button id="GeneralBTN" class="SelectionCSS" type="button" onclick="DisplayBasicInfo()">Student Information</button></div>
            </div>

        <!--------------- Tab 01: Profile Overview -------------->
            <div id="OverviewDIV" runat="server">
                <asp:Image ID="ProfilePic" runat="server"></asp:Image>
                <p runat="server" id="welcomename"></p>
                <div class="useremail">
                    <p><b>username: </b><span runat="server" id="username1"></span></p>
                    <p><b>email: </b><span runat="server" id="email1"></span></p>
                    <button id="HiddenBTN1" type="button" style="display: none;"></button>
                </div>
            </div>
        
        <!--------------- Tab 02: Student Information -------------->
            <div id="GeneralInfoDIV" runat="server">
                <h1>Student's Basic Information</h1>
                <table cellspacing="0">
                     <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Student's Name: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="name" runat="server"></label></td>
                    </tr>

                     <tr>
                        <td style="width: 30%;"><span class="boldtext">Father's Name: </span></td>
                        <td style="width: 70%;"><label id="fname" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Enrollment Number: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="enrol" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%;"><span class="boldtext">Year Enrolled: </span></td>
                        <td style="width: 70%;"><label id="yearenrolled" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Department: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="depart" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%;"><span class="boldtext">Seat Number: </span></td>
                        <td style="width: 70%;" ><label id="rolno" runat="server"></label></td>
                    </tr>
                </table>

                <h1>Student's Class Information</h1>
                <table cellspacing="0">
                    <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Class: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="classCI" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%;"><span class="boldtext">Current Semester: </span></td>
                        <td style="width: 70%;"><label id="semesterCI" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Class Section: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="sectionCI" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%;"><span class="boldtext">Shift: </span></td>
                        <td style="width: 70%;" ><label id="shiftCI" runat="server"></label></td>
                    </tr>
                </table>
            </div>
            
        </div>
    </form>

    <script>
        $(function () {
            $("#Nav-Profile").load("NavigationBar.aspx");

        });

        $(document).ready(function () {
            document.getElementById("OverviewBTN").style.backgroundColor = "#1D8AB5";
            document.getElementById("HiddenBTN1").click();
        });

        $("#HiddenBTN1").on("click", function () {
            sessionStorage.setItem('button', 'clicked');
        });

        var overview = document.getElementById("OverviewDIV");
        var BasicInfo = document.getElementById("GeneralInfoDIV");

        function DisplayOverview() {
            if (overview.style.display == "none") {
                overview.style.display = "block";
                document.getElementById("OverviewBTN").style.backgroundColor = "#1D8AB5";
                BasicInfo.style.display = "none";
                document.getElementById("GeneralBTN").style.backgroundColor = "#64C5EB";
    

            } else {
                overview.style.display = "block";
                document.getElementById("OverviewBTN").style.backgroundColor = "#1D8AB5";
                BasicInfo.style.display = "none";
                document.getElementById("GeneralBTN").style.backgroundColor = "#64C5EB";
   
            }

        }

        function DisplayBasicInfo() {
            if (BasicInfo.style.display == "none") {
                BasicInfo.style.display = "block";
                document.getElementById("GeneralBTN").style.backgroundColor = "#1D8AB5";
                overview.style.display = "none";
                document.getElementById("OverviewBTN").style.backgroundColor = "#64C5EB";
       
            }
            else {
                BasicInfo.style.display = "block";
                document.getElementById("GeneralBTN").style.backgroundColor = "#1D8AB5";
                overview.style.display = "none";
                document.getElementById("OverviewBTN").style.backgroundColor = "#64C5EB";

            }
        }
    </script>
</body>
</html>
