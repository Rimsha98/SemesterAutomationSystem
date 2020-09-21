using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace UokSemesterSystem
{
    public partial class StudentAttendance : System.Web.UI.Page
    {
        static string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(conString);

        string sId;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            sId = Session["AccountId"].ToString();
            getAttendance();
        }

        private void getAttendance()
        {
            List<string> courseId = new List<string>();
            List<string> tId = new List<string>();
            string classId = null, sem = null;
            string query = "Select ClassID, SemesterNo from Student where SId='" + sId + "' ";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            classId = dr[0].ToString();
            sem = dr["SemesterNo"].ToString();
            con.Close();

            query = "Select Distinct(CourseId) from TimeTable where ClassId='" + classId + "' and SemesterNo='" + sem + "' ";
            con.Open();
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                courseId.Add(dr["CourseId"].ToString());
                //tId.Add(dr["TId"].ToString());

            }
            con.Close();

            query = "Select TId from TimeTable where ClassId='" + classId + "' and SemesterNo='" + sem + "' ";
            con.Open();
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //  courseId.Add(dr["CourseId"].ToString());
                tId.Add(dr["TId"].ToString());

            }
            con.Close();

            for (int i = 0; i < courseId.Count; i++)
            {

                query = "Select TName from Teacher where TId='" + tId[i] + "' ";
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();
                dr.Read();
                tId[i] = dr["TName"].ToString();
                con.Close();
            }


            for (int i = 0; i < courseId.Count; i++)
            {
                TableRow row = new TableRow();
                query = "Select * from Course where CourseId='" + courseId[i] + "' ";
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();
                dr.Read();
                TableCell cell1 = new TableCell();
                cell1.Text = dr["CourseNo"].ToString();
                cell1.CssClass = "backcell";
                row.Cells.Add(cell1);

                TableCell cell2 = new TableCell();
                cell2.Text = dr["CourseName"].ToString();
                cell2.CssClass = "backcell";
                row.Cells.Add(cell2);

                TableCell cell3 = new TableCell();
                cell3.Text = tId[i];
                cell3.CssClass = "backcell";
                row.Cells.Add(cell3);

                if (i % 2 != 0)
                {
                    row.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                }

                stdAttTable.Rows.Add(row);



                con.Close();
            }


            getAttendanceP(tId, courseId);
        }

        private void getAttendanceP(List<string> tId, List<string> courseId)
        {
            string query;
            SqlCommand cmd;
            SqlDataReader dr;
            int i = 0, pCount = 0, totalClasses = 0, chk = 0;
            double percentage = 0;
            foreach (TableRow row in stdAttTable.Rows)
            {
                if (chk == 0)
                {
                    chk = 1;
                }
                else
                {

                    query = "Select Status from Attendance where SId='" + sId + "' and CourseId='" + courseId[i] + "' ";
                    con.Open();
                    cmd = new SqlCommand(query, con);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        totalClasses++;
                        if (dr["Status"].ToString().Equals("P"))
                            pCount++;

                    }
                    percentage = (Convert.ToDouble(pCount) / Convert.ToDouble(totalClasses)) * 100;


                    TableCell cell = new TableCell();
                    cell.Text = Math.Round(percentage, 0).ToString();
                    cell.CssClass = "backcell";
                    row.Cells.Add(cell);
                    con.Close();
                    i++;
                    pCount = 0;
                    totalClasses = 0;
                }
            }

        }


    }
}