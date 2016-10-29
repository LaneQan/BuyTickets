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
    public partial class Login : MaterialForm
    {
        public Login()
        {
            InitializeComponent();
            // Подключение MaterialSkin'a
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Register_Click(object sender, EventArgs e)
        {
            Register form = new Register();
            form.Show();
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            var Login = materialSingleLineTextField1;
            var Pass = materialSingleLineTextField2;
            const string databaseName = @"X:\Learning\BuyTickets\BuyTickets\DB\BT.sqlite";
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            connection.Open();

            SQLiteCommand cmd = new SQLiteCommand("SELECT Password FROM 'Users' WHERE Login = @Login;", connection);
            cmd.Parameters.AddWithValue("@Login", Login);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                MessageBox.Show(reader["Login"].ToString());
            connection.Close();

            Main form = new Main();
            form.Show();
        }
    }
}
