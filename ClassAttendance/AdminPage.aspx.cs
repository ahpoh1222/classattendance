using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ClassAttendance
{
    public partial class AdminPage : Page
    {

        DA.DA da = new DA.DA();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("Default.aspx", false);
                }
            }

            DataTable ClassList = new DataTable();
            ClassList = da.GetClassList();
            if (ClassList.Rows.Count >=1)
            {
                string str = "";

                foreach (DataRow row in ClassList.Rows)
                {
                    str += $"<tr>" +
                                $"<td>{row["CLASS_CODE"].ToString()}</td>" +
                                $"<td>{row["ROOM"].ToString()}</td>" +
                                $"<td>{row["NO"].ToString()}</td>" +
                                $"<td><a class='btn btn-primary' onclick='lol(\"AddClass.aspx?action=Edit&id={row["ID"].ToString()}\"); return false;' />Edit</a></td>" +
                           $"</tr>";
                }

                tblClass.Controls.Add(new LiteralControl(str));
            }
            else
            {
                string str = "<tr><td colspan='4'>None</td></tr>";
                tblClass.Controls.Add(new LiteralControl(str));
            }

            string StudentNo = da.GetStudentNo();
            string TeacherNo = da.GetTeacherNo();

            lblStudent.InnerHtml = StudentNo + " Students";
            lblTeacher.InnerHtml = TeacherNo + " Teachers";


        }
    }
}