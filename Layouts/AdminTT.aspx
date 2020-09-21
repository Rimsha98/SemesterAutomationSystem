﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminTT.aspx.cs" Inherits="UokSemesterSystem.AdminTT" EnableEventValidation="false"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
    <title></title>
    <style>
        #listparah {
            text-align: center;
            margin: 0 auto;
            margin-top: 1%;
            font-family: Calibri;
            font-size: 1.4vw;
            color: #4b4b4b;
        }

        .EditLink {
            width: 70%;
            background-color: #1D8AB5;
            color: #fff;
            text-align: center;
            outline: none;
            border: none;
            font-size: 1.2vw;
            font-family: Calibri;
            cursor: pointer;
            height: 2.3vw;
            font-weight: 600;
            margin: 0.5vw;
        }
        .EditLink:hover { background-color: #64C5EB; }
        .header {
            padding: 0.5vw;
            font-family: Calibri;
            font-weight: 600;
            font-size: 1.2vw;
        }
        .text {
            font-size: 1.2vw;
            padding: 0.5vw;
            font-family: Calibri;
        }
    
.headingtop {
    margin: 0 auto;
    width: 70%;
    text-align: center;
    margin-top: 5%;
    margin-bottom: 3%;
    border-bottom: 0.1vw solid #a9a9a9;
    padding: 0;
}

    .headingtop h1 {
        font-size: 1.5vw;
        text-align: center;
        margin: 0 auto;
        padding: 0.2vw;
        font-family: Calibri;
        font-weight: 600;
        background-color: #1D8AB5;
        width: 60%;
        height: 100%;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div id="Nav-TimeTableAdmin" style="float:left; width: 15%;"></div>
        <div style="float: left; width: 85%; height: 100vh; overflow-y:scroll; overflow-x: hidden;">
            <div class="headingtop">
            <h1>
                <span style="color: #fff;">Departments of University of Karachi</span>
            </h1>
            </div>
            <p id="listparah">Following is a list of all departments of University of Karachi:</p>
         <div id="CPView" runat="server" style="margin: 0 auto;  width: 80%; height: auto; margin-top: 2%;">
            <asp:GridView ID="GVCP" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="none" style="text-align: center; border: 0.1vw solid #a9a9a9" Width="100%" >

                <AlternatingRowStyle BackColor="White" />

                <Columns >
                    
                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" HeaderStyle-Width="40%" HeaderStyle-CssClass="header" ItemStyle-CssClass="text"/>
                    <asp:BoundField DataField="TName" HeaderText="Chairperson" HeaderStyle-Width="35%" HeaderStyle-CssClass="header" ItemStyle-CssClass="text"/>
                         
                    <asp:TemplateField HeaderStyle-Width="25%">
                         <ItemTemplate>
                            <asp:Button ID="EditLink" Text="View TimeTable" runat="server" CssClass="EditLink" CommandArgument='<%# Eval("DId") %>' OnClick="EditDepartment_Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1D8AB5" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView> 
                <br /><br /><br />
             <button id="admintt" type="button" style="display: none"></button>
        
        </div>
            </div>
    </form>

     <script>
    $(function () {
        $("#Nav-TimeTableAdmin").load("AdminNavBar.aspx");
    });

         $(document).ready(function () {
             document.getElementById("admintt").click();
         });

         $("#admintt").on("click", function () {
             sessionStorage.setItem("AdminTT", "loaded");
         });
         </script>
</body>
</html>
