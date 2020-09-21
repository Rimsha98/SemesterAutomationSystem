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
    public partial class Teacher : System.Web.UI.Page
    {
        private static string conString = Utilities1.GetConnectionString();
        private static SqlConnection con = new SqlConnection(conString);
        string Id, AccountID;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }
        protected void AccSetting_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccountSetting.aspx");

        }
        protected void BtnLogout_Click(object sender, EventArgs e)
        {

            Session["Id"] = null;
            Session["otherUser"] = null;
            Session["AccountId"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void GoToTimeTable(object sender, EventArgs e)
        {
            Response.Redirect("TeacherTT.aspx");
        }
        protected void GoToAttendance(object sender, EventArgs e)
        {
            Response.Redirect("AttendancePage.aspx");
        }

        protected void GoToResult(object sender, EventArgs e)
        {
            Response.Redirect("ResultPage.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["otherUser"] == null)
            {
                Id = Session["Id"].ToString();
                AccountID = Session["AccountId"].ToString();
            }
            else
            {
                Id = Session["otherUser"].ToString();
                AccountID = Session["otherAccountId"].ToString();
            }
            string department="";

            string query1 = "select * from Teacher where TId='" + AccountID + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query1, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                string path = dr["Image"].ToString();
                Session["ImagePath"] = dr["Image"].ToString();
                ProfilePic.ImageUrl = path;

                name.InnerText = dr["TName"].ToString();
                Session["NavName"] = dr["TName"].ToString();
                welcomename2.InnerText = "welcome back, " + dr["TName"].ToString() + "!";
                //depart.InnerText = dr["Department"].ToString();
                department = dr["Department"].ToString();

                contact.InnerText = dr["Conatact"].ToString();
                email.InnerText = dr["Email"].ToString();
                degree.InnerText = dr["Degree"].ToString();
                email2.InnerText = dr["Email"].ToString();
            }
            dr.Close();
            string q = "select DepartmentName from Department where DId='"+department+"'";
            SqlCommand c = new SqlCommand(q, con);
            SqlDataReader d = c.ExecuteReader();
            d.Read();
            if (d.HasRows)
            {
                depart.InnerText = d["DepartmentName"].ToString();
            }
            con.Close();
            string query2 = "select * from Login where AccoutType='" + Session["AccountType"].ToString() + "' and UserId='" + AccountID + "' ";
            con.Open();
            SqlCommand comm = new SqlCommand(query2, con);
            SqlDataReader drr = comm.ExecuteReader();
            drr.Read();
            if (drr.HasRows)
            {
                username2.InnerText = drr["UserName"].ToString();
            }
            con.Close();

        }
    }
}