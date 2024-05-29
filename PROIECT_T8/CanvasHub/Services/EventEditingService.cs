using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanvasHub.Models;
using CanvasHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CanvasHub.Services
{
    public class EventEditingService : IEventEditingService
    {
        private readonly CanvasHubContext _context;

        public EventEditingService(CanvasHubContext context)
        {
            _context = context;
        }

        public async Task<Event> EditEventAsync(int eventId, string userId, string newName, string newDescription, List<int> newResourceIds, string reason)
        {
            var eventToEdit = await _context.Events
                .Include(e => e.Resources)
                .Include(e => e.EventInvitations)
                .ThenInclude(ei => ei.User)
                .FirstOrDefaultAsync(e => e.EventId == eventId && e.User.Id == userId);

            if (eventToEdit == null)
            {
                throw new InvalidOperationException("Event not found or you do not have permission to edit this event.");
            }

            if (string.IsNullOrEmpty(reason))
            {
                throw new ArgumentNullException(nameof(reason), "A reason for the modification must be provided.");
            }

            eventToEdit.EventName = newName;
            eventToEdit.Description = newDescription;

            // Update resources
            eventToEdit.Resources.Clear();
            foreach (var resourceId in newResourceIds)
            {
                var resource = await _context.Resources.FindAsync(resourceId);
                if (resource != null)
                {
                    eventToEdit.Resources.Add(resource);
                }
            }

            // Save the changes
            await _context.SaveChangesAsync();

            // Notify invited users about the modification
            foreach (var invitation in eventToEdit.EventInvitations)
            {
                var notification = new Notification
                {
                    Subject = $"Event '{eventToEdit.EventName}' has been updated",
                    User = invitation.User,
                    IsRead = false
                };
                _context.Notifications.Add(notification);
            }

            await _context.SaveChangesAsync();

            return eventToEdit;
        }
    }

}
