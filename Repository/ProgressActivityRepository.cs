using Fitness.Data;
using Fitness.Interface.IRepository;
using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Repository
{
    public class ProgressActivityRepository : IProgressActivityRepository
    {
        private readonly AppDbContext _context;

        public ProgressActivityRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<ProgressActivity>> GetByUserIdAsync(int userId) =>
            await _context.ProgressActivities.Where(p => p.UserId == userId).ToListAsync();

        public async Task<ProgressActivity?> GetByIdAsync(int id) =>
            await _context.ProgressActivities.FindAsync(id);

        public async Task<ProgressActivity> CreateAsync(ProgressActivity progressActivity)
        {
            await _context.ProgressActivities.AddAsync(progressActivity);
            await SaveChangesAsync();
            return progressActivity;
        }

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
