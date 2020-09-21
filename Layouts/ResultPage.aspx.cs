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
    public partial class classes : System.Web.UI.Page
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
            TId = Session["AccountId"].ToString();
            getClasses();
        }

        private void getClasses()
        {
            string[,] classId;
            string[] courseId;


            con.Open();
            SqlCommand cmd = new SqlCommand("Select Count(Distinct ClassId ) from TimeTable where TId='" + TId + "' ", con);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            int c = Convert.ToInt32(dr[0].ToString());
            con.Close();

            classId = new string[c, 2];
            courseId = new string[c];

            string query = "Select Distinct(ClassId) from TimeTable where TId='" + TId + "' ";
            con.Open();
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                classId[i, 0] = dr["ClassId"].ToString();
                // courseId[i] = dr["CourseId"].ToString();
                i++;
            }
            con.Close();

            for (int j = 0; j < c; j++)
            {
                query = "Select Distinct(CourseId) from TimeTable where TId='" + TId + "' and ClassId='" + classId[j, 0] + "' ";
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

            for (i = 0; i < c; i++)
            {
                query = "select * from ApprovalTable where ChairPApp='" + 1 + "' and ClassId='" + classId[i, 0] + "' and CourseId='" + courseId[i] + "'";
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    classId[i, 1] = "1";
                }
                else
                    classId[i, 1] = "0";
                con.Close();
            }

            for (int j = 0; j < c; j++)
            {
                //  query = "Select CourseName,CourseNo from Course where CourseId='" + courseId[j]+ "' and Select ClassName,ClassSection from ClassTable where ClassID='"+classId[j]+"' ";
                query = "SELECT Course.CourseName, Course.CourseNo, Course.CreditHours, ClassTable.ClassName, ClassTable.ClassSection, ClassTable.Shift FROM Course CROSS JOIN ClassTable WHERE Course.CourseId='" + courseId[j] + "' and ClassTable.ClassID='" + classId[j, 0] + "'";
                
                
                
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();

                dr.Read();

                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                cell1.CssClass = "tablecellleft";
                cell1.Text = dr["ClassName"].ToString();
                if (j%2!=0)
                    cell1.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                row.Cells.Add(cell1);

                TableCell cell11 = new TableCell();
                cell11.CssClass = "tablecell";
                cell11.Text = dr["Shift"].ToString();
                if (j % 2 != 0)
                    cell11.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                row.Cells.Add(cell11);

                TableCell cell5 = new TableCell();
                cell5.CssClass = "tablecell";
                cell5.Text = dr["ClassSection"].ToString();
                if (j % 2 != 0)
                    cell5.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                row.Cells.Add(cell5);

                TableCell cell2 = new TableCell();
                cell2.CssClass = "tablecell";
                cell2.Text = dr["CourseNo"].ToString();
                if (j % 2 != 0)
                    cell2.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                row.Cells.Add(cell2);

                TableCell cell3 = new TableCell();
                cell3.CssClass = "tablecellright";
                cell3.Text = dr["CourseName"].ToString();
                if (j % 2 != 0)
                    cell3.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                row.Cells.Add(cell3);



                Button result = new Button();
                if (classId[j, 1].Equals("1"))
                {
                    result.Text = "Approved Result";
                }
                else
                    result.Text = "View Result";
                result.Click += new EventHandler(resultClick);

                result.ID = "res_" + classId[j, 0] + "_" + (j + 1);
                result.CssClass = "tablecellbtn";
                //l.Click += new EventHandler(leaveClick);
                TableCell cell6 = new TableCell();
                cell6.CssClass = "backbuttoncell";
                cell6.Style["vertical-align"] = "middle";
                cell6.Controls.Add(result);
                
                row.Cells.Add(cell6);
                classesTable.Rows.Add(row);
                con.Close();
            }

        }
        private void resultClick(object sender, EventArgs e)
        {
            string[] temp = ((Button)sender).ID.Split('_');
            int id = Convert.ToInt32(temp[1]);
            Session["classId"] = id;
            string cNo = classesTable.Rows[Convert.ToInt32(temp[2])].Cells[3].Text;
            String query = "Select CourseId from Course where CourseNo='" + cNo + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["courseId"] = dr[0].ToString();
            con.Close();

            con.Open();
            SqlDataAdapter sqlDa1 = new SqlDataAdapter("SELECT * FROM Result WHERE CourseID='" + Session["courseId"] + "'AND ClassID='" + id + "' AND TId='" + TId + "' ", con);

            DataTable dtbl = new DataTable();
            sqlDa1.Fill(dtbl);

            if (dtbl.Rows.Count > 0)
            {

                con.Close();
                Response.Redirect("FinalViewResult.aspx");
            }
            else
            {

                Response.Redirect("Result.aspx");
            }
            con.Close();
        }

    }
}