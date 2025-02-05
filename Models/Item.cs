namespace LAF.Models
{
    public class Item
    {
        public int Id { get; set; }

        // Required fields
        public required string Name { get; set; } // Use 'required' if using C# 11+
        public required string Description { get; set; }
        public required bool IsLost { get; set; } // True=Lost, False=Found
        public required DateTime Date { get; set; }
        public required string Location { get; set; }
        public Category Category { get; set; } // Enum, no need for 'required'

        // Nullable fields
        public string? ImagePath { get; set; } // Optional image path
        public required string UserId { get; set; } // Foreign key to ApplicationUser
        public ApplicationUser? User { get; set; } // Navigation property
    }
}