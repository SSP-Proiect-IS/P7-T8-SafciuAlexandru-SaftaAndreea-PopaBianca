using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CanvasHub.Models
{
    public class ProfitReport
    {
        public float TotalIncome { get; set; }
        public float TotalCost { get; set; }
        public float TotalProfit { get; set; }
    }
}

