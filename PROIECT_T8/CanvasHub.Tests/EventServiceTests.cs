using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CanvasHub.Models;
using CanvasHub.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CanvasHub.Tests
{
    public class EventServiceTests
    {
        private readonly EventService _eventService;
        private readonly CanvasHubContext _context;

        public EventServiceTests()
        {
            var options = new DbContextOptionsBuilder<CanvasHubContext>()
                .UseInMemoryDatabase(databaseName: "CanvasHubTestDatabase")
                .Options;

            _context = new CanvasHubContext(options);

            _eventService = new EventService(_context);
        }

        [Fact]
        public async Task AddEventAsync_WithValidInputs_ShouldCreateEvent()
        {
            // Arrange
            var userId = "testUserId123";
            var eventName = "Test Event";
            var description = "This is a test event";
            var resourceIds = new List<int>();
            var invitedUserIds = new List<string>();

            var user = new User { Id = userId, UserName = "testuser" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _eventService.AddEventAsync(userId, eventName, description, resourceIds, invitedUserIds);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(eventName, result.EventName);
            Assert.Equal(description, result.Description);
            Assert.Equal(user, result.User);
        }

        [Fact]
        public async Task AddEventAsync_InvalidUserId_ShouldThrowException()
        {
            // Arrange
            var userId = "invalidUserId";
            var eventName = "Test Event";
            var description = "This is a test event";
            var resourceIds = new List<int>();
            var invitedUserIds = new List<string>();

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _eventService.AddEventAsync(userId, eventName, description, resourceIds, invitedUserIds));
        }

        [Fact]
        public async Task AddEventAsync_WithResources_ShouldAddResourcesToEvent()
        {
            // Arrange
            var userId = "testUserIdasdasddddd";
            var eventName = "Test Event";
            var description = "This is a test event";
            var resourceIds = new List<int> { 1, 2 };
            var invitedUserIds = new List<string>();

            var user = new User { Id = userId, UserName = "testuser" };
            _context.Users.Add(user);

            var resource1 = new Resource { ResourceId = 1, ResourceType = "Type1", ResourceName = "Resource 1" };
            var resource2 = new Resource { ResourceId = 2, ResourceType = "Type2", ResourceName = "Resource 2" };
            _context.Resources.AddRange(resource1, resource2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _eventService.AddEventAsync(userId, eventName, description, resourceIds, invitedUserIds);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.Resources, r => r.ResourceId == resource1.ResourceId);
            Assert.Contains(result.Resources, r => r.ResourceId == resource2.ResourceId);
        }

        [Fact]
        public async Task AddEventAsync_WithInvitedUsers_ShouldAddInvitationsToEvent()
        {
            // Arrange
            var userId = "testUserIdasdasd";
            var eventName = "Test Event";
            var description = "This is a test event";
            var resourceIds = new List<int>();
            var invitedUserIds = new List<string> { "invitedUser1", "invitedUser2" };

            var user = new User { Id = userId, UserName = "testuser" };
            _context.Users.Add(user);
            var invitedUser1 = new User { Id = "invitedUser1", UserName = "inviteduser1" };
            var invitedUser2 = new User { Id = "invitedUser2", UserName = "inviteduser2" };
            _context.Users.AddRange(invitedUser1, invitedUser2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _eventService.AddEventAsync(userId, eventName, description, resourceIds, invitedUserIds);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.EventInvitations, i => i.User.Id == invitedUser1.Id && i.StatusInvitation == "Pending");
            Assert.Contains(result.EventInvitations, i => i.User.Id == invitedUser2.Id && i.StatusInvitation == "Pending");
        }
    }
}
