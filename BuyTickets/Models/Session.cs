using System.Collections.Generic;

namespace BuyTickets.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int CinemaId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public float Cost { get; set; }
        public List<OccSeat> OccSeats { get; set; }

        public Session()
        {
            OccSeats = new List<OccSeat>();
        }

        public int GetOccSeatsCount()
        {
            return OccSeats.Count;
        }
    }
}