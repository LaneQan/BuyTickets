using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace BuyTickets.DB
{
    internal class Database
    {
        private const string databaseName = @"..\..\DB\BT.db";
        private SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));

        // Авторизация | Проверка пароля по логину
        public void Auth(string loginFromForm, string passFromForm, out int isAdmin, out int balance, out bool success)
        {
            success = false;
            isAdmin = 0;
            balance = 0;
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Password,IsAdmin,Balance FROM Users WHERE Login='" + loginFromForm + "';", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader["Password"].ToString() == MD5crypt(passFromForm))
                {
                    isAdmin = Convert.ToInt16(reader["isAdmin"]);
                    balance = Convert.ToInt16(reader["Balance"]);
                    success = true;
                }
                else
                    success = false;
            }
            reader.Close();
            connection.Close();
        }

        // Загрузка сеансов в главном окне
        public void FilmsLoad(ImageList imageList, ListView listView, string date)
        {
            List<int> FilmId = new List<int>();
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Film_Id FROM Sessions WHERE Date='" + date + "' GROUP BY Film_id;", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                FilmId.Add(Convert.ToInt16(reader[0]));
            }
            reader.Close();
            cmd = new SQLiteCommand("SELECT * FROM Films;", connection);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string path = @"..\..\Images\" + reader["Id"].ToString() + ".jpg";
                imageList.Images.Add(reader["Name"].ToString(), Image.FromFile(path));
            }
            reader.Close();
            connection.Close();
            int k = 0;
            foreach (int p in FilmId)
            {
                listView.Items.Add(imageList.Images.Keys[p].ToString()).ImageIndex = p;
                listView.Items[k].Tag = p;
                k++;
            }
        }

        // Регистрация
        public void Registration(string Mail, string Login, string Pass, string Name, string Surname, string Phone)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO 'Users' (Login, Password, Name, Surname, Phone, Mail, isAdmin, Balance)"
            + "VALUES (@LoginParam, @PasswordParam, @NameParam, @SurnameParam, @PhoneParam, @MailParam, @isAdmin, @Balance)", connection);
            cmd.Parameters.AddWithValue("@LoginParam", Login);
            cmd.Parameters.AddWithValue("@PasswordParam", MD5crypt(Pass));
            cmd.Parameters.AddWithValue("@NameParam", Name);
            cmd.Parameters.AddWithValue("@SurnameParam", Surname);
            cmd.Parameters.AddWithValue("@PhoneParam", Phone);
            cmd.Parameters.AddWithValue("@MailParam", Mail);
            cmd.Parameters.AddWithValue("@isAdmin", false);
            cmd.Parameters.AddWithValue("@Balance", 0);
            cmd.ExecuteNonQuery();
            connection.Close();
        }


        // Проверка или занят login/mail для регистрации
        public bool userOnBase(string login, string mail)
        {
            connection.Open();
            bool onBase = false;
            SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM Users WHERE Login = '" + login + "' OR Mail = '" + mail + "'", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (Convert.ToInt16(reader[0]) == 1)
                    onBase = true;
            }
            reader.Close();
            connection.Close();
            return onBase;
        }

        private string MD5crypt(string toCrypt)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(toCrypt);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));
            return sb.ToString();
        }

        public bool isAdmin(string login)
        {
            bool admin = false;
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT IsAdmin FROM Users WHERE Login='" + login + "';", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (Convert.ToInt16(reader[0]) == 1)
                    admin = true;
                else admin = false;
            }
            reader.Close();
            connection.Close();
            return admin;
        }

        public void changePermissions(string login, int isAdmin)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("UPDATE 'Users' SET 'isAdmin'=" + isAdmin + " WHERE Login='" + login + "'", connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public string returnDescript(int id)
        {
            string description = "";
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Name,Description FROM Films WHERE Id=" + id + ";", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                description = Convert.ToString(reader[0]) + "\n\n" + Convert.ToString(reader[1]);
            }
            reader.Close();
            connection.Close();
            return description;
        }

        public List<string> CinemasLoad(int id, string date)
        {
            List<string> list = new List<string>();
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Cinemas.Name FROM Cinemas, Sessions WHERE Sessions.Date='" + date + "' AND Sessions.Film_Id=" + id + " AND Cinemas.Id=Sessions.Cinemas_Id GROUP BY Cinemas_Id;", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(Convert.ToString(reader["Name"]));
                }
            reader.Close();
            connection.Close();
            return list;
        }
        public List<string> TimeLoad(int id, string date, string cinema)
        {
            List<string> list = new List<string>();
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Sessions.Time FROM Cinemas, Sessions WHERE Sessions.Date='" + date + "' AND Sessions.Film_Id=" + id + " AND Sessions.Cinemas_Id=(SELECT Id FROM Cinemas WHERE Name='"+cinema+"') GROUP BY Cinemas_Id", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                {
                    list.Add(Convert.ToString(reader["Time"]));
                }
            reader.Close();
            connection.Close();
            return list;
        }
        public string OccSeats(int id, string date, string cinema)
        {
            string places = "";
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Sessions.Places FROM Cinemas, Sessions WHERE Sessions.Date='" + date + "' AND Sessions.Film_Id=" + id + " AND Sessions.Cinemas_Id=(SELECT Id FROM Cinemas WHERE Name='" + cinema + "') GROUP BY Cinemas_Id", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                places = Convert.ToString(reader["Places"]);
            }
            reader.Close();
            connection.Close();
            return places;
        }

        public int GetPrice(int id, string date, string cinema, string time)
        {
            int price = 0;
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Cost FROM Sessions WHERE Date='" + date + "' and Film_ID=" + id +
                " and Cinemas_Id=(SELECT Id From Cinemas WHERE Cinemas.Name='" + cinema + "') and Time='" + time + "';", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                price = Convert.ToInt16(reader[0]);
            }
            reader.Close();
            connection.Close();
            return price;
        }

        public void UpdateSeatsBalanceAndHistory(string login, int numberOfTickets, int oldBalance, int ticketCosts, List<string> seatList, int id, string date, string cinema, string time)
        {
            string filmName = "";
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("UPDATE Users SET Balance=" + Convert.ToString(oldBalance-numberOfTickets*ticketCosts)+" WHERE Login='"+login+"';", connection); // обновление баланса
            cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand("SELECT Name FROM Films WHERE ID =" + id+" ;", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                filmName = Convert.ToString(reader[0]);
            }
            reader.Close();
            string seats = string.Join(";", seatList.ToArray()) + ";";
            cmd = new SQLiteCommand("UPDATE Sessions SET Places = Places || '" + seats + "' WHERE Date='" + date + "' and Film_ID=" + id  +
                " and Cinemas_Id=(SELECT Id From Cinemas WHERE Cinemas.Name='" + cinema + "') and Time='" + time + "';", connection); // обновление мест
            cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand(("INSERT INTO Balance_History (User_login, Action, Change, Date)"
            + "VALUES (@User_login, @Action, @Change, @Date);"), connection);
            cmd.Parameters.AddWithValue("@User_login", login);
            cmd.Parameters.AddWithValue("@Action", "Покупка билетов в количестве: " + numberOfTickets + " шт. на сеанс '" + date + " " + filmName + " " + time+"'");
            cmd.Parameters.AddWithValue("@Change", "-"+numberOfTickets*ticketCosts+" руб.");
            cmd.Parameters.AddWithValue("@Date", Convert.ToString(DateTime.Today.ToString("dd.MM.yyyy"))); 
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public string[,] getBalanceHistory(string login)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*)FROM Balance_History WHERE User_Login='" + login + "';", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            int count=1;
            while (reader.Read())
            {
                count = Convert.ToInt16(reader[0]);
            }
            reader.Close();
            string[,] toList = new string[count,4];
            cmd = new SQLiteCommand("SELECT Date, Action, Change, Key FROM Balance_History WHERE User_Login='" + login + "';", connection);
            reader = cmd.ExecuteReader();
            int i = 0;
                while (reader.Read())
            {
                    toList[i, 0] = Convert.ToString(reader["Date"]);
                    toList[i, 1] = Convert.ToString(reader["Action"]);
                    toList[i, 2] = Convert.ToString(reader["Change"]);
                    toList[i, 3] = Convert.ToString(reader["Key"]);
                i++;
            }
            reader.Close();
            connection.Close();
            return toList;
        }

    }
}