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
    public partial class EditTeacherDetails : System.Web.UI.Page
    {
        private static string conString = Utilities1.GetConnectionString();
        private static SqlConnection con = new SqlConnection(conString);
        protected void Page_Load(object sender, EventArgs e)
        {
            getTeacherDetails();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }

        private string getDepartname(string temp)
        {
            string depart_name = "";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string query = "select * from Department where DId='" + temp + "' ";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                depart_name = dr["DepartmentName"].ToString();
            }
            con.Close();
            return depart_name;
        }

        private void getTeacherDetails()
        {
            DataTable dt;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select * FROM Teacher ", con);
                dt = new DataTable();
                sda.Fill(dt);
                con.Close();
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                TableRow row = new TableRow();
                TableCell cell0 = new TableCell();
                cell0.Text = (i + 1).ToString();
                cell0.CssClass = "backcell";
                row.Cells.Add(cell0);


                TableCell cell1 = new TableCell();
                cell1.Text = dt.Rows[i]["TName"].ToString();
                cell1.CssClass = "backcell";
                row.Cells.Add(cell1);

                TableCell cell2 = new TableCell();
                cell2.Text = getDepartname(dt.Rows[i]["Department"].ToString());
                cell2.CssClass = "backcell";
                row.Cells.Add(cell2);

                TableCell cell3 = new TableCell();
                cell3.Text = dt.Rows[i]["Email"].ToString();
                cell3.CssClass = "backcell";
                row.Cells.Add(cell3);

                TableCell cell4 = new TableCell();
                cell4.CssClass = "backcell";
                var btn_edit = new Button();
                btn_edit.ID = "edit_" + (i + 1);
                btn_edit.Text = "Edit Record";
                btn_edit.CssClass = "backbtn";
                btn_edit.Click += new EventHandler(btn_edit_Click);
                cell4.Controls.Add(btn_edit);
                //add cell to row
                row.Cells.Add(cell4);
                if (i == 0 || i % 2 == 0)
                {
                    row.BackColor = System.Drawing.Color.FromArgb(239, 243, 251);
                }
                tbl_teacher.Rows.Add(row);



            }
        }

        protected void DisplayStudentsList(object sender, EventArgs e)
        {
            Response.Redirect("EditStudentEduInfo.aspx");
        }
        void btn_edit_Click(object sender, EventArgs e)
        {

            string[] temp = ((Button)sender).ID.Split('_');
            int id = Convert.ToInt32(temp[1]);
            Session["email"] = tbl_teacher.Rows[id].Cells[3].Text;
            Response.Redirect("TeacherRegistration.aspx");
        }
    }
}