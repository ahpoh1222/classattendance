using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ClassAttendance
{
    public partial class StudentLIst : System.Web.UI.Page
    {

        DA.DA da = new DA.DA();
        protected void Page_Load(object sender, EventArgs e)
        {


            DataTable table = new DataTable();
            table = da.GetStudentList();    

            string str = string.Empty;
            if  (table.Rows.Count > 0)
            {
                foreach(DataRow row in table.Rows)
                {
                    str += $"<tr>" +
                                $"<td>{row["STUDENT_ID"].ToString()}</td>" +
                                $"<td>{row["NAME"].ToString()}</td>" +
                                $"<td>{row["CLASS_CODE"].ToString()}</td>" +
                                $"<td>{row["NO"].ToString()}</td>" +
                                $"<td><a class='btn btn-primary' onclick='lol(\"AddStudent.aspx?action=Edit&id={row["ID"].ToString()}\"); return false;' />Edit</a></td>" +
                           $"</tr>";
                }
            }
            else
            {
                str += $"<tr>" +
                                $"<td colspan='5'>NONE</td>" +
                           $"</tr>";
            }

            tblStudent.Controls.Add(new LiteralControl(str));

        }
    }
}