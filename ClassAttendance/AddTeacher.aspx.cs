using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ClassAttendance
{
    public partial class AddTeacher : System.Web.UI.Page
    {
        DA.DA da = new DA.DA();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();

            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Write("<script>window.close();");
                }

                string type = Request.QueryString["action"].ToString();
                string id = Request.QueryString["id"].ToString();

                if (type == "Add")
                {
                    btnPerform.Value = "Add New Teacher";
                }
                else if (type == "Edit")
                {
                    btnPerform.Value = "Edit";
                    btnDeletee.Visible = true;
                    txtPassword.Disabled = true;

                    //Get Student by ID
                    table = new DataTable();
                    table = da.GetTeacher(id);
                    txtName.Value = table.Rows[0]["NAME"].ToString();
                    txtUsername.Value = table.Rows[0]["USERNAME"].ToString();
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

            string Name = txtName.Value;
            string Username = txtUsername.Value;
            string Password = txtPassword.Value;

            try
            {
                if (Name == "" || Username == "")
                {
                    throw new Exception("Please do not leave blank");
                }

                if (type == "Add")
                {
                    string str = da.InsertTeacherAccount(Name, Username, Password);
                    if (str == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('New Teacher Account Added!')", true);
                        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    }
                    else
                    {
                        throw new Exception(str);
                    }
                }
                else if (type == "Edit")
                {
                    string str = da.UpdateTeacherAccount(id,Name,Username);
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

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
        }

        protected void btnDeletee_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["action"].ToString();
            string id = Request.QueryString["id"].ToString();

            try
            {
                if (type == "Edit")
                {
                    string str = da.DeleteTeacherAccount(id);
                    if (str == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Account Deleted.')", true);
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

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
        }
    }
}