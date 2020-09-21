using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UokSemesterSystem.Classes;

namespace UokSemesterSystem
{
    public partial class CpProformaClassView : System.Web.UI.Page
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
        protected void ClassLink_Click(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32((sender as Button).CommandArgument);
            Session["ClassID"] = CID;
            Response.Redirect("AdminResult.aspx");
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
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string query = "Select * from ClassTable where DId= '" + AccountID + "' ";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GvClass.DataSource = dt;

                GvClass.DataBind();
                con.Close();
            }

        }
    }

}