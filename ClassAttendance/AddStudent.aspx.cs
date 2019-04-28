using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ClassAttendance
{
    public partial class AddStudent : System.Web.UI.Page
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

                table = da.GetClassList();
                foreach (DataRow row in table.Rows)
                {
                    txtClassCode.Items.Add(new ListItem(row["CLASS_CODE"].ToString(), row["ID"].ToString()));
                }

                if (type == "Add")
                {
                    btnPerform.Value = "Add New Student";
                }
                else if (type == "Edit")
                {
                    btnPerform.Value = "Edit";
                    btnDeletee.Visible = true;
                    txtPassword.Disabled = true;

                    //Get Student by ID
                    table = new DataTable();
                    table = da.GetStudent(id);
                    txtStudentID.Value = table.Rows[0]["STUDENT_ID"].ToString();
                    txtName.Value = table.Rows[0]["NAME"].ToString();
                    txtUsername.Value = table.Rows[0]["USERNAME"].ToString();
                    txtClassCode.Items.FindByValue(table.Rows[0]["C_ID"].ToString()).Selected = true;
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

            string StudentID = txtStudentID.Value;
            string Name = txtName.Value;
            string Username = txtUsername.Value;
            string Password = txtPassword.Value;
            string ClassCode = txtClassCode.Value;

            try
            {
                if (StudentID == "" || Name == "" || Username == "")
                {
                    throw new Exception("Please do not leave blank");
                }

                if (type == "Add")
                {
                    string str = da.InsertStudentAccount(StudentID, Name, Username, Password, ClassCode);
                    if (str == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('New Student Added!')", true);
                        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    }
                    else
                    {
                        throw new Exception(str);
                    }
                }
                else if (type == "Edit")
                {
                    string str = da.UpdateStudentAccount(id, StudentID, Name, Username, ClassCode);
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

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            string type = Request.QueryString["action"].ToString();
            string id = Request.QueryString["id"].ToString();

            try
            {
                if (type == "Edit")
                {
                    string str = da.DeleteStudentAccount(id);
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