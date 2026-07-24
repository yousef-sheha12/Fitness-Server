using Fitness.Models;

namespace Fitness.Interface.IRepository
{
    public interface IFitnessProfileRepository
    {
        Task<FitnessProfile?> GetByUserIdAsync(int userId);
        Task<FitnessProfile> CreateAsync(FitnessProfile fitnessProfile);
        Task<FitnessProfile?> UpdateAsync(FitnessProfile fitnessProfile);
        Task<bool> SaveChangesAsync();
    }
}
