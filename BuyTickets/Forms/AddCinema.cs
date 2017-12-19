using System;
using System.Windows.Forms;
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
                listBox1.Items.Add(k.Name);
        }


        private void addCinemaButton_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text != "")
            {
                if (!Database.CinemaOnDatabase(materialSingleLineTextField1.Text))
                {
                    Database.AddCinema(materialSingleLineTextField1.Text);
                    listBox1.Items.Add(materialSingleLineTextField1.Text);
                    materialSingleLineTextField1.Text = null;
                    MessageBox.Show("Кинотеатр успешно добавлен!");
                }
                else MessageBox.Show("Кинотеатр уже в списке");

            }
            else MessageBox.Show("Введите название кинотеатра");
        }

        private void EditCinemaButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex!=-1)
            {
                materialSingleLineTextField2.Text = listBox1.SelectedItem.ToString(); 
                materialSingleLineTextField2.Visible = true;
                materialRaisedButton1.Visible = true;
            }
            else MessageBox.Show("Не выбран кинотеатр");
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            Database.ChangeCinemasName(listBox1.SelectedItem.ToString(), materialSingleLineTextField2.Text);
            materialSingleLineTextField2.Visible = false;
            materialRaisedButton1.Visible = false;
            listBox1.Items[listBox1.SelectedIndex] = materialSingleLineTextField2.Text;
            MessageBox.Show("Название успешно изменено!");
        }
    }
}
