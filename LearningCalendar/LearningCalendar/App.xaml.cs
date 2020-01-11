using Xamarin.Forms;
using LearningCalendar.Views;
using System.IO;
using System;

namespace LearningCalendar
{
    public partial class App : Application
    {
        static LernKalenderDatabase database;
        public static LernKalenderDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new LernKalenderDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LernKalenderSQLite.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
