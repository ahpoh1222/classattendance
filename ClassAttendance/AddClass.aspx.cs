using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ClassAttendance
{
    public partial class AddClass : System.Web.UI.Page
    {
        DA.DA da = new DA.DA();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check Session
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Write("<script>window.close();</script>");
                }

                string type = Request.QueryString["action"].ToString();
                string id = Request.QueryString["id"].ToString();

                if (type == "Add")
                {
                    btnPerform.Value = "Add New Class";
                }
                else if (type == "Edit")
                {
                    btnPerform.Value = "Change";

                    DataTable table = new DataTable();
                    table = da.GetClassListByID(id);
                    txtClass.Value = table.Rows[0]["CLASS_CODE"].ToString();
                    txtRoom.Value = table.Rows[0]["ROOM"].ToString();
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                }
            }
        }

        protected void btnPerform_ServerClick(object sender, EventArgs e)
        {
            string type = Request.QueryString["action"].ToString();
            string id = Request.QueryString["id"].ToString();

            string ClassCode = txtClass.Value;
            string Room = txtRoom.Value;

            try
            {

                if (ClassCode == "" || Room == "")
                {
                    throw new Exception("Do not leave any blank");
                }

                if (type == "Add")
                {
                    string str = da.InsertClassList(ClassCode, Room);
                    if (str == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('New Class Added!')", true);
                        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    }
                    else
                    {
                        throw new Exception(str);
                    }
                }
                else if (type == "Edit")
                {
                    string str = da.UpdateClassList(id, ClassCode, Room);
                    if (str == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Update successfully!')", true);
                        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    }
                    else
                    {
                        throw new Exception(str);
                    }
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+ex.Message.ToString()+"')", true);
            }
        }
    }
}