using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ClassAttendance
{
    public partial class StudentPage : System.Web.UI.Page
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

            string id = null;
            if (Session["username"] != null)
            {
                id = Session["username"].ToString();
            }

            lblName.InnerHtml = da.GetNameByUsername(id);

            String firstMacAddress = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault();

            StringBuilder buffer = new StringBuilder(firstMacAddress.Length * 3 / 2);
            for (int i = 0; i < firstMacAddress.Length; i++)
            {
                if ((i > 0) & (i % 2 == 0))
                    buffer.Append(":");
                buffer.Append(firstMacAddress[i]);
            }

            lblCurrentMac.InnerHtml = buffer.ToString();

            GetMacAddress(id);
        }

        protected void GetMacAddress(string id)
        {
            //Get MacAddress
            DataTable Table = new DataTable();
            try
            {
                Table = da.GetStudentMacAddr(id);

                int count = Table.Rows.Count;

                lblmac1.InnerHtml = "none";
                lblmac2.InnerHtml = "none";
                lblmac3.InnerHtml = "none";

                if (count >= 1)
                {
                    lblmac1.InnerHtml = Table.Rows[0]["MAC_ADDR"].ToString();
                }

                if (count >= 2)
                {
                    lblmac2.InnerHtml = Table.Rows[1]["MAC_ADDR"].ToString();
                }

                if (count == 3)
                {
                    lblmac3.InnerHtml = Table.Rows[2]["MAC_ADDR"].ToString();
                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
            }
        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            string username = Session["username"].ToString();

            string str = da.InsertMacAddress(username, lblCurrentMac.InnerHtml);
            if(str == "1")
            {
                Response.Write("<script>alert('Mac Address Updated!');</script>");
                GetMacAddress(username);
            }
            else
            {
                Response.Write("<script>alert('" + str + "');</script>");
            }
        }

        protected void btnLogOut_ServerClick(object sender, EventArgs e)
        {
            Session["username"] = null;
            Response.Redirect("Default.aspx");
        }
    }
}