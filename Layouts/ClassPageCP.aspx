<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassPageCP.aspx.cs" Inherits="UokSemesterSystem.ClassPageCP" %>

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

         #classesTable { 
            width: 80%;
            margin: 0 auto;
            margin-top: 3vw;
            text-align: center;
        }



        .headcell, .sno, .headcell1 { font-weight: 600; width: 30%;  border-bottom: 0.1vw solid #cccccc; border-top: 0.1vw solid #cccccc; }
        .backcellleft { border-left: 0.1vw solid #cccccc; border-bottom: 0.1vw solid #cccccc;}
        .backcellright { border-right: 0.1vw solid #cccccc; border-bottom: 0.1vw solid #cccccc;}
        .backcell { border-bottom: 0.1vw solid #cccccc; }
        .headcell1 { border-right: 0.1vw solid #cccccc;}
        .sno { width: 15%;  border-left: 0.1vw solid #cccccc;}
        .backcell {  }
        .backcellbutton {
            border: none;
            outline: none;
            cursor: pointer;
            background-color: #64C5EB;
            color: #fff;
            font-weight: 600;
            font-family: Calibri;
            width: 10vw;
            height: 2vw;
            margin: 0.1vw;
            margin-left: 0.5vw;
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

#ResultsBTN { background-color: #1D8AB5; }
#ProformaBTN { cursor: pointer; }

.test3 {
    border: 0.1vw solid white;
    float: left;
    width: 15.5vw;
}

.hiddenbtn { display: none; }
        .backcellbutton:hover { background-color: #1D8AB5;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Result" style="float:left; width: 15%;"></div>
        <div style="float: left; width: 85%; height: 100vh; overflow-y:scroll; overflow-x: hidden;">

            <div class="Selection">
                <div class="test3"><button id="ResultsBTN" class="SelectionCSS" type="button">View Results</button></div>
                <div class="test3"><button id="ProformaBTN" class="SelectionCSS" type="button" onclick="DisplayProformaPage()">View Proforma</button></div>
                <asp:Button ID="gotoproforma" runat="server" CssClass="hiddenbtn" OnClick="gotoproforma_Click" />
            </div>
            <div style="border-top: 0.1vw solid #a9a9a9; width: 80%; margin: 0 auto; margin-bottom: 3%;"></div>



         <asp:Table ID="classesTable" runat="server" CellSpacing="0">
            <asp:TableRow runat="server" BackColor="#D1E8F1">
                <asp:TableCell runat="server" CssClass="sno">S.No.</asp:TableCell>
                <asp:TableCell runat="server" CssClass="headcell">Class</asp:TableCell>
                <asp:TableCell runat="server" CssClass="headcell">Section</asp:TableCell>
                <asp:TableCell runat="server" CssClass="headcell1">Shift</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
            <button id="HiddenBTN1" type="button" style="display: none;"></button>
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
             sessionStorage.setItem('ClassPageCP', 'loaded');
         });


         function DisplayProformaPage() {
             $('#gotoproforma').trigger('click');
         }
        </script>
</body>
</html>
