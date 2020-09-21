<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="proforma.aspx.cs" Inherits="UokSemesterSystem.proforma" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title></title>
    <style type="text/css">
        #ProformaDIV { transform : scale(0.9); transform-origin: top center;}  

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
            
        }
        .Buttons:hover { background-color: #1D8AB5;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Proforma" style="float:left; width: 15%;"></div>

          
   
          
        
        <div style =" overflow-x: hidden; overflow-y: scroll; margin: 0 auto; width: 85%; height: 100vh;">
        
        <div style="width: 90%; margin: 0 auto; margin-top: 5%; margin-bottom: 3%; text-align: right">
            <asp:Button ID="DownloadPdf" runat="server" CssClass="Buttons" Text="Download Proforma" OnClick="DownloadPdf_Click" />
        </div>

        <div id="ProformaDIV" runat="server" style="width: 100%; margin: 0 auto; ">





        <div id="Proforma" runat="server" style="margin: 0 auto; width: 100%; background-color: #FFFFCC;  text-align:center; border: 0.1vw solid #cccccc; font-family: 'Times New Roman';">
            <br /><br /><br /><br />
            <asp:Label runat="server" Font-Bold="true" Font-Size="20" Text="University of Karachi"></asp:Label><br />
            <asp:Label runat="server" Font-Size="15" Text="Semester Examinations Section"></asp:Label><br />
            <asp:Label runat="server" Font-Size="15" Text="Provisional Marks Sheet for Semester - "></asp:Label>
            <asp:Label ID="Label3" runat="server" Font-Size="15"></asp:Label><br />
            <asp:Label runat="server" Font-Size="15" Text="Degree/Class: "></asp:Label>
            <asp:Label ID="classname" runat="server" Font-Size="15"></asp:Label><br />
            <asp:Label runat="server" Font-Bold="true" Font-Size="15" Text="ACADEMIC YEAR "></asp:Label>
            <asp:Label ID="currentyear" runat="server" Font-Bold="true" Font-Size="15"></asp:Label>
            <br /><br /><br /><br />

            <asp:Table runat="server" CellSpacing="10" Width="100%" GridLines="None" Font-Size="13" style="text-align: left;">
                <asp:TableRow>
                    <asp:TableCell Width="13%">
                        <asp:Label runat="server" Font-Bold="true" Text="Student's Name"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="40%"  ID="celluno">
                        <asp:Label runat="server" ID="stdname"></asp:Label>
                    </asp:TableCell>
                        
                    <asp:TableCell Width="13%">
                        <asp:Label runat="server" Font-Bold="true" Text="Seat No."></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="34%" ID="celldos">
                        <asp:Label runat="server" ID="sno"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="13%">
                        <asp:Label runat="server" Font-Bold="true" Text="Father's Name"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="40%" ID="celltres">
                        <asp:Label runat="server" ID="fname"></asp:Label>
                    </asp:TableCell>
                        
                    <asp:TableCell Width="13%">
                        <asp:Label runat="server" Font-Bold="true" Text="Enrolment No."></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="34%" ID="cellcuatro">
                        <asp:Label runat="server" ID="enrol"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="13%">
                        <asp:Label runat="server" Font-Bold="true" Text="Faculty of "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="40%" ID="cellcinco">
                        <asp:Label runat="server" ID="facult" Text="Science"></asp:Label>
                    </asp:TableCell>
                        
                    <asp:TableCell Width="13%">
                        <asp:Label runat="server" Font-Bold="true" Text="Department"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="34%" ID="cellseis">
                        <asp:Label runat="server" ID="dname"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br /><br /><br />

            <asp:Table ID="Table1" runat="server" CellSpacing="0" Width="100%" style="text-align:center;" >
                    <asp:TableRow ID="TableRow1" runat="server" BackColor="#FFFFCC">
                    <asp:TableCell ColumnSpan="4" ID="emptycell"></asp:TableCell>
                        <asp:TableCell ColumnSpan="2" BackColor="#FFFFCC" ID="marksobt"><asp:Label runat="server" Text="Marks Obtained"></asp:Label></asp:TableCell>
                          <asp:TableCell ColumnSpan="3" BackColor="#FFFFCC" ID="coursee"><asp:Label runat="server" Text="Course"></asp:Label></asp:TableCell>
                          <asp:TableCell ColumnSpan="2" BackColor="#FFFFCC" runat="server" ID="coursecleared"><asp:Label runat="server" Text="Courses Cleared in Semester"></asp:Label></asp:TableCell>
                      
                   </asp:TableRow>
                     <asp:TableRow ID="SheetHeader" runat="server" BackColor="#FFFFCC" >
                
            </asp:TableRow>
                </asp:Table>
                <br /><br /><br />
                <asp:Table ID="LinesTable" runat="server">
                    <asp:TableRow>
                        <asp:TableCell ID="cellone" runat="server">
                            <asp:Label ID="resultLBL" runat="server" Text="Result " Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="celltwo" runat="server">
                            <asp:Label ID="result" runat="server" Text="resultremarks" Font-Bold="true" ></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell ID="cellthree" runat="server">
                            <asp:Label ID="remarksLBL" runat="server" Text="Remarks " Font-Bold="true" ></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ID="cellfour" runat="server">
                            <asp:Label ID="remarks" runat="server" Text="" Font-Bold="true" ></asp:Label>
                        </asp:TableCell>
                        
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" runat="server" ID="cellfive"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br /><br /><br /><br /><br />
                <asp:Table runat="server" ID="EndingTable" Width="100%" CellSpacing="10" GridLines="None" Font-Size="11" style="text-align: left; font-family: 'Times New Roman';">
                    <asp:TableRow>
                        <asp:TableCell Width="30%">
                            <asp:Label runat="server" Font-Bold="true" Text="Dated: "></asp:Label>
                            <asp:Label ID="Date" runat="server" Text="current date"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="35%">
                            <asp:Label runat="server" Font-Bold="true" Text="Checked By: _______Admin_______"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="35%">
                            <asp:Label runat="server" Font-Bold="true" Text="Assistant Controller: ___________________"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <asp:Label ID="Label15" runat="server" Text="Generated By: " Font-Bold="true"></asp:Label>
                            <asp:Label ID="adminname" runat="server" Text="Shamim A.Raul"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:Table ID="newtable" runat="server" Width="100%" CellSpacing="10" GridLines="None" Font-Size="11" style="text-align: left; font-family: 'Times New Roman';">
                    <asp:TableRow>
                        <asp:TableCell Width="5%">
                            <asp:Label ID="Label17" runat="server" Text="Note:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="95%">
                            <asp:Label runat="server" Text="1. University reserves the right to correct any error that may be detected in the Marks Sheet / Proforma"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="5%">

                        </asp:TableCell>
                        <asp:TableCell Width="95%">
                            <asp:Label runat="server" Text="2. This Provisional mark proforma cannot be presented in any court of law by concerned candidate unless he/she is issued commulative marks sheet from the semester examinitions section as per semester rules"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
            <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
            </div>
        </div>
            <button id="HiddenBTN1" type="button" style="display: none;"></button>
            </div>
    </form>
    <script>
        $(function () {
            $("#Nav-Proforma").load("AdminNavBar.aspx");
        });

        $(document).ready(function () {
            document.getElementById("HiddenBTN1").click();
        });

        $("#HiddenBTN1").on("click", function () {
            sessionStorage.setItem("IndvProforma", "loaded");
        });
    </script>
</body>
</html>

