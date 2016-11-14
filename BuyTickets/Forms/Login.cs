﻿using System;
using MaterialSkin.Controls;
using MaterialSkin;
using BuyTickets.DB;
using System.Windows.Forms;

namespace BuyTickets
{
    public partial class Login : MaterialForm
    {
        int isAdmin = 0;
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
            DB.Auth(materialSingleLineTextField1.Text, materialSingleLineTextField2.Text, out isAdmin, out success);
            if (success)
            {
                Main form = new Main(this.isAdmin);
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
