using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public List<Film> FilmList;
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
            foreach (var k in FilmList)
                comboBox1.Items.Add(k.Name);
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {

        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            pictureBox1.Image = Image.FromFile(path+"/Images/"+FilmList[comboBox1.SelectedIndex].Image);
        }
    }
}
