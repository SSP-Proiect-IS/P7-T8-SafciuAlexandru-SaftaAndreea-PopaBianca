using System.ComponentModel.DataAnnotations;

namespace CanvasHub.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string ReviewName { get; set; }
        public string ReviewDescription { get; set; }

        public int EventId { get; set; }
        public int Rating { get; set; }
        public Event Event { get; set; }
    }
}
