using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanvasHub.Models;
using CanvasHub.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CanvasHub.Services
{
    public class InboxService : IInboxService
    {
        private readonly CanvasHubContext _context;
        private readonly UserManager<User> _userManager;

        public InboxService(CanvasHubContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<Message>> GetUnreadMessagesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            return await _context.Messages
                //.Where(m => m.RecipientId == userId && !m.IsRead)
                .ToListAsync();
        }

        public async Task<List<Notification>> GetUnreadNotificationsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            return await _context.Notifications
                //.Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();
        }

        public async Task MarkMessageAsReadAsync(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message != null)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkNotificationAsReadAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> RespondToEventInvitationAsync(int eventId, string userId, bool accept)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var eventInvitation = await _context.EventInvitations.FindAsync(eventId, userId);
            if (eventInvitation == null)
            {
                throw new InvalidOperationException("Event invitation not found.");
            }

            // Respond to the event invitation
            if (accept)
            {
                // Add logic to accept the invitation (e.g., add user to event attendees)
            }
            else
            {
                // Add logic to decline the invitation (e.g., remove user from event attendees)
            }

            // Mark the invitation as read
            eventInvitation.IsRead = true;
            await _context.SaveChangesAsync();

            return accept;
        }
    }
}
