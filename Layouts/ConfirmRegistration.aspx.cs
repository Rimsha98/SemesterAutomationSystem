using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UokSemesterSystem.Classes;

namespace UokSemesterSystem
{
    public partial class ConfirmRegistration : System.Web.UI.Page
    {
        private static string conString = Utilities1.GetConnectionString();
        private static SqlConnection con = new SqlConnection(conString);
        string Id, AccountID, List_ID, AccType;
        Random r = new Random();

        protected void BtnLogin_Click(object sender, EventArgs e)
        {

            //  BtnConfirm.Enabled = true;
            ConfirmTech.Enabled = true;


            if (Session["AccountT"].Equals("Student"))
            {
                List_ID = Session["StudentId"].ToString();
                string query2 = "select SName,Email from Student where SId='" + List_ID + "' ";
                con.Open();
                SqlCommand com = new SqlCommand(query2, con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    string name = dr["SName"].ToString();
                    string Email = dr["Email"].ToString();
                    sendEmail(Email, name, uname.InnerText.ToString(), password.InnerText.ToString());
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    string query = "insert into [dbo].[Login]([UserName],[Password],[AccoutType],[UserId]) values (@UserName,@Password,@AccoutType,@UserId)";
                    string query1 = "insert into Login(UserName,Password,AccoutType,UserId) values (" + uname.InnerText.ToString() + ",'"
                        + password.InnerText.ToString() + "'," + AccType + "," + List_ID + ")";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserName", uname.InnerText.ToString());
                    cmd.Parameters.AddWithValue("@Password", password.InnerText.ToString());
                    cmd.Parameters.AddWithValue("@AccoutType", AccType);
                    cmd.Parameters.AddWithValue("@UserId", List_ID);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                con.Close();
                //Response.Write("<script>alert('Account has been created')</script>");
                //Response.Redirect("CreateAccount.aspx");

            }

            else if (Session["AccountT"].Equals("Teacher") || Session["AccountT"].Equals("ChairPerson"))
            {
                List_ID = Session["TeacherId"].ToString();

                string query2 = "select TName,Email from Teacher where TId='" + List_ID + "' ";
                con.Open();
                SqlCommand com = new SqlCommand(query2, con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    string name = dr["TName"].ToString();
                    string Email = dr["Email"].ToString();
                    sendEmail(Email, name, Label12.InnerText.ToString(), Label13.InnerText.ToString());
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    string query = "insert into [dbo].[Login]([UserName],[Password],[AccoutType],[UserId]) values (@UserName,@Password,@AccoutType,@UserId)";
                    string query1 = "insert into Login(UserName,Password,AccoutType,UserId) values (" + Label12.InnerText.ToString() + ",'"
                        + Label13.InnerText.ToString() + "'," + AccType + "," + List_ID + ")";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserName", Label12.InnerText.ToString());
                    cmd.Parameters.AddWithValue("@Password", Label13.InnerText.ToString());
                    cmd.Parameters.AddWithValue("@AccoutType", AccType);
                    if (AccType.Equals("ChairPerson"))
                    {

                        cmd.Parameters.AddWithValue("@UserId", Session["DId"].ToString());

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@UserId", List_ID);
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                con.Close();

                //   Response.Redirect("CreateAccount.aspx");
            }

            Response.Write("<script>alert('Account has been created')</script>");
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);

            Session["ConfirmReg"] = "y";
            if (ViewState["PreviousPage"] != null)  //Check if the ViewState 
                                                    //contains Previous page URL
            {
                Response.Redirect(ViewState["PreviousPage"].ToString());//Redirect to 
                                                                        //Previous page by retrieving the PreviousPage Url from ViewState.
            }
            //     Response.Redirect("RegStudent.aspx");
        }


        private void sendEmail(string email, string name, string username, string password)
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
            if (AccType.Equals("ChairPerson"))
            {
                msg.Body = "Dear " + name + ",\nYou are selected as a Chair Person of Department of " + Session["DName"].ToString()
               + " \n\nYour account has been activated.\nKindly copy the following information and paste it in the required field.\n\n\t\t\t\t" + "User Name: " + username + "\n\n\t\t\t\t" + "Password: " + password + "\n";

            }
            else
            {
                msg.Body = "Dear " + name + ",\nThank you for joining us!\n\nYour account has been activated.\nKindly copy the following information and paste it in the required field.\n\n\t\t\t\t" + "User Name: " + username + "\n\n\t\t\t\t" + "Password: " + password + "\n";
            }

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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) //check if the webpage is loaded for the first time.
            {
                ViewState["PreviousPage"] =
            Request.UrlReferrer;//Saves the Previous page url in ViewState

            }

            if (Session["AccountT"].Equals("Student"))
            {
                StdView.Visible = true;
                TechView.Visible = false;
                List_ID = Session["StudentId"].ToString();
                string query1 = "select * from Student where SId='" + List_ID + "' ";
                con.Open();
                SqlCommand com = new SqlCommand(query1, con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {

                    string path = dr["Image"].ToString();
                    ProfilePic.ImageUrl = path;

                    name.InnerText = dr["SName"].ToString();
                    fname.InnerText = dr["FatherName"].ToString();
                    department.InnerText = dr["Department"].ToString();
                    major.InnerText = dr["Major"].ToString();
                    section.InnerText = dr["Section"].ToString();
                    year.InnerText = dr["year"].ToString();
                    email.InnerText = dr["Email"].ToString();
                    shift.InnerText = dr["Shift"].ToString();
                    semester.InnerText = dr["SemesterNo"].ToString();
                    enrol.InnerText = dr["Enrollment"].ToString();
                    rolnum.InnerText = dr["RollNumber"].ToString();


                    string username = dr["SName"].ToString() + "_" + rolnum.InnerText.ToString();

                    if (!IsPostBack) //check if the webpage is loaded for the first time.
                    {
                        string pass = string.Empty;
                        for (int i = 0; i < 5; i++)
                            pass = String.Concat(pass, r.Next(10).ToString());
                        password.InnerText = pass;
                    }
                    uname.InnerText = username;

                    AccType = "Student";


                }
                con.Close();

            }

            else if (Session["AccountT"].Equals("Teacher") || Session["AccountT"].Equals("ChairPerson"))
            {
                AccType = Session["AccountT"].ToString();

                string DepatmentName = "";
                TechView.Visible = true;
                StdView.Visible = false;

                List_ID = Session["TeacherId"].ToString();
                string query1 = "select * from Teacher where TId='" + List_ID + "' ";
                con.Open();
                SqlCommand com = new SqlCommand(query1, con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {

                    string path = dr["Image"].ToString();
                    Image1.ImageUrl = path;

                    Label1.InnerText = dr["TName"].ToString();
                    DepatmentName = dr["Department"].ToString();
                    Label3.InnerText = dr["Conatact"].ToString();
                    Label4.InnerText = dr["Email"].ToString();
                    Label5.InnerText = dr["Degree"].ToString();

                    if (AccType.Equals("Teacher"))
                    {
                        if (!IsPostBack) //check if the webpage is loaded for the first time.
                        {
                            string username = "", pass = "";
                            username = dr["TName"].ToString() + "_" + dr["TID"].ToString();
                            pass = string.Empty;
                            for (int i = 0; i < 5; i++)
                                pass = String.Concat(pass, r.Next(10).ToString());
                            Label12.InnerText = username;
                            Label13.InnerText = pass;
                        }

                    }



                }
                con.Close();
                string query2 = "select * from Department where DId='" + DepatmentName + "' ";

                con.Open();
                SqlCommand co1 = new SqlCommand(query2, con);
                SqlDataReader d1 = co1.ExecuteReader();
                d1.Read();
                if (d1.HasRows)
                {
                    Label2.InnerText = d1["DepartmentName"].ToString();
                }


                con.Close();

                if (AccType.Equals("ChairPerson"))
                {
                    string query = "select * from Department where TId='" + List_ID + "' ";

                    con.Open();
                    SqlCommand com1 = new SqlCommand(query, con);
                    SqlDataReader dr1 = com1.ExecuteReader();
                    dr1.Read();
                    if (dr1.HasRows)
                    {
                        Session["DName"] = dr1["DepartmentName"].ToString();
                        Session["DId"] = dr1["DId"].ToString();
                        if (!IsPostBack) //check if the webpage is loaded for the first time.
                        {
                            string username = "", pass = "";
                            for (int i = 0; i < 3; i++)
                                username = String.Concat(username, r.Next(3).ToString());
                            username = String.Concat(dr1["DepartmentName"].ToString() + "_", username);
                            pass = string.Empty;
                            for (int i = 0; i < 5; i++)
                                pass = String.Concat(pass, r.Next(10).ToString());
                            Label12.InnerText = username;
                            Label13.InnerText = pass;
                        }

                    }
                    con.Close();
                }
            }


        }

        protected void go_back(object sender, EventArgs e)
        {
            Session["ConfirmReg"] = "";
            Response.Redirect("CreateAccount.aspx");
        }
    }
}