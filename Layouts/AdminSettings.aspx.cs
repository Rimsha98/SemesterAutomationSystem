using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UokSemesterSystem.Classes;


namespace UokSemesterSystem
{
    public partial class AdminSettings : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                TextBox2.Attributes["value"] = TextBox2.Text;
                TextBox3.Attributes["value"] = TextBox3.Text;
            }
        }

        protected void VerifyAdminPass_Click(object sender, EventArgs e)
        {
             Id = Session["Id"].ToString();
             AccountID = Session["AccountId"].ToString();
            if (TextBox1.Text == "")
            {
                TextBox1.BackColor = ColorTranslator.FromHtml("#f5898e");
                TextBox1.Attributes.Add("placeholder", "must fillout this field");
                VerifyPass_Admin.Visible = false;
                PSave.Visible = false;

            }
            else
            {
                TextBox3.BackColor = ColorTranslator.FromHtml("#fff");
                TextBox3.Attributes.Add("placeholder", "re-enter your password");


                string query1 = "select * from Login where Id='" + Id + "' ";
                con.Open();
                SqlCommand com = new SqlCommand(query1, con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                //if (IsPostBack)
                //{
                if (dr["Password"].ToString().Equals(TextBox1.Text))
                {
                    pwdDiv.Visible = false;
                    CPTable2.Visible = true;
                    VerifyPass_Admin.Visible = false;
                    PSave.Visible = false;

                }
                else
                {
                    VerifyPass_Admin.Visible = true;
                    PSave.Visible = false;


                }


                // }

                con.Close();
            }
        }

        protected void UpdateAdminPass_Click(object sender, EventArgs e)
        {
            p1.ForeColor = Color.Red;
              PErr.Visible = false;
                PSave.Visible = false;
                Id = Session["Id"].ToString();
            p2.ForeColor = Color.Red;
         
            bool T3=ValidateTextBox3();
            bool T2=ValidateTextBox2();
              if (T3==false || T2==false)
               {
                
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
                CPTable2.Visible = false;
                pwdDiv.Visible = true;


            }
                else
                {
                    PErr.Visible = true;
                }
            
        }

        private bool ValidateTextBox2()
        {
            bool check = false;
            if (TextBox2.Text.Equals("") || TextBox2.Text == "")
            {
                // p2.Text = "Empty field not allowed!";
                TextBox2.BackColor = ColorTranslator.FromHtml("#f5898e");
                TextBox2.Attributes.Add("placeholder", "must fillout this field");

                check = false;
            }
            else if (TextBox2.Text.Length < 6)
            {
                //RequiredFieldValidator1.ErrorMessage = "";
                p1.Text = "Password must be greater than 6";
                check = false;
            }
            else if (TextBox2.Text.Length > 15)
            {
                p1.Text = "Password must be less than 15";
                check = false;
            }
            else if (!CheckPassword(TextBox2.Text))
            {
                p1.Text = "Password must be Alphanumeric";
                check = false;

            }
            else {
                TextBox2.BackColor = ColorTranslator.FromHtml("#fff");
                TextBox2.Attributes.Add("placeholder", "must fillout this field");

                p1.Text = ""; 
                check = true; }
            return check;
        }
        private bool ValidateTextBox3()
        {
            bool check = false;
            if (TextBox3.Text.Equals("") || TextBox3.Text == "")
            {
                // p2.Text = "Empty field not allowed!";
                TextBox3.BackColor = ColorTranslator.FromHtml("#f5898e");
                TextBox3.Attributes.Add("placeholder", "must fillout this field");

                check = false;
            }
            else if (TextBox3.Text.Length < 6)
            {
                //RequiredFieldValidator1.ErrorMessage = "";
                p2.Text = "Password must be greater than 6";
                check = false;
            }
            else if (TextBox3.Text.Length > 15)
            {
                p2.Text = "Password must be less than 15";
                check = false;
            }
            else if (!CheckPassword(TextBox3.Text))
            {
                p2.Text = "Password must be Alphanumeric";
                check = false;

            }
            else {
                TextBox3.BackColor = ColorTranslator.FromHtml("#fff");
                TextBox3.Attributes.Add("placeholder", "must fillout this field");

                p2.Text = ""; 
                check = true; }
            return check;
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

                //if (TextBox2.Text.Length < 6)
                //{
                //    p1.Text = "Password must be greater than 6";
                //}
                //else if (TextBox2.Text.Length > 15)
                //{
                //    p1.Text = "Password must be less than 7";
                //}
                //else if (CheckPassword(TextBox2.Text))
                //{
                //    p1.Text = "Password must be Alphanumeric";
                //}
                //else
                //{
                //    p1.Text = "";
                //}
            

        }

        public bool CheckPassword(string password)
        {
            bool check = false;
            //string MatchEmailPattern = "(?=.{6,})[a-zA-Z0-9]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+";
           // string MatchEmailPattern = "(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9])$";
            string MatchEmailPattern = "(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9])";

            if (password != null)
            { if (Regex.IsMatch(password, MatchEmailPattern))
                    check= true;
                else check= false;
            }

            return check;

        }
    }
}