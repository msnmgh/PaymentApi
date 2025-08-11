using Xunit;
using Microsoft.EntityFrameworkCore;
using PaymentApi.Data;
using PaymentApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using PaymentApi.Models;
using System.Collections.Generic;

namespace PaymentApi.Tests
{
    public class PaymentServiceTests
    {
        [Fact]
        public async System.Threading.Tasks.Task CreatePaymentAsync_CreatesRecord()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            using var db = new AppDbContext(options);
            var inMemorySettings = new Dictionary<string, string>
            {
                {"WebhookSecret", "testsecret"}
            };
            IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            var logger = new NullLogger<PaymentService>();
            var service = new PaymentService(db, config, logger);

            var req = new PaymentRequest { Amount = 10.5m, Currency = "USD", Description = "test" };
            var result = await service.CreatePaymentAsync(req);

            Assert.NotNull(result);
            Assert.Equal(10.5m, result.Amount);
            Assert.Equal("created", result.Status);
        }
    }
}
