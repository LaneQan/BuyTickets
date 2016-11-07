using System;
using MaterialSkin.Controls;
using MaterialSkin;
using BuyTickets.DB;

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
           /* Database DB = new Database();
             if (test.Auth(materialSingleLineTextField1.Text, materialSingleLineTextField2.Text))
            {
                Main form = new Main();
                form.Show();
                this.Visible = false;
            }
            else MessageBox.Show("Неправильный логин или пароль"); 
                */
            Main form = new Main();
            form.Show();
            this.Visible = false;
        }
    }
}
