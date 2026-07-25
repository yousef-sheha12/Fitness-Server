using Fitness.Interface.IService;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fitness.Controllers
{
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPackagePurchaseService _packagePurchaseService;

        public PurchasesController(IPackagePurchaseService packagePurchaseService)
        {
            _packagePurchaseService = packagePurchaseService;
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

        [HttpGet("api/my-packages")]
        public async Task<IActionResult> GetMyPackages()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var purchases = await _packagePurchaseService.GetByUserIdAsync(userId.Value);
            return ApiResponse(purchases);
        }

        [HttpGet("api/user-packages")]
        public async Task<IActionResult> GetUserPackages()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var purchases = await _packagePurchaseService.GetByUserIdAsync(userId.Value);
            return ApiResponse(purchases);
        }

        [HttpGet("api/purchases")]
        public async Task<IActionResult> GetPurchases()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var purchases = await _packagePurchaseService.GetByUserIdAsync(userId.Value);
            return ApiResponse(purchases);
        }
    }
}
