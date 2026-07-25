using Fitness.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fitness.Controllers
{
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        private int? GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : null;
        }

        private IActionResult ApiResponse(object? data = null, string message = "Success", int statusCode = 200)
        {
            return StatusCode(statusCode, new { success = true, message, data });
        }

        [HttpGet("api/payments-history")]
        public async Task<IActionResult> GetPaymentsHistory()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var payments = await _paymentService.GetByUserIdAsync(userId.Value);
            return ApiResponse(payments);
        }

        [HttpGet("api/cards")]
        public async Task<IActionResult> GetCards()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var payments = await _paymentService.GetByUserIdAsync(userId.Value);
            var cards = payments
                .Where(p => p.PaymentMethod == "Card" && !string.IsNullOrEmpty(p.StripePaymentId))
                .GroupBy(p => p.StripePaymentId)
                .Select(g => new
                {
                    id = g.Key,
                    last4 = "4242",
                    brand = "Visa",
                    expMonth = 12,
                    expYear = 2028,
                    isDefault = g.First() == payments.First()
                })
                .ToList();
            return ApiResponse(cards);
        }

        [HttpPost("api/cards")]
        public IActionResult AddCard([FromBody] object dto)
        {
            return ApiResponse(message: "Card added");
        }

        [HttpDelete("api/cards/{id}")]
        public IActionResult DeleteCard(string id)
        {
            return ApiResponse(message: "Card deleted");
        }
    }
}
