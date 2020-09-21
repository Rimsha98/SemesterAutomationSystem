<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CPViewResult.aspx.cs" Inherits="UokSemesterSystem.CPViewResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>
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
            margin: 0.2vw;
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
        <!--<asp:Button runat="server" ID="btnBack" Text="Back" onclick="btnBack_Click" /> -->
        <Center>
        <div>
            <div class="headingtop">
            <h1>
                <span style="color: #fff;">Results - University of Karachi</span>
            </h1>
            </div>
            <div id="DepartmentView" runat="server" style="margin: 0 auto;  width: 80%; height: auto; margin-top: 2%;">
            <asp:GridView ID="GVCP" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" style="text-align: center; border: 0.1vw solid #a9a9a9" Width="100%" >

                <AlternatingRowStyle BackColor="White" />

                <Columns >
                    
                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" HeaderStyle-Width="40%" HeaderStyle-CssClass="header" ItemStyle-CssClass="text"/>
                    <asp:BoundField DataField="TName" HeaderText="Chairperson" HeaderStyle-Width="35%" HeaderStyle-CssClass="header" ItemStyle-CssClass="text"/>
                        
                    <asp:TemplateField >
                         <ItemTemplate >
                            <asp:Button ID="EditLink" Text="View Classes" runat="server"  CssClass="EditLink" CommandArgument='<%# Eval("DId") %>' OnClick="EditLink_Click"/>
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
        
        </div>
            <asp:Label ID="Label1" runat="server" Text="No Results" Visible="false" style="font-weight: 700; color: #FF0000"></asp:Label>
             <div id="ClassView" runat="server" style="margin: 0 auto;  width: 80%; height: auto; margin-top: 2%;">
            <!--<asp:Button ID="Back" runat="server" Text="Back" OnClick="Back_Click"></asp:Button> -->
            <asp:GridView ID="GvClass" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" style="text-align: center; border: 0.1vw solid #a9a9a9" Width="100%" >

                <AlternatingRowStyle BackColor="White" />

                <Columns >
                    
                    <asp:BoundField DataField="ClassName" HeaderText="Class Name" HeaderStyle-CssClass="header" ItemStyle-CssClass="text"/>
                    <asp:BoundField DataField="ClassSection" HeaderText="Class Section" HeaderStyle-CssClass="header" ItemStyle-CssClass="text"/>
                    <asp:BoundField DataField="Shift" HeaderText="Shift" HeaderStyle-CssClass="header" ItemStyle-CssClass="text"/>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="ClassLink" Text="View Result" runat="server" CssClass="EditLink" CommandArgument='<%# Eval("ClassID") %>' OnClick="ClassLink_Click"/>
                            
                            
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
                  
        </div>
        </div>
            </Center>
            <button id="HiddenBTN1" type="button" style="display: none;"></button>
            </div>
    </form>
    <script>
    $(function () {
        $("#Nav-TimeTableAdmin").load("AdminNavBar.aspx");
    });

        $(document).ready(function () {
            document.getElementById("HiddenBTN1").click();
        });

        $("#HiddenBTN1").on("click", function () {
            sessionStorage.setItem("CPViewResult", "loaded");
        });
        </script>
</body>
</html>

