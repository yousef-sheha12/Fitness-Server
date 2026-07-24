using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models
{
    public class Trainer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Bio { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }

        public decimal Rating { get; set; } = 0;

        public int ExperienceYears { get; set; } = 0;

        [MaxLength(500)]
        public string? ProfileImage { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public bool IsApproved { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TrainerSpecialization> TrainerSpecializations { get; set; } = new List<TrainerSpecialization>();
        public ICollection<TrainerPackage> TrainerPackages { get; set; } = new List<TrainerPackage>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
