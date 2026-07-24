using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }

        public int TrainerId { get; set; }

        [ForeignKey("TrainerId")]
        public Trainer? Trainer { get; set; }

        public int? BookingId { get; set; }

        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }

        public DateTime SessionDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Scheduled";

        [MaxLength(1000)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
