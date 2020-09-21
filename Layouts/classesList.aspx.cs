using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace UokSemesterSystem
{

    public partial class classesList : System.Web.UI.Page
    {

        string departId;
        static string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(conString);
        private List<string[]> classIds = new List<string[]>();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["classesListLoaded"].ToString().Equals("0"))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey1", "AttendanceSession();", true);
            }
            else if (Session["classesListLoaded"].ToString().Equals("1"))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey2", "ResultSession();", true);
            }

            
            departId = Session["AccountId"].ToString();
            getClasses();
        }

        private void getClasses()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("Select * from ClassTable where DId='" + departId + "' ", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string[] cId = new string[5];
                cId[0] = dr["ClassID"].ToString();
                cId[1] = dr["ClassName"].ToString();
                cId[2] = dr["ClassSection"].ToString();
                cId[3] = "1";
                cId[4] = dr["Shift"].ToString();
                classIds.Add(cId);




            }

            con.Close();

            if (Session["isPro"].Equals("1"))
            {
                selectionDIV.Visible = true;
                headingDIV.Visible = false;
                lineDIV.Visible = true;
                for (int i = 0; i < classIds.Count; i++)
                {
                    con.Open();

                    cmd = new SqlCommand("Select * from ApprovalTable where AdminApp='" + 1 + "' and ClassId='" + classIds[i][0] + "' ", con);
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        classIds[i][3] = "1";
                    }
                    else
                        classIds[i][3] = "0";
                    con.Close();
                }
            }

            for (int i = 0; i < classIds.Count; i++)
            {
                if (classIds[i][3].Equals("1"))
                {
                    TableRow row = new TableRow();
                    TableCell cell0 = new TableCell();
                    cell0.Text = "" + (i + 1);
                    cell0.CssClass = "backcellleft";
                    if (i % 2 != 0)
                    {
                        cell0.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                    }
                    row.Cells.Add(cell0);

                    TableCell cell1 = new TableCell();
                    cell1.Text = classIds[i][1];
                    cell1.CssClass = "backcell";
                    if (i % 2 != 0)
                    {
                        cell1.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                    }
                    row.Cells.Add(cell1);

                    TableCell cell5 = new TableCell();
                    cell5.Text = classIds[i][4];
                    cell5.CssClass = "backcell";
                    if (i % 2 != 0)
                    {
                        cell5.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                    }
                    row.Cells.Add(cell5);

                    TableCell cell2 = new TableCell();
                    cell2.Text = classIds[i][2];
                    cell2.CssClass = "backcellright";
                    if (i % 2 != 0)
                    {
                        cell2.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                    }
                    row.Cells.Add(cell2);

                    TableCell cell3 = new TableCell();
                    if (Session["isPro"].Equals("1"))
                    {
                        Button viewPro = new Button();
                        viewPro.ID = classIds[i][0];
                        viewPro.Text = "View Proforma";
                        viewPro.CssClass = "backbtn";
                        viewPro.Click += new EventHandler(viewProClick);
                        cell3.Controls.Add(viewPro);
                    }
                    else
                    {
                        Button att = new Button();
                        att.ID = classIds[i][0];
                        att.Text = "View Attendance";
                        att.CssClass = "backbtn";
                        att.Click += new EventHandler(attClick);


                        cell3.Controls.Add(att);
                    }
                   
                    row.Cells.Add(cell3);

                    classList.Rows.Add(row);
                }
            }

            if (classList.Rows.Count == 1)
            {
                classList.Visible = false;
                Label1.Visible = true;
            }

        }

        private void viewProClick(object sender, EventArgs e)
        {
            string id = ((Button)sender).ID;
            Session["ClassID"] = id;
            Response.Redirect("classProforma.aspx");
        }

        private void attClick(object sender, EventArgs e)
        {
            string id = ((Button)sender).ID;
            Session["cId"] = id;
            Response.Redirect("AttToAdmin.aspx");

        }

        protected void gotoresult_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClassPageCP.aspx");
        }
    }
}