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

        public string GetNameByUsername(string username)
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"SELECT NAME FROM LOGINCREDENTIAL WHERE USERNAME = '{username}'";
                cmd = new MySqlCommand(sql, con);

                reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                if (table.Rows.Count > 0)
                {
                    return table.Rows[0]["NAME"].ToString();
                }
                else
                {
                    throw new Exception("Error when getting user's name");
                }
            }
            catch (Exception ex)
            {

                throw ex;
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

        //fk STATIC
        public static DataTable GetClassMacAddr(string c_id)
        {
            try
            {
                MySqlConnection con = new MySqlConnection("Server=localhost; database=classattendance; UID=root; password=;");
                con.Open();

                string sql = $"(SELECT a.NAME, b.MAC_ADDR FROM DEVICE AS b INNER JOIN STUDENT AS a ON b.S_ID = a.ID WHERE a.C_ID = '{c_id}')" +
                             $"UNION" +
                             $"(SELECT a.NAME, b.MAC_ADDR FROM STUDENT AS a LEFT JOIN DEVICE AS b ON a.ID = b.S_ID WHERE a.C_ID = '{c_id}')";
                //string sql = $"SELECT a.NAME, b.MAC_ADDR FROM STUDENT AS a LEFT JOIN DEVICE ON a.ID = b.S_ID AND a.C_ID = '{c_id}'";
                MySqlCommand cmd = new MySqlCommand(sql, con);

                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                return table;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetStudentList()
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"SELECT a.*, b.CLASS_CODE FROM (SELECT STUDENT.ID, STUDENT.STUDENT_ID, student.NAME, STUDENT.C_ID, (SELECT COUNT(*) FROM DEVICE WHERE DEVICE.S_ID = STUDENT.ID) AS NO FROM STUDENT) AS a INNER JOIN CLASS AS b ON a.C_ID = b.ID";
                cmd = new MySqlCommand(sql, con);

                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                return table;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetTeacherList()
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"SELECT ID, USERNAME, NAME FROM LOGINCREDENTIAL WHERE ROLE = '1'";
                cmd = new MySqlCommand(sql, con);

                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetStudent(string id)
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"SELECT a.C_ID, a.NAME, a.STUDENT_ID, b.USERNAME FROM STUDENT AS a INNER JOIN LOGINCREDENTIAL AS b ON a.L_ID = b.ID WHERE a.ID = {id}";
                cmd = new MySqlCommand(sql, con);

                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                return table;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetTeacher(string id)
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"SELECT USERNAME,PASSWORD,NAME FROM LOGINCREDENTIAL WHERE ID = '{id}' AND ROLE = '1'";
                cmd = new MySqlCommand(sql, con);

                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string InsertStudentAccount(string studentID,string Name, string Username, string Password, string Class)
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"INSERT INTO LOGINCREDENTIAL(USERNAME,PASSWORD,NAME,ROLE) VALUES('{Username}','{Password}','{Name}','2'); SELECT last_insert_id();";
                cmd = new MySqlCommand(sql, con);
                string L_ID = cmd.ExecuteScalar().ToString();

                sql = $"INSERT INTO STUDENT(L_ID,C_ID,NAME,STUDENT_ID,STATUS) VALUES('{L_ID}','{Class}','{Name}','{studentID}','1')";
                cmd = new MySqlCommand(sql, con);

                cmd.ExecuteNonQuery();
                return "1";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string InsertTeacherAccount(string Name, string Username, string Password)
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"INSERT INTO LOGINCREDENTIAL(USERNAME,PASSWORD,NAME,ROLE) VALUES('{Username}','{Password}','{Name}','1'); SELECT last_insert_id();";
                cmd = new MySqlCommand(sql, con);
                string L_ID = cmd.ExecuteScalar().ToString();

                sql = $"INSERT INTO TEACHER(L_ID,NAME,STATUS) VALUES('{L_ID}','{Name}','1')";
                cmd = new MySqlCommand(sql, con);

                cmd.ExecuteNonQuery();
                return "1";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string UpdateStudentAccount(string id, string StudentID, string Name, string Username, string Class)
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"UPDATE STUDENT SET C_ID = '{Class}', NAME = '{Name}', STUDENT_ID = '{StudentID}' WHERE ID = '{id}'; SELECT L_ID FROM STUDENT WHERE id = '{id}';";
                cmd = new MySqlCommand(sql, con);
                string L_ID = cmd.ExecuteScalar().ToString();

                sql = $"UPDATE LOGINCREDENTIAL SET USERNAME = '{Username}', NAME = '{Name}' WHERE ID = '{L_ID}'";
                cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                return "1";

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string UpdateTeacherAccount(string id, string Name, string Username)
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"UPDATE TEACHER SET NAME = '{Name}' WHERE L_ID = '{id}'";
                cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                sql = $"UPDATE LOGINCREDENTIAL SET USERNAME = '{Username}', NAME = '{Name}' WHERE ID = '{id}'";
                cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                return "1";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string DeleteStudentAccount(string id)
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"SELECT L_ID FROM STUDENT WHERE ID = '{id}'; DELETE FROM STUDENT WHERE ID = '{id}';";
                cmd = new MySqlCommand(sql, con);
                string L_ID = cmd.ExecuteScalar().ToString();

                sql = $"DELETE FROM LOGINCREDENTIAL WHERE ID = '{L_ID}'";
                cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                return "1";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string DeleteTeacherAccount(string id)
        {
            try
            {
                con = new MySqlConnection(ConnString);
                con.Open();

                string sql = $"DELETE FROM TEACHER WHERE L_ID = '{id}';";
                cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                sql = $"DELETE FROM LOGINCREDENTIAL WHERE ID = '{id}'";
                cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                return "1";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }


    }
}