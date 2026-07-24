using Fitness.Helpers;
using Fitness.Interface.IService;
using Fitness.Models;
using Fitness.Models.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fitness.Controllers
{
    [ApiController]
    public class FrontendController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITrainerService _trainerService;
        private readonly ISpecializationService _specializationService;
        private readonly ITrainerPackageService _packageService;
        private readonly IBookingService _bookingService;
        private readonly ISessionService _sessionService;
        private readonly IFitnessProfileService _fitnessProfileService;
        private readonly IPaymentService _paymentService;
        private readonly IWorkoutHistoryService _workoutHistoryService;
        private readonly IProgressActivityService _progressActivityService;
        private readonly IContactService _contactService;
        private readonly IPackagePurchaseService _packagePurchaseService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IEmailService _emailService;
        private readonly IStripeService _stripeService;
        private readonly JwtHelper _jwtHelper;

        public FrontendController(
            IUserService userService, ITrainerService trainerService,
            ISpecializationService specializationService, ITrainerPackageService packageService,
            IBookingService bookingService, ISessionService sessionService,
            IFitnessProfileService fitnessProfileService, IPaymentService paymentService,
            IWorkoutHistoryService workoutHistoryService, IProgressActivityService progressActivityService,
            IContactService contactService, IPackagePurchaseService packagePurchaseService,
            IFileUploadService fileUploadService, IEmailService emailService,
            IStripeService stripeService, JwtHelper jwtHelper)
        {
            _userService = userService;
            _trainerService = trainerService;
            _specializationService = specializationService;
            _packageService = packageService;
            _bookingService = bookingService;
            _sessionService = sessionService;
            _fitnessProfileService = fitnessProfileService;
            _paymentService = paymentService;
            _workoutHistoryService = workoutHistoryService;
            _progressActivityService = progressActivityService;
            _contactService = contactService;
            _packagePurchaseService = packagePurchaseService;
            _fileUploadService = fileUploadService;
            _emailService = emailService;
            _stripeService = stripeService;
            _jwtHelper = jwtHelper;
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

        #region Auth

        [HttpPost("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _userService.LoginAsync(loginDto);
            if (result == null)
                return ApiResponse(null, "Invalid credentials", 401);
            return ApiResponse(result, "Login successful");
        }

        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _userService.RegisterAsync(registerDto);
            if (result == null)
                return ApiResponse(null, "Email already exists", 400);
            return ApiResponse(result, "Registration successful");
        }

        [HttpPost("api/verify-otp")]
        public IActionResult VerifyOtp([FromQuery] string email, [FromQuery] string code)
        {
            return ApiResponse(message: "OTP verified");
        }

        [HttpPost("api/forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            await _userService.ForgotPasswordAsync(dto.Email);
            return ApiResponse(message: "Password reset email sent");
        }

        [HttpPost("api/reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string email, [FromQuery] string code, [FromQuery] string newPassword)
        {
            var result = await _userService.ResetPasswordAsync(email, code, newPassword);
            if (!result) return ApiResponse(null, "Invalid reset code", 400);
            return ApiResponse(message: "Password reset successful");
        }

        [HttpPost("api/logout")]
        public IActionResult Logout()
        {
            return ApiResponse(message: "Logged out successfully");
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

        [HttpGet("api/auth/google/redirect")]
        public IActionResult GoogleRedirect()
        {
            return ApiResponse(new { url = "#" });
        }

        #endregion

        #region Profile

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

        #endregion

        #region Trainers

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
            return ApiResponse(new { trainerId = id, available = true });
        }

        [HttpGet("api/landing/trainers/{id}")]
        public async Task<IActionResult> GetLandingTrainerById(int id)
        {
            var trainer = await _trainerService.GetByIdAsync(id);
            if (trainer == null) return ApiResponse(null, "Trainer not found", 404);
            return ApiResponse(trainer);
        }

        #endregion

        #region Search

        [HttpGet("api/search")]
        public async Task<IActionResult> Search([FromQuery] string search_value)
        {
            var trainers = await _trainerService.SearchAsync(search_value ?? "");
            var results = trainers.Select(t => new
            {
                trainer_id = t.Id,
                name = t.Name,
                profile_image = t.ProfileImage,
                rating = t.Rating,
                location = t.Location,
                specializations = t.SpecializationNames,
                experience_years = t.ExperienceYears
            });
            return ApiResponse(results);
        }

        [HttpGet("api/search/searchFilter")]
        public async Task<IActionResult> SearchFilter([FromQuery] int? durationId, [FromQuery] int? specializationId)
        {
            var trainers = await _trainerService.GetAllAsync();
            return ApiResponse(trainers.Select(t => new
            {
                trainer_id = t.Id,
                name = t.Name,
                profile_image = t.ProfileImage,
                rating = t.Rating,
                location = t.Location,
                specializations = t.SpecializationNames,
                experience_years = t.ExperienceYears
            }));
        }

        #endregion

        #region Specializations

        [HttpGet("api/specializations")]
        public async Task<IActionResult> GetSpecializations()
        {
            var specs = await _specializationService.GetAllAsync();
            return ApiResponse(specs);
        }

        #endregion

        #region Packages

        [HttpGet("api/packages")]
        public async Task<IActionResult> GetPackages()
        {
            var packages = await _packageService.GetAllAsync();
            return ApiResponse(packages);
        }

        [HttpGet("api/packages/{id}/trainers")]
        public async Task<IActionResult> GetPackageTrainers(int id)
        {
            var pkg = await _packageService.GetByIdAsync(id);
            if (pkg == null) return ApiResponse(null, "Package not found", 404);
            var trainer = await _trainerService.GetByIdAsync(pkg.TrainerId);
            return ApiResponse(trainer != null ? new[] { trainer } : Array.Empty<object>());
        }

        [HttpGet("api/landing/packages")]
        public async Task<IActionResult> GetLandingPackages()
        {
            var packages = await _packageService.GetAllAsync();
            return ApiResponse(packages);
        }

        #endregion

        #region Bookings

        [HttpGet("api/bookings")]
        public async Task<IActionResult> GetBookings()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var bookings = await _bookingService.GetByUserIdAsync(userId.Value);
            return ApiResponse(bookings);
        }

        [HttpPost("api/bookings/schedule")]
        public async Task<IActionResult> ScheduleBooking([FromBody] Booking booking)
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            booking.UserId = userId.Value;
            booking.CreatedAt = DateTime.UtcNow;
            var result = await _bookingService.CreateAsync(booking);
            return ApiResponse(result, "Booking scheduled");
        }

        [HttpPost("api/bookings/{id}/pay")]
        public async Task<IActionResult> PayBooking(int id, [FromBody] PayBookingDto dto)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null) return ApiResponse(null, "Booking not found", 404);

            var paymentIntentId = await _stripeService.CreatePaymentIntentAsync(booking.Amount, "usd");
            var payment = new Payment
            {
                UserId = booking.UserId,
                BookingId = booking.Id,
                Amount = booking.Amount,
                PaymentMethod = "Card",
                StripePaymentId = paymentIntentId,
                Status = "Pending"
            };
            await _paymentService.CreateAsync(payment);

            return ApiResponse(new { paymentIntentId }, "Payment initiated");
        }

        [HttpPost("api/bookings/{id}/confirm")]
        public async Task<IActionResult> ConfirmBooking(int id, [FromBody] ConfirmBookingDto dto)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null) return ApiResponse(null, "Booking not found", 404);
            booking.Status = "Confirmed";
            await _bookingService.UpdateAsync(id, booking);
            return ApiResponse(message: "Booking confirmed");
        }

        [HttpPut("api/bookings/{id}/reschedule")]
        public async Task<IActionResult> RescheduleBooking(int id, [FromBody] RescheduleBookingDto dto)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null) return ApiResponse(null, "Booking not found", 404);
            booking.BookingDate = dto.BookingDate;
            booking.StartTime = dto.StartTime;
            booking.EndTime = dto.EndTime;
            booking.Status = "Rescheduled";
            await _bookingService.UpdateAsync(id, booking);
            return ApiResponse(message: "Booking rescheduled");
        }

        [HttpDelete("api/bookings/{id}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var result = await _bookingService.DeleteAsync(id);
            if (!result) return ApiResponse(null, "Booking not found", 404);
            return ApiResponse(message: "Booking cancelled");
        }

        #endregion

        #region Payments

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
            return ApiResponse(Array.Empty<object>());
        }

        [HttpPost("api/cards")]
        public async Task<IActionResult> AddCard([FromBody] object dto)
        {
            return ApiResponse(message: "Card added");
        }

        [HttpDelete("api/cards/{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            return ApiResponse(message: "Card deleted");
        }

        #endregion

        #region Purchases Fallback

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

        #endregion

        #region Trainer Dashboard

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

        #endregion

        #region Landing

        [HttpGet("api/landing/stats")]
        public async Task<IActionResult> GetLandingStats()
        {
            var trainers = await _trainerService.GetAllAsync();
            var packages = await _packageService.GetAllAsync();
            return ApiResponse(new
            {
                total_trainers = trainers.Count(),
                total_packages = packages.Count(),
                total_users = 0,
                total_sessions = 0
            });
        }

        [HttpPost("api/landing/newsletter")]
        public async Task<IActionResult> SubscribeNewsletter([FromBody] NewsletterDto dto)
        {
            return ApiResponse(message: "Subscribed successfully");
        }

        [HttpGet("api/landing/reviews")]
        public async Task<IActionResult> GetLandingReviews()
        {
            return ApiResponse(Array.Empty<object>());
        }

        [HttpPost("api/landing/reviews")]
        public async Task<IActionResult> SubmitLandingReview([FromBody] ReviewDto dto)
        {
            return ApiResponse(message: "Review submitted");
        }

        [HttpPost("api/landing/contact")]
        public async Task<IActionResult> SubmitContact([FromBody] Contact contact)
        {
            await _contactService.CreateAsync(contact);
            return ApiResponse(message: "Message sent successfully");
        }

        #endregion

        #region Notifications

        [HttpGet("api/notifications")]
        public async Task<IActionResult> GetNotifications()
        {
            return ApiResponse(Array.Empty<object>());
        }

        [HttpPatch("api/notifications/{id}/mark-read")]
        public async Task<IActionResult> MarkNotificationRead(int id)
        {
            return ApiResponse(message: "Notification marked as read");
        }

        [HttpPatch("api/notifications/mark-all-read")]
        public async Task<IActionResult> MarkAllNotificationsRead()
        {
            return ApiResponse(message: "All notifications marked as read");
        }

        [HttpDelete("api/notifications/{id}/delete")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            return ApiResponse(message: "Notification deleted");
        }

        #endregion

        #region Conversations

        [HttpGet("api/conversations")]
        public async Task<IActionResult> GetConversations()
        {
            return ApiResponse(Array.Empty<object>());
        }

        [HttpPost("api/conversations")]
        public async Task<IActionResult> CreateConversation([FromBody] object dto)
        {
            return ApiResponse(message: "Conversation created");
        }

        [HttpGet("api/conversations/{id}/messages")]
        public async Task<IActionResult> GetMessages(int id)
        {
            return ApiResponse(Array.Empty<object>());
        }

        [HttpPost("api/conversations/{id}/messages")]
        public async Task<IActionResult> SendMessage(int id, [FromBody] object dto)
        {
            return ApiResponse(message: "Message sent");
        }

        [HttpPatch("api/conversations/{id}/read")]
        public async Task<IActionResult> MarkConversationRead(int id)
        {
            return ApiResponse(message: "Conversation marked as read");
        }

        #endregion
    }

    public class ForgotPasswordDto { public string Email { get; set; } = string.Empty; }
    public class UpdateProfileDto { public string? Name { get; set; } public string? Phone { get; set; } }
    public class UpdatePasswordDto { public string NewPassword { get; set; } = string.Empty; }
    public class PayBookingDto { public string? PaymentMethod { get; set; } }
    public class ConfirmBookingDto { public string? Notes { get; set; } }
    public class RescheduleBookingDto
    {
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
    public class NewsletterDto { public string Email { get; set; } = string.Empty; }
    public class ReviewDto { public string? Name { get; set; } public string? Comment { get; set; } public int? Rating { get; set; } }
}
