using Fitness.Models;

namespace Fitness.Interface.IService
{
    public interface IProgressActivityService
    {
        Task<IEnumerable<ProgressActivity>> GetByUserIdAsync(int userId);
        Task<ProgressActivity> CreateAsync(ProgressActivity progressActivity);
    }
}
