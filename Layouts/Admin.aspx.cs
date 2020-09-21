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
    public partial class Admin : System.Web.UI.Page
    {
        private static string conString = Utilities1.GetConnectionString();
        private static SqlConnection con = new SqlConnection(conString);
        string Id, AccountID;
        private static SqlCommand cmd;
        private static SqlDataReader dr;
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (ViewState["PreviousPage"] != null)  //Check if the ViewState 
                                                    //contains Previous page URL
            {
                Response.Redirect(ViewState["PreviousPage"].ToString());//Redirect to 
                                                                        //Previous page by retrieving the PreviousPage Url from ViewState.
            }
        }
        protected void CreateAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateAccount.aspx");
        }

        protected void AccountSet_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccountSetting.aspx");
        }

        protected void Result_Click(object sender, EventArgs e)
        {
            Response.Redirect("CPViewResult.aspx");
        }

        protected void TimeTable_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminTT.aspx");
        }

        protected void updateSem_Click(object sender, EventArgs e)
        {

            int sem = 0;
            string query;
            List<string> classId = new List<string>();

            query = "Select * from ClassTable ";
            con.Open();
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                    classId.Add(dr["ClassID"].ToString());

            }
            con.Close();

            int c = classId.Count;
            for (int k = 0; k < c; k++)
            {
                List<string> sList1 = new List<string>();
                query = "select * from Student where ClassID= '" + classId[k] + "' ";
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                        sList1.Add(dr["SId"].ToString());

                }
                con.Close();

                for (int i = 0; i < sList1.Count; i++)
                {
                    query = "select SemesterNo from Student where SId= '" + sList1[i] + "' ";
                    con.Open();
                    cmd = new SqlCommand(query, con);
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        sem = Convert.ToInt32(dr[0].ToString());

                    }
                    con.Close();

                    if (sem != 8)
                    {
                        sem++;
                        query = "update Student set SemesterNo='" + sem + "' where SId= '" + sList1[i] + "'";
                        con.Open();
                        cmd = new SqlCommand(query, con);
                        dr = cmd.ExecuteReader();
                        dr.Read();
                        con.Close();
                    }
                    else
                        classId[k] = null;


                }

                string cName = "";
                int year = 0;
                string[] temp;
                //update class
                if (classId[k] != null)
                {
                    if (sem % 2 == 1)
                    {
                        query = "select * from ClassTable where ClassID='" + classId[k] + "'";
                        con.Open();
                        cmd = new SqlCommand(query, con);
                        dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            cName = dr["ClassName"].ToString();
                            year = Convert.ToInt32(dr["Year"].ToString());



                        }
                        con.Close();

                        temp = cName.Split('-');
                        cName = temp[0] + "-" + getRomanInc(temp[1]);
                        year++;

                        query = "update ClassTable set ClassName='" + cName + "' , Year='" + year + "' where ClassID= '" + classId[k] + "'";
                        con.Open();
                        cmd = new SqlCommand(query, con);
                        dr = cmd.ExecuteReader();
                        dr.Read();
                        con.Close();
                    }
                }


            }
            insertIntoDb();
            Response.Redirect("Admin.aspx");
        }

        private void insertIntoDb()
        {
            int temp = 0;
            string query = "select * from Date";
            con.Open();
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                temp = 1;

            }
            con.Close();

            if (temp == 1)
            {
                query = "Update Date set IsCurrent='" + 0 + "' ";
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();
                dr.Read();
                con.Close();
            }

            query = "insert into Date (Date,IsCurrent) values ('" + DateTime.Now.ToString("dd/MM/yyyy") + "','" + 1 + "')";
            con.Open();
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            con.Close();
        }

        private string getRomanInc(string s)
        {
            if (s.Equals("I"))
                return "II";
            else if (s.Equals("II"))
                return "III";
            else if (s.Equals("III"))
                return "IV";
            else if (s.Equals("IV"))
                return "V";
            else if (s.Equals("V"))
                return "VI";
            else if (s.Equals("VI"))
                return "VII";
            else
                return "VIII";
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
            Session["AccountType"] = "Admin";


            if (!IsPostBack) //check if the webpage is loaded for the first time.
            {
                ViewState["PreviousPage"] =
            Request.UrlReferrer;//Saves the Previous page url in ViewState
            }


            ProfilePic.ImageUrl = "~/Img/admin.jpg";

            chkForUpdate();
        }

        private void chkForUpdate()
        {
            string query, date = "";
            string[] temp;
            string m;
            m = DateTime.Now.Month.ToString();
            if (m.Equals("6") || m.Equals("1"))
            {
                query = "select Date from Date where IsCurrent='" + 1 + "'";
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    date = dr[0].ToString();
                    Label1.Text = "Last Updated on " + date;
                    Label1.Visible = true;

                }
                con.Close();

                temp = date.Split('/');
                m = "0" + DateTime.Now.Month.ToString();
                if (m.Equals(temp[1]))
                {
                    updateSem.Enabled = false;
                    updateSem.BackColor = System.Drawing.Color.DarkGray;
                }
                else
                    updateSem.Enabled = true;

            }
            else
            {
                updateSem.Enabled = false;
                updateSem.BackColor = System.Drawing.Color.DarkGray;
            }
            
        }
    }
}