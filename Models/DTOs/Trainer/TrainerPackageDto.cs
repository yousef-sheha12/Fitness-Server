namespace Fitness.Models.DTOs.Trainer
{
    public class TrainerPackageDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public int TrainerId { get; set; }
        public bool IsActive { get; set; }
    }
}
