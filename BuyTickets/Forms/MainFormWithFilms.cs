﻿using BuyTickets.DB;
using BuyTickets.Forms;
using BuyTickets.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace BuyTickets
{
    public partial class Main : MaterialForm
    {
        private User _user;

        public Main(User user)
        {
            InitializeComponent();
            // Подключение MaterialSkin'a
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            _user = user;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (_user.IsAdmin)
            {
                materialRaisedButton1.Text = "Кабинет администратора";
                balance.Text = "";
            }
            else
                balance.Text = "Баланс: " + Convert.ToString(_user.Balance) + " руб";

            Database.FilmsLoad(imageList1, listView1, DateTime.Today.ToString("dd-MM-yyyy"));
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (!_user.IsAdmin)
            {
                Personal form = new Personal(_user);
                form.Show();
            }
            else
            {
                AdminPanel form = new AdminPanel();
                form.Show();
            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            SessionInfo form = new SessionInfo(Convert.ToInt16(listView1.SelectedItems[0].Tag), dateTimePicker1.Value.ToString("dd-MM-yyyy"), _user);
            form.Show();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            imageList1.Images.Clear();
            listView1.Items.Clear();
            Database.FilmsLoad(imageList1, listView1, dateTimePicker1.Value.ToString("dd-MM-yyyy"));
        }
    }
}