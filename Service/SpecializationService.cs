using Fitness.Interface.IRepository;
using Fitness.Interface.IService;
using Fitness.Models;

namespace Fitness.Service
{
    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository _repo;
        public SpecializationService(ISpecializationRepository repo) => _repo = repo;

        public async Task<IEnumerable<Specialization>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Specialization?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task<Specialization> CreateAsync(Specialization specialization) => await _repo.CreateAsync(specialization);
    }
}
