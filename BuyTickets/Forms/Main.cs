using BuyTickets.DB;
using BuyTickets.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace BuyTickets
{
    public partial class Main : MaterialForm
    {
        private int isAdmin = 0;

        public Main(int isAdmin)
        {
            InitializeComponent();
            // Подключение MaterialSkin'a
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            this.isAdmin = isAdmin;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Database DB = new Database();
            DB.SessionsLoad(imageList1, listView1);
            materialLabel1.Text += " ( " + DateTime.Now.ToString("dd.MM.y") + " )";
            if (isAdmin == 1)
            {
                materialRaisedButton1.Text = "Кабинет администратора";
                materialLabel2.Text = "";
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (isAdmin == 0)
            {
                Personal form = new Personal();
                form.Show();
                this.Visible = false;
            }
            else
            {
                AdminPanel form = new AdminPanel();
                form.Show();
            }
        }
    }
}