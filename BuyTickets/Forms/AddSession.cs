using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuyTickets.DB;
using BuyTickets.Models;
using MaterialSkin;
using MaterialSkin.Controls;

namespace BuyTickets.Forms
{
    public partial class AddSession : MaterialForm
    {
        private List<Film> FilmList;
        private List<Cinema> CinemaList; 
        public AddSession()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void AddSession_Load(object sender, EventArgs e)
        {
            FilmList = Database.GetAllFilms();
            CinemaList = Database.GetAllCinemas();
            foreach (var k in FilmList)
                comboBox1.Items.Add(k.Name);
            foreach (var k in CinemaList)
                comboBox2.Items.Add(k.Name);
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            var film = FilmList.FirstOrDefault(x => x.Name== comboBox1.SelectedItem.ToString());
            var cinema = CinemaList.FirstOrDefault(x => x.Name == comboBox2.SelectedItem.ToString());
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
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                
            }
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                pictureBox1.Image = Image.FromFile(path + "/Images/" + FilmList[comboBox1.SelectedIndex].Image);
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
