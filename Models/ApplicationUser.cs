using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public string Location { get; set; }
    public string PhotoPath { get; set; }
}