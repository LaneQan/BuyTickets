using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Runtime.InteropServices;


namespace BuyTickets
{

    public partial class Main : MaterialForm
    {

        public Main()
        {
            InitializeComponent();
            // Подключение MaterialSkin'a
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT; 
        }


        private void Main_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < imageList1.Images.Count; i++)
            {
                string name = imageList1.Images.Keys[i].ToString().Remove(imageList1.Images.Keys[i].ToString().IndexOf("."));
                listView1.Items.Add(name).ImageIndex = i;
            }


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
