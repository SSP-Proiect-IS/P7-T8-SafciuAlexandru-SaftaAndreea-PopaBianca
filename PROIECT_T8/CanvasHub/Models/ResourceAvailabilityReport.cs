using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CanvasHub.Models
{
    public class ResourceAvailabilityReport
    {
        public int ResourceId { get; set; }
        public string ResourceType { get; set; }
        public string ResourceName { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
    }
}
