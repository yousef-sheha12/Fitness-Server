namespace Fitness.Models.DTOs.Trainer
{
    public class TrainerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public decimal Rating { get; set; }
        public int ExperienceYears { get; set; }
        public string? ProfileImage { get; set; }
        public bool IsApproved { get; set; }
        public List<string> SpecializationNames { get; set; } = new();
    }
}
