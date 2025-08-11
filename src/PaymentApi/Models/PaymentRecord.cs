using System.ComponentModel.DataAnnotations;

namespace PaymentApi.Models
{
    public class PaymentRecord
    {
        [Key]
        public int Id { get; set; }
        public string TransactionId { get; set; } = System.Guid.NewGuid().ToString();
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string? Status { get; set; } = "created";
        public System.DateTime CreatedAt { get; set; } = System.DateTime.UtcNow;
        public string? Metadata { get; set; }
    }
}
