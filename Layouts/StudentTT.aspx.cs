using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UokSemesterSystem
{
    public partial class StudentTT : System.Web.UI.Page
    {
        string Class_ID, Class_Name, Student_Section, Student_Enrol;
        string Student_ID;
        string[,] CourseInformation = new string[15, 7];
        string Shift; int count = 0;
        string strcon = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        TableCell Cell1, Cell2, Cell3, Cell4, Cell5, Cell6, Cell7, Cell8, Cell9, Cell10;
        TableCell Cell11, Cell12, Cell13, Cell14, Cell15, c1, c2, c3;


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            Student_ID = Session["AccountID"].ToString();
            Student_Enrol = GetStudentEnrollment();
            Class_ID = GetClassID();
            Class_Name = GetClassName();
            classname.Text = Class_Name + " | ";
            classshift.Text = Shift + " | ";
            classsection.Text = "SECTION - " + Student_Section;

            CellInitialize();


            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                    GetTimeTable("monday");
                if (i == 1)
                    GetTimeTable("tuesday");
                if (i == 2)
                    GetTimeTable("wednesday");
                if (i == 3)
                    GetTimeTable("thursday");
                if (i == 4)
                    GetTimeTable("friday");
            }

            RemoveDups();
        }

        private void CellInitialize()
        {
            string S1T = "", S2T = "", S3T = "";
            if (Shift.Equals("Morning"))
            {
                S1T = "09:00 - 10:40";
                S2T = "11:00 - 11:50";
                S3T = "01:50 - 03:30";
            }
            else
            {
                S1T = "03:30 - 5:10";
                S2T = "5:10 - 6:50";
                S3T = "06:50 - 08:30";
            }

            Cell1 = new TableCell();
            Cell2 = new TableCell();
            Cell3 = new TableCell();
            Cell1.Text = "";
            Cell2.Text = "";
            Cell3.Text = "";
            MondayCourses.Cells.Add(Cell1);
            MondayCourses.Cells.Add(Cell2);
            MondayCourses.Cells.Add(Cell3);
            Cell4 = new TableCell();
            Cell5 = new TableCell();
            Cell6 = new TableCell();
            Cell4.Text = "";
            Cell5.Text = "";
            Cell6.Text = "";
            TuesdayCourses.Cells.Add(Cell4);
            TuesdayCourses.Cells.Add(Cell5);
            TuesdayCourses.Cells.Add(Cell6);
            Cell7 = new TableCell();
            Cell8 = new TableCell();
            Cell9 = new TableCell();
            Cell7.Text = "";
            Cell8.Text = "";
            Cell9.Text = "";
            WednesdayCourses.Cells.Add(Cell7);
            WednesdayCourses.Cells.Add(Cell8);
            WednesdayCourses.Cells.Add(Cell9);
            Cell10 = new TableCell();
            Cell11 = new TableCell();
            Cell12 = new TableCell();
            Cell10.Text = "";
            Cell11.Text = "";
            Cell12.Text = "";
            ThursdayCourses.Cells.Add(Cell10);
            ThursdayCourses.Cells.Add(Cell11);
            ThursdayCourses.Cells.Add(Cell12);
            Cell13 = new TableCell();
            Cell14 = new TableCell();
            Cell15 = new TableCell();
            Cell13.Text = "";
            Cell14.Text = "";
            Cell15.Text = "";
            FridayCourses.Cells.Add(Cell13);
            FridayCourses.Cells.Add(Cell14);
            FridayCourses.Cells.Add(Cell15);

            c1 = new TableCell();
            c1.Text = S1T;
            c1.CssClass = "time";
            c2 = new TableCell();
            c2.Text = S2T;
            c2.CssClass = "time";
            c3 = new TableCell();
            c3.Text = S3T;
            c3.CssClass = "time";

            MondayTime.Cells.Add(c1);
            MondayTime.Cells.Add(c2);
            MondayTime.Cells.Add(c3);
            c1 = new TableCell();
            c1.Text = S1T;
            c1.CssClass = "time";
            c2 = new TableCell();
            c2.Text = S2T;
            c2.CssClass = "time";
            c3 = new TableCell();
            c3.Text = S3T;
            c3.CssClass = "time";

            TuesdayTime.Cells.Add(c1);
            TuesdayTime.Cells.Add(c2);
            TuesdayTime.Cells.Add(c3);
            c1 = new TableCell();
            c1.Text = S1T;
            c1.CssClass = "time";
            c2 = new TableCell();
            c2.Text = S2T;
            c2.CssClass = "time";
            c3 = new TableCell();
            c3.Text = S3T;
            c3.CssClass = "time";

            WednesdayTime.Cells.Add(c1);
            WednesdayTime.Cells.Add(c2);
            WednesdayTime.Cells.Add(c3);

            c1 = new TableCell();
            c1.Text = S1T;
            c1.CssClass = "time";
            c2 = new TableCell();
            c2.Text = S2T;
            c2.CssClass = "time";
            c3 = new TableCell();
            c3.Text = S3T;
            c3.CssClass = "time";

            ThursdayTime.Cells.Add(c1);
            ThursdayTime.Cells.Add(c2);
            ThursdayTime.Cells.Add(c3);

            c1 = new TableCell();
            c1.Text = S1T;
            c1.CssClass = "time";
            c2 = new TableCell();
            c2.Text = S2T;
            c2.CssClass = "time";
            c3 = new TableCell();
            c3.Text = S3T;
            c3.CssClass = "time";


            FridayTime.Cells.Add(c1);
            FridayTime.Cells.Add(c2);
            FridayTime.Cells.Add(c3);

        }
        private string GetStudentEnrollment()
        {
            string temp = "";
            SqlConnection con = new SqlConnection(strcon);
            string query = "select * from Student where SId='" + Student_ID + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                temp = dr["Enrollment"].ToString();
            }
            con.Close();
            return temp;
        }

        private string GetClassID()
        {
            string temp = "";
            SqlConnection con = new SqlConnection(strcon);
            string query = "select * from Student where Enrollment='" + Student_Enrol + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                temp = dr["ClassID"].ToString();
                Student_Section = dr["Section"].ToString();
                Shift = dr["Shift"].ToString();
            }
            con.Close();
            return temp;
        }

        private string GetClassName()
        {
            string temp = "";
            SqlConnection con = new SqlConnection(strcon);
            string query = "select * from ClassTable where ClassID='" + Class_ID + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                temp = dr["ClassName"].ToString();
            }
            con.Close();
            return temp;
        }

        private void GetTimeTable(string CourseDay)
        {
            string slot = "";

            string Teacher_ID, CourseType, Room_No, Course_ID;
            SqlConnection con = new SqlConnection(strcon);
            string query = "select * from TimeTable where ClassId='" + Class_ID + "' and Section='" + Student_Section + "' and Day='" + CourseDay + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader countrows = com.ExecuteReader();
            int i = 0;
            if (countrows.HasRows)
            {
                while (countrows.Read())
                {
                    i++;
                }
                countrows.Close();
                SqlDataReader dr = com.ExecuteReader();
                TableRow Row1 = new TableRow();
                while (dr.Read())
                {
                    if (dr["Tid"].ToString().Equals("0"))
                    {
                        Teacher_ID = dr["Aid"].ToString();
                        CourseType = "LAB";
                    }
                    else
                    {
                        Teacher_ID = dr["Tid"].ToString();
                        CourseType = "T";
                    }

                    Room_No = dr["ClassRoomNo"].ToString();
                    Course_ID = dr["CourseId"].ToString();
                    CourseInformation[count, 0] = Course_ID;

                    if (CourseDay.Equals("monday"))
                    {
                        // TableCell Cell1 = new TableCell();
                        //Cell1.Text = dr["STime"].ToString() + " - " + dr["ETime"].ToString();
                        //Cell1.CssClass = "time";
                        //MondayTime.Cells.Add(Cell1);
                        //TableCell c1 = new TableCell();
                        //c1.Text = "09:00 - 10:40";
                        //c1.CssClass = "time";
                        //TableCell c2 = new TableCell();
                        //c2.Text = "11:00 - 11:50";
                        //c2.CssClass = "time";
                        //TableCell c3 = new TableCell();
                        //c3.Text = "01:50 - 03:30";
                        //c3.CssClass = "time";

                        //MondayTime.Cells.Add(c1);
                        //MondayTime.Cells.Add(c2);
                        //MondayTime.Cells.Add(c3);

                        if (dr["Slot"].Equals("1"))
                        {

                            Cell1.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);
                        }
                        else if (dr["Slot"].Equals("2"))
                        {

                            Cell2.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);
                        }
                        else
                            Cell3.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);

                        //MondayCourses.Cells.Add(Cell1);
                        //MondayCourses.Cells.Add(Cell2);
                        //MondayCourses.Cells.Add(Cell3);
                    }

                    if (CourseDay.Equals("tuesday"))
                    {
                        //TableCell Cell1 = new TableCell();
                        //Cell1.Text = dr["STime"].ToString() + " - " + dr["ETime"].ToString();
                        //Cell1.CssClass = "time";
                        //TuesdayTime.Cells.Add(Cell1);
                        //TableCell c1 = new TableCell();
                        //c1.Text = "09:00 - 10:40";
                        //c1.CssClass = "time";
                        //TableCell c2 = new TableCell();
                        //c2.Text = "11:00 - 11:50";
                        //c2.CssClass = "time";
                        //TableCell c3 = new TableCell();
                        //c3.Text = "01:50 - 03:30";
                        //c3.CssClass = "time";
                        //TuesdayTime.Cells.Add(c1);
                        //TuesdayTime.Cells.Add(c2);
                        //TuesdayTime.Cells.Add(c3);

                        //TableCell Cell1 = new TableCell();
                        //TableCell Cell2 = new TableCell();
                        //TableCell Cell3 = new TableCell();
                        if (dr["Slot"].Equals("1"))
                        {

                            Cell4.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);
                        }
                        else if (dr["Slot"].Equals("2"))
                        {

                            Cell5.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);
                        }
                        else
                            Cell6.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);

                        // TableCell Cell2 = new TableCell();
                        // Cell2.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);
                        //TuesdayCourses.Cells.Add(Cell1);
                        //TuesdayCourses.Cells.Add(Cell2);
                        //TuesdayCourses.Cells.Add(Cell3);
                    }

                    if (CourseDay.Equals("wednesday"))
                    {
                        //TableCell Cell1 = new TableCell();
                        //Cell1.Text = dr["STime"].ToString() + " - " + dr["ETime"].ToString();
                        //Cell1.CssClass = "time";
                        //WednesdayTime.Cells.Add(Cell1);
                        //TableCell c1 = new TableCell();
                        //c1.Text = "09:00 - 10:40";
                        //c1.CssClass = "time";
                        //TableCell c2 = new TableCell();
                        //c2.Text = "11:00 - 11:50";
                        //c2.CssClass = "time";
                        //TableCell c3 = new TableCell();
                        //c3.Text = "01:50 - 03:30";
                        //c3.CssClass = "time";
                        //WednesdayTime.Cells.Add(c1);
                        //WednesdayTime.Cells.Add(c2);
                        //WednesdayTime.Cells.Add(c3);

                        //TableCell Cell1 = new TableCell();
                        //TableCell Cell2 = new TableCell();
                        //TableCell Cell3 = new TableCell();
                        if (dr["Slot"].Equals("1"))
                        {

                            Cell7.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);
                        }
                        else if (dr["Slot"].Equals("2"))
                        {

                            Cell8.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);
                        }
                        else
                            Cell9.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);

                        //TableCell Cell2 = new TableCell();
                        //Cell2.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);
                        //WednesdayCourses.Cells.Add(Cell1);
                        //WednesdayCourses.Cells.Add(Cell2);
                        //WednesdayCourses.Cells.Add(Cell3);
                    }

                    if (CourseDay.Equals("thursday"))
                    {
                        //TableCell Cell1 = new TableCell();
                        //Cell1.Text = dr["STime"].ToString() + " - " + dr["ETime"].ToString();
                        //Cell1.CssClass = "time";
                        //ThursdayTime.Cells.Add(Cell1);
                        //TableCell c1 = new TableCell();
                        //c1.Text = "09:00 - 10:40";
                        //c1.CssClass = "time";
                        //TableCell c2 = new TableCell();
                        //c2.Text = "11:00 - 11:50";
                        //c2.CssClass = "time";
                        //TableCell c3 = new TableCell();
                        //c3.Text = "01:50 - 03:30";
                        //c3.CssClass = "time";
                        //ThursdayTime.Cells.Add(c1);
                        //ThursdayTime.Cells.Add(c2);
                        //ThursdayTime.Cells.Add(c3);

                        //TableCell Cell1 = new TableCell();
                        //TableCell Cell2 = new TableCell();
                        //TableCell Cell3 = new TableCell();
                        if (dr["Slot"].Equals("1"))
                        {

                            Cell10.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);
                        }
                        else if (dr["Slot"].Equals("2"))
                        {

                            Cell11.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);
                        }
                        else
                            Cell12.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);



                        //TableCell Cell2 = new TableCell();
                        //Cell2.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);
                        //ThursdayCourses.Cells.Add(Cell1);
                        //ThursdayCourses.Cells.Add(Cell2);
                        //ThursdayCourses.Cells.Add(Cell3);
                    }

                    if (CourseDay.Equals("friday"))
                    {
                        //TableCell Cell1 = new TableCell();
                        //Cell1.Text = dr["STime"].ToString() + " - " + dr["ETime"].ToString();
                        //Cell1.CssClass = "time";
                        //FridayTime.Cells.Add(Cell1);

                        // TableCell c1 = new TableCell();
                        // c1.Text = "09:00 - 10:40";
                        // c1.CssClass = "time";
                        // TableCell c2 = new TableCell();
                        // c2.Text = "11:00 - 11:50";
                        // c2.CssClass = "time";
                        // TableCell c3 = new TableCell();
                        // c3.Text = "01:50 - 03:30";
                        // c3.CssClass = "time";
                        //FridayTime.Cells.Add(c1);
                        // FridayTime.Cells.Add(c2);
                        // FridayTime.Cells.Add(c3);

                        // TableCell Cell1 = new TableCell();
                        // TableCell Cell2 = new TableCell();
                        // TableCell Cell3 = new TableCell();
                        if (dr["Slot"].Equals("1"))
                        {

                            Cell13.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);
                        }
                        else if (dr["Slot"].Equals("2"))
                        {

                            Cell14.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);
                        }
                        else
                            Cell15.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);

                        //TableCell Cell2 = new TableCell();
                        //Cell2.Text = GetCourseInfo(Teacher_ID, CourseType, Room_No, Course_ID, count);
                        //FridayCourses.Cells.Add(Cell1);
                        //FridayCourses.Cells.Add(Cell2);
                        //FridayCourses.Cells.Add(Cell3);
                    }

                    count++;
                }
                //    if (i != 3)
                //        CreateEmptyCells(i, CourseDay);
            }
            //else
            //{
            //    CreateEmptyCells(i, CourseDay);
            //}

            con.Close();

        }

        private string GetCourseInfo(string Teacher_ID, string CourseType, string Room_No, string Course_ID, int count)
        {
            string[] CourseDetail = new string[6];
            string PrintOut = "";

            SqlConnection con = new SqlConnection(strcon);
            string query = "select * from Teacher where TId='" + Teacher_ID + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                CourseDetail[3] = " (" + dr["TAbb"].ToString() + ") ";

                CourseInformation[count, 5] = dr["TName"].ToString();
                CourseInformation[count, 6] = dr["TAbb"].ToString();
            }
            dr.Close();

            string query2 = "select * from Course where CourseId='" + Course_ID + "' ";
            SqlCommand com2 = new SqlCommand(query2, con);
            SqlDataReader cr = com2.ExecuteReader();
            cr.Read();
            if (cr.HasRows)
            {
                CourseDetail[0] = cr["CourseNo"].ToString();
                CourseDetail[1] = " (" + CourseType + ") ";
                CourseDetail[2] = cr["CourseAbb"].ToString();

                CourseInformation[count, 1] = cr["CourseNo"].ToString();
                CourseInformation[count, 2] = cr["CourseName"].ToString();
                CourseInformation[count, 3] = cr["CourseAbb"].ToString();
                CourseInformation[count, 4] = cr["CreditHours"].ToString();
            }
            con.Close();
            CourseDetail[4] = "RM ";
            CourseDetail[5] = Room_No;

            for (int i = 0; i < CourseDetail.Length; i++)
            {
                PrintOut = PrintOut + CourseDetail[i];
            }
            return PrintOut;
        }
        private void CreateEmptyCells(int i, string CourseDay)
        {
            int Cellcount = 3 - i;
            if (Cellcount == 1)
            {
                CheckRow(CourseDay);

            }

            if (Cellcount == 2)
            {
                CheckRow(CourseDay);
                CheckRow(CourseDay);
            }

            if (Cellcount == 3)
            {
                CheckRow(CourseDay);
                CheckRow(CourseDay);
                CheckRow(CourseDay);
            }


        }

        private void CheckRow(string CourseDay)
        {
            if (CourseDay.Equals("monday"))
            {
                //TableCell Cell1 = new TableCell();
                //Cell1.Text = "";
                //Cell1.CssClass = "time";
                //MondayTime.Cells.Add(Cell1);

                TableCell Cell2 = new TableCell();
                Cell2.Text = "";
                MondayCourses.Cells.Add(Cell2);
            }

            if (CourseDay.Equals("tuesday"))
            {
                //TableCell Cell1 = new TableCell();
                //Cell1.Text = "";
                //Cell1.CssClass = "time";
                //TuesdayTime.Cells.Add(Cell1);

                TableCell Cell2 = new TableCell();
                Cell2.Text = "";
                TuesdayCourses.Cells.Add(Cell2);
            }

            if (CourseDay.Equals("wednesday"))
            {
                //TableCell Cell1 = new TableCell();
                //Cell1.Text = "";
                //Cell1.CssClass = "time";
                //WednesdayTime.Cells.Add(Cell1);

                TableCell Cell2 = new TableCell();
                Cell2.Text = "";
                WednesdayCourses.Cells.Add(Cell2);
            }

            if (CourseDay.Equals("thursday"))
            {
                //TableCell Cell1 = new TableCell();
                //Cell1.Text = "";
                //Cell1.CssClass = "time";
                //ThursdayTime.Cells.Add(Cell1);

                TableCell Cell2 = new TableCell();
                Cell2.Text = "";
                ThursdayCourses.Cells.Add(Cell2);
            }

            if (CourseDay.Equals("friday"))
            {
                //TableCell Cell1 = new TableCell();
                //Cell1.Text = "";
                //Cell1.CssClass = "time";
                //FridayTime.Cells.Add(Cell1);

                TableCell Cell2 = new TableCell();
                Cell2.Text = "";
                FridayCourses.Cells.Add(Cell2);
            }
        }

        private void SortCourses(string temp, int count)
        {
            for (int i = count + 1; i < CourseInformation.GetLength(0); i++)
            {
                if (CourseInformation[i, 0] == null)
                    break;

                if (temp == CourseInformation[i, 0])
                {
                    CourseInformation[i, 0] = "-1";

                }
            }
        }

        private void RemoveDups()
        {
            int count = 0;
            for (int i = 0; i < CourseInformation.GetLength(0); i++)
            {
                if (CourseInformation[i, 0] == null)
                    break;
                SortCourses(CourseInformation[i, 0], i);
                count++;
            }

            string[,] temp = new string[count, 7];

            for (int j = 0; j < count; j++)
            {
                for (int k = 0; k < 7; k++)
                {
                    if (CourseInformation[j, 0] == "-1")
                    {
                        temp[j, 0] = null;
                    }
                    else
                    {
                        temp[j, k] = CourseInformation[j, k];
                    }
                }
            }

            //for (int a = 0; a < temp.GetLength(0); a++)
            //{
            //    for(int b = a+1; b < temp.GetLength(0); b++)
            //    {
            //        if (temp[a, 1] == null)
            //            break;
            //        if(temp[b,1].CompareTo(temp[a,1]) == -1)
            //        {
            //            string str;
            //            str = temp[a, 1];
            //            temp[a, 1] = temp[b, 1];
            //            temp[b,1] = str;
            //        }

            //    }
            //}


            GenerateCourseTable(temp);

        }

        private void GenerateCourseTable(string[,] CourseInformation)
        {
            TableRow trow;
            TableCell tcell;
            for (int i = 0; i < CourseInformation.GetLength(0); i++)
            {
                if (CourseInformation[i, 0] != null)
                {

                    trow = new TableRow();

                    tcell = new TableCell();
                    tcell.Text = CourseInformation[i, 1];
                    tcell.CssClass = "ListCell";
                    trow.Cells.Add(tcell);

                    tcell = new TableCell();
                    tcell.Text = CourseInformation[i, 2] + " (" + CourseInformation[i, 3] + ") ";
                    tcell.CssClass = "ListCell2";
                    trow.Cells.Add(tcell);

                    tcell = new TableCell();
                    tcell.Text = CourseInformation[i, 4];
                    tcell.CssClass = "ListCell3";
                    trow.Cells.Add(tcell);

                    tcell = new TableCell();
                    tcell.Text = CourseInformation[i, 5] + " (" + CourseInformation[i, 6] + ") ";
                    tcell.CssClass = "ListCell4";
                    trow.Cells.Add(tcell);

                    CoursesList.Rows.Add(trow);
                }

            }
        }

    }
}