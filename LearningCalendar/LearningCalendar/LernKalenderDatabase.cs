using LearningCalendar.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningCalendar
{
    public class LernKalenderDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public LernKalenderDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            _database.DropTableAsync<LearningEntry>().Wait();
            _database.CreateTableAsync<LearningEntry>().Wait();

            // Seeds the database
            Task.Run(() => SeedDatabase());
        }

        public async Task SeedDatabase()
        {
            List<LearningEntry> seedData = new List<LearningEntry>()
            {
                new LearningEntry()
                {
                    Lesson = "Physik",
                    Duration = new TimeSpan(3, 0, 0),
                    StartingPoint = new TimeSpan(16, 0, 0),
                    Description = "Kinematik nochmal anschauen",
                    Date = DateTime.Today
                },
                new LearningEntry()
                {
                    Lesson = "Mathe",
                    Duration = new TimeSpan(1, 0, 0),
                    StartingPoint = new TimeSpan(19, 0, 0),
                    Description = "Linieare Abhängigkeit + Kurvendiskusion",
                    Date = DateTime.Today
                },
                new LearningEntry()
                {
                    Lesson = "Englisch",
                    Duration = new TimeSpan(2, 0, 0),
                    StartingPoint = new TimeSpan(20, 0, 0),
                    Description = "Vokabeln lernen Unit3",
                    Date = DateTime.Today
                },
                new LearningEntry()
                {
                    Lesson = "Englisch",
                    Duration = new TimeSpan(2, 0, 0),
                    StartingPoint = new TimeSpan(20, 0, 0),
                    Description = "Vokabeln lernen Unit4",
                    Date = new DateTime(2019, 12, 26)
                },
                new LearningEntry()
                {
                    Lesson = "Englisch",
                    Duration = new TimeSpan(2, 0, 0),
                    StartingPoint = new TimeSpan(20, 0, 0),
                    Description = "Vokabeln lernen Unit7",
                    Date = new DateTime(2019, 12, 28)
                }
            };

            Task[] tasks = new Task[seedData.Count];
            for(int i = 0; i < seedData.Count; i++)
            {
                var task = SaveLearningEntryAsync(seedData[i]);
                tasks[i] = task;
            }

            await Task.WhenAll(tasks);
        }

        public Task<List<LearningEntry>> GetLearningEntryAsync()
        {
            return _database.Table<LearningEntry>().OrderBy(x => x.Date).ToListAsync();
        }

        public Task<List<LearningEntry>> GetLearningEntryByDateAsync(DateTime day)
        {
            return _database.Table<LearningEntry>().Where(x => x.Date == day.Date).ToListAsync();
        }

        public Task SaveLearningEntryAsync(LearningEntry LearningEntry)
        {
            if (LearningEntry.ID != 0)
            {
                return _database.UpdateAsync(LearningEntry);
            }
            else
            {
                return _database.InsertAsync(LearningEntry);
            }
        }

        public Task DeleteLearningEntryAsync(LearningEntry learningEntry)
        {
            return _database.DeleteAsync(learningEntry);
        }
    }
}
