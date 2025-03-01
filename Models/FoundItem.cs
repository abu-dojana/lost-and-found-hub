using System.ComponentModel.DataAnnotations;

namespace LostandFound.Models
{
    public class FoundItem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; } // Foreign key to Users table

        [Required(ErrorMessage = "Item name is required.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Contact details are required.")]
        public string ContactDetails { get; set; }
    }
}