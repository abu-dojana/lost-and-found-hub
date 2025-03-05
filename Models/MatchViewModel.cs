// Models/MatchViewModel.cs
namespace LostandFound.Models
{
    public class MatchViewModel
    {
        public LostItem LostItem { get; set; }
        public FoundItem FoundItem { get; set; }
        public Notification Notification { get; set; }
    }
}
