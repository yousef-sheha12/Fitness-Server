using Fitness.Data;
using Fitness.Interface.IRepository;
using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Repository
{
    public class TrainerPackageRepository : ITrainerPackageRepository
    {
        private readonly AppDbContext _context;

        public TrainerPackageRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<TrainerPackage>> GetAllAsync() =>
            await _context.TrainerPackages.ToListAsync();

        public async Task<TrainerPackage?> GetByIdAsync(int id) =>
            await _context.TrainerPackages.FindAsync(id);

        public async Task<IEnumerable<TrainerPackage>> GetByTrainerIdAsync(int trainerId) =>
            await _context.TrainerPackages
                .Where(tp => tp.TrainerId == trainerId)
                .ToListAsync();

        public async Task<TrainerPackage> CreateAsync(TrainerPackage trainerPackage)
        {
            await _context.TrainerPackages.AddAsync(trainerPackage);
            await SaveChangesAsync();
            return trainerPackage;
        }

        public async Task<TrainerPackage?> UpdateAsync(TrainerPackage trainerPackage)
        {
            var existing = await _context.TrainerPackages.FindAsync(trainerPackage.Id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(trainerPackage);
            await SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pkg = await _context.TrainerPackages.FindAsync(id);
            if (pkg == null) return false;
            _context.TrainerPackages.Remove(pkg);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
