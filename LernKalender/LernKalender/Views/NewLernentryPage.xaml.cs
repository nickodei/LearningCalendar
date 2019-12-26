using LernKalender.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LernKalender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewLernentryPage : ContentPage
    {
        private Lerneinheit _Lerneinheit;

        public NewLernentryPage()
        {
            InitializeComponent();

            Title = "Neuer Eintrag";

            _Lerneinheit = new Lerneinheit();
            _Lerneinheit.Date = DateTime.Today;

            SetBindingContext();
        }

        public NewLernentryPage(Lerneinheit lerneinheit)
        {
            InitializeComponent();

            Title = "Eintrag bearbeiten";
            _Lerneinheit = lerneinheit;
            SetBindingContext();
        }

        private void SetBindingContext()
        {
            DayDatePicker.BindingContext = _Lerneinheit;
            DayDatePicker.SetBinding(DatePicker.DateProperty, "Date");

            FachEntry.BindingContext = _Lerneinheit;
            FachEntry.SetBinding(Entry.TextProperty, "Fach");

            BeschreibungEntry.BindingContext = _Lerneinheit;
            BeschreibungEntry.SetBinding(Entry.TextProperty, "Beschreibung");

            DauerTimePicker.BindingContext = _Lerneinheit;
            DauerTimePicker.SetBinding(TimePicker.TimeProperty, "Dauer");

            StartpunktTimePicker.BindingContext = _Lerneinheit;
            StartpunktTimePicker.SetBinding(TimePicker.TimeProperty, "StartZeitpunkt");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await App.Database.SaveLerneinheitAsync(_Lerneinheit);
            await Navigation.PopAsync();
        }
    }
}