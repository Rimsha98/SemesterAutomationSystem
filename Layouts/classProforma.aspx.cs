using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace UokSemesterSystem
{
    public partial class classProforma : System.Web.UI.Page
    {
        static string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(conString);

        string ClassID;
        string[] courseIDs;
        string[] studentInfo;
        string[] RomanCount;
        int grandTotal = 0;
        string[] PreSem;
        double TotalGPa = 0;
        string currentSemester = "";


        private List<string[]> courselist = new List<string[]>();
        private List<string[]> ResultList = new List<string[]>();
        private List<string> sList = new List<string>();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ClassID = Session["ClassID"].ToString();
            con.Open();
            string q = "select SId from Student  where ClassID='" + ClassID + "' ";
            SqlCommand com = new SqlCommand(q, con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                    sList.Add(dr[0].ToString());

            }
            con.Close();

            generateClassProforma();

        }

        private void generateClassProforma()
        {
            for (int i = 0; i < sList.Count; i++)
            {
                HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
                // createDiv.ID=i.ToString();


                Table Table1 = new Table();
                Table1.CellSpacing = 0;
                Table1.Width = new Unit("100%");
                Table1.Font.Size = 12;
                Table1.Style.Add("font-family", "Times New Roman");
                TableRow row1 = new TableRow();
                TableCell cell1 = new TableCell();
                cell1.ColumnSpan = 4;
                cell1.Style.Add("border", "1px solid black");
                cell1.Style.Add("padding", "5px");
                row1.Cells.Add(cell1);
                TableCell cell2 = new TableCell();
                cell2.ColumnSpan = 2;
                cell2.Text = "Marks Obtained";
                cell2.Font.Bold = true;
                cell2.Style.Add("border-top", "1px solid black");
                cell2.Style.Add("border-bottom", "1px solid black");
                cell2.Style.Add("padding", "5px");
                row1.Cells.Add(cell2);
                TableCell cell3 = new TableCell();
                cell3.Text = "Course";
                cell3.Font.Bold = true;
                cell3.ColumnSpan = 3;
                cell3.Style.Add("border-left", "1px solid black");
                cell3.Style.Add("border-top", "1px solid black");
                cell3.Style.Add("border-bottom", "1px solid black");
                cell3.Style.Add("padding", "5px");
                row1.Cells.Add(cell3);

                TableCell cell4 = new TableCell();
                cell4.Text = "Courses Cleared in Semester";
                cell4.ColumnSpan = 2;
                cell4.Font.Size = 10;
                cell4.Font.Bold = true;
                cell4.Style.Add("border", "1px solid black");
                cell4.Style.Add("padding", "5px");
                row1.Cells.Add(cell4);
                Table1.Rows.Add(row1);
                TableRow SheetHeader = new TableRow();
                // SID = Session["SID"].ToString();
                RomanCounteing();
                TableHeader(SheetHeader);
                Table1.Rows.Add(SheetHeader);
                GetStudent(sList[i]);
                GetCourses();
                GetResult(sList[i]);
                AddData(Table1);
                AddGeneralInfoData();

                GetPreviuosResult(sList[i]);
                AddRomanCount(Table1);


                /*  TableRow rowLast = new TableRow();
                  rowLast.BorderStyle = BorderStyle.Solid;
                  TableCell lcell1 = new TableCell();
                  lcell1.ColumnSpan = 6;
                  lcell1.HorizontalAlign = HorizontalAlign.Right;
                  lcell1.Text = "Grand Total";
                  rowLast.Cells.Add(lcell1);
                  TableCell lcell2 = new TableCell();
                  lcell2.Text = "100";
                  rowLast.Cells.Add(lcell2);

                  TableCell lcell3 = new TableCell();
                  rowLast.Cells.Add(lcell3);


                  TableCell lcell4 = new TableCell();
                  //lcell4.ColumnSpan = 2;
                  lcell4.Text = "66.6";
                  rowLast.Cells.Add(lcell4);

                  Table1.Rows.Add(rowLast);*/
                createDiv.Controls.Add(Table1);
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));

                Table LinesTable = new Table();
                LinesTable.Width = new Unit("100%");
                LinesTable.CellSpacing = 10;
                LinesTable.GridLines = GridLines.None;
                LinesTable.Style.Add("text-align", "left");
                LinesTable.Style.Add("font-family", "Times New Roman");
                LinesTable.Font.Size = 13;

                TableRow linerow1 = new TableRow();
                TableCell lc1 = new TableCell();

                lc1.Width = new Unit("10%");
                lc1.Style.Add("padding", "5px");
                Label r = new Label();
                r.Text = "Result: ";
                r.Font.Bold = true;
                lc1.Controls.Add(r);

                TableCell lc2 = new TableCell();
                lc2.Width = new Unit("90%");
                lc2.Style.Add("border-bottom", "1px solid black");
                Label result = new Label();
                result.Text = "PASSES";
                result.Font.Bold = true;
                lc2.Controls.Add(result);
                linerow1.Cells.Add(lc1);
                linerow1.Cells.Add(lc2);
                LinesTable.Rows.Add(linerow1);


                TableRow linerow2 = new TableRow();
                TableCell lc3 = new TableCell();

                lc3.Width = new Unit("10%");
                lc3.Style.Add("padding", "5px");
                Label rm = new Label();
                rm.Text = "Remarks: ";
                rm.Font.Bold = true;
                lc3.Controls.Add(rm);

                TableCell lc4 = new TableCell();
                lc4.Width = new Unit("90%");
                lc4.Style.Add("border-bottom", "1px solid black");
                Label remarks = new Label();
                lc4.Controls.Add(remarks);
                linerow2.Cells.Add(lc3);
                linerow2.Cells.Add(lc4);
                LinesTable.Rows.Add(linerow2);

                TableRow linerow3 = new TableRow();
                TableCell lc5 = new TableCell();
                lc5.ColumnSpan = 2;
                lc5.Style.Add("border-bottom", "1px solid black");
                lc5.Text = ".";
                lc5.ForeColor = System.Drawing.Color.FromArgb(255, 255, 204);
                linerow3.Cells.Add(lc5);
                LinesTable.Rows.Add(linerow3);

                createDiv.Controls.Add(LinesTable);



                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));



                Table EndingTable = new Table();
                EndingTable.Width = new Unit("100%");
                EndingTable.CellSpacing = 10;
                EndingTable.GridLines = GridLines.None;
                EndingTable.Style.Add("text-align", "left");
                EndingTable.Style.Add("font-family", "Times New Roman");
                EndingTable.Font.Size = 11;
                TableRow etrow1 = new TableRow();
                TableCell etc1 = new TableCell();
                etc1.Width = new Unit("30%");
                etc1.Text = "<b>Dated: </b>" + DateTime.Now.ToString("dd/MM/yyyy");
                etrow1.Controls.Add(etc1);

                TableCell etc2 = new TableCell();
                etc2.Width = new Unit("35%");
                string temp;
                if (chkAdminApp())
                {
                    temp = "_______<b>Admin</b>_______";
                }
                else
                    temp = "___________________";

                etc2.Text = "<b>Checked By: </b>" + temp;
                etrow1.Cells.Add(etc2);

                TableCell etc3 = new TableCell();
                etc3.Width = new Unit("35%");
                etc3.Text = "<b>Assistant Controller: </b> ___________________";
                etrow1.Cells.Add(etc3);
                EndingTable.Rows.Add(etrow1);


                TableRow etrow2 = new TableRow();
                TableCell etc4 = new TableCell();
                etc4.ColumnSpan = 2;
                etc4.Text = "<b>Generated by:</b> Shamim A.Raul";
                etrow2.Cells.Add(etc4);
                EndingTable.Rows.Add(etrow2);

                createDiv.Controls.Add(EndingTable);

                Table newtable = new Table();
                newtable.Width = new Unit("100%");
                newtable.CellSpacing = 10;
                newtable.GridLines = GridLines.None;
                newtable.Style.Add("text-align", "left");
                newtable.Style.Add("font-family", "Times New Roman");
                newtable.Font.Size = 12;

                TableRow etrow3 = new TableRow();
                TableCell etc5 = new TableCell();
                etc5.Width = new Unit("5%");
                etc5.Text = "<b>Note: </b>";
                etrow3.Cells.Add(etc5);

                TableCell etc6 = new TableCell();
                etc6.Width = new Unit("95%");
                etc6.Text = "1. University reserves the right to correct any error that may be detected in the Marks Sheet / Proforma";
                etrow3.Cells.Add(etc6);
                newtable.Rows.Add(etrow3);

                TableRow etrow4 = new TableRow();
                TableCell etc8 = new TableCell();
                etc8.Width = new Unit("5%");
                etc8.Text = "";
                etrow4.Cells.Add(etc8);


                TableCell etc7 = new TableCell();
                etc7.Width = new Unit("95%");
                etc7.Text = "2. This Provisional mark proforma cannot be presented in any court of law by concerned candidate unless he/she is issued commulative marks sheet from the semester examinitions section as per semester rules";
                etrow4.Cells.Add(etc7);
                newtable.Rows.Add(etrow4);


                createDiv.Controls.Add(newtable);

                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));
                createDiv.Controls.Add(new LiteralControl("<br/>"));


                div1.Controls.Add(createDiv);
                div1.Controls.Add(new LiteralControl("<br/>"));
                courselist.Clear();
                ResultList.Clear();
                grandTotal = 0;
                PreSem = null;
                TotalGPa = 0;
                currentSemester = "";


            }
        }
        protected void RomanCounteing()
        {
            RomanCount = new string[10];
            RomanCount[0] = "I";
            RomanCount[1] = "II";
            RomanCount[2] = "III";
            RomanCount[3] = "IV";
            RomanCount[4] = "V";
            RomanCount[5] = "VI";
            RomanCount[6] = "VII";
            RomanCount[7] = "VIII";
            RomanCount[8] = "IX";
            RomanCount[9] = "X";

        }

        public void TableHeader(TableRow SheetHeader)
        {


            TableCell cell1 = new TableCell();
            cell1.Text = "Course No.";
            cell1.Font.Bold = true;
            cell1.Style.Add("border-bottom", "1px solid black");
            cell1.Style.Add("border-left", "1px solid black");
            cell1.Style.Add("padding", "5px");
            cell1.Width = new Unit("10%");
            SheetHeader.Cells.Add(cell1);
            TableCell cell = new TableCell();
            cell.Text = "Course Title";
            cell.Font.Bold = true;
            cell.Style.Add("border-bottom", "1px solid black");
            cell.Style.Add("border-left", "1px solid black");
            cell.Style.Add("padding", "5px");
            cell.Width = new Unit("30%");
            SheetHeader.Cells.Add(cell);
            TableCell cell2 = new TableCell();
            cell2.Text = "Cr.Hrs";
            cell2.Font.Bold = true;
            cell2.Style.Add("border-bottom", "1px solid black");
            cell2.Style.Add("border-left", "1px solid black");
            cell2.Style.Add("padding", "5px");
            cell2.Width = new Unit("5%");
            SheetHeader.Cells.Add(cell2);
            TableCell cell3 = new TableCell();
            cell3.Text = "Max. Marks";
            cell3.Font.Bold = true;
            cell3.Style.Add("border-bottom", "1px solid black");
            cell3.Style.Add("border-left", "1px solid black");
            cell3.Style.Add("border-right", "1px solid black");
            cell3.Style.Add("padding", "5px");
            cell3.Width = new Unit("5%");
            SheetHeader.Cells.Add(cell3);
            TableCell cell4 = new TableCell();
            cell4.Text = "Theory";
            cell4.Font.Bold = true;
            cell4.Style.Add("border-bottom", "1px solid black");
            cell4.Style.Add("padding", "5px");
            cell4.Width = new Unit("5%");
            SheetHeader.Cells.Add(cell4);
            TableCell cell5 = new TableCell();
            cell5.Text = "Lab.";
            cell5.Width = new Unit("5%");
            cell5.Font.Bold = true;
            cell5.Style.Add("border-bottom", "1px solid black");
            cell5.Style.Add("border-left", "1px solid black");
            cell5.Style.Add("padding", "5px");
            SheetHeader.Cells.Add(cell5);
            TableCell cell6 = new TableCell();
            cell6.Text = "Total";
            cell6.Font.Bold = true;
            cell6.Style.Add("border-bottom", "1px solid black");
            cell6.Style.Add("border-left", "1px solid black");
            cell6.Width = new Unit("5%");
            cell6.Style.Add("padding", "5px");
            SheetHeader.Cells.Add(cell6);
            TableCell cell7 = new TableCell();
            cell7.Text = "Grade";
            cell7.Font.Bold = true;
            cell7.Width = new Unit("5%");
            cell7.Style.Add("border-bottom", "1px solid black");
            cell7.Style.Add("border-left", "1px solid black");
            SheetHeader.Cells.Add(cell7);
            TableCell cell8 = new TableCell();
            cell8.Text = "G.P.";
            cell8.Font.Bold = true;
            cell8.Style.Add("border-bottom", "1px solid black");
            cell8.Style.Add("border-left", "1px solid black");
            cell8.Width = new Unit("5%");
            cell8.Style.Add("padding", "5px");
            SheetHeader.Cells.Add(cell8);
            TableCell cell9 = new TableCell();
            cell9.Text = "";
            cell9.Font.Bold = true;
            cell9.Style.Add("padding", "5px");
            cell9.Style.Add("border-bottom", "1px solid black");
            cell9.Style.Add("border-left", "1px solid black");

            SheetHeader.Cells.Add(cell9);
            TableCell cell10 = new TableCell();
            cell10.Text = "";
            cell10.Style.Add("padding", "5px");
            cell10.Style.Add("border-bottom", "1px solid black");
            cell10.Style.Add("border-right", "1px solid black");
            SheetHeader.Cells.Add(cell10);




        }
        protected void GetStudent(string Sid)
        {
            string q = "Select * from Student where SId='" + Sid + "'";
            con.Open();
            SqlCommand com = new SqlCommand(q, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();

            if (dr.HasRows)
            {
                studentInfo = new string[dr.FieldCount];
                for (int i = 0; i < studentInfo.Length; i++)
                {
                    studentInfo[i] = dr[i].ToString();

                }
                ClassID = dr["ClassID"].ToString();
                currentSemester = dr["SemesterNo"].ToString();
            }
            con.Close();
        }

        protected void GetCourses()
        {

            int semm = Convert.ToInt32(currentSemester) - 1;
            string q = "Select distinct(CourseId) from Timetable where ClassID='" + ClassID + "' and SemesterNo='" + semm + "'";
            con.Open();
            SqlDataAdapter sqlDa1 = new SqlDataAdapter(q, con);

            DataTable dr1 = new DataTable();
            sqlDa1.Fill(dr1);
            courseIDs = new string[dr1.Rows.Count];
            for (int i = 0; i < dr1.Rows.Count; i++)
            {
                courseIDs[i] = dr1.Rows[i][0].ToString();
            }
            con.Close();


            for (int j = 0; j < courseIDs.Length; j++)
            {
                string[] CourseInfo;
                string q2 = "select * from Course where CourseId='" + courseIDs[j] + "'";
                con.Open();
                SqlCommand com = new SqlCommand(q2, con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    CourseInfo = new string[dr.FieldCount];
                    for (int i = 0; i < CourseInfo.Length; i++)
                    {
                        CourseInfo[i] = dr[i].ToString();

                    }
                    courselist.Add(CourseInfo);
                }
                con.Close();
            }


        }

        protected void GetResult(string Sid)
        {
            for (int i = 0; i < courseIDs.Length; i++)
            {
                string[] ResultInfo;
                string q = "select * from Result where SId='" + Sid + "'and ClassId='" + ClassID + "' and CourseId='" + courseIDs[i] + "'";

                con.Open();
                SqlCommand com = new SqlCommand(q, con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();

                if (dr.HasRows)
                {
                    ResultInfo = new string[dr.FieldCount];
                    for (int j = 0; j < ResultInfo.Length; j++)
                    {
                        ResultInfo[j] = dr[j].ToString();

                    }
                    ResultList.Add(ResultInfo);
                }
                else
                {
                    ResultInfo = new string[14];
                    for (int j = 0; j < 14; j++)
                    {
                        ResultInfo[j] = "-";
                    }

                    ResultList.Add(ResultInfo);
                }

                con.Close();

            }




        }

        //protected void AddData(Table Table1)
        //{
        //    TableCell cell = new TableCell();
        //    TableCell cell2 = new TableCell();
        //    TableCell cell3 = new TableCell();
        //    TableCell cell4 = new TableCell();
        //    TableCell cell5 = new TableCell();
        //    TableCell cell6 = new TableCell();
        //    TableCell cell7 = new TableCell();
        //    TableCell cell8 = new TableCell();
        //    TableCell cell9 = new TableCell();

        //    for (int i = 0; i < courselist.Count; i++)
        //    {

        //        TableRow row = new TableRow();


        //        cell.Text = cell.Text + courselist[i][2].ToString() + "<br/>"; //Courseno
        //        cell.Font.Size.Equals("Large");
        //        row.Cells.Add(cell);


        //        cell2.Text = cell2.Text + courselist[i][1].ToString() + "<br/>";// course title
        //        cell2.Font.Size.Equals("Large");
        //        row.Cells.Add(cell2);


        //        cell3.Text = cell3.Text + courselist[i][3].ToString() + "<br/>";//credit hours
        //        cell3.Font.Size.Equals("Large");
        //        row.Cells.Add(cell3);


        //        cell4.Text = cell4.Text + "100" + "<br/>";
        //        cell4.Font.Size.Equals("Large");
        //        row.Cells.Add(cell4);


        //        cell5.Text = cell5.Text + ResultList[i][3].ToString() + "<br/>"; //Theory
        //        cell5.Font.Size.Equals("Large");
        //        row.Cells.Add(cell5);


        //        cell6.Text = cell6.Text + ResultList[i][4].ToString() + "<br/>";// Lab
        //        cell6.Font.Size.Equals("Large");
        //        row.Cells.Add(cell6);


        //        cell7.Text = cell7.Text + ResultList[i][13].ToString() + "<br/>";//Total
        //        cell7.Font.Size.Equals("Large");
        //        row.Cells.Add(cell7);


        //        cell8.Text = cell8.Text + "A" + "<br/>";//Grade
        //        cell8.Font.Size.Equals("Large");
        //        row.Cells.Add(cell8);


        //        cell9.Text = cell9.Text + ResultList[i][5].ToString() + "<br/>";//GPA
        //        cell9.Font.Size.Equals("Large");
        //        row.Cells.Add(cell9);
        //        Table1.Rows.Add(row);
        //    }

        //    for (int j = courselist.Count; j < 10; j++)
        //    {
        //        cell.Text = cell.Text + "<br/>";
        //        cell2.Text = cell2.Text + "<br/>";
        //        cell3.Text = cell3.Text + "<br/>";
        //        cell4.Text = cell4.Text + "<br/>";
        //        cell5.Text = cell5.Text + "<br/>";
        //        cell6.Text = cell6.Text + "<br/>";
        //        cell7.Text = cell7.Text + "<br/>";
        //        cell8.Text = cell8.Text + "<br/>";
        //        cell9.Text = cell9.Text + "<br/>";
        //    }

        //}
        protected void AddData(Table Table1)
        {
            //TableCell cell = new TableCell();
            //TableCell cell2 = new TableCell();
            //TableCell cell3 = new TableCell();
            //TableCell cell4 = new TableCell();
            //TableCell cell5 = new TableCell();
            //TableCell cell6 = new TableCell();
            //TableCell cell7 = new TableCell();
            //TableCell cell8 = new TableCell();
            //TableCell cell9 = new TableCell();
            //TableCell cell10 = new TableCell();
            //TableCell cell11 = new TableCell();
            for (int i = 0; i < courselist.Count; i++)
            {

                TableRow row = new TableRow();


                TableCell cell = new TableCell();
                cell.Text = courselist[i][2].ToString(); //Coursename
                cell.Style.Add("border-left", "1px solid black");
                cell.Style.Add("padding", "5px");
                row.Cells.Add(cell);

                TableCell cell2 = new TableCell();
                cell2.Text = courselist[i][1].ToString();// course title
                cell2.Style.Add("border-left", "1px solid black");
                cell2.Style.Add("padding", "5px");
                row.Cells.Add(cell2);

                TableCell cell3 = new TableCell();
                cell3.Text = courselist[i][3].ToString();//credit hours
                cell3.Style.Add("border-left", "1px solid black");
                cell3.Style.Add("padding", "5px");
                row.Cells.Add(cell3);

                TableCell cell4 = new TableCell();
                cell4.Text = "100";
                cell4.Style.Add("border-left", "1px solid black");
                cell4.Style.Add("border-right", "1px solid black");
                cell4.Style.Add("padding", "5px");
                row.Cells.Add(cell4);

                TableCell cell5 = new TableCell();
                if (ResultList[i][3].Equals(""))
                {
                    cell5.Text = ResultList[i][13].ToString();//Total
                }
                else
                {
                    cell5.Text = ResultList[i][3].ToString(); //Theory
                }
                cell5.Style.Add("padding", "5px");
                row.Cells.Add(cell5);

                TableCell cell6 = new TableCell();
                if (ResultList[i][3].Equals(""))
                    cell6.Text = "-";
                else
                    cell6.Text = ResultList[i][4].ToString();// Lab
                cell6.Style.Add("border-left", "1px solid black");
                cell6.Style.Add("padding", "5px");
                row.Cells.Add(cell6);


                TableCell cell7 = new TableCell();
                cell7.Text = ResultList[i][13].ToString();//Total
                cell7.Style.Add("border-left", "1px solid black");
                cell7.Style.Add("padding", "5px");
                row.Cells.Add(cell7);

                int totalmarks;
                try
                {
                    totalmarks = Convert.ToInt32(cell7.Text);
                }
                catch (Exception e)
                {
                    totalmarks = 70;
                }
                TableCell cell8 = new TableCell();
                cell8.Text = GetGrade(totalmarks);//Grade
                cell8.Style.Add("border-left", "1px solid black");
                cell8.Style.Add("padding", "5px");
                row.Cells.Add(cell8);

                TableCell cell9 = new TableCell();
                double gp;


                try
                {
                    gp = Convert.ToDouble(ResultList[i][5].ToString()) * 3;
                }
                catch (Exception e)
                {
                    gp = 3.2;
                }
                cell9.Text = gp.ToString();//GPA
                TotalGPa = TotalGPa + gp;
                cell9.Style.Add("border-left", "1px solid black");
                cell9.Style.Add("padding", "5px");
                row.Cells.Add(cell9);
                Table1.Rows.Add(row);

                TableCell cell10 = new TableCell();

                cell10.Text = "";
                cell10.Font.Bold = true;
                cell10.Style.Add("padding", "5px");
                cell10.Style.Add("border-left", "1px solid black");
                cell10.Style.Add("border-bottom", "1px solid black");

                row.Cells.Add(cell10);

                TableCell cell11 = new TableCell();
                cell11.Text = "";
                cell11.Style.Add("border-right", "1px solid black");
                cell11.Style.Add("border-bottom", "1px solid black");
                cell11.Style.Add("padding", "5px");
                row.Cells.Add(cell11);
                Table1.Rows.Add(row);
            }

            //int rowCount = Table1.Rows.Count;
            //rowCount = 10;
            ////for (int k = 0; k < rowCount; k++)
            ////{
            ////    //TableRow r = new TableRow();

            ////    //TableCell c = new TableCell();
            ////    //c.Text = "";
            ////    //c.ColumnSpan = 9;
            ////    //r.Cells.Add(c);
            ////    //TableCell cc = new TableCell();
            ////    //cc.Text = "";
            ////    //r.Cells.Add(cc);
            ////    //TableCell ccc = new TableCell();
            ////    //ccc.Text = "";
            ////    //r.Cells.Add(ccc);

            ////    Table1.Rows.Add(new TableRow());
            ////}

            //var emptyRow = new TableRow();
            //for (int k = 0; k < rowCount; k++)
            //{

            //    var emptyCell = new TableCell();
            //    emptyCell.Text = string.Empty;
            //    emptyRow.Cells.Add(emptyCell);

            //}
            //Table1.Rows.Add(emptyRow);


            int count = Table1.Rows.Count - 2;
            for (int i = count; i < 8; i++)
            {
                TableRow row = new TableRow();
                for (int j = 0; j < 11; j++)
                {
                    TableCell cell = new TableCell();
                    cell.Text = "<br/>";
                    cell.Style.Add("padding", "5px");

                    if (j == 3)
                    {
                        cell.Style.Add("border-left", "1px solid black");
                        cell.Style.Add("border-right", "1px solid black");
                    }
                    else if (j == 4)
                    {
                    }
                    else if (j == 10)
                    {
                        cell.Style.Add("border-right", "1px solid black");
                        cell.Style.Add("border-bottom", "1px solid black");
                    }
                    else
                        cell.Style.Add("border-left", "1px solid black");



                    row.Cells.Add(cell);
                }
                Table1.Rows.Add(row);

            }
            count = Table1.Rows.Count;
            count = Table1.Rows[9].Cells.Count;

            TableRow row1 = new TableRow();


            TableCell c1 = new TableCell();
            c1.ColumnSpan = 4;
            c1.Style.Add("border", "1px solid black");

            row1.Cells.Add(c1);

            TableCell c2 = new TableCell();
            c2.ColumnSpan = 2;
            c2.Text = "Grand Total";
            c2.Style.Add("border-top", "1px solid black");
            c2.Style.Add("border-bottom", "1px solid black");
            c2.Style.Add("padding", "5px");
            c2.Font.Bold = true;
            row1.Cells.Add(c2);

            TableCell c3 = new TableCell();
            c3.Text = grandTotal.ToString();
            c3.Font.Bold = true;
            c3.Style.Add("border-left", "1px solid black");
            c3.Style.Add("border-top", "1px solid black");
            c3.Style.Add("border-bottom", "1px solid black");
            c3.Style.Add("padding", "5px");
            row1.Cells.Add(c3);

            TableCell c4 = new TableCell();
            c4.Text = "";
            c4.Style.Add("border-left", "1px solid black");
            c4.Style.Add("border-top", "1px solid black");
            c4.Style.Add("border-bottom", "1px solid black");
            row1.Cells.Add(c4);

            TableCell c5 = new TableCell();
            c5.Text = TotalGPa.ToString();
            c5.Font.Bold = true;
            c5.Style.Add("border-left", "1px solid black");
            c5.Style.Add("border-top", "1px solid black");
            c5.Style.Add("border-bottom", "1px solid black");
            c5.Style.Add("padding", "5px");
            row1.Cells.Add(c5);

            TableCell c6 = new TableCell();
            c6.Text = "";
            c6.Style.Add("border-left", "1px solid black");
            c6.Style.Add("border-right", "1px solid black");
            row1.Cells.Add(c6);
            TableCell c7 = new TableCell();
            c7.Text = "";
            c7.Style.Add("border-right", "1px solid black");
            c7.Style.Add("border-top", "1px solid black");
            c7.Style.Add("border-bottom", "1px solid black");
            row1.Cells.Add(c7);

            //    row1.BorderColor = Color.Black;
            Table1.Rows.Add(row1);

            //for (int j = courselist.Count; j < 10; j++)
            //{
            //    cell.Text = cell.Text + "<br/>";
            //    cell2.Text = cell2.Text + "<br/>";
            //    cell3.Text = cell3.Text + "<br/>";
            //    cell4.Text = cell4.Text + "<br/>";
            //    cell5.Text = cell5.Text + "<br/>";
            //    cell6.Text = cell6.Text + "<br/>";
            //    cell7.Text = cell7.Text + "<br/>";
            //    cell8.Text = cell8.Text + "<br/>";
            //    cell9.Text = cell9.Text + "<br/>";
            //    cell10.Text = cell9.Text + "<br/>";
            //    cell11.Text = cell9.Text + "<br/>";
            //}


        }


        protected void AddGeneralInfoData()
        {
            /* ------------- Section 1 -------------- */
            div1.Controls.Add(new LiteralControl("<br/>"));
            div1.Controls.Add(new LiteralControl("<br/>"));
            div1.Controls.Add(new LiteralControl("<br/>"));
            div1.Controls.Add(new LiteralControl("<br/>"));
            Label lbl = new Label();
            lbl.Text = "University of Karachi";
            lbl.Font.Bold = true;
            lbl.Font.Size = 20;
            lbl.Style.Add("font-family", "'Times New Roman'");
            div1.Controls.Add(lbl);
            div1.Controls.Add(new LiteralControl("<br/>"));

            Label lbl1 = new Label();
            lbl1.Text = " Semester Examinations Section";
            lbl1.Font.Size = 15;
            lbl1.Style.Add("font-family", "'Times New Roman'");
            div1.Controls.Add(lbl1);
            div1.Controls.Add(new LiteralControl("<br/>"));

            int semNo = Convert.ToInt32(studentInfo[13]);

            Label sem = new Label();
            sem.Text = "Provisional Marks Sheet For Semester - " + RomanCount[semNo - 1];
            sem.Font.Size = 15;
            sem.Style.Add("font-family", "'Times New Roman'");
            div1.Controls.Add(sem);
            div1.Controls.Add(new LiteralControl("<br/>"));

            Label classname = new Label();
            classname.Text = "Degree/Class: " + studentInfo[5];
            classname.Font.Size = 15;
            classname.Style.Add("font-family", "'Times New Roman'");
            div1.Controls.Add(classname);
            div1.Controls.Add(new LiteralControl("<br/>"));

            Label currentyear = new Label();
            currentyear.Text = "ACADEMIC YEAR " + DateTime.Now.Year.ToString();
            currentyear.Font.Bold = true;
            currentyear.Font.Size = 15;
            currentyear.Style.Add("font-family", "'Times New Roman'");
            div1.Controls.Add(currentyear);
            div1.Controls.Add(new LiteralControl("<br/>"));

            /* ------------- Section 2 -------------- */
            div1.Controls.Add(new LiteralControl("<br/>"));
            div1.Controls.Add(new LiteralControl("<br/>"));
            div1.Controls.Add(new LiteralControl("<br/>"));

            Table GeneralInfoTable = new Table();
            GeneralInfoTable.GridLines = GridLines.None;
            GeneralInfoTable.CellSpacing = 10;
            GeneralInfoTable.Style.Add("text-align", "left");
            GeneralInfoTable.Width = new Unit("100%");
            GeneralInfoTable.Style.Add("font-family", "'Times New Roman'");
            // ------------------ ROW 1 ----------------- //
            TableRow row1 = new TableRow();

            TableCell cell1 = new TableCell();
            cell1.Width = new Unit("13%");

            Label nameLBL = new Label();
            nameLBL.Text = "Student's Name ";
            nameLBL.Font.Size = 13;
            nameLBL.Font.Bold = true;
            cell1.Controls.Add(nameLBL);
            row1.Controls.Add(cell1);

            TableCell cell2 = new TableCell();
            cell2.Width = new Unit("40%");

            Label stdName = new Label();
            stdName.Text = "" + studentInfo[1];
            stdName.Font.Size = 13;
            cell2.Controls.Add(stdName);
            cell2.Style.Add("border-bottom", "1px solid black");
            row1.Controls.Add(cell2);

            TableCell cell3 = new TableCell();
            cell3.Width = new Unit("13%");

            Label snoLBL = new Label();
            snoLBL.Text = "Seat No. ";
            snoLBL.Font.Size = 13;
            snoLBL.Font.Bold = true;
            cell3.Controls.Add(snoLBL);
            row1.Controls.Add(cell3);

            TableCell cell4 = new TableCell();
            cell4.Width = new Unit("34%");

            Label sno = new Label();
            sno.Text = "" + studentInfo[4].ToString();
            sno.Font.Size = 13;
            cell4.Style.Add("border-bottom", "1px solid black");
            cell4.Controls.Add(sno);
            row1.Controls.Add(cell4);

            GeneralInfoTable.Rows.Add(row1);

            // ------------------ ROW 2 ----------------- //
            TableRow row2 = new TableRow();

            TableCell cell5 = new TableCell();
            cell5.Width = new Unit("13%");

            Label fnameLBL = new Label();
            fnameLBL.Text = "Father's Name ";
            fnameLBL.Font.Size = 13;
            fnameLBL.Font.Bold = true;
            cell5.Controls.Add(fnameLBL);
            row2.Controls.Add(cell5);

            TableCell cell6 = new TableCell();
            cell6.Width = new Unit("40%");

            Label fname = new Label();
            fname.Text = "" + studentInfo[2].ToString();
            fname.Font.Size = 13;
            cell6.Style.Add("border-bottom", "1px solid black");
            cell6.Controls.Add(fname);
            row2.Controls.Add(cell6);

            TableCell cell7 = new TableCell();
            cell7.Width = new Unit("13%");

            Label enoLBL = new Label();
            enoLBL.Text = "Enrolment No. ";
            enoLBL.Font.Size = 13;
            enoLBL.Font.Bold = true;
            cell7.Controls.Add(enoLBL);
            row2.Controls.Add(cell7);

            TableCell cell8 = new TableCell();
            cell8.Width = new Unit("34%");

            Label enrol = new Label();
            enrol.Text = "" + studentInfo[3].ToString();
            enrol.Font.Size = 13;
            cell8.Style.Add("border-bottom", "1px solid black");
            cell8.Controls.Add(enrol);
            row2.Controls.Add(cell8);

            GeneralInfoTable.Rows.Add(row2);

            // ------------------ ROW 3 ----------------- //
            TableRow row3 = new TableRow();

            TableCell cell9 = new TableCell();
            cell9.Width = new Unit("13%");

            Label facultyLBL = new Label();
            facultyLBL.Text = "Faculty of: ";
            facultyLBL.Font.Size = 13;
            facultyLBL.Font.Bold = true;
            cell9.Controls.Add(facultyLBL);
            row3.Controls.Add(cell9);

            TableCell cell10 = new TableCell();
            cell10.Width = new Unit("40%");

            Label facult = new Label();
            facult.Text = "Science";
            facult.Font.Size = 13;
            cell10.Style.Add("border-bottom", "1px solid black");
            cell10.Controls.Add(facult);
            row3.Controls.Add(cell10);

            TableCell cell11 = new TableCell();
            cell11.Width = new Unit("13%");

            Label departLBL = new Label();
            departLBL.Text = "Department ";
            departLBL.Font.Size = 13;
            departLBL.Font.Bold = true;
            cell11.Controls.Add(departLBL);
            row3.Controls.Add(cell11);

            TableCell cell12 = new TableCell();
            cell12.Width = new Unit("34%");

            Label dname = new Label();
            dname.Text = "" + studentInfo[8].ToString();
            dname.Font.Size = 13;
            cell12.Style.Add("border-bottom", "1px solid black");
            cell12.Controls.Add(dname);
            row3.Controls.Add(cell12);

            GeneralInfoTable.Rows.Add(row3);
            div1.Controls.Add(GeneralInfoTable);
            div1.Controls.Add(new LiteralControl("<br/>"));
            div1.Controls.Add(new LiteralControl("<br/>"));
            div1.Controls.Add(new LiteralControl("<br/>"));
        }

        protected void DownloadPdf_Click(object sender, EventArgs e)
        {
            GeneratePdf();
            Response.ContentType = "application/octect-stream";
            Response.AppendHeader("content-disposition", "filename=user.pdf");
            Response.TransmitFile(Server.MapPath("~/PDFDoc/user.pdf"));
            Response.End();
        }

        protected void GeneratePdf()
        {
            //StringWriter sw = new StringWriter();
            //sw.Write("");
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //div1.RenderControl(hw);
            //StringReader sr = new StringReader(sw.ToString());


            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //pdfDoc.SetPageSize(PageSize.A4.Rotate());
            //PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath("~/PDFDoc/AttendanceReport.pdf"), FileMode.Create));



            //pdfDoc.Open();
            //htmlparser.Parse(sr);


            //pdfDoc.Close();


            StringWriter sw = new StringWriter();
            sw.Write("");
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            div1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());


            Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 10f);

            //  HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            pdfDoc.SetPageSize(PageSize.A3);
            pdfDoc.HtmlStyleClass = "pagecolor";
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath("~/PDFDoc/user.pdf"), FileMode.Create));



            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(
                 writer, pdfDoc, sr
               );
            //FontFactory.GetFont("Times New Roman", 14);

            // htmlparser.Parse(sr);
            pdfDoc.Close();


        }

        protected void app_Click(object sender, EventArgs e)
        {
            string q1 = null;
            q1 = "UPDATE ApprovalTable SET AdminApp='" + 1 + "' WHERE ClassId= '" + ClassID + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(q1, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            con.Close();



            chkAdminApp();
            Response.Redirect("classProforma.aspx");
        }

        private bool chkAdminApp()
        {
            bool chk = false;
            SqlConnection con = new SqlConnection(conString);
            string query = "select * from ApprovalTable where AdminApp='" + 1 + "' and ClassId= '" + ClassID + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                appBtn.Visible = false;

                Label1.Visible = true;

                chk = true;
            }


            con.Close();
            return chk;
        }

        private string GetGrade(int totalmarks)
        {
            grandTotal = grandTotal + totalmarks;
            string grade = null;
            if (totalmarks >= 90)
            {
                grade = "A+";//Grade
            }
            else if (totalmarks >= 85 && totalmarks <= 89)
            {
                grade = "A";//Grade
            }
            else if (totalmarks >= 80 && totalmarks <= 84)
            {
                grade = "A-";//Grade
            }
            else if (totalmarks >= 75 && totalmarks <= 79)
            {
                grade = "B+";//Grade
            }
            else if (totalmarks >= 71 && totalmarks <= 74)
            {
                grade = "B";//Grade
            }
            else if (totalmarks >= 68 && totalmarks <= 70)
            {
                grade = "B-";//Grade
            }
            else if (totalmarks >= 64 && totalmarks <= 67)
            {
                grade = "C+";//Grade
            }
            else if (totalmarks >= 61 && totalmarks <= 63)
            {
                grade = "C";//Grade
            }
            else if (totalmarks >= 57 && totalmarks <= 60)
            {
                grade = "C-";//Grade
            }
            else if (totalmarks >= 57 && totalmarks <= 60)
            {
                grade = "C-";//Grade
            }
            else if (totalmarks >= 53 && totalmarks <= 56)
            {
                grade = "D+";//Grade
            }
            else if (totalmarks >= 50 && totalmarks <= 52)
            {
                grade = "D";//Grade
            }
            else
            {
                grade = "FAILS";
            }

            return grade;
        }

        private bool CheckForFail(Table Table1)
        {
            bool checkResult = false;

            for (int j = 2; j < (courselist.Count + 2); j++)
            {
                if (Table1.Rows[j].Cells[7].Text.Equals("FAILS"))
                {
                    checkResult = true;
                }
                if (checkResult == true)
                {
                    break;
                }
            }



            return checkResult;

        }

        private void GetPreviuosResult(string sId)
        {
            string TotalClearCourse = "";
            int tot = 0;
            int sem = Convert.ToInt32(currentSemester);
            PreSem = new string[sem];
            for (int i = 1; i < sem; i++)
            {
                string query = "Select * from Result where SId='" + sId + "' and SemesterNo='" + (i) + "' ";
                con.Open();
                //SqlCommand cmd = new SqlCommand(query, con);
                //SqlDataReader dr = cmd.ExecuteReader();
                SqlDataAdapter sqlDa1 = new SqlDataAdapter(query, con);

                DataTable dr1 = new DataTable();
                sqlDa1.Fill(dr1);
                if (dr1.Rows.Count > 0)
                {
                    TotalClearCourse = "/" + dr1.Rows.Count.ToString();
                    tot = dr1.Rows.Count;
                }
                else
                {
                    TotalClearCourse = "/4";
                    tot = 4;
                }
                con.Close();
                string remarks = "Fails";

                string query1 = "Select * from Result where SId='" + sId + "' and SemesterNo='" + (i + 1) + "' and Remarks='" + remarks + "'";
                con.Open();
                //SqlCommand cmd = new SqlCommand(query, con);
                //SqlDataReader dr = cmd.ExecuteReader();
                SqlDataAdapter sqlDa = new SqlDataAdapter(query1, con);

                DataTable dr = new DataTable();
                sqlDa.Fill(dr);
                if (dr.Rows.Count > 0)
                {
                    tot = tot - dr.Rows.Count;
                    TotalClearCourse = tot.ToString() + TotalClearCourse;
                }
                else
                {
                    TotalClearCourse = tot.ToString() + "/4";
                }
                con.Close();
                TotalClearCourse = "4/4";
                PreSem[i-1] = TotalClearCourse;
            }

        }

        public void AddRomanCount(Table Table1)
        {
            for (int j = 1; j < Table1.Rows.Count - 2; j++)
            {
                Table1.Rows[j].Cells[9].Font.Bold = true;
                Table1.Rows[j].Cells[9].Text = RomanCount[j - 1];
                Table1.Rows[j].Cells[9].Style.Add("border-bottom", "1px solid black");
                Table1.Rows[j].Cells[9].Style.Add("border-right", "1px solid black");

            }
            Table1.Rows[Table1.Rows.Count - 2].Cells[9].Text = RomanCount[8];
            Table1.Rows[Table1.Rows.Count - 2].Cells[9].Font.Bold = true;
            Table1.Rows[Table1.Rows.Count - 2].Cells[9].Style.Add("border-right", "1px solid black");
            Table1.Rows[Table1.Rows.Count - 1].Cells[5].Text = RomanCount[9];
            Table1.Rows[Table1.Rows.Count - 1].Cells[5].Font.Bold = true;
            Table1.Rows[Table1.Rows.Count - 1].Cells[5].Style.Add("border-bottom", "1px solid black");
            Table1.Rows[Table1.Rows.Count - 1].Cells[5].Style.Add("border-top", "1px solid black");
            //   int semNo = Convert.ToInt32(currentSemester);

            for (int j = 0; j < PreSem.Length; j++)
            {
                Table1.Rows[j + 1].Cells[10].Text = PreSem[j];
                Table1.Rows[j + 1].Cells[10].Style.Add("border-right", "1px solid black");
                Table1.Rows[j + 1].Cells[10].Style.Add("border-bottom", "1px solid black");
            }

        }

        protected void backbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("classesList.aspx");
        }
    }

}