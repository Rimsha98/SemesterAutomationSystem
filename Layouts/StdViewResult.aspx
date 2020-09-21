<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StdViewResult.aspx.cs" Inherits="UokSemesterSystem.StdViewResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="~/css/StdViewResult.css" />
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title>Student Result | UOK</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Result" style="float:left; width: 15%;"></div>
        <div class="ResultsTableDIV">
            <div class="headingtop">
            <h1>
                <span style="color: #fff;">Student Results</span>
            </h1>
            </div>

            <div style="width: 90%; margin: 0 auto;">
            <p id="ResultsMsg" runat="server" visible="true">The following results have been announced in the respective courses.</p>
            <button id="HiddenBTN4" type="button" style="display: none;"></button>

            <!------------------------ new code ---------------------->
            <asp:Label ID="s1_Lbl" runat="server" Text="SEMESTER NO 1" Visible="False" CssClass="TableLabel"></asp:Label>
            <asp:Table ID="sem1" runat="server" CellSpacing="0" Visible="False" CssClass="TableCSS">
            <asp:TableHeaderRow runat="server">
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">S.No.</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Course #</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecelllg">Course Title</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Credit Hours</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Theory</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Lab</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Total Marks</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
            <asp:Label ID="s2_Lbl" runat="server" Text="SEMESTER NO 2" Visible="False" CssClass="TableLabel"></asp:Label>
             <asp:Table ID="sem2" runat="server" CellSpacing="0" Visible="False" CssClass="TableCSS">
            <asp:TableHeaderRow runat="server">
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">S.No.</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Course #</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecelllg">Course Title</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Credit Hours</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Theory</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Lab</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Total Marks</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
            <asp:Label ID="s3_Lbl" runat="server" Text="SEMESTER NO 3" Visible="False" CssClass="TableLabel"></asp:Label>
             <asp:Table ID="sem3" runat="server" CellSpacing="0" Visible="False" CssClass="TableCSS">
            <asp:TableHeaderRow runat="server">
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">S.No.</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Course #</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecelllg">Course Title</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Credit Hours</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Theory</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Lab</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Total Marks</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
            <asp:Label ID="s4_Lbl" runat="server" Text="SEMESTER NO 4" Visible="False" CssClass="TableLabel"></asp:Label>
             <asp:Table ID="sem4" runat="server" CellSpacing="0" Visible="False" CssClass="TableCSS">
            <asp:TableHeaderRow runat="server">
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">S.No.</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Course #</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecelllg">Course Title</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Credit Hours</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Theory</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Lab</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Total Marks</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
            <asp:Label ID="s5_Lbl" runat="server" Text="SEMESTER NO 5" Visible="False" CssClass="TableLabel"></asp:Label>
             <asp:Table ID="sem5" runat="server" CellSpacing="0" Visible="False" CssClass="TableCSS">
            <asp:TableHeaderRow runat="server">
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">S.No.</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Course #</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecelllg">Course Title</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Credit Hours</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Theory</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Lab</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Total Marks</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
            <asp:Label ID="s6_Lbl" runat="server" Text="SEMESTER NO 6" Visible="False" CssClass="TableLabel"></asp:Label>
             <asp:Table ID="sem6" runat="server" CellSpacing="0" Visible="False" CssClass="TableCSS">
            <asp:TableHeaderRow runat="server">
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">S.No.</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Course #</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecelllg">Course Title</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Credit Hours</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Theory</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Lab</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Total Marks</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
              <br />
            <asp:Label ID="s7_Lbl" runat="server" Text="SEMESTER NO 7" Visible="False" CssClass="TableLabel"></asp:Label>
             <asp:Table ID="sem7" runat="server" CellSpacing="0" Visible="False" CssClass="TableCSS">
            <asp:TableHeaderRow runat="server">
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">S.No.</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Course #</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecelllg">Course Title</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Credit Hours</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Theory</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Lab</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Total Marks</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
            <asp:Label ID="s8_Lbl" runat="server" Text="SEMESTER NO 8" Visible="False" CssClass="TableLabel"></asp:Label>
             <asp:Table ID="sem8" runat="server" CellSpacing="0" Visible="False" CssClass="TableCSS">
            <asp:TableHeaderRow runat="server">
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">S.No.</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Course #</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecelllg">Course Title</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Credit Hours</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Theory</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellsm">Lab</asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" CssClass="tablecellmd">Total Marks</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
            <br />
            <br />
                </div>
            </div>
    </form>
    <script>
    $(function () {
        $("#Nav-Result").load("NavigationBar.aspx");
    });

        $(document).ready(function () {
            document.getElementById("HiddenBTN4").click();
        });

        $("#HiddenBTN4").on("click", function () {
            sessionStorage.setItem('font', 'Helvetica');
        });
        
        </script>
</body>
</html>
