using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UokSemesterSystem
{
    public partial class ClassPageCP : System.Web.UI.Page
    {
        string DeptId;
        static string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(conString);
        protected void Page_Load(object sender, EventArgs e)
        {
            DeptId = Session["AccountId"].ToString();
            getClasses();
        }


        void getClasses()
        {

            string query = "SELECT * FROM ClassTable WHERE DId='" + DeptId + "'   ";
            con.Open();
            SqlDataAdapter sqlDa1 = new SqlDataAdapter(query, con);
            DataTable dtbl = new DataTable();
            sqlDa1.Fill(dtbl);
            int count = 0;
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {


                TableRow row = new TableRow();

                TableCell cell0 = new TableCell();
                cell0.Text = "" + (count + 1);
                cell0.CssClass = "backcellleft";
                if (i % 2 != 0)
                {
                    cell0.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                }
                row.Cells.Add(cell0);


                TableCell cell1 = new TableCell();
                cell1.Text = dtbl.Rows[i]["ClassName"].ToString();
                cell1.CssClass = "backcell";
                if (i % 2 != 0)
                {
                    cell1.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                }
                row.Cells.Add(cell1);

                TableCell cell2 = new TableCell();
                cell2.Text = dtbl.Rows[i]["ClassSection"].ToString();
                cell2.CssClass = "backcell";
                if (i % 2 != 0)
                {
                    cell2.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                }
                row.Cells.Add(cell2);

                TableCell cell3 = new TableCell();
                cell3.Text = dtbl.Rows[i]["Shift"].ToString();
                cell3.CssClass = "backcellright";
                if (i % 2 != 0)
                {
                    cell3.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                }
                row.Cells.Add(cell3);
                Button button = new Button();
                button.Text = "View Courses";
                button.CssClass = "backcellbutton";
                button.Click += new EventHandler(viewClassCourses);

                button.ID = "btnCC_" + dtbl.Rows[i]["ClassID"];
                TableCell cell4 = new TableCell();
                cell4.Controls.Add(button);
                
                row.Cells.Add(cell4);
                classesTable.Rows.Add(row);
                count++;
                con.Close();
            }

        }

        private void viewClassCourses(object sender, EventArgs e)
        {
            string[] temp = ((Button)sender).ID.Split('_');
            int id = Convert.ToInt32(temp[1]);
            Session["classId"] = id;
            Response.Redirect("CourseList.aspx");
        }
        protected void gotoproforma_Click(object sender, EventArgs e)
        {
            Session["isPro"] = "1";
            Response.Redirect("classesList.aspx");
        }
    }
}