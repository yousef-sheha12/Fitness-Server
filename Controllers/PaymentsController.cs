using Fitness.Interface.IService;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fitness.Controllers
{
    [ApiController]
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
        public IActionResult GetCards()
        {
            return ApiResponse(Array.Empty<object>());
        }

        [HttpPost("api/cards")]
        public IActionResult AddCard([FromBody] object dto)
        {
            return ApiResponse(message: "Card added");
        }

        [HttpDelete("api/cards/{id}")]
        public IActionResult DeleteCard(int id)
        {
            return ApiResponse(message: "Card deleted");
        }
    }
}
