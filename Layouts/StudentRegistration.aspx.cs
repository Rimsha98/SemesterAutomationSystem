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
    public partial class StudentRegistration : System.Web.UI.Page
    {
        private static string conString = Utilities1.GetConnectionString();
        private static SqlConnection con = new SqlConnection(conString);
        string dId;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (IsPostBack)
            //{
                if (Session["Enrollment"] == null) //add new record
                {
                RegisterFrontEnd.Visible = true;
                UpdateFrontEnd.Visible = false;
                if (!IsPostBack)
                {
                    getData();

                    RegisterFrontEnd.Visible = true;
                    UpdateFrontEnd.Visible = false;

                    dd_shift.Items.Insert(0, new ListItem("Choose Shift", "-1"));
                    dd_shift.Items.Insert(1, new ListItem("Evening", "0"));
                    dd_shift.Items.Insert(2, new ListItem("Morning", "2"));
                }
                string vari = dd_shift.SelectedItem.Text;
                Session["Sh"] = vari;
            }
                else //edit record
                {
                RegisterFrontEnd.Visible = false;
                UpdateFrontEnd.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey6", "UpdateRedo();", true);
                //frontendRegister.Visible = false;
                if (!IsPostBack)
                {
                    Options.Visible = false;
                    HeadingPage.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey2", "UpdateRedo();", true);
                    getData();

                    RegisterFrontEnd.Visible = false;
                    UpdateFrontEnd.Visible = true;
                    fetchStdData();
                }
                }
            //}

           
        }



        private void fetchStdData()
        {
            dd_class.Enabled = true;
            dd_section.Enabled = true;
            dd_major.Enabled = true;
            dd_shift.Enabled = true;
            string cId;
            string maj;
            string cName;
            string shift;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Student WHERE Enrollment= '" + Session["Enrollment"] + "' ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                txt_name.Text = dr["SName"].ToString();
                txt_fname.Text = dr["FatherName"].ToString();
                txt_enum.Text = dr["Enrollment"].ToString();
                txt_rnum.Text = dr["RollNumber"].ToString();
                txt_year.Text = dr["Year"].ToString();
                txt_email.Text = dr["Email"].ToString();
                   blah.Src = dr["Image"].ToString();
             //   blah.ImageUrl = dr["Image"].ToString();

                txt_email.ReadOnly = true;
                txt_enum.ReadOnly = true;
                txt_rnum.ReadOnly = true;


                dd_dept.SelectedIndex = dd_dept.Items.IndexOf(dd_dept.Items.FindByText(dr["Department"].ToString()));
                dId = dd_dept.SelectedValue.ToString();
                Session["did1"] = dId;
                cId = dr["ClassID"].ToString();
                maj = dr["Major"].ToString();
                shift = dr["Shift"].ToString();
               
                con.Close();
            }
            //dId = dd_dept.SelectedValue.ToString();
            
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM ClassTable WHERE DId= '" + dId + "'AND Shift='"+shift+"' ", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dd_class.DataSource = dt;
                dd_class.DataTextField = "ClassName";
                dd_class.DataValueField = "ClassID";
                dd_class.DataBind();
                SqlCommand cmd1 = new SqlCommand("Select * from ClassTable WHERE ClassID= '" + cId + "' ", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                dr1.Read();
                dd_class.SelectedIndex = dd_class.Items.IndexOf(dd_class.Items.FindByText(dr1["ClassName"].ToString()));
                //dd_shift.SelectedIndex = dd_shift.Items.IndexOf(dd_shift.Items.FindByText(dr1["Shift"].ToString()));
                cName = dd_class.SelectedItem.Text;
                dd_section.DataSource = dt;
                dd_section.DataTextField = "ClassSection";
               // dd_section.DataValueField = "ClassID";
                dd_section.DataBind();
                dd_section.SelectedIndex = dd_section.Items.IndexOf(dd_section.Items.FindByText(dr1["ClassSection"].ToString()));

                con.Close();


            }
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM Major WHERE DId= '" + dId + "' ", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                dd_major.DataSource = dt1;
                dd_major.DataTextField = "Mname";
                dd_major.DataValueField = "MId";
                dd_major.DataBind();
                dd_major.SelectedIndex = dd_major.Items.IndexOf(dd_major.Items.FindByText(maj));
                con.Close();
            }
            //using (SqlConnection con = new SqlConnection(conString))
            //{
            //    con.Open();
            //    SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM ClassTable WHERE DId= '" + dId + "' AND ClassName='" + cName + "' ", con);
            //    DataTable dt1 = new DataTable();
            //    sda1.Fill(dt1);
            //    dd_shift.DataSource = dt1;
            //    dd_shift.DataTextField = "Shift";
            //    dd_shift.DataValueField = "ClassID";
            //    dd_shift.DataBind();

               dd_shift.SelectedIndex = dd_shift.Items.IndexOf(dd_shift.Items.FindByText(shift));
              dd_shift.Items.Insert(0, new ListItem("Choose Shift", "-1"));

            string vari = dd_shift.SelectedItem.Text;
            Session["Sh"] = vari;

            //    con.Close();
            //}
            dd_shift.Items.Insert(1, new ListItem("Evening", "0"));
            dd_shift.Items.Insert(2, new ListItem("Morning", "2"));

            //string vari = dd_shift.SelectedItem.Text;
            //Session["Sh"] = vari;

        }

        private void getData()
        {
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
                dd_class.Items.Insert(0, new ListItem("Choose Class", "-1"));
                dd_section.Items.Insert(0, new ListItem("Choose Section", "-1"));
                dd_major.Items.Insert(0, new ListItem("Choose Major", "-1"));
                //dd_shift.Items.Insert(0, new ListItem("Choose Shift", "-1")); 
                //dd_shift.Items.Insert(1, new ListItem("Evening", "0"));
                //dd_shift.Items.Insert(2, new ListItem("Morning", "2"));


                dd_class.Enabled = false;
                dd_section.Enabled = false;
                dd_major.Enabled = false;
                dd_shift.Enabled = false;



            }

        }

        protected void dd_dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            dId = dd_dept.SelectedValue.ToString();
            Session["did1"] = dId;
            if (dd_dept.SelectedIndex == 0)
            {

            }
            else
            {
               // dd_class.Enabled = true;
                dd_shift.Enabled = true;
                dd_major.Enabled = true;
                /* using (SqlConnection con = new SqlConnection(conString))
                 {
                     con.Open();
                     SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM ClassTable WHERE DId= '" + dId + "' ", con);
                     DataTable dt = new DataTable();
                     sda.Fill(dt);
                     dd_class.DataSource = dt;
                     dd_class.DataTextField = "ClassName";
                     dd_class.DataValueField = "ClassID";
                     dd_class.DataBind();

                     dd_class.Items.Insert(0, new ListItem("Choose Class", "-1"));
                     con.Close();
                 }*/

                /*  using (SqlConnection con = new SqlConnection(conString))
                  {
                      con.Open();
                      SqlDataAdapter sda = new SqlDataAdapter("SELECT Distinct(Shift) FROM ClassTable WHERE DId= '" + dId + "' ", con);
                      DataTable dt = new DataTable();
                      sda.Fill(dt);
                      ViewState["shiftKey"] = dt;
                      dd_shift.DataSource = dt;
                      dd_shift.DataTextField = "Shift";
                     //   dd_shift.DataValueField = "ClassID";
                      dd_shift.DataBind();

                      dd_shift.Items.Insert(0, new ListItem("Choose Shift", "-1"));
                      con.Close();
                  }
                 */

               

                using (SqlConnection con1 = new SqlConnection(conString))
                    {
                        con1.Open();
                        SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM Major WHERE DId= '" + dId + "' ", con);
                        DataTable dt1 = new DataTable();
                        sda1.Fill(dt1);
                        dd_major.DataSource = dt1;
                        dd_major.DataTextField = "Mname";
                        dd_major.DataValueField = "MId";
                        dd_major.DataBind();
                        dd_major.Items.Insert(0, new ListItem("Choose Major", "-1"));
                        con.Close();
                    }

                }

            

            
        }

        protected void dd_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            dd_section.Enabled = true;
            string cName = dd_class.SelectedItem.ToString();
            dId = dd_dept.SelectedValue.ToString();
            if (dd_class.SelectedIndex == 0)
            {

            }
            else
            {
                string cId = dd_class.SelectedValue.ToString();
                Session["ccId"] = cId;

                dd_section.Enabled = true;
                //    //dd_shift.Enabled = true;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    //SqlDataAdapter sda = new SqlDataAdapter("SELECT Distinct(ClassSection) FROM ClassTable WHERE ClassName= '" + cName + "'  ", con);
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT * from ClassTable where ClassId='"+cId+"'  ", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dd_section.DataSource = dt;
                    dd_section.DataTextField = "ClassSection";
                    // dd_section.DataValueField = "ClassID";
                    dd_section.DataBind();
                    dd_section.Items.Insert(0, new ListItem("Choose Section", "-1"));
                    con.Close();
                }


            }
            //using (SqlConnection con1 = new SqlConnection(conString))
            //{
            //    con1.Open();
            //    SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM ClassTable WHERE DId= '" + dId + "' AND ClassName='" + cName + "' ", con1);
            //    DataTable dt1 = new DataTable();
            //    sda1.Fill(dt1);
            //    dd_shift.DataSource = dt1;
            //    dd_shift.DataTextField = "Shift";
            //    dd_shift.DataValueField = "ClassID";
            //    dd_shift.DataBind();
            //    dd_shift.Items.Insert(0, new ListItem("Choose Shift", "-1"));
            //    con1.Close();
            //}
            // }
        }


        protected void dd_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string q = "select ClassSection from ClassTable where ClassName='" + dd_class.SelectedItem.Text + "' and Shift='" + dd_shift.SelectedItem.Text + "'";
        }

        protected void dd_shift_SelectedIndexChanged(object sender, EventArgs e)
        {
            dd_class.Enabled = true;
            if (IsPostBack) {
                if (dd_shift.SelectedIndex != -1)
                {
                    string shift = dd_shift.SelectedItem.ToString();
                    Session["Sh"] = shift;

                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM ClassTable WHERE DId= '" + Session["did1"].ToString() + "' AND Shift='" + shift + "'", con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dd_class.DataSource = dt;
                        dd_class.DataTextField = "ClassName";
                        dd_class.DataValueField = "ClassID";
                        dd_class.DataBind();

                        dd_class.Items.Insert(0, new ListItem("Choose Class", "-1"));
                        con.Close();
                        if (Session["Sh"].Equals("Morning"))
                        {
                            dd_shift.SelectedIndex = 2;
                            
                        }
                        else
                        {
                            dd_shift.SelectedIndex = 1;
                        }
                    }
                }
            }




            //string q = "select ClassSection from ClassTable where ClassName='" + dd_class.SelectedItem.Text + "' and Shift='" + dd_shift.SelectedItem.Text + "'";
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            picError.Visible = false;
            picTypeError.Visible = false;
            List<string> emaliList = new List<string>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select * from Student", con);
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
                    //check file was submitted
                    if (file != null && file.ContentLength > 0)
                    {


                        string query = "INSERT INTO Student(SName,FatherName,Enrollment,RollNumber,Section,Year,Department,Major,ClassID,IsRepeater,SemesterNo,Shift,Email,Image) VALUES(@SName,@FatherName,@Enrollment,@RollNumber,@Section,@Year,@Department,@Major,@ClassID,@IsRepeater,@SemesterNo,@Shift,@Email,@Image)";
                        SqlCommand sqlCmd = new SqlCommand(query, con);
                        con.Open();
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.AddWithValue("@SName", txt_name.Text);
                        sqlCmd.Parameters.AddWithValue("@FatherName", txt_fname.Text);
                        sqlCmd.Parameters.AddWithValue("@Enrollment", txt_enum.Text);
                        sqlCmd.Parameters.AddWithValue("@RollNumber", txt_rnum.Text);
                        sqlCmd.Parameters.AddWithValue("@Section", dd_section.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@Year", txt_year.Text);
                        sqlCmd.Parameters.AddWithValue("@Department", dd_dept.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@ClassID", dd_class.SelectedValue.ToString());
                        sqlCmd.Parameters.AddWithValue("@Major", dd_major.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@IsRepeater", "0");
                        sqlCmd.Parameters.AddWithValue("@SemesterNo", "1");
                        sqlCmd.Parameters.AddWithValue("@Shift", dd_shift.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@Email", txt_email.Text);
                        sqlCmd.Parameters.AddWithValue("@Image", path);
                        sqlCmd.ExecuteReader();
                        con.Close();
                        lblSuc.Text = "Student record successfully inserted.";

                        txt_name.Text = "";
                        txt_fname.Text = "";
                        txt_enum.Text = "";
                        txt_rnum.Text = "";
                        txt_year.Text = "";
                        txt_email.Text = "";

                        dd_dept.SelectedItem.Selected = false;
                        dd_class.SelectedItem.Selected = false;
                        dd_section.SelectedItem.Selected = false;
                        dd_major.SelectedItem.Selected = false;
                        dd_shift.SelectedItem.Selected = false;

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "Initialize();", true);

                }
                    else
                {
                    picError.Visible = true;
                    picTypeError.Visible = false;
                }
                
            }

            else
                lblSuc.Text = "This email already belongs to a record.";
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

                        string query = "UPDATE Student SET SName=@SName,FatherName=@FatherName,Major=@Major,Section=@Section,ClassID=@ClassID,Year=@Year,Department=@Department,Image=@Image,Shift=@Shift WHERE Enrollment='" + Session["Enrollment"] + "'";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@SName", txt_name.Text);
                        sqlCmd.Parameters.AddWithValue("@FatherName", txt_fname.Text);
                        sqlCmd.Parameters.AddWithValue("@Major", dd_major.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@Section", dd_section.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@ClassID", dd_class.SelectedValue.ToString());
                        sqlCmd.Parameters.AddWithValue("@Year", txt_year.Text);
                        sqlCmd.Parameters.AddWithValue("@Department", dd_dept.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@Image", path);
                        sqlCmd.Parameters.AddWithValue("@Shift", dd_shift.SelectedItem.Text);
                        sqlCmd.ExecuteNonQuery();
                        sqlCon.Close();
                        lblSuc.Text = "Student has been Updated";
                        picError.Visible = false;
                        picTypeError.Visible = false;
                    }
            }

            else
            {
                using (SqlConnection sqlCon = new SqlConnection(conString))
                {
                    sqlCon.Open();

                    string query = "UPDATE Student SET SName=@SName,FatherName=@FatherName,Major=@Major,Section=@Section,ClassID=@ClassID,Year=@Year,Department=@Department,Shift=@Shift WHERE Enrollment='" + Session["Enrollment"] + "'";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@SName", txt_name.Text);
                    sqlCmd.Parameters.AddWithValue("@FatherName", txt_fname.Text);
                    sqlCmd.Parameters.AddWithValue("@Major", dd_major.SelectedItem.Text);
                    sqlCmd.Parameters.AddWithValue("@Section", dd_section.SelectedItem.Text);
                    sqlCmd.Parameters.AddWithValue("@ClassID", dd_class.SelectedValue.ToString());
                    sqlCmd.Parameters.AddWithValue("@Year", txt_year.Text);
                    sqlCmd.Parameters.AddWithValue("@Department", dd_dept.SelectedItem.Text);
                    sqlCmd.Parameters.AddWithValue("@Shift", dd_shift.SelectedItem.Text);
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                    lblSuc.Text = "Student has been Updated";
                    picError.Visible = false;
                    picTypeError.Visible = false;
                }

            }

        }

        protected void DisplayTeacherRegistration(object sender, EventArgs e)
        {
            Response.Redirect("TeacherRegistration.aspx");
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