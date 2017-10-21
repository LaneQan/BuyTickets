using System;
using System.Windows;
using BuyTickets.DB;
using MaterialSkin;
using MaterialSkin.Controls;

namespace BuyTickets.Forms
{
    public partial class AddCinema : MaterialForm
    {
        public AddCinema()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void AddCinema_Load(object sender, EventArgs e)
        {
            var list = Database.GetAllCinemas();
            foreach (var k in list)
                listBox1.Items.Add(k);
        }


        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text != "")
            {
                Database.AddCinema(materialSingleLineTextField1.Text);
                listBox1.Items.Add(materialSingleLineTextField1.Text);
                materialSingleLineTextField1.Text = null;
                MessageBox.Show("Кинотеатр успешно добавлен!");

            }
            else MessageBox.Show("Введите название кинотеатра");
        }
    }
}
