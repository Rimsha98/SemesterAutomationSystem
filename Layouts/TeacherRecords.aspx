<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherRecords.aspx.cs" Inherits="UokSemesterSystem.TeacherRecords" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery340.js"></script>
    <title></title>

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
                  margin-top: 1.7%;
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

        .left { float: left; width: 50%; height: 60vh; background-color: #fcfcfc; padding: 2%; border: 0.1vw solid #cccccc; border-right: none; }
        .right { float: left; width: 50%; height: 60vh;  background-color: #fcfcfc;  padding: 2%; border: 0.1vw solid #cccccc; border-left: none; }

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
            width: 90%;
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
            <div class="Selection">
                <asp:Button ID="RecordsBTN" runat="server" CssClass="SelectionCSS" OnClick="RecordsBTN_Click" Text="Teacher Records"/>
                <asp:Button ID="InsertEditBTN" runat="server" CssClass="SelectionCSS" OnClick="InsertEditBTN_Click" Text="Insert Record"/>
            </div>

            <div class="Records" id="TeacherData" runat="server">
                <asp:Table ID="TeacherTable" runat="server" CssClass="Table" CellSpacing="0">
                    <asp:TableRow CssClass="HeaderRow">
                        <asp:TableCell CssClass="headcell">S.no</asp:TableCell>
                        <asp:TableCell CssClass="headcell">Teacher Name</asp:TableCell>
                        <asp:TableCell CssClass="headcell">Department</asp:TableCell>
                        <asp:TableCell CssClass="headcell">Email</asp:TableCell>
                        <asp:TableCell CssClass="headcell"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br /><br />
            </div>

             <div class="Form" id="Teacher" runat="server" visible="false">
                <div class="left">
                    <label for="teacher_name">Teacher Name:</label><br />
                    <asp:TextBox ID="teacher_name" runat="server" onfocus="TBFocus(1)" CssClass="TextBox" placeholder="enter teacher name" AutoCompleteType="Disabled"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="req_tname" runat="server" Display="Dynamic" CssClass="errorempty" ErrorMessage="must fillout this field" ControlToValidate="teacher_name" ValidationGroup="One"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="tname_error" runat="server" Display="Dynamic" CssClass="error" ErrorMessage="name must contain alphabets only" ControlToValidate="teacher_name" ValidationExpression="^[a-zA-Z .]*$" ValidationGroup="One"></asp:RegularExpressionValidator>
                    <div class="margin"></div>

                    <label for="department_select">Department:</label><br />
                    <asp:DropDownList ID="department_select" runat="server" AutoPostBack="true" CssClass="DropDown" OnSelectedIndexChanged="department_select_SelectedIndexChanged">
                        <asp:ListItem Selected="True">select department</asp:ListItem>
                    </asp:DropDownList><br />
                    <asp:CompareValidator ID="depart_error" runat="server" Display="Dynamic" SetFocusOnError="true" Operator="NotEqual" ControlToValidate="department_select" ErrorMessage="Department Required" CssClass="error" ValueToCompare="select department" ValidationGroup="One"></asp:CompareValidator>
                    <div class="margin"></div>

                    <label for="teacher_contact">Contact Number:</label><br />
                    <asp:TextBox ID="teacher_contact" runat="server" onfocus="TBFocus(5)" CssClass="TextBox" placeholder="enter teacher's mobile number" AutoCompleteType="Disabled" MaxLength="15"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="req_year" runat="server" Display="Dynamic" CssClass="errorempty" ErrorMessage="must fillout this field" ControlToValidate="teacher_contact" ValidationGroup="One"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="contact_error" runat="server" Display="Dynamic" CssClass="error" ErrorMessage="contact must contain numbers only" ControlToValidate="teacher_contact" ValidationExpression="^[0-9]*$" ValidationGroup="One"></asp:RegularExpressionValidator>
                    <div class="margin"></div>

                    <label for="degree">Degree:</label><br />
                    <asp:TextBox ID="degree" runat="server" onfocus="TBFocus(3)" CssClass="TextBox" placeholder="enter teacher's degree" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="req_deg" runat="server" Display="Dynamic" CssClass="errorempty" ErrorMessage="must fillout this field" ControlToValidate="degree" ValidationGroup="One"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="deg_error" runat="server" Display="Dynamic" CssClass="error" ErrorMessage="degree contains invalid characters" ControlToValidate="degree" ValidationExpression="^[a-zA-Z0-9 ._/-]*$" ValidationGroup="One"></asp:RegularExpressionValidator>
                    <div class="margin"></div>

                    <label for="teacher_email">Teacher's Email ID:</label><br />
                    <asp:TextBox ID="teacher_email" runat="server" onfocus="TBFocus(6)" CssClass="TextBox" placeholder="enter teacher's email ID" AutoCompleteType="Disabled"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="req_email" runat="server" Display="Dynamic" CssClass="errorempty" ErrorMessage="must fillout this field" ControlToValidate="teacher_email" ValidationGroup="One"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="email_error" Display="Dynamic" runat="server" CssClass="error" ErrorMessage="Invalid email format" ControlToValidate="teacher_email" ValidationExpression='^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$' ValidationGroup="One"></asp:RegularExpressionValidator>
                    <div class="margin"></div>
                </div>

                 <div class="right">
                    <label>Teacher's Picture:</label><br />
                    <img id="blah" src="~/appImages/DummyUserImage.png" alt="Teacher Image" class="InputImage" runat="server"/><br />
                    <label for="imgInp" class="ChooseFile">Select Image</label><br />
                    <input type='file' id="imgInp" runat="server" accept=".jpg,.jpeg,.png" onchange="validateFileType()"/>
                    <asp:Button ID="DummyButton" runat="server" OnClick="DummyButton_Click" CssClass="DummyBTN" style="display: none" />
                    <label id="picError" class="errors" runat="server" visible="false">Please select an image!</label>
                    <label id="picTypeError" class="errors" runat="server" visible="false">Image type must be JPG/JPEG or PNG</label><br /><br />
                    <div class="margin"></div>
                    
                    <asp:Button ID="register_teacher" CssClass="Button" runat="server" Text="Register Teacher" OnClick="register_teacher_Click" CausesValidation="true" ValidationGroup="One"/>
                    <asp:Button ID="update_teacher"   CssClass="Button" runat="server" Text="Update Teacher" OnClick="update_teacher_Click" CausesValidation="true" ValidationGroup="One" Visible="false"/>
                    <br />
                    <label id="saved" class="errors" runat="server" visible="false">All changes saved successfully.</label>
                </div>


                 

            </div>
            <button id="HiddenBTN1" type="button" style="display: none;"></button>
</div>
            
    <script>
        $(function () {
            $("#Nav-Records").load("AdminNavBar.aspx");

        });

        $(document).ready(function () {
            document.getElementById("HiddenBTN1").click();
        });

        $("#HiddenBTN1").on("click", function () {
            sessionStorage.setItem("TeacherRecords", "loaded");
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

    </script>
    </form>
</body>
</html>
