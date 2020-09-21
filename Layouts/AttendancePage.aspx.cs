using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UokSemesterSystem
{
    public partial class AttendancePage : System.Web.UI.Page
    {
        static string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(conString);
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }

        string TId;
        protected void Page_Load(object sender, EventArgs e)
        {
            TId = Session["AccountId"].ToString();
            getClasses();
        }

        private void getClasses()
        {
            string[] classId;
            string[] courseId;


            con.Open();
            SqlCommand cmd = new SqlCommand("Select Count(Distinct ClassId ) from TimeTable where TId='" + TId + "'  ", con);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            int c = Convert.ToInt32(dr[0].ToString());
            con.Close();

            classId = new string[c];
            courseId = new string[c];

            string query = "Select Distinct(ClassId) from TimeTable where TId='" + TId + "' ";
            con.Open();
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                classId[i] = dr["ClassId"].ToString();
                //   courseId[i] = dr["CourseId"].ToString();
                i++;
            }
            con.Close();

            for (int j = 0; j < c; j++)
            {
                query = "Select Distinct(CourseId) from TimeTable where TId='" + TId + "' and ClassId='" + classId[j] + "' ";
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    //classId[i] = dr["ClassId"].ToString();
                    courseId[j] = dr["CourseId"].ToString();

                }
                con.Close();
            }

            for (int j = 0; j < c; j++)
            {
                //  query = "Select CourseName,CourseNo from Course where CourseId='" + courseId[j]+ "' and Select ClassName,ClassSection from ClassTable where ClassID='"+classId[j]+"' ";
                query = "SELECT Course.CourseName, Course.CourseNo, Course.CreditHours, ClassTable.ClassName, ClassTable.ClassSection, ClassTable.Shift FROM Course CROSS JOIN ClassTable WHERE Course.CourseId='" + courseId[j] + "' and ClassTable.ClassID='" + classId[j] + "'";
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();

                dr.Read();

                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                cell1.Text = dr["ClassName"].ToString();
                cell1.CssClass = "backcell1";
                if (j % 2 != 0)
                    cell1.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                row.Cells.Add(cell1);

                TableCell cell11 = new TableCell();
                cell11.Text = dr["Shift"].ToString();
                cell11.CssClass = "backcell";
                if (j % 2 != 0)
                    cell11.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                row.Cells.Add(cell11);

                TableCell cell5 = new TableCell();
                cell5.Text = dr["ClassSection"].ToString();
                cell5.CssClass = "backcell";
                if (j % 2 != 0)
                    cell5.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                row.Cells.Add(cell5);

                TableCell cell2 = new TableCell();
                cell2.Text = dr["CourseNo"].ToString();
                if (j % 2 != 0)
                    cell2.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                cell2.CssClass = "backcell";
                row.Cells.Add(cell2);

                TableCell cell3 = new TableCell();
                cell3.Text = dr["CourseName"].ToString();
                if (j % 2 != 0)
                    cell3.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                cell3.CssClass = "cellright";
                row.Cells.Add(cell3);
                Button viewAtt = new Button();
                viewAtt.Text = "View Attendance";
                viewAtt.ID = "viewAtt_" + classId[j] + "_" + (j + 1);
                viewAtt.Click += new EventHandler(viewAttClick);
                viewAtt.CssClass = "viewbutton";
                TableCell cell6 = new TableCell();
                cell6.CssClass = "cell5css";
                cell6.Controls.Add(viewAtt);
                row.Cells.Add(cell6);

                
                classesTable.Rows.Add(row);
                con.Close();
            }

            checkMarked(classId);
        }

        private void checkMarked(string[] classId)
        {
            int j = 0;
            string query, cName, courseId = null;
            SqlCommand cmd;
            SqlDataReader dr;
            foreach (TableRow row in classesTable.Rows)
            {
                if (j == 0)
                    j++;
                else
                {
                    cName = classesTable.Rows[j].Cells[4].Text;
                    query = "Select CourseId from Course where CourseName='" + cName + "'";
                    con.Open();
                    cmd = new SqlCommand(query, con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    courseId = dr[0].ToString();
                    con.Close();
                    query = "SELECT * FROM Attendance WHERE CourseId='" + courseId + "' and TId='" + TId + "' and ClassId='" + classId[j - 1] + "' and Date='" + DateTime.Now.ToString("dd/MM/yyyy") + "'";
                    con.Open();
                    cmd = new SqlCommand(query, con);
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        TableCell cell4 = new TableCell();
                        cell4.Text = "Marked.";
                        cell4.ForeColor = Color.Red;
                        row.Cells.Add(cell4);
                    }
                    else
                    {

                        Button att = new Button();
                        att.Text = "Mark Attendance";
                        att.ID = "att_" + classId[j - 1] + "_" + (j);
                        att.Click += new EventHandler(attClick);
                        att.CssClass = "markatt";
                        TableCell cell4 = new TableCell();
                        cell4.CssClass = "cell6css";
                        cell4.Controls.Add(att);
                        row.Cells.Add(cell4);
                    }
                    con.Close();
                    j++;
                }

            }

        }

        private void viewAttClick(object sender, EventArgs e)
        {
            string[] temp = ((Button)sender).ID.Split('_');
            int id = Convert.ToInt32(temp[1]);
            Session["classId"] = id;
            string cName = classesTable.Rows[Convert.ToInt32(temp[2])].Cells[4].Text;
            String query = "Select CourseId from Course where CourseName='" + cName + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["courseId"] = dr[0].ToString();
            con.Close();
            Response.Redirect("AttToTeacher.aspx");

        }

        private void attClick(object sender, EventArgs e)
        {
            string[] temp = ((Button)sender).ID.Split('_');

            int id = Convert.ToInt32(temp[1]);
            Session["classId"] = id;

            string cName = classesTable.Rows[Convert.ToInt32(temp[2])].Cells[4].Text;
            String query = "Select CourseId from Course where CourseName='" + cName + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["courseId"] = dr[0].ToString();
            con.Close();

            Response.Redirect("Attendance.aspx");
        }
    }
}