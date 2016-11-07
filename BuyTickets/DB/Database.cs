﻿using System.Data.SQLite;
using System.Windows.Forms;
using System.Drawing;
using System;

namespace BuyTickets.DB
{
    class Database
    {
        const string databaseName = @"..\..\DB\BT.sqlite";
        SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));

        // Авторизация | Проверка пароля по логину
        public bool Auth(string loginFromForm, string passFromForm)
        {
            bool success = false;
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Password FROM Users WHERE Login='" + loginFromForm + "';");
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader["Password"].ToString() == passFromForm)
                    success = true;
                else
                    success = false;
            }
            reader.Close();
            connection.Close();
            return success;
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
            cmd.Parameters.AddWithValue("@PasswordParam", Pass);
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
    }
}
