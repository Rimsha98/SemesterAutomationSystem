<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentRecords.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="UokSemesterSystem.StudentRecords" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery340.js"></script>
    <title>Records | UOK</title>
    <style>
        * { box-sizing: border-box; }
body {
    padding: 0;
    margin: 0;
    overflow: hidden;
}
.InputImage {
    width: 13vw;
    height: 13vw;
    background-color: #fcfcfc;
    object-fit:cover;
    margin-top: 0.1%; margin-bottom: 1%; 
}

.ChooseFile {
    background-color: #64C5EB;
    display: inline-block;
    cursor: pointer;
    width: 13vw;
    height: 2.3vw;
    text-align: center;
    color: #fff;
    font-size: 1.2vw;
    font-weight: 600;
    padding-top: 1%;
}
.ChooseFile:hover { background-color: #1D8AB5; }

input[type="file"] {
    display: none;
}


        .Form, .Table {
            width: 80%;
            margin: 0 auto;
            font-family: Calibri;
            font-size: 1.1vw;
            margin-top: 2%; 
            height: 100%;
        }
        .Table { width: 90%; border: 0.1vw solid #cccccc; text-align: center; font-size: 1.2vw; }
        .backcell { padding: 1%; }

        .HeaderRow { text-align: center; color: #fff; font-size: 1.2vw; background-color: #1D8AB5;
                      font-weight: 600;
        }

        .headcell { padding: 1%; }

        label { color: #1D8AB5; font-weight: 600; }

        input[type=text]{
            width: 100%;
            height: 2.3vw;
            padding: 0 1% 0 1%;
            margin: 0;
            outline-color:lightblue;
        }

        .Button { width: 100%;  margin: 0; height: 2vw; cursor: pointer; outline-color:lightblue;
                  border: none;
                  color: #fff;
                  background-color: #64C5EB;
                  height: 2.3vw;
                  font-family: Calibri;
                  font-size: 1.1vw;
                  font-weight: bold;
        }
        .Button:hover {  background-color: #1D8AB5 !important; }

         .DropDown { 
             width: 100%;
             height: 2.3vw;
              outline-color:lightblue;
              cursor: pointer;
              padding: 1%;
         }
        .error, .errorempty { 
            font-size: 1vw;
            color: red;
            font-family: Calibri;
            float: right;
        }

        .errors { font-size: 1vw; color: red; font-family: Calibri; }
        .errorempty { }
        
        .margin { margin-top: 5%; }

        .NavigationBar { float: left; width: 15%;  }
        .content { float: left; width: 85%; overflow-x: hidden; overflow-y: scroll; height: 100vh;  }

        .left { float: left; width: 50%; height: 92vh; background-color: #fcfcfc; padding: 2%; border: 0.1vw solid #cccccc; border-right: none; }
        .right { float: left; width: 50%; height: 92vh;  background-color: #fcfcfc;  padding: 2%; border: 0.1vw solid #cccccc; border-left: none; }

        .Selection {
    margin: 0 auto;
    width: 80%;
    height: 2.6vw;
    text-align: center;
    margin-top: 4%;
    padding: 0;
    border-bottom: 0.1vw solid #cccccc;
}

.SelectionCSS{
    border: none;
    background-color: #64C5EB;
    color: #fff;
    font-size: 1.2vw;
    font-weight: 600;
    text-align: center;
    width: 15vw;
    height: 2.5vw;
    cursor: pointer;
    outline: none;
}
.SelectionCSS:hover, .FormButtons:hover {
    background-color: #1D8AB5 !important;
}

.backbtn {
            border: none;
            outline: none;
            cursor: pointer;
            background-color: #1D8AB5;
            color: #fff;
            font-weight: 600;
            width: 80%;
            height: 2vw;
            font-size: 1.2vw;
            font-family: Calibri;
        }

        .backbtn:hover { background-color: #64C5EB; }

    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div id="Nav-Records" class="NavigationBar">nav bar will load here</div>
        <div class="content">
            <button id="HiddenBTN1" type="button" style="display: none;"></button>
            <div class="Selection">
                <asp:Button ID="RecordsBTN" runat="server" CssClass="SelectionCSS" OnClick="RecordsBTN_Click" Text="Student Records"/>
                <asp:Button ID="InsertEditBTN" runat="server" CssClass="SelectionCSS" OnClick="InsertEditBTN_Click" Text="Insert Record"/>
            </div>

            <div class="Records" id="StudentData" runat="server">
                <asp:Table ID="StudentTable" runat="server" CssClass="Table" CellSpacing="0">
                    <asp:TableRow CssClass="HeaderRow">
                        <asp:TableCell CssClass="headcell">S.no</asp:TableCell>
                        <asp:TableCell CssClass="headcell">Enrolment Number</asp:TableCell>
                        <asp:TableCell CssClass="headcell">Student Name</asp:TableCell>
                        <asp:TableCell CssClass="headcell">Father's Name</asp:TableCell>
                        <asp:TableCell CssClass="headcell"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br /><br />
            </div>

            <div class="Form" id="Student" runat="server" visible="false">
                <div class="left">
                    <label for="student_name">Student Name:</label><br />
                    <asp:TextBox ID="student_name" runat="server" onfocus="TBFocus(1)" CssClass="TextBox" placeholder="enter student name" AutoCompleteType="Disabled"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="req_sname" runat="server" Display="Dynamic" CssClass="errorempty" ErrorMessage="must fillout this field" ControlToValidate="student_name" ValidationGroup="One"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="sname_error" runat="server" Display="Dynamic" CssClass="error" ErrorMessage="name must contain alphabets only" ControlToValidate="student_name" ValidationExpression="^[a-zA-Z ]*$" ValidationGroup="One"></asp:RegularExpressionValidator>
                    <div class="margin"></div>

                    <label for="father_name">Father's Name:</label><br />
                    <asp:TextBox ID="father_name" runat="server" onfocus="TBFocus(2)" CssClass="TextBox" placeholder="enter father's name" AutoCompleteType="Disabled"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="req_fname" runat="server" Display="Dynamic" CssClass="errorempty" ErrorMessage="must fillout this field" ControlToValidate="father_name" ValidationGroup="One"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="fname_error" runat="server" Display="Dynamic" CssClass="error" ErrorMessage="name must contain alphabets only" ControlToValidate="father_name" ValidationExpression="^[a-zA-Z ]*$" ValidationGroup="One"></asp:RegularExpressionValidator>
                    <div class="margin"></div>

                    <label for="enrolment_number">Enrolment Number:</label><br />
                    <asp:TextBox ID="enrolment_number" runat="server" onfocus="TBFocus(3)" CssClass="TextBox" placeholder="enter student's enrolment number" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="req_enrol" runat="server" Display="Dynamic" CssClass="errorempty" ErrorMessage="must fillout this field" ControlToValidate="enrolment_number" ValidationGroup="One"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="enum_error" runat="server" Display="Dynamic" CssClass="error" ErrorMessage="enrolment contains invalid characters" ControlToValidate="enrolment_number" ValidationExpression="^[a-zA-Z0-9 _/-]*$" ValidationGroup="One"></asp:RegularExpressionValidator>
                    <div class="margin"></div>

                    <label for="seat_number">Seat Number:</label><br />
                    <asp:TextBox ID="seat_number" runat="server" onfocus="TBFocus(4)" CssClass="TextBox" placeholder="enter student's seat number" AutoCompleteType="Disabled"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="req_seat" runat="server" Display="Dynamic" CssClass="errorempty" ErrorMessage="must fillout this field" ControlToValidate="seat_number" ValidationGroup="One"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="snum_error" runat="server" Display="Dynamic" CssClass="error" ErrorMessage="seat number contains invalid characters" ControlToValidate="seat_number" ValidationExpression="^[a-zA-Z0-9]*$" ValidationGroup="One"></asp:RegularExpressionValidator>
                    <div class="margin"></div>

                    <label for="department_select">Department:</label><br />
                    <asp:DropDownList ID="department_select" runat="server" AutoPostBack="true" CssClass="DropDown" OnSelectedIndexChanged="department_select_SelectedIndexChanged">
                        <asp:ListItem Selected="True">select department</asp:ListItem>
                    </asp:DropDownList><br />
                    <asp:CompareValidator ID="depart_error" runat="server" Display="Dynamic" SetFocusOnError="true" Operator="NotEqual" ControlToValidate="department_select" ErrorMessage="Department Required" CssClass="error" ValueToCompare="select department" ValidationGroup="One"></asp:CompareValidator>
                    <div class="margin"></div>

                    <label for="shift_select">Shift:</label><br />
                    <asp:DropDownList ID="shift_select" runat="server" CssClass="DropDown" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="shift_select_SelectedIndexChanged">
                        <asp:ListItem>Morning</asp:ListItem>
                        <asp:ListItem>Evening</asp:ListItem>
                    </asp:DropDownList><br />
                    <div class="margin"></div>
            
                    <label for="major_select">Major:</label><br />
                    <asp:DropDownList ID="major_select" runat="server" CssClass="DropDown" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="major_select_SelectedIndexChanged">
                        <asp:ListItem>select major</asp:ListItem>
                    </asp:DropDownList><br />
                    <asp:CompareValidator ID="major_error" Display="Dynamic" runat="server" SetFocusOnError="true" Operator="NotEqual" ControlToValidate="major_select" ErrorMessage="Major Required" CssClass="error" ValueToCompare="select major" ValidationGroup="One"></asp:CompareValidator>
                    <div class="margin"></div>
            
                    <label for="class_select">Class:</label><br />
                    <asp:DropDownList ID="class_select" runat="server" CssClass="DropDown" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="class_select_SelectedIndexChanged">
                        <asp:ListItem Selected="True">select class</asp:ListItem>
                    </asp:DropDownList><br />
                    <asp:CompareValidator ID="class_error" Display="Dynamic" runat="server" SetFocusOnError="true" Operator="NotEqual" ControlToValidate="class_select" ErrorMessage="Class Required" CssClass="error" ValueToCompare="select class" ValidationGroup="One"></asp:CompareValidator>
                    <div class="margin"></div>
                </div>

                <div class="right">
                    <label for="student_year">Year:</label><br />
                    <asp:TextBox ID="student_year" runat="server" onfocus="TBFocus(5)" CssClass="TextBox" placeholder="enter student's year of enrolment" AutoCompleteType="Disabled" MaxLength="4"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="req_year" runat="server" Display="Dynamic" CssClass="errorempty" ErrorMessage="must fillout this field" ControlToValidate="student_year" ValidationGroup="One"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="year_error" Display="Dynamic" runat="server" CssClass="errorempty" MaximumValue="2020" MinimumValue="1900" Type="Integer" ErrorMessage="Invalid Year" ControlToValidate="student_year" ValidationGroup="One"></asp:RangeValidator>
                    <div class="margin"></div>

                    <label for="section_select">Section:</label><br />
                    <asp:DropDownList ID="section_select" runat="server" CssClass="DropDown" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="section_select_SelectedIndexChanged">
                        <asp:ListItem Selected="True">select section</asp:ListItem>
                    </asp:DropDownList><br />
                    <asp:CompareValidator ID="section_error" Display="Dynamic" runat="server" SetFocusOnError="true" Operator="NotEqual" ControlToValidate="section_select" ErrorMessage="Section Required" CssClass="error" ValueToCompare="select section" ValidationGroup="One"></asp:CompareValidator>
                    <div class="margin"></div>

                    <label for="student_email">Student's Email ID:</label><br />
                    <asp:TextBox ID="student_email" runat="server" onfocus="TBFocus(6)" CssClass="TextBox" placeholder="enter student's email ID" AutoCompleteType="Disabled"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="req_email" runat="server" Display="Dynamic" CssClass="errorempty" ErrorMessage="must fillout this field" ControlToValidate="student_email" ValidationGroup="One"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="email_error" Display="Dynamic" runat="server" CssClass="error" ErrorMessage="Invalid email format" ControlToValidate="student_email" ValidationExpression='^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$' ValidationGroup="One"></asp:RegularExpressionValidator>
                    <div class="margin"></div>

                    <label>Student's Picture:</label><br />
                    <img id="blah" src="~/appImages/DummyUserImage.png" alt="Student Image" class="InputImage" runat="server"/><br />
                    <label for="imgInp" class="ChooseFile">Select Image</label><br />
                    <input type='file' id="imgInp" runat="server" accept=".jpg,.jpeg,.png" onchange="validateFileType()"/>
                    <asp:Button ID="DummyButton" runat="server" OnClick="DummyButton_Click" CssClass="DummyBTN" style="display: none" />
                    <label id="picError" class="errors" runat="server" visible="false">Please select an image!</label>
                    <label id="picTypeError" class="errors" runat="server" visible="false">Image type must be JPG/JPEG or PNG</label><br />
                    <br /><br /><div style="margin-bottom: 2.3%;"></div>
                    
                    <asp:Button ID="register_student" CssClass="Button" runat="server" Text="Register Student" OnClick="register_student_Click" CausesValidation="true" ValidationGroup="One"/>
                    <asp:Button ID="update_student"   CssClass="Button" runat="server" Text="Update Student" OnClick="update_student_Click" CausesValidation="true" ValidationGroup="One" Visible="false"/><br />
                    <label id="saved" class="errors" runat="server" visible="false">All changes saved successfully.</label>
                    
                </div>
            </div>
        </div>
    </form>

    <script>
        $(function () {
            $("#Nav-Records").load("AdminNavBar.aspx");

        });


        $(document).ready(function () {
            document.getElementById("HiddenBTN1").click();
        });

        $("#HiddenBTN1").on("click", function () {
            sessionStorage.setItem("StudentRecords", "loaded");
        });

        function validateFileType() {
            var fileName = document.getElementById("imgInp").value;
            var idxDot = fileName.lastIndexOf(".") + 1;
            var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
            if (extFile == "jpg" || extFile == "jpeg" || extFile == "png") {
                document.getElementById("picTypeError").style.display = "none";
            } else {
                $('#DummyButton').trigger('click');
            }
        }

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#blah').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }

        $("#imgInp").change(function () {
            readURL(this);
        });


        $(document).ready(function () {
           // document.getElementById("RecordsBTN").style.backgroundColor = "#1D8AB5";
        });
    </script>
</body>
</html>
