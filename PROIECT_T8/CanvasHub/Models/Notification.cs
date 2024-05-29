using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CanvasHub.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Subject { get; set; }
        public bool IsRead { get; set; }
        public Message Message { get; set; }

        public int Id { get; set; }
        public User User { get; set; }
    }
}
