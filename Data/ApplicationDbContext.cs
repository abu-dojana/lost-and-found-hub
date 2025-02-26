// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using LostandFound.Models;

namespace LostandFound.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define DbSets for your models
        public DbSet<User> Users { get; set; }
        public DbSet<LostItem> LostItems { get; set; }
        public DbSet<FoundItem> FoundItems { get; set; }
    }
}
