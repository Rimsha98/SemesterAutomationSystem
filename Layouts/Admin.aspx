<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="UokSemesterSystem.Admin" EnableEventValidation="false"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title></title>

    <style>
        * {
    box-sizing: border-box;
}

body {
    margin: 0;
    padding: 0;
    overflow: hidden;
}

#ProfilePic {
    border-radius: 50%;
    height: 18vw;
    width: 18vw;
    background-color: #fcfcfc;
    object-fit: cover;
    border: 0.3vw solid #64C5EB;
    margin-top: 10%;
}


.content{ text-align: center; }

#websitelink {
    font-size: 1.3vw;
    color: #4b4b4b;
}
#websitelink:hover { color: #1D8AB5;}

#updateSem { 
    width: 15vw;
    font-size: 1.2vw;
    font-weight: bold;
    background-color: #64C5EB;
    border: none;
    outline: none;
    color: #fff;
    cursor: pointer;
    height: 2.5vw;
}
#updateSem:hover {background-color: #1D8AB5;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Profile" style="float:left; width: 15%;"></div>
        <div style="float: left; width: 85%; height: 100vh; overflow:hidden;">

       <div class="content">
            <div style=" text-align: center; height: 100vh">
                 <asp:Image ID="ProfilePic" runat="server" style="text-align: center;" ></asp:Image>
                 <h1 style=" font-size: 2vw; color: #64C5EB; font-family: Calibri; margin-top: 2vw; font-weight: 600; ">UNIVERSITY OF KARACHI</h1>
                <h2 style="margin: 0; padding: 0; margin-top: -2vw; font-weight: 600; ">ADMINISTRATION</h2>
                <a id="websitelink" href="http://www.uok.edu.pk/" target="_blank">uok.edu.pk</a>
                <br /><br />
                <asp:Button ID="updateSem" runat="server" OnClick="updateSem_Click" Text="Update Semester" />
            <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
           </div>
            </div>

            
            <button id="adminprofile" type="button" style="display: none;"></button>
            </div>
    </form>

     <script>
    $(function () {
        $("#Nav-Profile").load("AdminNavBar.aspx");
    });

         $(document).ready(function () {
             document.getElementById("adminprofile").click();
             sessionStorage.setItem("Admin", "Profile");

         });

         $("#adminprofile").on("click", function () {
             sessionStorage.setItem("AdminProfile", "loaded");
         });
         </script>
</body>
</html>
