using MaterialSkin;
using MaterialSkin.Controls;
using System;
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
            if (Database.ChangePermissions(materialSingleLineTextField1.Text, true))
                MessageBox.Show(@"Успешно изменено");
            else MessageBox.Show(@"Пользователь не найден");
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            if(Database.ChangePermissions(materialSingleLineTextField1.Text, false))
            MessageBox.Show(@"Успешно изменено");
            else MessageBox.Show(@"Пользователь не найден");
        }
    }
}