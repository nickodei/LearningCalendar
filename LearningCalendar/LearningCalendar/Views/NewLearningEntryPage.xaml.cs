using LearningCalendar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearningCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewLearningEntryPage : ContentPage
    {
        private LearningEntry _LearningEntry;

        public NewLearningEntryPage()
        {
            InitializeComponent();

            // Setze den Titel der Seite
            Title = "Neuer Eintrag";

            // Eintrag noch nicht in der Datenbank vorhanden => nicht löschbar
            LoeschenButton.IsVisible = false;

            _LearningEntry = new LearningEntry() { Date = DateTime.Today };
            SetBindingContext();
        }

        public NewLearningEntryPage(LearningEntry learningEntry)
        {
            InitializeComponent();

            // Setze den Titel der Seite
            Title = "Eintrag bearbeiten";

            // Eintrag noch nicht in der Datenbank vorhanden
            if (learningEntry.ID == 0)
            {               
                // nicht löschbar
                LoeschenButton.IsVisible = false;
            }

            _LearningEntry = learningEntry;
            SetBindingContext();
        }

        private void SetBindingContext()
        {
            DayDatePicker.BindingContext = _LearningEntry;
            DayDatePicker.SetBinding(DatePicker.DateProperty, "Date");

            FachEntry.BindingContext = _LearningEntry;
            FachEntry.SetBinding(Entry.TextProperty, "Lesson");

            BeschreibungEntry.BindingContext = _LearningEntry;
            BeschreibungEntry.SetBinding(Entry.TextProperty, "Description");

            DauerTimePicker.BindingContext = _LearningEntry;
            DauerTimePicker.SetBinding(TimePicker.TimeProperty, "Duration");

            StartpunktTimePicker.BindingContext = _LearningEntry;
            StartpunktTimePicker.SetBinding(TimePicker.TimeProperty, "StartingPoint");
        }

        private async void SpeichernButton_Clicked(object sender, EventArgs e)
        {
            await App.Database.SaveLearningEntryAsync(_LearningEntry);
            await Navigation.PopAsync();
        }

        private async void LoeschenButton_Clicked(object sender, EventArgs e)
        {
            await App.Database.DeleteLearningEntryAsync(_LearningEntry);
            await Navigation.PopAsync();
        }
    }
}