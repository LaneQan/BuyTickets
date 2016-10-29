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
using System.Data.SQLite;


namespace BuyTickets
{

    public partial class Main : MaterialForm
    {
        public void SessionsLoad()
        {

            const string databaseName = @"..\..\DB\BT.sqlite";
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Sessions;", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string path = @"..\..\Images\" + reader["Id"].ToString() + ".jpg";
                imageList1.Images.Add(reader["Name"].ToString(), Image.FromFile(path));
            }
            reader.Close();
            connection.Close();
            listView1.LargeImageList = imageList1;
            for (int i = 0; i < imageList1.Images.Count; i++)
            {
                listView1.Items.Add(imageList1.Images.Keys[i].ToString()).ImageIndex = i;
            }
        }

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
            SessionsLoad();


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
