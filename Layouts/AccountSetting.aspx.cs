using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UokSemesterSystem.Classes;

namespace UokSemesterSystem
{
    public partial class AccountSetting : System.Web.UI.Page
    {
        private static string conString = Utilities1.GetConnectionString();
        private static SqlConnection con = new SqlConnection(conString);
        string Id, AccountID;
        string numbers;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }


        protected void updatePic_Click(object sender, EventArgs e)
        {
            picError.Visible = false;
            picTypeError.Visible = false;
            picSaved.Visible = false;

            HttpPostedFile file = Request.Files["imgInp"];
            string path0 = "~/Img/" + "_" + Path.GetFileName(file.FileName);
            file.SaveAs(Server.MapPath(path0));

            string fileExtension = Path.GetExtension(path0);
            if (file != null && file.ContentLength > 0)
            {
                if (fileExtension != ".png" && fileExtension != ".jpeg" && fileExtension != ".jpg" && fileExtension != ".tiff" && (number.Text.Equals(numbers) || PITable.Visible.Equals(false)))
                {
                    picTypeError.Visible = true;
                }

                else
                {
                    Id = Session["Id"].ToString();
                    AccountID = Session["AccountId"].ToString();

                    if (Session["AccountType"].ToString().Equals("Student"))
                    {
                        string query = "select Image,Email from Student where SId='" + AccountID + "'";
                        con.Open();
                        SqlCommand com = new SqlCommand(query, con);
                        SqlDataReader dr = com.ExecuteReader();
                        dr.Read();
                        string path = dr["Image"].ToString();
                        string email = dr["Email"].ToString();
                        con.Close();

                        path = path0;
                        Session["ImagePath"] = path;
                        string query1 = "Update Student set Image=@path where SId='" + AccountID + "' ";
                        con.Open();
                        com = new SqlCommand(query1, con);
                        com.Parameters.AddWithValue("@path", path);
                        dr = com.ExecuteReader();
                        dr.Read();
                        con.Close();
                        //  picSaved.Visible = true;
                        blah.Src = path;
                        //ProfilePic.ImageUrl = path;
                        picSaved.Visible = true;
                        picError.Visible = false;
                        picTypeError.Visible = false;
                    }
                    else if (Session["AccountType"].ToString().Equals("Teacher"))
                    {
                        string query = "select Image,Email from Teacher where TId='" + AccountID + "'";
                        con.Open();
                        SqlCommand com = new SqlCommand(query, con);
                        SqlDataReader dr = com.ExecuteReader();
                        dr.Read();
                        string path = dr["Image"].ToString();
                        string email = dr["Email"].ToString();
                        con.Close();

                        path = path0;
                        Session["ImagePath"] = path;
                        string query1 = "Update Teacher set Image=@path where TId='" + AccountID + "' ";
                        con.Open();
                        com = new SqlCommand(query1, con);
                        com.Parameters.AddWithValue("@path", path);
                        dr = com.ExecuteReader();
                        dr.Read();
                        con.Close();
                        blah.Src = path;
                        picSaved.Visible = true;
                        picError.Visible = false;
                        picTypeError.Visible = false;
                    }
                    else if (Session["AccountType"].ToString().Equals("ChairPerson"))
                    {
                        string CpId = "";
                        string Q = "select * from Department where DId='" + AccountID + "'";
                        con.Open();
                        SqlCommand sq = new SqlCommand(Q, con);
                        SqlDataReader drq = sq.ExecuteReader();
                        drq.Read();
                        if (drq.HasRows)
                        {
                            CpId = drq["TId"].ToString();

                        }
                        con.Close();
                        string query = "select Image,Email from Teacher where TId='" + CpId + "'";
                        con.Open();
                        SqlCommand com = new SqlCommand(query, con);
                        SqlDataReader dr = com.ExecuteReader();
                        dr.Read();
                        string path = dr["Image"].ToString();
                        string email = dr["Email"].ToString();
                        con.Close();

                        path = path0;
                        Session["ImagePath"] = path;
                        string query1 = "Update Teacher set Image=@path where TId='" + CpId + "' ";
                        con.Open();
                        com = new SqlCommand(query1, con);
                        com.Parameters.AddWithValue("@path", path);
                        dr = com.ExecuteReader();
                        dr.Read();
                        con.Close();
                        blah.Src = path;
                        picSaved.Visible = true;
                        picError.Visible = false;
                        picTypeError.Visible = false;

                    }

                }
            }
            else
            {
                picError.Visible = true;
                picTypeError.Visible = false;
                picSaved.Visible = false;
            }

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
            if (this.IsPostBack)
            {
                newPwd.Attributes["value"] = newPwd.Text;
                newPwd1.Attributes["value"] = newPwd1.Text;
            }

            if (CPTable.Visible == true)
            {
                CPSaved.Visible = false;
            }

            uname.Enabled = false;
            Id = Session["Id"].ToString();
            AccountID = Session["AccountId"].ToString();
            if (!IsPostBack) //check if the webpage is loaded for the first time.
            {
                ViewState["PreviousPage"] =
            Request.UrlReferrer;//Saves the Previous page url in ViewState
            }


            if (!Page.IsPostBack)
            {

                if (Session["AccountType"].ToString().Equals("Student"))
                {
                    //AccSet2.Visible = false;
                    AccSet1.Visible = true;

                    PITable.Visible = false;
                    string query1 = "select * from Student where SId='" + AccountID + "' ";
                    con.Open();
                    SqlCommand com = new SqlCommand(query1, con);
                    SqlDataReader dr = com.ExecuteReader();
                    dr.Read();

                    string path = dr["Image"].ToString();
                    // ProfilePic.ImageUrl = path;
                    blah.Src = path;
                    con.Close();
                }
                else if (Session["AccountType"].ToString().Equals("Teacher"))
                {
                    ///AccSet2.Visible = false;
                    AccSet1.Visible = true;

                    PITable.Visible = true;
                    string query1 = "select * from Teacher where TId='" + AccountID + "' ";
                    con.Open();
                    SqlCommand com = new SqlCommand(query1, con);
                    SqlDataReader dr = com.ExecuteReader();
                    dr.Read();

                    string path = dr["Image"].ToString();
                    blah.Src = path;
                    con.Close();
                }
                else if (Session["AccountType"].ToString().Equals("ChairPerson"))
                {
                    //AccSet2.Visible = false;
                    AccSet1.Visible = true;


                    string CpId = "";
                    string Q = "select * from Department where DId='" + AccountID + "'";
                    con.Open();
                    SqlCommand sq = new SqlCommand(Q, con);
                    SqlDataReader drq = sq.ExecuteReader();
                    drq.Read();
                    if (drq.HasRows)
                    {
                        CpId = drq["TId"].ToString();

                    }
                    con.Close();

                    PITable.Visible = true;
                    string query1 = "select * from Teacher where TId='" + CpId + "' ";
                    con.Open();
                    SqlCommand com = new SqlCommand(query1, con);
                    SqlDataReader dr = com.ExecuteReader();
                    dr.Read();


                    string path = dr["Image"].ToString();
                    blah.Src = path;
                    con.Close();
                }
                else if (Session["AccountType"].ToString().Equals("Admin"))
                {
                    //AccSet2.Visible = true;
                    AccSet1.Visible = false;


                }
            }
        }

        private bool ValidateTextBox2()
        {
            bool check = false;
            if (newPwd.Text.Equals("") || newPwd.Text == "")
            {
                // p1.Text = "Empty field not allowed!";
                newPwd.BackColor = ColorTranslator.FromHtml("#f5898e");
                newPwd.Attributes.Add("placeholder", "must fillout this field");

                check = false;
            }
            else if (newPwd.Text.Length < 6)
            {
                //RequiredFieldValidator1.ErrorMessage = "";
                p1.Text = "Password must be greater than 6";
                check = false;
            }
            else if (newPwd.Text.Length > 15)
            {
                p1.Text = "Password must be less than 15";
                check = false;
            }
            else if (!CheckPassword(newPwd.Text))
            {
                p1.Text = "Password must be Alphanumeric";
                check = false;

            }
            else
            {
                newPwd.BackColor = ColorTranslator.FromHtml("#fff");
                newPwd.Attributes.Add("placeholder", "must fillout this field");

                p1.Text = "";
                check = true;
            }
            return check;
        }
        private bool ValidateTextBox3()
        {
            bool check = false;
            if (newPwd1.Text.Equals("") || newPwd1.Text == "")
            {
                // p2.Text = "Empty field not allowed!";
                newPwd1.BackColor = ColorTranslator.FromHtml("#f5898e");
                newPwd1.Attributes.Add("placeholder", "must fillout this field");

                check = false;
            }
            else if (newPwd1.Text.Length < 6)
            {
                //RequiredFieldValidator1.ErrorMessage = "";
                p2.Text = "Password must be greater than 6";
                check = false;
            }
            else if (newPwd1.Text.Length > 15)
            {
                p2.Text = "Password must be less than 15";
                check = false;
            }
            else if (!CheckPassword(newPwd1.Text))
            {
                p2.Text = "Password must be Alphanumeric";
                check = false;

            }
            else
            {
                newPwd1.BackColor = ColorTranslator.FromHtml("#fff");
                newPwd1.Attributes.Add("placeholder", "must fillout this field");

                p2.Text = "";
                check = true;
            }
            return check;
        }

        public bool CheckPassword(string password)
        {
            bool check = false;
            //string MatchEmailPattern = "(?=.{6,})[a-zA-Z0-9]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+";
            // string MatchEmailPattern = "(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9])$";
            string MatchEmailPattern = "(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9])";

            if (password != null)
            {
                if (Regex.IsMatch(password, MatchEmailPattern))
                    check = true;
                else check = false;
            }

            return check;

        }
        protected void updatePwd_Click(object sender, EventArgs e)
        {
            CPError.Visible = false;
            CPSaved.Visible = false;
            Id = Session["Id"].ToString();

            //if (newPwd.Text.Equals("") || newPwd1.Text.Equals(""))
            //{
            //    CPError.InnerText = "You need to enter password";
            //    CPError.Visible = false;
            //}

            bool T3 = ValidateTextBox3();
            bool T2 = ValidateTextBox2();
            if (T3 == false || T2 == false)
            {

            }
            else if (newPwd.Text.Equals(newPwd1.Text))
            {
                string query2 = "UPDATE Login SET Password=@pass WHERE Id='" + Id + "'";
                con.Open();
                SqlCommand com1 = new SqlCommand(query2, con);
                com1.Parameters.AddWithValue("@pass", newPwd.Text);
                SqlDataReader dr1 = com1.ExecuteReader();
                dr1.Read();
                con.Close();
                CPSaved.Visible = true;
                CPTable.Visible = false;
                pwdDiv.Visible = true;
                VerifyPass.Visible = false;
            }
            else
            {
                CPError.Visible = true;
                CPSaved.Visible = false;
                VerifyPass.Visible = false;
            }
        }

        /*    protected void UpdateAdminPass_Click(object sender, EventArgs e)
            {
                PErr.Visible = false;
                PSave.Visible = false;
                Id = Session["Id"].ToString();
                if (TextBox2.Text.Equals(" ") || TextBox3.Text.Equals(" "))
                {
                    PErr.InnerText = "You need to enter password";
                    PErr.Visible = true;
                }
               else if (TextBox2.Text.Equals(TextBox3.Text))
                {
                    PErr.Visible = false;
                    string query2 = "UPDATE Login SET Password=@pass WHERE Id='" + Id + "'";
                    con.Open();
                    SqlCommand com1 = new SqlCommand(query2, con);
                    com1.Parameters.AddWithValue("@pass", TextBox2.Text);
                    SqlDataReader dr1 = com1.ExecuteReader();
                    dr1.Read();
                    con.Close();

                    PSave.Visible = true;
                    pwdDiv2.Visible = true;
                    CPTable2.Visible = false;

                }
                else
                {
                    PErr.Visible = true;
                }
            } */

        protected void VerifyAdminPass_Click(object sender, EventArgs e)
        {
            /* Id = Session["Id"].ToString();
             AccountID = Session["AccountId"].ToString();
             //if (Session["AccountType"].ToString().Equals("Student"))
             //{
             //   // PITable.Visible = false;
             string query1 = "select * from Login where Id='" + Id + "' ";
             con.Open();
             SqlCommand com = new SqlCommand(query1, con);
             SqlDataReader dr = com.ExecuteReader();
             dr.Read();
             //if (IsPostBack)
             //{
             if (dr["Password"].ToString().Equals(TextBox1.Text))
             {
                 pwdDiv2.Visible = false;
                 CPTable2.Visible = true;

             }
             else
             {
                 VerifyPass_Admin.Visible = true;


             }


             // }

             con.Close(); */
        }

        protected void DummyButton_Click(object sender, EventArgs e)
        {
            if (Session["AccountType"].ToString().Equals("Student"))
            {
                string query = "select Image from Student where SId='" + AccountID + "'";
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                blah.Src = dr["Image"].ToString();
                con.Close();
            }
            else if (Session["AccountType"].ToString().Equals("Teacher"))
            {
                string query = "select Image from Teacher where TId='" + AccountID + "'";
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                blah.Src = dr["Image"].ToString();
                con.Close();

            }
            else if (Session["AccountType"].ToString().Equals("ChairPerson"))
            {

                string CpId = "";
                string Q = "select * from Department where DId='" + AccountID + "'";
                con.Open();
                SqlCommand sq = new SqlCommand(Q, con);
                SqlDataReader drq = sq.ExecuteReader();
                drq.Read();
                if (drq.HasRows)
                {
                    CpId = drq["TId"].ToString();

                }
                con.Close();
                string query = "select Image,Email from Teacher where TId='" + CpId + "'";
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                blah.Src = dr["Image"].ToString();
                con.Close();

            }
            picTypeError.Visible = true;
            picError.Visible = false;
            picSaved.Visible = false;
        }

        protected void verifyPwd_Click(object sender, EventArgs e)
        {


            Id = Session["Id"].ToString();
            AccountID = Session["AccountId"].ToString();
            //if (Session["AccountType"].ToString().Equals("Student"))
            //{
            //   // PITable.Visible = false;
            if (pwd.Text == "")
            {
                pwd.BackColor = ColorTranslator.FromHtml("#f5898e");
                pwd.Attributes.Add("placeholder", "must fillout this field");
                VerifyPass.Visible = false;
                CPSaved.Visible = false;

            }
            else
            {
                pwd.BackColor = ColorTranslator.FromHtml("#fff");
                pwd.Attributes.Add("placeholder", "kindly enter your password to proceed");

                string query1 = "select * from Login where Id='" + Id + "' ";
                con.Open();
                SqlCommand com = new SqlCommand(query1, con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                //if (IsPostBack)
                //{
                if (dr["Password"].ToString().Equals(pwd.Text))
                {
                    pwdDiv.Visible = false;
                    CPTable.Visible = true;
                    CPSaved.Visible = false;
                }
                else
                {
                    VerifyPass.Visible = true;
                    CPSaved.Visible = false;


                }

            }
            // }

            con.Close();
            //  }
        }
    }
}