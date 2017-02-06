using System;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace BuyTickets.Forms
{
    public partial class Personal : MaterialForm
    {
        private void SetHeight(ListView listView, int height)
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, height);
            listView.SmallImageList = imgList;
        }
        public Personal()
        {
            InitializeComponent();
            // Подключение MaterialSkin'a
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void Personal_Load(object sender, EventArgs e)
        {
            SetHeight(listView1, 25);
        }

        private void Personal_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
