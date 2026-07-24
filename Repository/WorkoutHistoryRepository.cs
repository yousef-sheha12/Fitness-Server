using Fitness.Data;
using Fitness.Interface.IRepository;
using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Repository
{
    public class WorkoutHistoryRepository : IWorkoutHistoryRepository
    {
        private readonly AppDbContext _context;

        public WorkoutHistoryRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<WorkoutHistory>> GetByUserIdAsync(int userId) =>
            await _context.WorkoutHistories.Where(w => w.UserId == userId).ToListAsync();

        public async Task<WorkoutHistory?> GetByIdAsync(int id) =>
            await _context.WorkoutHistories.FindAsync(id);

        public async Task<WorkoutHistory> CreateAsync(WorkoutHistory workoutHistory)
        {
            await _context.WorkoutHistories.AddAsync(workoutHistory);
            await SaveChangesAsync();
            return workoutHistory;
        }

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
