using LearningCalendar.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearningCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayViewPage : ContentPage
    {
        private DateTime day;

        public DayViewPage()
        {
            InitializeComponent();

            Title = "Tagesansicht";
            day = DateTime.Now;
        }

        public DayViewPage(DateTime date)
        {
            InitializeComponent();

            Title = date.ToString("dd.MM.yyyy");
            day = date;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            LearningEntryListView.ItemsSource = await App.Database.GetLearningEntryByDateAsync(day);
        }

        private async void LearningEntryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            LearningEntry selectedEntry = e.SelectedItem as LearningEntry;
            if(selectedEntry != null)
            {
                await Navigation.PushAsync(new NewLearningEntryPage(selectedEntry));
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            LearningEntry lerningEntry = new LearningEntry();
            lerningEntry.Date = day;

            await Navigation.PushAsync(new NewLearningEntryPage(lerningEntry));
        }
    }
}