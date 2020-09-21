<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateTimeTable.aspx.cs" Inherits="UokSemesterSystem.CreateTimeTable" %>
 <meta http-equiv="Page-Enter" content="blendTrans(Duration=0)"/>
 <meta http-equiv="Page-Exit" content="blendTrans(Duration=0)"/>  
    
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="~/css/CreateTimeTable.css" />
   <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <style>
/* The Close Button */
.close {
  color: #aaaaaa;
  float: right;
  font-size: 28px;
  font-weight: bold;
}

.close:hover,
.close:focus {
  color: #000;
  text-decoration: none;
  cursor: pointer;
}
</style>
  
</head>

<body onload="Load()">
    <form id="form1" runat="server">
        <div id="Nav-TimeTable" style="float:left; width: 15%;"></div>
        <div style="float:left; width: 85%; height: 100vh; overflow-x: none; overflow-y: scroll">
        
            <!------------ Tabs ------------>
            <div class="Selection">
                <button id="ClassScheduleBTN" class="SelectionCSS" type="button" onclick="DisplaySchedule()">Class Schedule</button>
                <button id="AddTimeTableBTN" runat="server" class="SelectionCSS" type="button" onclick="DisplayAddForm()">Add Timetable</button>
            </div>
            <!------------- Class Schedule ----------->
            <div id="TimeTableDIV" style="margin-top: 5%;">
                <asp:Panel ID="Panel1" runat="server">
                <asp:Table ID="TeacherCoursesMorning" runat="server" CssClass="TableTT" CellSpacing="0">
                    <asp:TableRow CssClass="tablehead">
                        <asp:TableCell ColumnSpan="8">
                            <h2>MORNING SCHEDULE</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow CssClass="tablerow">
                        <asp:TableCell CssClass="cellmid">DAY</asp:TableCell>
                        <asp:TableCell CssClass="celllg">TEACHER</asp:TableCell>
                        <asp:TableCell CssClass="cellmid">CLASS</asp:TableCell>
                        <asp:TableCell CssClass="cellmid">SECTION</asp:TableCell>
                        <asp:TableCell CssClass="cellmid">COURSE #</asp:TableCell>
                        <asp:TableCell CssClass="celllg">COURSE TITLE</asp:TableCell>
                        <asp:TableCell CssClass="cellmid">TIME</asp:TableCell>
                        <asp:TableCell CssClass="cellright">ROOM #</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />
                <asp:Table ID="TeacherCoursesEvening" runat="server" CssClass="TableTT" CellSpacing="0">
                    <asp:TableRow CssClass="tablehead">
                        <asp:TableCell ColumnSpan="8">
                            <h2>EVENING SCHEDULE</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow CssClass="tablerow">
                        <asp:TableCell CssClass="cellmid">DAY</asp:TableCell>
                        <asp:TableCell CssClass="celllg">TEACHER</asp:TableCell>
                        <asp:TableCell CssClass="cellmid">CLASS</asp:TableCell>
                        <asp:TableCell CssClass="cellmid">SECTION</asp:TableCell>
                        <asp:TableCell CssClass="cellmid">COURSE #</asp:TableCell>
                        <asp:TableCell CssClass="celllg">COURSE TITLE</asp:TableCell>
                        <asp:TableCell CssClass="cellmid">TIME</asp:TableCell>
                        <asp:TableCell CssClass="cellright">ROOM #</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </asp:Panel>
            </div>

            <!----------- Form ---------------->
            <br />
         <div id="btn" runat="server">
            <asp:Label ID="SaveErr" runat="server" Text=""></asp:Label>
         </div>

            <div id="AddFormDIV" style="display: none; width: 50%; margin: 0 auto;">
                <asp:Panel ID="PnlMain" runat="server">
                <div runat="server" id="TT">
                    <table id="temp" style="width: 100%; border-collapse:separate; border-spacing: 0.5vw;" cellspacing="0" >
                        <tr class="trrow">
                            <td><asp:Label ID="Label1" runat="server" Text="Department:" CssClass="FormLabel"></asp:Label></td>
                            <td><asp:TextBox ID="DepartmentName" runat="server" CssClass="FormTextBox"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label  ID="Label13" runat="server" Text="Shift:" CssClass="FormLabel"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ShiftList" runat="server" CssClass="FormDropDown" OnSelectedIndexChanged="GetShift" AutoPostBack="true">
                                    <asp:ListItem>Morning</asp:ListItem>
                                    <asp:ListItem>Evening</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label2" runat="server" Text="Select Class:" CssClass="FormLabel"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ClassList" runat="server" CssClass="FormDropDown" OnSelectedIndexChanged="GetClassID" AutoPostBack="true"></asp:DropDownList>
                                <asp:Label ID="ClassErr" style="color:red" runat="server" CssClass="FormErrors" Text="You need to select class"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label4" runat="server" Text="Teacher:" CssClass="FormLabel"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="TeacherList" runat="server" CssClass="FormDropDown" OnSelectedIndexChanged="GetTeacherID" AutoPostBack="true"></asp:DropDownList>
                                <asp:Label ID="TeacherErr" style="color:red" runat="server" CssClass="FormErrors" Text="You need to select teacher"></asp:Label>
                            </td>
                        </tr>
                        <tr> 
                            <td><div runat="server" id="AssLabelDiv" visible="false">
                            <asp:Label ID="Label5"  runat="server" Text="Assistant Teacher:" CssClass="FormLabel"></asp:Label>
                              </div>
                             </td>
                           
                            <td>
                                <div runat="server" id="AssDiv" visible="false">
                                    <asp:DropDownList ID="AssisTeacherList" runat="server" CssClass="FormDropDown" OnSelectedIndexChanged="GetAssTeacherID" AutoPostBack="true"></asp:DropDownList>
                                    <asp:Label ID="AssErr" style="color:red" runat="server" CssClass="FormErrors" Text="You need to select Assistant Teacher"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label3" runat="server" Text="Select Course:" CssClass="FormLabel"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="CourseList" runat="server" CssClass="FormDropDown" OnSelectedIndexChanged="GetCoursesID" AutoPostBack="true"></asp:DropDownList>
                                <asp:Label ID="CourseErr" style="color:red" runat="server" CssClass="FormErrors" Text="You need to select Course"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label6" runat="server" Text="Start Time:" CssClass="FormLabel"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="STime" runat="server" CssClass="FormTextBox"></asp:TextBox>
                                <asp:Label Visible="false" style="color:red" ID="StimeErr" CssClass="FormErrors" runat="server" Text="You need to enter class start time"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td> <asp:Label ID="Label14" runat="server" CssClass="FormLabel" Text="Select Time Slot:"></asp:Label></td>
                            <td><asp:DropDownList ID="SlotTime" runat="server" CssClass="FormDropDown" OnSelectedIndexChanged="SlotTime_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>Slot 1</asp:ListItem>
                                    <asp:ListItem>Slot 2</asp:ListItem>
                                    <asp:ListItem>Slot 3</asp:ListItem>
                                    </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label7" runat="server" Text="End Time:" CssClass="FormLabel"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="ETime" runat="server" CssClass="FormTextBox"></asp:TextBox>
                                <asp:Label Visible="false" style="color:red" ID="EtimeErr" CssClass="FormErrors" runat="server" Text="You need to enter class End time"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label  ID="Label8" runat="server" Text="Class Room No#:" CssClass="FormLabel" ></asp:Label></td>
                            <td>
                                <asp:TextBox ID="RoomNo" runat="server" CssClass="FormTextBox"></asp:TextBox>
                                <asp:Label Visible="false" style="color:red" ID="RoomErr" CssClass="FormErrors" runat="server" Text="You need to enter class Room #"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label  ID="Label9" runat="server" Text="Semester No#:" CssClass="FormLabel"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="SemesterList" runat="server" CssClass="FormDropDown">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label  ID="Label10" runat="server" Text="Section:" CssClass="FormLabel"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="Section" runat="server" CssClass="FormTextBox"></asp:TextBox>
                                <asp:Label Visible="true" style="color:red" ID="Label11" runat="server" Text="" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label  ID="Label12" runat="server" Text="Day:" CssClass="FormLabel"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="DaysList" runat="server" CssClass="FormDropDown">
                                    <asp:ListItem>Monday</asp:ListItem>
                                    <asp:ListItem>Tuesday</asp:ListItem>
                                    <asp:ListItem>Wednesday</asp:ListItem>
                                    <asp:ListItem>Thursday</asp:ListItem>
                                    <asp:ListItem>Friday</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="AddTT" runat="server" CssClass="FormButtons" Text="Add Schedule" OnClick="AddTimeTable_Click" />
                                <asp:Button ID="UpdateTT" runat="server" CssClass="FormButtons" Visible="true" Text="Update TimeTable" OnClick="UpdateTT_Click" />
                            </td>

                        </tr>
                    </table>
                    <br />
            <asp:Button runat="server" Id="Back" Visible="false" Text="Cancel" OnClick="Back_Click"/>
                    <button style="display: none" type="button" id="createtimetable"></button>
                    <br />
        </div>
            </asp:Panel>
            </div>
            
        
        
            </div>

        <script>
            $(function () {
                $("#Nav-TimeTable").load("NavigationBar.aspx");
            });

            $(document).ready(function () {
                document.getElementById("ClassScheduleBTN").style.backgroundColor = "#1D8AB5";
                document.getElementById("createtimetable").click();
            });

            $("#createtimetable").on("click", function () {
                sessionStorage.setItem("createTT", "loaded");
            });


            var schedule = document.getElementById("TimeTableDIV");
            var addform = document.getElementById("AddFormDIV");
            function Load()
            {
                //if (schedule.style.display == "block")
                //{
                //    window.location.reload(true); 
                //}

                var username = '<%= Session["IsClick"] %>';
                sessionStorage["a"] = username;
                if (sessionStorage["a"] == "1") {
                    DisplaySchedule();
                } else if (sessionStorage["a"] == "0") {
                    DisplayAddForm();
                } 

            }
            function DisplaySchedule() {
                sessionStorage["a"] = "1";

                
                 if (schedule.style.display == "none") {
                    schedule.style.display = "block";
                    document.getElementById("ClassScheduleBTN").style.backgroundColor = "#1D8AB5";
                    addform.style.display = "none";
                     document.getElementById("AddTimeTableBTN").style.backgroundColor = "#64C5EB";
                     window.location.replace("CreateTimeTable.aspx");
                 } else {
                    schedule.style.display = "block";
                    document.getElementById("ClassScheduleBTN").style.backgroundColor = "#1D8AB5";
                    addform.style.display = "none";
                    document.getElementById("AddTimeTableBTN").style.backgroundColor = "#64C5EB";

                }

            }

            function DisplayAddForm() {
                sessionStorage["a"] = "0";
                if (addform.style.display == "none") {
                    addform.style.display = "block";
                    document.getElementById("AddTimeTableBTN").style.backgroundColor = "#1D8AB5";
                    schedule.style.display = "none";
                    document.getElementById("ClassScheduleBTN").style.backgroundColor = "#64C5EB";

                }
                else {
                    addform.style.display = "block";
                    document.getElementById("AddTimeTableBTN").style.backgroundColor = "#1D8AB5";
                    schedule.style.display = "none";
                    document.getElementById("ClassScheduleBTN").style.backgroundColor = "#64C5EB";

                }
            }

            var modal = document.getElementById("PnlMain");

            // Get the button that opens the modal
            var btn = document.getElementById("btn");

            // Get the <span> element that closes the modal
            var span = document.getElementsByClassName("close")[0];
            span.onclick = function () {
                modal.style.display = "none";
                btn.style.display = "block";
            }
        </script>
    </form>
</body>
</html>
