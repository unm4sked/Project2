using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Models
{
    public static class Db
    {

        public static string AddUser(UserModel u)
        {
            try
            {
                string connectionString = "Server=localhost\\SQLEXPRESS01;Database=master;Trusted_Connection=True;";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                //SqlCommand cmd = new SqlCommand("CREATE TABLE TestP2 ( ID int NOT NULL IDENTITY (1,1) PRIMARY KEY,Login varchar(255) NOT NULL,Password varchar(255) NOT NULL,Reg varchar(255) NOT NULL,Description varchar(255),MinLength int,MaxLength int,MinUppercase int,MinLowercase int,MinSpecialSigns int,MinDigits int); ", connection);
                string querry = "INSERT INTO TestP2 (Login,Password,Reg,Description,MinLength,MaxLength,MinUppercase,MinLowercase,MinSpecialSigns,MinDigits) VALUES (@Login,@Password,@Reg,@Description,@MinLength,@MaxLength,@MinUppercase,@MinLowercase,@MinSpecialSigns,@MinDigits)";

                SqlCommand cmd = new SqlCommand(querry, connection);
                cmd.Parameters.AddWithValue("@Login", u.Login);
                cmd.Parameters.AddWithValue("@Password", Hash.Hashing(u.Password));
                cmd.Parameters.AddWithValue("@Reg", u.Reg);
                cmd.Parameters.AddWithValue("@Description", u.Description);
                cmd.Parameters.AddWithValue("@MinLength", u.MinLength);
                cmd.Parameters.AddWithValue("@MaxLength", u.MaxLength);
                cmd.Parameters.AddWithValue("@MinUppercase", u.MinUppercase);
                cmd.Parameters.AddWithValue("@MinLowercase", u.MinLowercase);
                cmd.Parameters.AddWithValue("@MinSpecialSigns", u.MinSpecialSigns);
                cmd.Parameters.AddWithValue("@MinDigits", u.MinDigits);

                int x = cmd.ExecuteNonQuery();
                if (x < 0)
                {
                    return "Error inserting data into Database!";
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Ok";
        }
        public static int CheckLogin(string Login)
        {
            string connectionString = "Server=localhost\\SQLEXPRESS01;Database=master;Trusted_Connection=True;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string querry = "SELECT * FROM TestP2 WHERE Login=@Loginn";
            SqlCommand cmd = new SqlCommand(querry, connection);
            cmd.Parameters.AddWithValue("@Loginn", Login);

            var count = cmd.ExecuteScalar();
            connection.Close();
            if (count != null)
                return (Int32)count;
            else return 0;
        }

        public static int LoginToService(string login,string pass)
        {
            string Pass = Hash.Hashing(pass);
   
            string connectionString = "Server=localhost\\SQLEXPRESS01;Database=master;Trusted_Connection=True;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string querry = "SELECT * FROM TestP2 WHERE Login=@Loginn AND Password=@Passwordd";
            SqlCommand cmd = new SqlCommand(querry, connection);
            cmd.Parameters.AddWithValue("@Loginn", login);
            cmd.Parameters.AddWithValue("@Passwordd", Pass);
            var count = cmd.ExecuteScalar();

            connection.Close();
            if (count == null) return 0;
            return (Int32)count;

        }

        public static UserModel ViewQuerry(string Login)
        {
            //var list = new List<KeyValuePair<string, string>>();
            UserModel u = new UserModel();
            string connectionString = "Server=localhost\\SQLEXPRESS01;Database=master;Trusted_Connection=True;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            List<String> column = new List<String>{ "ID","Login","Password", "Reg", "Description", "MinLength","MaxLength", "MinUppercase","MinLowercase" ,"MinSpecialSigns", "MinDigits" };

            string querryLogin = $"SELECT {column[1]} FROM TestP2 WHERE Login=@Loginn";
            string querryPass = $"SELECT {column[2]} FROM TestP2 WHERE Login=@Loginn";
            string querryReg = $"SELECT {column[3]} FROM TestP2 WHERE Login=@Loginn";
            string querryDesc = $"SELECT {column[4]} FROM TestP2 WHERE Login=@Loginn";
            string querryMinLen = $"SELECT {column[5]} FROM TestP2 WHERE Login=@Loginn";
            string querryMaxLen = $"SELECT {column[6]} FROM TestP2 WHERE Login=@Loginn";
            string querryMinUp = $"SELECT {column[7]} FROM TestP2 WHERE Login=@Loginn";
            string querryMinLow = $"SELECT {column[8]} FROM TestP2 WHERE Login=@Loginn";
            string querryMinSpecial = $"SELECT {column[9]} FROM TestP2 WHERE Login=@Loginn";
            string querryMinDigi = $"SELECT {column[10]} FROM TestP2 WHERE Login=@Loginn";


            SqlCommand cmd1 = new SqlCommand(querryLogin, connection);
            cmd1.Parameters.AddWithValue("@Loginn", Login);
            u.Login = cmd1.ExecuteScalar().ToString();

            SqlCommand cmd2 = new SqlCommand(querryPass, connection);
            cmd2.Parameters.AddWithValue("@Loginn", Login);
            u.Password = cmd2.ExecuteScalar().ToString();

            SqlCommand cmd3 = new SqlCommand(querryReg, connection);
            cmd3.Parameters.AddWithValue("@Loginn", Login);
            u.Reg = cmd3.ExecuteScalar().ToString();

            SqlCommand cmd4 = new SqlCommand(querryDesc, connection);
            cmd4.Parameters.AddWithValue("@Loginn", Login);
            u.Description = cmd4.ExecuteScalar().ToString();


            connection.Close();
            return u;
        }
        
    }
}
