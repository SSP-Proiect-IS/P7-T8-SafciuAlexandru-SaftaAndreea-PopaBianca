using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CanvasHub.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }

        public int Id { get; set; }
        public User User { get; set; }
        
        public Review Review { get; set; }

        public ICollection<Resource> Resources { get; set; }
        public ICollection<EventInvitation> EventInvitations { get; set; }
    }

}
