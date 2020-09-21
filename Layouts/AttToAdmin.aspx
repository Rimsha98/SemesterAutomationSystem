<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttToAdmin.aspx.cs" Inherits="UokSemesterSystem.AttToAdmin" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title></title>

    <style>
        p { margin: 0; padding: 0; }

#attTable {
    width: 90%;
    margin: 0 auto;
    margin-top: 3vw;
    font-size: 1.2vw;
    font-family: Calibri;
    border: 0.1vw solid #cccccc;
}

.cellsmall {
    background-color: ghostwhite;
    font-weight: 600;
    color: black;
    border-bottom: 0.1vw solid #cccccc;
    padding: 0.5vw;
}

.cellmedium {
    background-color: ghostwhite;
    font-weight: 600;
    color: black;
    border-bottom: 0.1vw solid #cccccc;
     padding: 0.5vw;
}

.marked { 
    color: #red;
    
}

.sno , .fname ,.name, .rollno {  padding: 0.5vw; }

.cellbig {
    background-color: #D1E8F1;
    font-weight: 600;
    color: black;
    border-bottom: 0.1vw solid #cccccc;
     padding: 0.5vw;
}

.backcell {  padding: 0.5vw; }

.cellp {
    width: 10%;
    background-color: ghostwhite;
    font-weight: 600;
    color: black;
    border: 0.1vw solid #4b4b4b;
}

#backbtn, #pdf {
    width: 12vw;
    height: 2.3vw;
    margin-top: 2vw;
    background-color: #64C5EB;
    outline: none;
    cursor: pointer;
    font-weight: 600;
    color: #fff;
    border: none;
    font-size: 1.25vw;
    font-family: Calibri;
}
#backbtn { width: 8vw; background-color: #4b4b4b; }
#Label1 { text-align: center; }

#backbtn:hover{ background-color: #6b6b6b; }
#pdf:hover { background-color: #1D8AB5; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Attendance" style="float:left; width: 15%;"></div>

        <div style="float: left; width: 85%; height: 100vh; overflow-y:scroll; overflow-x: hidden;">
        <div id="sheet" runat="server" style="margin-left:auto; margin-right:auto; text-align:center; width: 90%;">
       
            <div style="margin-top: 2.5vw; font-family: Calibri; font-weight: 600; font-size: 20px; text-align: center;">
                <br /><br />
                <p><asp:Label ID="Label1" runat="server" Text="UNIVERSITY OF KARACHI" Font-Bold="true"></asp:Label></p>
                <p style="font-size: 20px; "><asp:Label ID="departLabel" runat="server" Text="Label" Font-Bold="false"></asp:Label></p>
                <p style="font-size: 20px; "><asp:Label ID="classLabel" runat="server" Text="Label" Font-Bold="false"></asp:Label></p>
                <br />
            </div>

        <asp:Table ID="attTable" runat="server" CellSpacing="0" GridLines="horizontal" Width="100%" style="text-align:center; margin: 0 auto;">
            <asp:TableRow runat="server"  style="background-color: #D1E8F1; font-weight: 600" bgColor="#D1E8F1">
                <asp:TableCell runat="server" CssClass="sno">S.No</asp:TableCell>
                <asp:TableCell runat="server" CssClass="rollno">Roll #</asp:TableCell>
                <asp:TableCell runat="server" CssClass="name">Student Name</asp:TableCell>
                <asp:TableCell runat="server" CssClass="fname">Father's Name</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
              </div>

            <div style="text-align: right; margin: 0 auto; width: 90%">
                <asp:Button ID="backbtn" runat="server" OnClick="go_back" Text="Back"/>
        <asp:Button ID="download" runat="server" OnClick="download_Click" Text="Download" Visible="False" />
            <asp:Button ID="pdf" runat="server" OnClick="pdf_Click" Text="Generate PDF" />
      </div>

        <div>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server">
            </rsweb:ReportViewer>
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
            <button id="HiddenBTN1" type="button" style="display: none;"></button>
            </div>
    </form>

    <script>
        $(function () {
            $("#Nav-Attendance").load("NavigationBar.aspx");
        });

        $(document).ready(function () {
            document.getElementById("HiddenBTN1").click();
        });

        $("#HiddenBTN1").on("click", function () {
            sessionStorage.setItem('AttToAdmin', 'loaded');
        });
    </script>
</body>
</html>

