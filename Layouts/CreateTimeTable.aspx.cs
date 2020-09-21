using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UokSemesterSystem.Classes;

namespace UokSemesterSystem
{
    public partial class CreateTimeTable : System.Web.UI.Page
    {
        private static string conString = Utilities1.GetConnectionString();
        private static SqlConnection con = new SqlConnection(conString);
        string Id, AccountID, listId, courseId, classId, TeacId, AssisId;
        string[] TimeTableData;
        private List<string[]> Courses = new List<string[]>();


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }
        protected void UpdateTT_Click(object sender, EventArgs e)
        {
            if (ClassList.SelectedValue.Equals("NA"))
            {
                ClassErr.Visible = true;
            }
            else if (TeacherList.SelectedValue.Equals("NA"))
            {
                TeacherErr.Visible = true;
            }
            else if (CourseList.SelectedValue.Equals("NA"))
            {
                CourseErr.Visible = true;

            }
            else if (AssisTeacherList.Visible.Equals(true) && AssisTeacherList.SelectedValue.Equals("NA"))
            {
                AssErr.Visible = true;


            }
            else if (STime.Text == "")
            {
                StimeErr.Visible = true;

            }
            else if (ETime.Text.Equals(""))
            {
                EtimeErr.Visible = true;
            }
            else if (RoomNo.Text.Equals(""))
            {
                RoomErr.Visible = true;
            }
            else
            {
                StimeErr.Visible = false;
                EtimeErr.Visible = false;
                RoomErr.Visible = false;
                TimeTableData = new string[13];
                TimeTableData[0] = Session["courseId"].ToString();
                TimeTableData[1] = Session["classId"].ToString();
                TimeTableData[2] = Session["TeacId"].ToString();
                if (AssisTeacherList.Visible.Equals(false))
                {
                    TimeTableData[3] = "0";
                }
                else
                {

                    TimeTableData[3] = Session["AssisId"].ToString();
                }
                TimeTableData[4] = STime.Text;
                TimeTableData[5] = ETime.Text;
                TimeTableData[6] = RoomNo.Text;
                TimeTableData[7] = SemesterList.SelectedValue;
                TimeTableData[8] = DateTime.Now.Year.ToString();
                TimeTableData[9] = Section.Text;
                TimeTableData[10] = DaysList.SelectedValue;
                TimeTableData[11] = Session["AccountId"].ToString();
                TimeTableData[12] = Session["Slot"].ToString();

                UpdateTimeTable(TimeTableData);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
                SaveErr.Text = "Update Successfully";
                //PnlMain.Visible = false;
                //  ShowTTForm.Visible = true;
            }

        }

        private void UpdateTimeTable(string[] TimeTable)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string query = "Update TimeTable set CourseId=@CourseId,ClassId=@ClassId,TId=@TId,AId=@AId,STime=@STime,ETime=@ETime,ClassRoomNo=@ClassRoomNo,SemesterNo=@SemesterNo,Year=@Year,Section=@Section,Day=@Day,DId=@DId,Slot=@Slot where Id='" + Session["TTId"].ToString() + "' ";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@CourseId", TimeTable[0]);
                cmd.Parameters.AddWithValue("@ClassId", TimeTable[1]);
                cmd.Parameters.AddWithValue("@TId", TimeTable[2]);
                cmd.Parameters.AddWithValue("@AId", TimeTable[3]);
                cmd.Parameters.AddWithValue("@STime", TimeTable[4]);
                cmd.Parameters.AddWithValue("@ETime", TimeTable[5]);
                cmd.Parameters.AddWithValue("@ClassRoomNo", TimeTable[6]);
                cmd.Parameters.AddWithValue("@SemesterNo", TimeTable[7]);
                cmd.Parameters.AddWithValue("@Year", TimeTable[8]);
                cmd.Parameters.AddWithValue("@Section", TimeTable[9]);
                cmd.Parameters.AddWithValue("@Day", TimeTable[10]);
                cmd.Parameters.AddWithValue("@DId", TimeTable[11]);
                cmd.Parameters.AddWithValue("@Slot", TimeTable[12]);

                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                con.Close();
            }

        }
        protected void AddTimeTable_Click(object sender, EventArgs e)
        {
            if (ClassList.SelectedValue.Equals("NA"))
            {
                ClassErr.Visible = true;
            }
            else if (TeacherList.SelectedValue.Equals("NA"))
            {
                TeacherErr.Visible = true;
            }
            else if (CourseList.SelectedValue.Equals("NA"))
            {
                CourseErr.Visible = true;

            }
            else if (AssisTeacherList.Visible.Equals(true) && AssisTeacherList.SelectedValue.Equals("NA"))
            {
                AssErr.Visible = true;


            }
            else if (STime.Text == "")
            {
                StimeErr.Visible = true;

            }
            else if (ETime.Text.Equals(""))
            {
                EtimeErr.Visible = true;
            }
            else if (RoomNo.Text.Equals(""))
            {
                RoomErr.Visible = true;
            }
            else
            {
                StimeErr.Visible = false;
                EtimeErr.Visible = false;
                RoomErr.Visible = false;
                TimeTableData = new string[13];
                TimeTableData[0] = Session["courseId"].ToString();
                TimeTableData[1] = Session["classId"].ToString();
                TimeTableData[2] = Session["TeacId"].ToString();
                if (AssisTeacherList.Visible.Equals(false))
                {
                    TimeTableData[3] = "0";
                }
                else
                {

                    TimeTableData[3] = Session["AssisId"].ToString();
                }
                TimeTableData[4] = STime.Text;
                TimeTableData[5] = ETime.Text;
                TimeTableData[6] = RoomNo.Text;
                TimeTableData[7] = SemesterList.SelectedValue;
                TimeTableData[8] = DateTime.Now.Year.ToString();
                TimeTableData[9] = Section.Text;
                TimeTableData[10] = DaysList.SelectedValue;
                TimeTableData[11] = Session["AccountId"].ToString();
                TimeTableData[12] = Session["Slot"].ToString();


                if (CheckForAvailability(TimeTableData))
                {
                    SaveErr.Text = "This Schedule is already register";

                }
                else
                {

                    AddTimeTable(TimeTableData);
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    SaveErr.Text = "TimeTable added successfully";
                    //PnlMain.Visible = false;
                    // ShowTTForm.Visible = true;

                }
            }




        }

        private void AddTimeTable(string[] TimeTable)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                //string query = "Select * from TimeTable where CourseId='" + TimeTable[2] + "' and AId='" + TimeTable[3] + "' and Year='" + TimeTable[8] + "' and STime='" + TimeTable[4] + "' and Day='" + TimeTable[10] + "'";
                string query = "insert into [dbo].[TimeTable]([CourseId],[ClassId],[TId],[AId],[STime],[ETime],[ClassRoomNo],[SemesterNo],[Year],[Section],[Day],[DId],[Slot]) values (@CourseId,@ClassId,@TId,@AId,@STime,@ETime,@ClassRoomNo,@SemesterNo,@Year,@Section,@Day,@DId,@Slot)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CourseId", TimeTable[0]);
                cmd.Parameters.AddWithValue("@ClassId", TimeTable[1]);
                cmd.Parameters.AddWithValue("@TId", TimeTable[2]);
                cmd.Parameters.AddWithValue("@AId", TimeTable[3]);
                cmd.Parameters.AddWithValue("@STime", TimeTable[4]);
                cmd.Parameters.AddWithValue("@ETime", TimeTable[5]);
                cmd.Parameters.AddWithValue("@ClassRoomNo", TimeTable[6]);
                cmd.Parameters.AddWithValue("@SemesterNo", TimeTable[7]);
                cmd.Parameters.AddWithValue("@Year", TimeTable[8]);
                cmd.Parameters.AddWithValue("@Section", TimeTable[9]);
                cmd.Parameters.AddWithValue("@Day", TimeTable[10]);
                cmd.Parameters.AddWithValue("@DId", TimeTable[11]);
                cmd.Parameters.AddWithValue("@Slot", TimeTable[12]);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private bool CheckForAvailability(string[] TimeTable)
        {
            bool check = false;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string query = "Select * from TimeTable where CourseId='" + TimeTable[1] + "' and TId='" + TimeTable[2] + "' and AId='" + TimeTable[3] + "' and Year='" + TimeTable[8] + "' and STime='" + TimeTable[4] + "' and Day='" + TimeTable[10] + "'";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    check = true;
                }
                con.Close();

                con.Open();
                string query1 = "Select * from TimeTable where TId='" + TimeTable[2] + "' and ClassId='" + TimeTable[1] + "' and Year='" + TimeTable[8] + "'";
                SqlCommand com1 = new SqlCommand(query1, con);
                SqlDataReader dr1 = com1.ExecuteReader();
                dr1.Read();
                if (dr1.HasRows)
                {
                    check = true;
                }
                con.Close();

            }
            return check;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            AccountID = Session["AccountId"].ToString();
            Section.Enabled = false;
            STime.Enabled = false;
            ETime.Enabled = false;



            if (!Page.IsPostBack)
            {
                Session["IsClick"] = "1";
                // DepartmentErr.Visible = false;
                Session["Shift"] = "Morning";
                STime.Text = "9:00";
                ETime.Text = "10:50";

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string query = "Select * from Department where DId='" + Session["AccountId"].ToString() + "'";
                    SqlCommand com = new SqlCommand(query, con);
                    SqlDataReader dr = com.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        DepartmentName.Text = dr["DepartmentName"].ToString();
                        DepartmentName.Enabled = false;
                    }

                    con.Close();
                    AddTT.Visible = true;
                    UpdateTT.Visible = false;
                }
                GetDeparmentID();


            }
            else
            {
                Session["IsClick"] = "0";
            }

            GetTimeTable();
            DisplayDetail();

        }

        protected void GetDeparmentID()
        {
            if (!IsPostBack)
            {

                string currentyear = DateTime.Now.Year.ToString();
                ClassErr.Visible = false;
                CourseErr.Visible = false;
                TeacherErr.Visible = false;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string query = "Select * from ClassTable where DId='" + Session["AccountId"].ToString() + "'  and Year='" + currentyear + "' and Shift='" + ShiftList.SelectedItem + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ClassList.DataSource = dt;
                    ClassList.DataBind();
                    ClassList.DataTextField = "ClassName";
                    ClassList.DataValueField = "ClassId";
                    ClassList.DataBind();

                    ClassList.Items.Insert(0, new ListItem("Select", "NA"));
                    Section.Text = dt.Rows[0][2].ToString();

                    con.Close();


                    con.Open();
                    string query1 = "Select * from Course";
                    SqlDataAdapter sda1 = new SqlDataAdapter(query1, con);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    if (!dt1.HasErrors)
                    {


                    }

                    CourseList.DataSource = dt1;
                    CourseList.DataBind();
                    CourseList.DataTextField = "CourseName";
                    CourseList.DataValueField = "CourseId";
                    CourseList.DataBind();

                    CourseList.Items.Insert(0, new ListItem("Select", "NA"));
                    con.Close();

                    con.Open();
                    string query2 = "Select * from Teacher where Department='" + Session["AccountId"].ToString() + "'";
                    SqlDataAdapter sda2 = new SqlDataAdapter(query2, con);
                    DataTable dt2 = new DataTable();
                    sda2.Fill(dt2);
                    TeacherList.DataSource = dt2;
                    TeacherList.DataBind();
                    TeacherList.DataTextField = "TName";
                    TeacherList.DataValueField = "TId";
                    TeacherList.DataBind();

                    TeacherList.Items.Insert(0, new ListItem("Select", "NA"));

                    //AssisTeacherList.DataSource = dt2;
                    //AssisTeacherList.DataBind();
                    //AssisTeacherList.DataTextField = "TName";
                    //AssisTeacherList.DataValueField = "TId";
                    //AssisTeacherList.DataBind();

                    //AssisTeacherList.Items.Insert(0, new ListItem("Select", "NA"));

                    con.Close();

                }


                //    }
            }


        }
        protected void GetShift(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string shift = ShiftList.SelectedItem.ToString();
                Session["Shift"] = shift;
                string currentyear = DateTime.Now.Year.ToString();
                ClassErr.Visible = false;
                CourseErr.Visible = false;
                TeacherErr.Visible = false;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string query = "Select * from ClassTable where DId='" + Session["AccountId"].ToString() + "'  and Year='" + currentyear + "' and Shift='" + Session["Shift"] + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ClassList.DataSource = dt;
                    ClassList.DataBind();
                    ClassList.DataTextField = "ClassName";
                    ClassList.DataValueField = "ClassId";
                    ClassList.DataBind();

                    ClassList.Items.Insert(0, new ListItem("Select", "NA"));
                    Section.Text = dt.Rows[0][2].ToString();

                    con.Close();
                    if (Session["Shift"].Equals("Evening"))
                    {
                        SlotTime.SelectedIndex = 0;
                        STime.Text = "3:30";
                        ETime.Text = "5:10";
                    }
                    else
                    {
                        SlotTime.SelectedIndex = 0;
                        STime.Text = "9:00";
                        ETime.Text = "11:30";
                    }

                }
            }

        }

        protected void SlotTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string Time = SlotTime.SelectedIndex.ToString();
                int slot = Convert.ToInt32(Time) + 1;
                Session["Slot"] = slot.ToString();

                if (Session["Shift"].ToString().Equals("Morning"))
                {
                    if (Time.Equals("0"))
                    {
                        STime.Text = "9:00";
                        ETime.Text = "10:50";
                    }
                    else if (Time.Equals("1"))
                    {
                        STime.Text = "11:00";
                        ETime.Text = "12:50";
                    }
                    else if (Time.Equals("2"))
                    {
                        STime.Text = "1:50";
                        ETime.Text = "3:50";
                    }

                }
                else if (Session["Shift"].ToString().Equals("Evening"))
                {
                    if (Time.Equals("0"))
                    {
                        STime.Text = "3:30";
                        ETime.Text = "5:10";
                    }
                    else if (Time.Equals("1"))
                    {
                        STime.Text = "5:10";
                        ETime.Text = "6:50";
                    }
                    else if (Time.Equals("2"))
                    {
                        STime.Text = "6:50";
                        ETime.Text = "8:50";
                    }

                }

            }
        }


        protected void GetClassID(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                classId = ClassList.SelectedValue;
                Session["classId"] = classId;
                if (classId.Equals("NA"))
                {
                }
                else
                {
                    ClassErr.Visible = false;


                }
            }


        }
        protected void GetCoursesID(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                courseId = CourseList.SelectedValue;
                Session["courseId"] = courseId;
                if (courseId.Equals("NA"))
                {
                }
                else
                {

                    CourseErr.Visible = false;
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        con.Open();
                        string query = "Select CreditHours from Course where CourseId='" + courseId + "'";
                        SqlCommand com = new SqlCommand(query, con);
                        SqlDataReader dr = com.ExecuteReader();
                        dr.Read();
                        if (dr.HasRows)
                        {
                            if (dr["CreditHours"].Equals("2+1"))
                            {
                                con.Close();

                                AssDiv.Visible = true;
                                AssLabelDiv.Visible = true;

                                AssErr.Visible = false;
                                string query2 = "Select * from Teacher where Department='" + Session["AccountId"].ToString() + "'";
                                con.Open();
                                SqlDataAdapter sda2 = new SqlDataAdapter(query2, con);
                                DataTable dt2 = new DataTable();
                                sda2.Fill(dt2);

                                AssisTeacherList.DataSource = dt2;
                                AssisTeacherList.DataBind();
                                AssisTeacherList.DataTextField = "TName";
                                AssisTeacherList.DataValueField = "TId";
                                AssisTeacherList.DataBind();

                                AssisTeacherList.Items.Insert(0, new ListItem("Select", "NA"));
                            }
                            else
                            {
                                AssDiv.Visible = false;
                                AssLabelDiv.Visible = false;
                                AssErr.Visible = false;
                            }
                        }
                        con.Close();

                    }


                }
            }


        }
        protected void GetTeacherID(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                TeacId = TeacherList.SelectedValue;
                Session["TeacId"] = TeacId;
                if (TeacId.Equals("NA"))
                {
                }
                else
                {
                    TeacherErr.Visible = false;


                }
            }


        }
        protected void GetAssTeacherID(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                AssisId = AssisTeacherList.SelectedValue;
                Session["AssisId"] = AssisId;
                if (AssisId.Equals("NA"))
                {
                    Session["AssisId"] = "0";
                }
                else
                {

                    AssErr.Visible = false;
                }
            }


        }
        private void GetTimeTable()
        {
            string cuurentyear = DateTime.Now.Year.ToString();
            string[] temp;
            string[] course;
            SqlConnection con = new SqlConnection(conString);
            string query = "select * from TimeTable where DId='" + AccountID + "' and Year='" + cuurentyear + "'";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            int i = 0;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    temp = new string[3];
                    string Tname;
                    course = new string[12];
                    course[0] = "" + (i + 1);
                    course[1] = dr["Day"].ToString();
                    course[7] = dr["STime"].ToString() + " - " + dr["ETime"].ToString();
                    course[8] = dr["ClassRoomNo"].ToString();
                    course[4] = dr["Section"].ToString();
                    course[11] = dr["Id"].ToString();

                    temp = GetClassInfo(dr["ClassId"].ToString());
                    course[3] = temp[0];
                    course[10] = temp[1];

                    temp = GetCourseInfo(dr["CourseId"].ToString());
                    course[5] = temp[0];
                    course[6] = temp[1];

                    Tname = GetTeacherInfo(dr["TId"].ToString());
                    course[2] = Tname;


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

        protected void ShowTTForm_Click(object sender, EventArgs e)
        {
            //PnlMain.Visible = true;
            //ShowTTForm.Visible = false;
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            //PnlMain.Visible = false;
            // ShowTTForm.Visible = true;
        }

        private string[] GetClassInfo(string Class_ID)
        {
            string[] temp = new string[3];
            SqlConnection con = new SqlConnection(conString);
            string query = "select * from ClassTable where ClassID='" + Class_ID + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                temp[0] = dr["ClassName"].ToString();
                temp[1] = dr["Shift"].ToString();
                temp[2] = dr["ClassSection"].ToString();
            }
            con.Close();
            return temp;
        }

        private string GetTeacherInfo(string TID)
        {
            string temp = "";
            SqlConnection con = new SqlConnection(conString);
            string query = "select TName from Teacher where TId='" + TID + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                temp = dr["TName"].ToString();

            }
            con.Close();
            return temp;
        }


        private string[] GetCourseInfo(string Course_ID)
        {
            string[] temp = new string[2];
            SqlConnection con = new SqlConnection(conString);
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
            string[] temp = new string[11];
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

        private void SortListTwo()
        {
            int[] indexList = new int[Courses.Count];
            int j = 0;
            List<string[]> temp = new List<string[]>();

            for (int a = 0; a < Courses.Count; a++)
            {
                if (Courses[a][9].Equals("1"))
                {
                    indexList[j] = a;
                    j++;
                }
            }
            for (int a = 0; a < Courses.Count; a++)
            {
                if (Courses[a][9].Equals("2"))
                {
                    indexList[j] = a;
                    j++;
                }
            }
            for (int a = 0; a < Courses.Count; a++)
            {
                if (Courses[a][9].Equals("3"))
                {
                    indexList[j] = a;
                    j++;
                }
            }
            for (int a = 0; a < Courses.Count; a++)
            {
                if (Courses[a][9].Equals("4"))
                {
                    indexList[j] = a;
                    j++;
                }
            }
            for (int a = 0; a < Courses.Count; a++)
            {
                if (Courses[a][9].Equals("5"))
                {
                    indexList[j] = a;
                    j++;
                }
            }
            string[] sortArray = new string[12];
            for (int i = 0; i < indexList.Length; i++)
            {
                int p = indexList[i];
                sortArray = Courses[p];
                temp.Add(sortArray);

            }
            Courses = temp;
        }

        private void DisplayDetail()
        {
            //SortList();
            SortListTwo();
            TableRow tr; TableCell tc;
            int countmorn = 1, counteven = 1;
            string[] temp = new string[12];
            int j;
            for (int i = 0; i < Courses.Count; i++)
            {
                tr = new TableRow();

                temp = Courses[i];
                if (temp[10] == "Morning")
                {
                    for (j = 1; j < 9; j++)
                    {
                        tc = new TableCell();
                        tc.Text = temp[j];
                        if (j == 1) tc.CssClass = "backcellleft";
                        else if (j == 8) tc.CssClass = "backcellright";
                        else
                            tc.CssClass = "backcell";
                        tr.Cells.Add(tc);
                    }
                    tc = new TableCell();
                    LinkButton link = new LinkButton();
                    link.ID = temp[11].ToString();
                    link.Text = "Edit";
                    link.CssClass = "tablelink";
                    link.Click += new EventHandler(EditClick);
                    tc.Controls.Add(link);
                    tr.Cells.Add(tc);
                    if (countmorn % 2 == 0)
                        tr.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                    tc.BackColor = System.Drawing.Color.White;
                    TeacherCoursesMorning.Rows.Add(tr);
                    countmorn++;
                }
                else
                {
                    for (j = 1; j < 9; j++)
                    {
                        tc = new TableCell();
                        tc.Text = temp[j];
                        if (j == 1) tc.CssClass = "backcellleft";
                        else if (j == 8) tc.CssClass = "backcellright";
                        else
                            tc.CssClass = "backcell";
                        tr.Cells.Add(tc);
                    }
                    tc = new TableCell();
                    LinkButton link = new LinkButton();
                    link.ID = temp[11].ToString();
                    link.CssClass = "tablelink";
                    link.Text = "Edit";
                    link.Click += new EventHandler(EditClick);

                    tc.Controls.Add(link);
                    if (counteven % 2 == 0)
                        tr.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                    tc.BackColor = System.Drawing.Color.White;
                    tr.Cells.Add(tc);
                    TeacherCoursesEvening.Rows.Add(tr);
                    counteven++;
                }
            }



        }



        private void EditClick(object sender, EventArgs e)
        {
            Session["IsClick"] = "0";
            //PnlMain.Visible = true;
            // ShowTTForm.Visible = false;
            string temp = ((LinkButton)sender).ID.ToString();

            Session["TTId"] = temp;
            if (Page.IsPostBack)
            {
                string[] classInfo = new string[2];
                string[] courseInfo = new string[2];
                string q = "Select * from TimeTable where Id='" + temp + "'";
                con.Open();
                SqlCommand com = new SqlCommand(q, con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();

                if (dr.HasRows)
                {
                    classInfo = GetClassInfo(dr["ClassId"].ToString());
                    //ShiftList.SelectedValue = classInfo[1];
                    ShiftList.SelectedIndex = ShiftList.Items.IndexOf(ShiftList.Items.FindByText(classInfo[1]));
                    Session["Shift"] = classInfo[1];
                    int slot = Convert.ToInt32(dr["Slot"].ToString()) - 1;
                    SlotTime.SelectedIndex = slot;
                    Session["Slot"] = (slot + 1).ToString();

                    Section.Text = classInfo[2];
                    courseInfo = GetCourseInfo(dr["CourseId"].ToString());
                    CourseList.SelectedIndex = CourseList.Items.IndexOf(CourseList.Items.FindByText(courseInfo[1]));
                    Session["courseId"] = CourseList.SelectedValue;

                    //CourseList.SelectedItem.Text = courseInfo[1];
                    //TeacherList.SelectedItem.Text = GetTeacherInfo(dr["TId"].ToString());
                    string Tname = GetTeacherInfo(dr["TId"].ToString());
                    TeacherList.SelectedIndex = TeacherList.Items.IndexOf(TeacherList.Items.FindByText(Tname));
                    Session["TeacId"] = TeacherList.SelectedValue;

                    STime.Text = dr["STime"].ToString();
                    ETime.Text = dr["ETime"].ToString();
                    RoomNo.Text = dr["ClassRoomNo"].ToString();
                    SemesterList.SelectedValue = dr["SemesterNo"].ToString();
                    DaysList.SelectedValue = dr["Day"].ToString();
                    Tname = GetTeacherInfo(dr["AId"].ToString());

                    if (!dr["AId"].Equals("0"))
                    {
                        AssDiv.Visible = true;
                        AssLabelDiv.Visible = true;
                        AssErr.Visible = false;
                        string query2 = "Select * from Teacher where Department='" + Session["AccountId"].ToString() + "'";
                        con.Close();
                        con.Open();
                        SqlDataAdapter sda2 = new SqlDataAdapter(query2, con);
                        DataTable dt2 = new DataTable();
                        sda2.Fill(dt2);

                        AssisTeacherList.DataSource = dt2;
                        AssisTeacherList.DataBind();
                        AssisTeacherList.DataTextField = "TName";
                        AssisTeacherList.DataValueField = "TId";
                        AssisTeacherList.DataBind();

                        AssisTeacherList.Items.Insert(0, new ListItem("Select", "NA"));
                        AssisTeacherList.SelectedIndex = AssisTeacherList.Items.IndexOf(AssisTeacherList.Items.FindByText(Tname));
                        Session["AssisId"] = AssisTeacherList.SelectedValue;
                        con.Close();
                    }
                    else
                    {
                        AssDiv.Visible = false;
                        AssLabelDiv.Visible = false;
                        AssErr.Visible = false;
                    }
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        con.Close();
                        string currentyear = DateTime.Now.Year.ToString();
                        con.Open();
                        string query = "Select * from ClassTable where DId='" + Session["AccountId"].ToString() + "'  and Year='" + currentyear + "' and Shift='" + classInfo[1] + "'";
                        SqlDataAdapter sda = new SqlDataAdapter(query, con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ClassList.DataSource = dt;
                        ClassList.DataBind();
                        ClassList.DataTextField = "ClassName";
                        ClassList.DataValueField = "ClassId";
                        ClassList.DataBind();

                        ClassList.Items.Insert(0, new ListItem("Select", "NA"));
                        Section.Text = dt.Rows[0][2].ToString();
                        ClassList.SelectedIndex = ClassList.Items.IndexOf(ClassList.Items.FindByText(classInfo[0]));
                        Session["classId"] = ClassList.SelectedValue;
                        con.Close();

                    }

                }
                con.Close();
                AddTT.Visible = false;
                UpdateTT.Visible = true;
                AddTimeTableBTN.InnerText = "Edit TimeTable";
            }
            //Response.Redirect("proforma.aspx");
        }
    }
}