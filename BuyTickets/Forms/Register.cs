using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Data.SQLite;

namespace BuyTickets
{
    public partial class Register : MaterialForm
    {
        public Register()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            var Login = materialSingleLineTextField2.Text;
            var Mail = materialSingleLineTextField1.Text;
            var Pass = materialSingleLineTextField3.Text;
            var Name = materialSingleLineTextField4.Text;
            var Surname = materialSingleLineTextField5.Text;
            var Phone = materialSingleLineTextField6.Text;

            // DATABASE

            const string databaseName = @"X:\Learning\BuyTickets\BuyTickets\DB\BT.sqlite";
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
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
    }
}
