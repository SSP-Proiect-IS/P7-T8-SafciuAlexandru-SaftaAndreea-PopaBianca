using System.ComponentModel.DataAnnotations;

namespace CanvasHub.Models
{
    public class Message
    {
        public int MessageId { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsRead { get; set; }

        public EventInvitation EventInvitation { get; set; }
    }
}
