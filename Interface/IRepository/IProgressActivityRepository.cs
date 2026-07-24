using Fitness.Models;

namespace Fitness.Interface.IRepository
{
    public interface IProgressActivityRepository
    {
        Task<IEnumerable<ProgressActivity>> GetByUserIdAsync(int userId);
        Task<ProgressActivity?> GetByIdAsync(int id);
        Task<ProgressActivity> CreateAsync(ProgressActivity progressActivity);
        Task<bool> SaveChangesAsync();
    }
}
