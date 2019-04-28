using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;

namespace ClassAttendance
{
    public partial class S_Connected : System.Web.UI.Page
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
                                $"<td><a class='btn btn-primary' onclick='btn_click(\"{row["ID"].ToString()}\",\"{row["CLASS_CODE"].ToString()}\",\"{row["ROOM"].ToString()}\"); return false;' />Go to Class</a></td>" +
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
            string str = string.Empty;

            int StudentNo =0;
            int ConnectNo =0;

            bool goo = false;

            DataTable Table = new DataTable();

            List<string> mac_list = new List<string>();
            List<ConnectedList> Connected = new List<ConnectedList>();
            List<DisconnectedList> Disconnected = new List<DisconnectedList>();
            List<string> NoDevice = new List<string>();

            string[] AllLine = File.ReadAllLines(@"C:\Users\User\Desktop\test.txt");
            int line = 0;
            foreach (string s in AllLine)
            {
                line += 1;
                if (s.StartsWith("192.168"))
                {
                    string[] lol = s.Split(null);
                    mac_list.Add(lol[1]);
                }
            }

            Table = DA.DA.GetClassMacAddr(id);
            foreach (DataRow row in Table.Rows)
            {
                if(Connected.Any(c => c.Name == row["NAME"].ToString()) || Disconnected.Any(c=> c.Name == row["NAME"].ToString()) || NoDevice.Contains(row["NAME"].ToString()))
                {
                    goo = true;
                }
                else
                {
                    goo = false;
                }

                if (goo == false)
                {
                    if (mac_list.Contains(row["MAC_ADDR"]))
                    {
                        ConnectedList c = new ConnectedList
                        {
                            Name = row["NAME"].ToString(),
                            MacAddr = row["MAC_ADDR"].ToString()
                        };
                        Connected.Add(c);
                        ConnectNo += 1;
                    }
                    else
                    {
                        if (row["MAC_ADDR"].ToString() == "")
                        {
                            NoDevice.Add(row["NAME"].ToString());
                        }
                        else
                        {
                            DisconnectedList d = new DisconnectedList
                            {
                                Name = row["NAME"].ToString(),
                                MacAddr = row["MAC_ADDR"].ToString()
                            };
                            Disconnected.Add(d);
                        }
                    }

                    StudentNo += 1;

                }
            }

            //All Data Collected
            //Turn Data to UI

            str += $"Students - {ConnectNo.ToString()} / {StudentNo.ToString()}|";

            foreach(ConnectedList item in Connected)
            {
                str += $"<tr><td>{item.Name}</td><td><b style='color:green;'>{item.MacAddr}</b></td></tr>";       
            }

            foreach(DisconnectedList item in Disconnected)
            {
                str += $"<tr><td>{item.Name}</td><td><b style='color:red;'>DIsconnect</b></td></tr>";
            }

            foreach(string item in NoDevice)
            {
                str += $"<tr><td>{item}</td><td><b>No Device</b></td></tr>";
            }

            return str;
        }

        protected void btnLogOut_ServerClick(object sender, EventArgs e)
        {
            Session["username"] = null;
            Response.Redirect("Default.aspx");
        }
    }

    public class ConnectedList
    {
        public string Name { set; get; }
        public string MacAddr { set; get; }
    }

    public class DisconnectedList
    {
        public string Name { set; get; }
        public string MacAddr { set; get; }
    }
}