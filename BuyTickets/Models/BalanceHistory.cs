using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyTickets.Models
{
    class BalanceHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public string Action { get; set; }
        public string Change { get; set; }
        public string Date { get; set; }
        public int Key { get; set; }
    }
}
