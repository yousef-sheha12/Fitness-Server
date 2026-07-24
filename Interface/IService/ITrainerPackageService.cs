using Fitness.Models;
using Fitness.Models.DTOs.Trainer;

namespace Fitness.Interface.IService
{
    public interface ITrainerPackageService
    {
        Task<IEnumerable<TrainerPackageDto>> GetAllAsync();
        Task<TrainerPackage?> GetByIdAsync(int id);
        Task<IEnumerable<TrainerPackageDto>> GetByTrainerIdAsync(int trainerId);
        Task<TrainerPackage> CreateAsync(TrainerPackage trainerPackage);
        Task<TrainerPackage?> UpdateAsync(int id, TrainerPackage trainerPackage);
        Task<bool> DeleteAsync(int id);
    }
}
