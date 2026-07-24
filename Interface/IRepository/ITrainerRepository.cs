using Fitness.Models;

namespace Fitness.Interface.IRepository
{
    public interface ITrainerRepository
    {
        Task<IEnumerable<Trainer>> GetAllAsync();
        Task<Trainer?> GetByIdAsync(int id);
        Task<Trainer?> GetByUserIdAsync(int userId);
        Task<Trainer> CreateAsync(Trainer trainer);
        Task<Trainer?> UpdateAsync(Trainer trainer);
        Task<bool> DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
