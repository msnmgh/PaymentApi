using Microsoft.AspNetCore.Mvc;
using PaymentApi.Models;
using PaymentApi.Services;

namespace PaymentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PaymentRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _paymentService.CreatePaymentAsync(request);
            return Ok(new { transactionId = result.TransactionId, status = result.Status });
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook([FromBody] object payload, [FromHeader(Name = "X-Signature")] string signature)
        {
            var handled = await _paymentService.HandleWebhookAsync(payload, signature);
            if (!handled) return BadRequest();
            return Ok();
        }
    }
}
