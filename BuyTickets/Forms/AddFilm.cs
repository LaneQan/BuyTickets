using System;
using System.Drawing;
using System.Windows.Forms;
using BuyTickets.DB;
using BuyTickets.Models;
using MaterialSkin;
using MaterialSkin.Controls;

namespace BuyTickets.Forms
{
    public partial class AddFilm : MaterialForm
    {
        public string FilePath;

        public AddFilm()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void AddFilm_Load(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            
                dlg.Title = "Выберите изображение";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(dlg.FileName);
                    FilePath = dlg.FileName;
                }

        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            FilePath = null;
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            SetNull();
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            Film film = new Film
            {
                Name = materialSingleLineTextField1.Text,
                Description = richTextBox1.Text,
                Image = FilePath.Substring(FilePath.LastIndexOf("Images") + 7, FilePath.Length - (FilePath.LastIndexOf("Images") + 7))
            };
            Database.AddFilm(film);
            MessageBox.Show("Фильм успешно добавлен!");
            SetNull();
        }

        public void SetNull()
        {
            pictureBox1.Image = null;
            materialSingleLineTextField1.Text = null;
            richTextBox1.Text = null;
            FilePath = null;
        }
    }
}
