namespace BuyTickets.Models
{
    internal class BalanceHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public string Change { get; set; }
        public string Date { get; set; }
        public int Key { get; set; }
    }
}