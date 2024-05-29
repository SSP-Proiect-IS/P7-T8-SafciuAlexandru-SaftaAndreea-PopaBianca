using System;
using System.Linq;
using System.Threading.Tasks;
using CanvasHub.Models;
using CanvasHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CanvasHub.Services
{
    public class ResourceManagementService : IResourceManagementService
    {
        private readonly DbSet<Resource> _resources;
        private readonly CanvasHubContext _context;

        public ResourceManagementService(CanvasHubContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _resources = _context.Set<Resource>();
        }

        public async Task AddResourceAsync(string resourceName, string resourceType, int resourceId)
        {
            if (string.IsNullOrEmpty(resourceName))
            {
                throw new ArgumentNullException(nameof(resourceName), "Resource name cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(resourceType))
            {
                throw new ArgumentNullException(nameof(resourceType), "Resource type cannot be null or empty.");
            }

            if (resourceId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(resourceId), "Resource ID must be greater than 0.");
            }

            // Check if resource with the same ID already exists
            if (_resources.Any(r => r.ResourceId == resourceId))
            {
                throw new InvalidOperationException($"Resource with ID {resourceId} already exists.");
            }

            var newResource = new Resource
            {
                ResourceName = resourceName,
                ResourceType = resourceType,
                ResourceId = resourceId
            };

            _resources.Add(newResource);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveResourceAsync(int resourceId)
        {
            var resource = await _resources.FindAsync(resourceId);
            if (resource == null)
            {
                throw new InvalidOperationException($"Resource with ID {resourceId} not found.");
            }

            _resources.Remove(resource);
            await _context.SaveChangesAsync();
        }
    }
}
