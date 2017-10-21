using BuyTickets.DB;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;
using BuyTickets.Models;

namespace BuyTickets
{
    public partial class Login : MaterialForm
    {

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
            var user = Database.Auth(materialSingleLineTextField1.Text, materialSingleLineTextField2.Text);
            if (user!=null)
            {
                Main form = new Main(user);
                form.Show();
                this.Visible = false;
            }
            else MessageBox.Show("Неправильный логин или пароль");
        }
    }
}