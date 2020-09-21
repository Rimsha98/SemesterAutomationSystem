<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherRegistration.aspx.cs" Inherits="UokSemesterSystem.TeacherRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link media="screen"  runat="server" rel="stylesheet" href="~/css/InsertRecord.css" />
     <script src="jquery/jquery123min.js"></script>
    <title>Insert Teacher Record | UOK</title>
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <script src="jquery/validateTeacher.js"></script>
    <script type="text/javascript">
        function Initialize() {
            localStorage.setItem("hasTeacherCodeRunBefore", true);
            localStorage.setItem('isTnameError', true);
            localStorage.setItem('isContactError', true);
            localStorage.setItem('isDegreeError', true);
            localStorage.setItem('isTemailError', true);
        }

        function UpdateRedo() {
            localStorage.setItem("hasTeacherCodeRunBefore", true);
            localStorage.setItem('isTnameError', false);
            localStorage.setItem('isContactError', false);
            localStorage.setItem('isDegreeError', false);
            localStorage.setItem('isTemailError', false);

            sessionStorage.removeItem("TeacherReg");
            sessionStorage.setItem("TeacherEdit", "loaded");
        }
    </script>

        <style>
        .errorMessage { color: Red; font-size: 1.1vw; }
        .margin {
    margin-top: 1%;
}

        .errors { color: red; font-size: 1.1vw; margin-left: 5%; }

        #StudentRecord { background-color: #64C5EB; }
        #TeacherRecord { background-color: #1D8AB5; }
    </style>
</head>
<body>
     <!------------ Navigation Bar --------------->
    <div id="Load-Navigation-Bar" style="float:left; width: 15%;"></div>

    <!------------ Content Panel --------------->
    <form id="form1" runat="server"  enctype="multipart/form-data"  autocomplete="off">
        <div class="Content">
            <div class="OptionsDIV" id="Options" runat="server">
                <asp:Button ID="StudentRecord" runat="server" CssClass="OptionsButton" Text="Student Record" OnClick="DisplayStudentRegistration" />
                <asp:Button ID="TeacherRecord" runat="server" CssClass="OptionsButton" Text="Teacher Record" />
            </div>
            <div class="OptionsDIV" id="HeadingPage" runat="server" visible="false">
                <h1>Edit Teacher Record</h1>
            </div>

            <div class="Left-Panel">
                <asp:Table runat="server" CssClass="InputFormTable" CellSpacing="10">
                    <asp:TableRow>
                        <asp:TableCell Width="30%">
                            <asp:Label runat="server" Text="Teacher's Name: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="70%">
                            <asp:TextBox ID="txt_name" runat="server" CssClass="InputTextBox" onfocus="TBFocus(1)" placeholder="enter teacher's full name"></asp:TextBox>
                            <span id ="name_error" class="errorMessage"></span>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="30%">
                            <asp:Label runat="server" Text="Department: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="70%">
                            <asp:DropDownList ID="dd_dept" runat="server" CssClass="DropDown" AutoPostBack="True"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="20%">
                            <asp:Label runat="server" Text="Contact: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="80%">
                            <asp:TextBox ID="txt_contact" runat="server" MaxLength="14" CssClass="InputTextBox" onfocus="TBFocus(2)" placeholder="enter contact number"></asp:TextBox>
                            <span id ="contact_error" class="errorMessage"></span>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="20%">
                            <asp:Label runat="server" Text="Degree: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="80%">
                            <asp:TextBox ID="txt_degree" runat="server" CssClass="InputTextBox" onfocus="TBFocus(3)" placeholder="enter degree"></asp:TextBox>
                            <span id ="degree_error" class="errorMessage"></span>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="20%">
                            <asp:Label runat="server" Text="Email ID: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell Width="80%">
                            <asp:TextBox ID="txt_email" runat="server" CssClass="InputTextBox" onfocus="TBFocus(4)" placeholder="enter email ID"></asp:TextBox>
                            <span id ="email_error" class="errorMessage"></span>
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
                <button type="button" onclick="Validate()" id="RegisterFrontEnd" class="RegisterButton" runat="server">Register Teacher</button>
                <button type="button" onclick="ValidateEditTeacher()" id="UpdateFrontEnd" class="RegisterButton" runat="server">Update Teacher</button>
                <br />
                <div style="display: none;">
                    <asp:Button ID="btn_submit" runat="server" CssClass="RegisterButton" OnClick="btn_submit_Click" Text="Register Teacher"/>
                    <asp:Button ID="btn_update" runat="server" OnClick="btn_update_Click" Text="Update Teacher" />
                </div>
                <div class="margin"></div>
                <label id="picError" class="errors" runat="server" visible="false">Please select an image!</label>
                <label id="picTypeError" class="errors" runat="server" visible="false">Image type must be JPG/JPEG or PNG</label><br />
                <asp:Label ID="Label7" runat="server" CssClass="errors"></asp:Label>
                  <button id="HiddenBTN1" type="button" style="display: none;"></button>
            </div>
             
   
            
        </div>
       
    </form>

    <script>

        $(document).ready(function () {
            document.getElementById("HiddenBTN1").click();
        });

        $("#HiddenBTN1").on("click", function () {
            sessionStorage.setItem('TeacherReg', 'loaded');
        });

        $(function () {
            $("#Load-Navigation-Bar").load("AdminNavBar.aspx");
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
                document.getElementById("txt_name").placeholder = "enter teacher's full name";
                document.getElementById("txt_name").style.backgroundColor = "#fff";
            }
            if (temp == 2) {
                document.getElementById("txt_contact").placeholder = "enter contact number";
                document.getElementById("txt_contact").style.backgroundColor = "#fff";
            }
            if (temp == 3) {
                document.getElementById("txt_degree").placeholder = "enter degree";
                document.getElementById("txt_degree").style.backgroundColor = "#fff";
            }
            if (temp == 4) {
                document.getElementById("txt_email").placeholder = "enter email ID";
                document.getElementById("txt_email").style.backgroundColor = "#fff";
            }

        }

        function Validate() {
            var d = document.getElementById("dd_dept");
            var depart = d.options[d.selectedIndex].text;

            var count = 0;
            if (localStorage.getItem("isTnameError") === 'true')
                count++;
            if (localStorage.getItem("isContactError") === 'true')
                count++;
            if (localStorage.getItem("isDegreeError") === 'true')
                count++;
            if (localStorage.getItem("isTemailError") === 'true')
                count++;

            if (depart == "Choose Dept")
                count++;

            if (count > 0) {
                document.getElementById("Label7").innerHTML = "Kindly re-check your input fields";
            }
            else {
                $('#btn_submit').trigger('click');
            }

        }




        function ValidateEditTeacher() {
            var d = document.getElementById("dd_dept");
            var depart = d.options[d.selectedIndex].text;

            var count = 0;
            if (localStorage.getItem("isTnameError") === 'true')
                count++;
            if (localStorage.getItem("isContactError") === 'true')
                count++;
            if (localStorage.getItem("isDegreeError") === 'true')
                count++;
            if (localStorage.getItem("isTemailError") === 'true')
                count++;

            if (depart == "Choose Dept")
                count++;

            if (count > 0) {
                document.getElementById("Label7").innerHTML = "Kindly re-check your input fields";
            }
            else {
                $('#btn_update').trigger('click');
            }

        }
          </script>
</body>
</html>