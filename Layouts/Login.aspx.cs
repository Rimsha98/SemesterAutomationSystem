using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UokSemesterSystem.Classes;

namespace UokSemesterSystem
{
    public partial class Login : System.Web.UI.Page
    {
        // string strcon = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        private static string strcon = Utilities1.GetConnectionString();
        private static SqlConnection con1 = new SqlConnection(strcon);
        Random r = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateChairPersonAccounts();
            if (Session["Id"] != null)
            {
                if (Session["AccountType"].ToString().Equals("Student"))
                {
                    Response.Redirect("Student.aspx");
                }
                else if (Session["AccountType"].ToString().Equals("Teacher"))
                {
                    Response.Redirect("Teacher.aspx");
                }
                else if (Session["AccountType"].ToString().Equals("ChairPerson"))
                {

                    Response.Redirect("ChairPerson.aspx");
                }
                else if (Session["AccountType"].ToString().Equals("Admin"))
                {
                    Response.Redirect("Admin.aspx");
                }
            }
            else
                Txtusername.Focus();
        }

        protected void Forgot_Password(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx");
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            //SqlConnection connection = new SqlConnection(strcon);
            //SqlDataAdapter sda = new SqlDataAdapter("select * from Login where UserName='" + Txtusername.Text + "' " +
            //    "and Password='" + Txtpassword.Text + "' and AccoutType='" + DDSelectUser.SelectedItem.ToString() + "'", connection);

            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    Response.Write("<script>alert('You are loged in as " + dt.Rows[0][3] + "')</script>");
            //    if (DDSelectUser.SelectedIndex == 3)
            //    {
            //        Response.Redirect("Teacher.aspx");
            //    }
            //    else if (DDSelectUser.SelectedIndex == 2)
            //    {
            //        Response.Redirect("Student.aspx");
            //    }
            //}
            //else
            //{
            //    Response.Write("Error in input");
            //}


            con1.Close();

            string query = "select * from Login where UserName='" + Txtusername.Text + "' " +
                "and Password='" + Txtpassword.Text + "' and AccoutType='" + DDSelectUser.SelectedItem.ToString() + "'";
            con1.Open();
            SqlCommand com = new SqlCommand(query, con1);
            //SqlDataAdapter sda = new SqlDataAdapter(query, con1);

            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //if (dt.Rows.Count > 0)
            SqlDataReader dr = com.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                Session["Id"] = dr["Id"].ToString();
                Session["AccountId"] = dr["UserId"].ToString();
                Session["AccountType"] = dr["AccoutType"].ToString();

                //Response.Write("<script>alert('You are loged in as " + dr.GetFieldValueRows[0][3] + "')</script>");
                Response.Write("<script>alert('You are loged in as " + DDSelectUser.SelectedItem.ToString() + "')</script>");
                if (DDSelectUser.SelectedIndex == 3)
                {
                    Response.Redirect("Admin.aspx");
                }
                else if (DDSelectUser.SelectedIndex == 2)
                {
                    Response.Redirect("ChairPerson.aspx");
                }
                else if (DDSelectUser.SelectedIndex == 1)
                {
                    Response.Redirect("Teacher.aspx");
                }
                else if (DDSelectUser.SelectedIndex == 0)
                {
                    Response.Redirect("Student.aspx");
                }

                con1.Close();
            }
            else
            {
                ErrorMsg.Visible = true;
            }
            con1.Close();
        }


        protected void CreateChairPersonAccounts()
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string cp = "ChairPerson";
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Department.DId,Department.DepartmentName,Department.TId,Teacher.Email,Teacher.TName,Teacher.TId FROM  Department INNER JOIN Teacher ON Department.TId = Teacher.TId  ", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string DId = dt.Rows[j][0].ToString();
                    string email = dt.Rows[j][3].ToString();
                    string Tname = dt.Rows[j][4].ToString();
                    string D_Name = dt.Rows[j][1].ToString();
                    string T_ID = dt.Rows[j][2].ToString();

                    string query = "select * from Login where AccoutType='" + cp + "' and UserId='" + DId + "' ";
                    con.Close();
                    con.Open();
                    SqlCommand com1 = new SqlCommand(query, con);
                    SqlDataReader dr1 = com1.ExecuteReader();
                    dr1.Read();
                    if (dr1.HasRows)
                    {

                    }
                    else
                    {
                        if (!IsPostBack)
                        {
                            string username = "", pass;
                            for (int i = 0; i < 3; i++)
                                username = String.Concat(username, r.Next(3).ToString());
                            username = String.Concat(D_Name + "_", username);
                            pass = string.Empty;
                            for (int i = 0; i < 5; i++)
                                pass = String.Concat(pass, r.Next(10).ToString());

                            sendEmail(email, Tname, username, pass, D_Name);
                            SqlConnection connection = new SqlConnection(strcon);
                            connection.Open();
                            string query1 = "insert into [dbo].[Login]([UserName],[Password],[AccoutType],[UserId]) values (@UserName,@Password,@AccoutType,@UserId)";

                            SqlCommand cmd = new SqlCommand(query1, connection);
                            cmd.Parameters.AddWithValue("@UserName", username);
                            cmd.Parameters.AddWithValue("@Password", pass);
                            cmd.Parameters.AddWithValue("@AccoutType", cp);
                            cmd.Parameters.AddWithValue("@UserId", DId);
                            cmd.ExecuteNonQuery();
                            connection.Close();

                            string q = "Update Teacher set IsChairman=@ch where TId='" + T_ID + "' ";
                            SqlConnection conn = new SqlConnection(strcon);
                            conn.Open();
                            SqlCommand c = new SqlCommand(q, conn);
                            c.Parameters.AddWithValue("@ch", "1");

                            c.ExecuteNonQuery();
                            conn.Close();


                        }
                    }
                }



                con.Close();
            }


        }

        private void sendEmail(string email, string name, string username, string password, string DepartmentName)
        {
            // ViewState["fpcode"] = (r.Next(1000, 9999).ToString("D4"));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            //smtp.Port = 25;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;

            smtp.Credentials = new System.Net.NetworkCredential("4bitdevelopers@gmail.com", "finalyearproject");
            smtp.EnableSsl = true;

            MailMessage msg = new MailMessage();
            msg.Subject = "UoK Semester Automation System | Account Activation ";

            msg.Body = "Dear " + name + ",\nYou are selected as a Chair Person of Department of " + DepartmentName
           + " \n\nYour account has been activated.\nKindly copy the following information and paste it in the required field.\n\n\t\t\t\t" + "User Name: " + username + "\n\n\t\t\t\t" + "Password: " + password + "\n";




            string to = email;
            msg.To.Add(to);

            string from = " UoK Semester Automation System <4bitdevelopers@gmail.com>";
            msg.From = new MailAddress(from);
            // smtp.EnableSsl = true;
            try
            {

                smtp.Send(msg);
                Response.Write("<script>alert('Email has been send successfully')</script> ");

            }
            catch (Exception exp)
            {
                Response.Write("<script>alert('Email not Sent')</script>");

            }


        }

    }
}