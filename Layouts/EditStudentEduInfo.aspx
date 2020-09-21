<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditStudentEduInfo.aspx.cs" Inherits="UokSemesterSystem.EditStudentEduInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title></title>
    <style>
        .head {
            font-weight: 600;
            color: #fff;
            font-family: Calibri;
            font-size: 1.2vw;
            padding: 1vw;

        }
        .backcell { padding: 1vw; }
        #tbl_std { 
            margin: 0 auto; text-align: center;
            border: 0.1vw solid #cccccc;
            font-size: 1.2vw;
            font-family: Calibri;
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

        h2{
            font-family: Calibri;
            font-weight: 600;
            font-size: 2vw;
        }

        .OptionsDIV {
    width: 70%;
    border-bottom: 0.1vw solid #a9a9a9;
    margin: 0 auto;
    margin-top: 7%;
    text-align: center;
}

.OptionsButton:hover { background-color: #1D8AB5 !important; }
.OptionsButton {
    border: none;
    background-color: #64C5EB;
    color: #fff;
    font-size: 1.2vw;
    font-weight: 700;
    text-align: center;
    width: 15vw;
    height: 2.5vw;
    cursor: pointer;
    outline: none;
}
#StudentRecord {
    background-color: #1D8AB5;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Nav-Edit" style="float:left; width: 15%;"></div>
        <div   style="float: left; width: 85%; height: 100vh; overflow-x:hidden; overflow-y:scroll">
        <div class="OptionsDIV" id="Options" runat="server">
                <asp:Button ID="StudentRecord" runat="server" CssClass="OptionsButton" Text="Student Records" />
                <asp:Button ID="TeacherRecord" runat="server" CssClass="OptionsButton" Text="Teacher Records" OnClick="DisplayTeachersList"/>
        </div>
            <br /><br />
        <asp:Table ID="tbl_std" runat="server" GridLines="none" Width="90%" CellSpacing="0">
            <asp:TableHeaderRow BackColor="#1D8AB5">
                <asp:TableHeaderCell CssClass="head">S No.</asp:TableHeaderCell>
                <asp:TableHeaderCell CssClass="head">Enrollment No.</asp:TableHeaderCell>
                <asp:TableHeaderCell CssClass="head">Student Name</asp:TableHeaderCell>
                <asp:TableHeaderCell CssClass="head">Father's Name</asp:TableHeaderCell>
                <asp:TableHeaderCell CssClass="head"></asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
             <button id="HiddenBTN1" type="button" style="display: none;"></button>
            <br /><br />
            </div>
    </form>

    <script>
        $(function () {
            $("#Nav-Edit").load("AdminNavBar.aspx");
        });

        $(document).ready(function () {
            document.getElementById("HiddenBTN1").click();
        });

        $("#HiddenBTN1").on("click", function () {
            sessionStorage.setItem('EditStudent', 'loaded');
        });
    </script>
</body>
</html>
