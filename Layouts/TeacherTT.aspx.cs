using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UokSemesterSystem
{
    public partial class TeacherTT : System.Web.UI.Page
    {

        string Teacher_ID;
        private List<string[]> Courses = new List<string[]>();

        string strcon = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Teacher_ID = Session["AccountId"].ToString();

            GetTimeTable();
            DisplayDetail();
        }

        private void GetTimeTable()
        {
            string[] temp;
            string[] course;
            SqlConnection con = new SqlConnection(strcon);
            string query = "select * from TimeTable where TId='" + Teacher_ID + "' OR AId='" + Teacher_ID + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            int i = 0;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    temp = new string[2];
                    course = new string[10];
                    course[0] = "" + (i + 1);
                    course[1] = dr["Day"].ToString();
                    course[6] = dr["STime"].ToString() + " - " + dr["ETime"].ToString();
                    course[7] = dr["ClassRoomNo"].ToString();
                    course[3] = dr["Section"].ToString();

                    temp = GetClassInfo(dr["ClassId"].ToString());
                    course[2] = temp[0];
                    course[8] = temp[1];

                    temp = GetCourseInfo(dr["CourseId"].ToString());
                    course[4] = temp[0];
                    course[5] = temp[1];

                    if (course[1] == "Monday")
                        course[9] = "1";
                    if (course[1] == "Tuesday")
                        course[9] = "2";
                    if (course[1] == "Wednesday")
                        course[9] = "3";
                    if (course[1] == "Thursday")
                        course[9] = "4";
                    if (course[1] == "Friday")
                        course[9] = "5";

                    Courses.Add(course);
                    i++;
                }
            }
            con.Close();
        }

        private string[] GetClassInfo(string Class_ID)
        {
            string[] temp = new string[2];
            SqlConnection con = new SqlConnection(strcon);
            string query = "select * from ClassTable where ClassID='" + Class_ID + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                temp[0] = dr["ClassName"].ToString();
                temp[1] = dr["Shift"].ToString();
            }
            con.Close();
            return temp;
        }

        private string[] GetCourseInfo(string Course_ID)
        {
            string[] temp = new string[2];
            SqlConnection con = new SqlConnection(strcon);
            string query = "select * from Course where CourseId='" + Course_ID + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                temp[0] = dr["CourseNo"].ToString();
                temp[1] = dr["CourseName"].ToString();
            }
            con.Close();
            return temp;
        }

        private void SortList()
        {
            string[] temp = new string[10];
            string value1, value2;
            for (int a = 0; a < Courses.Count; a++)
            {
                temp = Courses[a];
                value1 = temp[9];
                for (int b = a + 1; b < Courses.Count; b++)
                {
                    temp = Courses[b];
                    value2 = temp[9];
                    if (string.Compare(value1, value2) == 1)
                    {
                        temp = Courses[a];
                        Courses[a] = Courses[b];
                        Courses[b] = temp;
                    }
                }
            }
        }

        private void DisplayDetail()
        {
            SortList();
            TableRow tr; TableCell tc;
            string[] temp = new string[10];
            int j;
            int countmorn = 1, counteven = 1;

            if (Courses.Count == 0)
            {
                nocourses.Visible = true;
                TeacherCoursesMorning.Visible = false;
                TeacherCoursesEvening.Visible = false;
            }
            else
            {

                nocourses.Visible = false;
                for (int i = 0; i < Courses.Count; i++)
                {

                    tr = new TableRow();

                    temp = Courses[i];
                    if (temp[8] == "Morning" || temp[8] == "morning")
                    {
                        for (j = 1; j < 8; j++)
                        {
                            tc = new TableCell();
                            tc.Text = temp[j];
                            tc.CssClass = "tcell";
                            if (j == 5) tc.Style["width"] = "20vw";
                            tr.Cells.Add(tc);
                        }
                        if (countmorn % 2 == 0)
                            tr.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                        tr.CssClass = "tablerow";
                        TeacherCoursesMorning.Rows.Add(tr);
                        countmorn++;
                    }
                    else
                    {
                        for (j = 1; j < 8; j++)
                        {
                            tc = new TableCell();
                            tc.Text = temp[j];
                            tc.CssClass = "tcell";
                            if (j == 5) tc.Style["width"] = "20vw";
                            tr.Cells.Add(tc);
                        }
                        if (counteven % 2 == 0)
                            tr.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                        tr.CssClass = "tablerow";
                        TeacherCoursesEvening.Rows.Add(tr);
                        counteven++;
                    }
                }
            }

            if (TeacherCoursesMorning.Rows.Count == 2)
            {
                TeacherCoursesMorning.Visible = false;
            }
            else if (TeacherCoursesEvening.Rows.Count == 2)
            {
                TeacherCoursesEvening.Visible = false;
            }
        }
    }
}