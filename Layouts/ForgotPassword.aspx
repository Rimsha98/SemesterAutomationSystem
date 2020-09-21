<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="UokSemesterSystem.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link media="screen" rel="stylesheet" runat="server" href="~/css/ForgotPassword.css" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <script src="jquery/jquery340.js"></script>
</head>

<body  onload="popup()">
    <form id="form1" runat="server">
        <div class="screen">

        <!--------------- Screen Base Left -------------->
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

        <!--------------- Screen Base Right -------------->
        <div class="Right-Panel">
            <div class="Border-Line"></div>
            <div class="Border-Line2"></div>
        </div>

        <!-------------- Pop Up Modal --------------->
        <div id="myModal" class="modal">
            <div class="modal-content">
            <span class="close">&times;</span>
            <div>
               
               <!--------- Page 1 --------->
               <div id="div1" runat="server" visible="true"> 
                    <h1>FORGOT PASSWORD</h1>
                    <p>Forgot your password? You can retrieve it in just a few simple steps.</p>
                    <p>Kindly enter your <span style="color: Red;">email ID </span>below: </p>
                    <asp:TextBox ID="email" runat="server" placeholder="enter your email ID" onfocus="TBFocus(1)" ></asp:TextBox><br />
                    <asp:DropDownList ID="DDSelectUser" runat="server">
                            <asp:ListItem>Student</asp:ListItem>
                            <asp:ListItem>Teacher</asp:ListItem>
                            <asp:ListItem>ChairPerson</asp:ListItem>
                            <asp:ListItem>Admin</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:Button ID="Next1" runat="server" Text="NEXT" onclick="fpNext1_Click" /> 
                    <p id="invalidemail" runat="server" style="margin-top: 0.5vw;" visible=false><span style="color: Red">Invalid Email!</span> Try again.</p>
                    <p id="NotRegistered" runat="server" style="margin-top: 0.5vw;" visible=false><span style="color: Red">You are not Registered! Please Contact University Administration</span> </p>
                    <p id="notconnected" runat="server" style="margin-top: 0.5vw; " visible=false>You are not connected to the internet.</p>
               </div>

                <!--------- Page 2 --------->
                <div id="div2" runat="server" visible="false" >
                    <h1>CODE VERIFICATION</h1>
                    <p>A verification code was sent to the email that you entered.</p>
                    <p>Kindly enter that <span style="color: Red;">4-digit code </span>below: </p>
                    <asp:TextBox ID="code" runat="server"  placeholder="enter 4-digit code here" onfocus="TBFocus(2)"></asp:TextBox>
                    <br />
                    <asp:Button ID="resend" runat="server" onclick="Resend_Email" Text="RE-SEND CODE"/>
                    <asp:Button ID="Next2" runat="server" Text="NEXT" onclick="fpNext2_Click"/>
                    <p id="displaymsg" visible="false" runat="server" style=" margin-top: 0.5vw">
                    <span style="color: Red;">The Code you entered is wrong! </span>Try again.</p>
                    <p id="P2" visible="false" style=" margin-top: 0.5vw" runat="server">
                    <span style="color: Red;">Code Resent!</span> Try entering the new code.</p>
               </div>
                
                <!--------- Page 3 --------->
                <div id="div3" runat="server" visible="false">
                    <h1>UPDATE PASSWORD</h1>
                    <p>Kindly fillout the following fields.</p>
                    <br /><label>Enter New Password:</label>&nbsp;&nbsp;
                    <asp:TextBox ID="pass1"   placeholder="enter password"  type="password" runat="server" onfocus="TBFocus(3)"></asp:TextBox>
                    <p style="margin-left: 2.3%;"><asp:Label ID="p1" runat="server" Text=""></asp:Label></p>
                    <label style="margin-left: 1vw;">Re-Enter Password:</label>&nbsp;&nbsp;
                    <asp:TextBox ID="pass2"  placeholder="re-enter password" type="password" runat="server" onfocus="TBFocus(4)"></asp:TextBox>
                    <p style="margin-left: 2.3%;"><asp:Label ID="pp2" runat="server" Text=""></asp:Label></p>
                    <asp:Button ID="confirmpass" runat="server" Text="CONFIRM" onclick="confirm_Click" />
                    <p id="P3" visible="false" style="color: red; margin-top: 0.5vw; margin-left: -1.8vw;" runat="server">Passwords do not match!</p>
                </div>

                <!--------- Page 4 --------->
                <div id="div4" runat="server" visible="false">
                    <p>Your password has been changed successfully.</p>                     
                    <p><span style="color: Red;">Re-directing</span> you now to Login page...</p>
                </div>
                </div>
            </div>
        </div>
        </div>
    </form>
    
    <script>
    var modal = document.getElementById("myModal");
    var btn = document.getElementById("myBtn");
    var span = document.getElementsByClassName("close")[0];


        function TBFocus(temp) {
            if (temp == 1) {
                document.forms["form1"]["email"].placeholder = "enter your email ID";
                document.forms["form1"]["email"].style.backgroundColor = "#fff";
            }
            if (temp == 2) {
                document.forms["form1"]["code"].placeholder = "enter 4-digit code here";
                document.forms["form1"]["code"].style.backgroundColor = "#fff";
            }
            if (temp == 3) {
                document.forms["form1"]["pass1"].placeholder = "enter password";
                document.forms["form1"]["pass1"].style.backgroundColor = "#fff";
            }
            if (temp == 4) {
                document.forms["form1"]["pass2"].placeholder = "re-enter password";
                document.forms["form1"]["pass2"].style.backgroundColor = "#fff";
            }
            
        }

    function popup() {
        modal.style.display = "block";
    }

    span.onclick = function() {
        window.location.replace("Login.aspx");
    }

    window.onclick = function(event) {
      if (event.target == modal) {
          modal.style.display = "block";
      }
    }
    </script>
</body>
</html>

