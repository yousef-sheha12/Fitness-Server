using Fitness.Models;

namespace Fitness.Interface.IService
{
    public interface IFitnessProfileService
    {
        Task<FitnessProfile?> GetByUserIdAsync(int userId);
        Task<FitnessProfile> CreateAsync(FitnessProfile fitnessProfile);
        Task<FitnessProfile?> UpdateAsync(int userId, FitnessProfile fitnessProfile);
    }
}
