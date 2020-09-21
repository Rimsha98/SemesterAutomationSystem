using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UokSemesterSystem.Classes;

namespace UokSemesterSystem
{
    public partial class TeacherRegistration : System.Web.UI.Page
    {
        private static string conString = Utilities1.GetConnectionString();
        private static SqlConnection con = new SqlConnection(conString);
        string path;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["email"] == null)
                {
                    GetData();
                    RegisterFrontEnd.Visible = true;
                    UpdateFrontEnd.Visible = false;
                }
                else
                {
                    Options.Visible = false;
                    HeadingPage.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey2", "UpdateRedo();", true);
                    GetData();
                    RegisterFrontEnd.Visible = false;
                    UpdateFrontEnd.Visible = true;
                    fetcData();
                }
            }

            if(Session["email"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey7", "UpdateRedo();", true);
            }

        }

        private void fetcData()
        {
            string dept;
            btn_update.Visible = true;


            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Teacher WHERE Email= '" + Session["email"] + "' ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                txt_name.Text = dr["TName"].ToString();
                txt_contact.Text = dr["Conatact"].ToString();
                txt_degree.Text = dr["Degree"].ToString();

                txt_email.Text = dr["Email"].ToString();
                txt_email.ReadOnly = true;

                blah.Src = dr["Image"].ToString();

                dept = dr["Department"].ToString();
                dd_dept.SelectedIndex = dd_dept.Items.IndexOf(dd_dept.Items.FindByText(dept));

                con.Close();
            }

        }

        private void GetData()
        {


            btn_update.Visible = false;
            using (SqlConnection con = new SqlConnection(conString))
            {

                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select * from Department ", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dd_dept.DataTextField = "DepartmentName";
                dd_dept.DataValueField = "DId";
                dd_dept.DataSource = dt;
                dd_dept.DataBind();
                dd_dept.Items.Insert(0, new ListItem("Choose Dept", "-1"));


            }
            con.Close();

        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            List<string> emaliList = new List<string>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select * from Teacher", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                    emaliList.Add(dt.Rows[i]["Email"].ToString());

            }
            con.Close();

            if (!emaliList.Contains(txt_email.Text))
            {
                HttpPostedFile file = Request.Files["imgInp"];
                string path = "~/Img/" + "_" + Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath(path));

                string fileExtension = Path.GetExtension(path);
                if (file != null && file.ContentLength > 0)
                {
                    //string img = Path.GetFileName(imgFile_std.PostedFile.FileName);
                    string query = "INSERT INTO Teacher(TName,Department,Conatact,Email,Degree,IsChairman,Image) VALUES(@TName,@Department,@Conatact,@Email,@Degree,@IsChairman,@Image)";
                    SqlCommand sqlCmd = new SqlCommand(query, con);
                    con.Open();
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.AddWithValue("@TName", txt_name.Text);
                    sqlCmd.Parameters.AddWithValue("@Department", dd_dept.SelectedItem.Text);
                    sqlCmd.Parameters.AddWithValue("@Conatact", txt_contact.Text);
                    sqlCmd.Parameters.AddWithValue("@Email", txt_email.Text);
                    sqlCmd.Parameters.AddWithValue("@Degree", txt_degree.Text);
                    sqlCmd.Parameters.AddWithValue("@IsChairman", "0");

                    sqlCmd.Parameters.AddWithValue("@Image", path);
                    sqlCmd.ExecuteReader();
                    con.Close();
                    Label7.Text = "Teacher record successfully inserted.";

                    txt_name.Text = "";
                    txt_contact.Text = "";
                    txt_degree.Text = "";
                    dd_dept.SelectedItem.Selected = false;
                    txt_email.Text = "";

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "Initialize();", true);
                }
                else
                {
                    picError.Visible = true;
                    picTypeError.Visible = false;
                }
            }
            else
            {
                Label7.Text = "This email already belongs to a record.";
            }

        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            HttpPostedFile file = Request.Files["imgInp"];

            if (file.FileName != "")
            {
                string path = "~/Img/" + "_" + Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath(path));

                string fileExtension = Path.GetExtension(path);
                    using (SqlConnection sqlCon = new SqlConnection(conString))
                    {
                        sqlCon.Open();

                        string query = "UPDATE Teacher SET TName=@TName,Department=@Department,Conatact=@Conatact,Degree=@Degree,Image=@Image WHERE Email='" + Session["email"] + "'";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@TName", txt_name.Text);
                        sqlCmd.Parameters.AddWithValue("@Department", dd_dept.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@Conatact", txt_contact.Text);

                        sqlCmd.Parameters.AddWithValue("@Degree", txt_degree.Text);

                        sqlCmd.Parameters.AddWithValue("@Image", path);
                        sqlCmd.ExecuteNonQuery();

                        sqlCon.Close();
                        txt_name.Text = "";
                        txt_contact.Text = "";
                        txt_degree.Text = "";
                        txt_email.Text = "";
                        dd_dept.SelectedItem.Selected = false;
                        blah.Src = "#";
                        Label7.Text = "Teacher has been Updated";
                        picError.Visible = false;
                        picTypeError.Visible = false;
                    }
            }
            else
            {
                using (SqlConnection sqlCon = new SqlConnection(conString))
                {
                    sqlCon.Open();

                    string query = "UPDATE Teacher SET TName=@TName,Department=@Department,Conatact=@Conatact,Degree=@Degree WHERE Email='" + Session["email"] + "'";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@TName", txt_name.Text);
                    sqlCmd.Parameters.AddWithValue("@Department", dd_dept.SelectedItem.Text);
                    sqlCmd.Parameters.AddWithValue("@Conatact", txt_contact.Text);

                    sqlCmd.Parameters.AddWithValue("@Degree", txt_degree.Text);


                    sqlCmd.ExecuteNonQuery();

                    sqlCon.Close();
                    txt_name.Text = "";
                    txt_contact.Text = "";
                    txt_degree.Text = "";
                    txt_email.Text = "";
                    dd_dept.SelectedItem.Selected = false;
                    blah.Src = "#";
                    Label7.Text = "Teacher has been Updated";
                    picError.Visible = false;
                    picTypeError.Visible = false;
                }

            }


            //string img = Path.GetFileName(imgFile_std.PostedFile.FileName);




        }

        protected void DisplayStudentRegistration(object sender, EventArgs e)
        {
            Response.Redirect("StudentRegistration.aspx");
        }

        protected void DummyButton_Click(object sender, EventArgs e)
        {
            blah.Src = "~/appImages/DummyUserImage.png";
            picTypeError.Visible = true;
            picTypeError.Style.Add("display", "block");
            picError.Visible = false;
        }
    }
}