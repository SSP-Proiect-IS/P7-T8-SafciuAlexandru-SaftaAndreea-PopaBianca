using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CanvasHub.Models;

namespace CanvasHub.Services
{
    public interface IEventService
    {
        Task<Event> AddEventAsync(string userId, string eventName, string description, List<int> resourceIds, List<string> invitedUserIds);
    }
}
