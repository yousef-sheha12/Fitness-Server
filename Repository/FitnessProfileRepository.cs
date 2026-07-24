using Fitness.Data;
using Fitness.Interface.IRepository;
using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Repository
{
    public class FitnessProfileRepository : IFitnessProfileRepository
    {
        private readonly AppDbContext _context;

        public FitnessProfileRepository(AppDbContext context) => _context = context;

        public async Task<FitnessProfile?> GetByUserIdAsync(int userId) =>
            await _context.FitnessProfiles.FirstOrDefaultAsync(fp => fp.UserId == userId);

        public async Task<FitnessProfile> CreateAsync(FitnessProfile fitnessProfile)
        {
            await _context.FitnessProfiles.AddAsync(fitnessProfile);
            await SaveChangesAsync();
            return fitnessProfile;
        }

        public async Task<FitnessProfile?> UpdateAsync(FitnessProfile fitnessProfile)
        {
            var existing = await _context.FitnessProfiles.FindAsync(fitnessProfile.Id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(fitnessProfile);
            await SaveChangesAsync();
            return existing;
        }

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
