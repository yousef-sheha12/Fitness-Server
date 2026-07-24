using Fitness.Data;
using Fitness.Interface.IRepository;
using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Repository
{
    public class PackagePurchaseRepository : IPackagePurchaseRepository
    {
        private readonly AppDbContext _context;

        public PackagePurchaseRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<PackagePurchase>> GetAllAsync() =>
            await _context.PackagePurchases
                .Include(pp => pp.TrainerPackage)
                .ToListAsync();

        public async Task<PackagePurchase?> GetByIdAsync(int id) =>
            await _context.PackagePurchases
                .Include(pp => pp.TrainerPackage)
                .FirstOrDefaultAsync(pp => pp.Id == id);

        public async Task<IEnumerable<PackagePurchase>> GetByUserIdAsync(int userId) =>
            await _context.PackagePurchases
                .Include(pp => pp.TrainerPackage)
                .Where(pp => pp.UserId == userId)
                .ToListAsync();

        public async Task<PackagePurchase> CreateAsync(PackagePurchase purchase)
        {
            await _context.PackagePurchases.AddAsync(purchase);
            await SaveChangesAsync();
            return purchase;
        }

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
