using BuyTickets.DB;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BuyTickets.Forms
{
    public partial class Personal : MaterialForm
    {
        private string login;
        private void SetHeight(ListView listView, int height)
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, height);
            listView.SmallImageList = imgList;
        }

        public Personal(string login)
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            this.login = login;
        }


        private void Personal_Load(object sender, EventArgs e)
        {
            materialLabel1.Text = "Добро пожаловать, " + login + "!";
            Font defaultFont = new Font("Microsoft Sans Serif", 12);
            Database DB = new Database();
            SetHeight(listView1, 25);
            string[,] toList = DB.getBalanceHistory(login);
            for (int i=0;i<toList.GetLength(0);i++)
            {
                ListViewItem li = new ListViewItem();
                li.Text = toList[i, 0] + " | " + toList[i, 1] + " | " + toList[i, 2];
                li.Font = defaultFont;
                if (toList[i, 3] == "0")
                    li.ForeColor = Color.Red;
                else li.ForeColor = Color.Green;
                listView1.Items.Add(li);
            }


        }

        private void Personal_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
        }
    }
}