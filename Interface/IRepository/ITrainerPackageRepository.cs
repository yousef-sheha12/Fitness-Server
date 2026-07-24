using Fitness.Models;

namespace Fitness.Interface.IRepository
{
    public interface ITrainerPackageRepository
    {
        Task<IEnumerable<TrainerPackage>> GetAllAsync();
        Task<TrainerPackage?> GetByIdAsync(int id);
        Task<IEnumerable<TrainerPackage>> GetByTrainerIdAsync(int trainerId);
        Task<TrainerPackage> CreateAsync(TrainerPackage trainerPackage);
        Task<TrainerPackage?> UpdateAsync(TrainerPackage trainerPackage);
        Task<bool> DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
