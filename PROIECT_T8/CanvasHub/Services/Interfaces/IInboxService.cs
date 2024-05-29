using CanvasHub.Models;

namespace CanvasHub.Services.Interfaces
{
    public interface IInboxService
    {
        Task<List<Message>> GetUnreadMessagesAsync(string userId);
        Task<List<Notification>> GetUnreadNotificationsAsync(string userId);
        Task MarkMessageAsReadAsync(int messageId);
        Task MarkNotificationAsReadAsync(int notificationId);
        Task<bool> RespondToEventInvitationAsync(int eventId, string userId, bool accept);
    }
}
