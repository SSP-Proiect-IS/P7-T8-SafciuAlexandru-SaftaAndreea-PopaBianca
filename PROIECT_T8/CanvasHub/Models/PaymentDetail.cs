using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CanvasHub.Models
{
    public class PaymentDetail
    {
        public string UserName { get; set; }
        public float TotalAmount { get; set; }
    }
}
