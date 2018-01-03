using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuyTickets.Models;

namespace BuyTickets.DB
{
    public static class Database
    {
        private static BuyTicketsContext _db = new BuyTicketsContext();

        public static User Auth(string login, string password)
        {
            password = Md5Crypt(password);

            //var user2 = _db.Users.FirstOrDefault(x => x.Login == "useruser");
            //user2.Balance = 100;
            //user2.IsAdmin = false;
            //_db.Entry(user2).State = EntityState.Modified;
            //var user3 = _db.Users.FirstOrDefault(x => x.Login == "adminadmin");
            //user3.IsAdmin = true;
            //_db.Entry(user3).State = EntityState.Modified;
            _db.SaveChanges();

            var user = _db.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            return user;
        }

        public static void FilmsLoad(ImageList imageList, ListView listView, string date)
        {
            List<Session> sessions = _db.Sessions.Where(x => x.Date == date).ToList();
            var results = sessions.GroupBy(x => x.FilmId).Select(y => y.First()).ToList();
            List<Film> films = new List<Film>();
            foreach (var s in results)
                films.Add(_db.Films.FirstOrDefault(x => x.Id == s.FilmId));
            int k = 0;
            foreach (var b in films)
            {
                imageList.Images.Add(b.Name,
                    Image.FromFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\Images\" + b.Image));

                listView.Items.Add(imageList.Images.Keys[k]).ImageIndex = k;
                listView.Items[k].Tag = b.Id;
                k++;
            }
        }

        public static async void Registration(User user)
        {
            user.Password = Md5Crypt(user.Password);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }


        public static string Md5Crypt(string toCrypt)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(toCrypt);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));
            return sb.ToString();
        }


        public static bool ChangePermissions(string login, bool isAdmin)
        {
            var user = _db.Users.FirstOrDefault(x => x.Login == login);
            if (user == null) return false;
            else
            {
                user.IsAdmin = isAdmin;
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
                return true;
            }
        }


        

         public static void UpdateSeatsBalanceAndHistory(User user, Session session, int ticketsCount, List<string> btnList)
         {
      
            foreach (string k in btnList)
             {
                 session.OccSeats.Add(new OccSeat { Name = k});
             }
             _db.BalanceHistory.Add(new BalanceHistory
             {
                 Action = "Покупка билетов в количестве " + ticketsCount + " шт. на сеанс '" + session.Date + " " +
                          GetFilmById(session.FilmId).Name + " " + session.Time + "'",
                 Date = session.Date,
                 Key = 0,
                 Change = "-" + session.Cost * ticketsCount + " руб.",
                 UserId = user.Id
             });
             user.Balance -= session.Cost * ticketsCount;
             _db.Entry(user).State = EntityState.Modified;
             _db.Entry(session).State = EntityState.Modified;
             _db.SaveChanges();
        }
        
         public static List<BalanceHistory> getBalanceHistoryById(int id)
         {
             return _db.BalanceHistory.Where(x => x.UserId == id).ToList();
         }

        public static async void AddCinema(string name)
        {
            Cinema cinema = new Cinema {Name = name};
            _db.Cinemas.Add(cinema);
            await _db.SaveChangesAsync();
        }

        public static async void AddFilm(Film film)
        {
            _db.Films.Add(film);
            await _db.SaveChangesAsync();
        }

        public static List<Cinema> GetAllCinemas()
        {
            return _db.Cinemas.ToList();
        }

        public static List<Film> GetAllFilms()
        {
            return _db.Films.ToList();
        }

        public static bool CinemaOnDatabase(string name)
        {
            if (_db.Cinemas.FirstOrDefault(t => t.Name == name) != null)
                return true;
            else return false;
        }

        public static async void ChangeCinemasName(string name, string newName)
        {
            var cinema = await _db.Cinemas.FirstOrDefaultAsync(x => x.Name == name);
            cinema.Name = newName;
            _db.Entry(cinema).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public static  void AddSession(Session session)
        {
            _db.Sessions.Add(session);
            _db.SaveChanges();
        }

        public static Film GetFilmById(int id)
        {
            return _db.Films.FirstOrDefault(x => x.Id == id);
        }

        public static List<Session> GetSessionsByIdAndDate(int id, string date)
        {
            return _db.Sessions.Include(x => x.OccSeats).Where(x => x.FilmId == id && x.Date == date).ToList();
        }

        public static string GetCinemaNameById(int id)
        {
            return _db.Cinemas.FirstOrDefault(x => x.Id == id).Name;
        }

        public static int GetIdByCinemaName(string name)
        {
            return _db.Cinemas.FirstOrDefault(x => x.Name == name).Id;
        }

        public static bool ChangeMail(User user, string newMail)
        {
            if (_db.Users.Where(x => x.Mail == newMail) != null)
            {
                user.Mail = newMail;
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
                return true;
            }
            else return false;
        }
        public static void ChangePassword(User user, string newPassword)
        {
                user.Password = Md5Crypt(newPassword);
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
        }

        public static void UpdateFilm(Film film)
        {
            _db.Entry(film).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}