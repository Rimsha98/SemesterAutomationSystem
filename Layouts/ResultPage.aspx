<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultPage.aspx.cs" Inherits="UokSemesterSystem.classes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="~/css/ResultPage.css" />
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title>Class Results | UOK</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Result" style="float:left; width: 15%;"></div>
        <div class="ClassesTableDIV">
            <div class="headingtop">
                <h1>Classes List</h1>
            </div>
            <p id="ResultsMsg">Following is a list of all courses you are conducting.</p>
        <asp:Table ID="classesTable" runat="server" CellSpacing="0">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" CssClass="tablecell" BackColor="#D1E8F1" style="border-left: 0.1vw solid #cccccc;"><span class="colorWhite">Class</span></asp:TableCell>
                <asp:TableCell runat="server" CssClass="tablecell" BackColor="#D1E8F1"><span class="colorWhite">Shift</span></asp:TableCell>
                <asp:TableCell runat="server" CssClass="tablecell" BackColor="#D1E8F1"><span class="colorWhite">Section</span></asp:TableCell>
                <asp:TableCell runat="server" CssClass="tablecell" BackColor="#D1E8F1"><span class="colorWhite">Course #</span></asp:TableCell>
                <asp:TableCell runat="server" CssClass="tablecelllg" BackColor="#D1E8F1" style="border-right: 0.1vw solid #cccccc;"><span class="colorWhite">Course Title</span></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
            <button id="testbtn" style="display: none;" type="button"></button>
           
            </div>

        
        
    </form>
     <script>
    $(function () {
        $("#Nav-Result").load("NavigationBar.aspx");
    });

         $(document).ready(function () {
             document.getElementById("testbtn").click();
         });

         $("#testbtn").on("click", function () {
             sessionStorage.setItem("ResultPage", "loaded");
         });

         $("#check").on("click", function () {
             alert(ok);
         });
        </script>
</body>
</html>