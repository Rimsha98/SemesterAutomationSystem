<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinalViewResult.aspx.cs" Inherits="UokSemesterSystem.FinalViewResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title>Class Result | UOK</title>
    <style type="text/css">

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

#Label1 { font-size: 2vw; font-weight: bold; font-family: Arial; }
#Label2, #Label3 { font-size: 1.2vw; font-weight: bold; font-family: Arial;}
.labelcls { font-size: 1.2vw; }
.labelstyle {
    font-weight: 600; color: black; text-decoration: underline; padding: 0.2vw; font-size: 1.2vw;
}

#dt { 
    width: 100%; text-align: center; font-size: 1.2vw; border: 0.1vw solid #cccccc; font-family: Calibri;
}


.sno, .seatno, .name, .totalmarks, .totalinwords, .gpa, .remarks, .theory, .lab {
    background-color: #D1E8F1;
    font-size: 1.2vw;
    color:black;
    border-bottom: 0.1vw solid #cccccc;
    padding: 0.5vw;
}
.sno, .gpa, .theory, .lab, .totalmarks  { width: 7%; }
.seatno{ width: 10%; }
.remarks { width: 15%; }
.totalinwords { width: 15%; padding: 0.5vw;}

.backcell { padding: 0.5vw; font-family: Calibri; font-size: 1.2vw; }

.buttonsFVR {
    float: right;
    width: 12vw;
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
.buttonsFVR:hover {background-color: #1D8AB5;}


        </style>
</head>
<body>
   <div id="Nav-Result" style="float:left; width: 15%;"></div>
    <div class="ResultDIV">
        <form id ="form1" runat="server">
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
            <asp:Label ID="eDate" runat="server" CssClass="labelstyle"></asp:Label>
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
                <br />
                <asp:Button ID="sendToCh" runat="server" CssClass="buttonsFVR" OnClick="Button2_Click" Text="Result has been sent to Chairperson"/>
                <asp:Button ID="sendToStd" runat="server" CssClass="buttonsFVR" Text="Send to Students" OnClick="sendToStd_Click" Visible="true"  />
                <asp:Button ID="btn_edit" runat="server" CssClass="buttonsFVR" Text="Edit Result" OnClick="btn_edit_Click" />
                <asp:Label ID="chMsg" runat="server" Text="Result has been sent to Chairperson"  Visible="false" CssClass="auto-style4" style="float: right; color: red; font-size: 1.2vw;"></asp:Label>
               
                <br />
                  <div aling="bottom">
                     
              <asp:Label runat="server" Text="Chairperson: " style="font-size: 1.2vw; font-weight: normal;" ></asp:Label>
         <asp:Label runat="server" Text="________________"  ID="signLbl" style="text-decoration: underline; font-size: 1.2vw; font-weight: 600;"></asp:Label>
            <br />
               <asp:Label ID="Label22" runat="server" style="font-size: 1.2vw; font-weight: normal;"  Text="Date: "></asp:Label>
               <asp:Label ID="dateLbl" runat="server"  style="text-decoration: underline; font-size: 1.2vw; font-weight: 600;" Text="________________"></asp:Label>
       <br /> <asp:Label ID="lbl" runat="server" style="font-size: 1.2vw; float: right; color: red;"></asp:Label>
                </div>
            
            </div>

            



            <!-------------------------------------------------------------------
    

   
        <div class="auto-style2">
            <asp:Button ID="Button2" runat="server" CssClass="auto-style4" Height="25px" OnClick="Button2_Click" Text="Send to Chairman" Width="124px" />
            <asp:Button ID="Button1" runat="server" CssClass="auto-style3" Height="25px" Text="Send to Students" Width="119px" />
        </div> ----------->
            <button id="fvr" type="button" style="display: none"></button>
            <br /><br /><br /><br /><br /><br />
        </div>
        
    </form>
        
    </div>
     <script>
    $(function () {
        $("#Nav-Result").load("NavigationBar.aspx");
    });

         $(document).ready(function () {
             document.getElementById("fvr").click();
         });

         $("#fvr").on("click", function () {
             sessionStorage.setItem("FinalViewResult", "loaded");
         });
        </script>
   
</body>
</html>
