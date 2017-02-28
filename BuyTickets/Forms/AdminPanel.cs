using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace BuyTickets.Forms
{
    public partial class AdminPanel : MaterialForm
    {
        public AdminPanel()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
        }

        private void AdminPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            AddAdministator form = new AddAdministator();
            form.Show();
        }
    }
}