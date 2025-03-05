// Controllers/NotificationController.cs
using LostandFound.Data;
using LostandFound.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LostandFound.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return View(notifications);
        }

        public async Task<IActionResult> MarkAsRead(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MarkAllAsRead()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Controllers/NotificationController.cs - Update the ViewMatch method
        public async Task<IActionResult> ViewMatch(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (notification == null)
            {
                return NotFound();
            }

            // Mark notification as read
            notification.IsRead = true;
            await _context.SaveChangesAsync();

            // Create a view model to hold both items
            var viewModel = new MatchViewModel
            {
                Notification = notification
            };

            // Fetch both items based on the notification's stored IDs
            if (notification.SourceItemId.HasValue)
            {
                if (notification.ItemType == "Lost")
                {
                    viewModel.LostItem = await _context.LostItems
                        .FirstOrDefaultAsync(l => l.Id == notification.SourceItemId);
                }
                else // Found
                {
                    viewModel.FoundItem = await _context.FoundItems
                        .FirstOrDefaultAsync(f => f.Id == notification.SourceItemId);
                }
            }

            if (notification.MatchItemId.HasValue)
            {
                if (notification.MatchItemType == "Lost")
                {
                    viewModel.LostItem = await _context.LostItems
                        .FirstOrDefaultAsync(l => l.Id == notification.MatchItemId);
                }
                else // Found
                {
                    viewModel.FoundItem = await _context.FoundItems
                        .FirstOrDefaultAsync(f => f.Id == notification.MatchItemId);
                }
            }

            // Return the view with both items
            return View(viewModel);
        }

        //Single view match 
        //public async Task<IActionResult> ViewMatch(int id)
        //{
        //    var userId = HttpContext.Session.GetInt32("UserId");
        //    if (!userId.HasValue)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    var notification = await _context.Notifications
        //        .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

        //    if (notification == null)
        //    {
        //        return NotFound();
        //    }

        //    // Mark as read
        //    notification.IsRead = true;
        //    await _context.SaveChangesAsync();

        //    // Redirect to the appropriate view based on the match type
        //    if (notification.MatchItemType == "Lost")
        //    {
        //        return RedirectToAction("ViewLostItem", "Item", new { id = notification.MatchItemId });
        //    }
        //    else
        //    {
        //        return RedirectToAction("ViewFoundItem", "Item", new { id = notification.MatchItemId });
        //    }
        //}

        [HttpGet]
        public async Task<JsonResult> GetUnreadCount()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return Json(new { count = 0 });
            }

            var count = await _context.Notifications
                .CountAsync(n => n.UserId == userId && !n.IsRead);

            return Json(new { count });
        }
    }
}
