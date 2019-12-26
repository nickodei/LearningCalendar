using LernKalender.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LernKalender.Views
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

            LerneintraegeListView.ItemsSource = await App.Database.GetLerneinheitenByDateAsync(day);
        }

        private async void LerneintraegeListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Lerneinheit selectedEntry = e.SelectedItem as Lerneinheit;
            if(selectedEntry != null)
            {
                await Navigation.PushAsync(new NewLernentryPage(selectedEntry));
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Lerneinheit lerneinheit = new Lerneinheit();
            lerneinheit.Date = day;

            await Navigation.PushAsync(new NewLernentryPage(lerneinheit));
        }
    }
}