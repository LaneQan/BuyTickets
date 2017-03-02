using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data.SQLite;
using BuyTickets.Forms;
using BuyTickets.DB;

namespace BuyTickets
{

    public partial class Main : MaterialForm
    {
        Database DB = new Database();
        private int isAdmin = 0;
        public int balance = 0;
        private string login;
        public Main(int isAdmin, int balance, string login)
        {
            InitializeComponent();
            // Подключение MaterialSkin'a
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            this.isAdmin = isAdmin;
            this.balance = balance;
            this.login = login;


        }


        private void Main_Load(object sender, EventArgs e)
        {
            DB.FilmsLoad(imageList1, listView1, DateTime.Today.ToString("dd.MM.yyyy"));
            if (isAdmin==1)
            {
                materialRaisedButton1.Text = "Кабинет администратора";
                materialLabel2.Text = "";
            }
            else
            materialLabel2.Text = "Баланс: " + Convert.ToString(balance) + " руб";

        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (isAdmin == 0)
            {
                Personal form = new Personal(login);
                form.Show();            }
            else {
                AdminPanel form = new AdminPanel();
                form.Show();
            }
        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            AboutSession form = new AboutSession(Convert.ToInt16(listView1.SelectedItems[0].Tag), dateTimePicker1.Value.ToString("dd.MM.yyyy"), balance, login);
            form.Show();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            imageList1.Images.Clear();
            listView1.Items.Clear();
            DB.FilmsLoad(imageList1, listView1, dateTimePicker1.Value.ToString("dd.MM.yyyy"));
        }
    }
}
