using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyTickets.Models
{
    class BuyTicketsContext : DbContext
    {
        public BuyTicketsContext(): base("DbConnection") { }
        public DbSet<BalanceHistory> BalanceHistory { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
