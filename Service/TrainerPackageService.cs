using Fitness.Interface.IRepository;
using Fitness.Interface.IService;
using Fitness.Models;
using Fitness.Models.DTOs.Trainer;

namespace Fitness.Service
{
    public class TrainerPackageService : ITrainerPackageService
    {
        private readonly ITrainerPackageRepository _repo;
        public TrainerPackageService(ITrainerPackageRepository repo) => _repo = repo;

        public async Task<IEnumerable<TrainerPackageDto>> GetAllAsync()
        {
            var packages = await _repo.GetAllAsync();
            return packages.Select(p => new TrainerPackageDto
            {
                Id = p.Id, Name = p.Name, Description = p.Description,
                Price = p.Price, DurationDays = p.DurationDays,
                TrainerId = p.TrainerId, IsActive = p.IsActive
            });
        }

        public async Task<TrainerPackage?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<TrainerPackageDto>> GetByTrainerIdAsync(int trainerId)
        {
            var packages = await _repo.GetByTrainerIdAsync(trainerId);
            return packages.Select(p => new TrainerPackageDto
            {
                Id = p.Id, Name = p.Name, Description = p.Description,
                Price = p.Price, DurationDays = p.DurationDays,
                TrainerId = p.TrainerId, IsActive = p.IsActive
            });
        }

        public async Task<TrainerPackage> CreateAsync(TrainerPackage trainerPackage) => await _repo.CreateAsync(trainerPackage);

        public async Task<TrainerPackage?> UpdateAsync(int id, TrainerPackage trainerPackage)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;
            existing.Name = trainerPackage.Name ?? existing.Name;
            existing.Description = trainerPackage.Description ?? existing.Description;
            existing.Price = trainerPackage.Price;
            existing.DurationDays = trainerPackage.DurationDays;
            return await _repo.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
