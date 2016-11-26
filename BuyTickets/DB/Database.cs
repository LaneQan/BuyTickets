using System;
using System.Data.SQLite;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace BuyTickets.DB
{
    internal class Database
    {
        private const string databaseName = @"..\..\DB\BT.sqlite";
        private SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));

        // Авторизация | Проверка пароля по логину
        public void Auth(string loginFromForm, string passFromForm, out int isAdmin, out bool success)
        {
            success = false;
            isAdmin = 0;
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Password,IsAdmin FROM Users WHERE Login='" + loginFromForm + "';", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader["Password"].ToString() == MD5crypt(passFromForm))
                {
                    isAdmin = Convert.ToInt16(reader["isAdmin"]);
                    success = true;
                }
                else
                    success = false;
            }
            reader.Close();
            connection.Close();
        }

        // Загрузка сеансов в главном окне
        public void SessionsLoad(ImageList imageList, ListView listView)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Sessions;", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string path = @"..\..\Images\" + reader["Id"].ToString() + ".jpg";
                imageList.Images.Add(reader["Name"].ToString(), Image.FromFile(path));
            }
            reader.Close();
            connection.Close();
            for (int i = 0; i < imageList.Images.Count; i++)
            {
                listView.Items.Add(imageList.Images.Keys[i].ToString()).ImageIndex = i;
            }
        }

        // Регистрация
        public void Registration(string Mail, string Login, string Pass, string Name, string Surname, string Phone)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO 'Users' (Login, Password, Name, Surname, Phone, Mail, isAdmin)"
            + "VALUES (@LoginParam, @PasswordParam, @NameParam, @SurnameParam, @PhoneParam, @MailParam, @isAdmin)", connection);
            cmd.Parameters.AddWithValue("@LoginParam", Login);
            cmd.Parameters.AddWithValue("@PasswordParam", MD5crypt(Pass));
            cmd.Parameters.AddWithValue("@NameParam", Name);
            cmd.Parameters.AddWithValue("@SurnameParam", Surname);
            cmd.Parameters.AddWithValue("@PhoneParam", Phone);
            cmd.Parameters.AddWithValue("@MailParam", Mail);
            cmd.Parameters.AddWithValue("@isAdmin", false);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        // Проверка или занят login/mail для регистрации
        public bool canRegisterOrNot(string login, string mail)
        {
            connection.Open();
            bool canRegister = false;
            SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM Users WHERE Login = '" + login + "' OR Mail = '" + mail + "'", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (Convert.ToInt16(reader[0]) == 0)
                    canRegister = true;
            }
            connection.Close();
            return canRegister;
        }

        private string MD5crypt(string toCrypt)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(toCrypt);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));
            return sb.ToString();
        }
    }
}