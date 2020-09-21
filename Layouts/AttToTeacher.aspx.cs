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
    public partial class viewAttToTeacher : System.Web.UI.Page
    {
        static string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(conString);

        string TId, courseId, classId;
        List<string> sList = new List<string>();
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
            if (Session["classId"] == null)
                Response.Redirect("AttendancePage.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            classId = Session["classId"].ToString();
            courseId = Session["courseId"].ToString();
            TId = Session["AccountId"].ToString();
            getSheet();
        }

        private void getSheet()
        {
            string query = "Select * from Course where CourseId='" + courseId + "' ";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            cNo.Text = dr["CourseNo"].ToString();
            cName.Text = dr["CourseName"].ToString();
            con.Close();

            query = "Select * from ClassTable where ClassID='" + classId + "' ";
            con.Open();
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            className.Text = dr["ClassName"].ToString();
            con.Close();

            query = "Select * from Student where ClassID='" + classId + "' ";
            con.Open();
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            int i = 1;
            while (dr.Read())
            {
                sList.Add(dr["SId"].ToString());
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                cell1.CssClass = "backcell";
                cell1.Text = i.ToString();
                row.Cells.Add(cell1);
                TableCell cell2 = new TableCell();
                cell2.Text = dr["RollNumber"].ToString(); ;
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
                
                if (i % 2 == 0)
                {
                    row.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                }

                AttTView.Rows.Add(row);
                i++;
            }
            con.Close();


            getAtt();
            calPercentage();
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("AttendancePage.aspx");
        }

        private void getAtt()
        {
            List<string> date = new List<string>();
            string query = "Select Distinct(Date) from Attendance where ClassID='" + classId + "' and CourseId='" + courseId + "' and TId='" + TId + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                TableCell cell = new TableCell();
                cell.Text = dr[0].ToString();
                cell.CssClass = "cellattendance";
                date.Add(dr[0].ToString());
                AttTView.Rows[0].Cells.Add(cell);
            }
            con.Close();
            for (int i = 0; i < date.Count; i++)
            {
                int j = 1;
                for (int k = 0; k < sList.Count; k++)
                {
                    query = "Select * from Attendance where ClassID='" + classId + "' and SId='" + sList[k] + "' and CourseId='" + courseId + "' and TId='" + TId + "' and Date='" + date[i] + "'";
                    con.Open();
                    cmd = new SqlCommand(query, con);
                    dr = cmd.ExecuteReader();
                    TableCell cell = new TableCell();
                    cell.CssClass = "backcell";
                    if (dr.HasRows)
                    {
                        dr.Read();

                        cell.Text = dr["Status"].ToString();
                        if (cell.Text == "")
                            cell.Text = "N/A";

                    }
                    else
                    {
                        cell.Text = "N/A";
                    }

                    
                    AttTView.Rows[j].Cells.Add(cell);
                    j++;
                    con.Close();
                }

            }

        }

        private void calPercentage()
        {
            double pCount = 0;
            double totalClasses = AttTView.Rows[0].Cells.Count - 4;
            TableCell cell1 = new TableCell();
            cell1.CssClass = "cellp";
            cell1.Text = "Percent(%)";
            AttTView.Rows[0].Cells.Add(cell1);

            for (int j = 1; j < AttTView.Rows.Count; j++)
            {
                for (int i = 4; i < AttTView.Rows[j].Cells.Count; i++)
                {
                    if (AttTView.Rows[j].Cells[i].Text.Equals("P"))
                        pCount++;
                }

                double percentage = (pCount / totalClasses) * 100;
                TableCell cell = new TableCell();
                cell.CssClass = "backcell";
                cell.Text = Math.Round(percentage, 0).ToString();

                AttTView.Rows[j].Cells.Add(cell);
                pCount = 0;

            }
        }
    }
}