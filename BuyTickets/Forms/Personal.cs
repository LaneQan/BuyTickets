using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BuyTickets.DB;
using BuyTickets.Models;

namespace BuyTickets.Forms
{
    public partial class Personal : MaterialForm
    {
        private User _user;

        private void SetHeight(ListView listView, int height)
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, height);
            listView.SmallImageList = imgList;
        }

        public Personal(User user)
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            this._user = user;
        }

        private void Personal_Load(object sender, EventArgs e)
        {
             Font defaultFont = new Font("Microsoft Sans Serif", 16);
             SetHeight(listView1, 25);
             List<BalanceHistory> balanceHistoryList = Database.GetBalanceHistoryById(_user.Id);
             foreach (var k in balanceHistoryList)
             {
                 ListViewItem li = new ListViewItem();
                 li.Text = k.Action;
                 li.Font = defaultFont;
                 if (k.Key == 0)
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

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            ChangePassword form = new ChangePassword(_user);
            form.Show();
            this.Visible = false;
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            ChangeEmail form = new ChangeEmail(_user);
            form.Show();
            this.Visible = false;
        }
    }
}