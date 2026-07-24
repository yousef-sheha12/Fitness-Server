using Fitness.Interface.IRepository;
using Fitness.Interface.IService;
using Fitness.Models;

namespace Fitness.Service
{
    public class WorkoutHistoryService : IWorkoutHistoryService
    {
        private readonly IWorkoutHistoryRepository _repo;
        public WorkoutHistoryService(IWorkoutHistoryRepository repo) => _repo = repo;

        public async Task<IEnumerable<WorkoutHistory>> GetByUserIdAsync(int userId) => await _repo.GetByUserIdAsync(userId);
        public async Task<WorkoutHistory> CreateAsync(WorkoutHistory workoutHistory) => await _repo.CreateAsync(workoutHistory);
    }
}
