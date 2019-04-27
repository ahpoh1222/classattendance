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
                //string ePass = Encrpt(password);
                string ePass = password;

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

        public String Encrpt(String value)
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

        public DataTable GetClassList()
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"SELECT CLASS.*, (SELECT COUNT(*) FROM STUDENT WHERE STUDENT.C_ID = CLASS.ID) AS NO FROM CLASS";
                cmd = new MySqlCommand(sql, con);

                reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                return table;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public DataTable GetClassListByID(string id)
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"SELECT * FROM CLASS WHERE ID = {id}";
                cmd = new MySqlCommand(sql, con);

                reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                return table;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public string InsertClassList(string Class, string Room)
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"INSERT INTO CLASS(CLASS_CODE,ROOM) VALUES('{Class}','{Room}')";
                cmd = new MySqlCommand(sql, con);

                cmd.ExecuteNonQuery();
                return "1";
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

        public string UpdateClassList(string ID,string Class,string Room)
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"UPDATE CLASS SET CLASS_CODE = '{Class}', ROOM = '{Room}' WHERE ID = '{ID}'";
                cmd = new MySqlCommand(sql, con);

                cmd.ExecuteNonQuery();
                return "1";
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

        public string GetStudentNo()
        {
            con = new MySqlConnection(ConnString);
            con.Open();

            string sql = $"SELECT COUNT(*) AS NO FROM STUDENT WHERE STATUS = '1'";
            cmd = new MySqlCommand(sql, con);

            reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);

            return table.Rows[0]["NO"].ToString();
        }

        public string GetTeacherNo()
        {
            con = new MySqlConnection(ConnString);
            con.Open();

            string sql = $"SELECT COUNT(*) AS NO FROM TEACHER WHERE STATUS = '1'";
            cmd = new MySqlCommand(sql, con);

            reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);

            return table.Rows[0]["NO"].ToString();
        }

        public DataTable GetStudentMacAddr(string id)
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"SELECT a.MAC_ADDR FROM DEVICE AS a INNER JOIN STUDENT AS b ON a.S_ID = b.ID INNER JOIN LOGINCREDENTIAL as C ON b.L_ID = c.ID AND c.USERNAME = '{id}'";
                cmd = new MySqlCommand(sql, con);

                reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                return table;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string InsertMacAddress(string username,string MacAddr)
        {

            try
            {
                string SID = null;

                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"SELECT a.ID FROM STUDENT AS a INNER JOIN LOGINCREDENTIAL AS b ON a.L_ID = b.ID AND b.USERNAME = '{username}'";
                cmd = new MySqlCommand(sql, con);

                reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                SID = table.Rows[0]["ID"].ToString();

                sql = $"SELECT * FROM DEVICE WHERE MAC_ADDR = '{MacAddr}'";
                cmd = new MySqlCommand(sql, con);

                reader = cmd.ExecuteReader();
                table = new DataTable();
                table.Load(reader);

                if (table.Rows.Count > 0)
                {
                    throw new Exception("This device already registered.");
                }

                sql = $"SELECT * FROM DEVICE WHERE S_ID = {SID}";
                cmd = new MySqlCommand(sql, con);

                reader = cmd.ExecuteReader();
                table = new DataTable();
                table.Load(reader);

                if (table.Rows.Count >= 3)
                {
                    throw new Exception("You`re already registered 3 devices, please contact admin if further assistant needed.");
                }

                sql = $"INSERT INTO device(S_ID,MAC_ADDR) VALUES('{SID}','{MacAddr}')";
                cmd = new MySqlCommand(sql, con);

                cmd.ExecuteNonQuery();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }


    }
}