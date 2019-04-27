using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

namespace ClassAttendance
{
    public partial class ClassView : System.Web.UI.Page
    {

        DA.DA da = new DA.DA();

        protected void Page_Load(object sender, EventArgs e)
        {


            DataTable ClassList = new DataTable();
            ClassList = da.GetClassList();
            if (ClassList.Rows.Count >= 1)
            {
                string str = "";

                foreach (DataRow row in ClassList.Rows)
                {
                    str += $"<tr>" +
                                $"<td>{row["CLASS_CODE"].ToString()}</td>" +
                                $"<td>{row["ROOM"].ToString()}</td>" +
                                $"<td>{row["NO"].ToString()}</td>" +
                                $"<td><a class='btn btn-primary' onclick='btn_click(\"{row["ID"].ToString()}\"); return false;' />Go to Class</a></td>" +
                           $"</tr>";
                }

                tblClass.Controls.Add(new LiteralControl(str));
            }
            else
            {
                string str = "<tr><td colspan='4'>None</td></tr>";
                tblClass.Controls.Add(new LiteralControl(str));
            }
        }

        [WebMethod()]
        public static string GetList(string id)
        {

            string haha;
            haha = id;
            return id;
        }
    }
}