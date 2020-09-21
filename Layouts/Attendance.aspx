<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Attendance.aspx.cs" Inherits="UokSemesterSystem.Attendance" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title>Mark Attendance | UOK</title>

    <style>

        
.labelstyle {
    font-weight: 600; color: black; text-decoration: underline; padding: 0.2vw; font-size: 1.2vw;
}

#AttendanceSheetTable { 
    width: 100%;
    text-align: center;
    font-size: 1.2vw;
    border: 0.1vw solid #cccccc;
    font-family: Calibri;
}

#Label1 { font-size: 1.7vw; font-weight: bold; font-family: Arial;}
#Label2  { font-size: 1.2vw; font-weight: bold; font-family: Arial;}

td { padding: 0.2vw; }

.cellsno { width: 8%; font-weight: 600; border-bottom: 0.1vw solid #cccccc;  padding: 0.5vw;}
.cellsmall { 
    width: 10%;
    font-weight: 600;
    border-bottom: 0.1vw solid #cccccc;
    padding: 0.5vw;
}

.celllarge { 
    font-weight: 600;
    width: 23%;
    border-bottom: 0.1vw solid #cccccc;
     padding: 0.5vw;
}

.cellmid { 
    font-weight: 600;
    width: 12%;
    border-bottom: 0.1vw solid #cccccc;
     padding: 0.5vw;
}

.OptionButtons1, .OptionButtons2, .OptionButtons3 {
    border: none;
    outline: none;
    height: 1.5vw;
    width: 4.5vw;
    color: #fff;
    cursor: pointer;
    margin-bottom: 0.2vw;
}

.OptionButtons1 { background-color: green; }
.OptionButtons2 { background-color: red; margin: 0 0.3vw 0 0.3vw; }
.OptionButtons3 { background-color: skyblue; }

.OptionButtons1:hover { background-color: darkgreen; }
.OptionButtons2:hover { background-color: darkred; }
.OptionButtons3:hover { background-color:dodgerblue;}

#saveSheet {
    float: right;
    outline: none;
    border: none;
    cursor: pointer;
    margin-top: 2vw;
    width: 13vw;
    height: 2.5vw;
    font-size: 1.2vw;
    color: #fff;
    background-color: #64C5EB;
    font-weight: 600;
    margin-bottom: 3vw;

}

.backcell { padding: 0.5vw; }

#saveSheet:hover { background-color: #1D8AB5; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Attendance" style="float:left; width: 15%;"></div>


        <div style="float: left; width: 85%; height: 100vh; overflow-y:scroll; overflow-x: hidden;">


            <div style="width: 90%; margin: 0 auto; ">
            
                <div style="width: 100%;  text-align: center; text-align: center; padding-top: 5vw; padding-bottom: 2vw">
                    <asp:Label ID="Label1" runat="server" Text="UNIVERSITY OF KARACHI" ></asp:Label>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="DEPARTMENT OF COMPUTER SCIENCE" ></asp:Label>
                </div>

                <div style="width: 100%; ">
                    <div style="float: left; width: 50%; ">
                        <table>
                   <tr>
                       <td>
                           <asp:Label ID="depart" runat="server" Text="Department:"></asp:Label>
                            <asp:Label ID="departValue" runat="server" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                    <tr>
                       <td>
                           <asp:Label ID="mDepart" runat="server" Text="Major Department:"></asp:Label>
                            <asp:Label ID="mDepartValue" runat="server" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                    <tr>
                       <td>
                           <asp:Label ID="tName" runat="server" Text="Teacher's Name:"></asp:Label>
                            <asp:Label ID="tNameValue" runat="server" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                    <tr>
                       <td>
                            <asp:Label ID="courseNo" runat="server" Text="Course No:"></asp:Label>
                            <asp:Label ID="courseNoValue" runat="server" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                            <tr>
                                <td><label>&nbsp;</label></td>
                            </tr>
                </table>
                    </div>



                    <div style="float: left; width: 50%; ">

                         <table>
                   <tr>
                       <td>
                           <asp:Label ID="class" runat="server" Text="Class:"></asp:Label>
                            <asp:Label ID="classValue" runat="server" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                    <tr>
                       <td>
                           <asp:Label ID="year" runat="server" Text="Year:"></asp:Label>
<asp:Label ID="yearValue" runat="server" CssClass="labelstyle"></asp:Label>
&nbsp;<asp:Label ID="sem" runat="server" Text="Semester:"></asp:Label>
<asp:Label ID="semValue" runat="server" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                    <tr>
                       <td>
                           <asp:Label ID="date" runat="server" Text="Date:"></asp:Label>
<asp:Label ID="dateValue" runat="server" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                    <tr>
                       <td>
                            <asp:Label ID="courseName" runat="server" Text="Course Name:"></asp:Label>
                            <asp:Label ID="courseNameValue" runat="server" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                             <tr>
                                <td><label>&nbsp;</label></td>
                            </tr>
                </table>


                    </div>
                </div>

            <div runat="server" id="div1">
        <asp:Table ID="AttendanceSheetTable" runat="server" CellSpacing="0" GridLines="none">
            <asp:TableRow runat="server" BackColor="#D1E8F1">
                <asp:TableCell runat="server" CssClass="cellsno">SNo.</asp:TableCell>
                <asp:TableCell runat="server" CssClass="cellmid">Roll No.</asp:TableCell>
                <asp:TableCell runat="server" CssClass="celllarge">Name</asp:TableCell>
                <asp:TableCell runat="server" CssClass="celllarge">Father Name</asp:TableCell>
                <asp:TableCell runat="server" CssClass="cellsmall">Attendance</asp:TableCell>
                <asp:TableCell runat="server" CssClass="celllarge">Options</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </div>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
        
        <div id="reportDiv" runat="server" visible="false">
            
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="800px" style="margin-right: 54px">
                <LocalReport ReportPath="AttendanceSheet.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="table1" Name="DataSet1" />
                        <rsweb:ReportDataSource DataSourceId="SqlDataSource2" Name="DataSet2" />
                        <rsweb:ReportDataSource DataSourceId="SqlDataSource5" Name="DataSet4" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        
            </div>

                <asp:Button ID="saveSheet" runat="server" OnClick="saveSheet_Click" Text="Save Attendance" />
                <button id="Attendancebtn" type="button" style="display: none;"></button>
            </div>
            

                 
            </div>
    </form>

    <script>
    $(function () {
        $("#Nav-Attendance").load("NavigationBar.aspx");
    });

        $(document).ready(function () {
            document.getElementById("Attendancebtn").click();
        });

        $("#Attendancebtn").on("click", function () {
            sessionStorage.setItem("Attendance", "loaded");
        });
        </script>
</body>
</html>

