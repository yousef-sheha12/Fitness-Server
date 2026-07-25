using Fitness.Helpers;
using Fitness.Interface.IService;
using Fitness.Models;
using Fitness.Models.DTOs.Profile;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fitness.Controllers
{
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITrainerService _trainerService;
        private readonly ISessionService _sessionService;
        private readonly ITrainerPackageService _packageService;
        private readonly IBookingService _bookingService;
        private readonly IFitnessProfileService _fitnessProfileService;
        private readonly IWorkoutHistoryService _workoutHistoryService;
        private readonly IProgressActivityService _progressActivityService;
        private readonly IFileUploadService _fileUploadService;

        public ProfileController(
            IUserService userService, ITrainerService trainerService,
            ISessionService sessionService, ITrainerPackageService packageService,
            IBookingService bookingService, IFitnessProfileService fitnessProfileService,
            IWorkoutHistoryService workoutHistoryService, IProgressActivityService progressActivityService,
            IFileUploadService fileUploadService)
        {
            _userService = userService;
            _trainerService = trainerService;
            _sessionService = sessionService;
            _packageService = packageService;
            _bookingService = bookingService;
            _fitnessProfileService = fitnessProfileService;
            _workoutHistoryService = workoutHistoryService;
            _progressActivityService = progressActivityService;
            _fileUploadService = fileUploadService;
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

        [HttpGet("api/profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var user = await _userService.GetByIdAsync(userId.Value);
            if (user == null) return ApiResponse(null, "User not found", 404);
            return ApiResponse(new { user.Id, user.Name, user.Email, user.Phone, user.ProfileImage, user.Dob, user.CreatedAt });
        }

        [HttpPut("api/profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto)
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var user = await _userService.GetByIdAsync(userId.Value);
            if (user == null) return ApiResponse(null, "User not found", 404);
            user.Name = dto.Name ?? user.Name;
            user.Phone = dto.Phone ?? user.Phone;
            await _userService.UpdateAsync(userId.Value, user);
            return ApiResponse(new { user.Id, user.Name, user.Email, user.Phone, user.ProfileImage }, "Profile updated");
        }

        [HttpPost("api/profile/upload-image")]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            if (file == null || file.Length == 0) return ApiResponse(null, "No file uploaded", 400);
            var path = await _fileUploadService.UploadFileAsync(file, "profiles");
            var user = await _userService.GetByIdAsync(userId.Value);
            if (user == null) return ApiResponse(null, "User not found", 404);
            user.ProfileImage = path;
            await _userService.UpdateAsync(userId.Value, user);
            return ApiResponse(new { profileImage = path }, "Image uploaded");
        }

        [HttpPost("api/profile/fitness-profile")]
        public async Task<IActionResult> SaveFitnessProfile([FromBody] FitnessProfile profile)
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            profile.UserId = userId.Value;
            await _fitnessProfileService.UpdateAsync(userId.Value, profile);
            return ApiResponse(message: "Fitness profile saved");
        }

        [HttpPut("api/profile/password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto dto)
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var user = await _userService.GetByIdAsync(userId.Value);
            if (user == null) return ApiResponse(null, "User not found", 404);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            await _userService.UpdateAsync(userId.Value, user);
            return ApiResponse(message: "Password updated");
        }

        [HttpDelete("api/delete-account")]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            await _userService.DeleteAsync(userId.Value);
            return ApiResponse(message: "Account deleted");
        }

        [HttpGet("api/profile/sessions")]
        public async Task<IActionResult> GetProfileSessions()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var trainer = await _trainerService.GetByUserIdAsync(userId.Value);
            if (trainer == null) return ApiResponse(Array.Empty<object>());
            var sessions = await _sessionService.GetByTrainerIdAsync(trainer.Id);
            return ApiResponse(sessions);
        }

        [HttpGet("api/profile/packages")]
        public async Task<IActionResult> GetProfilePackages()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var trainer = await _trainerService.GetByUserIdAsync(userId.Value);
            if (trainer == null) return ApiResponse(Array.Empty<object>());
            var packages = await _packageService.GetByTrainerIdAsync(trainer.Id);
            return ApiResponse(packages);
        }

        [HttpGet("api/profile/bookings")]
        public async Task<IActionResult> GetProfileBookings()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var bookings = await _bookingService.GetByUserIdAsync(userId.Value);
            return ApiResponse(bookings);
        }

        [HttpGet("api/profile/progress-activity")]
        public async Task<IActionResult> GetProgressActivity()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var activities = await _progressActivityService.GetByUserIdAsync(userId.Value);
            return ApiResponse(activities);
        }

        [HttpGet("api/profile/workout-history")]
        public async Task<IActionResult> GetWorkoutHistory()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var history = await _workoutHistoryService.GetByUserIdAsync(userId.Value);
            return ApiResponse(history);
        }
    }
}
