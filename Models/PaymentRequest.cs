using System.ComponentModel.DataAnnotations;

namespace PaymentApi.Models
{
    public class PaymentRequest
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Currency { get; set; } = "USD";
        public string Description { get; set; }
        public string? CustomerEmail { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
