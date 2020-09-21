<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseList.aspx.cs" Inherits="UokSemesterSystem.CourseList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title></title>

    <style>
        .head { 
            margin: 0 auto;
    width: 70%;
    text-align: center;
    margin-top: 7%;
    border-bottom: 0.1vw solid #A9A9A9;
        }

        .head h1 { 
            font-size: 1.7vw;
            margin: 0.5vw;
            font-family: Calibri;
            font-weight: 700;
        }

        .TableCSS { 
            width: 100%;
            margin: 0 auto;
            margin-top: 1vw;
            text-align: center;
            font-size: 1.1vw;
        }

        .head1, .head2 { font-weight: 600; width: 60%; border: 0.1vw solid #cccccc; font-size: 1.2vw;}
        .backcell { border: 0.1vw solid #cccccc; }
        .head2 { width: 20%; }

       
        .backcellbutton, #backbtn {
            border: none;
            outline: none;
            cursor: pointer;
            background-color: #64C5EB;
            color: #fff;
            font-weight: 600;
            font-family: Calibri;
            width: 10vw;
            height: 2.3vw;
            margin: 0.1vw;
            margin-left: 0.5vw;
        }

        #backbtn { background-color: #4b4b4b; margin-right: 0.3vw; margin-top: 3vw;}
        #backbtn:hover { background-color: #6b6b6b; }
        .backcellbutton { height: 2vw; }
        .backcellbutton:hover { background-color: #1D8AB5;}

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

    .labelhead { 
        color: black;
        font-size: 1.5vw;
        font-family: Calibri;
        font-weight: 600;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Result" style="float:left; width: 15%;"></div>
        <div style="float: left; width: 85%; height: 100vh; overflow-y:scroll; overflow-x: hidden;">

            <div class="headingtop">
            <h1>
                <span style="color: #fff;">Courses List</span>
            </h1>
            </div>

            <div style="width: 80%; margin: 0 auto; text-align: center;">
            <asp:Label ID="msgLbl" runat="server" Text="No Results forwarded." Visible="False"></asp:Label>
            </div>
            <div id="unAppDiv" runat="server" style="width: 70%; margin: 0 auto;">
            <asp:Label ID="Label2" runat="server" Text="Results To be Approved" CssClass="labelhead"></asp:Label>
        <asp:Table ID="coursesTable" CssClass="TableCSS" runat="server" CellSpacing="0">
            <asp:TableRow runat="server" BackColor="#D1E8F1">
                <asp:TableCell runat="server" CssClass="head1">Course Name</asp:TableCell>
                <asp:TableCell runat="server" CssClass="head2">Course No.</asp:TableCell>
                <asp:TableCell runat="server" CssClass="head2">Semester</asp:TableCell>
                
            </asp:TableRow>
        </asp:Table>
                 </div>
            <br />
        <div id="appDiv" runat="server" style="width: 70%; margin: 0 auto;">
          
        <asp:Label ID="Label1" runat="server" Text="Approved Results" CssClass="labelhead"></asp:Label>
        <asp:Table ID="coursesTable1"  CssClass="TableCSS" runat="server" CellSpacing="0">
            <asp:TableRow runat="server" BackColor="#D1E8F1">
                <asp:TableCell runat="server" CssClass="head1">Course Name</asp:TableCell>
                <asp:TableCell runat="server" CssClass="head2">Course No.</asp:TableCell>
                <asp:TableCell runat="server" CssClass="head2">Semester</asp:TableCell>
                
            </asp:TableRow>
        </asp:Table>
               <button id="HiddenBTN1" type="button" style="display: none;"></button>
        </div>
        </div>
    </form>

     <script>
    $(function () {
        $("#Nav-Result").load("NavigationBar.aspx");
    });

         $(document).ready(function () {
             document.getElementById("HiddenBTN1").click();
         });

         $("#HiddenBTN1").on("click", function () {
             sessionStorage.setItem("CourseList", "loaded");
         });

        </script>
</body>
</html>
