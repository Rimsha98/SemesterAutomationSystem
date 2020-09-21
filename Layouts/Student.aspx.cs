using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UokSemesterSystem.Classes;
using System.Drawing;

namespace UokSemesterSystem
{
    public partial class Student : System.Web.UI.Page
    {
        private static string conString = Utilities1.GetConnectionString();
        private static SqlConnection con = new SqlConnection(conString);
        string Id, AccountID, ClassID;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
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
            Response.Redirect("StudentTT.aspx");
        }

        protected void AccSetting_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccountSetting.aspx");

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

            string enr = "";
            string query1 = "select * from Student where SId='" + AccountID + "' ";
            con.Open();
            SqlCommand com = new SqlCommand(query1, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {

                // fullname.InnerText = "@" + dr["username"].ToString();
                //if (dr["profession"].ToString() == "" || dr["profession"].ToString() == null)
                //{
                //    prof.Visible = false;
                //}
                //else
                //{
                //    prof.Visible = true;
                //    prof.InnerText = dr["profession"].ToString(); //get profession from db and send here.
                //}
                string path = dr["Image"].ToString();
                Session["ImagePath"] = dr["Image"].ToString();
                ProfilePic.ImageUrl = path;

                name.InnerText = dr["SName"].ToString();
                Session["NavName"] = dr["SName"].ToString();
                welcomename.InnerText = "welcome back, " + dr["SName"].ToString() + "!";
                fname.InnerText = dr["FatherName"].ToString();
                //if (dr["city"].ToString() != null || dr["city"].ToString() != "")
                //    city.InnerText = dr["city"].ToString();

                //if (dr["country"].ToString() != null || dr["country"].ToString() != "")
                //    country.InnerText = dr["country"].ToString();
                enrol.InnerText = dr["Enrollment"].ToString();
                rolno.InnerText = dr["RollNumber"].ToString();
                yearenrolled.InnerText = dr["Year"].ToString();
                depart.InnerText = dr["Department"].ToString();
                email1.InnerText = dr["email"].ToString();
                sectionCI.InnerText = dr["Section"].ToString();
                semesterCI.InnerText = dr["SemesterNo"].ToString();
                shiftCI.InnerText = dr["Shift"].ToString();
                ClassID = dr["ClassID"].ToString();

            }

            dr.Close();
            string query2 = "select * from Login where AccoutType='" + Session["AccountType"].ToString() + "' and UserId='" + AccountID + "' ";
            SqlCommand comm = new SqlCommand(query2, con);
            SqlDataReader drr = comm.ExecuteReader();
            drr.Read();
            if (drr.HasRows)
            {
                username1.InnerText = drr["UserName"].ToString();
            }
            drr.Close();

            string query3 = "select * from ClassTable where ClassID='" + ClassID + "' ";
            SqlCommand comcr = new SqlCommand(query3, con);
            SqlDataReader cr = comcr.ExecuteReader();
            cr.Read();
            if (cr.HasRows)
            {
                classCI.InnerText = cr["ClassName"].ToString();
            }

            con.Close();

        }

    }
}