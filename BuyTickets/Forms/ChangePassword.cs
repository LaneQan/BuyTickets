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
    public partial class ChangePassword : MaterialForm
    {
        private User _user;
        public ChangePassword(User user)
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            _user = user;
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (_user.Password == Database.Md5Crypt(materialSingleLineTextField1.Text))
            {
                Database.ChangePassword(_user, materialSingleLineTextField2.Text);
                MessageBox.Show("Пароль успешно изменен!");
                materialSingleLineTextField1.Text = null;
                materialSingleLineTextField2.Text = null;
            }
            else MessageBox.Show("Вы ввели неверный пароль");
        }
    }
}
