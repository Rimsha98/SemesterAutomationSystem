using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UokSemesterSystem
{
    public partial class StdViewResult : System.Web.UI.Page
    {
        static string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(conString);
        string std_id;

        protected override void OnInit(EventArgs e)
        {


            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            std_id = Session["AccountId"].ToString();
            int sem = 0;
            string cId = "";
            string q1 = "select SemesterNo, ClassID from Student where SId='" + std_id + "' ";
            con.Open();
            SqlCommand cmd = new SqlCommand(q1, con);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            sem = Convert.ToInt32(dr["SemesterNo"].ToString());
            cId = dr["ClassID"].ToString();
            con.Close();
            for (int i = 1; i <= sem; i++)
                getResults(i, cId);
        }

        private void getResults(int sem, string cId)
        {
            SqlDataReader dr;
            SqlCommand cmd;
            List<string> courseId = new List<string>();

            string classId, q1, q2, chk = "0", hrs = "";
            classId = cId;


            string q = "select Distinct(CourseId) from TimeTable where ClassId='" + classId + "' and SemesterNo='" + sem + "'";
            con.Open();
            cmd = new SqlCommand(q, con);
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                    courseId.Add(dr[0].ToString());
            }
            con.Close();

            for (int i = 0; i < courseId.Count; i++)
            {
                
                q = "select * from Course where CourseId='" + courseId[i] + "'  ";
                con.Open();
                cmd = new SqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    cell1.CssClass = "backcell";
                    cell1.Text = (i + 1).ToString();
                    row.Cells.Add(cell1);

                    TableCell cell2 = new TableCell();
                    cell2.CssClass = "backcell";
                    cell2.Text = dr["CourseNo"].ToString();
                    row.Cells.Add(cell2);

                    TableCell cell3 = new TableCell();
                    cell3.CssClass = "backcell";
                    cell3.Text = dr["CourseName"].ToString();
                    row.Cells.Add(cell3);

                    TableCell cell7 = new TableCell();
                    cell7.CssClass = "backcell";
                    if (dr["CreditHours"].ToString() == "3+0")
                    {
                        cell7.Text = "3+0";
                        hrs = "0";
                    }
                    else
                    {
                        cell7.Text = "2+1";
                        hrs = "1";
                    }
                    row.Cells.Add(cell7);

                    TableCell cell4 = new TableCell();
                    cell4.CssClass = "backcell";
                    cell4.Text = "N/A";
                    row.Cells.Add(cell4);

                    TableCell cell5 = new TableCell();
                    cell5.CssClass = "backcell";
                    if (dr["CreditHours"].ToString() == "3+0")
                    {
                        cell5.Text = "-";
                    }
                    else
                        cell5.Text = "N/A";
                    row.Cells.Add(cell5);

                    TableCell cell6 = new TableCell();
                    cell6.CssClass = "backcell";
                    cell6.Text = "N/A";
                    if (i % 2 != 0)
                    {
                        row.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                    }
                    row.Cells.Add(cell6);
                    if (sem == 1)
                        sem1.Rows.Add(row);
                    else if (sem == 2)
                        sem2.Rows.Add(row);
                    else if (sem == 3)
                        sem3.Rows.Add(row);
                    else if (sem == 4)
                        sem4.Rows.Add(row);
                    else if (sem == 5)
                        sem5.Rows.Add(row);
                    else if (sem == 6)
                        sem6.Rows.Add(row);
                    else if (sem == 7)
                        sem7.Rows.Add(row);
                    else if (sem == 8)
                        sem8.Rows.Add(row);
                }
                con.Close();

                if (sem == 1)
                {
                    sem1.Visible = true;
                    s1_Lbl.Visible = true;
                }
                else if (sem == 2)
                {

                    sem2.Visible = true;
                    s2_Lbl.Visible = true;
                }
                else if (sem == 3)
                {

                    sem3.Visible = true;
                    s3_Lbl.Visible = true;
                }
                else if (sem == 4)
                {

                    sem4.Visible = true;
                    s4_Lbl.Visible = true;
                }
                else if (sem == 5)
                {

                    sem5.Visible = true;
                    s5_Lbl.Visible = true;
                }
                else if (sem == 6)
                {

                    sem6.Visible = true;
                    s6_Lbl.Visible = true;
                }
                else if (sem == 7)
                {

                    sem7.Visible = true;
                    s7_Lbl.Visible = true;
                }
                else if (sem == 8)
                {

                    sem8.Visible = true;
                    s8_Lbl.Visible = true;
                }
                //load result

                q2 = "select TeacherAppStd from ApprovalTable where ClassId='" + classId + "' and CourseId='" + courseId[i] + "'  ";
                con.Open();
                cmd = new SqlCommand(q2, con);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    chk = dr[0].ToString();
                }
                con.Close();

                if (chk.Equals("1"))
                {
                    string query = "SELECT * FROM Result WHERE SId='" + std_id + "' and CourseID='" + courseId[i] + "'  ";
                    con.Open();
                    cmd = new SqlCommand(query, con);
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        if (sem == 1)
                        {
                            if (hrs.Equals("1"))
                            {

                                sem1.Rows[i + 1].Cells[4].Text = dr["Theory"].ToString();
                                sem1.Rows[i + 1].Cells[5].Text = dr["Lab"].ToString();



                            }
                            else
                                sem1.Rows[i + 1].Cells[4].Text = dr["TotalMarks"].ToString();

                            sem1.Rows[i + 1].Cells[6].Text = dr["TotalMarks"].ToString();



                        }
                        else if (sem == 2)
                        {
                            if (hrs.Equals("1"))
                            {

                                sem2.Rows[i + 1].Cells[4].Text = dr["Theory"].ToString();
                                sem2.Rows[i + 1].Cells[5].Text = dr["Lab"].ToString();



                            }
                            else
                                sem2.Rows[i + 1].Cells[4].Text = dr["TotalMarks"].ToString();

                            sem2.Rows[i + 1].Cells[6].Text = dr["TotalMarks"].ToString();


                        }
                        else if (sem == 3)
                        {
                            if (hrs.Equals("1"))
                            {

                                sem3.Rows[i + 1].Cells[4].Text = dr["Theory"].ToString();
                                sem3.Rows[i + 1].Cells[5].Text = dr["Lab"].ToString();



                            }
                            else
                                sem3.Rows[i + 1].Cells[4].Text = dr["TotalMarks"].ToString();

                            sem3.Rows[i + 1].Cells[6].Text = dr["TotalMarks"].ToString();


                        }
                        else if (sem == 4)
                        {
                            if (hrs.Equals("1"))
                            {

                                sem4.Rows[i + 1].Cells[4].Text = dr["Theory"].ToString();
                                sem4.Rows[i + 1].Cells[5].Text = dr["Lab"].ToString();



                            }
                            else
                                sem4.Rows[i + 1].Cells[4].Text = dr["TotalMarks"].ToString();

                            sem4.Rows[i + 1].Cells[6].Text = dr["TotalMarks"].ToString();


                        }
                        else if (sem == 5)
                        {
                            if (hrs.Equals("1"))
                            {

                                sem5.Rows[i + 1].Cells[4].Text = dr["Theory"].ToString();
                                sem5.Rows[i + 1].Cells[5].Text = dr["Lab"].ToString();



                            }
                            else
                                sem5.Rows[i + 1].Cells[4].Text = dr["TotalMarks"].ToString();

                            sem5.Rows[i + 1].Cells[6].Text = dr["TotalMarks"].ToString();


                        }
                        else if (sem == 6)
                        {
                            if (hrs.Equals("1"))
                            {

                                sem6.Rows[i + 1].Cells[4].Text = dr["Theory"].ToString();
                                sem6.Rows[i + 1].Cells[5].Text = dr["Lab"].ToString();



                            }
                            else
                                sem6.Rows[i + 1].Cells[4].Text = dr["TotalMarks"].ToString();

                            sem6.Rows[i + 1].Cells[6].Text = dr["TotalMarks"].ToString();


                        }
                        else if (sem == 7)
                        {
                            if (hrs.Equals("1"))
                            {

                                sem7.Rows[i + 1].Cells[4].Text = dr["Theory"].ToString();
                                sem7.Rows[i + 1].Cells[5].Text = dr["Lab"].ToString();



                            }
                            else
                                sem7.Rows[i + 1].Cells[4].Text = dr["TotalMarks"].ToString();

                            sem7.Rows[i + 1].Cells[6].Text = dr["TotalMarks"].ToString();


                        }
                        else if (sem == 8)
                        {
                            if (hrs.Equals("1"))
                            {

                                sem8.Rows[i + 1].Cells[4].Text = dr["Theory"].ToString();
                                sem8.Rows[i + 1].Cells[5].Text = dr["Lab"].ToString();



                            }
                            else
                                sem8.Rows[i + 1].Cells[4].Text = dr["TotalMarks"].ToString();

                            sem8.Rows[i + 1].Cells[6].Text = dr["TotalMarks"].ToString();


                        }
                    }

                }


                con.Close();

            }


            con.Close();

        }


    }





}