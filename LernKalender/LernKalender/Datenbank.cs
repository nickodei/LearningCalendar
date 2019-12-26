using LernKalender.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LernKalender
{
    public class Datenbank
    {
        readonly SQLiteAsyncConnection _database;

        public Datenbank(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            _database.DropTableAsync<Lerneinheit>().Wait();
            _database.CreateTableAsync<Lerneinheit>().Wait();

            // Seeds the database
            Task.Run(() => SeedDatabase());
        }

        public async Task SeedDatabase()
        {
            List<Lerneinheit> seedData = new List<Lerneinheit>()
            {
                new Lerneinheit()
                {
                    Fach = "Physik",
                    Dauer = new TimeSpan(3, 0, 0),
                    StartZeitpunkt = new TimeSpan(16, 0, 0),
                    Beschreibung = "Kinematik nochmal anschauen",
                    Date = DateTime.Today
                },
                new Lerneinheit()
                {
                    Fach = "Mathe",
                    Dauer = new TimeSpan(1, 0, 0),
                    StartZeitpunkt = new TimeSpan(19, 0, 0),
                    Beschreibung = "Linieare Abhängigkeit + Kurvendiskusion",
                    Date = DateTime.Today
                },
                new Lerneinheit()
                {
                    Fach = "Englisch",
                    Dauer = new TimeSpan(2, 0, 0),
                    StartZeitpunkt = new TimeSpan(20, 0, 0),
                    Beschreibung = "Vokabeln lernen Unit3",
                    Date = DateTime.Today
                },
                new Lerneinheit()
                {
                    Fach = "Englisch",
                    Dauer = new TimeSpan(2, 0, 0),
                    StartZeitpunkt = new TimeSpan(20, 0, 0),
                    Beschreibung = "Vokabeln lernen Unit4",
                    Date = new DateTime(2019, 12, 26)
                },
                new Lerneinheit()
                {
                    Fach = "Englisch",
                    Dauer = new TimeSpan(2, 0, 0),
                    StartZeitpunkt = new TimeSpan(20, 0, 0),
                    Beschreibung = "Vokabeln lernen Unit7",
                    Date = new DateTime(2019, 12, 28)
                }
            };

            Task[] tasks = new Task[seedData.Count];
            for(int i = 0; i < seedData.Count; i++)
            {
                var task = SaveLerneinheitAsync(seedData[i]);
                tasks[i] = task;
            }

            await Task.WhenAll(tasks);
        }

        public Task<List<Lerneinheit>> GetLerneinheitenAsync()
        {
            return _database.Table<Lerneinheit>().OrderBy(x => x.Date).ToListAsync();
        }

        public Task<List<Lerneinheit>> GetLerneinheitenByDateAsync(DateTime tag)
        {
            return _database.Table<Lerneinheit>().Where(x => x.Date == tag.Date).ToListAsync();
        }

        public Task SaveLerneinheitAsync(Lerneinheit lerneinheit)
        {
            if (lerneinheit.ID != 0)
            {
                return _database.UpdateAsync(lerneinheit);
            }
            else
            {
                return _database.InsertAsync(lerneinheit);
            }
        }

        public Task DeleteLerneinheitAsync(Lerneinheit lerneinheit)
        {
            return _database.DeleteAsync(lerneinheit);
        }
    }
}
