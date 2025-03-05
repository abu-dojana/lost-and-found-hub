// Models/Notification.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace LostandFound.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } // User who will receive the notification

        public int? SourceItemId { get; set; } // ID of the user's item
        public string ItemType { get; set; } = ""; // "Lost" or "Found"

        public int? MatchItemId { get; set; } // ID of the potential matching item
        public string MatchItemType { get; set; } = ""; // "Lost" or "Found"

        [Required]
        public string Message { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
