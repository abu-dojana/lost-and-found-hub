using LostandFound.Data;
using LostandFound.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using LostandFound.Services;

namespace LostandFound.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMatchingService _matchingService;

        public ItemController(
               ApplicationDbContext context,
               IWebHostEnvironment webHostEnvironment,
               IMatchingService matchingService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _matchingService = matchingService;
        }

        [HttpGet]
        public IActionResult PostLostItem()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostLostItem([FromForm] LostItem model, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "images");
                    Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    model.ImageUrl = "/uploads/images/" + uniqueFileName;
                }

                // Get the current user ID from session
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId.HasValue)
                {
                    model.UserId = userId.Value;
                }
                else
                {
                    // If not logged in, redirect to login page
                    return RedirectToAction("Login", "Account");
                }

                await _context.LostItems.AddAsync(model);
                await _context.SaveChangesAsync();

                // Check for potential matches and create notifications
                await _matchingService.CheckForMatchesAsync(model);

                TempData["SuccessMessage"] = "Lost item posted successfully!";
                return RedirectToAction("SeeLostListings");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the lost item.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult PostFoundItem()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostFoundItem([FromForm] FoundItem model, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "images");
                    Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    model.ImageUrl = "/uploads/images/" + uniqueFileName;
                }

                // Get the current user ID from session
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId.HasValue)
                {
                    model.UserId = userId.Value;
                }
                else
                {
                    // If not logged in, redirect to login page
                    return RedirectToAction("Login", "Account");
                }

                await _context.FoundItems.AddAsync(model);
                await _context.SaveChangesAsync();

                // Check for potential matches and create notifications
                await _matchingService.CheckForMatchesAsync(model);

                TempData["SuccessMessage"] = "Found item posted successfully!";
                return RedirectToAction("SeeFoundListings");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the found item.");
                return View(model);
            }
        }

        public async Task<IActionResult> SeeLostListings()
        {
            try
            {
                var lostItems = await _context.LostItems
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
                return View(lostItems);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching lost items.";
                return View(new List<LostItem>());
            }
        }

        public async Task<IActionResult> SeeFoundListings()
        {
            try
            {
                var foundItems = await _context.FoundItems
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
                return View(foundItems);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching found items.";
                return View(new List<FoundItem>());
            }
        }

        // Controllers/ItemController.cs (add these methods)
        public async Task<IActionResult> ViewLostItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostItem = await _context.LostItems
                .FirstOrDefaultAsync(m => m.Id == id);

            if (lostItem == null)
            {
                return NotFound();
            }

            return View(lostItem);
        }

        public async Task<IActionResult> ViewFoundItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foundItem = await _context.FoundItems
                .FirstOrDefaultAsync(m => m.Id == id);

            if (foundItem == null)
            {
                return NotFound();
            }

            return View(foundItem);
        }


    }
}
