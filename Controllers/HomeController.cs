using LAF.Data;
using LAF.Models;
using LAF.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LAF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString, Category? category)
        {
            // Start with a base query
            var query = _context.Items.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(i =>
                    i.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    (i.Description != null && i.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
            }

            // Apply category filter
            if (category.HasValue)
            {
                query = query.Where(i => i.Category == category);
            }

            // Fetch stats and items
            var stats = new HomeVM
            {
                TotalLostItems = _context.Items.Count(i => i.IsLost),
                RecentRecoveries = _context.Items.Count(i => !i.IsLost && i.Date >= DateTime.Now.AddDays(-1)),
                TotalUsers = _context.Users.Count(),
                Items = query.ToList() // Only fetch filtered items
            };

            // Log the items for debugging
            foreach (var item in stats.Items)
            {
                Console.WriteLine($"Item: {item.Name}, User: {item.User?.FullName}");
            }

            return View(stats);
        }
    }
}