using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CanvasHub.Models
{
    public class ResourceCalendarReport
    {
        public int ResourceId { get; set; }
        public string ResourceType { get; set; }
        public string ResourceName { get; set; }
        public string Schedule { get; set; }
    }
}
