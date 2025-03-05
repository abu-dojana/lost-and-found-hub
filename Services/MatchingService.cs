// Services/MatchingService.cs
using LostandFound.Data;
using LostandFound.Models;
using Microsoft.EntityFrameworkCore;

namespace LostandFound.Services
{
    public class MatchingService : IMatchingService
    {
        private readonly ApplicationDbContext _context;

        public MatchingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckForMatchesAsync(LostItem lostItem)
        {
            // Find potential matches from FoundItems based on similar properties
            var potentialMatches = await _context.FoundItems
                .Where(f =>
                        // Match by category (exact match)
                        f.Category == lostItem.Category &&
                        // Then require at least one of these match conditions
                        (
                            // Match by location (partial match)
                            f.Location.Contains(lostItem.Location) ||
                            lostItem.Location.Contains(f.Location) ||
                            // Match by name containing similar text (partial match)
                            f.ItemName.Contains(lostItem.ItemName) ||
                            lostItem.ItemName.Contains(f.ItemName) ||
                            // Or check for similarity in description (partial match)
                            f.Description.Contains(lostItem.Description) ||
                            lostItem.Description.Contains(f.Description)
                        )


                )
                .ToListAsync();

            if (potentialMatches.Any())
            {
                // Create notifications for each potential match
                foreach (var match in potentialMatches)
                {
                    // Notify the lost item owner
                    await CreateNotificationAsync(
                        lostItem.UserId,
                        lostItem.Id,
                        "Lost",
                        match.Id,
                        "Found",
                        $"Your posted lost item '{lostItem.ItemName}' has a potential match with a found item!"
                    );

                    // Notify the found item owner
                    await CreateNotificationAsync(
                        match.UserId,
                        match.Id,
                        "Found",
                        lostItem.Id,
                        "Lost",
                        $"Your posted found item '{match.ItemName}' might match someone's lost item!"
                    );
                }
                
                return true;
            }
            
            return false;
        }

        public async Task<bool> CheckForMatchesAsync(FoundItem foundItem)
        {
            // Find potential matches from LostItems based on similar properties
            var potentialMatches = await _context.LostItems
                .Where(l =>
                        // Match by category (exact match)
                        l.Category == foundItem.Category &&
                        // Then require at least one of these match conditions
                        (
                            // Match by location (partial match)
                            l.Location.Contains(foundItem.Location) ||
                            foundItem.Location.Contains(l.Location) ||
                            // Match by name containing similar text (partial match)
                            l.ItemName.Contains(foundItem.ItemName) ||
                            foundItem.ItemName.Contains(l.ItemName) ||
                            // Or check for similarity in description (partial match)
                            l.Description.Contains(foundItem.Description) ||
                            foundItem.Description.Contains(l.Description)
                        )
                )
                .ToListAsync();

            if (potentialMatches.Any())
            {
                // Create notifications for each potential match
                foreach (var match in potentialMatches)
                {
                    // Notify the found item owner
                    await CreateNotificationAsync(
                        foundItem.UserId,
                        foundItem.Id,
                        "Found",
                        match.Id,
                        "Lost",
                        $"Your posted found item '{foundItem.ItemName}' might match someone's lost item!"
                    );

                    // Notify the lost item owner
                    await CreateNotificationAsync(
                        match.UserId,
                        match.Id,
                        "Lost",
                        foundItem.Id,
                        "Found",
                        $"Your posted lost item '{match.ItemName}' has a potential match with a found item!"
                    );
                }
                
                return true;
            }
            
            return false;
        }

        private async Task CreateNotificationAsync(int userId, int sourceItemId, string itemType, int matchItemId, string matchItemType, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                SourceItemId = sourceItemId,
                ItemType = itemType,
                MatchItemId = matchItemId,
                MatchItemType = matchItemType,
                Message = message,
                CreatedAt = DateTime.Now
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }
    }
}
