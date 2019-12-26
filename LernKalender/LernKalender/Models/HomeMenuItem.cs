using System;
using System.Collections.Generic;
using System.Text;

namespace LernKalender.Models
{
    public enum MenuItemType
    {
        Tagesansicht,
        WeitereEintraege
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
