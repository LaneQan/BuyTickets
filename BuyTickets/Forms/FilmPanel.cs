using BuyTickets.DB;
using BuyTickets.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BuyTickets.Forms
{
    public partial class FilmPanel : MaterialForm
    {
        public string FilePath;
        private List<Film> filmList;
        private Film currentFilm;

        public FilmPanel()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void AddFilm_Load(object sender, EventArgs e)
        {
            filmList = Database.GetAllFilms();
            foreach (var k in filmList.OrderByDescending(x => x.Id).ToList())
                listBox1.Items.Add(k.Name);
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

        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                currentFilm = filmList[listBox1.SelectedIndex];
                richTextBox1.Text = currentFilm.Description;
                materialSingleLineTextField1.Text = currentFilm.Name;
                pictureBox1.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory +@"\Images\"+ currentFilm.Image);
            }
            else MessageBox.Show(@"Не выбран фильм");
        }

        private void materialRaisedButton6_Click(object sender, EventArgs e)
        {
            if (currentFilm != null)
            {
                currentFilm.Name = materialSingleLineTextField1.Text;
                currentFilm.Description = richTextBox1.Text;
                Database.UpdateFilm(currentFilm);
                MessageBox.Show(@"Успешно сохранено!");
            }
            else MessageBox.Show(@"Не выбран фильм");
        }
    }
}