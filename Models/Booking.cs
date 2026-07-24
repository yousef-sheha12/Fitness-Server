using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int TrainerId { get; set; }

        [ForeignKey("TrainerId")]
        public Trainer? Trainer { get; set; }

        public DateTime BookingDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Pending";

        [MaxLength(500)]
        public string? Notes { get; set; }

        public decimal Amount { get; set; }

        public bool IsPaid { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
