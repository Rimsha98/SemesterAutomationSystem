<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="UokSemesterSystem.CreateAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <script src="jquery/jquery311.js"></script>
    <script src="jquery/jquery1102.js"></script>

    <title></title>

    <style>
        .GridViewImage { 
            border-radius: 50%;
            width: 4vw;
            height: 4vw;
            padding: 0.5vw;
            font-weight: 600;
        }
        .GridViewHeads { 
            padding: 0.5vw;
            text-align: center;
            font-size: 1.2vw;
            font-family: Calibri;
            font-weight: 600;
        }

        .GridViewHeads1{
            padding: 0.5vw;
            text-align: center;
            font-size: 1.2vw;
            font-family: Calibri;
            font-weight: 600;
        }

        .elementEmail {
            padding: 0.5vw;
            word-break: break-word;
            text-align: center;
            font-size: 1.2vw;
            font-family: Calibri;
        }

        .element {
            padding: 0.5vw;
            text-align: center;
            font-size: 1.2vw;
            font-family: Calibri;
        }

        .btn, .btncp{
            width: 90%;
            background-color:#1D8AB5;
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
        .btn:hover, .btncp:hover { background-color: #64C5EB; }

        #Save, #Canel { width: 20%; height: 2.5vw; margin-top: 3%; margin-right: 0; }
        #Canel { width: 15%; }

        #DDSelectUser, #TeachList {
    height: 2.5vw;
    font-size: 1.2vw;
    font-family: Calibri;
    color: #4B4B4B;
    border: 0.1vw solid #a9a9a9;
    outline: none;
    cursor: pointer;
    padding-left: 0.8vw;
    background-color: #fff;
    margin: 0 auto;
    width: 25%;
}

        #TeachList { margin: 0; height: 2vw; width: 45%; font-size: 1.2vw; }

select{
    appearance: none;
    -webkit-appearance: none;
    -moz-appearance: none;
    -ms-appearance: none;
    background: url('../appImages/arrow.png');
    background-repeat: no-repeat;
    background-size: 1vw 0.8vw;
    background-position: right 0.4vw center;
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
    }

 td {
    padding: 0.6vw;
    font-size: 1.2vw;
    text-align: left;
}

.boldtext {
    font-weight: 600;
}

#Img {
     height: 13vw;
    width: 13vw;
    background-color: #fcfcfc;
    object-fit: cover;
    border: 0.15vw solid #1D8AB5;
        }


td {
    padding: 0.6vw;
    font-size: 1.2vw;
    text-align: left;
}

.boldtext {
    font-weight: 600;
}

.head, .picturehead { 
    font-size: 1.5vw;
    color: #fff;
    margin: 0; padding: 0.4vw;
    font-family: Calibri;
}

.picturehead { font-size: 1.3vw; color: #4b4b4b; margin: 0; padding: 0;  }

#ConfirmTech, #backbtn {
            width: 15vw;
            background-color: #1D8AB5;
            color: #fff;
            text-align: center;
            outline: none;
            border: none;
            font-size: 1.3vw;
            font-family: Calibri;
            cursor: pointer;
            height: 2.5vw;
            font-weight: 600;
            margin-top: 2%;
        }
#backbtn { margin-right: 0.5vw; width: 8vw; background-color: #4b4b4b; }
        #ConfirmTech:hover { background-color: #64C5EB; }
        #backbtn:hover { background-color: #6b6b6b; }

        #Canel { background-color: #4b4b4b; }
        #Canel:hover { background-color: #6b6b6b; }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div id="Nav-Create" style="float:left; width: 15%;"></div>
        <div style="float: left; width: 85%; height: 100vh; overflow-y:scroll; overflow-x: hidden;">

            <button id="createaccountstab" type="button" style="display: none;"></button>
         <Center>
        <div id="maindiv" runat="server">
            <div class="headingtop">
            <h1>
                <span style="color: #fff;">Create Accounts</span>
            </h1>
            </div>
            <div>
                <asp:DropDownList ID="DDSelectUser" runat="server" OnSelectedIndexChanged="DDSelectUser_SelectedIndexChanged" AutoPostBack=True>
                            
                            <asp:ListItem>Student</asp:ListItem>
                            <asp:ListItem>ChairPerson</asp:ListItem>
                            <asp:ListItem>Teacher</asp:ListItem>
                         </asp:DropDownList>
                
                <asp:Label ID="ConfirmMsg" runat="server" style="margin-left:5px;" Text="abc" ForeColor="Red"></asp:Label>
            </div>
            <br />
             <div id="stdView" runat="server">
            <asp:GridView ID="GvStudent"  runat="server" OnPageIndexChanging="GvStudent_PageIndexChanging" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="none" style="text-align: center; border: 0.1vw solid #a9a9a9" Width="96%" PageSize="20" OnPreRender="GvStudent_PreRender">

                <AlternatingRowStyle BackColor="White" />

                <Columns >
                    <asp:ImageField HeaderText="Image"  DataImageUrlField="Image" HeaderStyle-CssClass="GridViewHeads" >
                    <ControlStyle CssClass="GridViewImage"></ControlStyle>
                    </asp:ImageField>
                    <asp:BoundField DataField="SName" HeaderText="Student Name" HeaderStyle-CssClass="GridViewHeads" HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-CssClass="element"/>
                    <asp:BoundField DataField="FatherName" HeaderText="Father Name" HeaderStyle-CssClass="GridViewHeads" HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-CssClass="element"/>
                    <asp:BoundField DataField="Enrollment" HeaderText="Enrollment" HeaderStyle-CssClass="GridViewHeads" HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-CssClass="element"/>
                    <asp:BoundField DataField="RollNumber" HeaderText="Roll Number" HeaderStyle-CssClass="GridViewHeads" HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-CssClass="element"/>
                    <asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-CssClass="GridViewHeads" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-CssClass="element"/>
                    <asp:BoundField DataField="Department" HeaderText="Department" HeaderStyle-CssClass="GridViewHeads" HeaderStyle-Width="15%" ItemStyle-Width="15%" ItemStyle-CssClass="element"/>
                    <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-CssClass="GridViewHeads" HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-CssClass="elementEmail" />
                    <asp:BoundField DataField="Shift" HeaderText="Shift" HeaderStyle-CssClass="GridViewHeads" HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-CssClass="element"/>
                    <asp:TemplateField HeaderStyle-Width="15%" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Button ID="StdLink" Text="Create Account" style="margin: 0.5vw;" CssClass="btn" runat="server" CommandArgument='<%# Eval("SId") %>' OnClick="StdLink_Click"/>
                        </ItemTemplate>

                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1D8AB5" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#1D8AB5" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
             
            <div id="TeacherView" runat="server">
            <asp:GridView ID="GVTeacher" OnPreRender="GVTeacher_PreRender" OnPageIndexChanging="GVTeacher_PageIndexChanging" AllowPaging="true" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" style="text-align: center; border: 0.1vw solid #a9a9a9" Width="96%" PageSize="20">

                <AlternatingRowStyle BackColor="White" />

                <Columns >
                    <asp:ImageField HeaderText="Image"  DataImageUrlField="Image" HeaderStyle-CssClass="GridViewHeads1" >
                    <ControlStyle CssClass="GridViewImage"></ControlStyle>
                    </asp:ImageField>
                    <asp:BoundField DataField="TName" HeaderText="Teacher Name" HeaderStyle-CssClass="GridViewHeads1" HeaderStyle-Width="20%" ItemStyle-Width="20%" ItemStyle-CssClass="element"/>
                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" HeaderStyle-CssClass="GridViewHeads1" HeaderStyle-Width="20%" ItemStyle-Width="20%" ItemStyle-CssClass="element"/>
                    <asp:BoundField DataField="Conatact" HeaderText="Contact" HeaderStyle-CssClass="GridViewHeads1" HeaderStyle-Width="15%" ItemStyle-Width="15%" ItemStyle-CssClass="element"/>
                    <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-CssClass="GridViewHeads1" HeaderStyle-Width="20%" ItemStyle-Width="20%" ItemStyle-CssClass="elementEmail"/>
                    <asp:BoundField DataField="Degree" HeaderText="Degree" HeaderStyle-CssClass="GridViewHeads1" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-CssClass="element"/>
                    <asp:TemplateField HeaderStyle-Width="15%" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Button ID="TechLink" Text="Create Account" runat="server" CssClass="btn" CommandArgument='<%# Eval("TId") %>' OnClick="TechLink_Click"/>
                            
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1D8AB5" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#1D8AB5" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>

             <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
             
          
            <div id="CPView" runat="server">
            <asp:GridView ID="GVCP" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" style="text-align: center; border: 0.1vw solid #a9a9a9" Width="70%" >

                <AlternatingRowStyle BackColor="White" />

                <Columns >
                    
                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" HeaderStyle-CssClass="GridViewHeads1" HeaderStyle-Width="35%" ItemStyle-Width="35%" ItemStyle-CssClass="element"/>
                    <asp:BoundField DataField="TName" HeaderText="Chair Person" HeaderStyle-CssClass="GridViewHeads1" HeaderStyle-Width="35%" ItemStyle-Width="35%" ItemStyle-CssClass="element"/>
                         
                    <asp:TemplateField HeaderStyle-Width="30%" ItemStyle-Width="30%" >
                         <ItemTemplate >
                            <asp:Button ID="EditLink" Text="Update ChairPerson" runat="server" CssClass="btncp" CommandArgument='<%# Eval("DId") %>' OnClick="EditDepartment_Click"/>
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
             <div>
                    <asp:Panel ID="Panel1" Visible="false" runat="server">
                 <div  runat="server">
            <Center>
            <div id="Div2" runat="server" style="width: 90%; margin: 0 auto;  margin-top: 3%;">
                <div style="width: 100%; background-color: #1D8AB5; margin: 0 auto; padding: 0; text-align: center; margin-bottom: 2%;">
                    <h1 class="head">Update Chairperson</h1>
                </div>
                
                <div style="float: left; width: 20%; ">
                    <asp:Image ID="Img" runat="server" style="text-align: center" ></asp:Image>
                    <h1 class="picturehead">User's Picture</h1>
                </div>
                <div style="float: left; width: 80%; ">
                    <table cellspacing="0" style="width: 100%; margin: 0 auto;  ">
                     <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Department: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="Dname" runat="server"></label></td>
                    </tr>

                     <tr>
                        <td style="width: 30%;"><span class="boldtext">Select Teacher: </span></td>
                        <td style="width: 70%;">
                            <asp:DropDownList ID="TeachList" runat="server"  OnSelectedIndexChanged="GetID" AutoPostBack="true"></asp:DropDownList>
                        <label id="Label1" runat="server" class="adjustment"  style="color:red;"> </label> 
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Contact: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="contact" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%;"><span class="boldtext">Email: </span></td>
                        <td style="width: 70%;"><label id="email" runat="server"></label></td>
                    </tr>

                    <tr>
                        <td style="width: 30%; background-color: #f0f0f0"><span class="boldtext">Username: </span></td>
                        <td style="width: 70%; background-color: #f0f0f0"><label id="uname" runat="server"></label></td>
                    </tr>

                    </table>

                    <div style="width: 100%; text-align: right;">
               
                <asp:Button ID="Canel" runat="server" CssClass="btn"  Text="Cancel" OnClick="Canel_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
                <asp:Button ID="Save" runat="server" CssClass="btn"  Text="Save Changes" OnClick="Save_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
                </div>
                    </div>




                <br />

                

                
            </div>
                    
                   
                </Center>
        </div>
           </asp:Panel>
             </div>
            </Center>

            <br /><br />
            </div>
    </form>

    <script>
    $(function () {
        $("#Nav-Create").load("AdminNavBar.aspx");
    });

        $(document).ready(function () {
            document.getElementById("createaccountstab").click();
        });

        $("#createaccountstab").on("click", function () {
            sessionStorage.setItem("CreateAccount", "loaded");
        });





        </script>
</body>
</html>
