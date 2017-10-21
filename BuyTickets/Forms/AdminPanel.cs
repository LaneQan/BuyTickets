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

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            AddSession form = new AddSession();
            form.Show();
        }

        private void materialFlatButton5_Click(object sender, EventArgs e)
        {
            AddCinema form = new AddCinema();
            form.Show();
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            AddFilm form = new AddFilm();
            form.Show();
        }
    }
}