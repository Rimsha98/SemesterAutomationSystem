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
    public partial class EditStudentEduInfo : System.Web.UI.Page
    {

        private static string conString = Utilities1.GetConnectionString();
        private static SqlConnection con = new SqlConnection(conString);
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Enrollment"] = null;
            getStdData();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }
        private void getStdData()
        {
            DataTable dt;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select * FROM Student ", con);
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
                cell1.Text = dt.Rows[i]["Enrollment"].ToString();
                cell1.CssClass = "backcell";
                row.Cells.Add(cell1);

                TableCell cell2 = new TableCell();
                cell2.Text = dt.Rows[i]["SName"].ToString();
                cell2.CssClass = "backcell";
                row.Cells.Add(cell2);

                TableCell cell3 = new TableCell();
                cell3.Text = dt.Rows[i]["FatherName"].ToString();
                cell3.CssClass = "backcell";
                row.Cells.Add(cell3);

                TableCell cell4 = new TableCell();
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
                tbl_std.Rows.Add(row);



            }

        }

        protected void DisplayTeachersList(object sender, EventArgs e)
        {
            Response.Redirect("EditTeacherDetails.aspx");
        }

        void btn_edit_Click(object sender, EventArgs e)
        {

            string[] temp = ((Button)sender).ID.Split('_');
            int id = Convert.ToInt32(temp[1]);
            Session["Enrollment"] = tbl_std.Rows[id].Cells[1].Text;
            Response.Redirect("StudentRegistration.aspx");
        }
    }
}