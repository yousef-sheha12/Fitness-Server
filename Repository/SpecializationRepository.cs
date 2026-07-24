using Fitness.Data;
using Fitness.Interface.IRepository;
using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Repository
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly AppDbContext _context;

        public SpecializationRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Specialization>> GetAllAsync() =>
            await _context.Specializations.ToListAsync();

        public async Task<Specialization?> GetByIdAsync(int id) =>
            await _context.Specializations.FindAsync(id);

        public async Task<Specialization> CreateAsync(Specialization specialization)
        {
            await _context.Specializations.AddAsync(specialization);
            await SaveChangesAsync();
            return specialization;
        }

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
