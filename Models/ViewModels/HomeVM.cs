// Models/ViewModels/HomeVM.cs
using System.Collections.Generic;

namespace LAF.Models.ViewModels
{
    public class HomeVM
    {
        public int TotalLostItems { get; set; }
        public int RecentRecoveries { get; set; }
        public int TotalUsers { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
    }
}