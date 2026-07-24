using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models
{
    public class ProgressActivity
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [MaxLength(200)]
        public string ActivityType { get; set; } = string.Empty;

        public double Value { get; set; }

        [MaxLength(50)]
        public string Unit { get; set; } = string.Empty;

        public DateTime ActivityDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
