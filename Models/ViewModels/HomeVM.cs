using System.Collections.Generic;
using LAF.Models;
namespace LAF.Models.ViewModels
{
    public class HomeVM
    {
        public int TotalLostItems { get; set; }
        public int RecentRecoveries { get; set; }
        public int TotalUsers { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}
