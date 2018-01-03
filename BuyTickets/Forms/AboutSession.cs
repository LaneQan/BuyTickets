using BuyTickets.DB;
using BuyTickets.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BuyTickets.Forms
{
    public partial class AboutSession : MaterialForm
    {
        private int NumberOfTickets;

        private Film film;
        private List<Session> sessions;
        private Session currentSession;
        private User user;
        private string date;

        public AboutSession(int filmId, string date, User user)
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            film = Database.GetFilmById(filmId);
            this.date = date;
            sessions = Database.GetSessionsByIdAndDate(filmId, date);
            NumberOfTickets = 0;
            this.user = user;
        }

        private void AboutSession_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"\Images\" + film.Image);
            var input = film.Description;
            var lines_1 = Regex.Split(input, @"(.{1," + 55 + @"})(?:\s|$)")
                        .Where(x => x.Length > 0)
                        .Select(x => x.Trim()).ToArray(); ;
            listBox1.Items.AddRange(lines_1);
            foreach (var p in sessions.GroupBy(t => t.CinemaId).Select(x => x.First()).ToList())
                comboBox1.Items.Add(Database.GetCinemaNameById(p.CinemaId));
        }

        private void SetAllComponentsNull(Panel panel)
        {
            sessions = Database.GetSessionsByIdAndDate(film.Id, date);
            NumberOfTickets = 0;
            foreach (Control ctrl in panel.Controls)
            {
                ctrl.BackColor = Color.White;
            }
            materialLabel3.Text = @"Количество билетов: 0 шт.";
            materialLabel4.Text = @"Общая цена: 0 руб.";
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            DateTime sessionDateTime = DateTime.ParseExact(currentSession.Date + " " + currentSession.Time, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

            if (DateTime.Now < sessionDateTime)
            {
                if (NumberOfTickets != 0)
                {
                    if (user.Balance >= NumberOfTickets * currentSession.Cost)
                    {
                        var result = MessageBox.Show(@"Количество билетов: " + NumberOfTickets+
                                                     @".\nСумма к оплате: " +
                                                     Convert.ToString(NumberOfTickets * currentSession.Cost) +
                                                     @" руб.\nВы подтверждаете покупку?", @"Подтверждение покупки",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            List<string> btnList = new List<string>();
                            foreach (Control ctrl in panel1.Controls)
                            {
                                if (ctrl.BackColor == Color.Green)
                                    btnList.Add(ctrl.Name);
                            }
                            Database.UpdateSeatsBalanceAndHistory(user, currentSession, NumberOfTickets, btnList);
                            materialLabel3.Text = @"Количество билетов: 0 шт.";
                            materialLabel4.Text = @"Общая цена: 0 руб.";
                            foreach (Control ctrl in panel1.Controls)
                            {
                                if (ctrl.BackColor == Color.Green)
                                {
                                    ctrl.BackColor = Color.Red;
                                    ctrl.Enabled = false;
                                }
                            }
                            Main fc = (Main) Application.OpenForms["Main"];
                            if (fc != null)
                            {
                                fc.balance.Text = @"Баланс: " + user.Balance + @" руб";
                            }
                            MessageBox.Show(@"Благодарим за покупку!");
                        }
                    }
                    else MessageBox.Show(@"Недостаточно денег на балансе!");
                }
                else MessageBox.Show(@"Выберите места для покупки!");
            }
            else MessageBox.Show(@"Вы не можете приобрести билеты на данный сеанс!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetAllComponentsNull(panel1);
            if (panel1.Visible)
                panel1.Visible = false;
            sessions = sessions.Where(x => x.CinemaId == Database.GetIdByCinemaName(comboBox1.SelectedItem.ToString()))
                .ToList();
            List<string> time = sessions.Select(x => x.Time).ToList();
            comboBox2.Items.Clear();
            foreach (string p in time)
            {
                comboBox2.Items.Add(p);
            }
            comboBox2.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetAllComponentsNull(panel1);
            if (!panel1.Visible)
                panel1.Visible = true;
            materialRaisedButton1.Visible = true;
            currentSession = sessions.First(x => x.Time == comboBox2.SelectedItem.ToString());
            if (currentSession.OccSeats != null)
            {
                foreach (var seat in currentSession.OccSeats)
                {
                    panel1.Controls[seat.Name].Enabled = false;
                    panel1.Controls[seat.Name].BackColor = Color.Red;
                }
            }
        }


        private void onClick(object sender, EventArgs e)
        {
             var myButton = (Button)sender;
             Color color = myButton.BackColor;
             if (color == Color.Green)
             {
                 myButton.BackColor = Color.White;
                 NumberOfTickets--;
                 materialLabel3.Text = @"Количество билетов: " + Convert.ToString(NumberOfTickets) + @" шт.";
                 materialLabel4.Text = @"Общая цена: " + Convert.ToString(NumberOfTickets * currentSession.Cost) + @" руб.";
             }
             else
             {
                 myButton.BackColor = Color.Green;
                 NumberOfTickets++;
                 materialLabel3.Text = "Количество билетов: " + Convert.ToString(NumberOfTickets) + " шт.";
                 materialLabel4.Text = "Общая цена: " + Convert.ToString(NumberOfTickets * currentSession.Cost) + " руб.";
             }
        }
    }
}