using BuyTickets.Models;
using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace BuyTickets
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Database.SetInitializer<BuyTicketsContext>(new DropCreateDatabaseIfModelChanges<BuyTicketsContext>());
            Application.Run(new Login());
        }
    }
}