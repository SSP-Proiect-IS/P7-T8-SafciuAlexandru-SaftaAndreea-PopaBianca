using CanvasHub.Models;

namespace CanvasHub.Services.Interfaces
{
    public interface IEventEditingService
    {
        Task<Event> EditEventAsync(int eventId, string userId, string newName, string newDescription, List<int> newResourceIds, string reason);
    }
}
