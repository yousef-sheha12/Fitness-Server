using Fitness.Interface.IRepository;
using Fitness.Interface.IService;
using Fitness.Models;

namespace Fitness.Service
{
    public class ProgressActivityService : IProgressActivityService
    {
        private readonly IProgressActivityRepository _repo;
        public ProgressActivityService(IProgressActivityRepository repo) => _repo = repo;

        public async Task<IEnumerable<ProgressActivity>> GetByUserIdAsync(int userId) => await _repo.GetByUserIdAsync(userId);
        public async Task<ProgressActivity> CreateAsync(ProgressActivity progressActivity) => await _repo.CreateAsync(progressActivity);
    }
}
