using System.Data.Entity;

namespace BuyTickets.Models
{
    internal class BuyTicketsContext : DbContext
    {
        public BuyTicketsContext() : base("DbConnection")
        {
        }

        public DbSet<BalanceHistory> BalanceHistory { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OccSeat> OccSeats { get; set; }
    }
}