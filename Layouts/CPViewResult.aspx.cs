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
    public partial class CPViewResult : System.Web.UI.Page
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

            if (!Page.IsPostBack)
            {


                DepartmentView.Visible = true;
                ClassView.Visible = false;

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    //string query = "Select * from Department ";
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Department.DId,Department.DepartmentName,Department.TId,Teacher.TName,Teacher.TId FROM  Department INNER JOIN Teacher ON Department.TId = Teacher.TId  ", con);

                    //SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GVCP.DataSource = dt;

                    GVCP.DataBind();
                    con.Close();
                }

            }

        }

        protected void EditLink_Click(object sender, EventArgs e)
        {

            List<string> classId = new List<string>();
            List<string> appId = new List<string>();
            int DID = Convert.ToInt32((sender as Button).CommandArgument);
            Session["DID"] = DID;

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string query = "Select * from ClassTable where DId= '" + DID + "' ";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                        classId.Add(dr["ClassID"].ToString());

                }
                con.Close();

                for (int i = 0; i < classId.Count; i++)
                {
                    con.Open();
                    query = "Select * from ApprovalTable where ChairPApp='" + 1 + "' and ClassId='" + classId[i] + "'";
                    com = new SqlCommand(query, con);
                    dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        appId.Add(dr["ClassId"].ToString());

                    }

                    con.Close();
                }

                int count = 0;
                DataTable dt = new DataTable();
                dt.Columns.Add("ClassName", typeof(string));
                dt.Columns.Add("ClassSection", typeof(string));
                dt.Columns.Add("Shift", typeof(string));
                dt.Columns.Add("ClassID", typeof(string));
                for (int i = 0; i < appId.Count; i++)
                {
                    con.Open();
                    query = "Select * from ClassTable where ClassID='" + appId[i] + "'";

                    com = new SqlCommand(query, con);
                    dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        dt.Rows.Add(new object[] { dr["ClassName"].ToString(), dr["ClassSection"].ToString(), dr["Shift"].ToString(), dr["ClassID"].ToString() });
                        //  appId.Add(dr["ClassId"].ToString());
                        count++;
                    }

                    con.Close();
                }
                GvClass.DataSource = dt;

                GvClass.DataBind();
                DepartmentView.Visible = false;
                if (count == 0)
                    Label1.Visible = true;
                else
                    ClassView.Visible = true;

                /*  string query = "Select * from ClassTable where DId= '" + DID + "' ";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GvClass.DataSource = dt;

                    GvClass.DataBind();
                    con.Close();*/
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            DepartmentView.Visible = true;
            ClassView.Visible = false;
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
        protected void ClassLink_Click(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32((sender as Button).CommandArgument);
            Session["ClassID"] = CID;
            Response.Redirect("AdminResult.aspx");


        }
    }
}