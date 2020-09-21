<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminResult.aspx.cs" Inherits="UokSemesterSystem.AdminResult" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title></title>
    <style type="text/css">
        body { font-family: Calibri; }
        .auto-style1 {
            text-align: center;
        }

        .RSTable {
            width: 90%;
            text-align: center;
            font-size: 1.2vw;
            font-family: Calibri;
            border: 0.1vw solid #cccccc;
        }
        .tcell { padding: 0.5vw; }
        .tablerow { border: 0.1vw solid #cccccc; font-weight: 600; background-color: #D1E8F1;  }
        .trrow { border: 0.1vw solid #cccccc; }

        .generatebtn, #classProforma {
            background-color: #1D8AB5;
            color: #fff;
            font-size: 1.2vw;
            font-family: Calibri;
            height: 2vw;
            width: 80%;
            cursor: pointer;
            border: none;
            outline: none;
        }
        .generatebtn:hover, #classProforma:hover { background-color: #64C5EB;}
        .generatebtnDisable, #classProforma {
            background-color: #64C5EB;
            color: #fff;
            font-size: 1.2vw;
            font-family: Calibri;
            height: 2vw;
            width: 20%;
            cursor: pointer;
            border: none;
            outline: none;
        }
        .generatebtnDisable:hover, #classProforma:hover { background-color: #1D8AB5;}

        #classProforma { float: left; width: 20%; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-TimeTableAdmin" style="float:left; width: 15%;"></div>
        <div style="float: left; width: 85%; height: 100vh; overflow-y:scroll; overflow-x: hidden;">
       <!-- <asp:Button runat="server" ID="btnBack" Text="Back" onclick="btnBack_Click" /> -->
       
            <Center>
          <div class="auto-style2" id="ResultReport" runat="server">
               <div style="                       text-align: center;
                       font-family: Calibri;">
                   <br /><br /><br />
               <asp:Label  runat="server" Text="University Of Karachi" style="font-size: 2.3vw; font-weight: 600;"></asp:Label><br />

            <asp:Label ID="lbl_deptName" runat="server" Text="dcs" style="font-size: 1.6vw"></asp:Label><br />
            
            <asp:Label   runat="server" Text="Total Marks: 100" style="font-size: 1.6vw"></asp:Label>
            <br />
            <asp:Label ID="Label12" runat="server" Text="Class:   " style="font-size: 1.6vw"></asp:Label>
            <asp:Label ID="lbl_className" runat="server" Text="BSSE-4th Year" style="font-size: 1.6vw"></asp:Label>
            <br />
            <asp:Label ID="Label14" runat="server" Text="Year:   " style="font-size: 1.6vw"></asp:Label>
            <asp:Label ID="lbl_dYear" runat="server" Text="2000" style="font-size: 1.6vw"></asp:Label>

       
               </div>

       
        <div>
            <br />
        <asp:Table ID="ResultSheetTable" runat="server" CssClass="RSTable" CellSpacing="0">
            <asp:TableRow ID="SheetHeader" runat="server" CssClass="tablerow">
                
            </asp:TableRow>
        </asp:Table>
    
        </div>
               <div aling="bottom" style="width: 90%; margin: 0 auto;">
               <br />
               <asp:Button ID="classProforma" runat="server" OnClick="classProforma_Click" Text="Generate Class Proforma" />
               <br />
                </div>
           <div aling="bottom" runat="server" visible="false">
               <asp:Image ID="Approve" runat="server" ImageUrl="~/Img/approved.jpg" width="150px" Height="100px" Visible="false"></asp:Image><br />
              <asp:Label runat="server" Text="ChairPerson _______________________" Font-Bold="true" Font-Size="Large"></asp:Label><br />
                </div>
               </div>
                </Center>
      <asp:Button Id="fixedbutton"  Text="Approve Result" runat="server" OnClick="fixedbutton_Click" Visible="false"/>
            <br />
      <asp:Button Id="PdfGenerator"  Text="Download Result" runat="server" OnClick="PdfGenerator_Click" Visible="false"/>
           <button id="HiddenBTN1" type="button" style="display: none;"></button>
     
        </div>
        
    </form>
    <script>
        $(function () {
            $("#Nav-TimeTableAdmin").load("AdminNavBar.aspx");
        });

        $(document).ready(function () {
            document.getElementById("HiddenBTN1").click();
        });

        $("#HiddenBTN1").on("click", function () {
            sessionStorage.setItem("AdminResult", "loaded");
        });
    </script>
</body>
</html>



