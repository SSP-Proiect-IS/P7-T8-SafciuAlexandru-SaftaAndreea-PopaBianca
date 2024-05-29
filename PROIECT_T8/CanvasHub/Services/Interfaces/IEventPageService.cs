using CanvasHub.Models;

namespace CanvasHub.Services.Interfaces
{
    public interface IEventPageService
    {
        Task<List<Event>> GetAllEventsAsync();
        Task<bool> JoinEventAsync(int eventId, string userId);
        Task<bool> LeaveReviewAsync(int eventId, string userId, string reviewName, string reviewDescription, int rating);
        Task CleanupOldEventsAsync();
    }
}
