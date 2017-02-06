using System;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Windows.Forms;
using BuyTickets.DB;

namespace BuyTickets.Forms
{
    public partial class AddAdministator : MaterialForm
    {
        public AddAdministator()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void AddAdministator_Load(object sender, EventArgs e)
        {

        }

        private void AddAdministator_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {

        }

        private void materialSingleLineTextField1_Click(object sender, EventArgs e)
        {
            materialSingleLineTextField1.Text = null;
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            Database DB = new Database();
            if (DB.userOnBase(materialSingleLineTextField1.Text, "xx"))
            {
                if (DB.isAdmin(materialSingleLineTextField1.Text))
                {
                    DB.changePermissions(materialSingleLineTextField1.Text, 0);
                    MessageBox.Show("Права изменены с 'Администратор' на 'Пользователь'!");
                }
                else
                {
                    DB.changePermissions(materialSingleLineTextField1.Text, 1);
                    MessageBox.Show("Права изменены с 'Пользователь' на 'Администратор'!");
                }
            }
            else MessageBox.Show("Данный пользователь не зарегистрирован!");

        }

    }
}
