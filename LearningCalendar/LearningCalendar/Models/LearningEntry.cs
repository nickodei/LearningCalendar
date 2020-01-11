using SQLite;
using System;

namespace LearningCalendar.Models
{
    public class LearningEntry
    {
        public LearningEntry()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Lesson { get; set; }

        public string Description { get; set; }       

        public DateTime Date { get; set; }

        public int _duration { get; set; }
        [Ignore]
        public TimeSpan Duration 
        {
            get
            {
                return TimeSpan.FromMinutes(_duration);
            }
            set
            {
                _duration = (int) value.TotalMinutes;
            }
        }

        public int _startingPoint { get; set; }
        [Ignore]
        public TimeSpan StartingPoint
        {
            get
            {
                return TimeSpan.FromMinutes(_startingPoint);
            }
            set
            {
                _startingPoint = (int)value.TotalMinutes;
            }
        }
    }
}
