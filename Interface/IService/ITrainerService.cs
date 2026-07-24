using Fitness.Models;
using Fitness.Models.DTOs.Trainer;

namespace Fitness.Interface.IService
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerDto>> GetAllAsync();
        Task<TrainerDetailsDto?> GetByIdAsync(int id);
        Task<Trainer?> GetByUserIdAsync(int userId);
        Task<Trainer> CreateAsync(Trainer trainer);
        Task<Trainer?> UpdateAsync(int id, Trainer trainer);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TrainerDto>> SearchAsync(string searchValue);
    }
}
