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
    public partial class TeacherRecords : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTeacherRecords();
            if (!IsPostBack)
            {
                GetDepartmentsList();
                RecordsBTN.BackColor = System.Drawing.Color.FromArgb(29, 138, 181);
            }
        }

        protected void LoadTeacherRecords()
        {
            TableRow tr;
            SqlConnection con = new SqlConnection(strcon);
            string query = "SELECT * from Teacher";
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
                tc2.Text = dr["TName"].ToString();
                tc2.CssClass = "backcell";
                tr.Cells.Add(tc2);

                TableCell tc3 = new TableCell();
                tc3.Text = getDepartname(dr["Department"].ToString());
                tc3.CssClass = "backcell";
                tr.Cells.Add(tc3);

                TableCell tc4 = new TableCell();
                tc4.Text = dr["Email"].ToString();
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

                TeacherTable.Rows.Add(tr);
                count++;
            }
            con.Close();
        }

        private string getDepartname(string temp)
        {
            string depart_name = "";
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            string query = "select * from Department where DId='" + temp + "' ";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                depart_name = dr["DepartmentName"].ToString();
            }
            con.Close();
            return depart_name;
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            InsertEditBTN.Text = "Edit Record";
            InsertEditBTN.BackColor = System.Drawing.Color.FromArgb(29, 138, 181);
            RecordsBTN.BackColor = System.Drawing.Color.FromArgb(100, 197, 235);
            // saved.Visible = false;

            string[] temp = ((Button)sender).ID.Split('_');
            int id = Convert.ToInt32(temp[1]);

            LoadTeacherData(TeacherTable.Rows[id].Cells[3].Text);
            Session["Email"] = TeacherTable.Rows[id].Cells[3].Text;
            //blah.Src = Session["Image"].ToString();
        }

        protected void LoadTeacherData(string email)
        {
            TeacherData.Visible = false;

            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            string query = "SELECT * from Teacher where Email='" + email + "' ";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                teacher_name.Text = dr["TName"].ToString();
                blah.Src = dr["Image"].ToString();
                Session["Image"] = dr["Image"].ToString();
                department_select.SelectedIndex = department_select.Items.IndexOf(department_select.Items.FindByText(getDepartname(dr["Department"].ToString())));
                teacher_contact.Text = dr["Conatact"].ToString();
                degree.Text = dr["Degree"].ToString();
                teacher_email.Text = dr["Email"].ToString();
            }
            con.Close();

            Teacher.Visible = true;
            register_teacher.Visible = false;
            update_teacher.Visible = true;
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

        protected void department_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            tname_error.Validate(); contact_error.Validate();
            deg_error.Validate();
            email_error.Validate();
        }

        protected void register_teacher_Click(object sender, EventArgs e)
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
                string query = "INSERT into Teacher(TName, Department, Conatact, Degree, Email, Image) " +
                               "VALUES(@name, @depart, @contact, @degree, @email, @image)";
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@name", teacher_name.Text);
                com.Parameters.AddWithValue("@depart", GetDepartmentID());
                com.Parameters.AddWithValue("@contact", teacher_contact.Text);
                com.Parameters.AddWithValue("@degree", degree.Text);
                com.Parameters.AddWithValue("@email", teacher_email.Text);
                com.Parameters.AddWithValue("@image", path0);
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }

        protected void update_teacher_Click(object sender, EventArgs e)
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
                string query = "UPDATE Teacher SET TName=@name, Department=@depart, Conatact=@contact, Degree=@degree, Email=@email, Image=@image WHERE Email='" + Session["Email"] + "'";
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@name", teacher_name.Text);
                com.Parameters.AddWithValue("@depart", GetDepartmentID());
                com.Parameters.AddWithValue("@contact", teacher_contact.Text);
                com.Parameters.AddWithValue("@degree", degree.Text);
                com.Parameters.AddWithValue("@email", teacher_email.Text);
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
                com.ExecuteNonQuery();
                con.Close();

                ClearFields();
                InsertEditBTN.Text = "Insert Record";
                saved.Visible = true;
                register_teacher.Visible = true;
                update_teacher.Visible = false;

            }
            else
            {
                picError.Visible = true;
                picTypeError.Visible = false;
                saved.Visible = false;
            }
        }

        protected void ClearFields()
        {
            teacher_name.Text = "";
            degree.Text = "";
            teacher_email.Text = "";
            teacher_contact.Text = "";
            department_select.SelectedIndex = 0;
            blah.Src = "~/appImages/DummyUserImage.png";
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
                temp = dr1["DId"].ToString();
            }
            con1.Close();
            return temp;
        }

        protected void DummyButton_Click(object sender, EventArgs e)
        {
            blah.Src = "~/appImages/DummyUserImage.png";
            picTypeError.Visible = true;
            picTypeError.Style.Add("display", "block");
            picError.Visible = false;
        }

        protected void InsertEditBTN_Click(object sender, EventArgs e)
        {
            register_teacher.Visible = true;
            update_teacher.Visible = false;
            Teacher.Visible = true;
            TeacherData.Visible = false;
            InsertEditBTN.Text = "Insert Record";
            saved.Visible = false;
            InsertEditBTN.BackColor = System.Drawing.Color.FromArgb(29, 138, 181);
            RecordsBTN.BackColor = System.Drawing.Color.FromArgb(100, 197, 235);

            ClearFields();

        }

        protected void RecordsBTN_Click(object sender, EventArgs e)
        {
            Teacher.Visible = false;
            TeacherData.Visible = true;
            InsertEditBTN.Text = "Insert Record";

            RecordsBTN.BackColor = System.Drawing.Color.FromArgb(29, 138, 181);
            InsertEditBTN.BackColor = System.Drawing.Color.FromArgb(100, 197, 235);
        }
    }
}