using Fitness.Models;

namespace Fitness.Interface.IRepository
{
    public interface ISpecializationRepository
    {
        Task<IEnumerable<Specialization>> GetAllAsync();
        Task<Specialization?> GetByIdAsync(int id);
        Task<Specialization> CreateAsync(Specialization specialization);
        Task<bool> SaveChangesAsync();
    }
}
