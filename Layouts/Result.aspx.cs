using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResultSystem
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public int EmptyFields;
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
        bool resultAvailable = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            courseId = Session["courseId"].ToString();
            // lbl_TeacherName.Text = Session["TeacherName"].ToString();
            Tid = Session["AccountId"].ToString();

            string q = "SELECT * FROM Teacher where TId='" + Tid + "' ";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand com = new SqlCommand(q, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            lbl_TeacherName.Text = dr["TName"].ToString();
            con.Close();


            if (btn_submit.Visible == true)
            {
                aa.Style.Add("display", "block");
                btn_edit.Visible = false;
                btn_updateResult.Visible = false;
            }
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

                //cmd.CommandText = "SELECT ClassId FROM TimeTable WHERE CourseId ='" + courseId + "'and TId='" +Tid+ "'";
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




                cmd.CommandText = "SELECT Department FROM Student WHERE ClassId ='" + classId + "'";
                cmd.Connection = sqlCon;
                sqlDa.SelectCommand = cmd;

                DataSet ds4 = new DataSet();
                sqlDa.Fill(ds4);
                lbl_deptName.Text = ds4.Tables[0].Rows[0]["Department"].ToString();
                lbl_majDeptName.Text = ds4.Tables[0].Rows[0]["Department"].ToString();


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


                // saving all the Stds Ids
                for (int a = 0; a < dtbl.Rows.Count; a++)
                {
                    stdIdArr.Add(dtbl.Rows[a]["SId"].ToString());
                }


                SqlDataAdapter sqlDaR = new SqlDataAdapter("SELECT * FROM Result WHERE CourseID='" + courseId + "'AND ClassID='" + classId + "' AND TId='" + Tid + "' ", conString);

                DataTable dtblR = new DataTable();
                sqlDaR.Fill(dtblR);

                if (dtblR.Rows.Count > 0)
                {

                    resultAvailable = true;

                }
                if (resultAvailable == true)
                {
                    btn_submit.Visible = false;
                    btn_updateResult.Visible = true;
                    ss.Style.Add("display", "block");
                    aa.Style.Add("display", "none");
                    eDate.Text = dtblR.Rows[0]["ExamDate"].ToString();
                    eDate.ReadOnly = true;

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
                        lbl.Text = dtbl.Rows[a]["RollNumber"].ToString();
                        cell1.CssClass = "backcell";
                        cell1.Controls.Add(lbl);
                        //add cell to row
                        row.Cells.Add(cell1);

                        TableCell cell2 = new TableCell();
                        var lbl2 = new Label();
                        lbl2.Text = dtbl.Rows[a]["SName"].ToString();
                        cell2.CssClass = "backcell";
                        cell2.Controls.Add(lbl2);
                        //add cell to row
                        row.Cells.Add(cell2);

                        //add row to dt
                        TableCell cell3 = new TableCell();
                        TextBox tb = new TextBox();
                        tb.ID = "TextBox" + a;
                        tb.CssClass = "resulttextbox";
                        tb.MaxLength = 3;

                        RangeValidator rng2 = new RangeValidator();
                        rng2.ErrorMessage = " x";
                        rng2.MinimumValue = "0";
                        rng2.MaximumValue = "100";
                        rng2.ToolTip = "Value must be in the range: 0-100";
                        rng2.ForeColor = System.Drawing.Color.Red;
                        rng2.Font.Bold = true;
                        rng2.Type = ValidationDataType.Integer;
                        rng2.Style.Add("cursor", "default");
                        rng2.ControlToValidate = tb.ID;

                        if (resultAvailable == true)
                            tb.Text = dtblR.Rows[a]["TotalMarks"].ToString();
                        cell3.CssClass = "backcell";
                        cell3.Controls.Add(tb);
                        cell3.Controls.Add(rng2);
                        //add cell to row
                        row.Cells.Add(cell3);

                        TableCell cell4 = new TableCell();
                        var lbl3 = new Label();
                        lbl3.ID = "lbl_inWords" + a;
                        cell4.CssClass = "backcell";
                        cell4.Controls.Add(lbl3);
                        //add cell to row
                        row.Cells.Add(cell4);

                        TableCell cell5 = new TableCell();
                        var lbl4 = new Label();
                        lbl4.ID = "lbl_gpa" + a;
                        cell5.CssClass = "backcell";
                        cell5.Controls.Add(lbl4);
                        //add cell to row
                        row.Cells.Add(cell5);

                        TableCell cell6 = new TableCell();
                        var lbl5 = new Label();
                        lbl5.ID = "lbl_remarks" + a;
                        cell6.CssClass = "backcell";
                        cell6.Controls.Add(lbl5);
                        //add cell to row
                        row.Cells.Add(cell6);
                        if (a % 2 != 0)
                        {
                            row.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                        }

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
                        lbl1.Text = dtbl.Rows[a]["RollNumber"].ToString();
                        cell1.CssClass = "backcell";
                        cell1.Controls.Add(lbl1);
                        //add cell to row
                        row.Cells.Add(cell1);

                        TableCell cell2 = new TableCell();
                        var lbl2 = new Label();
                        lbl2.Text = dtbl.Rows[a]["SName"].ToString();
                        cell2.CssClass = "backcell";
                        cell2.Controls.Add(lbl2);
                        //add cell to row
                        row.Cells.Add(cell2);


                        

                        //add row to dt
                        TableCell cell3 = new TableCell();
                        TextBox tbT = new TextBox();
                        tbT.ID = "TextBoxT" + a;
                        tbT.CssClass = "resulttextbox";
                        tbT.MaxLength = 2;

                        RangeValidator rng = new RangeValidator();
                        rng.ErrorMessage = " x";
                        rng.MinimumValue = "0";
                        rng.MaximumValue = "80";
                        rng.ToolTip = "Value must be in the range: 0-80";
                        rng.ForeColor = System.Drawing.Color.Red;
                        rng.Font.Bold = true;
                        rng.Type = ValidationDataType.Integer;
                        rng.Style.Add("cursor", "default");
                        rng.ControlToValidate = tbT.ID;


                        if (resultAvailable == true)
                            tbT.Text = dtblR.Rows[a]["Theory"].ToString();
                        cell3.CssClass = "backcell";
                        cell3.Controls.Add(tbT);
                        cell3.Controls.Add(rng);
                        //add cell to row
                        row.Cells.Add(cell3);

                        TableCell cell4 = new TableCell();
                        TextBox tbL = new TextBox();
                        tbL.CssClass = "resulttextbox";
                        tbL.MaxLength = 2;
                        tbL.ID = "TextBoxL" + a;


                        RangeValidator rng1 = new RangeValidator();
                        rng1.ErrorMessage = " x";
                        rng1.MinimumValue = "0";
                        rng1.MaximumValue = "20";
                        rng1.ToolTip = "Value must be in the range: 0-20";
                        rng1.ForeColor = System.Drawing.Color.Red;
                        rng1.Font.Bold = true;
                        rng1.Type = ValidationDataType.Integer;
                        rng1.Style.Add("cursor", "default");
                        rng1.ControlToValidate = tbL.ID;


                        if (resultAvailable == true)
                            tbL.Text = dtblR.Rows[a]["Lab"].ToString();
                        cell4.CssClass = "backcell";
                        cell4.Controls.Add(tbL);
                        cell4.Controls.Add(rng1);
                        //add cell to row
                        row.Cells.Add(cell4);

                        TableCell cell5 = new TableCell();
                        var lbl3 = new Label();
                        lbl3.ID = "lbl_TotalMarks" + a;
                        cell5.CssClass = "backcell";
                        cell5.Controls.Add(lbl3);
                        //add cell to row
                        row.Cells.Add(cell5);


                        TableCell cell6 = new TableCell();
                        var lbl4 = new Label();
                        lbl4.ID = "lbl_inWords" + a;
                        cell6.CssClass = "backcell";
                        cell6.Controls.Add(lbl4);
                        //add cell to row
                        row.Cells.Add(cell6);

                        TableCell cell7 = new TableCell();
                        var lbl5 = new Label();
                        lbl5.ID = "lbl_gpa" + a;
                        cell7.CssClass = "backcell";
                        cell7.Controls.Add(lbl5);
                        //add cell to row
                        row.Cells.Add(cell7);

                        TableCell cell8 = new TableCell();
                        var lbl6 = new Label();
                        lbl6.ID = "lbl_remarks" + a;
                        cell8.CssClass = "backcell";
                        cell8.Controls.Add(lbl6);
                        //add cell to row
                        row.Cells.Add(cell8);
                        if (a % 2 != 0)
                        {
                            row.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                        }


                        dt.Rows.Add(row);
                    }

                }

                sqlCon.Close();

            }

        }

        protected bool CheckDate()
        {
            int count = 0;
            bool isCorrect = false;
            string dateString = eDate.Text;
            string[] date_elements = dateString.Split('/');

            int current_year = Int32.Parse(DateTime.Now.Year.ToString());

            int day = Int32.Parse(date_elements[0]);
            int month = Int32.Parse(date_elements[1]);
            int year = Int32.Parse(date_elements[2]);
            if (day > 0 && day <= 31)
                count++;
            if (month > 0 && month <= 12)
                count++;
            if (year > 1900 && year <= current_year)
                count++;

            if (count > 0)
                isCorrect = true;
            else
                isCorrect = false;

            return isCorrect;
        }

        protected void submitResult(object sender, EventArgs e)
        {
           
            List<String> totalMarks = new List<String>();
            List<String> totalMarksInWords = new List<String>();
            List<String> remarks = new List<String>();
            List<String> gpa = new List<String>();
            List<String> theoryMarks = new List<String>();
            List<String> labMarks = new List<String>();
            int j = 1;

            for (int i = 0; i < stdIdArr.Count; i++)
            {
                if (creditHours == "3+0")
                {
                    totalMarks.Add(((dt.Rows[j].Cells[3].FindControl("TextBox" + i)) as TextBox).Text.Trim());
                    totalMarksInWords.Add(NumberToWords(Convert.ToInt32(((dt.Rows[j].Cells[3].FindControl("TextBox" + i)) as TextBox).Text.Trim())));
                    remarks.Add(getRemarks(Convert.ToInt32(((dt.Rows[j].Cells[3].FindControl("TextBox" + i)) as TextBox).Text.Trim())));
                    gpa.Add(getGpa(Convert.ToInt32(((dt.Rows[j].Cells[3].FindControl("TextBox" + i)) as TextBox).Text.Trim())));
                    j++;
                }
                else
                {
                    theoryMarks.Add(((dt.Rows[j].Cells[3].FindControl("TextBoxT" + i)) as TextBox).Text.Trim());
                    labMarks.Add(((dt.Rows[j].Cells[4].FindControl("TextBoxL" + i)) as TextBox).Text.Trim());
                    int totalM = Convert.ToInt32(((dt.Rows[j].Cells[3].FindControl("TextBoxT" + i)) as TextBox).Text.Trim()) + Convert.ToInt32(((dt.Rows[j].Cells[4].FindControl("TextBoxL" + i)) as TextBox).Text.Trim());
                    totalMarks.Add(totalM.ToString());
                    totalMarksInWords.Add(NumberToWords(totalM));
                    remarks.Add(getRemarks(Convert.ToInt32(((dt.Rows[j].Cells[3].FindControl("TextBoxT" + i)) as TextBox).Text.Trim()), Convert.ToInt32(((dt.Rows[j].Cells[4].FindControl("TextBoxL" + i)) as TextBox).Text.Trim())));
                    gpa.Add(getGpa(Convert.ToInt32(((dt.Rows[j].Cells[3].FindControl("TextBoxT" + i)) as TextBox).Text.Trim()), Convert.ToInt32(((dt.Rows[j].Cells[4].FindControl("TextBoxL" + i)) as TextBox).Text.Trim())));
                    j++;
                }
            }

            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                sqlCon.Open();
                if (creditHours == "3+0")
                {
                    string query = "INSERT INTO Result(CourseID,SId,TotalMarks,GPA,TotInWords,Remarks,TId,ClassID,SemesterNo,ExamDate,Section,ExamAttendance) VALUES(@CourseID,@SId,@TotalMarks,@GPA,@TotInWords,@Remarks,@TId,@ClassID,@SemesterNo,@ExamDate,@Section,@eAtt)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    for (int i = 0; i < stdIdArr.Count; i++)
                    {
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.AddWithValue("@CourseID", courseId);
                        sqlCmd.Parameters.AddWithValue("@SId", stdIdArr[i]);
                        sqlCmd.Parameters.AddWithValue("@TotalMarks", totalMarks[i]);
                        sqlCmd.Parameters.AddWithValue("@GPA", gpa[i]);
                        sqlCmd.Parameters.AddWithValue("@TotInWords", totalMarksInWords[i]);
                        sqlCmd.Parameters.AddWithValue("@Remarks", remarks[i]);
                        sqlCmd.Parameters.AddWithValue("@TId", Tid);
                        sqlCmd.Parameters.AddWithValue("@ClassID", classId);
                        sqlCmd.Parameters.AddWithValue("@SemesterNo", semesterNo);
                        sqlCmd.Parameters.AddWithValue("@ExamDate", examDate);
                        sqlCmd.Parameters.AddWithValue("@Section", classSection);
                        int m = Convert.ToInt32(totalMarks[i]);
                        if (m == 0)
                        {
                            sqlCmd.Parameters.AddWithValue("@eAtt", "A");

                        }
                        else
                        {
                            sqlCmd.Parameters.AddWithValue("@eAtt", "P");
                        }

                        sqlCmd.ExecuteNonQuery();

                    }

                    for (int k = 0; k < stdIdArr.Count; k++)
                    {
                        int l = 1;
                        (dt.Rows[l].Cells[4].FindControl("lbl_inWords" + k) as Label).Text = totalMarksInWords[k];
                        l++;

                    }
                    for (int k = 0; k < stdIdArr.Count; k++)
                    {
                        int l = 1;
                        (dt.Rows[l].Cells[5].FindControl("lbl_gpa" + k) as Label).Text = gpa[k];
                        l++;

                    }

                    for (int k = 0; k < stdIdArr.Count; k++)
                    {
                        int l = 1;
                        (dt.Rows[l].Cells[5].FindControl("lbl_remarks" + k) as Label).Text = remarks[k];
                        l++;

                    }

                    sqlCon.Close();

                    btn_submit.Visible = false;
                    btn_edit.Visible = true;

                    aa.Style.Add("display", "none");
                    if (btn_submit.Visible == false)
                    {
                        for (int k = 0; k < stdIdArr.Count; k++)
                        {
                            int l = 1;
                            (dt.Rows[l].Cells[3].FindControl("TextBox" + k) as TextBox).ReadOnly = true;
                            l++;

                        }
                    }

                }
                else
                {
                    string query = "INSERT INTO Result(CourseID,SId,Theory,Lab,TotalMarks,GPA,TotInWords,Remarks,TId,ClassID,SemesterNo,ExamDate,Section,ExamAttendance) VALUES(@CourseID,@SId,@Theory,@Lab,@TotalMarks,@GPA,@TotInWords,@Remarks,@TId,@ClassID,@SemesterNo,@ExamDate,@Section,@eAtt)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    for (int i = 0; i < stdIdArr.Count; i++)
                    {
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.AddWithValue("@CourseID", courseId);
                        sqlCmd.Parameters.AddWithValue("@SId", stdIdArr[i]);
                        sqlCmd.Parameters.AddWithValue("@Theory", theoryMarks[i]);
                        sqlCmd.Parameters.AddWithValue("@Lab", labMarks[i]);
                        sqlCmd.Parameters.AddWithValue("@TotalMarks", totalMarks[i]);
                        sqlCmd.Parameters.AddWithValue("@GPA", gpa[i]);
                        sqlCmd.Parameters.AddWithValue("@TotInWords", totalMarksInWords[i]);
                        sqlCmd.Parameters.AddWithValue("@Remarks", remarks[i]);
                        sqlCmd.Parameters.AddWithValue("@TId", Tid);
                        sqlCmd.Parameters.AddWithValue("@ClassID", classId);
                        sqlCmd.Parameters.AddWithValue("@SemesterNo", semesterNo);
                        sqlCmd.Parameters.AddWithValue("@ExamDate", examDate);
                        sqlCmd.Parameters.AddWithValue("@Section", classSection);
                        int m = Convert.ToInt32(totalMarks[i]);
                        if (m == 0)
                        {
                            sqlCmd.Parameters.AddWithValue("@eAtt", "A");

                        }
                        else
                        {
                            sqlCmd.Parameters.AddWithValue("@eAtt", "P");
                        }
                        sqlCmd.ExecuteNonQuery();

                    }
                    for (int k = 0; k < stdIdArr.Count; k++)
                    {
                        int l = 1;
                        (dt.Rows[l].Cells[5].FindControl("lbl_TotalMarks" + k) as Label).Text = totalMarks[k];
                        l++;

                    }
                    for (int k = 0; k < stdIdArr.Count; k++)
                    {
                        int l = 1;
                        (dt.Rows[l].Cells[6].FindControl("lbl_inWords" + k) as Label).Text = totalMarksInWords[k];
                        l++;

                    }

                    for (int k = 0; k < stdIdArr.Count; k++)
                    {
                        int l = 1;
                        (dt.Rows[l].Cells[7].FindControl("lbl_gpa" + k) as Label).Text = gpa[k];
                        l++;

                    }

                    for (int k = 0; k < stdIdArr.Count; k++)
                    {
                        int l = 1;
                        (dt.Rows[l].Cells[8].FindControl("lbl_remarks" + k) as Label).Text = remarks[k];
                        l++;

                    }
                    sqlCon.Close();

                    btn_submit.Visible = false;
                    btn_edit.Visible = true;
                    aa.Style.Add("display", "none");
                    if (btn_submit.Visible == false)
                    {
                        for (int k = 0; k < stdIdArr.Count; k++)
                        {
                            int l = 1;
                            (dt.Rows[l].Cells[3].FindControl("TextBoxT" + k) as TextBox).ReadOnly = true;
                            (dt.Rows[l].Cells[4].FindControl("TextBoxL" + k) as TextBox).ReadOnly = true;
                            l++;

                        }
                    }

                }


            }
            Response.Redirect("FinalViewResult.aspx");
        
        } 


        private string getGpa(int marks)
        {

            if (marks >= 85)
                return "4";

            if (marks >= 80 && marks <= 84)
                return "3.8";

            if (marks >= 75 && marks <= 79)
                return "3.4";

            if (marks >= 71 && marks <= 74)
                return "3.0";

            if (marks >= 68 && marks <= 70)
                return "2.8";

            if (marks >= 64 && marks <= 67)
                return "2.4";

            if (marks >= 61 && marks <= 63)
                return "2.0";

            if (marks >= 57 && marks <= 60)
                return "1.8";

            if (marks >= 53 && marks <= 56)
                return "1.4";

            if (marks >= 50 && marks <= 52)
                return "1";
            else
                return "0.0";

        }

        private string getRemarks(int marks)
        {
            if (marks >= 50)
                return "Passes";
            else
                return "Fails";
        }

        string getGpa(int theory, int lab)
        {
            int marks = theory + lab;
            if (theory >= 50 && lab >= 10)
            {
                if (marks >= 85)
                    return "4";

                if (marks >= 80 && marks <= 84)
                    return "3.8";

                if (marks >= 75 && marks <= 79)
                    return "3.4";

                if (marks >= 71 && marks <= 74)
                    return "3.0";

                if (marks >= 68 && marks <= 70)
                    return "2.8";

                if (marks >= 64 && marks <= 67)
                    return "2.4";

                if (marks >= 61 && marks <= 63)
                    return "2.0";

                if (marks >= 57 && marks <= 60)
                    return "1.8";

                if (marks >= 53 && marks <= 56)
                    return "1.4";

                if (marks >= 50 && marks <= 52)
                    return "1";
            }
            else if (theory < 50 || lab < 10)
                return "0.0";
            return "";

        }

        string getRemarks(int theory, int lab)
        {
            if (theory >= 50 && lab >= 10)
                return "Passes";
            else if (theory < 50 && lab < 10)
                return "Fails in Theory and Lab";
            else if (theory < 50)
                return "Fails in Theory";
            else if (lab < 10)
                return "Fails in Lab";

            else
                return "Fails";
        }


        public string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            if (creditHours == "3+0")
            {
                for (int k = 0; k < stdIdArr.Count; k++)
                {
                    int l = 1;
                    (dt.Rows[l].Cells[3].FindControl("TextBox" + k) as TextBox).ReadOnly = false;
                    l++;

                }


                for (int k = 0; k < stdIdArr.Count; k++)
                {
                    int l = 1;
                    (dt.Rows[l].Cells[4].FindControl("lbl_inWords" + k) as Label).Text = "";
                    l++;

                }
                for (int k = 0; k < stdIdArr.Count; k++)
                {
                    int l = 1;
                    (dt.Rows[l].Cells[5].FindControl("lbl_gpa" + k) as Label).Text = "";
                    l++;

                }

                for (int k = 0; k < stdIdArr.Count; k++)
                {
                    int l = 1;
                    (dt.Rows[l].Cells[5].FindControl("lbl_remarks" + k) as Label).Text = "";
                    l++;

                }

            }

            else
            {
                for (int k = 0; k < stdIdArr.Count; k++)
                {
                    int l = 1;
                    (dt.Rows[l].Cells[3].FindControl("TextBoxT" + k) as TextBox).ReadOnly = false;
                    (dt.Rows[l].Cells[4].FindControl("TextBoxL" + k) as TextBox).ReadOnly = false;
                    l++;

                }

                for (int k = 0; k < stdIdArr.Count; k++)
                {
                    int l = 1;
                    (dt.Rows[l].Cells[5].FindControl("lbl_TotalMarks" + k) as Label).Text = "";
                    l++;

                }
                for (int k = 0; k < stdIdArr.Count; k++)
                {
                    int l = 1;
                    (dt.Rows[l].Cells[6].FindControl("lbl_inWords" + k) as Label).Text = "";
                    l++;

                }

                for (int k = 0; k < stdIdArr.Count; k++)
                {
                    int l = 1;
                    (dt.Rows[l].Cells[7].FindControl("lbl_gpa" + k) as Label).Text = "";
                    l++;

                }

                for (int k = 0; k < stdIdArr.Count; k++)
                {
                    int l = 1;
                    (dt.Rows[l].Cells[8].FindControl("lbl_remarks" + k) as Label).Text = "";
                    l++;

                }
            }

            btn_edit.Visible = false;
            btn_updateResult.Visible = true;
            ss.Style.Add("display", "block");
        }

        protected void btn_updateResult_Click(object sender, EventArgs e)
        {
            

            List<String> totalMarks = new List<String>();
            List<String> totalMarksInWords = new List<String>();
            List<String> remarks = new List<String>();
            List<String> gpa = new List<String>();
            List<String> theoryMarks = new List<String>();
            List<String> labMarks = new List<String>();
            int j = 1;

            for (int i = 0; i < stdIdArr.Count; i++)
            {
                if (creditHours == "3+0")
                {
                    totalMarks.Add(((dt.Rows[j].Cells[3].FindControl("TextBox" + i)) as TextBox).Text.Trim());
                    totalMarksInWords.Add(NumberToWords(Convert.ToInt32(((dt.Rows[j].Cells[3].FindControl("TextBox" + i)) as TextBox).Text.Trim())));
                    remarks.Add(getRemarks(Convert.ToInt32(((dt.Rows[j].Cells[3].FindControl("TextBox" + i)) as TextBox).Text.Trim())));
                    gpa.Add(getGpa(Convert.ToInt32(((dt.Rows[j].Cells[3].FindControl("TextBox" + i)) as TextBox).Text.Trim())));
                    j++;
                }
                else
                {
                    theoryMarks.Add(((dt.Rows[j].Cells[3].FindControl("TextBoxT" + i)) as TextBox).Text.Trim());
                    labMarks.Add(((dt.Rows[j].Cells[4].FindControl("TextBoxL" + i)) as TextBox).Text.Trim());
                    int totalM = Convert.ToInt32(((dt.Rows[j].Cells[3].FindControl("TextBoxT" + i)) as TextBox).Text.Trim()) + Convert.ToInt32(((dt.Rows[j].Cells[4].FindControl("TextBoxL" + i)) as TextBox).Text.Trim());
                    totalMarks.Add(totalM.ToString());
                    totalMarksInWords.Add(NumberToWords(totalM));
                    remarks.Add(getRemarks(Convert.ToInt32(((dt.Rows[j].Cells[3].FindControl("TextBoxT" + i)) as TextBox).Text.Trim()), Convert.ToInt32(((dt.Rows[j].Cells[4].FindControl("TextBoxL" + i)) as TextBox).Text.Trim())));
                    gpa.Add(getGpa(Convert.ToInt32(((dt.Rows[j].Cells[3].FindControl("TextBoxT" + i)) as TextBox).Text.Trim()), Convert.ToInt32(((dt.Rows[j].Cells[4].FindControl("TextBoxL" + i)) as TextBox).Text.Trim())));
                    j++;
                }
            }

            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                sqlCon.Open();
                if (creditHours == "3+0")
                {
                    string query = "UPDATE Result SET TotalMarks=@TotalMarks,TotInWords=@TotInWords,GPA=@GPA,Remarks=@Remarks WHERE SId=@SId AND CourseID=@CourseID ";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    for (int i = 0; i < stdIdArr.Count; i++)
                    {
                        sqlCmd.Parameters.Clear();

                        sqlCmd.Parameters.AddWithValue("@TotalMarks", totalMarks[i]);

                        sqlCmd.Parameters.AddWithValue("@TotInWords", totalMarksInWords[i]);
                        sqlCmd.Parameters.AddWithValue("@GPA", gpa[i]);
                        sqlCmd.Parameters.AddWithValue("@Remarks", remarks[i]);
                        sqlCmd.Parameters.AddWithValue("@SId", stdIdArr[i]);
                        sqlCmd.Parameters.AddWithValue("@CourseID", courseId);
                        sqlCmd.ExecuteNonQuery();

                    }

                    for (int k = 0; k < stdIdArr.Count; k++)
                    {
                        int l = 1;
                        (dt.Rows[l].Cells[4].FindControl("lbl_inWords" + k) as Label).Text = totalMarksInWords[k];
                        l++;

                    }
                    for (int k = 0; k < stdIdArr.Count; k++)
                    {
                        int l = 1;
                        (dt.Rows[l].Cells[5].FindControl("lbl_gpa" + k) as Label).Text = gpa[k];
                        l++;

                    }

                    for (int k = 0; k < stdIdArr.Count; k++)
                    {
                        int l = 1;
                        (dt.Rows[l].Cells[5].FindControl("lbl_remarks" + k) as Label).Text = remarks[k];
                        l++;

                    }

                    sqlCon.Close();


                    btn_edit.Visible = true;
                    btn_updateResult.Visible = false;
                    ss.Style.Add("display", "none");
                    if (btn_submit.Visible == false)
                    {
                        for (int k = 0; k < stdIdArr.Count; k++)
                        {
                            int l = 1;
                            (dt.Rows[l].Cells[3].FindControl("TextBox" + k) as TextBox).ReadOnly = true;
                            l++;

                        }
                    }

                }
                else
                {
                    string query = "UPDATE Result SET Theory=@Theory,Lab=@Lab,TotalMarks=@TotalMarks,TotInWords=@TotInWords,GPA=@GPA,Remarks=@Remarks WHERE SId=@SId AND CourseID=@CourseID ";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    for (int i = 0; i < stdIdArr.Count; i++)
                    {
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.AddWithValue("@Theory", theoryMarks[i]);
                        sqlCmd.Parameters.AddWithValue("@Lab", labMarks[i]);
                        sqlCmd.Parameters.AddWithValue("@TotalMarks", totalMarks[i]);
                        sqlCmd.Parameters.AddWithValue("@TotInWords", totalMarksInWords[i]);
                        sqlCmd.Parameters.AddWithValue("@GPA", gpa[i]);
                        sqlCmd.Parameters.AddWithValue("@Remarks", remarks[i]);
                        sqlCmd.Parameters.AddWithValue("@SId", stdIdArr[i]);
                        sqlCmd.Parameters.AddWithValue("@CourseID", courseId);
                        sqlCmd.ExecuteNonQuery();
                    }
                    for (int k = 0; k < stdIdArr.Count; k++)
                    {
                        int l = 1;
                        (dt.Rows[l].Cells[5].FindControl("lbl_TotalMarks" + k) as Label).Text = totalMarks[k];
                        l++;

                    }
                    for (int k = 0; k < stdIdArr.Count; k++)
                    {
                        int l = 1;
                        (dt.Rows[l].Cells[6].FindControl("lbl_inWords" + k) as Label).Text = totalMarksInWords[k];
                        l++;

                    }

                    for (int k = 0; k < stdIdArr.Count; k++)
                    {
                        int l = 1;
                        (dt.Rows[l].Cells[7].FindControl("lbl_gpa" + k) as Label).Text = gpa[k];
                        l++;

                    }

                    for (int k = 0; k < stdIdArr.Count; k++)
                    {
                        int l = 1;
                        (dt.Rows[l].Cells[8].FindControl("lbl_remarks" + k) as Label).Text = remarks[k];
                        l++;

                    }
                    sqlCon.Close();


                    btn_edit.Visible = true;
                    btn_updateResult.Visible = false;
                    ss.Style.Add("display", "none");
                    if (btn_submit.Visible == false)
                    {
                        for (int k = 0; k < stdIdArr.Count; k++)
                        {
                            int l = 1;
                            (dt.Rows[l].Cells[3].FindControl("TextBoxT" + k) as TextBox).ReadOnly = true;
                            (dt.Rows[l].Cells[4].FindControl("TextBoxL" + k) as TextBox).ReadOnly = true;
                            l++;

                        }
                    }

                }
                Response.Redirect("FinalViewResult.aspx");

       
            }
        }

        protected void emptyButton_Click(object sender, EventArgs e)
        {
            EmptyFields = 1;
        }
    }
}