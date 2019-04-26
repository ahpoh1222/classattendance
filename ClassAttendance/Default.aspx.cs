using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace ClassAttendance
{
    public partial class _Default : Page
    {

        DA.DA da = null;

        protected void Page_Load(object sender, EventArgs e)
        {
        
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            da = new DA.DA();

            string username = txtUsername.Value;

            string Login = da.CheckUSer(username, txtPassword.Value);
            if (Login == "1")
            {
                //  1 = Teacher
                Session["username"] = username;
                lblError.InnerHtml = "";
            }
            else if( Login == "2")
            {
                Session["username"] = username;
                lblError.InnerHtml = "";
            }
            else if(Login == "3")
            {
                Session["username"] = username;
                lblError.InnerHtml = "";
                Response.Redirect("Account/Login.aspx");
            }
            else
            {
                lblError.InnerHtml = Login;
                Session["username"] = null;
            }
            
        }
    }
}