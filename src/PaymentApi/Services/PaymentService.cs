using Microsoft.EntityFrameworkCore;
using PaymentApi.Data;
using PaymentApi.Models;
using System.Text;
using System.Security.Cryptography;

namespace PaymentApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(AppDbContext db, IConfiguration config, ILogger<PaymentService> logger)
        {
            _db = db;
            _config = config;
            _logger = logger;
        }

        public async Task<PaymentRecord> CreatePaymentAsync(PaymentRequest request)
        {
            var payment = new PaymentRecord
            {
                TransactionId = System.Guid.NewGuid().ToString(),
                Amount = request.Amount,
                Currency = request.Currency,
                Status = "created",
                Metadata = request.Description
            };

            _db.Payments.Add(payment);
            await _db.SaveChangesAsync();

            // In a real integration you would call payment provider APIs here.
            return payment;
        }

        public Task<bool> HandleWebhookAsync(object payload, string signature)
        {
            var secret = _config["WebhookSecret"];
            if (string.IsNullOrEmpty(secret))
            {
                _logger.LogWarning("Webhook secret not configured.");
                return Task.FromResult(false);
            }

            var payloadJson = System.Text.Json.JsonSerializer.Serialize(payload);
            var computed = ComputeHmacSha256(payloadJson, secret);

            if (!string.Equals(computed, signature))
            {
                _logger.LogWarning("Invalid webhook signature.");
                return Task.FromResult(false);
            }

            _logger.LogInformation("Received valid webhook: {payload}", payloadJson);
            return Task.FromResult(true);
        }

        private static string ComputeHmacSha256(string message, string secret)
        {
            var key = Encoding.UTF8.GetBytes(secret);
            using var hmac = new HMACSHA256(key);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            return Convert.ToHexString(hash).ToLower();
        }
    }
}
