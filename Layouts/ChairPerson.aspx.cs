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
    public partial class ChairPerson : System.Web.UI.Page
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
            Response.Redirect("ChairPersonTT.aspx");
        }
        protected void GoToAttendance(object sender, EventArgs e)
        {
            Response.Redirect("AttendancePage.aspx");
        }
        protected void ViewProforma_Click(object sender, EventArgs e)
        {
            Session["isPro"] = "1";
            Response.Redirect("classesList.aspx");
        }
        protected void GoToResult(object sender, EventArgs e)
        {
            
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


            string department = "";

                string query1 = "select * from Teacher where TId='" + CpId + "' ";
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
                welcomename2.InnerText = "welcome back, " + dr["TName"].ToString() + "!";
                Session["NavName"] = dr["TName"].ToString();
                //depart.InnerText = dr["Department"].ToString();
                department = dr["Department"].ToString();

                contact.InnerText = dr["Conatact"].ToString();
                email.InnerText = dr["Email"].ToString();
                email2.InnerText = dr["Email"].ToString();
                degree.InnerText = dr["Degree"].ToString();

            }
            con.Close();
            string q = "select DepartmentName from Department where DId='" + department + "'";
            con.Open();
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