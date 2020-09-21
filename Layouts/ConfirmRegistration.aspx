<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmRegistration.aspx.cs" Inherits="UokSemesterSystem.ConfirmRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title></title>
    <style>
        #ProfilePic, #Image1 {
       
    height: 13vw;
    width: 13vw;
    background-color: #fcfcfc;
    object-fit: cover;
    border: 0.15vw solid #1D8AB5;
        }

td {
    padding: 0.6vw;
    font-size: 1.2vw;
    text-align: left;
}

.boldtext {
    font-weight: 600;
}

.head, .picturehead { 
    font-size: 1.5vw;
    color: #fff;
    margin: 0; padding: 0.4vw;
    font-family: Calibri;
}

.picturehead { font-size: 1.3vw; color: #4b4b4b; margin: 0; padding: 0; margin-left: 2vw; }

#ConfirmTech, #backbtn {
            width: 15vw;
            background-color: #1D8AB5;
            color: #fff;
            text-align: center;
            outline: none;
            border: none;
            font-size: 1.3vw;
            font-family: Calibri;
            cursor: pointer;
            height: 2.5vw;
            font-weight: 600;
            margin-top: 2%;
        }
#backbtn { margin-right: 0.5vw; width: 8vw; background-color: #4b4b4b; }
        #ConfirmTech:hover { background-color: #64C5EB; }
        #backbtn:hover { background-color: #6b6b6b; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Create" style="float:left; width: 15%;"></div>
        <div style="float: left; width: 85%; height: 100vh; overflow:hidden">
        <div>
             <div id="StdView" runat="server">
            <div id="Std" runat="server" style="width: 90%; margin: 0 auto; margin-top: 3%;">
                <div style="width: 100%; background-color: #1D8AB5; margin: 0 auto; padding: 0; text-align: center; margin-bottom: 2%;">
                    <h1 class="head">Student's Information</h1>
                </div>
                
                <div style="float: left; width: 20%; ">
                    <asp:Image ID="ProfilePic" runat="server" style="text-align: center" ></asp:Image>
                    <h1 class="picturehead">Student's Picture</h1>
                </div>
                <div style="float: left; width: 80%; ">

                
                

                 <table cellspacing="0" style="width: 100%; margin: 0 auto;  ">
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
                        <td style="width: 30%;"><span class="boldtext">Roll Number: </span></td>
                        <td style="width: 70%;"><label id="rolnum" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Department: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="department" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%;"><span class="boldtext">Major: </span></td>
                        <td style="width: 70%;" ><label id="major" runat="server"></label></td>
                    </tr>
                      <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Section: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="section" runat="server"></label></td>
                    </tr>

                     <tr>
                        <td style="width: 30%;"><span class="boldtext">Year Enrolled: </span></td>
                        <td style="width: 70%;"><label id="year" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Email Address: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="email" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%;"><span class="boldtext">Semester: </span></td>
                        <td style="width: 70%;"><label id="semester" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Shift: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="shift" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%;"><span class="boldtext">Username: </span></td>
                        <td style="width: 70%;" ><label id="uname" runat="server"></label></td>
                    </tr>

                      <tr>
                        <td style="width: 30%; background-color: #f0f0f0"" ><span class="boldtext">Password: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"" ><label id="password" runat="server"></label></td>
                    </tr>

               
            </table>
               </div>
            </div>
        </div>

            <!------------ teacher view -------------->
            <div id="TechView" runat="server">
            <div id="Div3" runat="server" style="width: 90%; margin: 0 auto; margin-top: 3%;">
                <div style="width: 100%; background-color: #1D8AB5; margin: 0 auto; padding: 0; text-align: center; margin-bottom: 2%;">
                    <h1 class="head">Teacher's Information</h1>
                </div>
                
                <div style="float: left; width: 20%; ">
                    <asp:Image ID="Image1" runat="server" style="text-align: center" ></asp:Image>
                    <h1 class="picturehead">Teacher's Picture</h1>
                </div>
                <div style="float: left; width: 80%; ">

                
                

                 <table cellspacing="0" style="width: 100%; margin: 0 auto;  ">
                     <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Teacher's Name: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="Label1" runat="server"></label></td>
                    </tr>

                     <tr>
                        <td style="width: 30%;"><span class="boldtext">Department: </span></td>
                        <td style="width: 70%;"><label id="Label2" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Contact: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="Label3" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%;"><span class="boldtext">Email Address: </span></td>
                        <td style="width: 70%;"><label id="Label4" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Degree: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="Label5" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%;"><span class="boldtext">Username: </span></td>
                        <td style="width: 70%;" ><label id="Label12" runat="server"></label></td>
                    </tr>
                      <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Password: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="Label13" runat="server"></label></td>
                    </tr>

            </table>
               </div>
            </div>
        </div>

            <div style="width: 90%; text-align: right; margin: 0 auto;">
                <asp:Button ID="backbtn" runat="server" Text="Back" OnClick="go_back"/>
                <asp:Button ID="ConfirmTech" runat="server"  Text="Confirm Registration" OnClick="BtnLogin_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
            </div>
             
            <button id="confirmregistration" type="button" style="display: none;"></button>
        </div>
            </div>
    </form>

     <script>
    $(function () {
        $("#Nav-Create").load("AdminNavBar.aspx");
    });

         $(document).ready(function () {
             document.getElementById("confirmregistration").click();
         });

         $("#confirmregistration").on("click", function () {
             sessionStorage.setItem("Confirm", "loaded");
         });
         </script>
</body>
</html>
