<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountSetting.aspx.cs" Inherits="UokSemesterSystem.AccountSetting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="~/css/AccountSettings.css" />
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title>Account Settings | UOK</title>
   <script type="text/javascript">
        function Navigate() {
            history.go(-1);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!------------ Navigation Bar -------------->
        <div id="Nav-Settings" style="float:left; width: 15%;"></div>

        <!------------ Main Panel ------------------>
        <div style="float: left; width: 85%; height: 100vh; overflow: hidden;">
            <button id="HiddenBTN5" type="button" style="display: none;"></button>

            <div class="AccountSettingsHead">
                <h1>Account Settings</h1>
            </div>

            <div style="width: 80%; margin: 0 auto;">
                <!----------------- Change Profile Picture ------------------>
                <div class="Left-Panel">
                    <h1>Change Profile Picture</h1>
                    <img id="blah" src="#" alt="Student Image" class="InputImage" runat="server"/><br />
                    <label for="imgInp" class="ChooseFile">Select Image</label><br />
                    <input type='file' id="imgInp" runat="server" accept=".jpg,.jpeg,.png" onchange="validateFileType()"/>
                    <asp:Button ID="DummyButton" runat="server" OnClick="DummyButton_Click" CssClass="DummyBTN" />
                    <asp:Button ID="updatePic" runat="server" Text="Upload Picture" OnClick="updatePic_Click"  /><br />
                    <div class="margin"></div>
                    <label id="picError" class="errors" runat="server" visible="false">Please select an image!</label>
                    <label id="picTypeError" class="errors" runat="server" visible="false">Image type must be JPG/JPEG or PNG</label>
                    <label id="picSaved" class="errors" runat="server" visible="false">Profile Picture changed successfully.</label>
                </div>

                <!------------------ Change Password ----------------------->
                <div class="Right-Panel">
                    <h1>Change Account Password</h1>
                    <div id="pwdDiv" runat="server">
                        <div class="Custom-TextBox">
                            <div class="left"><asp:TextBox ID="pwd" TextMode="Password" runat="server" placeholder="kindly enter your password to proceed" onfocus="TBFocus(1)" ></asp:TextBox></div>
                            <div class="right"><img id="PasswordIco0" class="PasswordIco" src="appImages/passiconew.png" onclick="Toggle_Password(0)"/></div>
                        </div>
                        <asp:Button ID="verifyPwd" runat="server" Text="Verify my Password" OnClick="verifyPwd_Click"  /><br />
                        <div class="margin"></div>
                        <label id="VerifyPass" runat="server" visible="false" style="font-size: 1.2vw;"><span style="color: Red">Wrong Password! </span>Try entering it again.</label>
                    </div>

                    <div id="CPTable" visible="false" runat="server">
                        <p><b>Note:</b> Password must be <b>alphanumeric</b>, greater than<b> 6 characters</b> and less than <b>15 characters.</b></p>
                        <div style="margin-bottom: 5%;"></div>
                        <label>Enter New Password: </label><br />
                        <div class="margin"></div>
                        <div class="Custom-TextBox">
                            <div class="left"><asp:TextBox ID="newPwd" TextMode="Password" runat="server" placeholder="enter your password" onfocus="TBFocus(2)" MaxLength="100"></asp:TextBox></div>
                            <div class="right"><img id="PasswordIco1" class="PasswordIco" src="appImages/passiconew.png" onclick="Toggle_Password(1)"/></div>
                        </div>
                        <asp:Label ID="p1" CssClass="errors2" runat="server" ForeColor="Red"></asp:Label><br />
                        <label>Re-Enter New Password: </label><br />
                        <div class="margin"></div>
                        <div class="Custom-TextBox">
                            <div class="left"><asp:TextBox ID="newPwd1" TextMode="Password"  runat="server" placeholder="re-enter your password" onfocus="TBFocus(3)" MaxLength="100"></asp:TextBox></div>
                            <div class="right"><img id="PasswordIco2" class="PasswordIco"" src="appImages/passiconew.png" onclick="Toggle_Password(2)"/></div>
                        </div>
                        <asp:Label ID="p2" CssClass="errors2" runat="server" ForeColor="Red"></asp:Label><br />
                        
                        <asp:Button ID="updatePwd" runat="server" Text="Update Changes" OnClick="updatePwd_Click"/><br />
                        <div class="margin"></div>
                        <label id="CPError" style="margin-right: 5vw; color: red;" runat="server" visible="false">Passwords do not match!</label>
                    </div>
                    <label id="CPSaved"  style="margin-right: 5vw; color: red;" runat="server" visible="false">All changes saved successfully.</label>
            </div>
            </div>








                <label id="lbltestt"></label>
                <!-------------------- Account Settings 1: Student/Teacher/ChairPerson ------------------->
                <div id="AccSet1" visible="true" runat="server">

                    <!----------------- Change Profile Picture -------------------->
                    
                    

                    <!---------------- Change Password ------------------>
                    

                    <!---------------- Other Information ------------------>

                    <div>
                        <div id="setting" visible="true" runat="server" style="display: none;">
                            <div id="ChangeOtherhead" style="display: none;">Other Information</div>

                            <table id="PITable" runat="server">
                                <tr>
                                    <td style="width: 16vw;"><label>Username: </label></td>
                                    <td><asp:TextBox ID="uname" runat="server" placeholder="enter your username" onfocus="TBFocus(2)" MaxLength="50"></asp:TextBox>
                                    <br />
                                    <label id="UsernameError" class="errors" runat="server" visible="false">username must be alphanumeric.</label>
                                    </td>
                                </tr>
               
                                <tr runat="server" Id="ContactNumber">
                                    <td ><label>Contact Number: </label></td>
                                    <td><asp:TextBox ID="number" runat="server" placeholder="enter your mobile number" onfocus="TBFocus(4)" MaxLength="12"></asp:TextBox>
                                    <br />
                                    <label id="NumberError" class="errors" runat="server" visible="false">number not in correct format.</label>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td></td>
                                    <td>
                                    <label id="PISaved" class="errors" runat="server" visible="false">All changes saved successfully.</label>
                                    <br />
                                    </td>
                                </tr>
                        </table>
                    </div>
                </div>
            </div>
        
            <!-------------------- Account Settings 2: Administrator ---------------->

            </div>

    </form>

    <script type="text/javascript">
        $(function () {
            $("#Nav-Settings").load("NavigationBar.aspx");
        });

        $(document).ready(function () {
            document.getElementById("HiddenBTN5").click();
        });

        $("#HiddenBTN5").on("click", function () {
            sessionStorage.setItem("test1", "lorem");
        });

        function validateFileType() {
            var fileName = document.getElementById("imgInp").value;
            var idxDot = fileName.lastIndexOf(".") + 1;
            var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
            if (extFile == "jpg" || extFile == "jpeg" || extFile == "png") {
                //TO DO
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
                document.forms["form1"]["pwd"].placeholder = "kindly enter your password to proceed";
                document.forms["form1"]["pwd"].style.backgroundColor = "#fff";
            }
            if (temp == 2) {
                document.forms["form1"]["newPwd"].placeholder = "enter your password";
                document.forms["form1"]["newPwd"].style.backgroundColor = "#fff";
            }

            if (temp == 3) {
                document.forms["form1"]["newPwd1"].placeholder = "re-enter your password";
                document.forms["form1"]["newPwd1"].style.backgroundColor = "#fff";
            }
        }


        function Toggle_Password(value) {
            var temp;
            if (value == 0) {
                temp = document.getElementById("pwd");
                if (temp.type === "password") {
                    temp.type = "text";
                    document.getElementById("PasswordIco0").src = "appImages/passopennew.png";
                } else {
                    temp.type = "password";
                    document.getElementById("PasswordIco0").src = "appImages/passiconew.png";
                }
            }
            else if (value == 1) {
                temp = document.getElementById("newPwd");
                if (temp.type === "password") {
                    temp.type = "text";
                    document.getElementById("PasswordIco1").src = "appImages/passopennew.png";
                } else {
                    temp.type = "password";
                    document.getElementById("PasswordIco1").src = "appImages/passiconew.png";
                }

            }
            else if (value == 2) {
                temp = document.getElementById("newPwd1");
                if (temp.type === "password") {
                    temp.type = "text";
                    document.getElementById("PasswordIco2").src = "appImages/passopennew.png";
                } else {
                    temp.type = "password";
                    document.getElementById("PasswordIco2").src = "appImages/passiconew.png";
                }
            }
        }
        </script>
</body>
</html>
