using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UokSemesterSystem.Classes;

namespace UokSemesterSystem
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        private static string conString = Utilities1.GetConnectionString();
        private static SqlConnection con = new SqlConnection(conString);
        string Id, AccountID;
        Random r = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {


        }
        protected void fpNext1_Click(object sender, EventArgs e)
        {

            string AccountType = DDSelectUser.SelectedValue;
            string query = string.Empty;
            if(email.Text=="")
            {
                email.BackColor = ColorTranslator.FromHtml("#f5898e");
                email.Attributes.Add("placeholder", "must fillout this field");

            }
            else if (AccountType.Equals("Student"))
            {
                email.BackColor = ColorTranslator.FromHtml("#fff");
                email.Attributes.Add("placeholder", "must fillout this field");

                notconnected.Visible = false;
                P2.Visible = false;
                string Email = email.Text;
                query = "select * from Student where Email='" + Email + "'";
                con.Close();
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                try
                {
                    SqlDataReader dr = com.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        string Sname = dr["SName"].ToString();
                        string sid = dr["SId"].ToString();
                        if (!IsRegistered(dr["SId"].ToString(), "Student"))
                        {
                            // con.Close();
                            NotRegistered.Visible = true;
                            invalidemail.Visible = false;
                        }
                        else
                        {
                            div1.Visible = false;
                            div2.Visible = true;
                            sendEmail(Sname);
                            ViewState["fpid"] = sid;
                            P2.Visible = false;
                        }
                        con.Close();

                    }
                    else
                    {
                        NotRegistered.Visible = false;
                        invalidemail.Visible = true;
                        con.Close();
                    }

                }
                catch (Exception ex)
                {
                    displaymsg.Visible = true;
                    con.Close();
                }

            }
            else if (AccountType.Equals("Teacher"))
            {
                email.BackColor = ColorTranslator.FromHtml("#fff");
                email.Attributes.Add("placeholder", "must fillout this field");

                notconnected.Visible = false;
                P2.Visible = false;
                string Email = email.Text;
                query = "select * from Teacher where Email='" + Email + "' ";
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                try
                {
                    SqlDataReader dr = com.ExecuteReader();
                    dr.Read();
                    if(dr.HasRows)
                    {
                        string Tname = dr["TName"].ToString();
                        string tid= dr["TId"].ToString();
                        if (!IsRegistered(dr["TId"].ToString(), "Teacher"))
                        {
                           // con.Close();
                            NotRegistered.Visible = true;
                        }
                        else
                        {
                            //con.Open();
                            div1.Visible = false;
                            div2.Visible = true;
                            sendEmail(Tname);
                            ViewState["fpid"] = tid;
                            P2.Visible = false;
                            //con.Close();
                        }
                        con.Close();
                    }
                    else
                    {
                        invalidemail.Visible = true;
                        con.Close();
                    }

                }
                catch (Exception ex)
                {
                    displaymsg.Visible = true;
                    con.Close();
                }

            }
            else if (AccountType.Equals("ChairPerson"))
            {
                email.BackColor = ColorTranslator.FromHtml("#fff");
                email.Attributes.Add("placeholder", "must fillout this field");

                notconnected.Visible = false;
                P2.Visible = false;
                string Email = email.Text;
                string iscp = "1";
                query = "select * from Teacher where Email='" + Email + "' and IsChairman='" + iscp + "'";
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                try
                {
                    SqlDataReader dr = com.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        string Tname = dr["TName"].ToString();
                        string did = dr["Department"].ToString();
                        if (!IsRegistered(did,"Chairperson"))
                        {
                            NotRegistered.Visible = true;
                            invalidemail.Visible = false;
                        }
                        else
                        {
                            div1.Visible = false;
                            div2.Visible = true;
                            sendEmail(Tname);
                            ViewState["fpid"] = did;
                            P2.Visible = false;
                        }
                        con.Close();

                    }
                    else
                    {
                        NotRegistered.Visible = false;
                        invalidemail.Visible = true;
                        con.Close();
                    }

                }
                catch (Exception ex)
                {
                    displaymsg.Visible = true;
                    con.Close();
                }

            }
            else if (AccountType.Equals("Admin"))
            {
                email.BackColor = ColorTranslator.FromHtml("#fff");
                email.Attributes.Add("placeholder", "must fillout this field");

                notconnected.Visible = false;
                P2.Visible = false;
                string Email = email.Text;
                string Id = "1";
                query = "select * from Login where UserId='" + Email + "'";
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                try
                {
                    SqlDataReader dr = com.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        div1.Visible = false;
                        div2.Visible = true;
                        sendEmail(dr["UserName"].ToString());
                        // ViewState["fpid"] = dr["Department"].ToString();
                        P2.Visible = false;
                        con.Close();

                    }
                    else
                    {
                        invalidemail.Visible = true;
                        con.Close();
                    }

                }
                catch (Exception ex)
                {
                    displaymsg.Visible = true;
                    con.Close();
                }

            }


        }

        public bool IsRegistered(string userID,string AccountType) 
        {
            bool check = false;
         con.Close();
            string query = "select * from Login where UserId='" + userID + "' and AccoutType='"+AccountType+"'";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
              SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {

                check = true;
                }
            con.Close();

            return check;

        }
        private void sendEmail(string name)
        {
            ViewState["fpcode"] = (r.Next(1000, 9999).ToString("D4"));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;

            smtp.Credentials = new System.Net.NetworkCredential("4bitdevelopers@gmail.com", "finalyearproject");
            smtp.EnableSsl = true;

            MailMessage msg = new MailMessage();
            msg.Subject = "UoK Semester Automation System | Account Activation ";
            msg.Body = "Dear " + name + ",\nYour Password recovery code is Here!\nKindly copy the code and paste in required field\n\n\t\t\t\t" + ViewState["fpcode"].ToString() + "\n\nIf you face any problems during password recovery, kindly contact us:\n4bitdevelopers@gmail.com";
            string to = email.Text;
            msg.To.Add(to);

            string from = " UoK Semester System <4bitdevelopers@gmail.com>";
            msg.From = new MailAddress(from);

            try
            {
                smtp.Send(msg);
            }
            catch (Exception exp)
            {
                // Response.Write("<script>alert('You are not connected to Internet')</script>");
                div2.Visible = false;
                div1.Visible = true;
                notconnected.Visible = true;
                invalidemail.Visible = false;
                // Response.AddHeader("REFRESH", "2;URL=ForgotPassword.aspx");
            }
        }

        protected void fpNext2_Click(object sender, EventArgs e)
        {
            if(code.Text=="")
            {
                code.BackColor = ColorTranslator.FromHtml("#f5898e");
                code.Attributes.Add("placeholder", "must fillout this field");

            }
            else if (code.Text.All(char.IsDigit))
            {
                code.BackColor = ColorTranslator.FromHtml("#fff");
                code.Attributes.Add("placeholder", "Verification Code");

                if (code.Text == ViewState["fpcode"].ToString())
                {
                    div2.Visible = false;
                    div3.Visible = true;

                    pass1.Focus();
                }
                else
                {
                    displaymsg.Visible = true;
                }
            }
            else
            {
                displaymsg.Visible = true;
            }
        }
        protected void Resend_Email(object sender, EventArgs e)
        {
            displaymsg.Visible = false;
            string query;
            string Email = email.Text;
            string AccountType = DDSelectUser.SelectedValue;

            if (AccountType.Equals("Student"))
            {
                query = "select * from Student where Email='" + Email + "'";
                con.Open();
                SqlCommand com = new SqlCommand(query, con);

                try
                {
                    SqlDataReader dr = com.ExecuteReader();
                    dr.Read();
                    sendEmail(dr["SName"].ToString());
                    ViewState["fpid"] = dr["SId"].ToString();
                    P2.Visible = true;
                    con.Close();
                }
                catch (Exception ex)
                {
                    displaymsg.Visible = true;
                    con.Close();
                }
            }
            else if (AccountType.Equals("Teacher"))
            {
                query = "select * from Teacher where Email='" + Email + "'";
                con.Open();
                SqlCommand com = new SqlCommand(query, con);

                try
                {
                    SqlDataReader dr = com.ExecuteReader();
                    dr.Read();
                    sendEmail(dr["TName"].ToString());
                    ViewState["fpid"] = dr["TId"].ToString();
                    P2.Visible = true;
                    con.Close();
                }
                catch (Exception ex)
                {
                    displaymsg.Visible = true;
                    con.Close();
                }
            }
            else if (AccountType.Equals("ChairPerson"))
            {
                string iscp = "1";
                query = "select * from Teacher where Email='" + Email + "' and IsChairman='" + iscp + "'";
                con.Open();
                SqlCommand com = new SqlCommand(query, con);

                try
                {
                    SqlDataReader dr = com.ExecuteReader();
                    dr.Read();
                    sendEmail(dr["TName"].ToString());
                    ViewState["fpid"] = dr["Department"].ToString();
                    P2.Visible = true;
                    con.Close();
                }
                catch (Exception ex)
                {
                    displaymsg.Visible = true;
                    con.Close();
                }
            }
            else if (AccountType.Equals("Admin"))
            {
                // string Email = email.Text;
                string Id = "1";
                query = "select * from Login where UserId='" + Email + "'";
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                try
                {
                    SqlDataReader dr = com.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        div1.Visible = false;
                        div2.Visible = true;
                        sendEmail(dr["UserName"].ToString());
                        // ViewState["fpid"] = dr["Department"].ToString();
                        P2.Visible = false;
                        con.Close();

                    }
                    else
                    {
                        invalidemail.Visible = true;
                        con.Close();
                    }

                }
                catch (Exception ex)
                {
                    displaymsg.Visible = true;
                    con.Close();
                }

            }
        }
        protected void confirm_Click(object sender, EventArgs e)
        {
            string AccountType = DDSelectUser.SelectedValue;
            string q1 = "";
            p1.ForeColor = Color.Red;
            pp2.ForeColor = Color.Red;
            bool T3 = ValidateTextBox3();
            bool T2 = ValidateTextBox2();
            if (T3 == false || T2 == false)
            {

            }
            else 
            {
                pass1.BackColor = ColorTranslator.FromHtml("#fff");
                pass1.Attributes.Add("placeholder", "confirm password");
                pass2.BackColor = ColorTranslator.FromHtml("#fff");
                pass2.Attributes.Add("placeholder", "confirm password");


                if (pass1.Text == pass2.Text)
                {
                    con.Open();
                    if (AccountType.Equals("Admin"))
                    {

                        q1 = "UPDATE Login SET Password='" + pass1.Text + "' WHERE AccoutType='" + AccountType + "' ";

                    }
                    else
                    {
                        q1 = "UPDATE Login SET Password='" + pass1.Text + "' WHERE AccoutType='" + AccountType + "' and UserId='" + ViewState["fpid"].ToString() + "'";
                    }
                    SqlCommand com1 = new SqlCommand(q1, con);
                    SqlDataReader dr = com1.ExecuteReader();
                    dr.Read();
                    con.Close();
                    div3.Visible = false;
                    div4.Visible = true;
                    Response.AddHeader("REFRESH", "2;URL=Login.aspx");

                }
                else
                {
                    P3.Visible = true;
                }
            }
        }

        private bool ValidateTextBox2()
        {
            bool check = false;
            if (pass1.Text == "")
            {
                pass1.BackColor = ColorTranslator.FromHtml("#f5898e");
                pass1.Attributes.Add("placeholder", "must fillout this field");
            }
            else if (pass1.Text.Length < 6)
            {
                //RequiredFieldValidator1.ErrorMessage = "";
                p1.Text = "Password must be greater than 6";
                check = false;
            }
            else if (pass1.Text.Length > 15)
            {
                p1.Text = "Password must be less than 15";
                check = false;
            }
            else if (!CheckPassword(pass1.Text))
            {
                p1.Text = "Password must be Alphanumeric";
                check = false;

            }
            else
            {
                pass1.BackColor = ColorTranslator.FromHtml("#fff");
                pass1.Attributes.Add("placeholder", "must fillout this field");

                p1.Text = "";
                check = true;
            }
            return check;
        }
        private bool ValidateTextBox3()
        {
            bool check = false;
            if (pass2.Text == "")
            {
                pass2.BackColor = ColorTranslator.FromHtml("#f5898e");
                pass2.Attributes.Add("placeholder", "must fillout this field");
            }
            else if (pass2.Text.Length < 6)
            {
                //RequiredFieldValidator1.ErrorMessage = "";
                pp2.Text = "Password must be greater than 6";
                check = false;
            }
            else if (pass2.Text.Length > 15)
            {
                pp2.Text = "Password must be less than 15";
                check = false;
            }
            else if (!CheckPassword(pass2.Text))
            {
                pp2.Text = "Password must be Alphanumeric";
                check = false;

            }
            else
            {
                pass2.BackColor = ColorTranslator.FromHtml("#fff");
                pass2.Attributes.Add("placeholder", "");

                pp2.Text = "";
                check = true;
            }
            return check;
        }

        public bool CheckPassword(string password)
        {
            bool check = false;
             string MatchEmailPattern = "(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9])";

            if (password != null)
            {
                if (Regex.IsMatch(password, MatchEmailPattern))
                    check = true;
                else check = false;
            }

            return check;

        }

    }
}