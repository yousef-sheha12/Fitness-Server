using Fitness.Models;

namespace Fitness.Interface.IService
{
    public interface IWorkoutHistoryService
    {
        Task<IEnumerable<WorkoutHistory>> GetByUserIdAsync(int userId);
        Task<WorkoutHistory> CreateAsync(WorkoutHistory workoutHistory);
    }
}
