using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UokSemesterSystem
{
    public partial class CourseList : System.Web.UI.Page
    {
        static string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(conString);
        string classId;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["classId"] == null)
                Response.Redirect("Login.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            classId = Session["classId"].ToString();
            getCourses();
        }


        void getCourses()
        {

            string query = "SELECT Distinct(CourseId) FROM TimeTable WHERE ClassId='" + classId + "'   ";
            con.Open();
            SqlDataAdapter sqlDa1 = new SqlDataAdapter(query, con);
            DataTable dtbl = new DataTable();
            sqlDa1.Fill(dtbl);
            List<String> courseIdList = new List<String>();

            for (int j = 0; j < dtbl.Rows.Count; j++)
            {
                courseIdList.Add(dtbl.Rows[j]["CourseId"].ToString());

            }

            con.Close();

            List<string> sem = new List<string>();
            SqlDataReader dr;
            SqlCommand cmd;

            for (int i = 0; i < courseIdList.Count; i++)
            {
                query = "SELECT SemesterNo FROM TimeTable WHERE ClassId='" + classId + "' and  CourseId='" + courseIdList[i] + "'  ";
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();
                dr.Read();
                sem.Add(dr["SemesterNo"].ToString());


                con.Close();
            }


            int count = 1;
            int count1 = 1;

            for (int i = 0; i < courseIdList.Count; i++)
            {
                string chk = "0", cpApp = "0";
                string query2 = "SELECT TeacherApp,ChairPApp FROM ApprovalTable WHERE CourseId='" + courseIdList[i] + "' and ClassId='" + classId + "'";
                con.Open();
                cmd = new SqlCommand(query2, con);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    chk = dr["TeacherApp"].ToString();
                    cpApp = dr["ChairPApp"].ToString();
                }
                con.Close();


                if (chk.Equals("1"))
                {

                    query2 = "SELECT * FROM Course WHERE CourseId='" + courseIdList[i] + "'";
                    con.Open();
                    cmd = new SqlCommand(query2, con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    cell1.CssClass = "backcell";
                    cell1.Text = dr["CourseName"].ToString();
                    //      row.Cells.Add(cell1);

                    TableCell cell2 = new TableCell();
                    cell2.Text = dr["CourseNo"].ToString();
                    cell2.CssClass = "backcell";
                    // row.Cells.Add(cell2);
                    TableCell cell3 = new TableCell();
                    cell3.Text = sem[i];
                    cell3.CssClass = "backcell";

                    TableCell cell4 = new TableCell();
                    Button button = new Button();

                    button.Click += new EventHandler(viewResult);

                    button.ID = "btnCC_" + dr["CourseId"] + "_" + i;
                    button.CssClass = "backcellbutton";
                    if (cpApp.Equals("1"))
                    {
                        button.Text = "View Result";

                    }
                    else
                        button.Text = "Approve Result";

                    cell4.Controls.Add(button);

                    //  row.Cells.Add(cell4);
                    if (cpApp.Equals("1"))
                    {
                        if (count % 2 == 0)
                        {
                            cell1.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                            cell2.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                            cell3.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                        }
                        row.Cells.Add(cell1);
                        row.Cells.Add(cell2);
                        row.Cells.Add(cell3);
                        row.Cells.Add(cell4);
                        coursesTable1.Rows.Add(row);
                        count++;
                    }
                    else
                    {
                        if (count1 % 2 == 0)
                        {
                            cell1.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                            cell2.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                            cell3.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                        }
                        row.Cells.Add(cell1);
                        row.Cells.Add(cell2);
                        row.Cells.Add(cell3);
                        row.Cells.Add(cell4);

                        coursesTable.Rows.Add(row);
                        count1++;
                    }
                    con.Close();
                }
            }

            if (coursesTable.Rows.Count == 1 && coursesTable1.Rows.Count == 1)
                msgLbl.Visible = true;

            if (coursesTable.Rows.Count == 1)
            {
                unAppDiv.Visible = false;

            }
            if (coursesTable1.Rows.Count == 1)
            {
                appDiv.Visible = false;
            }


        }

        protected void go_back(object sender, EventArgs e)
        {
            Response.Redirect("ClassPageCP.aspx");
        }


        private void viewResult(object sender, EventArgs e)
        {
            string[] temp = ((Button)sender).ID.Split('_');
            int id = Convert.ToInt32(temp[1]);
            Session["courseId"] = id;
            Response.Redirect("ResultViewCP.aspx");
        }
    }
}