using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanvasHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CanvasHub.Services
{
    public class EventService : IEventService
    {
        private readonly CanvasHubContext _context;

        public EventService(CanvasHubContext context)
        {
            _context = context;
        }

        public async Task<Event> AddEventAsync(string userId, string eventName, string description, List<int> resourceIds, List<string> invitedUserIds)
        {
            // Basic validation
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(eventName))
            {
                throw new ArgumentNullException(nameof(eventName), "Event name cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(nameof(description), "Description cannot be null or empty.");
            }

            var creator = await _context.Users.FindAsync(userId);
            if (creator == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var newEvent = new Event
            {
                EventName = eventName,
                Description = description,
                User = creator,
                Resources = new List<Resource>(),
                EventInvitations = new List<EventInvitation>()
            };

            // Add resources to the event
            if (resourceIds != null)
            {
                foreach (var resourceId in resourceIds)
                {
                    var resource = await _context.Resources.FindAsync(resourceId);
                    if (resource != null)
                    {
                        newEvent.Resources.Add(resource);
                    }
                }
            }

            // Invite users to the event
            if (invitedUserIds != null)
            {
                foreach (var invitedUserId in invitedUserIds)
                {
                    var invitedUser = await _context.Users.FindAsync(invitedUserId);
                    if (invitedUser != null)
                    {
                        newEvent.EventInvitations.Add(new EventInvitation
                        {
                            User = invitedUser,
                            StatusInvitation = "Pending" // Setting the StatusInvitation property
                        });
                    }
                }
            }

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return newEvent;
        }
    }
}
