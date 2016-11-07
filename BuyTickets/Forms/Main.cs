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
        public Main()
        {
            InitializeComponent();
            // Подключение MaterialSkin'a
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT; 
        }


        private void Main_Load(object sender, EventArgs e)
        {
            Database DB = new Database();
            DB.SessionsLoad(imageList1, listView1);
            materialLabel1.Text += " ( " + DateTime.Now.ToString("dd.MM.y") + " )";

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
            Personal form = new Personal();
            form.Show();
            this.Visible = false;
        }
    }
}
