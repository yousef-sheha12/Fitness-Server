using Fitness.Data;
using Fitness.Interface.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Controllers
{
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        private readonly ISessionService _sessionService;
        private readonly AppDbContext _context;

        public TrainersController(ITrainerService trainerService, ISessionService sessionService, AppDbContext context)
        {
            _trainerService = trainerService;
            _sessionService = sessionService;
            _context = context;
        }

        private IActionResult ApiResponse(object? data = null, string message = "Success", int statusCode = 200)
        {
            return StatusCode(statusCode, new { success = true, message, data });
        }

        [HttpGet("api/trainers")]
        public async Task<IActionResult> GetTrainers()
        {
            var trainers = await _trainerService.GetAllAsync();
            return ApiResponse(trainers);
        }

        [HttpGet("api/trainers/{id}")]
        public async Task<IActionResult> GetTrainerById(int id)
        {
            var trainer = await _trainerService.GetByIdAsync(id);
            if (trainer == null) return ApiResponse(null, "Trainer not found", 404);
            return ApiResponse(trainer);
        }

        [HttpGet("api/trainers/{id}/schedule")]
        public async Task<IActionResult> GetTrainerSchedule(int id)
        {
            var sessions = await _sessionService.GetByTrainerIdAsync(id);
            return ApiResponse(sessions);
        }

        [HttpGet("api/trainers/{id}/availability")]
        public async Task<IActionResult> GetTrainerAvailability(int id)
        {
            var trainer = await _trainerService.GetByIdAsync(id);
            if (trainer == null) return ApiResponse(null, "Trainer not found", 404);

            var now = DateTime.UtcNow;
            var hasActiveSession = await _context.Sessions
                .AnyAsync(s => s.TrainerId == id &&
                               s.SessionDate.Date == now.Date &&
                               s.Status != "Cancelled");

            var upcomingBookings = await _context.Bookings
                .Where(b => b.TrainerId == id && b.BookingDate.Date == now.Date && b.Status != "Cancelled")
                .CountAsync();

            var available = !hasActiveSession && upcomingBookings < 8;

            return ApiResponse(new { trainerId = id, available });
        }

        [HttpGet("api/landing/trainers/{id}")]
        public async Task<IActionResult> GetLandingTrainerById(int id)
        {
            var trainer = await _trainerService.GetByIdAsync(id);
            if (trainer == null) return ApiResponse(null, "Trainer not found", 404);
            return ApiResponse(trainer);
        }
    }
}
