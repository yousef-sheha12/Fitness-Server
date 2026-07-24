using Fitness.Models;

namespace Fitness.Interface.IService
{
    public interface IPackagePurchaseService
    {
        Task<IEnumerable<PackagePurchase>> GetByUserIdAsync(int userId);
        Task<PackagePurchase?> GetByIdAsync(int id);
        Task<PackagePurchase> CreateAsync(PackagePurchase purchase);
    }
}
