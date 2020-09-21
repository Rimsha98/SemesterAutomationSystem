<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="ResultSystem.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title>Edit Result | UOK</title>
    <script type="text/javascript">
       
    </script>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .auto-style2 {
            width: 902px;
            height: 161px;
        }

             * {
    box-sizing: border-box;
}

body {
    padding: 0;
    margin: 0;
    overflow: hidden;
}

.ResultDIV {
    float: left;
    width: 85%;
    height: 100vh;
    overflow-y: scroll;
    overflow-x: hidden;
    margin: 0 auto;
}
.aligncenter { text-align: center; }
#Label1 { font-size: 2vw; font-weight: bold; font-family: Arial; }
#Label2, #Label3 { font-size: 1.2vw; font-weight: bold; font-family: Arial;}

.labelstyle {
    font-weight: 600; color: black; text-decoration: underline; padding: 0.2vw; font-size: 1.2vw;
}

#dt { 
    width: 100%; text-align: center;  border: 0.1vw solid #cccccc; font-family: Calibri; font-size: 1.2vw;
}


.sno, .seatno, .name, .totalmarks, .totalinwords, .gpa, .remarks, .theory, .lab {
    background-color: #D1E8F1;
    font-size: 1vw;
    color:black;
     border-bottom: 0.1vw solid #cccccc;
     padding: 0.5vw;
}
.sno, .gpa, .theory, .lab, .totalmarks  { width: 7%; }
.seatno { width: 10%; }
.remarks { width: 15%; }
.totalinwords { width: 15%; padding: 0.5vw;}

#btn_updateResult, #btn_submit, .ButtonCSS {
    float: right;
    margin-top: 2vw;
    width: 10vw;
    font-size: 1.2vw;
    font-weight: bold;
    background-color: #64C5EB;
    border: none;
    outline: none;
    color: #fff;
    cursor: pointer;
    height: 2.5vw;
}
#btn_updateResult:hover, #btn_submit:hover, .ButtonCSS:hover {background-color: #1D8AB5;}
.backcell { padding: 0.5vw; font-family: Calibri; font-size: 1.2vw; }

.resulttextbox {
    width: 3vw;
    height: 2vw;
    outline: none;
    text-align: center;
    border: none;
    border: 0.05vw solid #aaaaaa;
}

.DateTextbox { 
    width: 10vw;
    height: 1.5vw;
    outline: none;
    text-align: left;
    border: none;
    border: 0.05vw solid #aaaaaa;
    padding: 0.5vw;
}
        </style>
</head>
<body>
    <div id="Nav-Result" style="float:left; width: 15%;"></div>
    <div class="ResultDIV">
         <form id ="form1" runat="server" autocomplete="off">
        <div style="width: 90%; height: 100vh; margin: 0 auto; ">
            <div style="                    width: 100%;
                    text-align: center;
                    padding-top: 5vw;
                    padding-bottom: 2vw
            ">
                <asp:Label ID="Label1" runat="server" Text="University of Karachi"></asp:Label>
            <p style="margin-top: -0.1vw;">
                <asp:Label ID="Label2" runat="server" Text="(SEMESTER EXAMINATION)"></asp:Label>
            </p>
            <asp:Label ID="Label3" runat="server" Text="FACULTY OF SCIENCE"></asp:Label>
            </div>




            
            <div style="float: left; width: 50%; ">
                <table>
                   <tr>
                       <td>
                           <asp:Label ID="Label4" runat="server" Text="Department:  " CssClass="labelcls"></asp:Label>
                <asp:Label ID="lbl_deptName" runat="server" Text="dept Here" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                    <tr>
                       <td>
                           <asp:Label ID="Label6" runat="server" Text="Major Department:  " CssClass="labelcls"></asp:Label>
                <asp:Label ID="lbl_majDeptName" runat="server" Text="dept again" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                    <tr>
                       <td>
                           <asp:Label ID="Label8" runat="server" Text="Teacher's Name:    " CssClass="labelcls"></asp:Label>
            <asp:Label ID="lbl_TeacherName" runat="server" Text="name" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                    <tr>
                       <td>
                           <asp:Label ID="Label10" runat="server" Text="Course No.    " CssClass="labelcls"></asp:Label>
            <asp:Label ID="lbl_CourseNum" runat="server" Text="course num here" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                    <tr>
                       <td>
                           <asp:Label ID="Label21" runat="server" Text="Credit Hours:   " CssClass="labelcls"></asp:Label>
            <asp:Label ID="lbl_creditHours" runat="server" Text="2/3" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" >&nbsp;</asp:Label>
                        </td>
                    </tr>
                </table>
            </div>



            <div style="float: left; width: 50%; ">
                <table>
                    <tr>
                        <td>

                <asp:Label ID="Label12" runat="server" Text="Class:   " CssClass="labelcls"></asp:Label>
            <asp:Label ID="lbl_className" runat="server" Text="class" CssClass="labelstyle"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
<asp:Label ID="Label14" runat="server" Text="Year:   "></asp:Label>
            <asp:Label ID="lbl_dYear" runat="server" Text="0000" CssClass="labelstyle"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label16" runat="server" Text="Semester:   " CssClass="labelcls"></asp:Label>
            <asp:Label ID="lbl_semNo" runat="server" Text="sem num" CssClass="labelstyle"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
<asp:Label ID="Label18" runat="server" Text="Date Examination Held:   " CssClass="labelcls"></asp:Label>
                        <asp:TextBox ID="eDate" runat="server" MaxLength="10" CssClass="DateTextbox" placeholder="DD/MM/YYYY"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
<asp:Label ID="Label19" runat="server" Text="Course Title:   " CssClass="labelcls"></asp:Label>
            <asp:Label ID="lbl_courseName" runat="server" Text="course name here" CssClass="labelstyle"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" >&nbsp;</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" >&nbsp;</asp:Label>
                        </td>
                    </tr>

                </table>
                
            </div>






            <div style=" width: 100%; ">
                <asp:Table ID="dt" runat="server" GridLines="None" CellSpacing="0" >
           
        </asp:Table>
                <div style="display: none;">
                <asp:Button ID="btn_submit" OnClick="submitResult"  Text="Submit Result" runat="server" CausesValidation="true"  />
        <asp:Button ID="btn_updateResult" runat="server" OnClick="btn_updateResult_Click" Text="Update Result" CausesValidation="true" />
                    </div>
        <asp:Button ID="btn_edit" runat="server" OnClick="btn_edit_Click" Text="Edit Result" Width="104px" />
                <label id="emptyLabel" runat="server" style="color: red; display: none; text-align:center; margin-top: 1%;">Must fill out all fields</label>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ForeColor="Red" ControlToValidate="eDate" ErrorMessage="Date format: DD/MM/YYYY (Year must be >1900 & <2100)"
ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" CssClass="aligncenter"></asp:RegularExpressionValidator>
                <button id="resultaspx" type="button" style="display: none"></button>
                <button id="ss" runat="server" type="button" class="ButtonCSS" style="display: none;">Update Result</button>
                <button id="aa" runat="server" type="button" class="ButtonCSS" style="display: none;">Submit Result</button>
                <br /><br /><br /><br /><br /><br />
                </div>
        </div>
             
             </form>
    </div>

    <script type="text/javascript">



    $(function () {
        $("#Nav-Result").load("NavigationBar.aspx");

    });

        $(document).ready(function () {
            document.getElementById("resultaspx").click();
        });

        $("#resultaspx").on("click", function () {
            sessionStorage.setItem("Result", "loaded");
        });

        $(document).on("click", 'input[type=text]', function () {
           $(this).css({ "backgroundColor": "white" });
        });

        $(".resulttextbox").bind("keypress", function (event) {
            
            if (event.charCode != 0) {
                var regex = new RegExp("^[0-9]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (!regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            }
        });

        var count = 0;
        var submit_count = 0;

        $('#ss').click(function () {
            $('.resulttextbox').each(function (i, item) {
                var grade = $(item).val();
                if (!$.trim(grade).length) {
                    $(item).css({ "backgroundColor": "#f5898e" });
                    count++;
                }
            });

            if (count > 0) {
                document.getElementById("emptyLabel").style.display = "block";
            }
            else {
                document.getElementById("emptyLabel").style.display = "none";
                $('#btn_updateResult').trigger('click');
                
            }

            count = 0;
        });

        $('#aa').click(function () {
            $('input[type=text]').each(function (i, item) {
                var grade = $(item).val();
                if (!$.trim(grade).length) {
                    $(item).css({ "backgroundColor": "#f5898e" });
                    submit_count++;
                }
            });

            if (submit_count > 0) {
                document.getElementById("emptyLabel").style.display = "block";
            }
            else {
                document.getElementById("emptyLabel").style.display = "none";
                $('#btn_submit').trigger('click');

            }

            submit_count = 0;
        });
        
        </script>
</body>
</html>

