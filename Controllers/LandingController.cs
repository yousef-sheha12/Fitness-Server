using Fitness.Interface.IService;
using Fitness.Models;
using Fitness.Models.DTOs.Landing;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    [ApiController]
    public class LandingController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        private readonly ITrainerPackageService _packageService;
        private readonly IContactService _contactService;

        public LandingController(
            ITrainerService trainerService,
            ITrainerPackageService packageService,
            IContactService contactService)
        {
            _trainerService = trainerService;
            _packageService = packageService;
            _contactService = contactService;
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
            return ApiResponse(new
            {
                total_trainers = trainers.Count(),
                total_packages = packages.Count(),
                total_users = 0,
                total_sessions = 0
            });
        }

        [HttpPost("api/landing/newsletter")]
        public IActionResult SubscribeNewsletter([FromBody] NewsletterDto dto)
        {
            return ApiResponse(message: "Subscribed successfully");
        }

        [HttpGet("api/landing/reviews")]
        public IActionResult GetLandingReviews()
        {
            return ApiResponse(Array.Empty<object>());
        }

        [HttpPost("api/landing/reviews")]
        public IActionResult SubmitLandingReview([FromBody] ReviewDto dto)
        {
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
