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
        private bool Auth (string loginFromForm, string passFromForm)
        {
            bool success = false;
            const string databaseName = @"..\..\DB\BT.sqlite";
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            connection.Open();

            SQLiteCommand cmd = new SQLiteCommand("SELECT Password FROM Users WHERE Login='" + loginFromForm + "';", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string passFromDB = reader["Password"].ToString();
                if (passFromDB == passFromForm)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
                
            }
            reader.Close();
            connection.Close();
            return success;
        }
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
            this.Visible = false;
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            string loginFromForm = materialSingleLineTextField1.Text;
            string passFromForm = materialSingleLineTextField2.Text;
            /* if (Auth(loginFromForm, passFromForm) == true)
            {
                Main form = new Main();
                form.Show();
                this.Visible = false;
            }
            else MessageBox.Show("Неправильный логин или пароль"); */

            Main form = new Main();
            form.Show();
            this.Visible = false;



        }
    }
}
