using Fitness.Interface.IService;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly ITrainerPackageService _packageService;
        private readonly ITrainerService _trainerService;

        public PackagesController(ITrainerPackageService packageService, ITrainerService trainerService)
        {
            _packageService = packageService;
            _trainerService = trainerService;
        }

        private IActionResult ApiResponse(object? data = null, string message = "Success", int statusCode = 200)
        {
            return StatusCode(statusCode, new { success = true, message, data });
        }

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
    }
}
