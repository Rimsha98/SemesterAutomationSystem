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
    public partial class FinalViewResult : System.Web.UI.Page
    {
        bool chairmanApproval = false;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }

        static string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

        List<String> stdIdArr = new List<String>();
        string classId;
        string courseId;
        string classSection;
        string semesterNo;
        string examDate;
        string shift;
        string Tid;
        string creditHours;
        protected void Page_Load(object sender, EventArgs e)
        {
            courseId = Session["courseId"].ToString();
            //  lbl_TeacherName.Text = Session["TeacherName"].ToString();
            Tid = Session["AccountId"].ToString();

            string q = "SELECT * FROM Teacher where TId='"+Tid+"' ";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand com = new SqlCommand(q, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            lbl_TeacherName.Text = dr["TName"].ToString();
            con.Close();

            getDataWithTable();
            chkToChairPerson();
        }

        private void getDataWithTable()
        {
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter sqlDa = new SqlDataAdapter();
                cmd.CommandText = "SELECT *  FROM Course WHERE CourseId='" + courseId + "' ";
                cmd.Connection = sqlCon;
                sqlDa.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sqlDa.Fill(ds);
                lbl_CourseNum.Text = ds.Tables[0].Rows[0]["CourseNo"].ToString();
                lbl_courseName.Text = ds.Tables[0].Rows[0]["CourseName"].ToString();
                lbl_dYear.Text = DateTime.Now.Year.ToString();
                creditHours = ds.Tables[0].Rows[0]["CreditHours"].ToString();
                if (creditHours == "3+0")
                {
                    lbl_creditHours.Text = "3 + 0";

                }
                else
                    lbl_creditHours.Text = "2 + 1";

                //cmd.CommandText = "SELECT ClassId FROM TimeTable WHERE CourseId ='" + courseId + "'";
                //cmd.Connection = sqlCon;
                //sqlDa.SelectCommand = cmd;

                //DataSet ds1 = new DataSet();
                //sqlDa.Fill(ds1);
                classId = Session["classId"].ToString();



                cmd.CommandText = "SELECT ClassSection,ClassName,Shift FROM ClassTable WHERE ClassId ='" + classId + "'";
                cmd.Connection = sqlCon;
                sqlDa.SelectCommand = cmd;

                DataSet ds2 = new DataSet();
                sqlDa.Fill(ds2);
                classSection = ds2.Tables[0].Rows[0]["ClassSection"].ToString();
                shift = ds2.Tables[0].Rows[0]["Shift"].ToString();

                cmd.CommandText = "SELECT Year FROM Student WHERE ClassId ='" + classId + "'";
                cmd.Connection = sqlCon;
                sqlDa.SelectCommand = cmd;

                DataSet ds3 = new DataSet();
                sqlDa.Fill(ds3);
                lbl_className.Text = ds2.Tables[0].Rows[0]["ClassName"].ToString() + "-" + ds3.Tables[0].Rows[0]["Year"].ToString();




                cmd.CommandText = "SELECT Department,Major FROM Student WHERE ClassId ='" + classId + "'";
                cmd.Connection = sqlCon;
                sqlDa.SelectCommand = cmd;

                DataSet ds4 = new DataSet();
                sqlDa.Fill(ds4);
                lbl_deptName.Text = ds4.Tables[0].Rows[0]["Department"].ToString();
                lbl_majDeptName.Text = ds4.Tables[0].Rows[0]["Major"].ToString();


                cmd.CommandText = "SELECT SemesterNo FROM Student WHERE ClassId ='" + classId + "'";
                cmd.Connection = sqlCon;
                sqlDa.SelectCommand = cmd;

                DataSet ds5 = new DataSet();
                sqlDa.Fill(ds5);
                semesterNo = ds5.Tables[0].Rows[0]["SemesterNo"].ToString();
                lbl_semNo.Text = ds5.Tables[0].Rows[0]["SemesterNo"].ToString();


                SqlDataAdapter sqlDa1 = new SqlDataAdapter("SELECT SId,SName,RollNumber FROM Student WHERE ClassID='" + classId + "' ", sqlCon);



                DataTable dtbl = new DataTable();
                sqlDa1.Fill(dtbl);

                examDate = eDate.Text;
                SqlDataAdapter sqlDa2 = new SqlDataAdapter("SELECT * FROM Result WHERE ClassID='" + classId + "' AND CourseID='" + courseId + "' AND TId='" + Tid + "'", sqlCon);
                DataTable dtblResult = new DataTable();
                sqlDa2.Fill(dtblResult);

                // saving all the Stds Ids
                for (int a = 0; a < dtbl.Rows.Count; a++)
                {
                    stdIdArr.Add(dtbl.Rows[a]["SId"].ToString());
                }

                if (creditHours == "3+0")
                {

                    TableHeaderRow header = new TableHeaderRow(); // Creating a header row
                    dt.Rows.Add(header); // Add the header row to table tbl 

                    TableHeaderCell headerTableCell1 = new TableHeaderCell();
                    headerTableCell1.Text = "S.No.";
                    headerTableCell1.CssClass = "sno";
                    header.Cells.Add(headerTableCell1);

                    TableHeaderCell headerTableCell2 = new TableHeaderCell();
                    headerTableCell2.Text = "Seat No";
                    headerTableCell2.CssClass = "seatno";
                    header.Cells.Add(headerTableCell2);

                    TableHeaderCell headerTableCell3 = new TableHeaderCell();
                    headerTableCell3.Text = "Name";
                    headerTableCell3.CssClass = "name";
                    header.Cells.Add(headerTableCell3);

                    TableHeaderCell headerTableCell4 = new TableHeaderCell();
                    headerTableCell4.Text = "Total Marks";
                    headerTableCell4.CssClass = "totalmarks";
                    header.Cells.Add(headerTableCell4);

                    TableHeaderCell headerTableCell5 = new TableHeaderCell();
                    headerTableCell5.Text = "Total Marks In Words";
                    headerTableCell5.CssClass = "totalinwords";
                    header.Cells.Add(headerTableCell5);

                    TableHeaderCell headerTableCell6 = new TableHeaderCell();
                    headerTableCell6.Text = "GPA";
                    headerTableCell6.CssClass = "gpa";
                    header.Cells.Add(headerTableCell6);


                    TableHeaderCell headerTableCell7 = new TableHeaderCell();
                    headerTableCell7.Text = "Remarks";
                    headerTableCell7.CssClass = "remarks";
                    header.Cells.Add(headerTableCell7);


                    for (int a = 0; a < dtbl.Rows.Count; a++)
                    {


                        TableRow row = new TableRow();

                        TableCell cell0 = new TableCell();
                        var lb0 = new Label();
                        lb0.Text = (a + 1).ToString();
                        cell0.CssClass = "backcell";
                        cell0.Controls.Add(lb0);
                        //add cell to row
                        row.Cells.Add(cell0);


                        TableCell cell1 = new TableCell();

                        var lbl = new Label();
                        try
                        {
                            lbl.Text = dtbl.Rows[a]["RollNumber"].ToString();
                        }
                        catch (Exception ex)
                        {
                            lbl.Text = "N/A";
                        }
                        cell1.CssClass = "backcell";
                        cell1.Controls.Add(lbl);
                        //add cell to row
                        row.Cells.Add(cell1);

                        TableCell cell2 = new TableCell();
                        var lbl2 = new Label();
                        try
                        {
                            lbl2.Text = dtbl.Rows[a]["SName"].ToString();
                        }
                        catch (Exception ex)
                        {
                            lbl2.Text = "N/A";
                        }
                        cell2.CssClass = "backcell";
                        cell2.Controls.Add(lbl2);
                        //add cell to row
                        row.Cells.Add(cell2);

                        //add row to dt
                        TableCell cell3 = new TableCell();
                        //TextBox tb = new TextBox();
                        //tb.ID = "TextBox" + a;
                        Label label = new Label();
                        try
                        {
                            label.Text = dtblResult.Rows[a]["TotalMarks"].ToString();
                        }
                        catch (Exception ex)
                        {
                            label.Text = "N/A";
                        }
                        cell3.CssClass = "backcell";
                        cell3.Controls.Add(label);
                        //add cell to row
                        row.Cells.Add(cell3);

                        TableCell cell4 = new TableCell();
                        var lbl3 = new Label();
                        lbl3.ID = "lbl_inWords" + a;
                        try
                        {
                            lbl3.Text = dtblResult.Rows[a]["TotInWords"].ToString();
                        }
                        catch (Exception ex)
                        {
                            lbl3.Text = "N/A";
                        }
                        cell4.CssClass = "backcell";
                        cell4.Controls.Add(lbl3);
                        //add cell to row
                        row.Cells.Add(cell4);

                        TableCell cell5 = new TableCell();
                        var lbl4 = new Label();
                        lbl4.ID = "lbl_gpa" + a;
                        try
                        {
                            lbl4.Text = dtblResult.Rows[a]["GPA"].ToString();
                        }
                        catch (Exception ex)
                        {
                            lbl4.Text = "N/A";
                        }
                        cell5.CssClass = "backcell";
                        cell5.Controls.Add(lbl4);
                        //add cell to row
                        row.Cells.Add(cell5);

                        TableCell cell6 = new TableCell();
                        var lbl5 = new Label();
                        lbl5.ID = "lbl_remarks" + a;
                        try
                        {
                            lbl5.Text = dtblResult.Rows[a]["Remarks"].ToString();
                        }
                        catch (Exception ex)
                        {
                            lbl5.Text = "N/A";
                        }
                        cell6.CssClass = "backcell";
                        cell6.Controls.Add(lbl5);
                        //add cell to row
                        if (a % 2 != 0)
                        {
                            row.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                        }
                        row.Cells.Add(cell6);


                        dt.Rows.Add(row);
                    }
                }

                else
                {
                    TableHeaderRow header = new TableHeaderRow(); // Creating a header row
                    dt.Rows.Add(header); // Add the header row to table tbl 
                    TableHeaderCell headerTableCell1 = new TableHeaderCell();
                    headerTableCell1.Text = "S.No.";
                    headerTableCell1.CssClass = "sno";
                    header.Cells.Add(headerTableCell1);

                    TableHeaderCell headerTableCell2 = new TableHeaderCell();
                    headerTableCell2.Text = "Seat No";
                    headerTableCell2.CssClass = "seatno";
                    header.Cells.Add(headerTableCell2);

                    TableHeaderCell headerTableCell3 = new TableHeaderCell();
                    headerTableCell3.Text = "Name";
                    headerTableCell3.CssClass = "name";
                    header.Cells.Add(headerTableCell3);

                    TableHeaderCell headerTableCell4 = new TableHeaderCell();
                    headerTableCell4.Text = "Theory";
                    headerTableCell4.CssClass = "theory";
                    header.Cells.Add(headerTableCell4);


                    TableHeaderCell headerTableCell5 = new TableHeaderCell();
                    headerTableCell5.Text = "Lab";
                    headerTableCell5.CssClass = "lab";
                    header.Cells.Add(headerTableCell5);

                    TableHeaderCell headerTableCell6 = new TableHeaderCell();
                    headerTableCell6.Text = "Total Marks";
                    headerTableCell6.CssClass = "totalmarks";
                    header.Cells.Add(headerTableCell6);

                    TableHeaderCell headerTableCell7 = new TableHeaderCell();
                    headerTableCell7.Text = "Total Marks In Words";
                    headerTableCell7.CssClass = "totalinwords";
                    header.Cells.Add(headerTableCell7);

                    TableHeaderCell headerTableCell8 = new TableHeaderCell();
                    headerTableCell8.Text = "GPA";
                    headerTableCell8.CssClass = "gpa";
                    header.Cells.Add(headerTableCell8);


                    TableHeaderCell headerTableCell9 = new TableHeaderCell();
                    headerTableCell9.Text = "Remarks";
                    headerTableCell9.CssClass = "remarks";
                    header.Cells.Add(headerTableCell9);

                    for (int a = 0; a < dtbl.Rows.Count; a++)
                    {


                        TableRow row = new TableRow();

                        TableCell cell0 = new TableCell();
                        var lb0 = new Label();
                        lb0.Text = (a + 1).ToString();
                        cell0.CssClass = "backcell";
                        cell0.Controls.Add(lb0);
                        //add cell to row
                        row.Cells.Add(cell0);


                        TableCell cell1 = new TableCell();
                        var lbl1 = new Label();
                        try
                        {
                            lbl1.Text = dtbl.Rows[a]["RollNumber"].ToString();
                        }
                        catch (Exception ex)
                        {
                            lbl1.Text = "N/A";
                        }
                        cell1.CssClass = "backcell";
                        cell1.Controls.Add(lbl1);
                        //add cell to row
                        row.Cells.Add(cell1);

                        TableCell cell2 = new TableCell();
                        var lbl2 = new Label();
                        try
                        {
                            lbl2.Text = dtbl.Rows[a]["SName"].ToString();
                        }
                        catch (Exception ex)
                        {
                            lbl2.Text = "N/A";
                        }
                        cell2.CssClass = "backcell";
                        cell2.Controls.Add(lbl2);
                        //add cell to row
                        row.Cells.Add(cell2);

                        //add row to dt
                        TableCell cell3 = new TableCell();
                        //TextBox tbT = new TextBox();
                        //tbT.ID = "TextBoxT" + a;
                        Label lblT = new Label();
                        try
                        {
                            lblT.Text = dtblResult.Rows[a]["Theory"].ToString();
                        }
                        catch (Exception ex)
                        {
                            lblT.Text = "N/A";
                        }
                        cell3.CssClass = "backcell";
                        cell3.Controls.Add(lblT);
                        //add cell to row
                        row.Cells.Add(cell3);

                        TableCell cell4 = new TableCell();
                        //TextBox tbL = new TextBox();
                        //tbL.ID = "TextBoxL" + a;
                        Label lblL = new Label();
                        try
                        {
                            lblL.Text = dtblResult.Rows[a]["Lab"].ToString();
                        }
                        catch (Exception ex)
                        {
                            lblL.Text = "N/A";
                        }
                        cell4.CssClass = "backcell";
                        cell4.Controls.Add(lblL);
                        //add cell to row
                        row.Cells.Add(cell4);

                        TableCell cell5 = new TableCell();
                        var lbl3 = new Label();
                        lbl3.ID = "lbl_TotalMarks" + a;
                        try
                        {
                            lbl3.Text = dtblResult.Rows[a]["TotalMarks"].ToString();
                        }
                        catch (Exception ex)
                        {
                            lbl3.Text = "N/A";
                        }
                        cell5.CssClass = "backcell";
                        cell5.Controls.Add(lbl3);
                        //add cell to row
                        row.Cells.Add(cell5);


                        TableCell cell6 = new TableCell();
                        var lbl4 = new Label();
                        lbl4.ID = "lbl_inWords" + a;
                        try
                        {
                            lbl4.Text = dtblResult.Rows[a]["TotInWords"].ToString();
                        }
                        catch (Exception ex)
                        {
                            lbl4.Text = "N/A";
                        }
                        cell6.CssClass = "backcell";
                        cell6.Controls.Add(lbl4);
                        //add cell to row
                        row.Cells.Add(cell6);

                        TableCell cell7 = new TableCell();
                        var lbl5 = new Label();
                        lbl5.ID = "lbl_gpa" + a;
                        try
                        {
                            lbl5.Text = dtblResult.Rows[a]["GPA"].ToString();
                        }
                        catch (Exception ex)
                        {
                            lbl5.Text = "N/A";
                        }

                        cell7.CssClass = "backcell";
                        cell7.Controls.Add(lbl5);
                        //add cell to row
                        row.Cells.Add(cell7);

                        TableCell cell8 = new TableCell();
                        var lbl6 = new Label();
                        lbl6.ID = "lbl_remarks" + a;
                        try
                        {
                            lbl6.Text = dtblResult.Rows[a]["Remarks"].ToString();
                        }
                        catch (Exception ex)
                        {
                            lbl6.Text = "N/A";
                        }
                        cell8.CssClass = "backcell";
                        cell8.Controls.Add(lbl6);
                        //add cell to row
                        row.Cells.Add(cell8);

                        if(a%2!=0)
                        {
                            row.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                        }

                        dt.Rows.Add(row);
                    }

                }
                eDate.Text = dtblResult.Rows[0]["ExamDate"].ToString();
                sqlCon.Close();

            }
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Result.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            btn_edit.Visible = false;
            string q1 = null, chk = null;
            SqlConnection con = new SqlConnection(conString);
            string query = "select * from ApprovalTable where ClassId= '" + classId + "' and CourseId='" + courseId + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                q1 = "UPDATE ApprovalTable SET TeacherApp='" + 1 + "' WHERE ClassId= '" + classId + "' and CourseId='" + courseId + "'";

                chk = "1";
            }


            con.Close();

            if (chk == null)
            {
                q1 = "insert into ApprovalTable(TeacherApp,ClassId,CourseId) values ('" + 1 + "','" + classId + "','" + courseId + "') ";
            }
            con.Open();
            cmd = new SqlCommand(q1, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            con.Close();


            chkToChairPerson();
        }

        private void checkResult()
        {

            SqlConnection con = new SqlConnection(conString);
            string query = "select * from result where ClassID='" + classId + "' and CourseID='" + courseId + "' and TId='" + Tid + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                sendToStd.Visible = true;
                sendToCh.Visible = true;
            }

            con.Close();

        }

        protected void sendToStd_Click(object sender, EventArgs e)
        {
            string q1 = null, chk = null;
            SqlConnection con = new SqlConnection(conString);
            string query = "select * from ApprovalTable where ClassId= '" + classId + "' and CourseId='" + courseId + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                q1 = "UPDATE ApprovalTable SET TeacherAppStd='" + 1 + "' WHERE ClassId= '" + classId + "' and CourseId='" + courseId + "'";

                chk = "1";
            }


            con.Close();

            if (chk == null)
            {
                q1 = "insert into ApprovalTable(TeacherAppStd,ClassId,CourseId) values ('" + 1 + "','" + classId + "','" + courseId + "') ";
            }
            con.Open();
            cmd = new SqlCommand(q1, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            con.Close();


            lbl.Text = "Result has been sent to students";
        }

        private void chkToChairPerson()
        {
            SqlConnection con = new SqlConnection(conString);
            string query = "select * from ApprovalTable where TeacherApp='" + 1 + "' and ClassId= '" + classId + "' and CourseId='" + courseId + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sendToCh.Visible = false;
                sendToStd.Visible = false;
                btn_edit.Visible = false;
                chMsg.Visible = true;
            }


            con.Close();

            query = "select * from ApprovalTable where ChairPApp='" + 1 + "' and ClassId= '" + classId + "' and CourseId='" + courseId + "'";
            con.Open();
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                signLbl.Text = "Approved by " + dr["CPName"].ToString();
                dateLbl.Text = dr["DateOfApp"].ToString();
                chMsg.Visible = false;
            }


            con.Close();
        }
    }
}