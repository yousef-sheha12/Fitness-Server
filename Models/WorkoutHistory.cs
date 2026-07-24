using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models
{
    public class WorkoutHistory
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [MaxLength(200)]
        public string WorkoutName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public int DurationMinutes { get; set; }

        public int CaloriesBurned { get; set; }

        public DateTime WorkoutDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
