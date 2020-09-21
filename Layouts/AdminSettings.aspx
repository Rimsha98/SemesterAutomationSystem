<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSettings.aspx.cs" Inherits="UokSemesterSystem.AdminSettings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title></title>
    <link rel="stylesheet" runat="server" media="screen" href="~/css/AccountSettings.css" />
    <style>

        #TextBox1, #TextBox2, #TextBox3 {
            font-size: 1.1vw;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Settings" style="float:left; width: 15%;"></div>
        <div   style="float: left; width: 85%; height: 100vh; overflow-x:hidden; overflow-y:hidden">

            <div class="AccountSettingsHead">
                <h1>Account Settings</h1>
            </div>

            <div style="width: 40%; margin: 0 auto; margin-top: 2%" class="temp">
                <h1>Change Account Password</h1>
                        <div id="pwdDiv" runat="server">
                        <div class="Custom-TextBox">
                            <div class="left"><asp:TextBox ID="TextBox1" TextMode="Password" runat="server" placeholder="kindly enter your password to proceed" onfocus="TBFocus(1)" ></asp:TextBox></div>
                            <div class="right"><img id="PasswordIco0" class="PasswordIco" src="appImages/passiconew.png" onclick="Toggle_Password(0)"/></div>
                        </div>
                        <asp:Button ID="VerifyAdminPass" runat="server" Text="Verify my Password" OnClick="VerifyAdminPass_Click"  /><br />
                        <div class="margin"></div>
                        <label id="VerifyPass_Admin" runat="server" visible="false" style="font-size: 1.2vw;"><span style="color: Red">Wrong Password! </span>Try entering it again.</label>
                    </div>

                    <div id="CPTable2" visible="false" runat="server">
                        <p><b>Note:</b> Password must be <b>alphanumeric</b>, greater than<b> 6 characters</b> and less than <b>15 characters.</b></p>
                        <div style="margin-bottom: 5%;"></div>
                        <label>Enter New Password: </label><br />
                        <div class="margin"></div>
                        <div class="Custom-TextBox">
                            <div class="left"><asp:TextBox ID="TextBox2" TextMode="Password" runat="server" placeholder="enter your password" onfocus="TBFocus(2)" MaxLength="100"></asp:TextBox></div>
                            <div class="right"><img id="PasswordIco1" class="PasswordIco" src="appImages/passiconew.png" onclick="Toggle_Password(1)"/></div>
                        </div>
                        <asp:Label ID="p1" CssClass="errors2" runat="server" ForeColor="Red"></asp:Label><br />
                        <label>Re-Enter New Password: </label><br />
                        <div class="margin"></div>
                        <div class="Custom-TextBox">
                            <div class="left"><asp:TextBox ID="TextBox3" TextMode="Password"  runat="server" placeholder="re-enter your password" onfocus="TBFocus(3)" MaxLength="100"></asp:TextBox></div>
                            <div class="right"><img id="PasswordIco2" class="PasswordIco"" src="appImages/passiconew.png" onclick="Toggle_Password(2)"/></div>
                        </div>
                        <asp:Label ID="p2" CssClass="errors2" runat="server" ForeColor="Red"></asp:Label><br />
                        
                        <asp:Button ID="UpdateAdminPass" runat="server" Text="Update Changes" OnClick="UpdateAdminPass_Click"/><br />
                        <div class="margin"></div>
                        <label id="PErr" style="margin-right: 5vw; color: red;" runat="server" visible="false">Passwords do not match!</label>
                    </div>
                    <label id="PSave"  style="margin-right: 5vw; color: red;" runat="server" visible="false">All changes saved successfully.</label>
            <button id="HiddenBTN1" type="button" style="display: none;"></button>
            </div>

        </div>
    </form>

    <script>
        $(function () {
            $("#Nav-Settings").load("AdminNavBar.aspx");
        });

        $(document).ready(function () {
            document.getElementById("HiddenBTN1").click();
        });

        $("#HiddenBTN1").on("click", function () {
            sessionStorage.setItem("AdminSettings", "loaded");
        });


        function TBFocus(temp) {
            if (temp == 1) {
                document.forms["form1"]["TextBox1"].placeholder = "kindly enter your password to proceed";
                document.forms["form1"]["TextBox1"].style.backgroundColor = "#fff";
            }
            if (temp == 2) {
                document.forms["form1"]["TextBox2"].placeholder = "enter your password";
                document.forms["form1"]["TextBox2"].style.backgroundColor = "#fff";
            }

            if (temp == 3) {
                document.forms["form1"]["TextBox3"].placeholder = "re-enter your password";
                document.forms["form1"]["TextBox3"].style.backgroundColor = "#fff";
            }
        }


        function Toggle_Password(value) {
            var temp;
            if (value == 0) {
                temp = document.getElementById("TextBox1");
                if (temp.type === "password") {
                    temp.type = "text";
                    document.getElementById("PasswordIco0").src = "appImages/passopennew.png";
                } else {
                    temp.type = "password";
                    document.getElementById("PasswordIco0").src = "appImages/passiconew.png";
                }
            }
            else if (value == 1) {
                temp = document.getElementById("TextBox2");
                if (temp.type === "password") {
                    temp.type = "text";
                    document.getElementById("PasswordIco1").src = "appImages/passopennew.png";
                } else {
                    temp.type = "password";
                    document.getElementById("PasswordIco1").src = "appImages/passiconew.png";
                }

            }
            else if (value == 2) {
                temp = document.getElementById("TextBox3");
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
