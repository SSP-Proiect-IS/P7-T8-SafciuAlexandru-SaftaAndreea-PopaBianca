using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CanvasHub.Models
{
    public class Resource
    {
        public int ResourceId { get; set; }
        public string ResourceType { get; set; }
        public string ResourceName { get; set; }
        public DateTime BookedDate { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
