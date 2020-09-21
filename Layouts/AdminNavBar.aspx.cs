using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UokSemesterSystem
{
    public partial class AdminNavBar : System.Web.UI.Page
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["Id"] == null)
                Response.Redirect("Login.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnTimeTable_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminTT.aspx");
        }

        protected void BtnResults_Click(object sender, EventArgs e)
        {
            Response.Redirect("CPViewResult.aspx");
        }

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateAccount.aspx");
        }

        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            Session["Enrollment"] = null;
            Session["Email"] = null;
            Response.Redirect("StudentRecords.aspx");
        }
        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("TeacherRecords.aspx");
        }

        protected void BtnSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminSettings.aspx");
        }

        protected void BtnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }

        protected void BtnAttendance_Click(object sender, EventArgs e)
        {
            Response.Redirect("departList.aspx");
        }


        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session["Id"] = null;
            Session["otherUser"] = null;
            Session["AccountId"] = null;
            Response.Redirect("Login.aspx");

        }
    }
}