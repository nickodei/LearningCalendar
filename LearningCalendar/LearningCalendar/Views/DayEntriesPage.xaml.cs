using LearningCalendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearningCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayEntriesPage : ContentPage
    {
        public DayEntriesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<LearningEntry> learningEntry = await App.Database.GetLearningEntryAsync();

            //StackOverlow Frage
            //https://stackoverflow.com/questions/5716762/datetime-now-dayofweek-tostring-with-cultureinfo
            var culture = new System.Globalization.CultureInfo("de-DE");

            List<CalendarEntry> calendarEntries = learningEntry.GroupBy(x => x.Date)
                                                            .Select(group => new CalendarEntry
                                                            {
                                                                Date = group.Key,
                                                                NameOfWeek = culture.DateTimeFormat.GetDayName(group.Key.DayOfWeek)
                                                            }).ToList();

            DayEntriesListView.ItemsSource = calendarEntries;
        }

        private async void DayEntriesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CalendarEntry calenderEntry = e.SelectedItem as CalendarEntry;
            if(calenderEntry != null)
            {
                await Navigation.PushAsync(new DayViewPage(calenderEntry.Date));
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewLearningEntryPage());
        }
    }
}