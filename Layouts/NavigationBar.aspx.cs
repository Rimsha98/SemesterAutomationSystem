using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UokSemesterSystem.Classes;

namespace UokSemesterSystem
{
    public partial class NavigationBar : System.Web.UI.Page
    {
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AccountType"].ToString().Equals("Student"))
            {
                ProfileNav.Text = "Student Profile";
                ProfilePicNav.ImageUrl = Session["ImagePath"].ToString();
                namenav.InnerText = Session["NavName"].ToString();

            }
            else if (Session["AccountType"].ToString().Equals("Teacher"))
            {
                ProfileNav.Text = "Teacher Profile";
                ProfilePicNav.ImageUrl = Session["ImagePath"].ToString();
                namenav.InnerText = Session["NavName"].ToString();

            }
            else if (Session["AccountType"].ToString().Equals("ChairPerson"))
            {
                ProfileNav.Text = "Chairman Profile";
                ProfilePicNav.ImageUrl = Session["ImagePath"].ToString();
                namenav.InnerText = Session["NavName"].ToString();

            }

            if (!IsPostBack)
            {
                ViewState["CurrentUrl"] = Request.Url;
                string temp = ViewState["CurrentUrl"].ToString();


            }


        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session["Id"] = null;
            Session["otherUser"] = null;
            Session["AccountId"] = null;
            Response.Redirect("Login.aspx");

        }

        protected void BtnTimeTable_Click(object sender, EventArgs e)
        {
           
            if (Session["Id"] != null)
            {
                if (Session["AccountType"].ToString().Equals("Student"))
                {
                    Response.Redirect("StudentTT.aspx");
                
                }
                else if (Session["AccountType"].ToString().Equals("Teacher"))
                {
                    Response.Redirect("TeacherTT.aspx");
                 
                }
                else if (Session["AccountType"].ToString().Equals("ChairPerson"))
                {
                    Response.Redirect("CreateTimeTable.aspx");
                    
                }
            }
        }

        protected void BtnAttendance_Click(object sender, EventArgs e)
        {
            
            if (Session["Id"] != null)
            {
                if (Session["AccountType"].ToString().Equals("Student"))
                {
                    Response.Redirect("StudentAttendance.aspx");
                }
                else if (Session["AccountType"].ToString().Equals("Teacher"))
                {
                    Response.Redirect("AttendancePage.aspx");
                }
                else if (Session["AccountType"].ToString().Equals("ChairPerson"))
                {
                    Session["classesListLoaded"] = 0;
                    Session["isPro"] = 0;
                    Response.Redirect("classesList.aspx");
                }
            }
        }

        protected void BtnResult_Click(object sender, EventArgs e)
        {

            if (Session["Id"] != null)
            {
                if (Session["AccountType"].ToString().Equals("Student"))
                {
                    Response.Redirect("StdViewResult.aspx");
                }
                else if (Session["AccountType"].ToString().Equals("Teacher"))
                {
                    Response.Redirect("ResultPage.aspx");
                }
                else if (Session["AccountType"].ToString().Equals("ChairPerson"))
                {
                    Session["classesListLoaded"] = 1;
                    Response.Redirect("ClassPageCP.aspx");
                }
            }
        }

        protected void BtnSettings_Click(object sender, EventArgs e)
        {
            if (Session["Id"] != null)
            {
                Response.Redirect("AccountSetting.aspx");
            }
        }

        protected void BtnProfile_Click(object sender, EventArgs e)
        {
            if (Session["Id"] != null)
            {
                if (Session["AccountType"].ToString().Equals("Student"))
                {
                    Response.Redirect("Student.aspx");
                    ProfileNav.BackColor = System.Drawing.Color.FromArgb(29, 138, 181);
                }
                else if (Session["AccountType"].ToString().Equals("Teacher"))
                {
                    Response.Redirect("Teacher.aspx");
                }
                else if (Session["AccountType"].ToString().Equals("ChairPerson"))
                {
                    Response.Redirect("ChairPerson.aspx");
                }
            }
        }
    }
}