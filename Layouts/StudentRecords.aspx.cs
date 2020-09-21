using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UokSemesterSystem
{
    public partial class StudentRecords : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

        private string Department_ID;
        protected void Page_Load(object sender, EventArgs e)
        {


            LoadStudentRecords();
            if (!IsPostBack)
            {
                GetDepartmentsList();
                RecordsBTN.BackColor = System.Drawing.Color.FromArgb(29, 138, 181);
            }
        }

        protected void LoadStudentRecords()
        {
            TableRow tr;
            SqlConnection con = new SqlConnection(strcon);
            string query = "SELECT * from Student";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            int count = 1;
            while (dr.Read())
            {
                tr = new TableRow();
                TableCell tc1 = new TableCell();
                tc1.Text = "" + count;
                tc1.CssClass = "backcell";
                tr.Cells.Add(tc1);

                TableCell tc2 = new TableCell();
                tc2.Text = dr["Enrollment"].ToString();
                tc2.CssClass = "backcell";
                tr.Cells.Add(tc2);

                TableCell tc3 = new TableCell();
                tc3.Text = dr["SName"].ToString();
                tc3.CssClass = "backcell";
                tr.Cells.Add(tc3);

                TableCell tc4 = new TableCell();
                tc4.Text = dr["FatherName"].ToString();
                tc4.CssClass = "backcell";
                tr.Cells.Add(tc4);

                TableCell tc5 = new TableCell();
                Button btn_edit = new Button();
                btn_edit.ID = "edit_" + count;
                btn_edit.Text = "Edit Record";
                btn_edit.CssClass = "backbtn";
                btn_edit.Click += new EventHandler(btn_edit_Click);
                tc5.Controls.Add(btn_edit);
                tr.Cells.Add(tc5);

                if (count % 2 != 0)
                    tr.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

                StudentTable.Rows.Add(tr);
                count++;
            }
            con.Close();
        }

        protected void LoadStudentData(string enrolment)
        {
            StudentData.Visible = false;
            string classID = "", section = "";

            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            string query = "SELECT * from Student where Enrollment='" + enrolment + "' ";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                student_name.Text = dr["SName"].ToString();
                father_name.Text = dr["FatherName"].ToString();
                enrolment_number.Text = dr["Enrollment"].ToString();
                seat_number.Text = dr["RollNumber"].ToString();
                student_year.Text = dr["Year"].ToString();
                student_email.Text = dr["Email"].ToString();
                blah.Src = dr["Image"].ToString();
                Session["Image"] = dr["Image"].ToString();
                department_select.SelectedIndex = department_select.Items.IndexOf(department_select.Items.FindByText(dr["Department"].ToString()));
                shift_select.SelectedIndex = shift_select.Items.IndexOf(shift_select.Items.FindByText(dr["Shift"].ToString()));
                Session["DepartID"] = GetDepartmentID();
                shift_select.Enabled = true;
                getMajors();
                major_select.SelectedIndex = major_select.Items.IndexOf(major_select.Items.FindByText(dr["Major"].ToString()));
                major_select.Enabled = true;
                GetClasses();
                class_select.Enabled = true;
                classID = dr["ClassID"].ToString();
                section = dr["Section"].ToString();
            }
            dr.Close();

            string query1 = "SELECT * from ClassTable where ClassID='" + classID + "' ";
            SqlCommand com1 = new SqlCommand(query1, con);
            SqlDataReader dr1 = com1.ExecuteReader();
            while (dr1.Read())
            {
                class_select.SelectedIndex = class_select.Items.IndexOf(class_select.Items.FindByText(dr1["ClassName"].ToString()));
            }
            con.Close();

            section_select.Enabled = true;
            GetSection();
            section_select.SelectedIndex = section_select.Items.IndexOf(section_select.Items.FindByText(section));
            Student.Visible = true;
            register_student.Visible = false;
            update_student.Visible = true;
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            InsertEditBTN.Text = "Edit Record";
            InsertEditBTN.BackColor = System.Drawing.Color.FromArgb(29, 138, 181);
            RecordsBTN.BackColor = System.Drawing.Color.FromArgb(100, 197, 235);
            saved.Visible = false;

            string[] temp = ((Button)sender).ID.Split('_');
            int id = Convert.ToInt32(temp[1]);

            major_select.Items.Clear();
            major_select.Items.Insert(0, "select major");
            class_select.Items.Clear();
            class_select.Items.Insert(0, "select class");
            section_select.Items.Clear();
            section_select.Items.Insert(0, "select section");


            LoadStudentData(StudentTable.Rows[id].Cells[1].Text);
            Session["Enrollment"] = StudentTable.Rows[id].Cells[1].Text;
            blah.Src = Session["Image"].ToString();
        }

        protected void update_student_Click(object sender, EventArgs e)
        {
            int temp = 1;
            picError.Visible = false;
            picTypeError.Visible = false;
            saved.Visible = false;

            HttpPostedFile file = Request.Files["imgInp"];
            string path0 = "~/Img/" + "_" + Path.GetFileName(file.FileName);
            file.SaveAs(Server.MapPath(path0));

            if (temp == 1)
            {

                SqlConnection con = new SqlConnection(strcon);
                string query = "UPDATE Student SET SName=@name, FatherName=@fname, Enrollment=@enrol, RollNumber=@seat, Major=@major, Section=@sec, Year=@year, Department=@depart, Email=@email, Image=@image, ClassID=@classid, SemesterNo=@semester, Shift=@shift  WHERE Enrollment='" + Session["Enrollment"] + "'";
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@name", student_name.Text);
                com.Parameters.AddWithValue("@fname", father_name.Text);
                com.Parameters.AddWithValue("@enrol", enrolment_number.Text);
                com.Parameters.AddWithValue("@seat", seat_number.Text);
                com.Parameters.AddWithValue("@major", major_select.SelectedValue);
                com.Parameters.AddWithValue("@sec", section_select.SelectedValue);
                com.Parameters.AddWithValue("@year", student_year.Text);
                com.Parameters.AddWithValue("@depart", department_select.SelectedValue);
                com.Parameters.AddWithValue("@email", student_email.Text);
                string currentImage;

                if (file != null && file.ContentLength > 0)
                {
                    currentImage = path0;
                }
                else
                {
                    currentImage = Session["Image"].ToString();
                }

                com.Parameters.AddWithValue("@image", currentImage);
                com.Parameters.AddWithValue("@classID", GetClassID());
                com.Parameters.AddWithValue("@semester", "1");
                com.Parameters.AddWithValue("@shift", shift_select.SelectedValue);
                com.ExecuteNonQuery();
                con.Close();

                ClearFields();
                InsertEditBTN.Text = "Insert Record";
                saved.Visible = true;
                register_student.Visible = true;
                update_student.Visible = false;
            }
            else
            {
                picError.Visible = true;
                picTypeError.Visible = false;
                saved.Visible = false;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }

        protected string GetClassID()
        {
            string classID = "";
            Department_ID = Session["DepartID"].ToString();
            SqlConnection con = new SqlConnection(strcon);
            string query = "SELECT * from ClassTable where ClassName='" + class_select.SelectedValue + "' and Shift= '" + shift_select.SelectedValue +
                            "' and DId= '" + Department_ID + "' and ClassSection= '" + section_select.SelectedValue + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                classID = dr["ClassID"].ToString();
            }
            con.Close();
            return classID;
        }

        protected void register_student_Click(object sender, EventArgs e)
        {
            picError.Visible = false;
            picTypeError.Visible = false;
            saved.Visible = false;

            HttpPostedFile file = Request.Files["imgInp"];
            string path0 = "~/Img/" + "_" + Path.GetFileName(file.FileName);
            file.SaveAs(Server.MapPath(path0));

            string fileExtension = Path.GetExtension(path0);
            if (file != null && file.ContentLength > 0)
            {
                SqlConnection con = new SqlConnection(strcon);
                string query = "INSERT into Student(SName, FatherName, Enrollment, RollNumber, Major, Section, Year, Department, Email, Image, ClassID, SemesterNo, Shift) " +
                               "VALUES(@name, @fname, @enrol, @seat, @major, @sec, @year, @depart, @email, @image, @classID, @semester, @shift)";
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@name", student_name.Text);
                com.Parameters.AddWithValue("@fname", father_name.Text);
                com.Parameters.AddWithValue("@enrol", enrolment_number.Text);
                com.Parameters.AddWithValue("@seat", seat_number.Text);
                com.Parameters.AddWithValue("@major", major_select.SelectedValue);
                com.Parameters.AddWithValue("@sec", section_select.SelectedValue);
                com.Parameters.AddWithValue("@year", student_year.Text);
                com.Parameters.AddWithValue("@depart", department_select.SelectedValue);
                com.Parameters.AddWithValue("@email", student_email.Text);
                com.Parameters.AddWithValue("@image", path0);
                com.Parameters.AddWithValue("@classID", GetClassID());
                com.Parameters.AddWithValue("@semester", "1");
                com.Parameters.AddWithValue("@shift", shift_select.SelectedValue);
                com.ExecuteNonQuery();
                con.Close();

                ClearFields();
                saved.Visible = true;
            }
            else
            {
                picError.Visible = true;
                picTypeError.Visible = false;
            }
        }

        protected void GetDepartmentsList()
        {
            SqlConnection con = new SqlConnection(strcon);
            string query = "SELECT * from Department";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                department_select.Items.Add(dr["DepartmentName"].ToString());
            }
            con.Close();
        }

        protected void getMajors()
        {
            SqlConnection con = new SqlConnection(strcon);
            string query = "SELECT * from Major where DId='" + Department_ID + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                major_select.Items.Add(dr["Mname"].ToString());
            }
            con.Close();
        }

        protected string GetDepartmentID()
        {
            string temp = "";
            SqlConnection con1 = new SqlConnection(strcon);
            string query1 = "SELECT * from Department where DepartmentName='" + department_select.SelectedValue + "' ";
            con1.Open();
            SqlCommand com1 = new SqlCommand(query1, con1);
            SqlDataReader dr1 = com1.ExecuteReader();
            while (dr1.Read())
            {
                Department_ID = dr1["DId"].ToString();
            }
            con1.Close();
            temp = Department_ID;
            return temp;
        }

        protected void department_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (department_select.SelectedValue == "select department")
            {
                major_select.Enabled = false;
                class_select.Enabled = false;
                section_select.Enabled = false;
                shift_select.Enabled = false;
            }
            else
            {
                shift_select.Enabled = true;
                string department = department_select.SelectedValue;

                Session["DepartID"] = GetDepartmentID();
                string temp = Session["DepartID"].ToString();
                string shift = shift_select.SelectedValue;

                major_select.Items.Clear();
                major_select.Enabled = true;
                major_select.Items.Insert(0, "select major");
                getMajors();

                class_select.Items.Clear();
                class_select.Enabled = true;
                class_select.Items.Insert(0, "select class");
                GetClasses();

                section_select.Enabled = false;


            }
            sname_error.Validate(); fname_error.Validate();
            enum_error.Validate(); snum_error.Validate();
            year_error.Validate(); email_error.Validate();
        }

        protected void GetClasses()
        {
            SqlConnection con = new SqlConnection(strcon);
            string query = "SELECT * from ClassTable where DId='" + Department_ID + "' and Shift= '" + shift_select.SelectedValue + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                class_select.Items.Add(dr["ClassName"].ToString());
            }
            con.Close();
        }

        protected void shift_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            string shift = shift_select.SelectedValue;
            Department_ID = Session["DepartID"].ToString();
            class_select.Items.Clear();
            int num;
            Int32.TryParse(Department_ID, out num);
            if (num != 0)
                class_select.Enabled = true;
            else
                class_select.Enabled = false;
            class_select.Items.Insert(0, "select class");

            SqlConnection con = new SqlConnection(strcon);
            string query = "SELECT * from ClassTable where DId='" + Department_ID + "' and Shift= '" + shift + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                class_select.Items.Add(dr["ClassName"].ToString());
            }
            con.Close();
            sname_error.Validate(); fname_error.Validate();
            enum_error.Validate(); snum_error.Validate();
            year_error.Validate(); email_error.Validate();
        }

        protected void class_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (class_select.SelectedValue == "select class")
            {
                section_select.Enabled = false;
                section_select.Items.Clear();
                section_select.Items.Insert(0, "select section");
            }
            else
            {
                string shift = shift_select.SelectedValue;
                string classname = class_select.SelectedValue;

                section_select.Items.Clear();
                section_select.Enabled = true;
                section_select.Items.Insert(0, "select section");

                Department_ID = Session["DepartID"].ToString();
                GetSection();
            }
            sname_error.Validate(); fname_error.Validate();
            enum_error.Validate(); snum_error.Validate();
            year_error.Validate(); email_error.Validate();
        }

        protected void section_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            sname_error.Validate(); fname_error.Validate();
            enum_error.Validate(); snum_error.Validate();
            year_error.Validate(); email_error.Validate();
        }

        protected void GetSection()
        {
            SqlConnection con = new SqlConnection(strcon);
            string query = "SELECT * from ClassTable where DId='" + Department_ID + "' and Shift= '" + shift_select.SelectedValue + "' and ClassName= '" + class_select.SelectedValue + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                section_select.Items.Add(dr["ClassSection"].ToString());
            }
            con.Close();
        }

        protected void major_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            sname_error.Validate(); fname_error.Validate();
            enum_error.Validate(); snum_error.Validate();
            year_error.Validate(); email_error.Validate();
        }

        protected void DummyButton_Click(object sender, EventArgs e)
        {
            blah.Src = "~/appImages/DummyUserImage.png";
            picTypeError.Visible = true;
            picTypeError.Style.Add("display", "block");
            picError.Visible = false;
        }

        protected void RecordsBTN_Click(object sender, EventArgs e)
        {
            Student.Visible = false;
            StudentData.Visible = true;
            InsertEditBTN.Text = "Insert Record";

            RecordsBTN.BackColor = System.Drawing.Color.FromArgb(29, 138, 181);
            InsertEditBTN.BackColor = System.Drawing.Color.FromArgb(100, 197, 235);
        }

        protected void ClearFields()
        {
            student_name.Text = "";
            father_name.Text = "";
            enrolment_number.Text = "";
            seat_number.Text = "";
            student_year.Text = "";
            student_email.Text = "";

            department_select.SelectedIndex = 0;
            class_select.SelectedIndex = 0;
            section_select.SelectedIndex = 0;
            shift_select.SelectedIndex = 0;
            major_select.SelectedIndex = 0;

            class_select.Enabled = false;
            section_select.Enabled = false;
            shift_select.Enabled = false;
            major_select.Enabled = false;

            blah.Src = "~/appImages/DummyUserImage.png";
        }

        protected void InsertEditBTN_Click(object sender, EventArgs e)
        {
            register_student.Visible = true;
            update_student.Visible = false;
            Student.Visible = true;
            StudentData.Visible = false;
            InsertEditBTN.Text = "Insert Record";
            saved.Visible = false;
            InsertEditBTN.BackColor = System.Drawing.Color.FromArgb(29, 138, 181);
            RecordsBTN.BackColor = System.Drawing.Color.FromArgb(100, 197, 235);

            ClearFields();

        }
    }
}