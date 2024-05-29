using System.Threading.Tasks;
using CanvasHub.Models;

namespace CanvasHub.Services
{
    public interface IMembershipService
    {
        Task<bool> AddMemberAsync(User user);
        Task<bool> AddAdminAsync(User user);
    }
}