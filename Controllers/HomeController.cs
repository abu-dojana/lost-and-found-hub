// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
using LAF.Data;
using LAF.Models;
using System.Linq;
using LAF.Models.ViewModels;

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
            // Fetch stats for counters
            var stats = new HomeVM
            {
                TotalLostItems = _context.Items.Count(i => i.IsLost),
                RecentRecoveries = _context.Items.Count(i => !i.IsLost && i.Date >= DateTime.Now.AddDays(-1)),
                TotalUsers = _context.Users.Count(),
                Items = _context.Items.ToList() // Default list of items
            };

            // Apply search filter
            if (!string.IsNullOrEmpty(searchString))
            {
                stats.Items = stats.Items.Where(i =>
                    i.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    i.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Apply category filter
            if (category.HasValue)
            {
                stats.Items = stats.Items.Where(i => i.Category == category).ToList();
            }

            return View(stats);
        }
    }
}