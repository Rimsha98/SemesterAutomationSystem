using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Reporting.WebForms;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;

namespace UokSemesterSystem
{
    public partial class AttToAdmin : System.Web.UI.Page
    {
        static string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(conString);
        string classId;
        List<string> courseId, sId;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            // if (Session["Id"] == null)
            //   Response.Redirect("Login.aspx");
            if (Session["cId"] == null)
                Response.Redirect("classesList.aspx");

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            classId = Session["cId"].ToString();
            getSheet();
            getPercentage();
        }

        private void getSheet()
        {
            sId = new List<string>();
            string sem = null;
            string query = "Select * from Student where ClassID='" + classId + "' ";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            int i = 1;
            while (dr.Read())
            {
                sem = dr["SemesterNo"].ToString();
                if (i == 1)
                    departLabel.Text = dr["Department"].ToString();

                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                cell1.CssClass = "backcell";
                cell1.Text = i.ToString();
                row.Cells.Add(cell1);
                TableCell cell2 = new TableCell();
                cell2.CssClass = "backcell";
                cell2.Text = dr["RollNumber"].ToString(); ;
                row.Cells.Add(cell2);
                TableCell cell3 = new TableCell();
                cell3.CssClass = "backcell";
                cell3.Text = dr["SName"].ToString();
                row.Cells.Add(cell3);
                TableCell cell4 = new TableCell();
                cell4.CssClass = "backcell";
                cell4.Text = dr["FatherName"].ToString(); ;
                row.Cells.Add(cell4);
                if (i % 2 == 0)
                {
                    row.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                }
                attTable.Rows.Add(row);

                sId.Add(dr["SId"].ToString());
                i++;
            }
            con.Close();

            //   getCourses
            courseId = new List<string>();

            query = "Select Distinct(CourseId) from TimeTable where ClassId='" + classId + "' and SemesterNo='" + sem + "' ";
            con.Open();
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                courseId.Add(dr[0].ToString());
            }
            con.Close();

            for (i = 0; i < courseId.Count; i++)
            {
                query = "Select CourseAbb,CourseNo from Course where CourseId='" + courseId[i] + "' ";
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TableCell cell = new TableCell();
                    cell.Text = dr["CourseAbb"].ToString() + "(" + dr["CourseNo"].ToString() + ")\n%age";
                    attTable.Rows[0].Cells.Add(cell);
                }
                con.Close();
            }

            query = "Select ClassName from ClassTable where ClassId='" + classId + "' ";
            con.Open();
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            classLabel.Text = dr[0].ToString();
            con.Close();
        }


        private void getPercentage()
        {
            double pCount = 0, totalClasses = 0, percentage;
            SqlCommand cmd;
            SqlDataReader dr;
            string query;


            for (int i = 0; i < sId.Count; i++)
            {
                for (int j = 0; j < courseId.Count; j++)
                {
                    query = "Select Status from Attendance where SId='" + sId[i] + "' and CourseId='" + courseId[j] + "' ";
                    con.Open();
                    cmd = new SqlCommand(query, con);
                    dr = cmd.ExecuteReader();
                    TableCell cell = new TableCell();
                    cell.CssClass = "backcell";
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            totalClasses++;
                            if (dr["Status"].ToString().Equals("P"))
                                pCount++;

                        }
                        percentage = (Convert.ToDouble(pCount) / Convert.ToDouble(totalClasses)) * 100;



                        cell.Text = Math.Round(percentage, 0).ToString();
                        attTable.Rows[i + 1].Cells.Add(cell);



                        pCount = 0;
                        totalClasses = 0;
                    }
                    else
                    {
                        cell.Text = "N/A";
                        attTable.Rows[i + 1].Cells.Add(cell);

                    }
                    con.Close();
                }
            }
        }

        protected void go_back(object sender, EventArgs e)
        {
            Response.Redirect("classesList.aspx");
        }

        protected void pdf_Click(object sender, EventArgs e)
        {
            StringWriter sw = new StringWriter();
            sw.Write("");
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            sheet.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());


            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            pdfDoc.SetPageSize(PageSize.A4.Rotate());
            PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath("~/PDFDoc/AttendanceReport.pdf"), FileMode.Create));



            pdfDoc.Open();
            htmlparser.Parse(sr);


            pdfDoc.Close();
            Response.ContentType = "application/octect-stream";
            Response.AppendHeader("content-disposition", "filename=AttendanceReport.pdf");
            Response.TransmitFile(Server.MapPath("~/PDFDoc/AttendanceReport.pdf"));
            Response.End();
        }

        protected void download_Click(object sender, EventArgs e)
        {
            sheet.Visible = false;

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Student where ClassID='" + classId + "'", con);
            sda.Fill(dt);

            int k = 1;
            foreach (DataRow dr in dt.Rows)
            {
                dr["SId"] = k;
                k++;
            }

            DataTable[] courseDT = new DataTable[courseId.Count];
            for (int i = 0; i < courseId.Count; i++)
            {
                courseDT[i] = new DataTable();

                string query = "Select CourseAbb,CourseNo from Course where CourseId='" + courseId[i] + "' ";
                sda = new SqlDataAdapter(query, con);
                sda.Fill(courseDT[i]);

            }

            DataTable[] perDt = new DataTable[courseId.Count];
            for (int i = 0; i < perDt.Length; i++)
            {
                perDt[i] = new DataTable();
                sda = new SqlDataAdapter("Select Major from Student where ClassID='" + classId + "'", con);
                sda.Fill(perDt[i]);
                k = 1;
                foreach (DataRow dr in perDt[i].Rows)
                {
                    dr["Major"] = attTable.Rows[k].Cells[i + 4].Text;
                    k++;
                }

            }

            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report1.rdlc");

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + "/AttReport.rdlc";
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("StudentDS", dt));
            for (int j = 1; j <= courseDT.Length; j++)
            {
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CourseDS" + j, courseDT[j - 1]));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("AttDS" + j, perDt[j - 1]));

            }//  ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CourseDS2", courseDT[1]));

            // ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ClassDS", dt1));
            // ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TeacherDS", dt2));

            ReportViewer1.DataBind();
            ReportViewer1.LocalReport.Refresh();
        }
    }

}