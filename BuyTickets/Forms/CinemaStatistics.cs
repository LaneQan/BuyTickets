using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using BuyTickets.DB;
using BuyTickets.Models;
using MaterialSkin;
using MaterialSkin.Controls;

namespace BuyTickets.Forms
{
    public partial class CinemaStatistics : MaterialForm
    {
        private List<Session> allHistory;
        private List<Session> historyByDate;
        private List<Cinema> allCinemas;

        public CinemaStatistics()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            allHistory = Database.GetAllSessions();
            allCinemas = Database.GetAllCinemas();
        }

        private void CinemaStatistics_Load(object sender, EventArgs e)
        {
            Dictionary<string, double> dictionaryForAllDates = new Dictionary<string, double>();
            if (allHistory != null)
            {
                foreach (var k in allHistory)
                {
                    if (!dictionaryForAllDates.ContainsKey(allCinemas.FirstOrDefault(x => x.Id == k.CinemaId).Name))
                        dictionaryForAllDates.Add(allCinemas.FirstOrDefault(x => x.Id == k.CinemaId).Name, k.GetOccSeatsCount());
                    else dictionaryForAllDates[allCinemas.FirstOrDefault(x => x.Id == k.CinemaId).Name] += k.GetOccSeatsCount();
                }

                int count = allHistory.Sum(x => x.GetOccSeatsCount());
                var keys = new List<string>(dictionaryForAllDates.Keys);
                foreach (string key in keys)
                {
                    dictionaryForAllDates[key] = Math.Round(dictionaryForAllDates[key] / count*100, 2);
                }

                chart1.Series["Sdata"].Points.DataBindXY(
                    dictionaryForAllDates.Select(X => X.Key).ToArray(),
                    dictionaryForAllDates.Select(Y => Y.Value).ToArray());
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           LoadChart2(dateTimePicker1.Value.ToString("dd-MM-yyyy"));
        }

        public void LoadChart2(string date)
        {
            historyByDate = allHistory.Where(x => x.Date == date).ToList();

            Dictionary<string, double> dictionaryForOneDate = new Dictionary<string, double>();
                foreach (var k in historyByDate)
                {
                    if (!dictionaryForOneDate.ContainsKey(allCinemas.FirstOrDefault(x => x.Id == k.CinemaId).Name))
                        dictionaryForOneDate.Add(allCinemas.FirstOrDefault(x => x.Id == k.CinemaId).Name,
                            k.GetOccSeatsCount());
                    else
                        dictionaryForOneDate[allCinemas.FirstOrDefault(x => x.Id == k.CinemaId).Name] +=
                            k.GetOccSeatsCount();
                }

                int count = historyByDate.Sum(x => x.GetOccSeatsCount());
                var keys = new List<string>(dictionaryForOneDate.Keys);
                foreach (string key in keys)
                {
                    dictionaryForOneDate[key] = Math.Round(dictionaryForOneDate[key] / count * 100, 2);
                }

                chart2.Series["Sdata"].Points.DataBindXY(
                    dictionaryForOneDate.Select(X => X.Key).ToArray(),
                    dictionaryForOneDate.Select(Y => Y.Value).ToArray());
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                LoadChart2(DateTime.Now.ToString("dd-MM-yyyy"));
            }
        }
    }
}
