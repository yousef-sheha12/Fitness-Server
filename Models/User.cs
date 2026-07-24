using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [MaxLength(500)]
        public string? ProfileImage { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsAdmin { get; set; } = false;

        public DateTime? Dob { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<PackagePurchase> PackagePurchases { get; set; } = new List<PackagePurchase>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<FitnessProfile> FitnessProfiles { get; set; } = new List<FitnessProfile>();
        public ICollection<WorkoutHistory> WorkoutHistories { get; set; } = new List<WorkoutHistory>();
    }
}
