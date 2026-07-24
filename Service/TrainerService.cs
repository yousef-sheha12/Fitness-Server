using Fitness.Interface.IRepository;
using Fitness.Interface.IService;
using Fitness.Models;
using Fitness.Models.DTOs.Trainer;

namespace Fitness.Service
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;

        public TrainerService(ITrainerRepository trainerRepository)
        {
            _trainerRepository = trainerRepository;
        }

        public async Task<IEnumerable<TrainerDto>> GetAllAsync()
        {
            var trainers = await _trainerRepository.GetAllAsync();
            return trainers.Select(MapToDto);
        }

        public async Task<TrainerDetailsDto?> GetByIdAsync(int id)
        {
            var trainer = await _trainerRepository.GetByIdAsync(id);
            if (trainer == null) return null;
            return new TrainerDetailsDto
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Bio = trainer.Bio,
                Location = trainer.Location,
                Rating = trainer.Rating,
                ExperienceYears = trainer.ExperienceYears,
                ProfileImage = trainer.ProfileImage,
                IsApproved = trainer.IsApproved,
                UserId = trainer.UserId,
                SpecializationNames = trainer.TrainerSpecializations?
                    .Select(ts => ts.Specialization?.Name ?? "").ToList() ?? new(),
                Packages = trainer.TrainerPackages?
                    .Select(tp => new TrainerPackageDto
                    {
                        Id = tp.Id,
                        Name = tp.Name,
                        Description = tp.Description,
                        Price = tp.Price,
                        DurationDays = tp.DurationDays,
                        TrainerId = tp.TrainerId,
                        IsActive = tp.IsActive
                    }).ToList() ?? new()
            };
        }

        public async Task<Trainer?> GetByUserIdAsync(int userId) =>
            await _trainerRepository.GetByUserIdAsync(userId);

        public async Task<Trainer> CreateAsync(Trainer trainer)
        {
            await _trainerRepository.CreateAsync(trainer);
            return trainer;
        }

        public async Task<Trainer?> UpdateAsync(int id, Trainer trainer)
        {
            var existing = await _trainerRepository.GetByIdAsync(id);
            if (existing == null) return null;
            existing.Bio = trainer.Bio ?? existing.Bio;
            existing.Location = trainer.Location ?? existing.Location;
            existing.ExperienceYears = trainer.ExperienceYears;
            return await _trainerRepository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(int id) =>
            await _trainerRepository.DeleteAsync(id);

        public async Task<IEnumerable<TrainerDto>> SearchAsync(string searchValue)
        {
            var trainers = await _trainerRepository.GetAllAsync();
            var filtered = trainers.Where(t =>
                t.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                (t.Location != null && t.Location.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                t.TrainerSpecializations.Any(ts =>
                    ts.Specialization != null &&
                    ts.Specialization.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)));
            return filtered.Select(MapToDto);
        }

        private static TrainerDto MapToDto(Trainer trainer) => new()
        {
            Id = trainer.Id,
            Name = trainer.Name,
            Bio = trainer.Bio,
            Location = trainer.Location,
            Rating = trainer.Rating,
            ExperienceYears = trainer.ExperienceYears,
            ProfileImage = trainer.ProfileImage,
            IsApproved = trainer.IsApproved,
            SpecializationNames = trainer.TrainerSpecializations?
                .Select(ts => ts.Specialization?.Name ?? "").ToList() ?? new()
        };
    }
}
