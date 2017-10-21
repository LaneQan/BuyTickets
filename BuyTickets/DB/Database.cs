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
        static BuyTicketsContext dbc = new BuyTicketsContext();

        public static User Auth(string login, string password)
        {
            password = Md5Crypt(password);
            var user = dbc.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            return user;
        }

        /*public void FilmsLoad(ImageList imageList, ListView listView, string date)
        {
            List<int> FilmId = new List<int>();
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Film_Id FROM Sessions WHERE Date='" + date + "' GROUP BY Film_id;", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                FilmId.Add(Convert.ToInt16(reader[0]));
            }
            reader.Close();
            cmd = new SQLiteCommand("SELECT * FROM Films;", connection);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string path = @"..\..\Images\" + reader["Id"].ToString() + ".jpg";
                imageList.Images.Add(reader["Name"].ToString(), Image.FromFile(path));
            }
            reader.Close();
            connection.Close();
            int k = 0;
            foreach (int p in FilmId)
            {
                listView.Items.Add(imageList.Images.Keys[p].ToString()).ImageIndex = p;
                listView.Items[k].Tag = p;
                k++;
            }
        }*/

        public static async void Registration(User user)
        {
            user.Password = Md5Crypt(user.Password);
            dbc.Users.Add(user);
            await dbc.SaveChangesAsync();
        }

        public static bool UserOnBase(string login, string mail)
        {
            var user = dbc.Users.FirstOrDefault(x => x.Login == login || x.Mail == mail);
            if (user != null)
                return true;
            else return false;
        }

        private static string Md5Crypt(string toCrypt)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(toCrypt);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));
            return sb.ToString();
        }

        public static async Task<bool> IsAdmin(string login)
        {
            var user = await dbc.Users.FirstOrDefaultAsync(x => x.Login == login);
            return user.IsAdmin;
        }

        public static async void ChangePermissions(string login, bool isAdmin)
        {
            var user = await dbc.Users.FirstOrDefaultAsync(x => x.Login == login);
            user.IsAdmin = isAdmin;
            dbc.Entry(user).State = EntityState.Modified;
            await dbc.SaveChangesAsync();
        }

        public static async Task<string> ReturnDescription(int id)
        {
            var film = await dbc.Films.FirstOrDefaultAsync(x => x.Id == id);
            return film.Name + "\n\n" + film.Description;
        }

        /*public static async Task<List<string>> CinemasLoad(int FilmId, string date) // переделать LINQ запрос в сложный
        {
            var cinemasIds = await dbc.Sessions.Where(x => x.FilmId == FilmId && x.Date == date).Select(x => x.CinemaId).ToListAsync();
            return list;
        }*/

        /* public List<string> TimeLoad(int id, string date, string cinema)
         {
             List<string> list = new List<string>();;
             SQLiteCommand cmd = new SQLiteCommand("SELECT Sessions.Time FROM Cinemas, Sessions WHERE Sessions.Date='" + date + "' AND Sessions.Film_Id=" + id + " AND Sessions.Cinemas_Id=(SELECT Id FROM Cinemas WHERE Name='"+cinema+"') GROUP BY Cinemas_Id", connection);
                     list.Add(Convert.ToString(reader["Time"]));
             return list;
         }

         public string OccSeats(int id, string date, string cinema)
         {
             string places = "";
             connection.Open();
             SQLiteCommand cmd = new SQLiteCommand("SELECT Sessions.Places FROM Cinemas, Sessions WHERE Sessions.Date='" + date + "' AND Sessions.Film_Id=" + id + " AND Sessions.Cinemas_Id=(SELECT Id FROM Cinemas WHERE Name='" + cinema + "') GROUP BY Cinemas_Id", connection);
             SQLiteDataReader reader = cmd.ExecuteReader();
             while (reader.Read())
             {
                 places = Convert.ToString(reader["Places"]);
             }
             reader.Close();
             connection.Close();
             return places;
         }

         public static float GetPrice(int id, string date, string cinema, string time)
         {
             int cinemaId = dbc.Cinemas.Where(x => x.Name == cinema).Select(x => x.Id).First();
             return dbc.Sessions.Where(x =>
                 x.Date == date
                 && x.FilmId == id
                 && x.CinemaId == cinemaId).Select(x => x.Cost).First();
         }

         public void UpdateSeatsBalanceAndHistory(string login, int numberOfTickets, int oldBalance, int ticketCosts, List<string> seatList, int id, string date, string cinema, string time)
         {
             string filmName = "";
             connection.Open();
             SQLiteCommand cmd = new SQLiteCommand("UPDATE Users SET Balance=" + Convert.ToString(oldBalance-numberOfTickets*ticketCosts)+" WHERE Login='"+login+"';", connection); 
             cmd.ExecuteNonQuery();
             cmd = new SQLiteCommand("SELECT Name FROM Films WHERE ID =" + id+" ;", connection);
             SQLiteDataReader reader = cmd.ExecuteReader();
             while (reader.Read())
             {
                 filmName = Convert.ToString(reader[0]);
             }
             reader.Close();
             string seats = string.Join(";", seatList.ToArray()) + ";";
             cmd = new SQLiteCommand("UPDATE Sessions SET Places = Places || '" + seats + "' WHERE Date='" + date + "' and Film_ID=" + id  +
                 " and Cinemas_Id=(SELECT Id From Cinemas WHERE Cinemas.Name='" + cinema + "') and Time='" + time + "';", connection); 
             cmd.ExecuteNonQuery();
             cmd = new SQLiteCommand(("INSERT INTO Balance_History (User_login, Action, Change, Date, Key)"
             + "VALUES (@User_login, @Action, @Change, @Date, @Key);"), connection);
             cmd.Parameters.AddWithValue("@User_login", login);
             cmd.Parameters.AddWithValue("@Action", "Покупка билетов в количестве: " + numberOfTickets + " шт. на сеанс '" + date + " " + filmName + " " + time+"'");
             cmd.Parameters.AddWithValue("@Change", "-"+numberOfTickets*ticketCosts+" руб.");
             cmd.Parameters.AddWithValue("@Date", Convert.ToString(DateTime.Today.ToString("dd.MM.yyyy")));
             cmd.Parameters.AddWithValue("@Key", 0);
             cmd.ExecuteNonQuery();
             connection.Close();
         }

         public string[,] getBalanceHistory(string login)
         {
             connection.Open();
             SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*)FROM Balance_History WHERE User_Login='" + login + "';", connection);
             SQLiteDataReader reader = cmd.ExecuteReader();
             int count=1;
             while (reader.Read())
             {
                 count = Convert.ToInt16(reader[0]);
             }
             reader.Close();
             string[,] toList = new string[count,4];
             cmd = new SQLiteCommand("SELECT Date, Action, Change, Key FROM Balance_History WHERE User_Login='" + login + "';", connection);
             reader = cmd.ExecuteReader();
             int i = 0;
                 while (reader.Read())
             {
                     toList[i, 0] = Convert.ToString(reader["Date"]);
                     toList[i, 1] = Convert.ToString(reader["Action"]);
                     toList[i, 2] = Convert.ToString(reader["Change"]);
                     toList[i, 3] = Convert.ToString(reader["Key"]);
                 i++;
             }
             reader.Close();
             connection.Close();
             return toList;
         }*/

        public static async void AddCinema(string name)
        {
            Cinema cinema = new Cinema {Name = name};
            dbc.Cinemas.Add(cinema);
            await dbc.SaveChangesAsync();
        }
        public static async void AddFilm(Film film)
        {
            dbc.Films.Add(film);
            await dbc.SaveChangesAsync();
        }
        public static List<string> GetAllCinemas()
        {
            return dbc.Cinemas.Select(x => x.Name).ToList();
        }
        public static List<Film> GetAllFilms()
        {
            return dbc.Films.ToList();
        }
    }
}