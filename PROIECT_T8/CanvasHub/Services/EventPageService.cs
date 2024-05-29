using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanvasHub.Models;
using CanvasHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace CanvasHub.Services
{
    public class EventPageService : IEventPageService
    {
        private readonly CanvasHubContext _context;

        public EventPageService(CanvasHubContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _context.Events
                .Include(e => e.User)
                .Include(e => e.Resources)
                .Include(e => e.Review)
                .ToListAsync();
        }

        public async Task<bool> JoinEventAsync(int eventId, string userId)
        {
            var eventToJoin = await _context.Events
                .Include(e => e.EventInvitations)
                .FirstOrDefaultAsync(e => e.EventId == eventId);

            if (eventToJoin == null)
            {
                throw new InvalidOperationException("Event not found.");
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            if (eventToJoin.EventInvitations.Any(ei => ei.User.Id == userId))
            {
                return false; // User is already part of the event
            }

            var invitation = new EventInvitation
            {
                EventId = eventId,
                User = user,
                StatusInvitation = "Joined"
            };

            eventToJoin.EventInvitations.Add(invitation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> LeaveReviewAsync(int eventId, string userId, string reviewName, string reviewDescription, int rating)
        {
            var eventToReview = await _context.Events
                .Include(e => e.Review)
                .FirstOrDefaultAsync(e => e.EventId == eventId);

            if (eventToReview == null)
            {
                throw new InvalidOperationException("Event not found.");
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var review = new Review
            {
                EventId = eventId,
                ReviewName = reviewName,
                ReviewDescription = reviewDescription,
                Rating = rating,
                Event = eventToReview
            };

            _context.Reviews.Add(review);
            eventToReview.Review = review;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task CleanupOldEventsAsync()
        {
            var oneWeekAgo = DateTime.Now.AddDays(-7);
            var oldEvents = await _context.Events
                //.Where(e => e.Review != null && e.Review.CreatedDate < oneWeekAgo)
                .ToListAsync();

            _context.Events.RemoveRange(oldEvents);
            await _context.SaveChangesAsync();
        }
    }
}
