// Controllers/ItemsController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LAF.Data;
using LAF.Models;
using LAF.Services;
using System.Threading.Tasks;

namespace LAF.Controllers
{
    [Authorize] // Ensures only logged-in users can access these actions
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public ItemsController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: Items/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Contact/{id}
        [HttpPost]
        public async Task<IActionResult> Contact(int itemId)
        {
            var item = await _context.Items.FindAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            // Get current user's email
            var userEmail = User.Identity.Name;

            // Send email to the poster
            await _emailService.SendContactEmail(userEmail, item.User.Email, item.Name);

            TempData["Message"] = "Your contact request has been sent.";
            return RedirectToAction("Details", new { id = itemId });
        }

        // GET: Items/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        public async Task<IActionResult> Create(Item item, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                // Handle image upload
                if (image != null)
                {
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    item.ImagePath = fileName; //save the file Path
                }

                // Set the user ID for the item
                item.UserId = User.Identity.Name;

                // Save the item to the database
                _context.Items.Add(item);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(item);
        }
    }
}