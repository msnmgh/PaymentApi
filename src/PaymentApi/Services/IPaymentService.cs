using PaymentApi.Models;

namespace PaymentApi.Services
{
    public interface IPaymentService
    {
        Task<PaymentRecord> CreatePaymentAsync(PaymentRequest request);
        Task<bool> HandleWebhookAsync(object payload, string signature);
    }
}
