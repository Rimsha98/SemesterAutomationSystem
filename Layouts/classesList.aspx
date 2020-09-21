<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="classesList.aspx.cs" Inherits="UokSemesterSystem.classesList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title></title>
    <script type="text/javascript">
        function AttendanceSession() {
            sessionStorage.setItem("AttendanceSession", "loaded");
        }

        function ResultSession() {
            sessionStorage.setItem("ResultSession", "loaded");
        }
    </script>


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

        #classList { 
            width: 70%;
            margin: 0 auto;
            margin-top: 3vw;
            text-align: center;
            font-size: 1.2vw;
        }

        .headcell, .sno, .headcell1 { font-weight: 600; width: 30%; border-bottom: 0.1vw solid #cccccc; border-top: 0.1vw solid #cccccc;}
        .sno { width: 15%; border-left: 0.1vw solid #cccccc; }
        .backcellleft { border-left: 0.1vw solid #cccccc; border-bottom: 0.1vw solid #cccccc;}
        .backcellright { border-right: 0.1vw solid #cccccc; border-bottom: 0.1vw solid #cccccc;}
        .backcell { border-bottom: 0.1vw solid #cccccc; }
        .headcell1 { border-right: 0.1vw solid #cccccc;}
        .backbtn {
            border: none;
            outline: none;
            cursor: pointer;
            background-color: #64C5EB;
            color: #fff;
            font-weight: 600;
            font-family: Calibri;
            width: 12vw;
            height: 2vw;
            margin: 0.1vw;
            margin-left: 0.5vw;
        }

        .backbtn:hover { background-color: #1D8AB5;}

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
    margin: 0 auto;
    width: 53%;
    height: 2.6vw;
    text-align: center;
    margin-top: 7%;
}

.SelectionCSS{
    border: none;
    background-color: #64C5EB;
    color: #fff;
    font-size: 1.2vw;
    font-weight: 600;
    text-align: center;
    width: 15vw;
    height: 2.5vw;
    position: fixed;
    outline: none;
}
.SelectionCSS:hover {
    background-color: #1D8AB5 !important;
}

#ProformaBTN { background-color: #1D8AB5; }
#ResultsBTN { cursor: pointer; }

.test3 {
    border: 0.1vw solid white;
    float: left;
    width: 15.5vw;
}
.hiddenbtn { display: none; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Attendance" style="float:left; width: 15%;"></div>
        <div style="float: left; width: 85%; height: 100vh; overflow-y:scroll; overflow-x: hidden;">
            <div id="selectionDIV" class="Selection" runat="server" visible="false">
                <div class="test3"><button id="ResultsBTN" class="SelectionCSS" type="button"  onclick="DisplayResults()">View Results</button></div>
                <div class="test3"><button id="ProformaBTN" class="SelectionCSS" type="button"">View Proforma</button></div>
               <asp:Button ID="gotoresult" runat="server" CssClass="hiddenbtn" OnClick="gotoresult_Click" />
            </div>
            <div id="lineDIV" runat="server" visible="false" style="border-top: 0.1vw solid #a9a9a9; width: 80%; margin: 0 auto; margin-bottom: 3%;"></div>


            <div id="headingDIV" class="headingtop" runat="server" visible ="true">
            <h1>
                <span style="color: #fff;">Department Classes List</span>
            </h1>
            </div>
        <asp:Table ID="classList" runat="server" CellSpacing="0">
            <asp:TableRow runat="server" BackColor="#D1E8F1">
                <asp:TableCell runat="server" CssClass="sno">S.No.</asp:TableCell>
                <asp:TableCell runat="server" CssClass="headcell">Class</asp:TableCell>
                <asp:TableCell runat="server" CssClass="headcell">Shift</asp:TableCell>
                <asp:TableCell runat="server" CssClass="headcell1">Section</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
            <div style="margin: 0 auto; width: 50%;  text-align: center;">
            <asp:Label ID="Label1" runat="server" style="color: #FF0000;" Text="No Proformas Generated" Visible="False"></asp:Label>
        </div>
            </div>

        
    </form>

    <script>
        $(function () {
            $("#Nav-Attendance").load("NavigationBar.aspx");
        });
        function DisplayResults() {
            $('#gotoresult').trigger('click');
        }
    </script>
</body>
</html>
