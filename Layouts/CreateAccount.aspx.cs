using System;
using System.Collections.Generic;
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
    public partial class CreateAccount : System.Web.UI.Page
    {
        private static string conString = Utilities1.GetConnectionString();
        private static SqlConnection con = new SqlConnection(conString);
        string Id, AccountID,listId;
        Random r = new Random();
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (ViewState["PreviousPage"] != null)  //Check if the ViewState 
                                                    //contains Previous page URL
            {
                Response.Redirect(ViewState["PreviousPage"].ToString());//Redirect to 
                                                                        //Previous page by retrieving the PreviousPage Url from ViewState.
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
            try
            {
                if (!Session["ConfirmReg"].Equals(""))
                {
                    ConfirmMsg.Text = "Account has been created successfully";
                }
                else
                {
                    ConfirmMsg.Text = "";
                }
            }
            catch(Exception ex)
            {
                ConfirmMsg.Text = "";
            }
            
            if (!IsPostBack) //check if the webpage is loaded for the first time.
            {
                ViewState["PreviousPage"] =
            Request.UrlReferrer;//Saves the Previous page url in ViewState
            }

            if (!Page.IsPostBack)
            {
                try
                {
                    if (Session["Index"].Equals("0"))
                    {
                        DDSelectUser.SelectedIndex = 0;

                        stdView.Visible = true;
                        TeacherView.Visible = false;

                        using (SqlConnection con = new SqlConnection(conString))
                        {
                            con.Open();
                            SqlDataAdapter sda = new SqlDataAdapter("Select * from Student ", con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            GvStudent.DataSource = dt;

                            GvStudent.DataBind();

                            con.Close();

                        }
                    }
                    if (Session["Index"].Equals("1"))
                    {
                        DDSelectUser.SelectedIndex = 1;

                        stdView.Visible = false;
                        CPView.Visible = true;
                        TeacherView.Visible = false;
                        Label2.Visible = false;
                        using (SqlConnection con = new SqlConnection(conString))
                        {
                            string cp = "ChairPerson";
                            con.Open();
                            SqlDataAdapter sda = new SqlDataAdapter("SELECT Department.DId,Department.DepartmentName,Department.TId,Teacher.TName,Teacher.TId FROM  Department INNER JOIN Teacher ON Department.TId = Teacher.TId  ", con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            GVCP.DataSource = dt;

                            GVCP.DataBind();




                            con.Close();
                        }
                    }
                    if (Session["Index"].Equals("2"))
                    {
                        DDSelectUser.SelectedIndex = 2;

                        stdView.Visible = false;
                        CPView.Visible = false;
                        TeacherView.Visible = true;
                        Label2.Visible = true;
                        using (SqlConnection con = new SqlConnection(conString))
                        {
                            con.Open();
                            string query = "SELECT Department.DId,Department.DepartmentName,Teacher.TName,Teacher.TId,Teacher.Image,Teacher.Conatact,Teacher.Email,Teacher.Degree,Teacher.Department FROM Teacher  INNER JOIN Department ON Teacher.Department = Department.DId  ";
                            // string query = "SELECT Department.DId,Department.DepartmentName,Teacher.TName,Teacher.TId,Teacher.Image,Teacher.Conatact,Teacher.Email,Teacher.Degree,Teacher.Department FROM Teacher  INNER JOIN Department ON Teaccher.Department = Department.DId  ";
                            SqlDataAdapter sda = new SqlDataAdapter(query, con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            GVTeacher.DataSource = dt;

                            GVTeacher.DataBind();
                            con.Close();
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Session["Index"] = "";
                    DDSelectUser.SelectedIndex = 0;

                    stdView.Visible = true;
                    TeacherView.Visible = false;

                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        con.Open();

                        SqlDataAdapter sda = new SqlDataAdapter("Select * from Student ", con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        GvStudent.DataSource = dt;

                        if (dt.Rows[0][13].ToString().Equals("1"))
                        {

                        }
                        GvStudent.DataBind();

                        con.Close();

                    }
                }
            }
            //if(Page.IsPostBack)
            //{
              
            //}

        }

        protected void StdLink_Click(object sender, EventArgs e)
        {
            Session["AccountT"] = "Student";
            int stdID = Convert.ToInt32((sender as Button).CommandArgument);
            Session["StudentId"] = stdID;
            string AccountType = "Student";
            string query1 = "select * from Login where AccoutType='" + AccountType + "' and UserId='" + stdID + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query1, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {

                //Response.Write("<script>alert('This user already registered ')</script>");
                ConfirmMsg.Text = "This user is already registered";
                con.Close();
            }
            else
            {
                con.Close();
                Response.Redirect("ConfirmRegistration.aspx");
            }



        }

        protected void DDSelectUser_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DDSelectUser.SelectedIndex == 0)
            {
                Session["Index"] = "0";
                ConfirmMsg.Text = "";
                stdView.Visible = true;
                CPView.Visible = false;
                TeacherView.Visible = false;
                Label2.Visible = true;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select * from Student ", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GvStudent.DataSource = dt;
                    if (dt.Rows[0][13].ToString().Equals("1"))
                    {

                    }
                    GvStudent.DataBind();
                    con.Close();
                }
            }
            else if (DDSelectUser.SelectedIndex == 2)
            {
                Session["Index"] = "2";
                ConfirmMsg.Text = "";
                stdView.Visible = false;
                CPView.Visible = false;
                TeacherView.Visible = true;
                Label2.Visible = true;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string query = "SELECT Department.DId,Department.DepartmentName,Teacher.TName,Teacher.TId,Teacher.Image,Teacher.Conatact,Teacher.Email,Teacher.Degree,Teacher.Department FROM Teacher  INNER JOIN Department ON Teacher.Department = Department.DId  ";
                    // string query = "SELECT Department.DId,Department.DepartmentName,Teacher.TName,Teacher.TId,Teacher.Image,Teacher.Conatact,Teacher.Email,Teacher.Degree,Teacher.Department FROM Teacher  INNER JOIN Department ON Teaccher.Department = Department.DId  ";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GVTeacher.DataSource = dt;

                    GVTeacher.DataBind();
                    con.Close();
                }
            }
            else if (DDSelectUser.SelectedIndex == 1)
            {
                Session["Index"] = "1";
                ConfirmMsg.Text = "";
                stdView.Visible = false;
                CPView.Visible = true;
                TeacherView.Visible = false;
                Label2.Visible = false;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string cp = "ChairPerson";
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Department.DId,Department.DepartmentName,Department.TId,Teacher.TName,Teacher.TId FROM  Department INNER JOIN Teacher ON Department.TId = Teacher.TId  ", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GVCP.DataSource = dt;

                    GVCP.DataBind();
                    
                    
                   
                   
                    con.Close();
                }
            }
        }

        
        protected void TechLink_Click(object sender, EventArgs e)
        {
            Session["AccountT"] = "Teacher";
            int TID = Convert.ToInt32((sender as Button).CommandArgument);
            Session["TeacherId"] = TID;
            string AccountType = "Teacher";
            string query1 = "select * from Login where AccoutType='" + AccountType + "' and UserId='" + TID + "' ";
            con.Close();
            con.Open();
            SqlCommand com = new SqlCommand(query1, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                // Response.Write("<script>alert('This user already register ')</script>");
                ConfirmMsg.Text = "This user is already registered";
                con.Close();
            }
            else
            {
                con.Close();
                Response.Redirect("ConfirmRegistration.aspx");
            }



        }
    protected void ChairPerson_Click(object sender, EventArgs e)
        {
            Session["CPupdate"] = "";
            Session["AccountT"] = "ChairPerson";
            int DId = Convert.ToInt32((sender as Button).CommandArgument);
            string AccountType = "ChairPerson";
            string query1 = "select * from Login where AccoutType='" + AccountType + "' and UserId='" + DId + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query1, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                // Response.Write("<script>alert('This user already register ')</script>");
                ConfirmMsg.Text = "This user is already registered";
                con.Close();
            }
            else
            {
                con.Close();
                string q1 = "select TId from Department where DId='" + DId + "'  ";
                con.Open();
                SqlCommand c = new SqlCommand(q1, con);
                SqlDataReader d = c.ExecuteReader();
                d.Read();
                if (d.HasRows)
                {
                    Session["TeacherId"] = d["TId"].ToString();
                    Response.Redirect("ConfirmRegistration.aspx");
                }


               
            }



        }

        protected void Save_Click(object sender, EventArgs e)
        {
            if (TeachList.SelectedIndex == 0)
            {
                
                Label1.InnerText = "you need to select chairperson";
            }
            else
            {
                Label1.InnerText = "";
                if (!Session["Prev_TID"].ToString().Equals(Session["listId"].ToString()))
                {
                    string query2 = "select TName,Email from Teacher where TId='" + Session["listId"].ToString() + "' ";
                    con.Open();
                    SqlCommand com = new SqlCommand(query2, con);
                    SqlDataReader dr = com.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        string name = dr["TName"].ToString();
                        string Email = dr["Email"].ToString();
                        string username = "", pass = "";
                        if (IsPostBack)
                        {
                            for (int i = 0; i < 3; i++)
                                username = String.Concat(username, r.Next(3).ToString());
                            username = String.Concat(Dname.InnerText + "_", username);
                            uname.InnerText = username;
                            pass = string.Empty;
                            for (int i = 0; i < 5; i++)
                                pass = String.Concat(pass, r.Next(10).ToString());

                        }

                        sendEmail(Email, name, username, pass, Session["Dname"].ToString());

                        SqlConnection con = new SqlConnection(conString);
                        con.Open();
                        //string query = "Update [dbo].[Login]([UserName],[Password]) values (@UserName,@Password)";
                        string query = "Update Login set UserName=@UserName,Password=@Password where Id='" + Session["Log_Id"].ToString() + "' ";

                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@UserName", username);
                        cmd.Parameters.AddWithValue("@Password", pass);

                        cmd.ExecuteNonQuery();
                        con.Close();

                        string query1 = "Update Department set TId=@Id where DId='" + Session["DId"].ToString() + "' ";
                        con.Open();
                        SqlCommand com1 = new SqlCommand(query1, con);
                        com1.Parameters.AddWithValue("@Id", Session["listId"].ToString());
                        SqlDataReader dr1 = com1.ExecuteReader();
                        dr1.Read();
                        con.Close();

                        string q2 = "Update Teacher set IsChairman=@ch where TId='" + Session["listId"].ToString() + "' ";
                        con.Open();
                        SqlCommand com2 = new SqlCommand(q2, con);
                        com2.Parameters.AddWithValue("@ch", "1");
                        SqlDataReader dr2 = com2.ExecuteReader();
                        dr2.Read();
                        con.Close();


                        string q3 = "Update Teacher set IsChairman=@ch where TId='" + Session["Prev_TID"].ToString() + "' ";
                        con.Open();
                        SqlCommand com3 = new SqlCommand(q3, con);
                        com3.Parameters.AddWithValue("@ch", "0");
                        SqlDataReader dr3 = com3.ExecuteReader();
                        dr3.Read();
                        con.Close();
                        Session["Prev_TID"] = Session["listId"].ToString();
                        // MessageBox.Show(this.Page, "Data Updated Successfully");

                    }
                    con.Close();
                }
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

        protected void Canel_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            maindiv.Visible = true;
            con.Close();
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Department.DId,Department.DepartmentName,Department.TId,Teacher.TName,Teacher.TId FROM  Department INNER JOIN Teacher ON Department.TId = Teacher.TId  ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GVCP.DataSource = dt;

            GVCP.DataBind();
        }

        protected void GvStudent_PreRender(object sender, EventArgs e)
        {
            Label2.Text = "Displaying Page " + (GvStudent.PageIndex + 1).ToString() + " of " + GvStudent.PageCount.ToString();
        }

        protected void GvStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvStudent.PageIndex = e.NewPageIndex;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select * from Student ", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                GvStudent.DataSource = dt;

                if (dt.Rows[0][13].ToString().Equals("1"))
                {

                }
                GvStudent.DataBind();

                con.Close();

            }
            // GvStudent.DataBind();

            ConfirmMsg.Text = "";
        }

        protected void GVTeacher_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVTeacher.PageIndex = e.NewPageIndex;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string query = "SELECT Department.DId,Department.DepartmentName,Teacher.TName,Teacher.TId,Teacher.Image,Teacher.Conatact,Teacher.Email,Teacher.Degree,Teacher.Department FROM Teacher  INNER JOIN Department ON Teacher.Department = Department.DId  ";
                // string query = "SELECT Department.DId,Department.DepartmentName,Teacher.TName,Teacher.TId,Teacher.Image,Teacher.Conatact,Teacher.Email,Teacher.Degree,Teacher.Department FROM Teacher  INNER JOIN Department ON Teaccher.Department = Department.DId  ";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GVTeacher.DataSource = dt;

                GVTeacher.DataBind();
                con.Close();
            }
            ConfirmMsg.Text = "";

        }

        protected void GVTeacher_PreRender(object sender, EventArgs e)
        {
            Label2.Text = "Displaying Page " + (GVTeacher.PageIndex + 1).ToString() + " of " + GVTeacher.PageCount.ToString();

        }

        protected void EditDepartment_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            maindiv.Visible = false;
            Session["AccountT"] = "ChairPerson";
            int DId = Convert.ToInt32((sender as Button).CommandArgument);
            string TId = "";
           /// Session["DId"] = DId;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string query = "Select * from Department where DId='" + DId + "'";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    Dname.InnerText = dr["DepartmentName"].ToString();
                    Session["Dname"]= dr["DepartmentName"].ToString();
                    TId = dr["TId"].ToString();
                    Session["Prev_TID"] = TId;
                    Session["listId"] = TId;
                }

                con.Close();
            }

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string query = "Select * from Teacher where Department='" + DId + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                TeachList.DataSource = dt;
                TeachList.DataBind();
                TeachList.DataTextField = "TName";
                TeachList.DataValueField = "TId";
                TeachList.DataBind();

                TeachList.Items.Insert(0, new ListItem("Select", "NA"));
                con.Close();
                
                
                    con.Open();
                string q = "Select * from Teacher where Department='" + DId + "' and TId= '" + TId + "'";
                SqlCommand com = new SqlCommand(q, con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    TeachList.SelectedIndex = TeachList.Items.IndexOf(TeachList.Items.FindByText(dr["TName"].ToString()));
                    email.InnerText = dr["Email"].ToString();
                    Img.ImageUrl = dr["Image"].ToString();
                    contact.InnerText = dr["Conatact"].ToString();
                  
                }
                con.Close();


                con.Open();
                string q1 = "Select * from Login where AccoutType='" + "ChairPerson" + "' and UserId= '" + DId + "'";
                SqlCommand c = new SqlCommand(q1, con);
                SqlDataReader d = c.ExecuteReader();
                d.Read();
                {
                    uname.InnerText = d["UserName"].ToString();
                    Session["Log_Id"] = d["Id"].ToString();
                    Session["DId"] = d["UserId"].ToString();
                }
                con.Close();

            }


       //     Response.Redirect("DepartmentReg.aspx");




        }

        protected void GetID(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                listId = TeachList.SelectedValue;
                Session["listId"] = listId;
                if (listId.Equals("NA"))
                {
                }
                else
                {
                    string q = "Select * from Teacher where TId= '" + listId + "'";
                    con.Open();
                    SqlCommand com = new SqlCommand(q, con);
                    SqlDataReader dr = com.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //              TeachList.SelectedIndex = TeachList.Items.IndexOf(TeachList.Items.FindByText(dr["TName"].ToString()));
                        email.InnerText = dr["Email"].ToString();
                        Img.ImageUrl = dr["Image"].ToString();
                        contact.InnerText = dr["Conatact"].ToString();

                    }
                    con.Close();
                  
                   
                }
            }

            
        }

     

    }
}