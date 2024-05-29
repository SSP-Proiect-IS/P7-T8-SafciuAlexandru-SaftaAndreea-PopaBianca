using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CanvasHub.Models
{
    public class EventInvitation
    {
        public int EventInvitationId { get; set; }
        public string StatusInvitation { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int MessageId { get; set; }
        public bool IsRead { get; set; }
        public Message Message { get; set; }

        public int Id { get; set; }
        public User User { get; set; }
    }
}
