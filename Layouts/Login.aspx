<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UokSemesterSystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="~/css/Login.css" />
    <script src="jquery/jquery340.js"></script>
    <title>Login | UOK</title>
</head>

<body>
    <div class="screen">
        <!--------------- Screen Left BG -------------->
        <div class="Left-Panel">
            <div class="Border-Line"></div>
            <div class="overlay">
                <div class="Grid-Container">
                    <div class="Logo">
                        <img src="appImages/KuLogoWhite.png" alt="KU-Logo" style="width: 10.5vw;"/>
                    </div>
                    <div class="LeftText">
                        <h1 id="UOKhead">UNIVERSITY OF KARACHI</h1>
                        <h1 id="UOKtext">ONLINE PORTAL</h1>
                        <div class="line" style="margin-top: 0.4vw;"></div>
                        <p>Welcome to the Official Portal of University of Karachi, unarguably the most renowned academic institution of Pakistan.</p>
                        <div class="line"></div>
                    </div>
                </div>
            </div>
            <div class="Border-Line2"></div>
        </div>

        <!--------------- Screen Right BG -------------->
        <div class="Right-Panel">
            <div class="Border-Line"></div>
            <div class="Border-Line2"></div>
        </div>

        <!--------------- Screen Middle Form -------------->
        <div class="Middle-Panel">
            <form id="form1" runat="server">
                <div class="Login-Container">
                    <div class="heading"><h1>USER LOGIN</h1></div>
                    <div class="username">
                        <div class="Custom-Textbox">
                           <div class="col1"><asp:TextBox ID="Txtusername" runat="server" onfocus="TBFocus(1)" placeholder="username"></asp:TextBox></div>
                           <div class="col2"><asp:Image ID="UsernameIco" runat="server" imageurl="~/appImages/usericon.png"/></div>
                        </div>
                    </div>
                    <div class="password">
                        <div class="Custom-Textbox">
                            <div class="col1"><asp:TextBox ID="Txtpassword" runat="server" onfocus="TBFocus(2)" placeholder="password" TextMode="Password"></asp:TextBox></div>
                            <div class="col2"><img id="PasswordIco" src="appImages/passiconew.png" onclick="Toggle_Password()"/></div>
                        </div>
                    </div>
                    <div class="dropdownfield">
                        <div class="container">
                            <asp:DropDownList ID="DDSelectUser" runat="server">
                            <asp:ListItem>Student</asp:ListItem>
                            <asp:ListItem>Teacher</asp:ListItem>
                            <asp:ListItem>ChairPerson</asp:ListItem>
                            <asp:ListItem>Admin</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="forgotpass">
                        <asp:LinkButton ID="FP" runat="server" OnClick="Forgot_Password">forgot password</asp:LinkButton>
                        <div style="margin-bottom: 1vw;"></div>
                        <p id="ErrorMsg" visible="false" runat="server"><span style="color: Red">Incorrect</span> User Name or Password.</p>
                    </div>
                    <div class="loginbutton">
                        <button id="verifybutton" class="formbtn" type="button" onclick="Validate_Step()" runat="server">LOGIN</button>
                        <div style="display: none;"><asp:Button ID="BtnLogin" runat="server" OnClick="BtnLogin_Click" Text="Login" /></div>
                        <div style="margin-bottom: 3vw;"></div>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <!--------------- JavaScript -------------->
    <script>
        function Toggle_Password() {
            var temp = document.getElementById("Txtpassword");
            if (temp.type === "password") {
                temp.type = "text";
                document.getElementById("PasswordIco").src = "appImages/passopennew.png";
            } else {
                temp.type = "password";
                document.getElementById("PasswordIco").src = "appImages/passiconew.png";
            }
        }

        function TBFocus(temp) {
            if (temp == 1) {
                document.forms["form1"]["Txtusername"].placeholder = "username";
                document.forms["form1"]["Txtusername"].style.backgroundColor = "#fff";
            }
            if (temp == 2) {
                document.forms["form1"]["Txtpassword"].placeholder = "password";
                document.forms["form1"]["Txtpassword"].style.backgroundColor = "#fff";
            }
        }

        function Validate_Step() {
            var user = document.forms["form1"]["Txtusername"].value;
            var pass = document.forms["form1"]["Txtpassword"].value;

            if (user == "") {
                document.getElementById("Txtusername").style.backgroundColor = "#f5898e";
                document.getElementById("Txtusername").placeholder = "must fillout this field";
            }
            if (pass == "") {
                document.getElementById("Txtpassword").style.backgroundColor = "#f5898e";
                document.getElementById("Txtpassword").placeholder = "must fillout this field";
            }

            if (user != "" && pass != "") {
                $('#BtnLogin').trigger('click');
            }
        }
    </script>    
</body>
</html>
