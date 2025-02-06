namespace LAF.Models;

using System.ComponentModel.DataAnnotations;
// Models/Item.cs

using Microsoft.AspNetCore.Identity;

public class Item
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Item name is required.")]
    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsLost { get; set; } // True = Lost, False = Found

    [Required(ErrorMessage = "Date is required.")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Location is required.")]
    public string Location { get; set; }

    public string ImagePath { get; set; } // Path to the uploaded image

    public Category Category { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}


// Extend IdentityUser in Models/ApplicationUser.cs
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public string Location { get; set; }
    public string PhotoPath { get; set; }
}