using Fitness.Data;
using Fitness.Interface.IService;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        private readonly AppDbContext _context;

        public SearchController(ITrainerService trainerService, AppDbContext context)
        {
            _trainerService = trainerService;
            _context = context;
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

            if (specializationId.HasValue)
            {
                var trainerIds = _context.TrainerSpecializations
                    .Where(ts => ts.SpecializationId == specializationId.Value)
                    .Select(ts => ts.TrainerId)
                    .ToList();
                trainers = trainers.Where(t => trainerIds.Contains(t.Id));
            }

            if (durationId.HasValue)
            {
                var trainerIdsWithPackages = _context.TrainerPackages
                    .Where(tp => tp.DurationDays >= durationId.Value * 30 && tp.DurationDays < (durationId.Value + 1) * 30)
                    .Select(tp => tp.TrainerId)
                    .Distinct()
                    .ToList();
                trainers = trainers.Where(t => trainerIdsWithPackages.Contains(t.Id));
            }

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
