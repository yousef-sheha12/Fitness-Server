using Fitness.Interface.IRepository;
using Fitness.Interface.IService;
using Fitness.Models;

namespace Fitness.Service
{
    public class PackagePurchaseService : IPackagePurchaseService
    {
        private readonly IPackagePurchaseRepository _repo;
        public PackagePurchaseService(IPackagePurchaseRepository repo) => _repo = repo;

        public async Task<IEnumerable<PackagePurchase>> GetByUserIdAsync(int userId) => await _repo.GetByUserIdAsync(userId);
        public async Task<PackagePurchase?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task<PackagePurchase> CreateAsync(PackagePurchase purchase) => await _repo.CreateAsync(purchase);
    }
}
