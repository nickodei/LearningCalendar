using SQLite;
using System;

namespace LernKalender.Models
{
    public class Lerneinheit
    {
        public Lerneinheit()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Fach { get; set; }

        public string Beschreibung { get; set; }       

        public DateTime Date { get; set; }

        public int _dauer { get; set; }
        [Ignore]
        public TimeSpan Dauer 
        {
            get
            {
                return TimeSpan.FromMinutes(_dauer);
            }
            set
            {
                _dauer = (int) value.TotalMinutes;
            }
        }

        public int _startZeitpunkt { get; set; }
        [Ignore]
        public TimeSpan StartZeitpunkt
        {
            get
            {
                return TimeSpan.FromMinutes(_startZeitpunkt);
            }
            set
            {
                _startZeitpunkt = (int)value.TotalMinutes;
            }
        }
    }
}
