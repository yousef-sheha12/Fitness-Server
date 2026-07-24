using Fitness.Models;

namespace Fitness.Interface.IRepository
{
    public interface IPackagePurchaseRepository
    {
        Task<IEnumerable<PackagePurchase>> GetAllAsync();
        Task<PackagePurchase?> GetByIdAsync(int id);
        Task<IEnumerable<PackagePurchase>> GetByUserIdAsync(int userId);
        Task<PackagePurchase> CreateAsync(PackagePurchase purchase);
        Task<bool> SaveChangesAsync();
    }
}
