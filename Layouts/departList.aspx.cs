using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UokSemesterSystem
{
    public partial class departList : System.Web.UI.Page
    {
        static string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(conString);

        protected void Page_Load(object sender, EventArgs e)
        {
            getDepart();
        }

        private void getDepart()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Department ", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                cell1.Text = dr["DepartmentName"].ToString();
                row.Cells.Add(cell1);



                Button att = new Button();
                att.ID = dr["DId"].ToString();
                att.Text = "View Classes";
                att.Click += new EventHandler(attClick);

                TableCell cell3 = new TableCell();
                cell3.Controls.Add(att);
                row.Cells.Add(cell3);




                departTable.Rows.Add(row);

            }

            con.Close();

        }

        private void attClick(object sender, EventArgs e)
        {
            string id = ((Button)sender).ID;
            Session["DId"] = id;
            Response.Redirect("classesList.aspx");

        }
    }
}