using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Reporting.WebForms;


namespace UokSemesterSystem
{
    public partial class Attendance : System.Web.UI.Page
    {
        static string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(conString);

        string TId;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            // reportDiv.Visible = false;
            TId = Session["AccountId"].ToString();
            string id = Session["classId"].ToString();
            getSheet();



        }

        private void getSheet()
        {

            string query = "Select * from Student where ClassID='" + Session["classId"].ToString() + "' ";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            int i = 1;
            while (dr.Read())
            {
                if (i == 1)
                {
                    departValue.Text = dr["Department"].ToString();
                    mDepartValue.Text = dr["Department"].ToString();
                    yearValue.Text = dr["Year"].ToString();
                    semValue.Text = dr["SemesterNo"].ToString();
                }
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                cell1.Text = i.ToString();
                cell1.CssClass = "backcell";
                row.Cells.Add(cell1);
                TableCell cell2 = new TableCell();
                cell2.Text = dr["RollNumber"].ToString();
                cell2.CssClass = "backcell";
                row.Cells.Add(cell2);
                TableCell cell3 = new TableCell();
                cell3.Text = dr["SName"].ToString();
                cell3.CssClass = "backcell";
                row.Cells.Add(cell3);
                TableCell cell4 = new TableCell();
                cell4.Text = dr["FatherName"].ToString();
                cell4.CssClass = "backcell";
                row.Cells.Add(cell4);
                TableCell cell5 = new TableCell();
                cell5.Text = "P";
                cell5.CssClass = "backcell";
                row.Cells.Add(cell5);
                TableCell cell6 = new TableCell();
                cell6.CssClass = "backcell";
                Button p = new Button();
                p.Text = "Present";
                p.ID = "p_" + i;
                p.CssClass = "OptionButtons1";
                p.Click += new EventHandler(presentClick);
                Button a = new Button();
                a.Text = "Absent";
                a.ID = "a_" + i;
                a.CssClass = "OptionButtons2";
                a.Click += new EventHandler(absentClick);
                Button l = new Button();
                l.Text = "Leave";
                l.ID = "l_" + i;
                l.CssClass = "OptionButtons3";
                l.Click += new EventHandler(leaveClick);
                cell6.Controls.Add(p);
                cell6.Controls.Add(a);
                cell6.Controls.Add(l);
                row.Cells.Add(cell6);
                if (i % 2 == 0)
                {
                    row.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                }
                AttendanceSheetTable.Rows.Add(row);
                i++;
            }
            con.Close();

            con.Open();
            cmd = new SqlCommand("select * from ClassTable where ClassID='" + Session["classId"].ToString() + "' ", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            classValue.Text = dr["ClassName"].ToString();
            con.Close();

            con.Open();
            cmd = new SqlCommand("select TName from Teacher where TId = '" + TId + "' ", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            tNameValue.Text = dr["TName"].ToString();
            con.Close();

            con.Open();
            cmd = new SqlCommand("select * from Course where CourseId='" + Session["courseId"].ToString() + "' ", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            courseNoValue.Text = dr["CourseNo"].ToString();
            courseNameValue.Text = dr["CourseName"].ToString();
            con.Close();

            dateValue.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }

        private DataTable getDataTable()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("select * from Student where ClassID='" + Session["classId"].ToString() + "' ", con);

            sda.Fill(dt);

            int j = 1;
            dt.Columns.Add("Status", typeof(System.String));

            foreach (DataRow row in dt.Rows)
            {
                row["Status"] = AttendanceSheetTable.Rows[j].Cells[4].Text;
                row["SId"] = j;
                j++;
            }

            return dt;
        }
        private void presentClick(object sender, EventArgs e)
        {
            string[] temp = ((Button)sender).ID.Split('_');
            int id = Convert.ToInt32(temp[1]);
            AttendanceSheetTable.Rows[id].Cells[4].Text = "P";
        }
        private void absentClick(object sender, EventArgs e)
        {
            string[] temp = ((Button)sender).ID.Split('_');
            int id = Convert.ToInt32(temp[1]);
            AttendanceSheetTable.Rows[id].Cells[4].Text = "A";
        }
        private void leaveClick(object sender, EventArgs e)
        {
            string[] temp = ((Button)sender).ID.Split('_');
            int id = Convert.ToInt32(temp[1]);
            AttendanceSheetTable.Rows[id].Cells[4].Text = "L";
        }



        protected void saveSheet_Click(object sender, EventArgs e)
        {
            reportDiv.Visible = true;
            div1.Visible = false;
            string q1, q2, Sid = null;
            // TextBox1.Text=i.ToString();
            for (int i = 1; i < AttendanceSheetTable.Rows.Count; i++)
            {
                q1 = "Select SId, ClassID from Student where RollNumber='" + AttendanceSheetTable.Rows[i].Cells[1].Text + "' ";
                con.Open();
                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                    Sid = dr[0].ToString();
                con.Close();

                q2 = "insert into Attendance(SId,Status,CourseId,TId,Date,ClassId) values (@sid,@status,@cid,@tid,@date,@class) ";
                con.Open();
                SqlCommand com = new SqlCommand(q2, con);
                com.Parameters.AddWithValue("@sid", Sid);
                com.Parameters.AddWithValue("@status", AttendanceSheetTable.Rows[i].Cells[4].Text);
                com.Parameters.AddWithValue("@cid", Session["courseId"].ToString());
                com.Parameters.AddWithValue("@tid", TId);
                com.Parameters.AddWithValue("@date", dateValue.Text);
                com.Parameters.AddWithValue("@class", Session["classId"].ToString());
                dr = com.ExecuteReader();

                con.Close();

            }
            Response.Write(@"<script language='javascript'> alert('Attendance has been saved!')</script>");
            Response.Redirect("AttToTeacher.aspx");
            //  export();
        }


        protected void export()
        {
            DataTable dt = getDataTable();
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("select * from ClassTable where ClassID='" + Session["classId"].ToString() + "' ", con);
            sda.Fill(dt1);
            DataTable dt2 = new DataTable();
            sda = new SqlDataAdapter("select TName from Teacher where TId='" + TId + "' ", con);
            sda.Fill(dt2);
            DataTable dt3 = new DataTable();
            sda = new SqlDataAdapter("select * from Course where CourseId='" + Session["courseId"].ToString() + "' ", con);
            sda.Fill(dt3);

            DataTable dt4 = new DataTable();
            sda = new SqlDataAdapter("select * from Attendance where CourseId='" + Session["courseId"].ToString() + "' and TId='" + TId + "' and ClassId='" + Session["classId"].ToString() + "'  ", con);
            sda.Fill(dt4);

            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report1.rdlc");

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + "/AttendanceSheet.rdlc";
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("StudentDS", dt));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("AttDS", dt4));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CourseDS", dt3));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ClassDS", dt1));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TeacherDS", dt2));

            ReportViewer1.DataBind();
            ReportViewer1.LocalReport.Refresh();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}