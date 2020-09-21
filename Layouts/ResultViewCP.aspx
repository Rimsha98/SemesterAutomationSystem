<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultViewCP.aspx.cs" Inherits="UokSemesterSystem.ResultViewCP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .auto-style2 {
            width: 902px;
            height: 161px;
        }
        .auto-style3 {
            margin-left: 671px;
        }
        .auto-style4 {
            margin-left: 667px;
        }

        * {
    box-sizing: border-box;
}

body {
    padding: 0;
    margin: 0;
    font-family: 'Segoe UI';
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


.Border-Line, .Border-Line2 {
    background-color: #707070;
    width: 100%;
    height: 1%;
}

.Border-Line {
    background-color: #707070;
    width: 100%;
    height: 1%;
}

.Border-Line2 {
    width: 98.7%;
    position: absolute;
    bottom: 0;
}
#Label1 { font-size: 2vw; font-weight: bold; font-family: Arial; }
#Label2, #Label3 { font-size: 1.2vw; font-weight: bold; font-family: Arial;}
.labelcls { font-size: 1.2vw; }
.labelstyle {
    font-weight: 600; color: black; text-decoration: underline; padding: 0.2vw; font-size: 1.2vw;
}

#dt { 
    width: 100%; text-align: center; font-size: 1.1vw; border: 0.1vw solid #cccccc;
}


.sno, .seatno, .name, .totalmarks, .totalinwords, .gpa, .remarks, .theory, .lab {
    background-color:  #D1E8F1;
    font-size: 1vw;
    color:black;
     border-bottom: 0.1vw solid #cccccc;
}
.sno, .gpa, .theory, .lab, .totalmarks  { width: 7%; }
.seatno{ width: 10%; }
.remarks { width: 15%; }
.totalinwords { width: 15%; }

#btn_edit:hover {background-color: #1D8AB5;}

.buttonsFVR {
    float: right;
    width: 8vw;
    height: 2.2vw;
    font-family: Calibri;
    font-size: 1.25vw;
    border: none;
    outline: none;
    cursor: pointer;
    color: #fff;
    background-color: #64C5EB;
    font-weight: 600;
    margin-left: 0.5vw;
}
#approveBtn { width: 12vw; }
.buttonsFVR:hover {background-color: #1D8AB5;}
        </style>
</head>

<body>
   <div id="Nav-Result" style="float:left; width: 15%;"></div>
    <div class="ResultDIV">
        <form id ="form2" runat="server">
        <div style="width: 90%; height: 100vh; margin: 0 auto; ">
            <div style="width: 100%;  text-align: center; padding-top: 5vw; padding-bottom: 2vw">
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
                          <asp:Label ID="Label4" runat="server" Text="Department:   "></asp:Label>
            <asp:Label ID="lbl_deptName" runat="server" Text="dept Here" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                    <tr>
                       <td>
                          <asp:Label ID="Label6" runat="server" Text="Major Department:  "></asp:Label>
            <asp:Label ID="lbl_majDeptName" runat="server" Text="dept again" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                    <tr>
                       <td>
                           <asp:Label ID="Label8" runat="server" Text="Teacher's Name:    "></asp:Label>
            <asp:Label ID="lbl_TeacherName" runat="server" Text="name" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                    <tr>
                       <td>
                          <asp:Label ID="Label10" runat="server" Text="Course No.    "></asp:Label>
            <asp:Label ID="lbl_CourseNum" runat="server" Text="course num here" CssClass="labelstyle"></asp:Label>
                       </td>
                   </tr>
                    <tr>
                       <td>
                           <asp:Label ID="Label21" runat="server" Text="Credit Hours:   "></asp:Label>
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

               <asp:Label ID="Label12" runat="server" Text="Class:   "></asp:Label>
            <asp:Label ID="lbl_className" runat="server" Text="class" CssClass="labelstyle"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
<asp:Label ID="Label14" runat="server" Text="Year:   "></asp:Label>
            <asp:Label ID="lbl_dYear" runat="server" Text="0000" CssClass="labelstyle"></asp:Label>
            &nbsp;&nbsp;&nbsp;
           <asp:Label ID="Label16" runat="server" Text="Semester:   "></asp:Label>
            <asp:Label ID="lbl_semNo" runat="server" Text="sem num" CssClass="labelstyle"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
<asp:Label ID="Label18" runat="server" Text="Date Examination Held:   "></asp:Label>
            <asp:Label ID="eDate" runat="server" CssClass="labelstyle"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
<asp:Label ID="Label19" runat="server" Text="Course Title:   "></asp:Label>
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
                 <asp:Table ID="dt" runat="server" CellSpacing="0">
           
        </asp:Table>
                <br />
                <asp:Button Id="approveBtn" CssClass="buttonsFVR"  Text="Approve Result" runat="server" OnClick="approve_Click" />
                <asp:Button id="backbtn" CssClass="buttonsFVR" runat="server" Text="Back" OnClick="back_btn"/>
            </div>
            <br />

            <div align="bottom">
              <asp:Label runat="server" Text="Chairperson: " style="font-size: 1.2vw; font-weight: normal;" ></asp:Label>
         <asp:Label runat="server" Text="________________"  ID="signLbl" style="text-decoration: underline; font-size: 1.2vw; font-weight: 600;"></asp:Label>
            <br />
               <asp:Label ID="Label22" runat="server" style="font-size: 1.2vw; font-weight: normal;"  Text="Date: "></asp:Label>
               <asp:Label ID="dateLbl" runat="server"  style="text-decoration: underline; font-size: 1.2vw; font-weight: 600;" Text="________________"></asp:Label>
            <br />       
        <br />
               <br />
                 <button id="HiddenBTN1" type="button" style="display: none;"></button>
                </div>
        </div>
        
    </form>
        
    </div>
     <script>
    $(function () {
        $("#Nav-Result").load("NavigationBar.aspx");
    });

        
         $(document).ready(function () {
             document.getElementById("HiddenBTN1").click();
         });

         $("#HiddenBTN1").on("click", function () {
             sessionStorage.setItem('ResultViewCP', 'loaded');
         });
        </script>
   
</body>







</html>
