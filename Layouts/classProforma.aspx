<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="classProforma.aspx.cs" Inherits="UokSemesterSystem.classProforma" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <style>
        * { box-sizing: border-box; }
body {
    padding: 0;
    margin: 0;
    font-family: 'Times New Roman';
}
        .pagecolor { background-color: #FFFFCC;}
        #ProformaDIV { transform : scale(0.9); transform-origin: top center;}

        #backbtn { float: left;}

        .Buttons {
            border: none;
            outline: none;
            cursor: pointer;
            background-color: #64C5EB;
            color: #fff;
            font-weight: 600;
            font-family: Calibri;
            width: 12vw;
            height: 2vw;
            margin-left: 7vw;
        }

        #download { margin-left:0.5vw;}

        .Buttons:hover { background-color: #1D8AB5;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Proforma" style="float:left; width: 15%;"></div>

          
   
          
        
        <div style =" overflow-y: scroll; overflow-x: hidden; width: 85%; height: 100vh;">
            <div style="width: 90%; text-align: right; margin: 0 auto; margin-top: 3%; margin-bottom: 3%; ">
                <asp:Label ID="Label1" runat="server" style="color: #660066" Text="Approved" Visible="False"></asp:Label>
                <asp:Button ID="appBtn"  CssClass="Buttons" runat="server" Text="Approve" OnClick="app_Click" />
                <asp:Button ID="download" CssClass="Buttons" runat="server" Text="Download" OnClick="DownloadPdf_Click" />
             <!--   <asp:Button ID="backbtn"  CssClass="Buttons" runat="server" Text="Back" OnClick="backbtn_Click" /> -->
            </div>
            
            
     
        <div id="ProformaDIV" runat="server" style="width: 100%; margin: 0 auto; ">
            
        

        <div id="div1" runat="server" style="width: 100%; margin: 0 auto; text-align: center; border: 0.1vw solid #cccccc; background-color: #FFFFCC;">
        </div>
             <button id="HiddenBTN1" type="button" style="display: none;"></button>
            </div></div>
       </form>
    <script>
        $(function () {
            if (sessionStorage.getItem("Chairperson") === "Profile")
                $("#Nav-Proforma").load("NavigationBar.aspx");
            else
                $("#Nav-Proforma").load("AdminNavBar.aspx");
        });

        $(document).ready(function () {
            document.getElementById("HiddenBTN1").click();
        });

        $("#HiddenBTN1").on("click", function () {
            sessionStorage.setItem('classProformaCP', 'loaded');
            sessionStorage.setItem("classProformaAdmin", "loaded");
        });
    </script>
</body>
</html>
