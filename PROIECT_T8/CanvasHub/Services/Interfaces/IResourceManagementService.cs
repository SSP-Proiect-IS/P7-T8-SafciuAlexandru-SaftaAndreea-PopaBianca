namespace CanvasHub.Services.Interfaces
{
    public interface IResourceManagementService
    {
        Task AddResourceAsync(string resourceName, string resourceType, int resourceId);
        Task RemoveResourceAsync(int resourceId);
    }
}
