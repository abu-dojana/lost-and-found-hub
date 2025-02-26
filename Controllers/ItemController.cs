using LostandFound.Data;
using LostandFound.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LostandFound.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult PostLostItem()
        {
            return View();
        }

        // POST: /Item/PostLostItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostLostItem(LostItem model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _context.LostItems.AddAsync(model);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Lost item posted successfully!";
                return RedirectToAction("SeeLostListings");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the lost item.");
                return View(model);
            }
        }
        // GET: /Item/PostFoundItem
        [HttpGet]
        public IActionResult PostFoundItem()
        {
            return View();
        }

        // POST: /Item/PostFoundItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostFoundItem(FoundItem model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _context.FoundItems.AddAsync(model);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Found item posted successfully!";
                return RedirectToAction("SeeFoundListings");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the found item.");
                return View(model);
            }
        }

        // GET: /Item/SeeLostListings
        public async Task<IActionResult> SeeLostListings()
        {
            try
            {
                // Fetch all lost items from the database (consider adding pagination)
                var lostItems = await _context.LostItems.ToListAsync();
                return View(lostItems);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) for debugging purposes
                TempData["ErrorMessage"] = "An error occurred while fetching lost items.";
                return View(new List<LostItem>());
            }
        }

        // GET: /Item/SeeFoundListings
        public async Task<IActionResult> SeeFoundListings()
        {
            try
            {
                // Fetch all found items from the database (consider adding pagination)
                var foundItems = await _context.FoundItems.ToListAsync();
                return View(foundItems);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) for debugging purposes
                TempData["ErrorMessage"] = "An error occurred while fetching found items.";
                return View(new List<FoundItem>());
            }
        }

        // Helper method to get the current user's ID (if authentication is implemented)
        // private int GetCurrentUserId()
        // {
        //     var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        //     return userId;
        // }
    }
}
