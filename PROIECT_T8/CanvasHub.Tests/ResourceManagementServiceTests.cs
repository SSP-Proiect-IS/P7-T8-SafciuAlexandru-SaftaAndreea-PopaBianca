using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CanvasHub.Models;
using CanvasHub.Services;

namespace CanvasHub.Tests
{
    public class ResourceManagementServiceTests
    {
        [Fact]
        public async Task AddResourceAsync_Success()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CanvasHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Clear existing data from the database
            using (var context = new CanvasHubContext(options))
            {
                await context.Database.EnsureDeletedAsync();
            }

            // Act
            using (var context = new CanvasHubContext(options))
            {
                var resourceManagementService = new ResourceManagementService(context);
                await resourceManagementService.AddResourceAsync("Resource1", "Type1", 1);
            }

            // Assert
            using (var context = new CanvasHubContext(options))
            {
                var resource = await context.Resources.FindAsync(1);
                Assert.NotNull(resource);
                Assert.Equal("Resource1", resource.ResourceName);
                Assert.Equal("Type1", resource.ResourceType);
                Assert.Equal(1, resource.ResourceId);
            }
        }


        [Fact]
        public async Task RemoveResourceAsync_Success()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CanvasHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Insert a resource into the database
            using (var context = new CanvasHubContext(options))
            {
                var resource = new Resource
                {
                    ResourceId = 1,
                    ResourceName = "Resource1",
                    ResourceType = "Type1"
                };
                await context.Resources.AddAsync(resource);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new CanvasHubContext(options))
            {
                var resourceManagementService = new ResourceManagementService(context);
                await resourceManagementService.RemoveResourceAsync(1);
            }

            // Assert
            using (var context = new CanvasHubContext(options))
            {
                var resource = await context.Resources.FindAsync(1);
                Assert.Null(resource);
            }
        }
    }
}
