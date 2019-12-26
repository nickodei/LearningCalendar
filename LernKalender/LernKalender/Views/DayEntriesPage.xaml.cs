using LernKalender.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LernKalender.Views
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

            List<Lerneinheit> lerneinheiten = await App.Database.GetLerneinheitenAsync();

            //StackOverlow Frage
            //https://stackoverflow.com/questions/5716762/datetime-now-dayofweek-tostring-with-cultureinfo
            var culture = new System.Globalization.CultureInfo("de-DE");

            List<Kalendereintrag> kalendereintraege = lerneinheiten.GroupBy(x => x.Date)
                                                            .Select(group => new Kalendereintrag
                                                            {
                                                                Date = group.Key,
                                                                NameOfWeek = culture.DateTimeFormat.GetDayName(group.Key.DayOfWeek)
                                                            }).ToList();

            DayEntriesListView.ItemsSource = kalendereintraege;
        }

        private async void DayEntriesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Kalendereintrag kalendereintrag = e.SelectedItem as Kalendereintrag;
            if(kalendereintrag != null)
            {
                await Navigation.PushAsync(new DayViewPage(kalendereintrag.Date));
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewLernentryPage());
        }
    }
}