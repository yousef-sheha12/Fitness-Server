using Fitness.Models;

namespace Fitness.Interface.IService
{
    public interface ISpecializationService
    {
        Task<IEnumerable<Specialization>> GetAllAsync();
        Task<Specialization?> GetByIdAsync(int id);
        Task<Specialization> CreateAsync(Specialization specialization);
    }
}
