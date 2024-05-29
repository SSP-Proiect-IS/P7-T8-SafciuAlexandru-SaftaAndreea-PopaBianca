using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CanvasHub.Models
{
    public class User : IdentityUser
    {
        public bool FreeResourceBool { get; set; } = false;
        public float Subscription { get; set; } = 0.0f;
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}