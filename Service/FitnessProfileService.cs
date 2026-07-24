using Fitness.Interface.IRepository;
using Fitness.Interface.IService;
using Fitness.Models;

namespace Fitness.Service
{
    public class FitnessProfileService : IFitnessProfileService
    {
        private readonly IFitnessProfileRepository _repo;
        public FitnessProfileService(IFitnessProfileRepository repo) => _repo = repo;

        public async Task<FitnessProfile?> GetByUserIdAsync(int userId) => await _repo.GetByUserIdAsync(userId);

        public async Task<FitnessProfile> CreateAsync(FitnessProfile fitnessProfile) => await _repo.CreateAsync(fitnessProfile);

        public async Task<FitnessProfile?> UpdateAsync(int userId, FitnessProfile fitnessProfile)
        {
            var existing = await _repo.GetByUserIdAsync(userId);
            if (existing == null)
                return await _repo.CreateAsync(fitnessProfile);

            existing.Weight = fitnessProfile.Weight ?? existing.Weight;
            existing.Height = fitnessProfile.Height ?? existing.Height;
            existing.FitnessGoal = fitnessProfile.FitnessGoal ?? existing.FitnessGoal;
            existing.FitnessLevel = fitnessProfile.FitnessLevel ?? existing.FitnessLevel;
            existing.MedicalConditions = fitnessProfile.MedicalConditions ?? existing.MedicalConditions;
            existing.DietaryPreferences = fitnessProfile.DietaryPreferences ?? existing.DietaryPreferences;
            existing.UpdatedAt = DateTime.UtcNow;
            return await _repo.UpdateAsync(existing);
        }
    }
}
