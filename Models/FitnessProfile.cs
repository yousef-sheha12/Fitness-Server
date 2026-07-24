using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models
{
    public class FitnessProfile
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public double? Weight { get; set; }

        public double? Height { get; set; }

        [MaxLength(50)]
        public string? FitnessGoal { get; set; }

        [MaxLength(50)]
        public string? FitnessLevel { get; set; }

        [MaxLength(500)]
        public string? MedicalConditions { get; set; }

        [MaxLength(500)]
        public string? DietaryPreferences { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
