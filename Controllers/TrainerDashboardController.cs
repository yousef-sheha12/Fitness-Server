using Fitness.Interface.IService;
using Fitness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fitness.Controllers
{
    [ApiController]
    [Authorize]
    public class TrainerDashboardController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        private readonly ISessionService _sessionService;
        private readonly IBookingService _bookingService;
        private readonly ITrainerPackageService _packageService;

        public TrainerDashboardController(
            ITrainerService trainerService,
            ISessionService sessionService,
            IBookingService bookingService,
            ITrainerPackageService packageService)
        {
            _trainerService = trainerService;
            _sessionService = sessionService;
            _bookingService = bookingService;
            _packageService = packageService;
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

        [HttpGet("api/trainer/sessions")]
        public async Task<IActionResult> GetTrainerSessions()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var trainer = await _trainerService.GetByUserIdAsync(userId.Value);
            if (trainer == null) return ApiResponse(Array.Empty<object>());
            var sessions = await _sessionService.GetByTrainerIdAsync(trainer.Id);
            return ApiResponse(sessions);
        }

        [HttpGet("api/trainer/bookings")]
        public async Task<IActionResult> GetTrainerBookings()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var trainer = await _trainerService.GetByUserIdAsync(userId.Value);
            if (trainer == null) return ApiResponse(Array.Empty<object>());
            var bookings = await _bookingService.GetByTrainerIdAsync(trainer.Id);
            return ApiResponse(bookings);
        }

        [HttpGet("api/trainer/packages")]
        public async Task<IActionResult> GetTrainerPackages()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var trainer = await _trainerService.GetByUserIdAsync(userId.Value);
            if (trainer == null) return ApiResponse(Array.Empty<object>());
            var packages = await _packageService.GetByTrainerIdAsync(trainer.Id);
            return ApiResponse(packages);
        }

        [HttpPost("api/trainer/packages")]
        public async Task<IActionResult> CreateTrainerPackage([FromBody] TrainerPackage trainerPackage)
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var trainer = await _trainerService.GetByUserIdAsync(userId.Value);
            if (trainer == null) return ApiResponse(null, "Trainer not found", 404);
            trainerPackage.TrainerId = trainer.Id;
            var result = await _packageService.CreateAsync(trainerPackage);
            return ApiResponse(result, "Package created");
        }

        [HttpPut("api/trainer/packages/{id}")]
        public async Task<IActionResult> UpdateTrainerPackage(int id, [FromBody] TrainerPackage trainerPackage)
        {
            var result = await _packageService.UpdateAsync(id, trainerPackage);
            if (result == null) return ApiResponse(null, "Package not found", 404);
            return ApiResponse(result, "Package updated");
        }

        [HttpDelete("api/trainer/packages/{id}")]
        public async Task<IActionResult> DeleteTrainerPackage(int id)
        {
            var result = await _packageService.DeleteAsync(id);
            if (!result) return ApiResponse(null, "Package not found", 404);
            return ApiResponse(message: "Package deleted");
        }
    }
}
