using BuyTickets.DB;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BuyTickets.Forms
{
    public partial class AboutSession : MaterialForm
    {
        private int NumberOfTickets = 0;
        private int price = 0;
        private int id;
        private int balance;
        private string login;
        private string date;
        private Database DB = new Database();

        public AboutSession(int id, string date, int balance, string login)
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            this.id = id;
            this.date = date;
            this.balance = balance;
            this.login = login;
        }

        private void AboutSession_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(@"..\..\Images\" + id + ".jpg");
            var input = DB.returnDescript(id);
            var lines_1 = Regex.Split(input, @"(.{1," + 55 + @"})(?:\s|$)")
                        .Where(x => x.Length > 0)
                        .Select(x => x.Trim()).ToArray(); ;
            listBox1.Items.AddRange(lines_1);
            List<string> cinemas = DB.CinemasLoad(id, date);
            foreach (string p in cinemas)
                comboBox1.Items.Add(p);
        }

        private void setAllComponentsNull(Panel panel)
        {
            price = 0;
            NumberOfTickets = 0;
            foreach (Control ctrl in panel.Controls)
            {
                ctrl.BackColor = Color.White;
            }
            materialLabel3.Text = "Количество билетов: 0 шт.";
            materialLabel4.Text = "Общая цена: 0 руб.";
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (NumberOfTickets != 0)
            {
                if (balance >= NumberOfTickets * price)
                {
                    var result = MessageBox.Show("Количество билетов: " + NumberOfTickets.ToString() + ".\nСумма к оплате: " +
                        Convert.ToString(NumberOfTickets * price) + " руб.\nВы подтверждаете покупку?", "Подтверждение покупки", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        List<string> btnList = new List<string>();
                        foreach (Control ctrl in panel1.Controls)
                        {
                            if (ctrl.BackColor == Color.Green)
                                btnList.Add(ctrl.Name);
                        }
                        DB.UpdateSeatsBalanceAndHistory(login, NumberOfTickets, balance, NumberOfTickets * price, btnList, id, date, Convert.ToString(comboBox1.SelectedItem), Convert.ToString(comboBox2.SelectedItem));
                        materialLabel3.Text = "Количество билетов: 0 шт.";
                        materialLabel4.Text = "Общая цена: 0 руб.";
                        foreach (Control ctrl in panel1.Controls)
                        {
                            if (ctrl.BackColor == Color.Green)
                                ctrl.BackColor = Color.Red;
                        }
                        MessageBox.Show("Благодарим за покупку!");
                    }
                }
                else MessageBox.Show("Недостаточно денег на балансе!");

            }
            else MessageBox.Show("Выберите места для покупки!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setAllComponentsNull(panel1);
            if (panel1.Visible)
                panel1.Visible = false;
            List<string> time = DB.TimeLoad(id, date, Convert.ToString(comboBox1.SelectedItem)); 
            comboBox2.Items.Clear();
            foreach (string p in time)
            {
                comboBox2.Items.Add(p);
            }
            comboBox2.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            setAllComponentsNull(panel1);
            if (!panel1.Visible)
                panel1.Visible = true;
            materialRaisedButton1.Visible = true;
            price = DB.GetPrice(id, date, Convert.ToString(comboBox1.SelectedItem), Convert.ToString(comboBox2.SelectedItem));
            List<string> list = new List<string>(getOccSeats());
            foreach (string seat in list)
            {
                panel1.Controls[seat].Enabled = false;
                panel1.Controls[seat].BackColor = Color.Red;

            }

        }
        private List<string> getOccSeats()
        {
            string seats = DB.OccSeats(id, date, Convert.ToString(comboBox1.SelectedItem));
            List<string> list = new List<string>();
            if (seats != "0")
            {
                string seat = "";
                for (int i = 0; i < seats.Length; i++)
                    if (seats[i] != ';')
                        seat += seats[i];
                    else
                    {
                        list.Add(seat);
                        seat = "";
                    }

            }
            else list.Add("0");
            return list;
        }

        private void onClick(object sender, EventArgs e)
        {
            var myButton = (Button)sender;
            Color color = myButton.BackColor;
            if (color == Color.Green)
            {
                myButton.BackColor = Color.White;
                NumberOfTickets--;
                materialLabel3.Text = "Количество билетов: " + Convert.ToString(NumberOfTickets) + " шт.";
                materialLabel4.Text = "Общая цена: " + Convert.ToString(NumberOfTickets * price) + " руб.";
            }
            else
            {
                myButton.BackColor = Color.Green;
                NumberOfTickets++;
                materialLabel3.Text = "Количество билетов: " + Convert.ToString(NumberOfTickets) + " шт.";
                materialLabel4.Text = "Общая цена: " + Convert.ToString(NumberOfTickets * price) + " руб.";
            }
        }
    }
}