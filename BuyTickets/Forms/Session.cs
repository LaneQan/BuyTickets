using MaterialSkin;
using MaterialSkin.Controls;
using System;

namespace BuyTickets.Forms
{
    public partial class Session : MaterialForm
    {
        public Session()
        {
            InitializeComponent();
            // Подключение MaterialSkin'a
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void Session_Load(object sender, EventArgs e)
        {
        }
    }
}