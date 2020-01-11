using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCalendar.Models
{
    public enum MenuItemType
    {
        DayViewPage,
        DayEntriesPage
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
