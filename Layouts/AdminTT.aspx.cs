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
    public partial class AdminTT : System.Web.UI.Page
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

            if (!IsPostBack) //check if the webpage is loaded for the first time.
            {
                ViewState["PreviousPage"] =
            Request.UrlReferrer;//Saves the Previous page url in ViewState
                
            }

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
        protected void btnBack_Click(object sender, EventArgs e)
        {
            //if (ViewState["PreviousPage"] != null)  //Check if the ViewState 
            //                                        //contains Previous page URL
            //{
            //    Response.Redirect(ViewState["PreviousPage"].ToString());//Redirect to 
            //                                                            //Previous page by retrieving the PreviousPage Url from ViewState.
            //}
            Response.Redirect("Admin.aspx");
        }
        protected void EditDepartment_Click(object sender, EventArgs e)
        {

            int DID = Convert.ToInt32((sender as Button).CommandArgument);
            Session["d"] = DID;
            Response.Redirect("ChairPersonTT.aspx");
        }
    }
}