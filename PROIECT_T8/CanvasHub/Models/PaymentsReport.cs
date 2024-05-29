using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CanvasHub.Models
{
    public class PaymentsReport
    {
        public List<PaymentDetail> Payments { get; set; }
    }
}
