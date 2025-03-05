// Services/IMatchingService.cs
using LostandFound.Models;

namespace LostandFound.Services
{
    public interface IMatchingService
    {
        Task<bool> CheckForMatchesAsync(LostItem lostItem);
        Task<bool> CheckForMatchesAsync(FoundItem foundItem);
    }
}
