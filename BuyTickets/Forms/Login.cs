using BuyTickets.DB;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace BuyTickets
{
    public partial class Login : MaterialForm
    {
        private int isAdmin = 0;
        private int balance = 0;

        public Login()
        {
            InitializeComponent();
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
            bool success;
            Database DB = new Database();
            DB.Auth(materialSingleLineTextField1.Text, materialSingleLineTextField2.Text, out isAdmin, out balance, out success);
            if (success)
            {
                Main form = new Main(this.isAdmin, balance);
                form.Show();
                this.Visible = false;
            }
            else MessageBox.Show("Неправильный логин или пароль");
            /*
            Main form = new Main();
            form.Show();
            this.Visible = false; */
        }
    }
}