using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CanvasHub.Models;
using CanvasHub.Services;
using CanvasHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;


namespace CanvasHub.Tests
{
    public class EventEditingServiceTests
    {
        private DbContextOptions<CanvasHubContext> _options;

        public EventEditingServiceTests()
        {
            _options = new DbContextOptionsBuilder<CanvasHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        private async Task SeedDatabase(CanvasHubContext context)
        {
            var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Id == "user1");
            if (existingUser == null)
            {
                var user = new User { Id = "user1" };
                context.Users.Add(user);

                var eventToEdit = new Event
                {
                    EventId = 1,
                    EventName = "Old Event Name",
                    Description = "Old Event Description",
                    User = user,
                    Resources = new List<Resource>(),
                    EventInvitations = new List<EventInvitation>
            {
                new EventInvitation { User = new User { Id = "user2" }, StatusInvitation = "Pending" }
            }
                };

                context.Events.Add(eventToEdit);
                await context.SaveChangesAsync();
            }
        }



        [Fact]
        public async Task EditEventAsync_EventNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            using (var context = new CanvasHubContext(_options))
            {
                var service = new EventEditingService(context);

                // Act & Assert
                await Assert.ThrowsAsync<InvalidOperationException>(() => service.EditEventAsync(1, "user2", "New Name", "New Description", new List<int>(), "Reason"));
            }
        }

        [Fact]
        public async Task EditEventAsync_NoReasonProvided_ThrowsArgumentNullException()
        {
            // Arrange
            using (var context = new CanvasHubContext(_options))
            {
                await SeedDatabase(context);
                var service = new EventEditingService(context);

                // Act & Assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => service.EditEventAsync(1, "user1", "New Name", "New Description", new List<int>(), null));
            }
        }

        [Fact]
        public async Task EditEventAsync_ValidEvent_UpdatesEventDetails()
        {
            // Arrange
            using (var context = new CanvasHubContext(_options))
            {
                await SeedDatabase(context);
                var service = new EventEditingService(context);

                var newName = "New Event Name";
                var newDescription = "New Event Description";

                // Act
                var result = await service.EditEventAsync(1, "user1", newName, newDescription, new List<int>(), "Reason");

                // Assert
                Assert.Equal(newName, result.EventName);
                Assert.Equal(newDescription, result.Description);
            }
        }

        [Fact]
        public async Task EditEventAsync_ValidEvent_AddsNotificationsForInvitedUsers()
        {
            // Arrange
            using (var context = new CanvasHubContext(_options))
            {
                await SeedDatabase(context);
                var service = new EventEditingService(context);

                // Act
                var result = await service.EditEventAsync(1, "user1", "New Name", "New Description", new List<int>(), "Reason");

                // Assert
                var notification = context.Notifications.FirstOrDefault(n => n.User.Id == "user2");
                Assert.NotNull(notification);
            }
        }
    }
    }