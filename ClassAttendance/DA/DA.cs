using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text;
using System.Security.Cryptography;

namespace ClassAttendance.DA
{
    public class DA
    {

        private string ConnString = "Server=localhost; database=classattendance; UID=root; password=;";
        private MySqlConnection con = null;
        private MySqlCommand cmd = null;
        private MySqlDataReader reader = null;

        public string CheckUSer(string username,string password)
        {
            try
            {
                string ePass = Encrpt(password);

                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"SELECT USERNAME, ROLE FROM LOGINCREDENTIAL WHERE USERNAME = '{username}' AND PASSWORD = '{ePass}'";
                cmd = new MySqlCommand(sql, con);

                reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                if (table.Rows.Count == 1)
                {
                    string username_d = table.Rows[0]["USERNAME"].ToString();
                    if (username == username_d)
                    {
                        return table.Rows[0]["ROLE"].ToString();
                    }
                    else
                    {
                        throw new Exception("Couldn't retreive user's role, please contact admin");
                    }
                }
                else if(table.Rows.Count <= 0)
                {
                    throw new Exception("Username / Password incorrect.");
                }
                else

                {
                    throw new Exception("Username appear twice or more in database, please contact admin.");
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public static String Encrpt(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

    }
}