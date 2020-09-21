using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using iTextSharp.tool.xml;
using System.Drawing;

namespace UokSemesterSystem
{
    public partial class AdminResult : System.Web.UI.Page
    {
        static string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(conString);
        string ClassID,CurrentSemester;
        List<String> CourseID = new List<String>();
        List<String> CourseNo = new List<String>();
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (ViewState["PreviousPage"] != null)  //Check if the ViewState 
                                                    //contains Previous page URL
            {
                Response.Redirect(ViewState["PreviousPage"].ToString());//Redirect to 
                                                                        //Previous page by retrieving the PreviousPage Url from ViewState.
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack) //check if the webpage is loaded for the first time.
            {
                ViewState["PreviousPage"] =
            Request.UrlReferrer;//Saves the Previous page url in ViewState
            }

           

            string query1 = "Select DepartmentName from Department where DId='" + Session["DID"].ToString() + "' ";
            con.Open();

            SqlCommand com = new SqlCommand(query1, con);
            SqlDataReader dr = com.ExecuteReader();

            dr.Read();

            if (dr.HasRows)
            {

                lbl_deptName.Text = dr["DepartmentName"].ToString();
            }
            con.Close();
            ClassID = Session["ClassID"].ToString();

            string query = "Select SemesterNo from Student where ClassId='" + ClassID + "' ";
            con.Open();

            SqlCommand comm = new SqlCommand(query, con);
            SqlDataReader drr = comm.ExecuteReader();

            drr.Read();

            if (drr.HasRows)
            {

                CurrentSemester = drr["SemesterNo"].ToString();
            }
            con.Close();
           

            string query2 = "Select ClassName from ClassTable where ClassId='" + ClassID + "' ";
            con.Open();

            SqlCommand com1 = new SqlCommand(query2, con);
            SqlDataReader dr1 = com1.ExecuteReader();
            dr1.Read();

            if (dr1.HasRows)
            {

                lbl_className.Text = dr1["ClassName"].ToString();
            }
            con.Close();
            lbl_dYear.Text = DateTime.Now.Year.ToString();

            TableHeader();

            if (CheckResultAvailability())///
            {
                for (int j = 1; j < ResultSheetTable.Rows.Count; j++)
                {
                    ResultSheetTable.Rows[j].Cells[SheetHeader.Cells.Count - 1].Enabled = false;
                    //ResultSheetTable.Rows[j].Cells[SheetHeader.Cells.Count - 1].CssClass = "generatebtnDisable";

                    //if (j % 2 != 0)
                    //{
                    //    ResultSheetTable.Rows[j].Cells[SheetHeader.Cells.Count - 1].BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                    //}
                    //else
                    //{
                    //    ResultSheetTable.Rows[j].Cells[SheetHeader.Cells.Count - 1].BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

                    //}

                }
                classProforma.Visible = false;
            }
        }
        public void TableHeader()
        {

            GetCourses();
            TableCell cell1 = new TableCell();
            cell1.Text = "Roll Number";
            cell1.CssClass = "tcell";
            SheetHeader.Cells.Add(cell1);
            TableCell cell = new TableCell();
            cell.Text = "Student Name";
            cell.CssClass = "tcell";
            SheetHeader.Cells.Add(cell);

            for (int i = 0; i < CourseNo.Count; i++)
            {
                TableCell celli = new TableCell();
                celli.CssClass = "tcell";
                celli.Text = CourseNo[i].ToString();
                SheetHeader.Cells.Add(celli);
            }

            TableCell emp = new TableCell();
            emp.CssClass = "tcell";
            emp.Text = "";
            SheetHeader.Cells.Add(emp);




        }



        public void GetCourses()
        {

            int semm = Convert.ToInt32(CurrentSemester)-1;
            string query1 = "Select distinct(CourseId) from TimeTable where ClassID='" + ClassID + "' and SemesterNo='"+semm + "'";
            con.Open();

            SqlDataAdapter sqlDa1 = new SqlDataAdapter(query1, con);

            DataTable dr1 = new DataTable();
            sqlDa1.Fill(dr1);
            for (int a = 0; a < dr1.Rows.Count; a++)
            {
                CourseID.Add(dr1.Rows[a]["CourseId"].ToString());
            }
            con.Close();

            for (int a = 0; a < dr1.Rows.Count; a++)
            {
                string query2 = "Select CourseNo from Course where CourseId='" + CourseID[a] + "' ";
                con.Open();

                SqlDataAdapter sqlDa = new SqlDataAdapter(query2, con);

                DataTable dr = new DataTable();
                sqlDa.Fill(dr);

                CourseNo.Add(dr.Rows[0]["CourseNo"].ToString());
                con.Close();
            }

            AddData();

        }

        public void AddData()
        {
            List<String> Tmarks = new List<String>();


            string query = "Select SName,RollNumber,SId from Student where ClassID='" + ClassID + "' ";
            con.Open();
            //SqlCommand cmd = new SqlCommand(query, con);
            //SqlDataReader dr = cmd.ExecuteReader();
            SqlDataAdapter sqlDa1 = new SqlDataAdapter(query, con);

            DataTable dr1 = new DataTable();
            sqlDa1.Fill(dr1);
            con.Close();
            for (int i = 0; i < dr1.Rows.Count; i++)
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                cell.Text = dr1.Rows[i]["RollNumber"].ToString();
                cell.CssClass = "tcell";
                row.Cells.Add(cell);
                TableCell cell1 = new TableCell();
                cell1.Text = dr1.Rows[i]["SName"].ToString();
                cell.CssClass = "tcell";
                row.Cells.Add(cell1);


                for (int j = 0; j < CourseID.Count; j++)
                {




                    string Query = "select TotalMarks from Result where SId='" + dr1.Rows[i]["SId"].ToString() + "'" +
                   "and CourseId='" + CourseID[j] + "' ";
                    con.Close();
                    con.Open();
                    //SqlCommand cmd = new SqlCommand(Query, con);
                    //SqlDataReader dr = cmd.ExecuteReader();
                    SqlDataAdapter sqlDa = new SqlDataAdapter(Query, con);

                    DataTable dr = new DataTable();
                    sqlDa.Fill(dr);

                    //Tmarks.Add(dr.Rows[0]["TotalMarks"].ToString());
                    TableCell cell2 = new TableCell();

                    try
                    {
                        cell2.Text = dr.Rows[0]["TotalMarks"].ToString();
                        cell.CssClass = "tcell";
                        con.Close();
                        if (!CheckForApproval(CourseID[j])) 
                        {
                            cell2.Text = "N/A";
                        }
                        row.Cells.Add(cell2);
                    }
                    catch (Exception ex)
                    {
                        cell2.Text = "N/A";
                        cell.CssClass = "tcell";
                        row.Cells.Add(cell2);
                    }






                    con.Close();


                }

                TableCell cell6 = new TableCell();
                cell6.CssClass = "tcell";
                Button p = new Button();
                if (Session["AccountType"].Equals("ChairPerson"))
                {
                    p.Text = "View Proforma";
                }
                else
                {
                    p.Text = " Generate Proforma";
                }
                p.ID = "p_" + i;
                p.CssClass = "generatebtn";
                p.Click += new EventHandler(presentClick);
                cell6.Controls.Add(p);
                row.Cells.Add(cell6);
                if(i%2 != 0)
                {
                    row.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                }
                ResultSheetTable.Rows.Add(row);



            }

        }
        private void presentClick(object sender, EventArgs e)
        {
            string[] temp = ((Button)sender).ID.Split('_');
            int id = Convert.ToInt32(temp[1]);
            string rolnumber = ResultSheetTable.Rows[id + 1].Cells[0].Text;

            string q = "Select SId from Student where RollNumber='" + rolnumber + "'";
            con.Open();
            SqlCommand com = new SqlCommand(q, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();

            if (dr.HasRows)
            {
                Session["SID"] = dr["SId"].ToString();
                string sid = Session["SID"].ToString();
            }
            con.Close();

            Response.Redirect("proforma.aspx");
        }

        protected void fixedbutton_Click(object sender, EventArgs e)
        {
            Approve.Visible = true;
            fixedbutton.Visible = false;
        }

        protected void PdfGenerator_Click(object sender, EventArgs e)
        {
            //StringWriter sw = new StringWriter();
            //sw.Write("");
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //ResultReport.RenderControl(hw);
            //StringReader sr = new StringReader(sw.ToString());


            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //pdfDoc.SetPageSize(PageSize.A4.Rotate());
            //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath("~/PDFDoc/user.pdf"), FileMode.Create));



            //pdfDoc.Open();
            //XMLWorkerHelper.GetInstance().ParseXHtml(
            //     writer, pdfDoc, sr
            //   );
            ////FontFactory.GetFont("Times New Roman", 14);

            //// htmlparser.Parse(sr);
            //pdfDoc.Close();
            //Response.ContentType = "application/octect-stream";
            //Response.AppendHeader("content-disposition", "filename=user.pdf");
            //Response.TransmitFile(Server.MapPath("~/PDFDoc/user.pdf"));
            //Response.End();
        }

        private bool CheckResultAvailability()
        {
            bool checkResult = false;
            for (int i = 2; i < SheetHeader.Cells.Count; i++)
            {
                for (int j = 0; j < ResultSheetTable.Rows.Count; j++)
                {
                    if (ResultSheetTable.Rows[j].Cells[i].Text.Equals("N/A"))
                    {
                        checkResult = true;
                       
                    }
                }

                if (checkResult == true)
                {
                    break;
                }
            }
            return checkResult;

        }
        protected void classProforma_Click(object sender, EventArgs e)
        {
            Response.Redirect("classProforma.aspx");
        }

        private bool CheckForApproval(string courseId) 
        {
            bool check = false;
            string query = "Select * from ApprovalTable where CourseId='" + courseId + "' and ChairPApp='1' and ClassId='"+ClassID+"'";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();

            if (dr.HasRows)
            {
                check = true;
            }
            con.Close();
            return check;
        }

    }
}