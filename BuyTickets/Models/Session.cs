using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyTickets.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int CinemaId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public List<Place> Places { get; set; }
        public float Cost { get; set; }
    }
}
