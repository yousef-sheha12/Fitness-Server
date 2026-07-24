using Fitness.Models;

namespace Fitness.Interface.IRepository
{
    public interface IWorkoutHistoryRepository
    {
        Task<IEnumerable<WorkoutHistory>> GetByUserIdAsync(int userId);
        Task<WorkoutHistory?> GetByIdAsync(int id);
        Task<WorkoutHistory> CreateAsync(WorkoutHistory workoutHistory);
        Task<bool> SaveChangesAsync();
    }
}
