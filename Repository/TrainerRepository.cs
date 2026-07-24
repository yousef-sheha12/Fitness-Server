using Fitness.Data;
using Fitness.Interface.IRepository;
using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Repository
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly AppDbContext _context;

        public TrainerRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Trainer>> GetAllAsync() =>
            await _context.Trainers
                .Include(t => t.TrainerSpecializations)
                    .ThenInclude(ts => ts.Specialization)
                .ToListAsync();

        public async Task<Trainer?> GetByIdAsync(int id) =>
            await _context.Trainers
                .Include(t => t.TrainerSpecializations)
                    .ThenInclude(ts => ts.Specialization)
                .Include(t => t.TrainerPackages)
                .FirstOrDefaultAsync(t => t.Id == id);

        public async Task<Trainer?> GetByUserIdAsync(int userId) =>
            await _context.Trainers
                .Include(t => t.TrainerSpecializations)
                    .ThenInclude(ts => ts.Specialization)
                .FirstOrDefaultAsync(t => t.UserId == userId);

        public async Task<Trainer> CreateAsync(Trainer trainer)
        {
            await _context.Trainers.AddAsync(trainer);
            await SaveChangesAsync();
            return trainer;
        }

        public async Task<Trainer?> UpdateAsync(Trainer trainer)
        {
            var existing = await _context.Trainers.FindAsync(trainer.Id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(trainer);
            await SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null) return false;
            _context.Trainers.Remove(trainer);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
