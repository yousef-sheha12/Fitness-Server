using Fitness.Interface.IService;
using Fitness.Models;
using Fitness.Models.DTOs.Landing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    [ApiController]
    public class LandingController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        private readonly ITrainerPackageService _packageService;
        private readonly IContactService _contactService;
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;

        private static readonly List<object> _reviews = new()
        {
            new { name = "Sarah Johnson", rating = 5, comment = "Amazing trainers and facilities! I've made incredible progress in just 3 months." },
            new { name = "Mike Chen", rating = 4, comment = "Great package options and the trainers are very professional." },
            new { name = "Emily Davis", rating = 5, comment = "Best fitness platform I've ever used. Highly recommend!" }
        };

        public LandingController(
            ITrainerService trainerService,
            ITrainerPackageService packageService,
            IContactService contactService,
            IUserService userService,
            ISessionService sessionService)
        {
            _trainerService = trainerService;
            _packageService = packageService;
            _contactService = contactService;
            _userService = userService;
            _sessionService = sessionService;
        }

        private IActionResult ApiResponse(object? data = null, string message = "Success", int statusCode = 200)
        {
            return StatusCode(statusCode, new { success = true, message, data });
        }

        [HttpGet("api/landing/stats")]
        public async Task<IActionResult> GetLandingStats()
        {
            var trainers = await _trainerService.GetAllAsync();
            var packages = await _packageService.GetAllAsync();
            var sessions = await _sessionService.GetAllAsync();

            return ApiResponse(new
            {
                total_trainers = trainers.Count(),
                total_packages = packages.Count(),
                total_sessions = sessions.Count(),
                total_users = 0
            });
        }

        [HttpPost("api/landing/newsletter")]
        public async Task<IActionResult> SubscribeNewsletter([FromBody] NewsletterDto dto)
        {
            var contact = new Contact
            {
                Name = "Newsletter Subscriber",
                Email = dto.Email,
                Subject = "Newsletter Subscription",
                Message = "Subscribed to newsletter"
            };
            await _contactService.CreateAsync(contact);
            return ApiResponse(message: "Subscribed successfully");
        }

        [HttpGet("api/landing/reviews")]
        public IActionResult GetLandingReviews()
        {
            return ApiResponse(_reviews);
        }

        [HttpPost("api/landing/reviews")]
        public IActionResult SubmitLandingReview([FromBody] ReviewDto dto)
        {
            _reviews.Add(new { name = dto.Name ?? "Anonymous", rating = dto.Rating ?? 5, comment = dto.Comment ?? "" });
            return ApiResponse(message: "Review submitted");
        }

        [HttpPost("api/landing/contact")]
        public async Task<IActionResult> SubmitContact([FromBody] Contact contact)
        {
            await _contactService.CreateAsync(contact);
            return ApiResponse(message: "Message sent successfully");
        }
    }
}
