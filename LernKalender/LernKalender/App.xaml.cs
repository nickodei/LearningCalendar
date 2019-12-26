using Xamarin.Forms;
using LernKalender.Views;
using System.IO;
using System;

namespace LernKalender
{
    public partial class App : Application
    {
        static Datenbank database;
        public static Datenbank Database
        {
            get
            {
                if (database == null)
                {
                    database = new Datenbank(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LernKalenderSQLite.db3"));
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
