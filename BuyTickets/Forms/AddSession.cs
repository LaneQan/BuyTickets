using BuyTickets.DB;
using BuyTickets.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace BuyTickets.Forms
{
    public partial class AddSession : MaterialForm
    {
        private List<Film> _filmList;
        private List<Cinema> _cinemaList;

        public AddSession()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void AddSession_Load(object sender, EventArgs e)
        {
            _filmList = Database.GetAllFilms();
            _cinemaList = Database.GetAllCinemas();
            foreach (var k in _filmList)
                comboBox1.Items.Add(k.Name);
            foreach (var k in _cinemaList)
                comboBox2.Items.Add(k.Name);
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            var film = _filmList.FirstOrDefault(x => x.Name == comboBox1.SelectedItem.ToString());
            var cinema = _cinemaList.FirstOrDefault(x => x.Name == comboBox2.SelectedItem.ToString());
            string time = materialSingleLineTextField1.Text;
            float cost = float.Parse(materialSingleLineTextField2.Text, CultureInfo.InvariantCulture.NumberFormat);
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
            {
                var session = new Session
                {
                    FilmId = film.Id,
                    CinemaId = cinema.Id,
                    Time = time,
                    Cost = cost,
                    Date = dateTimePicker1.Value.ToString("dd-MM-yyyy")
                };
                Database.AddSession(session);
                MessageBox.Show(@"Успешно добавлено!");
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                var currentDate = dateTimePicker2.Value;
                while (currentDate.Day != dateTimePicker3.Value.Day+1)
                {
                    var session = new Session
                    {
                        FilmId = film.Id,
                        CinemaId = cinema.Id,
                        Time = time,
                        Cost = cost,
                        Date = currentDate.ToString("dd-MM-yyyy")
                    };
                    Database.AddSession(session);
                    currentDate = currentDate.AddDays(1);
                }
                MessageBox.Show(@"Успешно добавлено!");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                pictureBox1.Image = Image.FromFile(path + "Images/" + _filmList[comboBox1.SelectedIndex].Image);
            }
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            materialSingleLineTextField1.Text = null;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void materialSingleLineTextField1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != ':'))
            {
                e.Handled = true;
            }
        }
    }
}