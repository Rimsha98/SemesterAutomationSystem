<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentRegistration.aspx.cs" Inherits="UokSemesterSystem.StudentRegistration" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link media="screen"  runat="server" rel="stylesheet" href="~/css/InsertRecord.css" />
    <script src="jquery/jquery123min.js"></script>
    <title>Insert Student Record | UOK</title>
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <script src="jquery/validateStudent.js"></script>
    <script type="text/javascript">
        function Initialize() {
            localStorage.setItem("hasCodeRunBefore", true);
            localStorage.setItem('isSnameError', true);
            localStorage.setItem('isFnameError', true);
            localStorage.setItem('isEnumError', true);
            localStorage.setItem('isRnumError', true);
            localStorage.setItem('isYearError', true);
            localStorage.setItem('isEmailError', true);
        }

        function UpdateRedo() {
            localStorage.setItem("hasCodeRunBefore", true);
            localStorage.setItem('isSnameError', false);
            localStorage.setItem('isFnameError', false);
            localStorage.setItem('isEnumError', false);
            localStorage.setItem('isRnumError', false);
            localStorage.setItem('isYearError', false);
            localStorage.setItem('isEmailError', false);

            sessionStorage.removeItem("StudentReg");
            sessionStorage.setItem("StudentEdit","loaded");
        }
    </script>
    <style>
        .errorMessage { color: Red; font-size: 1.1vw; }
        .margin {
    margin-top: 1%;
}

        .errors { color: red; font-size: 1.1vw; margin-left: 5%; }
    </style>
</head>

<body>
    <!------------ Navigation Bar --------------->
    <div id="Load-Navigation-Bar" style="float:left; width: 15%;"></div>

    <!------------ Content Panel --------------->
    <form id="form1" runat="server"  enctype="multipart/form-data" autocomplete="off">
        <div class="Content">
            <div class="OptionsDIV" id="Options" runat="server">
                <asp:Button ID="StudentRecord" runat="server" CssClass="OptionsButton" Text="Student Record" />
                <asp:Button ID="TeacherRecord" runat="server" CssClass="OptionsButton" Text="Teacher Record" OnClick="DisplayTeacherRegistration"/>
            </div>
            <div class="OptionsDIV" id="HeadingPage" runat="server" visible="false">
                <h1>Edit Student Record</h1>
            </div>
            <div class="Left-Panel">
                <asp:Table runat="server" CssClass="InputFormTable" CellSpacing="10">
                    <asp:TableRow>
                        <asp:TableCell Width="30%">
                            <asp:Label runat="server" Text="Student Name: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="70%">
                            <asp:TextBox ID="txt_name" runat="server" CssClass="InputTextBox" onfocus="TBFocus(1)" placeholder="enter student's full name"></asp:TextBox>
                            <span id ="name_error" class="errorMessage"></span>
                            <span id="err"></span>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="30%">
                            <asp:Label runat="server" Text="Father's Name: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="70%">
                            <asp:TextBox ID="txt_fname" runat="server" CssClass="InputTextBox" onfocus="TBFocus(2)" placeholder="enter father's name"></asp:TextBox>
                            <span id ="fname_error" class="errorMessage"></span>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="20%">
                            <asp:Label runat="server" Text="Enrolment No: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="80%">
                            <asp:TextBox ID="txt_enum" runat="server" CssClass="InputTextBox" onfocus="TBFocus(3)" placeholder="enter enrolment number"></asp:TextBox>
                            <span id ="enum_error" class="errorMessage"></span>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="20%">
                            <asp:Label runat="server" Text="Seat Number: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="80%">
                            <asp:TextBox ID="txt_rnum" runat="server" CssClass="InputTextBox" onfocus="TBFocus(4)" placeholder="enter seat number"></asp:TextBox>
                            <span id ="rnum_error" class="errorMessage"></span>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="20%">
                            <asp:Label runat="server" Text="Department: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="80%">
                            <asp:DropDownList ID="dd_dept" runat="server" CssClass="DropDown" AutoPostBack="True" OnSelectedIndexChanged="dd_dept_SelectedIndexChanged"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="20%">
                            <asp:Label runat="server" Text="Shift: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="80%">
                           
                        <asp:DropDownList ID="dd_shift" CssClass="DropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dd_shift_SelectedIndexChanged" ></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="20%">
                            <asp:Label runat="server" Text="Class: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="80%">
                             <asp:DropDownList ID="dd_class" runat="server" CssClass="DropDown" AutoPostBack="True" OnSelectedIndexChanged="dd_class_SelectedIndexChanged"></asp:DropDownList>
                            
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="20%">
                            <asp:Label runat="server" Text="Major: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="80%">
                            <asp:DropDownList ID="dd_major" CssClass="DropDown" runat="server" AutoPostBack="True"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="20%">
                            <asp:Label runat="server" Text="Year: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="80%">
                            <asp:TextBox ID="txt_year" CssClass="InputTextBox" runat="server" onfocus="TBFocus(5)" placeholder="enter year of enrolment"></asp:TextBox>
                            <span id="year_error" class="errorMessage"></span>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="20%">
                            <asp:Label runat="server" Text="Section: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="80%">
                            <asp:DropDownList ID="dd_section" runat="server" CssClass="DropDown" AutoPostBack="True" OnSelectedIndexChanged="dd_section_SelectedIndexChanged"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="20%">
                            <asp:Label runat="server" Text="Email ID: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="80%">
                            <asp:TextBox ID="txt_email" CssClass="InputTextBox" runat="server" onfocus="TBFocus(6)" placeholder="enter student's email ID"></asp:TextBox>
                            <span id="email_error" class="errorMessage"></span>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>

            <div class="Right-Panel">
               <img id="blah" src="~/appImages/DummyUserImage.png" alt="Student Image" class="InputImage" runat="server"/><br />
                <label for="imgInp" class="ChooseFile">Select Image</label><br />
                <input type='file' id="imgInp" runat="server" accept=".jpg,.jpeg,.png" onchange="validateFileType()"/>
                <asp:Button ID="DummyButton" runat="server" OnClick="DummyButton_Click" CssClass="DummyBTN" style="display: none" />
                <div class="margin"></div>
                <button type="button" id="RegisterFrontEnd" onclick="Validate()" class="RegisterButton" runat="server">Register Student</button>
                <button type="button" id="UpdateFrontEnd" onclick="ValidateEditStudent()" class="RegisterButton" runat="server">Update Student</button>
                <br />
                <div style="display: none;">
                    <asp:Button ID="btn_submit" runat="server" CssClass="RegisterButton" OnClick="btn_submit_Click" Text="Register Student"/>
                    <asp:Button ID="btn_update" runat="server" CssClass="RegisterButton" OnClick="btn_update_Click" Text="Update Student" />
                </div>
                <div class="margin"></div>
                <label id="picError" class="errors" runat="server" visible="false">Please select an image!</label>
                <label id="picTypeError" class="errors" runat="server" visible="false">Image type must be JPG/JPEG or PNG</label><br />
                <asp:Label ID="lblSuc" runat="server" CssClass="errors" ></asp:Label>
            </div>
            <button id="HiddenBTN1" type="button" style="display: none;"></button>
        </div>
       
    </form>
     <script>
         $(function () {
             $("#Load-Navigation-Bar").load("AdminNavBar.aspx");
         });

         $(document).ready(function () {
             document.getElementById("HiddenBTN1").click();
         });

         $("#HiddenBTN1").on("click", function () {
             sessionStorage.setItem('StudentReg', 'loaded');
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

         function TBFocus(temp) {
             if (temp == 1) {
                 document.getElementById("txt_name").placeholder = "enter student's full name";
                 document.getElementById("txt_name").style.backgroundColor = "#fff";
             }
             if (temp == 2) {
                 document.getElementById("txt_fname").placeholder = "enter father's name";
                 document.getElementById("txt_fname").style.backgroundColor = "#fff";
             }
             if (temp == 3) {
                 document.getElementById("txt_enum").placeholder = "enter enrolment number";
                 document.getElementById("txt_enum").style.backgroundColor = "#fff";
             }
             if (temp == 4) {
                 document.getElementById("txt_rnum").placeholder = "enter seat number";
                 document.getElementById("txt_rnum").style.backgroundColor = "#fff";
             }
             if (temp == 5) {
                 document.getElementById("txt_year").placeholder = "enter year of enrolment";
                 document.getElementById("txt_year").style.backgroundColor = "#fff";
             }
             if (temp == 6) {
                 document.getElementById("txt_email").placeholder = "enter student's email ID";
                 document.getElementById("txt_email").style.backgroundColor = "#fff";
             } 
         }

         function Validate() {
             var d = document.getElementById("dd_dept");
             var depart = d.options[d.selectedIndex].text;

             var c = document.getElementById("dd_class");
             var classs = c.options[c.selectedIndex].text;

             var s = document.getElementById("dd_section");
             var section = s.options[s.selectedIndex].text;

             var m = document.getElementById("dd_major");
             var major = m.options[m.selectedIndex].text;

             var sh = document.getElementById("dd_shift");
             var shift = sh.options[sh.selectedIndex].text;


             var count = 0;
             if (localStorage.getItem("isSnameError") === 'true')
                 count++;
             if (localStorage.getItem("isFnameError") === 'true')
                 count++;
             if (localStorage.getItem("isEnumError") === 'true')
                 count++;
             if (localStorage.getItem("isRnumError") === 'true')
                 count++;
             if (localStorage.getItem("isYearError") === 'true')
                 count++;
             if (localStorage.getItem("isEmailError") === 'true')
                 count++;

             if (depart == "Choose Dept")
                 count++;
             if (classs == "Choose Class")
                 count++;
             if (section == "Choose Section")
                 count++;
             if (major == "Choose Major")
                 count++;
             if (shift == "Choose Shift")
                 count++;


             if (count > 0) {
                 document.getElementById("lblSuc").innerHTML = "Kindly re-check your input fields";
             }
             else {
                 $('#btn_submit').trigger('click');
             }
                 
         }





         function ValidateEditStudent() {
             var d = document.getElementById("dd_dept");
             var depart = d.options[d.selectedIndex].text;

             var c = document.getElementById("dd_class");
             var classs = c.options[c.selectedIndex].text;

             var s = document.getElementById("dd_section");
             var section = s.options[s.selectedIndex].text;

             var m = document.getElementById("dd_major");
             var major = m.options[m.selectedIndex].text;

             var sh = document.getElementById("dd_shift");
             var shift = sh.options[sh.selectedIndex].text;


             var count = 0;
             if (localStorage.getItem("isSnameError") === 'true')
                 count++;
             if (localStorage.getItem("isFnameError") === 'true')
                 count++;
             if (localStorage.getItem("isEnumError") === 'true')
                 count++;
             if (localStorage.getItem("isRnumError") === 'true')
                 count++;
             if (localStorage.getItem("isYearError") === 'true')
                 count++;
             if (localStorage.getItem("isEmailError") === 'true')
                 count++;

             if (depart == "Choose Dept")
                 count++;
             if (classs == "Choose Class")
                 count++;
             if (section == "Choose Section")
                 count++;
             if (major == "Choose Major")
                 count++;
             if (shift == "Choose Shift")
                 count++;


             if (count > 0) {
                 document.getElementById("lblSuc").innerHTML = "Kindly re-check your input fields";
             }
             else {
                 $('#btn_update').trigger('click');
             }

         }
          </script>
</body>
</html>
