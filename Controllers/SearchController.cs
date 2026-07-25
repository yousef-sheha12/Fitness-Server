using Fitness.Interface.IService;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ITrainerService _trainerService;

        public SearchController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        private IActionResult ApiResponse(object? data = null, string message = "Success", int statusCode = 200)
        {
            return StatusCode(statusCode, new { success = true, message, data });
        }

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
    }
}
