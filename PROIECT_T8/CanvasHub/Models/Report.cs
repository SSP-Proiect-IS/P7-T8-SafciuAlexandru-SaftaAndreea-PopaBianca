using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CanvasHub.Models
{
    public class Report
    {
        public int ReportId { get; set; }

        public string ReportType { get; set; }
        public string ReportName { get; set; }
        public string ReportDetails { get; set; }
        public DateTime GeneratedDate { get; set; }

        public int Id { get; set; }
        public User User { get; set; }
    }
}
