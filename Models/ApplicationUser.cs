using Microsoft.AspNetCore.Identity;

namespace LAF.Models
{
    public class ApplicationUser : IdentityUser
    {
        public required string FullName { get; set; }
        public required string Location { get; set; }
        public string? PhotoPath { get; set; } // Optional photo path
    }
}