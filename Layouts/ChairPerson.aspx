<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChairPerson.aspx.cs" Inherits="UokSemesterSystem.ChairPerson" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="~/css/TeacherProfile.css" />
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title></title>

    <style>
    </style>
</head>

<body>
    <form id="form2" runat="server">
            <div id="Nav-Profile" style="float:left; width: 15%;"></div>
        <div class="content">
          <div class="Selection">
              <button id="chairmanprofile" type="button" style="display: none;"></button>
              <div class="test3"><button id="OverviewBTN" class="SelectionCSS" type="button" onclick="DisplayOverview()">Profile Overview</button></div>
              <div class="test3"><button id="GeneralBTN" class="SelectionCSS" type="button" onclick="DisplayBasicInfo()">Chairman Information</button></div>
          </div>

            <div id="OverviewDIV" style="text-align: center; height: 90vh; width: 80%; margin: 0 auto; display: block; border-top: 0.1vw solid #A9A9A9;" runat="server">
              <asp:Image ID="ProfilePic" runat="server"></asp:Image>
              <p style="color: #4B4B4B; font-size: 1.2vw;" runat="server" id="welcomename2">chairperson</p>
              <div class="useremail" style=" border-left: 0.13vw solid #64C5EB; border-right: 0.13vw solid #64C5EB; width: auto; display: table; padding: 0vw 1vw 0vw 1vw; margin: 0 auto; ">
                <p style="color: #4B4B4B; font-size: 1.2vw;"><b>username: </b><span runat="server" id="username2">@chairperson</span></p>
                <p style="color: #4B4B4B; font-size: 1.2vw;"><b>email: </b><span runat="server" id="email2">chairman@gmail.com</span></p>
              </div>
          </div>


            <div id="GeneralInfoDIV" runat="server">
               
                <h1>Chairperson's Information</h1>
                <table cellspacing="0">
                     <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Chairperson Name: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="name" runat="server" class="adjustment"></label></td>
                    </tr>

                     <tr>
                        <td style="width: 30%;"><span class="boldtext">Department: </span></td>
                        <td style="width: 70%;"><label id="depart" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Contact Number: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="contact" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%;"><span class="boldtext">Email: </span></td>
                        <td style="width: 70%;"><label id="email" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Degree: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="degree" runat="server"></label></td>
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
            document.getElementById("chairmanprofile").click();
            sessionStorage.setItem("Chairperson", "Profile");
        });

        $("#chairmanprofile").on("click", function () {
            sessionStorage.setItem("chairmanprofile", "loaded");
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
