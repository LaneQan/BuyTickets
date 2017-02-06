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
        int isAdmin = 0;
        int balance = 0;
        public Main(int isAdmin, int balance)
        {
            InitializeComponent();
            // Подключение MaterialSkin'a
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            this.isAdmin = isAdmin;
            this.balance = balance;

        }


        private void Main_Load(object sender, EventArgs e)
        {
            Database DB = new Database();
            DB.SessionsLoad(imageList1, listView1);
            if (isAdmin==1)
            {
                materialRaisedButton1.Text = "Кабинет администратора";
                materialLabel2.Text = "";
            }
            materialLabel2.Text = "Баланс: " + Convert.ToString(balance) + " руб";

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
                form.Show();            }
            else {
                AdminPanel form = new AdminPanel();
                form.Show();
            }
        }
    }
}
