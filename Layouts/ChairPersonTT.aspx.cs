using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UokSemesterSystem
{
    public partial class ChairPersonTT : System.Web.UI.Page
    {
        string Department_ID;
        private List<string[]> Courses = new List<string[]>();

        string strcon = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (ViewState["PreviousPage"] != null)  //Check if the ViewState 
                                                    //contains Previous page URL
            {
                Response.Redirect(ViewState["PreviousPage"].ToString());//Redirect to 
                                                                        //Previous page by retrieving the PreviousPage Url from ViewState.
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AccountType"].Equals("ChairPerson"))
            {
                Department_ID = Session["AccountId"].ToString();
            }
            else 
            {
                Department_ID = Session["d"].ToString();
            }
            
            if (!IsPostBack) //check if the webpage is loaded for the first time.
            {
                ViewState["PreviousPage"] =
            Request.UrlReferrer;//Saves the Previous page url in ViewState
            }

            GetTimeTable();
            DisplayDetail();
        }

        private void GetTimeTable()
        {
            string[] temp;
            string[] course;
            SqlConnection con = new SqlConnection(strcon);
            string query = "select * from TimeTable where DId='" + Department_ID + "'";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            int i = 0;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    temp = new string[2];
                    string Tname;
                    course = new string[11];
                    course[0] = "" + (i + 1);
                    course[1] = dr["Day"].ToString();
                    course[7] = dr["STime"].ToString() + " - " + dr["ETime"].ToString();
                    course[8] = dr["ClassRoomNo"].ToString();
                    course[4] = dr["Section"].ToString();

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

        private string GetTeacherInfo(string TID)
        {
            string temp="";
            SqlConnection con = new SqlConnection(strcon);
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
            SortListTwo();
            //SortList();
            TableRow tr; TableCell tc;
            int countmorn = 1, counteven = 1;
            string[] temp = new string[11];
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
                        tc.CssClass = "backcell";
                        tr.Cells.Add(tc);
                    }
                    if (countmorn % 2 == 0)
                        tr.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                    tr.Style.Add("border", "0.1vw solid #cccccc");
                    TeacherCoursesMorning.Rows.Add(tr);
                    countmorn++;
                }
                else
                {
                    for (j = 1; j < 9; j++)
                    {
                        tc = new TableCell();
                        tc.Text = temp[j];
                        tc.CssClass = "backcell";
                        tr.Cells.Add(tc);
                    }
                    if (counteven % 2 == 0)
                        tr.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                    tr.Style.Add("border", "0.1vw solid #cccccc");
                    TeacherCoursesEvening.Rows.Add(tr);
                    counteven++;
                }
            }
        }
    }
}