using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UokSemesterSystem
{
    public partial class proforma : System.Web.UI.Page
    {
        static string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(conString);
        int grandTotal = 0;
        string[] PreSem;
        double TotalGPa = 0;
        string currentSemester = "";
        string SID = "1", ClassID;
        string[] courseIDs;
        string[] studentInfo;
        string[] RomanCount;

        private List<string[]> courselist = new List<string[]>();
        private List<string[]> ResultList = new List<string[]>();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SID = Session["SID"].ToString();
            RomanCounteing();
            TableHeader();
            GetStudent();
            GetCourses();
            GetResult();
            AddData();
            AddGeneralInfoData();
            GetPreviuosResult();
            AddRomanCount();
            // GeneratePdf();





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

        public void TableHeader()
        {

            Table1.Font.Size = 12;
            Table1.Style.Add("font-family", "Times New Roman");
            emptycell.Style.Add("border", "1px solid black");
            emptycell.Style.Add("padding", "5px");
            marksobt.Font.Bold = true;
            marksobt.Style.Add("border-top", "1px solid black");
            marksobt.Style.Add("border-bottom", "1px solid black");
            marksobt.Style.Add("padding", "5px");
            coursee.Font.Bold = true;
            coursee.Style.Add("border-left", "1px solid black");
            coursee.Style.Add("border-top", "1px solid black");
            coursee.Style.Add("border-bottom", "1px solid black");
            coursee.Style.Add("padding", "5px");
            coursecleared.Font.Size = 10;
            coursecleared.Font.Bold = true;
            coursecleared.Style.Add("border", "1px solid black");
            coursecleared.Style.Add("padding", "5px");

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
        protected void GetStudent()
        {
            string q = "Select * from Student where SId='" + SID + "'";
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
            int semm = Convert.ToInt32(currentSemester)-1;
            string q = "Select distinct(CourseId) from Timetable where ClassID='" + ClassID + "'and SemesterNo='"+semm+"'";
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

        protected void GetResult()
        {
            for (int i = 0; i < courseIDs.Length; i++)
            {
                string[] ResultInfo;
                string q = "select * from Result where SId='" + SID + "'and ClassId='" + ClassID + "' and CourseId='" + courseIDs[i] + "'";

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

        protected void AddData()
        {

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
                int totalmarks = 0;
                try
                {
                    totalmarks = Convert.ToInt32(cell7.Text);
                }
                catch (Exception e)
                {
                    totalmarks = 90;
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
                    gp = 3.5 * 3;
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


            LinesTable.Width = new Unit("100%");
            LinesTable.CellSpacing = 10;
            LinesTable.GridLines = GridLines.None;
            LinesTable.Style.Add("text-align", "left");
            LinesTable.Style.Add("font-family", "Times New Roman");
            LinesTable.Font.Size = 13;

            cellone.Width = new Unit("10%");
            cellone.Style.Add("padding", "5px");

            celltwo.Width = new Unit("90%");
            celltwo.Style.Add("border-bottom", "1px solid black");


            cellthree.Width = new Unit("10%");
            cellthree.Style.Add("padding", "5px");

            cellfour.Width = new Unit("90%");
            cellfour.Style.Add("border-bottom", "1px solid black");

            cellfive.Style.Add("border-bottom", "1px solid black");
            cellfive.Text = ".";
            cellfive.ForeColor = System.Drawing.Color.FromArgb(255, 255, 204);


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

        //protected void AddData()
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

        protected void AddGeneralInfoData()
        {
            celluno.Style.Add("border-bottom", "1px solid black");
            celldos.Style.Add("border-bottom", "1px solid black");
            celltres.Style.Add("border-bottom", "1px solid black");
            cellcuatro.Style.Add("border-bottom", "1px solid black");
            cellcinco.Style.Add("border-bottom", "1px solid black");
            cellseis.Style.Add("border-bottom", "1px solid black");

            int semNo = Convert.ToInt32(studentInfo[13]);
            Label3.Text = Label3.Text + " " + RomanCount[semNo - 1];
            classname.Text = studentInfo[5];
            currentyear.Text = DateTime.Now.Year.ToString();
            stdname.Text = studentInfo[1];
            sno.Text = studentInfo[4].ToString();
            fname.Text = studentInfo[2].ToString();
            enrol.Text = studentInfo[3].ToString();
            dname.Text = studentInfo[8].ToString();
            facult.Text = "Science";

            var temp = DateTime.Now;
            var date = temp.ToShortDateString();

            Date.Text = date.ToString();

            if (CheckForFail())
            {
                result.Text = "FAIL";
            }
            else
            {
                result.Text = "PASSES";
            }


        }


        protected void DownloadPdf_Click(object sender, EventArgs e)
        {
            StringWriter sw = new StringWriter();
            sw.Write("");
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            Proforma.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());


            Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 10f);
            //  HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            pdfDoc.SetPageSize(PageSize.A3);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath("~/PDFDoc/user.pdf"), FileMode.Create));



            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(
                 writer, pdfDoc, sr
               );
            //FontFactory.GetFont("Times New Roman", 14);

            // htmlparser.Parse(sr);
            pdfDoc.Close();

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
            //Proforma.RenderControl(hw);
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
            Proforma.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());


            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            //  HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            pdfDoc.SetPageSize(PageSize.A4.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath("~/PDFDoc/user.pdf"), FileMode.Create));



            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(
                 writer, pdfDoc, sr
               );
            //FontFactory.GetFont("Times New Roman", 14);

            // htmlparser.Parse(sr);
            pdfDoc.Close();


            //String eventTemplate = Server.MapPath("~/PDFDoc/user.pdf");

            //String SinglePreview = Server.MapPath("~/PDFDoc/user.pdf");

            //String PDFPreview = Server.MapPath("~/PDFDoc/user.pdf");

            //String previewPDFs = Server.MapPath("~/PDFDoc/user.pdf");

            //if (System.IO.File.Exists((String)eventTemplate))
            //{
            //    StringWriter sw = new StringWriter();
            //    sw.Write("");
            //    HtmlTextWriter hw = new HtmlTextWriter(sw);
            //    Proforma.RenderControl(hw);
            //    StringReader sr = new StringReader(sw.ToString());


            //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            //    //  HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //    pdfDoc.SetPageSize(PageSize.A4.Rotate());
            //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath("~/PDFDoc/user.pdf"), FileMode.Create));



            //    pdfDoc.Open();
            //    XMLWorkerHelper.GetInstance().ParseXHtml(
            //         writer, pdfDoc, sr
            //       );
            //    //FontFactory.GetFont("Times New Roman", 14);

            //    // htmlparser.Parse(sr);
            //    pdfDoc.Close();
            //    //Trying to merge
            //    String[] previewsSmall = new String[1];
            //    previewsSmall[0] = PDFPreview;
            //    string outFile= Server.MapPath("~/PDFDoc/merge.pdf");
            //    CombineMultiplePDFs(previewsSmall, outFile);
            // }  
        }

        //protected void MultipleDiv_Click(object sender, EventArgs e)
        //{
        //    //   divContainer.InnerHtml = "<div style=\"color:#ff0000;\">some new content goes here</div>";
        //    //Proforma.Visible = true;
        //    //PlaceHolder1.Controls.Add(Proforma);
        //    //for (int i = 0; i < 2; i++)
        //    //{
        //    //    PlaceHolder1.Controls.Add(Proforma);
        //    //}
        //    //System.Web.UI.HtmlControls.HtmlGenericControl divContent = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        //    //divContent.ID = "divContent";
        //    //divContent.Style.Add(HtmlTextWriterStyle.Color, "Red");
        //    //divContent.InnerHtml = "some new content goes here";
        //    //divContent.Controls.Add(Proforma);
        //    //divContainer.Controls.Add(divContent);


        //    //for (int i = 0; i < 2; i++)
        //    //{
        //    //    System.Web.UI.HtmlControls.HtmlGenericControl divContent = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        //    //    divContent.ID = "divContent";
        //    //    divContent.Style.Add(HtmlTextWriterStyle.Color, "Red");
        //    //   // divContent.InnerHtml = "some new content goes here";
        //    //    divContent.Controls.Add(Proforma);
        //    //    //divContainer.InnerHtml = "<div style=\"color:#ff0000;\">some new content goes here</div>";
        //    //    //Proforma.Visible = true;
        //    //    //PlaceHolder1.Controls.Add(Proforma);

        //    //    TableRow row = new TableRow();
        //    //    TableCell cell = new TableCell();
        //    //    cell.Controls.Add(divContent);
        //    //    row.Cells.Add(cell);
        //    //    ProformaList.Rows.Add(row);
        //    //}




        //}

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

        private bool CheckForFail()
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

        private void GetPreviuosResult()
        {
            string TotalClearCourse = "";
            int tot = 0;
            int sem = Convert.ToInt32(currentSemester);
            PreSem = new string[sem];
            for (int i = 1; i < sem; i++)
            {
                string query = "Select * from Result where SId='" + SID + "' and SemesterNo='" + (i + 1) + "' ";
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

                string query1 = "Select * from Result where SId='" + SID + "' and SemesterNo='" + (i) + "' and Remarks='" + remarks + "'";
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
                // PreSem[i] = TotalClearCourse;
                TotalClearCourse = "4/4";
                PreSem[i-1] = TotalClearCourse;
            }

        }

        public void AddRomanCount()
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


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //}
        }

        //    public static void CombineMultiplePDFs(string[] fileNames, string outFile)
        //    {
        //        // step 1: creation of a document-object
        //        Document document = new Document();
        //        //create newFileStream object which will be disposed at the end
        //        using (FileStream newFileStream = new FileStream(outFile, FileMode.Create))
        //        {
        //            // step 2: we create a writer that listens to the document
        //            PdfCopy writer = new PdfCopy(document, newFileStream);
        //            if (writer == null)
        //            {
        //                return;
        //            }

        //            // step 3: we open the document
        //            document.Open();

        //            foreach (string fileName in fileNames)
        //            {
        //                // we create a reader for a certain document
        //                PdfReader reader = new PdfReader(fileName);
        //                reader.ConsolidateNamedDestinations();

        //                // step 4: we add content
        //                for (int i = 1; i <= reader.NumberOfPages; i++)
        //                {
        //                    PdfImportedPage page = writer.GetImportedPage(reader, i);
        //                    writer.AddPage(page);
        //                }

        //                PRAcroForm form = reader.AcroForm;
        //                if (form != null)
        //                {
        //                    writer.CopyDocumentFields(reader);
        //                   // writer.CopyAcroForm(reader);
        //                }

        //                reader.Close();
        //            }

        //            // step 5: we close the document and writer
        //            writer.Close();
        //            document.Close();
        //        }//disposes the newFileStream object
        //    }

    }
}