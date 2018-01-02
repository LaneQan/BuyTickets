using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuyTickets.DB;
using BuyTickets.Models;
using MaterialSkin;
using MaterialSkin.Controls;

namespace BuyTickets.Forms
{
    public partial class ChangeEmail : MaterialForm
    {
        private User _user;

        public ChangeEmail(User user)
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            this._user = user;
        }

        private void ChangeEmail_Load(object sender, EventArgs e)
        {
            materialSingleLineTextField1.Text = _user.Mail;
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (_user.Mail != materialSingleLineTextField1.Text)
            {

                if (Database.ChangeMail(_user, materialSingleLineTextField1.Text))
                {
                    MessageBox.Show("E-mail был успешно изменен");
                }
                else MessageBox.Show("E-mail не был изменен! Он уже занят");

            }
            else MessageBox.Show("Введите новый e-mail");
        }
    }
}
